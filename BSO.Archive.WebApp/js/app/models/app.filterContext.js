var BSO = window.BSO || {};

_(BSO).extend(function ($) {
    var ArchiveContext = BSO.Model.extend({
        initialize: function () {
            this.allFilters = new BSO.FilterGroupSet();
        },

        clearFilters: function () {
            this.allFilters.reset();
        },

        initializeAllFilters: function () {
            this.allFilters = this.generateFilters(this.app.searchResults);
        },

        setFilterActiveBySearch: function (category, key, value, active) {
            this.allFilters.setFilterActiveBySearch(category, key, value, active);
        },

        generateFilters: function (searchResults) {
            // Creates ALL filters based on initial results from load.
            return this.generateFiltersFromConfig(this.getFilterConfiguration(), searchResults);
        },

        getFilterConfiguration: function () {
            var type = this.app.getCurrentSearchType();

            if (type == "Performance")
                return BSO.FilterConfig.PerformanceFilters;
            else if (type == "Artist")
                return BSO.FilterConfig.ArtistFilters;
            else if (type == "Repertoire")
                return BSO.FilterConfig.RepertoireFilters;
        },

        generateFiltersFromConfig: function (filterConfigList, allResults) {
            var newFilterGroupSet = new BSO.FilterGroupSet();

            newFilterGroupSet.reset(_.map(filterConfigList, _.bind(function (filterConfig) {
                // Execute GetValue for Each Search Result, and Make a filter.

                // Group of filters for the category
                var newFilterGroup = new BSO.FilterGroup();
                newFilterGroup.Category = filterConfig.displayName;

                var arrayToFilterFrom = allResults;

                if (typeof (allResults.models) !== "undefined") // Sometimes the input is a collection, sometimes it is the previously-filtered array. This handles both.
                    arrayToFilterFrom = allResults.models;

                _.each(arrayToFilterFrom, function (searchResult) {

                    var resultOfGetValue = filterConfig.memoizedGet(searchResult);

                    if (filterConfig.isArrayOfResults) {
                        _.map(resultOfGetValue, _.bind(function (result) {
                            if (!newFilterGroup.filters.findWhere({ value: result }) && typeof (result) != "undefined" && result.trim() !== "")
                                newFilterGroup.addFilterToGroup(filterConfig.displayName, filterConfig.key, result, filterConfig);
                        }, this));
                    } else {
                        if (!newFilterGroup.filters.findWhere({ value: resultOfGetValue }) && typeof (resultOfGetValue) != "undefined" && resultOfGetValue.trim() !== "")
                            newFilterGroup.addFilterToGroup(filterConfig.displayName, filterConfig.key, resultOfGetValue, filterConfig);
                    }
                }, this);

                return newFilterGroup;
            }, this)));

            return newFilterGroupSet;
        },

        getSelectedFilters: function () {
            // Returns filters from ALLFILTERS that are active.
            return this.allFilters.getActiveFilters();
        },

        getAvailableFilters: function () {
            // Generates filters from search results
            return this.overrideActiveFilters(this.generateFilters(this.getResultsThatApplyToSelectedFilters(this.app.searchResults)));
        },

        overrideActiveFilters: function (filtersToOverride) {
            var selectedFilters = this.getSelectedFilters();

            _.each(selectedFilters, function (selectedFilterGroup) {
                var matchingFilterGroup = filtersToOverride.find(function (filterGroup) {
                    return (filterGroup.Category === selectedFilterGroup.Category);
                });

                if (!!matchingFilterGroup) {
                    selectedFilterGroup.filters.each(function (selectedFilter) {
                        var foundFilterMatch = matchingFilterGroup.filters.findWhere({ key: selectedFilter.get('key'), value: selectedFilter.get('value') });

                        if (!!foundFilterMatch)
                            foundFilterMatch.set('active', selectedFilter.get('active'));
                    });
                }
            }, this);

            return filtersToOverride;
        },

        isWorkInSelectedFilters: function (workDTO) {
            //var workDTO = workDTO || null;
            var selectedFilters = this.getSelectedFilters()

            if (selectedFilters.length === 0) return true;

            var foundOne = _.find(selectedFilters, function (selectedFilterGroup) {
                if (selectedFilterGroup.Category == "Work") {
                    return _.find(selectedFilterGroup.filters.where({ active: true }), function (selectedFilter) {
                        if (workDTO.get(selectedFilter.get('key')) === selectedFilter.get('value'))
                            return true;
                    }, this);

                    return false;
                } else {
                    return true;
                }
            }, this);


            if (typeof (foundOne) !== "undefined")
                return true;

            return false;
        },

        getResultsThatApplyToSelectedFilters: function (searchResults) {
            var matches = [];
            var selectedFilters = this.getSelectedFilters();

            var resultArr = [];

            if (selectedFilters.length === 0) {
                resultArr = searchResults.models;
            } else {
                _.each(selectedFilters, function (selectedFilterGroup) {
                    var categoryResults = [];

                    _.each(selectedFilterGroup.filters.where({ active: true }), function (selectedFilter) {
                        var filterResults = searchResults.filter(function (dto) {
                            return dto.matchesFilter(selectedFilter);
                        });

                        categoryResults.push(filterResults);
                    }, this);

                    matches.push(_(categoryResults).chain().flatten().uniq().value());
                }, this);

                _.each(matches, function (categoryResult) {
                    if (resultArr.length == 0)
                        resultArr = categoryResult;

                    resultArr = _.intersection(resultArr, categoryResult);
                });
            }

            var applicableSorts = _.where(BSO.SortConfig.SortingOptions, { active: true, isNestedListSort: undefined });
            var applicableNestedSorts = _.where(BSO.SortConfig.SortingOptions, { isNestedListSort: true });

            if (applicableSorts.length == 0 && _.where(applicableNestedSorts, { active: true }).length == 0) {
                var defaultOption = this.getDefaultSortingOption();
                applicableSorts.push(defaultOption);
                BSO.SortConfig.setDefaultActive(defaultOption.name);
            }

            _.map(applicableSorts, function (sortConfig) {
                resultArr = resultArr.sort(_(sortConfig.sortFn).bind(sortConfig));
            }, this);

            if (this.app.getCurrentSearchType() === "Performance") {
                _.each(resultArr, function (result) {
                    // Removed to retain work order
                    //result.works.sort();

                    _.each(result.works.models, function (work) {
                        work.artists.sort();
                    });
                });

                // Manual Re-sort based on nested sorts.
                var artistSort = _.findWhere(BSO.SortConfig.SortingOptions, { name: "ArtistListSort" });
                var workSort = _.findWhere(BSO.SortConfig.SortingOptions, { name: "WorkListSort" });

                if (artistSort.active || workSort.active) {
                    resultArr = _.sortBy(resultArr, function (result) {
                        if (workSort.active) {
                            if (result.works.length > 0)
                                return result.works.at(0).get('ComposerFullName');
                        } else if (artistSort.active) {
                            return result.getFirstArtistFullNameForSort();
                        }

                        return "";
                    }, this);
                }
            }

            this.filteredResultCount = resultArr.length;
            return resultArr;
        },

        getDefaultSortingOption: function () {
            var searchType = this.app.getCurrentSearchType();

            if (searchType === "Performance")
                return _.findWhere(BSO.SortConfig.SortingOptions, { name: "SortPerformanceByDate" });
            else if (searchType === "Artist")
                return _.findWhere(BSO.SortConfig.SortingOptions, { name: "SortArtistByName" });
            else if (searchType === "Repertoire")
                return _.findWhere(BSO.SortConfig.SortingOptions, { name: "SortRepertoireByComposer" });
        }
    });

    return {
        ArchiveContext: ArchiveContext
    };
}(jQuery));