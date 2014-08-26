using System;
using Bso.Archive.BusObj;
using Bso.Archive.BusObj.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BSO.Archive.BusObj.Test
{
    [TestClass]
    public class VenueTest
    {
        [Ignore]
        [TestMethod]
        public void UpdateVenueTest()
        {
            Venue testVenue = Venue.GetVenueByID(-1);

            if (testVenue.IsNew)
            {
                testVenue.VenueID = -1;
            }
            testVenue.VenueName = "Adage";
            BsoArchiveEntities.Current.Save();

            var venueID = Helper.CreateXElement(Constants.Venue.venueIDElement, "-1");
            var venueName = Helper.CreateXElement(Constants.Venue.venueNameElement, "Test");
            var venueItem = new System.Xml.Linq.XElement(Constants.Venue.venueElement, venueID, venueName);
            var eventItem = new System.Xml.Linq.XElement(Constants.Event.eventElement, venueItem);
            var doc = new System.Xml.Linq.XDocument(eventItem);

            Venue typeVenue = Venue.NewVenue();

            typeVenue.UpdateData(doc, "VenueName", Constants.Venue.venueNameElement);

            Assert.IsTrue(testVenue.VenueName == "Test");
            BsoArchiveEntities.Current.DeleteObject(testVenue);
            BsoArchiveEntities.Current.DeleteObject(typeVenue);
            BsoArchiveEntities.Current.Save();
        }

        /// <summary>
        /// Creates an eventItem XElement node with only
        /// a Venue child and calls the GetVenueFromNodeItem
        /// method and verifies it returns the created Venue
        /// object.
        /// </summary>
        [TestMethod]
        public void GetVenueFromNodeTest()
        {
            var venueID = Helper.CreateXElement(Constants.Venue.venueIDElement, "-1");
            var venueName = Helper.CreateXElement(Constants.Venue.venueNameElement, "TestName");
            var venueCode = Helper.CreateXElement(Constants.Venue.venueCodeElement, "Test Venue Code");
            var venueElement = new System.Xml.Linq.XElement(Constants.Venue.venueElement, venueID, venueName, venueCode);
            System.Xml.Linq.XElement node = new System.Xml.Linq.XElement(Constants.Event.eventElement, venueElement);

            Venue venue = Venue.GetVenueFromNode(node);
            Assert.IsNotNull(venue);
            Assert.IsTrue(venue.VenueID == -1);
        }
    }
}