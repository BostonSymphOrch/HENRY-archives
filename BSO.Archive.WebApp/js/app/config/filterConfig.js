BSO.FilterConfig = {
    PerformanceFilters: [
        {
            displayName: "Composer",
            key: "ComposerFullName",
            isArrayOfResults: true,
            memoizedGet: _.memoize(function (someObj) {
                return this.getValue(someObj);
            }, function (someObj) {
                return this.key + "-_-" + someObj.cid;
            }),
            getValue: function (perf) {
                var arrayOfResults = perf.works.map(_.bind(function (work) {
                    if (work.get(this.key) != "")
                        return work.get(this.key);
                }, this));

                return arrayOfResults;
            }
        },

        {
            displayName: "Work",
            key: "WorkTitle",
            isArrayOfResults: true,
            getValue: function (perf) {
                var arrayOfResults = perf.works.map(_.bind(function (work) {
                    if (work.get(this.key) != "")
                        return work.get(this.key);
                }, this));

                return arrayOfResults;
            },
            memoizedGet: _.memoize(function (someObj) {
                return this.getValue(someObj);
            }, function (someObj) {
                return this.key + "-_-" + someObj.cid;
            })
        },


        {
            displayName: "Orchestra",
            key: "OrchestraName",
            isArrayOfResults: false,
            getValue: function (perf) {
                if (perf.orchestra.get(this.key) != "")
                    return perf.orchestra.get(this.key);
            },
            memoizedGet: _.memoize(function (someObj) {
                return this.getValue(someObj);
            }, function (someObj) {
                return this.key + "-_-" + someObj.cid;
            })
        },

        {
            displayName: "Conductor",
            key: "ConductorFullName",
            isArrayOfResults: false,
            getValue: function (perf) {
                if (perf.conductor.get(this.key) != "")
                    return perf.conductor.get(this.key);
            },
            memoizedGet: _.memoize(function (someObj) {
                return this.getValue(someObj);
            }, function (someObj) {
                return this.key + "-_-" + someObj.cid;
            })
        },

        {
            displayName: "Soloist",
            key: "ArtistFullName",
            isArrayOfResults: true,
            getValue: function (perf) {
                var arrayOfResults = perf.works.map(_.bind(function (work) {
                    var results = work.artists.map(_.bind(function (artist) {
                        if (artist.get(this.key) != "")
                            return artist.get(this.key);
                    }, this));

                    return results;
                }, this));

                var combinedArray = [];

                arrayOfResults.map(_.bind(function (result) {
                    return result.map(_.bind(function (innerResult) {
                        combinedArray.push(innerResult);
                    }, this));
                }, this));

                return combinedArray;
            },
            memoizedGet: _.memoize(function (someObj) {
                return this.getValue(someObj);
            }, function (someObj) {
                return this.key + "-_-" + someObj.cid;
            })
        },

        {
            displayName: "Season",
            key: "SeasonName",
            isArrayOfResults: false,
            getValue: function (perf) {
                if (perf.season.get(this.key) != "")
                    return perf.season.get(this.key);
            },
            memoizedGet: _.memoize(function (someObj) {
                return this.getValue(someObj);
            }, function (someObj) {
                return this.key + "-_-" + someObj.cid;
            })
        },

        {
            displayName: "Venue",
            key: "VenueName",
            isArrayOfResults: false,
            getValue: function (perf) {
                if (perf.venue.get(this.key) != "")
                    return perf.venue.get(this.key);
            },
            memoizedGet: _.memoize(function (someObj) {
                return this.getValue(someObj);
            }, function (someObj) {
                return this.key + "-_-" + someObj.cid;
            })
        },

        {
            displayName: "City",
            key: "VenueCity",
            isArrayOfResults: false,
            getValue: function (perf) {
                if (perf.venue.get(this.key) != "")
                    return perf.venue.get(this.key);
            },
            memoizedGet: _.memoize(function (someObj) {
                return this.getValue(someObj);
            }, function (someObj) {
                return this.key + "-_-" + someObj.cid;
            })
        },

        {
            displayName: "State",
            key: "VenueState",
            isArrayOfResults: false,
            getValue: function (perf) {
                if (perf.venue.get(this.key) != "")
                    return perf.venue.get(this.key);
            },
            memoizedGet: _.memoize(function (someObj) {
                return this.getValue(someObj);
            }, function (someObj) {
                return this.key + "-_-" + someObj.cid;
            })
        },
        {
            displayName: "Country",
            key: "VenueCountry",
            isArrayOfResults: false,
            getValue: function (perf) {
                if (perf.venue.get(this.key).trim() != "")
                    return perf.venue.get(this.key);
            },
            memoizedGet: _.memoize(function (someObj) {
                return this.getValue(someObj);
            }, function (someObj) {
                return this.key + "-_-" + someObj.cid;
            })
        },
        {
            displayName: "Arranger",
            key: "Arranger",
            isArrayOfResults: true,
            getValue: function (perf) {
                var arrayOfResults = perf.works.map(_.bind(function (work) {
                    if (work.get(this.key) != "")
                        return work.get(this.key);
                }, this));

                return arrayOfResults;
            },
            memoizedGet: _.memoize(function (someObj) {
                return this.getValue(someObj);
            }, function (someObj) {
                return this.key + "-_-" + someObj.cid;
            })
        }
    ],
    ArtistFilters: [
        {
            displayName: "Artist/Ensemble",
            key: "ArtistFullName",
            isArrayOfResults: false,
            getValue: function (artistDTO) {
                if (artistDTO.get(this.key) != "")
                    return artistDTO.get(this.key);
            },
            memoizedGet: _.memoize(function (someObj) {
                return this.getValue(someObj);
            }, function (someObj) {
                return this.key + "-_-" + someObj.cid;
            })
        },
        {
            displayName: "Instrument/Role",
            key: "Instrument",
            isArrayOfResults: true,
            getValue: function (artistDTO) {
                var arrayOfResults = artistDTO.instruments.map(_.bind(function (instrument) {
                    if (instrument.get(this.key) != "")
                        return instrument.get(this.key);
                }, this));

                return arrayOfResults;
            },
            memoizedGet: _.memoize(function (someObj) {
                return this.getValue(someObj);
            }, function (someObj) {
                return this.key + "-_-" + someObj.cid;
            })
        },
        {
            displayName: "Composer",
            key: "ComposerFullName",
            isArrayOfResults: false,
            getValue: function (artistDTO) {
                if (artistDTO.get(this.key) != "")
                    return artistDTO.get(this.key);
            },
            memoizedGet: _.memoize(function (someObj) {
                return this.getValue(someObj);
            }, function (someObj) {
                return this.key + "-_-" + someObj.cid;
            })
        },
        {
            displayName: "Work",
            key: "WorkTitle",
            isArrayOfResults: false,
            getValue: function (artistDTO) {
                if (artistDTO.get(this.key) != "")
                    return artistDTO.get(this.key);
            },
            memoizedGet: _.memoize(function (someObj) {
                return this.getValue(someObj);
            }, function (someObj) {
                return this.key + "-_-" + someObj.cid;
            })
        }
    ],
    RepertoireFilters: [
        {
            displayName: "Composer",
            key: "ComposerFullName",
            isArrayOfResults: false,
            getValue: function (repDTO) {
                if (repDTO.get(this.key) != "")
                    return repDTO.get(this.key);
            },
            memoizedGet: _.memoize(function (someObj) {
                return this.getValue(someObj);
            }, function (someObj) {
                return this.key + "-_-" + someObj.cid;
            })
        },
        {
            displayName: "Work",
            key: "WorkTitle",
            isArrayOfResults: false,
            getValue: function (repDTO) {
                if (repDTO.get(this.key) != "")
                    return repDTO.get(this.key);
            },
            memoizedGet: _.memoize(function (someObj) {
                return this.getValue(someObj);
            }, function (someObj) {
                return this.key + "-_-" + someObj.cid;
            })
        },
        {
            displayName: "Arranger",
            key: "Arranger",
            isArrayOfResults: false,
            getValue: function (repDTO) {
                if (repDTO.get(this.key) != "")
                    return repDTO.get(this.key);
            },
            memoizedGet: _.memoize(function (someObj) {
                return this.getValue(someObj);
            }, function (someObj) {
                return this.key + "-_-" + someObj.cid;
            })
        }
    ]
};

