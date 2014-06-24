using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bso.Archive.BusObj;
using Bso.Archive.BusObj.Utility;

namespace BSO.Archive.BusObj.Test
{
    [TestClass]
    public class SeasonTest
    {
        [TestMethod]
        public void UpdateSeasonTest()
        {
            Season testSeason = Season.GetSeasonByID(53);
            Assert.IsTrue(testSeason.SeasonCode == "2010-11 S");

            var seasonID = Helper.CreateXElement(Constants.Season.seasonIDElement, "53");
            var seasonCode = Helper.CreateXElement(Constants.Season.seasonCodeElement, "ADAGE Test Code");
            var seasonItem = new System.Xml.Linq.XElement(Constants.Season.seasonElement, seasonID, seasonCode);
            var eventItem = new System.Xml.Linq.XElement(Constants.Event.eventElement, seasonItem);
            System.Xml.Linq.XDocument doc = new System.Xml.Linq.XDocument(eventItem);

            Season season = Season.NewSeason();
            season.UpdateData(doc, "SeasonCode", "eventSeasonCode");
            Assert.IsTrue(testSeason.SeasonCode == "ADAGE Test Code");
            testSeason.SeasonCode = "2010-11 S";
            BsoArchiveEntities.Current.Detach(season);
            BsoArchiveEntities.Current.Save();
        }

        /// <summary>
        /// Tests GetSeasonFromNodeItem by creating an eventItem XElement element and passing it 
        /// to the method. Checks that the object was created and with the correct values. 
        /// </summary>
        [TestMethod]
        public void GetSeasonFromNodeItemTest()
        {

            var seasonID = Helper.CreateXElement(Constants.Season.seasonIDElement, "1");
            var seasonName = Helper.CreateXElement(Constants.Season.seasonNameElement, "TestSeasonName");
            var seasonCode = Helper.CreateXElement(Constants.Season.seasonCodeElement, "TestSeasonCode");
            var seasonItem = new System.Xml.Linq.XElement(Constants.Season.seasonElement, seasonID, seasonName, seasonCode);
            var node = new System.Xml.Linq.XElement(Constants.Event.eventElement, seasonItem);


            Season season = Season.GetSeasonFromNode(node);
            Assert.IsNotNull(season);
            Assert.IsTrue(season.SeasonID == 1 && season.SeasonName == "TestSeasonName");
        }

        /// <summary>
        /// Tests the GetSeasonByID method.
        /// </summary>
        [TestMethod]
        public void GetSeasonByIDTest()
        {
            Season season1 = Season.GetSeasonByID(1);
            if(season1.IsNew)
                season1.SeasonID = 1;
            BsoArchiveEntities.Current.Save();

            Season type2 = Season.GetSeasonByID(1);
            Assert.IsTrue(season1.Equals(type2));
        }

    }
}
