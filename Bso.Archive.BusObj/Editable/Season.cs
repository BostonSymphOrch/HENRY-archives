using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Bso.Archive.BusObj.Interface;
using Bso.Archive.BusObj.Utility;

namespace Bso.Archive.BusObj
{
    partial class Season : IOPASData
    {
        #region IOPASData

        /// <summary>
        /// Updates the existing database Season on the column name using the 
        /// XML document parsed using the tagName.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="columnName"></param>
        /// <param name="tagName"></param>
        public void UpdateData(System.Xml.Linq.XDocument doc, string columnName, string tagName)
        {
            IEnumerable<System.Xml.Linq.XElement> eventElements = doc.Descendants(Constants.Event.eventElement);
            foreach (System.Xml.Linq.XElement element in eventElements)
            {
                Season updateSeason = Season.GetSeasonFromNode(element);

                if (updateSeason == null) continue;

                System.Xml.Linq.XElement seasonNode = element.Element(Constants.Season.seasonElement);

                object newValue = seasonNode.GetXElement(tagName);

                BsoArchiveEntities.UpdateObject(updateSeason, newValue, columnName);
            }
        }
        #endregion

        /// <summary>
        /// Returns a Season object based on the eventSeason data passed by the
        /// XElement eventItem element. 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static Season GetSeasonFromNode(System.Xml.Linq.XElement node)
        {
            System.Xml.Linq.XElement seasonElement = node.Element(Constants.Season.seasonElement);
            if (seasonElement == null || string.IsNullOrEmpty((string)seasonElement.GetXElement(Constants.Season.seasonIDElement)))
                return null;

            int seasonID;
            int.TryParse(seasonElement.GetXElement(Constants.Season.seasonIDElement), out seasonID);


            Season season = GetSeasonByID(seasonID);
            if (!season.IsNew)
                return season;

            string seasonName = seasonElement.GetXElement(Constants.Season.seasonNameElement);
            string seasonCode = seasonElement.GetXElement(Constants.Season.seasonCodeElement);

            season = SetSeasonData(seasonID, season, seasonCode, seasonName);

            return season;
        }

        /// <summary>
        /// Takes season values and assigns them to season attributes.
        /// </summary>
        /// <param name="seasonID"></param>
        /// <param name="season"></param>
        /// <param name="seasonCode"></param>
        /// <param name="seasonName"></param>
        /// <returns></returns>
        private static Season SetSeasonData(int seasonID, Season season, string seasonCode, string seasonName)
        {
            season.SeasonID = seasonID;
            season.SeasonCode = seasonCode;
            season.SeasonName = seasonName;

            return season;
        }

        /// <summary>
        /// Checks existing Seasons based upon the given ID. If the season 
        /// already exists then return it. Otherwise create a new season 
        /// and return it.
        /// </summary>
        /// <param name="seasonID"></param>
        /// <returns></returns>
        internal static Season GetSeasonByID(int seasonID)
        {
            Season season = BsoArchiveEntities.Current.Seasons.FirstOrDefault(s => s.SeasonID == seasonID) ?? 
                Season.NewSeason();

            return season;
        }

    }
}
