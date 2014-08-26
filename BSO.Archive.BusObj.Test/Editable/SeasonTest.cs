using System;
using Bso.Archive.BusObj;
using Bso.Archive.BusObj.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BSO.Archive.BusObj.Test
{
    [TestClass]
    public class SeasonTest
    {
        [Ignore]
        [TestMethod]
        public void UpdateSeasonTest()
        {
            Season testSeason = Season.GetSeasonByID(-1);
            if (testSeason.IsNew)
            {
                testSeason.SeasonID = -1;
            }
            testSeason.SeasonCode = "Adage";
            BsoArchiveEntities.Current.Save();

            var seasonID = Helper.CreateXElement(Constants.Season.seasonIDElement, "-1");
            var seasonCode = Helper.CreateXElement(Constants.Season.seasonCodeElement, "Test");
            var seasonItem = new System.Xml.Linq.XElement(Constants.Season.seasonElement, seasonID, seasonCode);
            var eventItem = new System.Xml.Linq.XElement(Constants.Event.eventElement, seasonItem);
            System.Xml.Linq.XDocument doc = new System.Xml.Linq.XDocument(eventItem);

            Season season = Season.NewSeason();
            season.UpdateData(doc, "SeasonCode", "eventSeasonCode");
            Assert.IsTrue(testSeason.SeasonCode == "Test");
            BsoArchiveEntities.Current.DeleteObject(testSeason);
            BsoArchiveEntities.Current.DeleteObject(season);
            BsoArchiveEntities.Current.Save();
        }

        /// <summary>
        /// Tests GetSeasonFromNodeItem by creating an eventItem XElement element and passing it
        /// to the method. Checks that the object was created and with the correct values.
        /// </summary>
        [TestMethod]
        public void GetSeasonFromNodeItemTest()
        {
            var seasonID = Helper.CreateXElement(Constants.Season.seasonIDElement, "-1");
            var seasonName = Helper.CreateXElement(Constants.Season.seasonNameElement, "TestSeasonName");
            var seasonCode = Helper.CreateXElement(Constants.Season.seasonCodeElement, "TestSeasonCode");
            var seasonItem = new System.Xml.Linq.XElement(Constants.Season.seasonElement, seasonID, seasonName, seasonCode);
            var node = new System.Xml.Linq.XElement(Constants.Event.eventElement, seasonItem);

            Season season = Season.GetSeasonFromNode(node);
            Assert.IsNotNull(season);
            Assert.IsTrue(season.SeasonID == -1);
        }
    }
}