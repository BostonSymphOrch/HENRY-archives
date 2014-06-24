var BSO = window.BSO || {};

_(BSO).extend(function ($) {
    var Filter = BSO.Model.extend({
        defaults: {
            category: "DEFAULT_CAT",
            key: "DEFAULT_KEY",
            value: "DEFAULT_VALUE",
            active: false
        }
    });

    var FilterSet = BSO.Collection.extend({
        model: Filter,
        
        comparator: function(item) {
            return item.get('value');
        },

        getActiveFilters: function () {
            return this.filter(function (filter) {
                return filter.get('active');
            }, this);
        }
});

var FilterGroup = BSO.Model.extend({
    defaults: {
        displayName: "A CATEGORY"
    },

    initialize: function () {
        this.filters = new FilterSet();
    },

    addFilterToGroup: function (category, key, value, filtConfig) {
        var newFilt = new BSO.Filter();

        newFilt.filterConfig = filtConfig;
        newFilt.set('category', category);
        newFilt.set('key', key);
        newFilt.set('value', value);

        this.filters.add(newFilt);
    },

    setFilterActiveBySearch: function (category, key, value, active) {
        var theFilter = this.filters.findWhere({
            category: category,
            key: key,
            value: value
        });

        console.log("This filter ", theFilter, " set from ", theFilter.get('active'), " to ", active);
        theFilter.set('active', active);

    },

    getFilterByValue: function (value) {
        return this.filters.findWhere({ value: value });
    },

    getActiveFilters: function () {
        return this.filters.where({ active: true });
    }
});

var FilterGroupSet = BSO.Collection.extend({
    model: FilterGroup, // This class isnt used right now because YOU CANT NEST COLLECTIONS.. GAH!

    initialize: function () {
    },

    setFilterActiveBySearch: function (category, key, value, active) {
        var filtGroup = this.getFilterGroupWithCategory(category);

        filtGroup.setFilterActiveBySearch(category, key, value, active);
    },

    getFilterGroupWithCategory: function (category) {
        var filtGroup = this.find(_.bind(function (group) {
            return group.Category == category;
        }, this));

        return filtGroup;
    },

    getActiveFilters: function () {
        return this.filter(function (group) {
            if (group.getActiveFilters().length > 0)
                return true;

            return false;
        }, this);
    },

    getNumberOfActiveFilters: function () {
        return this.getActiveFilters().length;
    }
});

return {
    FilterSet: FilterSet,
    Filter: Filter,
    FilterGroup: FilterGroup,
    FilterGroupSet: FilterGroupSet
};
}(jQuery));