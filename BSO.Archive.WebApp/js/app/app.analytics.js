var BSO = window.BSO || {};

_(BSO).extend(function ($) {
    var Analytics = Backbone.View.extend({
        el: $('body'),

        initialize: function () {

        },

        // Detail Page Events
        events: {
            "click .detailPageComposerLink": "click_detailPageComposerLink",
            "click .detailPageWorkLink": "click_detailPageWorkLink",
            "click .detailPageArtistLink": "click_detailPageArtistLink",
            "click .detailPageRoleLink": "click_detailPageRoleLink",
            "click .detailPageDateLink": "click_detailPageDateLink",

            "click .detailPageSeasonLink": "click_detailPageSeasonLink",
            "click .detailPageEnsembleLink": "click_detailPageEnsembleLink",
            "click .detailPageConductorLink": "click_detailPageConductorLink",
            "click .detailPageLocationLink": "click_detailPageLocationLink",
            "click .detailPageEventLink": "click_detailPageEventLink",
            "click .detailPageVenueLink": "click_detailPageVenueLink"
        },

        trackSearch: function (tabName, $tabArea) {
            _gaq.push(['_trackEvent', 'archives form', 'click', 'search']);

            this.trackSearchTerms(tabName, $tabArea);
        },

        trackSearchTerms: function (theTabName, $tabArea) {
            //var searchTerms = [];
            var tabName = theTabName;

            $('input[type=text]', $tabArea).each(function (index, element) {
                var searchCriteria = $(element).siblings('span').html();
                var searchTerm = $(element).val();
                //searchTerms.push(searchCriteria + "=" + searchTerm);

                if (searchTerm !== "") {
                    _gaq.push(['_trackEvent', 'archives form', 'perform ' + tabName.toLowerCase() + ' search ' + searchCriteria, searchTerm]);
                }
            });

            $('select', $tabArea).each(function (index, element) {
                var searchCriteria = $(element).siblings('span').html();
                var searchTerm = $(element).val();
                //searchTerms.push(searchCriteria + "=" + searchTerm);

                if (searchTerm !== "") {
                    _gaq.push(['_trackEvent', 'archives form', 'perform ' + tabName.toLowerCase() + ' search composer', searchCriteria + ': ' + searchTerm]);
                }
            });
        },

        trackRepertoireDetailPageClick: function ($target) {
            _gaq.push(['_trackEvent', 'archives my history', 'repertoire search click', $target.attr('href')]);
        },

        trackPerformanceDetailPageClick: function ($target) {
            _gaq.push(['_trackEvent', 'archives my history', 'performance search click', $target.attr('href')]);
        },

        trackArtistDetailPageClick: function ($target) {
            _gaq.push(['_trackEvent', 'archives my history', 'artist search click', $target.attr('href')]);
        },

        trackDetailPageShareClick: function () {
            _gaq.push(['_trackEvent', 'archives my history', 'click', 'share results']);
        },

        trackExportClick: function () {
            _gaq.push(['_trackEvent', 'archives results', 'click', 'export xls']);

        },

        trackPerformanceHistoryClick: function () {
            _gaq.push(['_trackEvent', 'archives form', 'click', 'search performance history']);
        },

        trackProgramImagesClick: function () {
            _gaq.push(['_trackEvent', 'archives form', 'click', 'program notes']);
        },

        trackSearchHistoryClick: function () {
            _gaq.push(['_trackEvent', 'archives form', 'click', 'my search history']);
        },

        trackClearSearch: function () {
            _gaq.push(['_trackEvent', 'archives form', 'click', 'clear search']);
        },

        trackDetailsIconClick: function () {
            _gaq.push(['_trackEvent', 'archives results', 'click', 'more detail']);
        },

        trackProgramBookClick: function () {
            _gaq.push(['_trackEvent', 'archives results', 'click', 'pdf']);
        },

        trackSearchTabClick: function (currentSearchType) {
            // Google Analytics Code goes here for tracking search TAB clicks
            // "currentSearchType" is either Performance/Artist/Repertoire based on the tab you clicked.
            _gaq.push(['_trackEvent', 'archives form', 'click', currentSearchType + ' Search']);
        },

        // Detail Pages
        click_detailPageComposerLink: function (evt) {
            var $target = $(evt.currentTarget);

            // link data
            var linkData = $target.attr('href');

            // link title
            var linkTitle = $target.html();

            _gaq.push(['_trackEvent', 'archives detail', 'click composer', linkTitle + ' ' + linkData]);
        },

        click_detailPageWorkLink: function (evt) {
            var $target = $(evt.currentTarget);

            // link data
            var linkData = $target.attr('href');

            // link title
            var linkTitle = $target.html();

            _gaq.push(['_trackEvent', 'archives detail', 'click work', linkTitle + ' ' + linkData]);
        },

        click_detailPageArtistLink: function (evt) {
            var $target = $(evt.currentTarget);

            // link data
            var linkData = $target.attr('href');

            // link title
            var linkTitle = $target.html();

            _gaq.push(['_trackEvent', 'archives detail', 'click artist', linkTitle + ' ' + linkData]);
        },
        
        click_detailPageRoleLink: function (evt) {
            var $target = $(evt.currentTarget);

            // link data
            var linkData = $target.attr('href');

            // link title
            var linkTitle = $target.html();

            _gaq.push(['_trackEvent', 'archives detail', 'click role', linkTitle + ' ' + linkData]);
        },
                
        click_detailPageDateLink: function (evt) {
            var $target = $(evt.currentTarget);

            // link data
            var linkData = $target.attr('href');

            // link title
            var linkTitle = $target.html();

            _gaq.push(['_trackEvent', 'archives detail', 'click date', linkTitle + ' ' + linkData]);
        },
        
        click_detailPageSeasonLink: function (evt) {
            var $target = $(evt.currentTarget);

            // link data
            var linkData = $target.attr('href');

            // link title
            var linkTitle = $target.html();

            _gaq.push(['_trackEvent', 'archives detail', 'click season', linkTitle + ' ' + linkData]);
        },


        click_detailPageEnsembleLink: function (evt) {
            var $target = $(evt.currentTarget);

            // link data
            var linkData = $target.attr('href');

            // link title
            var linkTitle = $target.html();

            _gaq.push(['_trackEvent', 'archives detail', 'click ensemble', linkTitle + ' ' + linkData]);
        },


        click_detailPageConductorLink: function (evt) {
            var $target = $(evt.currentTarget);

            // link data
            var linkData = $target.attr('href');

            // link title
            var linkTitle = $target.html();

            _gaq.push(['_trackEvent', 'archives detail', 'click conductor', linkTitle + ' ' + linkData]);
        },
        
        click_detailPageVenueLink: function (evt) {
            var $target = $(evt.currentTarget);

            // link data
            var linkData = $target.attr('href');

            // link title
            var linkTitle = $target.html();

            _gaq.push(['_trackEvent', 'archives detail', 'click venue', linkTitle + ' ' + linkData]);
        },

        click_detailPageLocationLink: function (evt) {
            var $target = $(evt.currentTarget);

            // link data
            var linkData = $target.attr('href');

            // link title
            var linkTitle = $target.html();

            _gaq.push(['_trackEvent', 'archives detail', 'click location', linkTitle + ' ' + linkData]);
        },

        click_detailPageEventLink: function (evt) {
            var $target = $(evt.currentTarget);

            // link data
            var linkData = $target.attr('href');

            // link title
            var linkTitle = $target.html();

            _gaq.push(['_trackEvent', 'archives detail', 'click event', linkTitle + ' ' + linkData]);
        }
    });

    return {
        Analytics: Analytics
    };
}(jQuery));