var BSO = window.BSO || {};

_(BSO).extend(function ($) {
    var App = BSO.View.extend({
        initialize: function () {
            this.maxResultShown = parseInt($("#MaxSearchResults").val());

            BSO.View.prototype.app = this;
            BSO.Model.prototype.app = this;
            BSO.Collection.prototype.app = this;

            this.services = new BSO.WebServices();

            this.analytics = new BSO.Analytics();

            this.filterContext = new BSO.ArchiveContext();

            this.initViews();

            this.loadInitialIdsFromDOM();
        },

        events: {
            "click .SearchButton": "didClickSearchButton",
            "click #clearSearchButton": "clearSearch",
            "click .tab": "clickedTab",
            "click #performanceHistoryLink": "trackPerformanceHistoryClick",
            "click #programImagesLink": "trackProgramImagesClick",
            "click #searchHistoryLink": "trackSearchHistoryClick",

            "click .repertoireHistoryItemLink": "trackRepertoireHistoryPageClick",
            "click .performanceHistoryItemLink": "trackPerformanceHistoryPageClick",
            "click .artistHistoryItemLink": "trackArtistHistoryPageClick",
            "click .searchHistoryShareLink" : "trackSearchHistoryShareClick"
        },

        trackSearchHistoryShareClick: function (evt) {
            this.analytics.trackDetailPageShareClick();
        },

        trackRepertoireHistoryPageClick: function (evt) {
            this.analytics.trackRepertoireDetailPageClick($(evt.currentTarget));
        },

        trackPerformanceHistoryPageClick: function (evt) {
            this.analytics.trackPerformanceDetailPageClick($(evt.currentTarget));
        },

        trackArtistHistoryPageClick: function (evt) {
            this.analytics.trackArtistDetailPageClick($(evt.currentTarget));
        },

        trackPerformanceHistoryClick: function (evt) {
            this.analytics.trackPerformanceHistoryClick();
        },

        trackProgramImagesClick: function (evt) {
            this.analytics.trackProgramImagesClick();
        },

        trackSearchHistoryClick: function (evt) {
            this.analytics.trackSearchHistoryClick();
        },

        clickedTab: function (evt) {
            this.analytics.trackSearchTabClick(this.getCurrentSearchType());
        },

        didClickSearchButton: function (evt) {
            $('#preloaderFish').show();
            this.analytics.trackSearch(this.getCurrentSearchType(), this.getCurrentFormFieldArea());
        },

        clearSearch: function (evt) {
            this.analytics.trackClearSearch();

            $("input[type=text]").val("");
            $("select").val("");

            this.searchResults.reset();
            this.filterContext.clearFilters();
            this.resultsView.clearView();
            this.filtersView.clearView();
        },

        initViews: function () {
            this.resultsView = new BSO.SearchResultTableView({ el: this.getBBElement(this.getCurrentSearchResultView()) });
            this.filtersView = new BSO.FilterAreaView({ el: this.getBBElement(this.getCurrentSearchFilterView()) });
        },

        loadInitialIdsFromDOM: function () {
            var jsonHf = document.getElementById(this.getCurrentSearchIdElementName());

            this.initializeSearchResults();

            if (jsonHf && jsonHf.value) {
                //var eventIds = JSON.parse(jsonHf.value);
                this.searchResults.loadAsyncWithIds(jsonHf.value);
            }
        },

        getCurrentSearchResultView: function () {
            var searchType = this.getCurrentSearchType();

            if (searchType === "Performance")
                return "performanceTableDiv";
            else if (searchType === "Artist")
                return "artistTableDiv";
            else if (searchType === "Repertoire")
                return "repertoireTableDiv";

            return "performanceTableDiv";
        },


        getCurrentFormFieldArea: function () {
            var searchType = this.getCurrentSearchType();

            if (searchType === "Performance")
                return $("#tabs-performance").first();
            else if (searchType === "Artist")
                return $("#tabs-artist").first();
            else if (searchType === "Repertoire")
                return $("#tabs-repertoire").first();

            return $("#tabs-performance").first();
        },



        getCurrentSearchFilterView: function () {
            var searchType = this.getCurrentSearchType();

            if (searchType === "Performance")
                return "performanceFilterDiv";
            else if (searchType === "Artist")
                return "artistFilterDiv";
            else if (searchType === "Repertoire")
                return "repertoireFilterDiv";

            return "performanceFilterDiv";
        },

        getCurrentSearchIdElementName: function () {
            // Gah this needs to be one common element server-side but its all in different controls :|
            var searchType = this.getCurrentSearchType();

            if (searchType === "Performance")
                return "hfPerformanceSearchResultIds";
            else if (searchType === "Artist")
                return "hfArtistSearchResultIds";
            else if (searchType === "Repertoire")
                return "hfRepertoireSearchResultIds";

            return "hfPerformanceSearchResultIds";
        },

        initializeSearchResults: function () {
            var searchType = this.getCurrentSearchType();

            if (searchType === "Performance")
                this.searchResults = new BSO.EventSearchResults();
            else if (searchType === "Artist")
                this.searchResults = new BSO.ArtistSearchResults();
            else if (searchType === "Repertoire")
                this.searchResults = new BSO.RepertoireSearchResults();
        },

        getCurrentSearchType: function () {
            var domTab = $('.tab.active').first();

            if (domTab.attr('id') === "performanceTab")
                return "Performance";
            else if (domTab.attr('id') === "artistTab")
                return "Artist";
            else if (domTab.attr('id') === "repertoireTab")
                return "Repertoire";

            return "Performance";
        }
    });

    return {
        App: App,
        Dispatch: _(Backbone.Events).clone()
    };
}(jQuery));