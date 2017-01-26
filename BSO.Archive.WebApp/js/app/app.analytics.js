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
            dataLayer.push({ 'event': 'GAevent', 'eventCategory': 'archives form', 'eventAction': 'click', 'eventLabel': 'search' });

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
                    dataLayer.push({ 'event': 'GAevent', 'eventCategory': 'archives form', 'eventAction': 'perform ' + tabName.toLowerCase() + ' search ' + searchCriteria, 'eventLabel': searchTerm });

                }
            });

            $('select', $tabArea).each(function (index, element) {
                var searchCriteria = $(element).siblings('span').html();
                var searchTerm = $(element).val();
                //searchTerms.push(searchCriteria + "=" + searchTerm);

                if (searchTerm !== "") {
                    dataLayer.push({ 'event': 'GAevent', 'eventCategory': 'archives form', 'eventAction': 'perform ' + tabName.toLowerCase() + ' search composer' + searchCriteria, 'eventLabel': searchTerm });
                }
            });
        },

        trackRepertoireDetailPageClick: function ($target) {
            dataLayer.push({ 'event': 'GAevent', 'eventCategory': 'archives my history', 'eventAction': 'repertoire search click', 'eventLabel': $target.attr('href') });
        },

        trackPerformanceDetailPageClick: function ($target) {
            dataLayer.push({ 'event': 'GAevent', 'eventCategory': 'archives my history', 'eventAction': 'performance search click', 'eventLabel': $target.attr('href') });

        },

        trackArtistDetailPageClick: function ($target) {
            dataLayer.push({ 'event': 'GAevent', 'eventCategory': 'archives my history', 'eventAction': 'artist search click', 'eventLabel': $target.attr('href') });
        },

        trackDetailPageShareClick: function () {
            dataLayer.push({ 'event': 'GAevent', 'eventCategory': 'archives my history', 'eventAction': 'click', 'eventLabel': 'share results' });
        },

        trackExportClick: function () {
            dataLayer.push({ 'event': 'GAevent', 'eventCategory': 'archives results', 'eventAction': 'click', 'eventLabel': 'export xls' });

        },

        trackPerformanceHistoryClick: function () {
            dataLayer.push({ 'event': 'GAevent', 'eventCategory': 'archives form', 'eventAction': 'click', 'eventLabel': 'search performance history' });
        },

        trackProgramImagesClick: function () {
            dataLayer.push({ 'event': 'GAevent', 'eventCategory': 'archives form', 'eventAction': 'click', 'eventLabel': 'program notes' });
        },

        trackSearchHistoryClick: function () {
            dataLayer.push({ 'event': 'GAevent', 'eventCategory': 'archives form', 'eventAction': 'click', 'eventLabel': 'my search history' });
        },

        trackClearSearch: function () {
            dataLayer.push({ 'event': 'GAevent', 'eventCategory': 'archives form', 'eventAction': 'click', 'eventLabel': 'clear search' });
        },

        trackDetailsIconClick: function () {
            dataLayer.push({ 'event': 'GAevent', 'eventCategory': 'archives results', 'eventAction': 'click', 'eventLabel': 'more detail' });
        },

        trackProgramBookClick: function () {
            dataLayer.push({ 'event': 'GAevent', 'eventCategory': 'archives results', 'eventAction': 'click', 'eventLabel': 'pdf' });
        },

        trackSearchTabClick: function (currentSearchType) {
            // Google Analytics Code goes here for tracking search TAB clicks
            // "currentSearchType" is either Performance/Artist/Repertoire based on the tab you clicked.
            dataLayer.push({ 'event': 'GAevent', 'eventCategory': 'archives form', 'eventAction': 'click', 'eventLabel': currentSearchType + ' Search' });
        },

        // Detail Pages
        click_detailPageComposerLink: function (evt) {
            var $target = $(evt.currentTarget);

            // link data
            var linkData = $target.attr('href');

            // link title
            var linkTitle = $target.html();

            dataLayer.push({ 'event': 'GAevent', 'eventCategory': 'archives detail', 'eventAction': 'click composer', 'eventLabel': linkTitle + ' ' + linkData });
        },

        click_detailPageWorkLink: function (evt) {
            var $target = $(evt.currentTarget);

            // link data
            var linkData = $target.attr('href');

            // link title
            var linkTitle = $target.html();

            dataLayer.push({ 'event': 'GAevent', 'eventCategory': 'archives detail', 'eventAction': 'click work', 'eventLabel': linkTitle + ' ' + linkData });
        },

        click_detailPageArtistLink: function (evt) {
            var $target = $(evt.currentTarget);

            // link data
            var linkData = $target.attr('href');

            // link title
            var linkTitle = $target.html();

            dataLayer.push({ 'event': 'GAevent', 'eventCategory': 'archives detail', 'eventAction': 'click artist', 'eventLabel': linkTitle + ' ' + linkData });
        },

        click_detailPageRoleLink: function (evt) {
            var $target = $(evt.currentTarget);

            // link data
            var linkData = $target.attr('href');

            // link title
            var linkTitle = $target.html();

            dataLayer.push({ 'event': 'GAevent', 'eventCategory': 'archives detail', 'eventAction': 'click role', 'eventLabel': linkTitle + ' ' + linkData });
        },

        click_detailPageDateLink: function (evt) {
            var $target = $(evt.currentTarget);

            // link data
            var linkData = $target.attr('href');

            // link title
            var linkTitle = $target.html();

            dataLayer.push({ 'event': 'GAevent', 'eventCategory': 'archives detail', 'eventAction': 'click date', 'eventLabel': linkTitle + ' ' + linkData });
        },

        click_detailPageSeasonLink: function (evt) {
            var $target = $(evt.currentTarget);

            // link data
            var linkData = $target.attr('href');

            // link title
            var linkTitle = $target.html();

            dataLayer.push({ 'event': 'GAevent', 'eventCategory': 'archives detail', 'eventAction': 'click season', 'eventLabel': linkTitle + ' ' + linkData });
        },


        click_detailPageEnsembleLink: function (evt) {
            var $target = $(evt.currentTarget);

            // link data
            var linkData = $target.attr('href');

            // link title
            var linkTitle = $target.html();

            dataLayer.push({ 'event': 'GAevent', 'eventCategory': 'archives detail', 'eventAction': 'click ensemble', 'eventLabel': linkTitle + ' ' + linkData });
        },


        click_detailPageConductorLink: function (evt) {
            var $target = $(evt.currentTarget);

            // link data
            var linkData = $target.attr('href');

            // link title
            var linkTitle = $target.html();

            dataLayer.push({ 'event': 'GAevent', 'eventCategory': 'archives detail', 'eventAction': 'click conductor', 'eventLabel': linkTitle + ' ' + linkData });

        },

        click_detailPageVenueLink: function (evt) {
            var $target = $(evt.currentTarget);

            // link data
            var linkData = $target.attr('href');

            // link title
            var linkTitle = $target.html();

            dataLayer.push({ 'event': 'GAevent', 'eventCategory': 'archives detail', 'eventAction': 'click venue', 'eventLabel': linkTitle + ' ' + linkData });
        },

        click_detailPageLocationLink: function (evt) {
            var $target = $(evt.currentTarget);

            // link data
            var linkData = $target.attr('href');

            // link title
            var linkTitle = $target.html();

            dataLayer.push({ 'event': 'GAevent', 'eventCategory': 'archives detail', 'eventAction': 'click location', 'eventLabel': linkTitle + ' ' + linkData });
        },

        click_detailPageEventLink: function (evt) {
            var $target = $(evt.currentTarget);

            // link data
            var linkData = $target.attr('href');

            // link title
            var linkTitle = $target.html();

            dataLayer.push({ 'event': 'GAevent', 'eventCategory': 'archives detail', 'eventAction': 'click event', 'eventLabel': linkTitle + ' ' + linkData });
        }
    });

    return {
        Analytics: Analytics
    };
}(jQuery));