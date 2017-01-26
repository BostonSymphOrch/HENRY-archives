using System;
using System.Collections.Generic;
using System.Linq;

namespace Bso.Archive.BusObj.Utility
{
    public class ArchiveAutocomplete
    {

        public class AutoCompleteKeyValue
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        public class AutoCompleteComparator : IEqualityComparer<AutoCompleteKeyValue>
        {
            public bool Equals(AutoCompleteKeyValue a, AutoCompleteKeyValue b)
            {
                if (a == null || b == null)
                    return false;
                if (a.Key == b.Key)
                    return true;
                else
                    return false;
            }

            public int GetHashCode(AutoCompleteKeyValue obj)
            {
                return obj.Key.GetHashCode();
            }
        }
        /// <summary>
        /// Get Distinct Composers from Event Details
        /// </summary>
        /// <returns></returns>
        public static List<AutoCompleteKeyValue> GetDistinctComposers()
        {
            using (var txn = new System.Transactions.TransactionScope(
                System.Transactions.TransactionScopeOption.Required,
                new System.Transactions.TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                }
            ))
            {
                var distinctComposers = BsoArchiveEntities.Current.EventDetails.
                    Where(composer => composer.ComposerFullName != "null" && !String.IsNullOrEmpty(composer.ComposerFullName)).
                    Select(ed => new AutoCompleteKeyValue { Key = ed.ComposerFullName.Trim(), Value = ed.ComposerFullName2.Trim() }).AsEnumerable().Distinct(new ArchiveAutocomplete.AutoCompleteComparator());

                return distinctComposers.ToList();
            }
        }

        /// <summary>
        /// Get Distinct Works from Event Details
        /// </summary>
        /// <returns></returns>
        public static List<AutoCompleteKeyValue> GetDistinctWorks()
        {
            using (var txn = new System.Transactions.TransactionScope(
                System.Transactions.TransactionScopeOption.Required,
                new System.Transactions.TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                }
            ))
            {
                var allWorks = BsoArchiveEntities.Current.EventDetails.Where(work => work.WorkTitle != "null" && !String.IsNullOrEmpty(work.WorkTitle));

                foreach (var excludedId in SettingsHelper.ExcludedWorkGroupIds.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                {
                    int workIdToExclude;

                    int.TryParse(excludedId, out workIdToExclude);

                    if (workIdToExclude == 0) continue;

                    var excludedWork = allWorks.Where(work => work.WorkGroupId == workIdToExclude);

                    allWorks = allWorks.Except(excludedWork);
                }

                var distinctWorks = allWorks.Select(ed => new AutoCompleteKeyValue { Key = ed.WorkTitle.Trim(), Value = ed.WorkAddTitle1.Trim() }).AsEnumerable().Distinct(new ArchiveAutocomplete.AutoCompleteComparator());

                return distinctWorks.ToList();
            }
        }

