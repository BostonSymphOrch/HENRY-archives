using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bso.Archive.BusObj;
using Bso.Archive.BusObj.Utility;

namespace BSO.Archive.BusObj.Test
{
    [TestClass]
    public class VenueTest
    {
        [TestMethod]
        public void UpdateVenueTest()
        {
            Venue testVenue = Venue.GetVenueByID(6);

            Assert.IsTrue(testVenue.VenueZipCode == "");

            var venueID = Helper.CreateXElement(Constants.Venue.venueIDElement, "6");
            var venueZipCode = Helper.CreateXElement(Constants.Venue.venueZipCodeElement, "0123456");
            var venueItem = new System.Xml.Linq.XElement(Constants.Venue.venueElement, venueID, venueZipCode);
            var eventItem = new System.Xml.Linq.XElement(Constants.Event.eventElement, venueItem);
            var doc = new System.Xml.Linq.XDocument(eventItem);

            Venue typeVenue = Venue.NewVenue();

            typeVenue.UpdateData(doc, "VenueZipCode", "eventVenueZipCode");

            Assert.IsTrue(testVenue.VenueZipCode == "0123456");
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
            var venueID = Helper.CreateXElement(Constants.Venue.venueIDElement, "1");
            var venueName = Helper.CreateXElement(Constants.Venue.venueNameElement, "TestName");
            var venueCode = Helper.CreateXElement(Constants.Venue.venueCodeElement, "Test Venue Code");
            var venueElement = new System.Xml.Linq.XElement(Constants.Venue.venueElement, venueID, venueName, venueCode);
            System.Xml.Linq.XElement node = new System.Xml.Linq.XElement(Constants.Event.eventElement, venueElement);

            Venue venue = Venue.GetVenueFromNode(node);
            Assert.IsNotNull(venue);
            Assert.IsTrue(venue.VenueID == 1);
        }

        /// <summary>
        /// Tests the GetVenueID method.
        /// </summary>
        [TestMethod]
        public void GetVenueByIDTest()
        {
            Venue venue1 = Venue.GetVenueByID(1);

            Assert.IsNotNull(venue1);

            if(venue1.IsNew)
                venue1.VenueID = 1;

            BsoArchiveEntities.Current.Save();

            Venue venue2 = Venue.GetVenueByID(1);

            Assert.IsNotNull(venue2);

            Assert.IsTrue(venue1.Equals(venue2));
        }
    }
}
