var BSO = window.BSO || {};

_(BSO).extend(function ($) {
    /*
    var SearchResults = Backbone.Collection.extend({
        __bbType: 'SearchResults',

        model: BSO.EventDetail,
        
        fullDataSet: null,
        dataFilterSet: null,
        _sortKey: null,
        _isAscending: true,

        setSortKey: function(key) {
            if (key === this._sortKey) {
                this._isAscending = !this._isAscending;
            } else {
                this._isAscending = true;
                this._sortKey = key;
            }
        },

        comparator: function(resultOne, resultTwo) {
            if (this._sortKey) {
                if (this._isAscending) {
                    if (resultOne.get(this._sortKey) < resultTwo.get(this._sortKey)) return 1;
                    else if (resultOne.get(this._sortKey) > resultTwo.get(this._sortKey)) return -1;
                    else return 0;
                } else {
                    if (resultOne.get(this._sortKey) > resultTwo.get(this._sortKey)) return 1;
                    else if (resultOne.get(this._sortKey) < resultTwo.get(this._sortKey)) return -1;
                    else return 0;
                }
            }

            return null;
        },

        initialize: function() {
            BSO.FilterGroupSet.prototype.searchResults = this;

            this.dataFilterSet = new BSO.FilterGroupSet();
            this.view = new BSO.SearchTable({ collection: this });
            this.loadFromDOM();

            this.listenTo(this.dataFilterSet, 'add', _(this.didAddFilter).bind(this));
            this.listenTo(this.dataFilterSet, 'remove', _(this.removeFilter).bind(this));
        },

        loadFromDOM: function() {
            var jsonHf = document.getElementById('hfSearchResults');

            if (jsonHf && jsonHf.value) {
                this.fullDataSet = JSON.parse(jsonHf.value);

                var filteredData = _(this.fullDataSet).groupBy(function(result) {
                    return result.EventID;
                }, this);

                _(filteredData).each(function(result, index) {
                    if (_(result).isEmpty() === false) {
                        var newResult = _(result).first(),
                            finalResult = _(result).groupBy(function(results) {
                                return results.WorkTitle2;
                            }, this),
                            flattenedResults = _(finalResult).flatten(),
                            seen = [];

                        newResult.innerResults = _(_(flattenedResults).map(function(result) {
                            var workTitle = result.WorkTitle2;
                            if (_(seen).contains(workTitle) === false) {
                                seen.push(workTitle);
                                return result;
                            }

                            return null;
                        }, this)).compact();

                        filteredData[index] = newResult;
                    }
                }, this);

                this.reset(_(filteredData).flatten());
                jsonHf.value = '';
            }
        },

        loadEventDetails: function () {

        },

        addFilter: function (dataFilter) {
            if ((dataFilter instanceof BSO.DataFilter) === false) {
                return;
            }

            var existingFilter = this.dataFilterSet.findWhere({ key: dataFilter.get('value') });

            if (existingFilter) {
                this.dataFilterSet.remove(existingFilter);
            }

            this.dataFilterSet.add(dataFilter);
        },

        removeFilter: function (filterValue) {
            var existingFilter = this.dataFilterSet.findWhere({ value: filterValue.get('value') });

            if (existingFilter) {
                this.dataFilterSet.remove(existingFilter);
            }
        },

        didAddFilter: function() {
            var filteredData = _(this.fullDataSet).clone();

            if (this.dataFilterSet.length > 0) {
                filteredData = _(filteredData).filter(function(result) {
                    return this.dataFilterSet.any(function(dataFilter) {
                        return dataFilter.get('value') == result[dataFilter.get('key')];
                    }, this);
                }, this);
            }

            filteredData = _(filteredData).groupBy(function(result) {
                return result.EventID;
            }, this);

            _(filteredData).each(function(result, index) {
                if (_(result).isEmpty() === false) {
                    var newResult = _(result).first(),
                        finalResult = _(result).groupBy(function(results) {
                                return results.WorkTitle2;
                            }, this),
                            flattenedResults = _(finalResult).flatten(),
                            seen = [];

                    newResult.innerResults = _(_(flattenedResults).map(function(result) {
                        var workTitle = result.WorkTitle2;
                        if (_(seen).contains(workTitle) === false) {
                            seen.push(workTitle);
                            return result;
                        }

                        return null;
                    }, this)).compact();

                    filteredData[index] = newResult;
                }
            }, this);

            this.reset(_(filteredData).flatten());
            
            this.dataFilterSet.selectedView.render();
        },

        rebindFilters: function() {
            _(this.dataFilterSet.models).each(function (dataFilter) {
                var me = "SomeFilter";
                //dataFilter.model.find('[data-value="' + dataFilter.get('value') + '"]').prop('checked', true);
            }, this);
        }
    });



    return {
        SearchResults: SearchResults
    };
    */
}(jQuery));