        /// <summary>
        /// Get Distinct Orchestras from Event Details
        /// </summary>
        /// <returns></returns>
        public static IQueryable<string> GetDistinctOrchestras()
        {
            using (var txn = new System.Transactions.TransactionScope(
                System.Transactions.TransactionScopeOption.Required,
                new System.Transactions.TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                }
            ))
            {
                return BsoArchiveEntities.Current.EventDetails.
                    Where(ed => ed.OrchestraName != "null" && !String.IsNullOrEmpty(ed.OrchestraName)).
                    Select(ed => ed.OrchestraName.Trim()).Distinct();
            }
        }

        /// <summary>
        /// Get Distinct Seasons from Event Details
        /// </summary>
        /// <returns></returns>
        public static IQueryable<string> GetDistinctSeasons()
        {
            using (var txn = new System.Transactions.TransactionScope(
                System.Transactions.TransactionScopeOption.Required,
                new System.Transactions.TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                }
            ))
            {
                return BsoArchiveEntities.Current.EventDetails.
                    Where(ed => ed.SeasonName != "null" && !String.IsNullOrEmpty(ed.SeasonName)).
                    Select(ed => ed.SeasonName.Trim()).Distinct();
            }
        }

        /// <summary>
        /// Get Distinct Conductors from Event Details
        /// </summary>
        /// <returns></returns>
        public static List<AutoCompleteKeyValue> GetDistinctConductors()
        {
            using (var txn = new System.Transactions.TransactionScope(
                System.Transactions.TransactionScopeOption.Required,
                new System.Transactions.TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                }
            ))
            {
                var distinctConductors = BsoArchiveEntities.Current.EventDetails.
                     Where(ed => ed.ConductorFullName != "null" && !String.IsNullOrEmpty(ed.ConductorFullName)).
                     Select(ed => new AutoCompleteKeyValue { Key = ed.ConductorFullName.Trim(), Value = ed.ComposerFullName2.Trim() }).AsEnumerable().Distinct(new ArchiveAutocomplete.AutoCompleteComparator());

                return distinctConductors.ToList();
            }
        }

        /// <summary>
        /// Get Distinct Artists from Event Details
        /// </summary>
        /// <returns></returns>
        public static List<AutoCompleteKeyValue> GetDistinctSoloists()
        {
            using (var txn = new System.Transactions.TransactionScope(
                System.Transactions.TransactionScopeOption.Required,
                new System.Transactions.TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                }
            ))
            {
                var distinctSoloists = BsoArchiveEntities.Current.EventDetails.
                    Where(ed => ed.ArtistFullName != "null" && !String.IsNullOrEmpty(ed.ArtistFullName)).
                    Select(ed => new AutoCompleteKeyValue { Key = ed.ArtistFullName.Trim(), Value = ed.ArtistFullName2.Trim() }).AsEnumerable().Distinct(new ArchiveAutocomplete.AutoCompleteComparator());

                return distinctSoloists.ToList();
            }
        }

        /// <summary>
        /// Get Distinct instruments from Event Details
        /// </summary>
        /// <returns></returns>
        public static IQueryable<string> GetDistinctPerformanceInstruments()
        {
            using (var txn = new System.Transactions.TransactionScope(
                System.Transactions.TransactionScopeOption.Required,
                new System.Transactions.TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                }
            ))
            {
                return BsoArchiveEntities.Current.EventDetails.
                    Where(ed => ed.Instrument1 != "null" && !String.IsNullOrEmpty(ed.Instrument1)).
                    Select(ed => ed.Instrument1.Trim()).Distinct();
            }
        }

        /// <summary>
        /// Get Distinct work arrangers from Event Details
        /// </summary>
        /// <returns></returns>
        public static IQueryable<string> GetDistinctArrangers()
        {
            using (var txn = new System.Transactions.TransactionScope(
                System.Transactions.TransactionScopeOption.Required,
                new System.Transactions.TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                }
            ))
            {
                return BsoArchiveEntities.Current.EventDetails.
                    Where(ed => ed.WorkArrangement != "null" && !String.IsNullOrEmpty(ed.WorkArrangement)).
                    Select(ed => ed.WorkArrangement.Trim()).Distinct();
            }
        }

        /// <summary>
        /// Get Distince event Series from EventDetails
        /// </summary>
        /// <returns></returns>
        public static List<string> GetDistinctSeries()
        {
            using (var txn = new System.Transactions.TransactionScope(
                System.Transactions.TransactionScopeOption.Required,
                new System.Transactions.TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                }
            ))
            {
                return BsoArchiveEntities.Current.EventDetails.
                    Where(ed => ed.EventSeries != "null" && !String.IsNullOrEmpty(ed.EventSeries)).
                    Select(ed => ed.EventSeries.Trim()).Distinct().ToList().Select(x => x.Split(';')).SelectMany(x => x).Select(x => x.Trim()).Distinct().ToList();
            }
        }

        /// <summary>
        /// Get Distinct event program titles from Event Details
        /// </summary>
        /// <returns></returns>
        public static IQueryable<string> GetDistinctEventTitles()
        {
            using (var txn = new System.Transactions.TransactionScope(
                System.Transactions.TransactionScopeOption.Required,
                new System.Transactions.TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                }
            ))
            {
                return BsoArchiveEntities.Current.EventDetails.
                    Where(ed => ed.EventProgramTitle != "null" && !String.IsNullOrEmpty(ed.EventProgramTitle)).
                    Select(ed => ed.EventProgramTitle.Trim()).Distinct();
            }
        }

        /// <summary>
        /// Get Distinct event venues from Event Details
        /// </summary>
        /// <returns></returns>
        public static IQueryable<string> GetDistinctVenues()
        {
            using (var txn = new System.Transactions.TransactionScope(
                System.Transactions.TransactionScopeOption.Required,
                new System.Transactions.TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                }
            ))
            {
                return BsoArchiveEntities.Current.EventDetails.
                    Where(ed => ed.VenueName != "null" && !String.IsNullOrEmpty(ed.VenueName)).
                    Select(ed => ed.VenueName.Trim()).Distinct();
            }
        }

        public static IQueryable<string> GetDistinctMediaTypes()
        {
            using (var txn = new System.Transactions.TransactionScope(
                System.Transactions.TransactionScopeOption.Required,
                new System.Transactions.TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                }
            ))
            {
                return BsoArchiveEntities.Current.WorkDocuments.
                    Where(wd => wd.WorkDocumentName != "null" && !String.IsNullOrEmpty(wd.WorkDocumentName)).
                    Select(wd => wd.WorkDocumentName.Trim()).Distinct();
            }
        }

        /// <summary>
        /// Get Distinct cities from Event Details
        /// </summary>
        /// <returns></returns>
        public static IQueryable<string> GetDistinctCities()
        {
            using (var txn = new System.Transactions.TransactionScope(
                System.Transactions.TransactionScopeOption.Required,
                new System.Transactions.TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                }
            ))
            {
                return BsoArchiveEntities.Current.EventDetails.
                    Where(ed => ed.VenueCity != "null" && !String.IsNullOrEmpty(ed.VenueCity)).
                    Select(ed => ed.VenueCity.Trim()).Distinct();
            }
        }

        /// <summary>
        /// Get Distinct States from Event Details
        /// </summary>
        /// <returns></returns>
        public static IQueryable<string> GetDistinctStates()
        {
            using (var txn = new System.Transactions.TransactionScope(
                System.Transactions.TransactionScopeOption.Required,
                new System.Transactions.TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                }
            ))
            {
                return BsoArchiveEntities.Current.EventDetails.
                    Where(ed => ed.VenueState != "null" && !String.IsNullOrEmpty(ed.VenueState)).
                    Select(ed => ed.VenueState.Trim()).Distinct();
            }
        }

        /// <summary>
        /// Get Distinct countries from Event Details
        /// </summary>
        /// <returns></returns>
        public static IQueryable<string> GetDistinctCountries()
        {
            using (var txn = new System.Transactions.TransactionScope(
                System.Transactions.TransactionScopeOption.Required,
                new System.Transactions.TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                }
            ))
            {
                return BsoArchiveEntities.Current.EventDetails.
                    Where(ed => ed.VenueCountry != "null" && !String.IsNullOrEmpty(ed.VenueCountry)).
                    Select(ed => ed.VenueCountry.Trim()).Distinct();
            }
        }

        /// <summary>
        /// Get Distinct Premieres
        /// </summary>
        /// <returns></returns>
        public static IQueryable<string> GetDistinctPremieres()
        {
            using (var txn = new System.Transactions.TransactionScope(
                System.Transactions.TransactionScopeOption.Required,
                new System.Transactions.TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                }
            ))
            {
                return BsoArchiveEntities.Current.EventDetails.
                  Where(ed => ed.WorkPremiere != "null" && !String.IsNullOrEmpty(ed.WorkPremiere)).
                  Select(ed => ed.WorkPremiere.Trim()).Distinct();
            }
        }

        /// <summary>
        /// Get Distinct Commissions
        /// </summary>
        /// <returns></returns>
        public static IQueryable<string> GetDistinctCommissions()
        {
            using (var txn = new System.Transactions.TransactionScope(
                System.Transactions.TransactionScopeOption.Required,
                new System.Transactions.TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                }
            ))
            {
                return BsoArchiveEntities.Current.EventDetails.
                 Where(ed => ed.WorkCommission != "null" && !String.IsNullOrEmpty(ed.WorkCommission)).
                 Select(ed => ed.WorkCommission.Trim()).Distinct();
            }
        }

        /// <summary>
        /// Get Distinct Ensembles from Artist Detail
        /// </summary>
        /// <returns></returns>
        public static List<AutoCompleteKeyValue> GetDistinctEnsembles()
        {
            using (var txn = new System.Transactions.TransactionScope(
                System.Transactions.TransactionScopeOption.Required,
                new System.Transactions.TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                }
            ))
            {
                var distinctEnsembles = BsoArchiveEntities.Current.ArtistDetails.
                   Where(ad => ad.EnsembleName != "null" && !String.IsNullOrEmpty(ad.EnsembleName)).
                   Select(ad => new AutoCompleteKeyValue { Key = ad.EnsembleName.Trim(), Value = ad.EnsembleName2.Trim() }).AsEnumerable().Distinct(new ArchiveAutocomplete.AutoCompleteComparator());

                return distinctEnsembles.ToList();

            }
        }


        public class InstrumentListItem
        {
            public string label;
            public string value;
        }
        /// <summary>
        /// Get Distinct Ensembles from Artist Detail
        /// </summary>
        /// <returns></returns>
        public static IQueryable<string> GetDistinctInstruments()
        {
            using (var txn = new System.Transactions.TransactionScope(
                System.Transactions.TransactionScopeOption.Required,
                new System.Transactions.TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                }
            ))
            {
                return BsoArchiveEntities.Current.ArtistDetails.
                   Where(ad => ad.EnsembleType != "null" && !String.IsNullOrEmpty(ad.EnsembleType)).
                   Select(ad => ad.EnsembleType.Trim()).Distinct();
            }
        }
    }
}
