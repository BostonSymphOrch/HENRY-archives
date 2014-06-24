var BSO = window.BSO || {};

_(BSO).extend(function ($) {
    var DataFilterView = Backbone.View.extend({
        __bbType: 'DataFilterView',

        template: Handlebars.templates.selectedDataFilter,

        render: function () {
            this.el.innerHTML = this.template(this.model.attributes);
            return this;
        }
    });

    var SelectedDataFilterView = DataFilterView.extend({
        __bbType: 'SelectedDataFilterView',

        template: Handlebars.templates.selectedDataFilter
    });

    var DataFilterSetView = Backbone.View.extend({
        __bbType: 'DataFilterSetView',

        initialize: function () {
            this.setElement(document.querySelector('[data-bb="dataFilterSetView"]'));
        },

        render: function () {
            var fragment = document.createDocumentFragment();

            this.collection.each(function (dataFilter) {
                fragment.appendChild(dataFilter.view.render().el);
            }, this);

            this.el.innerHTML = '';
            this.el.appendChild(fragment);
        }
    });

    var SelectedDataFilterSetView = DataFilterSetView.extend({
        __bbType: 'SelectedDataFilterSetView',

        template: Handlebars.templates.selectedFilters,

        initialize: function () {
            this.setElement(document.querySelector('[data-bb="selectedDataFilterSetView"]'));
        },

        events: {
            'click .remove': 'didClickRemove'
        },

        didClickRemove: function (evt) {
            var $target = $(evt.currentTarget);
            var value = $target.data('value');
            var match = this.collection.findWhere({ value: value });
            if (match) {
                this.collection.remove(match);

                if (match.view) {
                    match.view.deselectByValue(match.get('value'));
                }

                // this.collection.removeFilter(match);
                evt.stopPropagation();
            }
        },

        render: function () {
            var groupedCollection = {
                groups: _(this.collection.models).groupBy(function (model) {
                    return model.get('key');
                }, this)
            };
            console.log(groupedCollection);
            if (this.el)
                this.el.innerHTML = this.template(groupedCollection);
        }
    });

    var FilterSetView = Backbone.View.extend({
        __bbType: 'FilterSetView',

        initialize: function () {
            this.setElement(document.querySelector('[data-bb="filterResults"]'));
            this.listenTo(this.collection, 'reset', _(this.render).bind(this));
        },

        render: function () {
            var fragment = document.createDocumentFragment();

            this.collection.each(function (filter) {
                fragment.appendChild(filter.view.render().el);
            }, this);

            this.el.innerHTML = '';
            this.el.appendChild(fragment);

            return this;
        }
    });

    var FilterSelect = Backbone.View.extend({
        __bbType: 'FilterSelect',

        initialize: function () {
            $(document).on('mousedown', '#container', _(this.closeFilter).bind(this));
        },

        closeFilter: function (evt) {
            var $target = $(evt.target);
            if ($target.parents('#filterResults').length === 0 && $target.attr('id') !== 'filterResults') {
                $('.filterOptions').hide();
                $('.active').removeClass('active');
                evt.stopPropagation();
            }
        },

        template: Handlebars.templates.filterSelect,

        tagName: 'li',

        events: {
            'click .filterToggle': 'didClickToggle',
            'click input[type="checkbox"]': 'didClickCheckbox',
            'change [data-bb="selectAll"]': 'didClickCheckAll',
            'click .filterOptions': 'didClickFilterOptions'
        },

        didClickFilterOptions: function (evt) {
            evt.stopPropagation();
        },

        didClickCheckAll: function (evt) {
            var checkAll = this.$el.find('[data-bb=selectAll]')[0];
            var checkboxes = this.$el.find('input[type=checkbox]');
            var shouldCheckAll = checkAll.checked;
            var value;
            _(checkboxes).each(function (cbElement) {
                value = cbElement.parentNode.innerText;
                value = _.str.trim(value);
                if (shouldCheckAll) {
                    $(cbElement).prop('checked', true);
                    if (value !== " Select All") {
                        var dataFilter = this.model.dataFilterSet.findWhere({ value: value });
                        if (dataFilter) {
                            this.searchResults.addFilter(dataFilter);
                        }
                    }
                }
                else {
                    $(cbElement).prop('checked', false);
                    if (value !== " Select All") {
                        this.searchResults.removeFilter(value);
                    }
                }
            }, this);
            this.updateFilterState();
            //this.didClickCheckbox(evt);
        },

        didClickToggle: function (evt) {
            var $target = $(evt.currentTarget);
            var $myFilterOptions = this.$el.find('.filterOptions');
            $('.filterOptions').not($myFilterOptions).hide();
            $myFilterOptions.toggle();
            $('.active').not($target).removeClass('active');
            $target.toggleClass('active');
        },

        deselectByValue: function (value) {
            var checkboxes = this.$el.find('input[type="checkbox"]'),
                checkBoxMatchingValue = _(checkboxes).filter(function (box) {
                    return $(box).data('value') === value;
                }, this);

            if (checkBoxMatchingValue) {
                $(checkBoxMatchingValue).prop('checked', false);
            }

            this.updateFilterState();
        },

        didClickCheckbox: function (evt) {
            var $target = $(evt.currentTarget);
            var value = $target.data('value');

            if ($target.prop('checked')) {
                var dataFilter = this.model.dataFilterSet.findWhere({ value: value });

                if (!dataFilter) {
                    this.model.dataFilterSet.add(dataFilter = new BSO.DataFilter({
                        key: this.model.get('key'),
                        value: value
                    }));
                    dataFilter.view = this;
                }

                this.searchResults.addFilter(dataFilter);
            } else {
                this.searchResults.removeFilter(value);
            }
            this.updateFilterState();
        },

        updateFilterState: function () {

            var hasFilterChecked = this.$el.find('.filterOptions input:checked').length;
            console.log(hasFilterChecked);
            if (hasFilterChecked) {
                this.$el.find(".filterToggle").css({ "background": "#4c0065" });
            }
            else {
                this.$el.find(".filterToggle").css({ "background": "#666" });
            }
        },

        render: function () {
            this.el.innerHTML = this.template(this.model.attributes);
            return this;
        }
    });

    var SearchTable = Backbone.View.extend({
        __bbType: 'SearchTable',

        filters: null,

        initialize: function () {
            this.setElement(document.querySelector('[data-bb="searchResults"]'));
            this.listenTo(this.collection, 'reset', _(this.render).bind(this));
            this.listenTo(this.collection, 'sort', _(this.render).bind(this));
        },

        events: {
            'click [data-sort]': 'didClickSort'
        },

        didClickSort: function (evt) {
            var $target = $(evt.currentTarget);
            var sortKey = $target.data('sort');

            if (sortKey) {
                this.collection.setSortKey(sortKey);
                this.collection.sort();
            }
        },

        render: function () {
            var $tbody = this.$el.find('tbody');
            var fragment = document.createDocumentFragment();

            this.collection.each(function (searchResult) {
                fragment.appendChild(searchResult.rowView.render().el);
            }, this);

            $tbody.empty();
            $tbody.append(fragment);

            this.renderFilters();

            return this;
        },

        filterCategories: {
            'ArtistFullName': 'Artist',
            'Instrument1': 'Instrument',
            'OrchestraName': 'Orchestra',
            'ComposerFullName': 'Composer',
            'WorkTitle2' : 'Work',
            'EventConductorFullName': 'Conductor',
            'EventArtistFullName': 'Soloist',
            'ParticipantFullName': 'Participant'
        },

        renderFilters: _(function () {
            this.filters = new BSO.FilterSet();

            this.filters.reset(
                _(_(this.filterCategories).map(_(function (categoryName, categoryKey) {
                    category = _(this.collection.map(function (model) {
                        if (model.attributes.innerResults) {
                            return _(model.attributes.innerResults).pluck(categoryKey);
                        }
                    })).flatten();

                    if (category.length === 1 && _(category[0]).isObject()) {
                        category = _(category[0]).map(function (value, key) { return key; });
                    }

                    var unique = _(category).uniq();

                    if (unique && unique.length > 0) {
                        return new BSO.Filter({
                            name: categoryName,
                            options: _(unique).compact(),
                            key: categoryKey
                        });
                    } else {
                        return null;
                    }
                }).bind(this))).compact()
            );
        }).once()
    });

    var SearchTableRow = Backbone.View.extend({
        __bbType: 'SearchTableRow',

        tagName: 'tr',

        
        template: Handlebars.templates.artistTableRow,

        render: function () {
            this.el.innerHTML = this.template(this.model.attributes);
            return this;
        }
    });

    return {
        SelectedDataFilterView: SelectedDataFilterView,
        SelectedDataFilterSetView: SelectedDataFilterSetView,
        SearchTableRow: SearchTableRow,
        SearchTable: SearchTable,
        FilterSetView: FilterSetView,
        FilterSelect: FilterSelect,
        DataFilterView: DataFilterView,
        DataFilterSetView: DataFilterSetView
    };
}(jQuery));