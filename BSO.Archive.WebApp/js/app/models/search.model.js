var BSO = window.BSO || {};

_(BSO).extend(function ($) {
    var SearchResults = BSO.Collection.extend({
        initializeModelLoadingCount: function () {
            this.modelsPendingLoad = 0;
        },

        handleAllModelsLoaded: function () {
            this.modelsPendingLoad -= 1;

            if (this.modelsPendingLoad === 0) {
                BSO.Dispatch.trigger('searchModelsParsed');
            }
        }
    });

    var ArtistSearchResults = SearchResults.extend({
        model: BSO.Artist,

        initialize: function () {
        },

        loadAsyncWithIds: function (badString) {
            
            if (badString === "[]") {
                // No Results
                BSO.Dispatch.trigger('noSearchResultsFound');
                return;
            }

            var eventArray = JSON.parse(badString);

            var sizeOfRequest = 500; // Maximum amount of eventDetailIds we want to ask for at once.

            // Counts
            var lastEventIndexLoaded = 0;
            var totalNumberOfEventDetails = _(eventArray).flatten().length;
            var totalNumberOfEventDetailsLoaded = 0;

            this.modelsPendingLoad = 0;

            // While not all the eventDetails are loaded
            while (totalNumberOfEventDetailsLoaded < totalNumberOfEventDetails) {
                // Initialize Counts
                var numberOfEventDetailsThisRequest = 0; // How many details am I requesting?
                var lastEventIdIncludedInThisRequest = 0; // What is the last index in the eventArray that I am requesting? So I know where to start next time.

                for (var i = lastEventIndexLoaded ; i < eventArray.length && (lastEventIdIncludedInThisRequest <= eventArray.length - 1) ; i++) {
                    // For each list of eventDetailIds in eventArray,
                    var eventDetailArr = eventArray[i];

                    // Does the list of eventDetailIds fit in the request?
                    if (numberOfEventDetailsThisRequest + eventDetailArr.length <= sizeOfRequest) {
                        numberOfEventDetailsThisRequest += eventDetailArr.length; // Update the amount of event details in this request
                        lastEventIdIncludedInThisRequest = i + 1; // Mark the last index in the eventArray we want in this request
                    } else {
                        // It ate too many eventDetails. This request is ready.
                        break;
                    }
                }

                // Get the substring events
                var splicedArr = eventArray.slice(lastEventIndexLoaded, lastEventIdIncludedInThisRequest);

                // Keep track of the last index we loaded for next request.
                lastEventIndexLoaded = lastEventIdIncludedInThisRequest;

                // Get the eventDetailIds in a string
                var subArrStr = splicedArr.join(',');

                // Update total number of event details loaded so we don't continue past the amount we need.
                totalNumberOfEventDetailsLoaded += numberOfEventDetailsThisRequest;

                this.modelsPendingLoad += 1;
                this.app.services.loadArtists(subArrStr, this.parseArtists, this);
            }
        },

        parseArtists: function (response) {
            var jsonData = JSON.parse(response.d);

            this.reset(_.map(jsonData, _.bind(function (jsonItem) {
                var newArtistDTO = new BSO.Artist();
                newArtistDTO.buildArtistSearchResultFromJSON(jsonItem);
                return newArtistDTO;
            }, this)));


            this.handleAllModelsLoaded();
        }
    });

    var RepertoireSearchResults = SearchResults.extend({
        model: BSO.Artist,

        initialize: function () {
        },

        loadAsyncWithIds: function (badString) {

            if (badString === "[]") {
                // No Results
                BSO.Dispatch.trigger('noSearchResultsFound');
                return;
            }

            var eventArray = JSON.parse(badString);

            var sizeOfRequest = 500; // Maximum amount of eventDetailIds we want to ask for at once.

            // Counts
            var lastEventIndexLoaded = 0;
            var totalNumberOfEventDetails = _(eventArray).flatten().length;
            var totalNumberOfEventDetailsLoaded = 0;

            this.modelsPendingLoad = 0;

            // While not all the eventDetails are loaded
            while (totalNumberOfEventDetailsLoaded < totalNumberOfEventDetails) {
                // Initialize Counts
                var numberOfEventDetailsThisRequest = 0; // How many details am I requesting?
                var lastEventIdIncludedInThisRequest = 0; // What is the last index in the eventArray that I am requesting? So I know where to start next time.

                for (var i = lastEventIndexLoaded ; i < eventArray.length && (lastEventIdIncludedInThisRequest <= eventArray.length - 1) ; i++) {
                    // For each list of eventDetailIds in eventArray,
                    var eventDetailArr = eventArray[i];

                    // Does the list of eventDetailIds fit in the request?
                    if (numberOfEventDetailsThisRequest + eventDetailArr.length <= sizeOfRequest) {
                        numberOfEventDetailsThisRequest += eventDetailArr.length; // Update the amount of event details in this request
                        lastEventIdIncludedInThisRequest = i + 1; // Mark the last index in the eventArray we want in this request
                    } else {
                        // It ate too many eventDetails. This request is ready.
                        break;
                    }
                }

                // Get the substring events
                var splicedArr = eventArray.slice(lastEventIndexLoaded, lastEventIdIncludedInThisRequest);

                // Keep track of the last index we loaded for next request.
                lastEventIndexLoaded = lastEventIdIncludedInThisRequest;

                // Get the eventDetailIds in a string
                var subArrStr = splicedArr.join(',');

                // Update total number of event details loaded so we don't continue past the amount we need.
                totalNumberOfEventDetailsLoaded += numberOfEventDetailsThisRequest;

                this.modelsPendingLoad += 1;
                this.app.services.loadRepertoires(subArrStr, this.parseRepertoires, this);
            }
        },


        parseRepertoires: function (response) {
            var jsonData = JSON.parse(response.d);

            if (jsonData.length === 0) {
                // No Results
                BSO.Dispatch.trigger('noSearchResultsFound');
                return;
            }

            this.reset(_.map(jsonData, _.bind(function (jsonItem) {
                var newRepertoireDTO = new BSO.Repertoire();
                newRepertoireDTO.parseFromJson(jsonItem);
                return newRepertoireDTO;
            }, this)));

            this.handleAllModelsLoaded();
        }
    });

    var EventSearchResults = SearchResults.extend({
        model: BSO.Event,
        
        loadAsyncWithIds: function (data) {
            // This function breaks eventDetailIds into sets of 500 each for asynchronous network requests

            if (data === "[]") {
                // No Results
                BSO.Dispatch.trigger('noSearchResultsFound');
                return;
            }

            var eventArray = JSON.parse(data);

            var sizeOfRequest = 500; // Maximum amount of eventDetailIds we want to ask for at once.

            // Counts
            var lastEventIndexLoaded = 0;
            var totalNumberOfEventDetails = _(eventArray).flatten().length;
            var totalNumberOfEventDetailsLoaded = 0;

            this.modelsPendingLoad = 0;

            // While not all the eventDetails are loaded
            while (totalNumberOfEventDetailsLoaded < totalNumberOfEventDetails) {
                // Initialize Counts
                var numberOfEventDetailsThisRequest = 0; // How many details am I requesting?
                var lastEventIdIncludedInThisRequest = 0; // What is the last index in the eventArray that I am requesting? So I know where to start next time.

                for (var i = lastEventIndexLoaded ; i < eventArray.length && (lastEventIdIncludedInThisRequest <= eventArray.length - 1); i++) {
                    // For each list of eventDetailIds in eventArray,
                    var eventDetailArr = eventArray[i];

                    // Does the list of eventDetailIds fit in the request?
                    if (numberOfEventDetailsThisRequest + eventDetailArr.length <= sizeOfRequest) {
                        numberOfEventDetailsThisRequest += eventDetailArr.length; // Update the amount of event details in this request
                        lastEventIdIncludedInThisRequest = i+1; // Mark the last index in the eventArray we want in this request
                    } else {
                        // It ate too many eventDetails. This request is ready.
                        break;
                    }
                }

                // Get the substring events
                var splicedArr = eventArray.slice(lastEventIndexLoaded, lastEventIdIncludedInThisRequest);

                // Keep track of the last index we loaded for next request.
                lastEventIndexLoaded = lastEventIdIncludedInThisRequest;

                // Get the eventDetailIds in a string
                var subArrStr = splicedArr.join(',');

                // Update total number of event details loaded so we don't continue past the amount we need.
                totalNumberOfEventDetailsLoaded += numberOfEventDetailsThisRequest;

                this.modelsPendingLoad += 1;
                this.app.services.loadPerformances(subArrStr, this.parseEvents, this);
            }
        },
        
        parseEvents: function (response) {
            var jsonData = JSON.parse(response.d);

            this.add(_.map(jsonData, _.bind(function (jsonItem) {
                var newEventDTO = new BSO.Event();
                newEventDTO.parseFromJson(jsonItem);
                return newEventDTO;
            }, this)));

            this.handleAllModelsLoaded();
        }
    });

    var EventDetail = BSO.Model.extend({
        __bbType: 'EventDetail',

        initialize: function () {
            this.rowView = new BSO.SearchTableRow({ model: this });
        }
    });

    return {
        EventDetail: EventDetail,
        SearchResults: SearchResults,
        EventSearchResults: EventSearchResults,
        ArtistSearchResults: ArtistSearchResults,
        RepertoireSearchResults: RepertoireSearchResults
    };
}(jQuery));