BSO.SortConfig = {
    resetSort: function () {
        _(_(this.SortingOptions).where({ active: true })).map(function (option) { option.active = false; });
    },
    setDefaultActive: function (sortName) {
        _(this.SortingOptions).findWhere({ name: sortName }).active = true;
    },
    SortingOptions: [
        {
            name: "SortPerformanceByDate",
            active: false,
            doDescending: false,
            getFn: function (p) {
                // This is the function you edit for easy sorting changes
                return Date.parse(p.get('EventDate'));
            },
            doSort: function (performance1, performance2) {
                // This can be edited for custom sorting.
                var value1 = this.getFn(performance1);
                var value2 = this.getFn(performance2);

                if (value1 > value2) {
                    return 1;
                } else if (value1 < value2) {
                    return -1;
                } else {
                    return 0;
                }
            },
            sortFn: function (performance1, performance2) {
                // THIS IS THE COMPARATOR. It reverses the doSort for you if descending
                if (this.doDescending)
                    return -this.doSort(performance1, performance2);
                else
                    return this.doSort(performance1, performance2);
            }
        },
        {
            name: "SortPerformanceByConductor",
            active: false,
            doDescending: false,
            getFn: function (p) {
                // This is the function you edit.
                return p.conductor.get('ConductorFullName');
            },
            doSort: function (performance1, performance2) {
                // This can be edited for custom sorting.
                var value1 = this.getFn(performance1);
                var value2 = this.getFn(performance2);

                if (value1 > value2) {
                    return 1;
                } else if (value1 < value2) {
                    return -1;
                } else {
                    return 0;
                }
            },
            sortFn: function (performance1, performance2) {
                // THIS IS THE COMPARATOR. It reverses the doSort for you if descending
                if (this.doDescending)
                    return -this.doSort(performance1, performance2);
                else
                    return this.doSort(performance1, performance2);
            }
        },
        {
            name: "SortPerformanceBySeason",
            active: false,
            doDescending: false,
            getFn: function (p) {
                // This is the function you edit.
                return p.season.get('SeasonName');
            },
            doSort: function (performance1, performance2) {
                // This can be edited for custom sorting.
                var value1 = this.getFn(performance1);
                var value2 = this.getFn(performance2);

                if (value1 > value2) {
                    return 1;
                } else if (value1 < value2) {
                    return -1;
                } else {
                    return 0;
                }
            },
            sortFn: function (performance1, performance2) {
                // THIS IS THE COMPARATOR. It reverses the doSort for you if descending
                if (this.doDescending)
                    return -this.doSort(performance1, performance2);
                else
                    return this.doSort(performance1, performance2);
            }
        },
        {
            name: "SortPerformanceByVenue",
            active: false,
            doDescending: false,
            getFn: function (p) {
                // This is the function you edit.
                return p.venue.get('VenueName');
            },
            doSort: function (performance1, performance2) {
                // This can be edited for custom sorting.
                var value1 = this.getFn(performance1);
                var value2 = this.getFn(performance2);

                if (value1 > value2) {
                    return 1;
                } else if (value1 < value2) {
                    return -1;
                } else {
                    return 0;
                }
            },
            sortFn: function (performance1, performance2) {
                // THIS IS THE COMPARATOR. It reverses the doSort for you if descending
                if (this.doDescending)
                    return -this.doSort(performance1, performance2);
                else
                    return this.doSort(performance1, performance2);
            }
        },
        {
            name: "SortPerformanceByOrchestra",
            active: false,
            doDescending: false,
            getFn: function (p) {
                // This is the function you edit.
                return p.orchestra.get('OrchestraName');
            },
            doSort: function (performance1, performance2) {
                // This can be edited for custom sorting.
                var value1 = this.getFn(performance1);
                var value2 = this.getFn(performance2);

                if (value1 > value2) {
                    return 1;
                } else if (value1 < value2) {
                    return -1;
                } else {
                    return 0;
                }
            },
            sortFn: function (performance1, performance2) {
                // THIS IS THE COMPARATOR. It reverses the doSort for you if descending
                if (this.doDescending)
                    return -this.doSort(performance1, performance2);
                else
                    return this.doSort(performance1, performance2);
            }
        },
        {
            name: "SortArtistByName",
            active: false,
            doDescending: false,
            getFn: function (a) {
                // This is the function you edit.
                return a.get('ArtistFullName');
            },
            doSort: function (artist1, artist2) {
                // This can be edited for custom sorting.
                var value1 = this.getFn(artist1);
                var value2 = this.getFn(artist2);

                if (value1 > value2) {
                    return 1;
                } else if (value1 < value2) {
                    return -1;
                } else {
                    return 0;
                }
            },
            sortFn: function (performance1, performance2) {
                // THIS IS THE COMPARATOR. It reverses the doSort for you if descending
                if (this.doDescending)
                    return -this.doSort(performance1, performance2);
                else
                    return this.doSort(performance1, performance2);
            }
        },
        {
            name: "SortArtistByComposer",
            active: false,
            doDescending: false,
            getFn: function (a) {
                // This is the function you edit.
                return a.get('ComposerFullName');
            },
            doSort: function (artist1, artist2) {
                // This can be edited for custom sorting.
                var value1 = this.getFn(artist1);
                var value2 = this.getFn(artist2);

                if (value1 > value2) {
                    return 1;
                } else if (value1 < value2) {
                    return -1;
                } else {
                    return 0;
                }
            },
            sortFn: function (performance1, performance2) {
                // THIS IS THE COMPARATOR. It reverses the doSort for you if descending
                if (this.doDescending)
                    return -this.doSort(performance1, performance2);
                else
                    return this.doSort(performance1, performance2);
            }
        },
        {
            name: "SortArtistByWork",
            active: false,
            doDescending: false,
            getFn: function (a) {
                // This is the function you edit.
                return a.get('WorkTitle');
            },
            doSort: function (artist1, artist2) {
                // This can be edited for custom sorting.
                var value1 = this.getFn(artist1);
                var value2 = this.getFn(artist2);

                if (value1 > value2) {
                    return 1;
                } else if (value1 < value2) {
                    return -1;
                } else {
                    return 0;
                }
            },
            sortFn: function (performance1, performance2) {
                // THIS IS THE COMPARATOR. It reverses the doSort for you if descending
                if (this.doDescending)
                    return -this.doSort(performance1, performance2);
                else
                    return this.doSort(performance1, performance2);
            }
        },
        {
            name: "SortArtistByConductorCount",
            active: false,
            doDescending: false,
            getFn: function (a) {
                // This is the function you edit.
                return parseInt(a.get('ConductorCount'));
            },
            doSort: function (artist1, artist2) {
                // This can be edited for custom sorting.
                var value1 = this.getFn(artist1);
                var value2 = this.getFn(artist2);

                if (value1 > value2) {
                    return 1;
                } else if (value1 < value2) {
                    return -1;
                } else {
                    return 0;
                }
            },
            sortFn: function (performance1, performance2) {
                // THIS IS THE COMPARATOR. It reverses the doSort for you if descending
                if (this.doDescending)
                    return -this.doSort(performance1, performance2);
                else
                    return this.doSort(performance1, performance2);
            }
        },
        {
            name: "SortArtistBySoloistCount",
            active: false,
            doDescending: false,
            getFn: function (a) {
                // This is the function you edit.
                return parseInt(a.get('SoloistCount'));
            },
            doSort: function (artist1, artist2) {
                // This can be edited for custom sorting.
                var value1 = this.getFn(artist1);
                var value2 = this.getFn(artist2);

                if (value1 > value2) {
                    return 1;
                } else if (value1 < value2) {
                    return -1;
                } else {
                    return 0;
                }
            },
            sortFn: function (performance1, performance2) {
                // THIS IS THE COMPARATOR. It reverses the doSort for you if descending
                if (this.doDescending)
                    return -this.doSort(performance1, performance2);
                else
                    return this.doSort(performance1, performance2);
            }
        },
        {
            name: "SortArtistByParticipantCount",
            active: false,
            doDescending: false,
            getFn: function (a) {
                // This is the function you edit.
                return parseInt(a.get('ParticipantCount'));
            },
            doSort: function (artist1, artist2) {
                // This can be edited for custom sorting.
                var value1 = this.getFn(artist1);
                var value2 = this.getFn(artist2);

                if (value1 > value2) {
                    return 1;
                } else if (value1 < value2) {
                    return -1;
                } else {
                    return 0;
                }
            },
            sortFn: function (performance1, performance2) {
                // THIS IS THE COMPARATOR. It reverses the doSort for you if descending
                if (this.doDescending)
                    return -this.doSort(performance1, performance2);
                else
                    return this.doSort(performance1, performance2);
            }
        },
        {
            name: "SortArtistByEnsembleCount",
            active: false,
            doDescending: false,
            getFn: function (a) {
                // This is the function you edit.
                return parseInt(a.get('EnsembleCount'));
            },
            doSort: function (artist1, artist2) {
                // This can be edited for custom sorting.
                var value1 = this.getFn(artist1);
                var value2 = this.getFn(artist2);

                if (value1 > value2) {
                    return 1;
                } else if (value1 < value2) {
                    return -1;
                } else {
                    return 0;
                }
            },
            sortFn: function (performance1, performance2) {
                // THIS IS THE COMPARATOR. It reverses the doSort for you if descending
                if (this.doDescending)
                    return -this.doSort(performance1, performance2);
                else
                    return this.doSort(performance1, performance2);
            }
        },
        {
            name: "SortRepertoireByComposer",
            active: false,
            doDescending: false,
            getFn: function (r) {
                // This is the function you edit.
                return r.get('ComposerFullName');
            },
            doSort: function (rep1, rep2) {
                // This can be edited for custom sorting.
                var value1 = this.getFn(rep1);
                var value2 = this.getFn(rep2);

                if (value1 > value2) {
                    return 1;
                } else if (value1 < value2) {
                    return -1;
                } else {
                    return 0;
                }
            },
            sortFn: function (repertoire1, repertoire2) {
                // THIS IS THE COMPARATOR. It reverses the doSort for you if descending
                if (this.doDescending)
                    return -this.doSort(repertoire1, repertoire2);
                else
                    return this.doSort(repertoire1, repertoire2);
            }
        },
        {
            name: "SortRepertoireByWork",
            active: false,
            doDescending: false,
            getFn: function (r) {
                // This is the function you edit.
                return r.get('WorkTitle');
            },
            doSort: function (rep1, rep2) {
                // This can be edited for custom sorting.
                var value1 = this.getFn(rep1);
                var value2 = this.getFn(rep2);

                if (value1 > value2) {
                    return 1;
                } else if (value1 < value2) {
                    return -1;
                } else {
                    return 0;
                }
            },
            sortFn: function (repertoire1, repertoire2) {
                // THIS IS THE COMPARATOR. It reverses the doSort for you if descending
                if (this.doDescending)
                    return -this.doSort(repertoire1, repertoire2);
                else
                    return this.doSort(repertoire1, repertoire2);
            }
        },
        {
            name: "SortRepertoireByArranger",
            active: false,
            doDescending: false,
            getFn: function (r) {
                // This is the function you edit.
                return r.get('Arranger');
            },
            doSort: function (rep1, rep2) {
                // This can be edited for custom sorting.
                var value1 = this.getFn(rep1);
                var value2 = this.getFn(rep2);

                if (value1 > value2) {
                    return 1;
                } else if (value1 < value2) {
                    return -1;
                } else {
                    return 0;
                }
            },
            sortFn: function (repertoire1, repertoire2) {
                // THIS IS THE COMPARATOR. It reverses the doSort for you if descending
                if (this.doDescending)
                    return -this.doSort(repertoire1, repertoire2);
                else
                    return this.doSort(repertoire1, repertoire2);
            }
        },
        {
            name: "SortRepertoireByPerformanceCount",
            active: false,
            doDescending: false,
            getFn: function (r) {
                // This is the function you edit.
                return parseInt(r.get('PerformanceCount'));
            },
            doSort: function (rep1, rep2) {
                // This can be edited for custom sorting.
                var value1 = this.getFn(rep1);
                var value2 = this.getFn(rep2);

                if (value1 > value2) {
                    return 1;
                } else if (value1 < value2) {
                    return -1;
                } else {
                    return 0;
                }
            },
            sortFn: function (repertoire1, repertoire2) {
                // THIS IS THE COMPARATOR. It reverses the doSort for you if descending
                if (this.doDescending)
                    return -this.doSort(repertoire1, repertoire2);
                else
                    return this.doSort(repertoire1, repertoire2);
            }
        },
        {
            name: "WorkListSort",
            active: false,
            doDescending: false,
            isNestedListSort: true,
            targetProperty: "works", // This targets the performance.works collection.
            getFn: function (work) {
                // This is the function you edit.
                return work.get('ComposerFullName');
            },
            comparatorFlippable: function (work1, work2) {
                // This can be edited for custom sorting.
                var value1 = this.getFn(work1);
                var value2 = this.getFn(work2);

                if (value1 > value2) {
                    return 1;
                } else if (value1 < value2) {
                    return -1;
                } else {
                    return 0;
                }
            },
            doCompare: function (work1, work2) {
                if (this.active && this.doDescending)
                    return -this.comparatorFlippable(work1, work2);
                else
                    return this.comparatorFlippable(work1, work2);
            }
        },
        {
            name: "ArtistListSort",
            active: false,
            doDescending: false,
            isNestedListSort: true,
            targetClass: this.BSO.ArtistList,
            getFn: function (artist) {
                // This is the function you edit.
                return artist.get('ArtistFullName');
            },
            comparatorFlippable: function (artist1, artist2) {
                // This can be edited for custom sorting.
                var value1 = this.getFn(artist1);
                var value2 = this.getFn(artist2);

                if (value1 > value2) {
                    return 1;
                } else if (value1 < value2) {
                    return -1;
                } else {
                    return 0;
                }
            },
            doCompare: function (artist1, artist2) {
                if (this.active && this.doDescending)
                    return -this.comparatorFlippable(artist1, artist2);
                else
                    return this.comparatorFlippable(artist1, artist2);
            }
        }
    ]
};