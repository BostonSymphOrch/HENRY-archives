using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Bso.Archive.BusObj.Utility
{
    public static class Helper
    {
        public static IEnumerable<Object> ProjectEventDetails(this IEnumerable<EventDetail> results)
        {
            var groupedByEventId = results.GroupBy(result => result.EventID);

            return groupedByEventId.Select(result =>
            {
                if (!result.Any()) return null;

                var firstEvent = result.First();
                var composers = result.Select(r => r.ComposerFullName);
                var works = result.Select(a => a.WorkTitle);
                return new
                {
                    Composers = composers,
                    WorkTitle = works,
                    firstEvent.EventID,
                    firstEvent.ArtistFullName,
                    firstEvent.EventFullDate,
                    firstEvent.SeasonName,
                    firstEvent.ConductorFullName,
                    firstEvent.OrchestraName,
                    firstEvent.VenueName
                };

            }).Where(x => x != null);
        }

        /// <summary>
        /// Create XElement from Name and Value
        /// </summary>
        /// <param name="xName"></param>
        /// <param name="xValue"></param>
        /// <returns></returns>
        public static XElement CreateXElement(string xName, string xValue)
        {
            return new System.Xml.Linq.XElement(xName, xValue);
        }

        internal static string ConcatString(string[] args)
        {
            var argsList = args.ToList();

            var concatedString = String.Join(", ", argsList.Where(arg => !String.IsNullOrEmpty(arg)));

            return concatedString;
        }

        /// <summary>
        /// Get Tab Type for easy tab plugin based on the type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetTabType(string type)
        {
            return String.Concat("#tabs-", type.ToLower());
        }
    }
}
