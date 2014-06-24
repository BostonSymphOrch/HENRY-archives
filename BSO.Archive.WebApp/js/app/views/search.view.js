var BSO = window.BSO || {};
_(BSO).extend(function ($) {
    var SearchResultTableView = BSO.View.extend({
        __bbType: 'SearchTable',

        // TEMPLATE IS NOT DEFINED. Custom Render Function in place

        initialize: function () {
            this.debouncedRender = _.debounce(this.renderFnc, 100);

            BSO.Dispatch.on('noSearchResultsFound', this.showNoResultsFound, this);

            BSO.Dispatch.on('didStartNetworkCalls', this.didStartNetworkCalls, this);
            BSO.Dispatch.on('didFinishNetworkCalls', this.didFinishNetworkCalls, this);

            BSO.Dispatch.on('didGenerateFilters', this.renderFnc, this);

            BSO.Dispatch.on('refreshResultsArea', this.renderFnc, this);
        },

        events: {
            "click [data-bb=sortableTH]": "didClickSortByTableColumn",
            "click .icon-search": "trackDetailsIconClick",
            "click .searchPDF": "trackProgramBookClick",
        },

        
        trackDetailsIconClick: function (evt) {
            this.app.analytics.trackDetailsIconClick();
        },

        trackProgramBookClick: function (evt) {
            this.app.analytics.trackProgramBookClick();
        },

        didClickSortByTableColumn: function (evt) {
            evt.stopPropagation();
            var $target = $(evt.currentTarget);

            var $arrow = $target.find('span.triangleSort').first();

            var sortOption = _.findWhere(BSO.SortConfig.SortingOptions, { name: $target.attr('data-sortOptionName') });

            var sortType = $target.attr('data-sortType');
            var prependStr;


            if (typeof (sortOption) != "undefined") {
                // Sort Config Options Saved for after reset.
                var wasActive = sortOption.active;
                var wasDescending = sortOption.doDescending;

                _(BSO.SortConfig.resetSort).bind(BSO.SortConfig)(); // Remove any previous active sorting configurations

                // Sort Option Exists
                if (wasActive) {
                    if (wasDescending) {
                        // Current state is actively descending sorting. 
                        // Switch to: Inactive ascending.
                        sortOption.active = false;
                        sortOption.doDescending = false;
                    } else {
                        // Current state: Actively Ascending Sorting
                        // Switch to:  Actively Descending Sorting
                        sortOption.active = true;
                        sortOption.doDescending = true;
                    }
                } else {
                    // Current state is disabled.
                    // Switch to: active ascending sorting
                    sortOption.active = true;
                    sortOption.doDescending = false;
                }
            }

            // Re-render results
            this.render();
        },

        getCurrentTableTemplate: function () {
            var searchType = this.app.getCurrentSearchType();

            if (searchType === "Performance")
                return Handlebars.templates.performanceTable;
            else if (searchType === "Artist")
                return Handlebars.templates.artistTable;
            else if (searchType === "Repertoire")
                return Handlebars.templates.repertoireTable;
        },

        willRender: function () {
            this.showLoadingGif();
        },

        renderFnc: function () {
            this.willRender();
            this.el.innerHTML = (this.getCurrentTableTemplate())(this.viewModel());
            this.didRender();
        },

        render: function () {
            this.debouncedRender();
        },

        didRender: function () {
            // manually adds the sort triangles so there isn't ten lines of this each in the handlebars
            this.$el.find('[data-bb=sortableTH]').find('span.triangleSort').html('<img src="/images/trianglenosort.png"  height="10px" width="10px"  alt="image of a triangle" class="nosort" /><img src="/images/triangleup.png"  height="10px" width="10px"  alt="image of a triangle" class="up" /><img src="/images/triangle.png"  height="10px" width="10px"  alt="image of a triangle" class="down" />');

            this.addTriangleSortClassesFromConfig();

            this.hideLoadingGif();
        },

        addTriangleSortClassesFromConfig: function () {
            _.each(BSO.SortConfig.SortingOptions, function (sortOption) {
                var $found = this.$el.find('[data-bb=sortableTH][data-sortOptionName=' + sortOption.name + ']');
                if ($found.length !== 0) {
                    var $triangle = $found.find('span.triangleSort').first();

                    if (sortOption.active) {
                        if (sortOption.doDescending) {
                            $triangle.addClass('sortDescending');
                        } else {
                            $triangle.addClass('sortAscending');
                        }
                    }
                }
            }, this);
        },

        showLoadingGif: function () {
            $('#preloaderFish').show();
        },

        hideLoadingGif: function () {
            $('#preloaderFish').hide();
        },

        didStartNetworkCalls: function () {
            this.showLoadingGif();
        },

        didFinishNetworkCalls: function () {
            this.hideLoadingGif();
        },


        hasResults: true,

        showNoResultsFound: function () {
            this.hasResults = false;
            this.render();
        },

        viewModel: function () {
            var rawResults = this.app.filterContext.getResultsThatApplyToSelectedFilters(this.app.searchResults);
            
            if (rawResults.length >= this.app.maxResultShown)
                rawResults = rawResults.slice(0, this.app.maxResultShown);

            var searchType = this.app.getCurrentSearchType();

            if (searchType === "Performance") { // Special Performance Visible Works
                _(rawResults).map(function (event) {
                    event.visibleWorks = event.works.filter(function (work) {
                        return this.app.filterContext.isWorkInSelectedFilters(work);
                    }, this);
                }, this);
            }

            return {
                hasResults: this.hasResults,
                results: rawResults
            };
        }
    });

    return {
        SearchResultTableView: SearchResultTableView
    };
}(jQuery));