var BSO = window.BSO || {};

_(BSO).extend(function ($) {
    var DTO = BSO.Model.extend({
        matchesFilter: function (filter) {
            var resultOfGetValue = filter.filterConfig.memoizedGet(this);

            if (filter.filterConfig.isArrayOfResults) {
                if (resultOfGetValue.indexOf(filter.get('value')) < 0)
                    return false;
            } else {
                if (resultOfGetValue != filter.get('value'))
                    return false;
            }
            return true;
        }

    });

    var DTOList = BSO.Collection.extend({
    });

    var Artist = DTO.extend({
        initialize: function () {
            this.instruments = new InstrumentList();
        },

        buildFromJSON: function (jsonData) {
            this.set('ArtistFullName', jsonData.ArtistFullName);
            this.set('ArtistInstrument', jsonData.ArtistInstrument);
            this.set('ConductorCount', jsonData.ConductorCount);
            this.set('SoloistCount', jsonData.SoloistCount);
            this.set('EnsembleCount', jsonData.EnsembleCount);
            this.set('ConductorLink', jsonData.ConductorLink);
            this.set('SoloistLink', jsonData.SoloistLink);
            this.set('OrchestraLink', jsonData.OrchestraLink);
        },

        buildArtistSearchResultFromJSON: function (jsonData) {
            this.set('ArtistFullName', jsonData.ArtistFullName);
            this.set('WorkTitle', jsonData.work.WorkTitle);
            this.set('ComposerFullName', jsonData.work.ComposerFullName);
            this.set('ConductorCount', jsonData.ConductorCount);
            this.set('SoloistCount', jsonData.SoloistCount);
            this.set('EnsembleCount', jsonData.EnsembleCount);
            this.set('ConductorLink', jsonData.ConductorLink);
            this.set('SoloistLink', jsonData.SoloistLink);
            this.set('OrchestraLink', jsonData.OrchestraLink);

            this.instruments.buildFromJSON(jsonData.artistInstruments);
        }
    });

    var Document = DTO.extend({
        buildFromJSON: function (jsonData) {
            this.set('WorkDocumentName', jsonData.WorkDocumentName);
            this.set('WorkDocumentSummary', jsonData.WorkDocumentSummary);
            this.set('WorkDocumentNotes', jsonData.WorkDocumentNotes);
            this.set('WorkDocumentFileLocation', jsonData.WorkDocumentFileLocation);
        }
    });

    var Instrument = DTO.extend({
        buildFromJSON: function (jsonData) {
            // TODO: FILL OUT INITIALIZATION
            this.set('Instrument', jsonData.Instrument1);
        }
    });

    var InstrumentList = DTOList.extend({
        model: Instrument,

        buildFromJSON: function (jsonData) {
            _.each(jsonData, _.bind(function (instrument) {
                var newInstrum = new Instrument();
                newInstrum.buildFromJSON(instrument);
                this.add(newInstrum);
            }, this));
        }
    });

    var ArtistList = DTOList.extend({
        model: Artist,

        comparator: function (artist1, artist2) {
            var artistSort = _.findWhere(BSO.SortConfig.SortingOptions, { name: "ArtistListSort" });

            return artistSort.doCompare(artist1, artist2);
        },

        buildFromJSON: function (jsonData) {
            _.each(jsonData, _.bind(function (artistData) {
                var newArtist = new Artist();
                newArtist.buildFromJSON(artistData);
                this.add(newArtist);
            }, this));
        }
    });

    var DocumentList = DTOList.extend({
        model: Document,

        buildFromJSON: function (jsonData) {
            _.each(jsonData, _.bind(function (documentData) {
                var newDocument = new Document();
                newDocument.buildFromJSON(documentData);
                this.add(newDocument);
            }, this));
        }
    })

    var Conductor = DTO.extend({
        buildFromJSON: function (jsonData) {
            this.set('ConductorFullName', jsonData.ConductorFullName);
        }
    });

    var ConductorList = DTOList.extend({
        model: Conductor,

        buildFromJSON: function (jsonData) {
            _.each(jsonData, _.bind(function (itemData) {
                var newChild = new Conductor();
                newChild.buildFromJSON(itemData);
                this.add(newChild);
            }, this));
        }
    });

    var Work = DTO.extend({
        buildFromJSON: function (jsonData) {
            this.set('WorkTitle', jsonData.WorkTitle);
            this.set('ComposerFullName', jsonData.ComposerFullName);
            this.set('Arranger', jsonData.Arranger);

            this.artists = new ArtistList();
            this.artists.buildFromJSON(jsonData.WorkArtists);

            this.documents = new DocumentList();
            this.documents.buildFromJSON(jsonData.WorkDocuments);
        }
    });

    var WorkList = DTOList.extend({
        model: Work,

        
        getFirstArtistFullNameForSort: function () {
            var allArtistFullNames = [];

            this.map(function (work) {
                if (work.artists.length > 0) {
                    allArtistFullNames.push(work.artists.at(0).get('ArtistFullName'));
                }
            });

            return allArtistFullNames.sort()[0];
        },

        buildFromJSON: function (jsonData) {
            _.each(jsonData, _.bind(function (itemData) {
                var newChild = new Work();
                newChild.buildFromJSON(itemData);
                this.add(newChild);
            }, this));
        }
    });

    var Venue = DTO.extend({
        buildFromJSON: function (jsonData) {
            this.set('VenueCity', jsonData.VenueCity.trim());
            this.set('VenueState', jsonData.VenueState.trim());
            this.set('VenueCountry', jsonData.VenueCountry.trim());
            this.set('VenueName', jsonData.VenueName.trim());
            this.set('VenueLocation', jsonData.VenueLocation.trim());
        }
    });

    var VenueList = DTOList.extend({
        model: Venue,

        buildFromJSON: function (jsonData) {
            _.each(jsonData, _.bind(function (itemData) {
                var newChild = new Venue();
                newChild.buildFromJSON(itemData);
                this.add(newChild);
            }, this));
        }
    });

    var Orchestra = DTO.extend({
        buildFromJSON: function (jsonData) {
            this.set('OrchestraName', jsonData.OrchestraName);
        }
    });

    var OrchestraList = DTOList.extend({
        model: Orchestra,

        buildFromJSON: function (jsonData) {
            _.each(jsonData, _.bind(function (itemData) {
                var newChild = new Orchestra();
                newChild.buildFromJSON(itemData);
                this.add(newChild);
            }, this));
        }
    });

    var Season = DTO.extend({
        buildFromJSON: function (jsonData) {
            this.set('SeasonName', jsonData.SeasonName);
        }
    });

    var SeasonList = DTOList.extend({
        model: Season,

        buildFromJSON: function (jsonData) {
            _.each(jsonData, _.bind(function (seasonData) {
                var newSeason = new Season();
                newSeason.buildFromJSON(seasonData);
                this.add(newSeason);
            }, this));
        }
    });

    var Repertoire = DTO.extend({
        initialize: function () { },

        parseFromJson: function (jsonData) {
            this.set('ComposerFullName', jsonData.ComposerFullName);
            this.set('Arranger', jsonData.Arranger);
            this.set('WorkTitle', jsonData.WorkTitle);
            this.set('WorkID', jsonData.workID);
            this.set('SoloistCount', jsonData.SoloistCount);
            this.set('ParticipantCount', jsonData.ParticipantCount);
            this.set('PerformanceCount', jsonData.PerformanceCount);
            this.set('WorkLink', jsonData.WorkLink);
        }
    });

    var Event = DTO.extend({
        initialize: function () {
            this.artists = new ArtistList();
            this.works = new WorkList();
            this.workDocuments = new DocumentList();
            this.orchestra = new Orchestra();
            this.season = new Season();
            this.venue = new Venue();
            this.conductor = new Conductor();
        },
        
        buildFromEventId: function (eventId) {
            this.set('EventId', eventId);
            this.loadAllDataAsync(); // Only a list of Ids
        },

        loadAllDataAsync: function () {
            this.app.services.loadPerformance(this.get('EventId'), this.parseAsyncEventDetails, this);
        },

        buildFromJSON: function (dto) {
            this.set('EventProgramTitle', dto.EventProgramTitle);
            this.set('EventDate', dto.EventDateString);
            this.set('EventStartTime', dto.EventStartTime);
            this.set('EventEndTime', dto.EventEndTime);
            this.set('EventProgramNo', dto.EventProgramNo);
            this.set('DetailLink', dto.DetailLink);
            this.set('ProgramBookLink', dto.ProgramBookLink);

            this.loadAdditionalDataAsync(); // This would work when we have 
        },

        loadAdditionalDataAsync: function () {
        },

        parseAsyncEventDetails: function (dto) {
            var jsonData = JSON.parse(dto.d);
            this.parseFromJson(jsonData);
        },
        
        getFirstArtistFullNameForSort: function () {
            return this.works.getFirstArtistFullNameForSort();
        },

        parseFromJson: function (jsonData) {
            this.set('EventProgramTitle', jsonData.EventProgramTitle);
            this.set('EventDate', jsonData.EventDateString);
            this.set('EventStartTime', jsonData.EventStartTime);
            this.set('EventEndTime', jsonData.EventEndTime);
            this.set('EventProgramNo', jsonData.EventProgramNo);
            this.set('EventId', jsonData.EventID);
            this.set('DetailLink', jsonData.DetailLink);
            this.set('ProgramBookLink', jsonData.ProgramBookLink);

            this.artists.buildFromJSON(jsonData.artists);
            this.works.buildFromJSON(jsonData.works);
            this.workDocuments.buildFromJSON(jsonData.workDocuments);
            this.conductor.buildFromJSON(jsonData.conductor);
            this.venue.buildFromJSON(jsonData.venue);
            this.orchestra.buildFromJSON(jsonData.orchestra);
            this.season.buildFromJSON(jsonData.season);
        }
    });

    return {
        Event: Event,
        Artist: Artist,
        ArtistList: ArtistList,
        Conductor: Conductor,
        ConductorList: ConductorList,
        Work: Work,
        WorkList: WorkList,
        Venue: Venue,
        VenueList: VenueList,
        Orchestra: Orchestra,
        OrchestraList: OrchestraList,
        Season: Season,
        SeasonList: SeasonList,
        Repertoire: Repertoire,
        Instrument: Instrument,
        InstrumentList: InstrumentList,
        Document: Document,
        DocumentList: DocumentList
    };
}(jQuery));