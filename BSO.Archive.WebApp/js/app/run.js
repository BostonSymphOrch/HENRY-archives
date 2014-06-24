viewtest = [];

head.js(
   
    // libraries
    "/js/lib/underscore-min.js",
    "/js/lib/underscore.string.js",
    "/js/lib/backbone-min.js",
    "/js/lib/handlebars.js",
    "/js/lib/ie8compliance.js",
    "/js/lib/jquery.easytabs.js",
    "/js/lib/googleAnalytics.js",
    "/js/lib/FileSaver.js",

    // templates
    "/js/app/app.templates.js",
    "/js/app/app.handlebars.js",
    
    // BASE
    "/js/app/app.base.js",

    // DTOs
    "/js/app/models/app.dtos.js",

    // Filters
    "/js/app/config/filterConfig.js",
    "/js/app/models/app.filterContext.js",

    // services
    "/js/app/app.analytics.js",

    // services
    "/js/app/services/app.services.js",

    // models
    "/js/app/models/filter.model.js",
    "/js/app/models/search.model.js",

    // collections
    "/js/app/collections/search.collection.js",

    // views
    "/js/app/views/filters.view.js",
    "/js/app/views/search.view.js",

    // app
    "/js/app/web_archive.js",

    function () {
        $('#tab-container').easytabs();

        $('#tab-container').bind('easytabs:midTransition', function (e) {
            //e.preventDefault();
            //e.stopPropagation();
            //return false;
        });

  

        window.bso = new BSO.App({ el: document.body });
    });

    $(window).scroll(function () {
        scrolled = true;
    });


$(document).ready(function () {


    $('.etabs a').on('click', function (e) {
    

        if (location.hash) {
            window.scrollTo(0, 0);
        }

    });


});