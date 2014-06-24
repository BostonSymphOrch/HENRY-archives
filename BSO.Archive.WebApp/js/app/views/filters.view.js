var BSO = window.BSO || {};

_(BSO).extend(function ($) {
    var FilterAreaView = BSO.View.extend({
        template: Handlebars.templates.filterArea,

        initialize: function () {
            this.showFilterArea = false;

            BSO.Dispatch.on('searchModelsParsed', this.generateInitialFilters, this);

            BSO.Dispatch.on('refreshResultsArea', this.render, this);
        },

        events: {
            "click [data-bb=filterToggle]": "didToggleFilterDropdown",
            "change [data-bb=filterOptionItemCheckbox]": "didClickFilterOption",
            "click [data-bb=removeFilterButton]": "didClickRemoveFilter",
            "click [data-bb=selectedFilterValue]": "didClickRemoveFilter",
            "click [data-bb=selectAllFilterOptions]": "didClickSelectAllFilterOptions",
            "click .exportResults": "didClickExport",
            "click #filterAreaShowHideButton": "didClickShowHideButton",
            "click .shareSingleResult" : "showEmailShareDialog"
        },

        filtersHaveChanged: true,

        showEmailShareDialog: function () {
            $(".modalWrap").fadeIn(150);
        },
        
        generateInitialFilters: function () {
            this.app.filterContext.initializeAllFilters();
            this.render();

            BSO.Dispatch.trigger('didGenerateFilters');
        },

        didClickShowHideButton: function (evt) {
            var filterDiv = $("#filterBackground");
            
            var isShown = filterDiv.css('display') !== 'none';

            if (isShown) {
                this.showFilterArea = false;
                filterDiv.slideUp();
            } else {
                this.showFilterArea = true;
                filterDiv.slideDown();
            }
        },

        didClickSelectAllFilterOptions: function (evt) {
            evt.stopPropagation();
            var $target = $(evt.currentTarget);

            this.filtersHaveChanged = true;
            $target.closest('[data-bb=filterOptionsList]').find('[data-bb=filterOptionItemCheckbox]').prop('checked', $target.prop('checked'));
            $target.closest('[data-bb=filterOptionsList]').find('[data-bb=filterOptionItemCheckbox]').change();
        },

        didClickRemoveFilter: function (evt) {
            evt.stopPropagation();
            var $target = $(evt.currentTarget);

            var category = $target.attr('data-category');
            var key = $target.attr('data-key');
            var value = $target.attr('data-value');

            this.filtersHaveChanged = true;
            this.app.filterContext.setFilterActiveBySearch(category, key, value, false); // Force false for remove

            BSO.Dispatch.trigger('refreshResultsArea');
        },

        didClickFilterOption: function (evt) {
            evt.stopPropagation();

            var $target = $(evt.currentTarget);
            var category = $target.attr('data-category');
            var key = $target.attr('data-key');
            var value = $target.attr('data-value');

            this.filtersHaveChanged = true;
            this.app.filterContext.setFilterActiveBySearch(category, key, value, $target.prop('checked'));
        },
        
        didClickExport: function (evt) {
            this.app.analytics.trackExportClick();

            // Render function: this.template(this.viewModel()) with extended XLS template variable
            var resultFullHtmlString = (this.app.resultsView.getCurrentTableTemplate())(_.extend(this.app.resultsView.viewModel(), { exportXLS: true }));
            var $resultHtml = $(resultFullHtmlString);

            
            var $result = _.findWhere($resultHtml, { tagName: 'TABLE' });

            $('.triangleSort', $result).remove();
            $('td[data-bb=programBook]', $result).remove();
            $('th[data-bb=programBook]', $result).remove();

            var html = $result.outerHTML;

            this.app.services.SaveExportDataToSession(html);
        },


        didToggleFilterDropdown: function (evt) {
            evt.stopPropagation();
            var $target = $(evt.currentTarget);

            this.toggleNearestOptionList($target);
        },

        hideOptionLists: function () {
            var $allDropdowns = this.$el.find('[data-bb=filterOptionsList]');
            $allDropdowns.removeClass('active');

            var $allToggles = this.$el.find('[data-bb=filterToggle]');
            $allToggles.removeClass("searchFilterSelected").removeClass("active");
        },

        toggleNearestOptionList: function ($target) {
            var $dropdown = $target.siblings('[data-bb=filterOptionsList]').first();
            var alreadyActive = $dropdown.hasClass('active');

            this.hideOptionLists();

            if (alreadyActive) {
                if (this.filtersHaveChanged) {
                    BSO.Dispatch.trigger('refreshResultsArea');
                }
            } else {
                $dropdown.addClass('active');

                $target.addClass("active");
                $target.addClass("searchFilterSelected");
            }
        },


        render: function () {
            if (this.filtersHaveChanged === true) {
                this.trigger('willRender', this);
                this.el.innerHTML = this.template(this.viewModel());
                this.trigger('didRender', this);
                this.filtersHaveChanged = false;
            }
        },
        
        viewModel: function () {
            return {
                showFilterArea: this.showFilterArea,
                availableFilters: this.app.filterContext.getAvailableFilters(),
                selectedFilters: this.app.filterContext.getSelectedFilters(),

                resultCount: this.app.filterContext.filteredResultCount,
                overMax: (this.app.filterContext.filteredResultCount > this.app.maxResultShown),
                maxResults: this.app.maxResultShown
            };
        }
    });

    var SelectedFilterGroupsView = BSO.View.extend({
        template: Handlebars.template.selectedFilters,

        initialize: function () {
            this.filterGroups = new BSO.FilterGroupSet();
        },

        viewModel: function () {
            return {
                filterGroups: this.filterGroups
            };
        }
    });

    return {
        FilterAreaView: FilterAreaView
    };
}(jQuery));