using Bso.Archive.BusObj;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Bso.Archive.BusObj.Utility;

namespace BSO.Archive.BusObj.Test
{
    /// <summary>
    /// Summary description for ImportOPASDataTest
    /// </summary>
    [TestClass]
    public class ImportOPASDataTest
    {

        /// <summary>
        /// Method to test the AddEventVenue method in ImportOPASData
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        [TestMethod()]
        public void AddEventVenueTest()
        {
            ImportOPASData importOPAS = new ImportOPASData();

            Event evt = new Event();
            evt.EventNote = "BSO AddEventVenueTest";

            var venueID = Helper.CreateXElement(Constants.Venue.venueIDElement, "1");
            var venueName = Helper.CreateXElement(Constants.Venue.venueNameElement, "TestName");
            var venueCode = Helper.CreateXElement(Constants.Venue.venueCodeElement, "Test Venue Code");
            var venueElement = new System.Xml.Linq.XElement(Constants.Venue.venueElement, venueID, venueName, venueCode);
            System.Xml.Linq.XElement node = new System.Xml.Linq.XElement(Constants.Event.eventElement, venueElement);

            Venue venue = importOPAS.AddEventVenue(evt, node);

            Assert.IsTrue(evt.Venue == venue);
        }

        /// <summary>
        /// Test the AddEventArtist method
        /// </summary>
        /// <remarks>
        /// Tests the functionality of the AddEventArtist method 
        /// in the ImportOPASData class.
        /// </remarks>
        [TestMethod()]
        public void AddEventArtistTest()
        {
            ImportOPASData importOPAS = new ImportOPASData();

            Event evt = Event.GetEventByID(0);
            evt.EventNote = "BSO AddEventArtistTest";
            evt.EventDate = DateTime.Today;

            var artistId = Helper.CreateXElement(Constants.Artist.artistIDElement, "1");
            var artistFirstName = Helper.CreateXElement(Constants.Artist.artistFirstNameElement, "TestFName");
            var artistLastName = Helper.CreateXElement(Constants.Artist.artistLastNameElement, "TestLCode");
            var artistNotes = Helper.CreateXElement(Constants.Artist.artistNoteElement, "TestNotes");
            var artistInstrument = Helper.CreateXElement(Constants.Artist.artistInstrumentElement, "TestInstr");
            var artistInstrument2 = Helper.CreateXElement(Constants.Artist.artistInstrument2Element, "TestInstr2");
            var artistInstrumentID = Helper.CreateXElement(Constants.Artist.artistInstrumentIDElement, "3");
            var artistItem = new System.Xml.Linq.XElement(Constants.Artist.artistElement, artistId, artistFirstName, artistLastName, artistNotes, artistInstrument, artistInstrument2, artistInstrumentID);
            System.Xml.Linq.XElement node = new System.Xml.Linq.XElement(Constants.Event.eventElement, artistItem);

            importOPAS.AddEventArtist(evt, node);

            Assert.IsTrue(evt.EventArtists.Count == 1);
        }

        /// <summary>
        /// Test the AddParticipant method
        /// </summary>
        /// <remarks>
        /// Tests the functionality of the AddEventParticipant method 
        /// in the ImportOPASData class.
        /// </remarks>
        [TestMethod()]
        public void AddEventParticipantTest()
        {
            ImportOPASData importOPAS = new ImportOPASData();

            Event evt = new Event();
            evt.EventNote = "BSO AddEventParticipantTest";

            Assert.IsTrue(evt.EventParticipants.Count == 0);

            var participantID = Helper.CreateXElement(Constants.Participant.participantIDElement, "1");
            var participantFirstName = Helper.CreateXElement(Constants.Participant.participantFirstNameElement, "TestFName");
            var participantLastName = Helper.CreateXElement(Constants.Participant.participantLastNameElement, "TestLName");
            var participantGroup = Helper.CreateXElement(Constants.Participant.participantGroupNameElement, "TestGroup");
            var participantStatusID = Helper.CreateXElement(Constants.Participant.participantStatusIDElement, "1");
            var participantStatus = Helper.CreateXElement(Constants.Participant.participantStatusElement, "1");
            var participantItem = new System.Xml.Linq.XElement(Constants.Participant.participantElement, participantID, participantFirstName, participantLastName,
                participantGroup, participantStatus, participantStatusID);
            var node = new System.Xml.Linq.XElement(Constants.Event.eventElement, participantItem);

            importOPAS.AddEventParticipant(evt, node);
            Assert.IsTrue(evt.EventParticipants.Count == 1);
            Assert.IsTrue(evt.EventParticipantTypes.Count == 1);
        }

        /// <summary>
        /// Test the AddEventOrchestraMethod
        /// </summary>
        /// <remarks>
        /// Tests the functionality of the AddEventOrchestra class 
        /// of the ImportOPASData class.
        /// </remarks>
        [TestMethod()]
        public void AddEventOrchestra()
        {
            ImportOPASData importOPAS = new ImportOPASData();

            Event evt = new Event();
            evt.EventNote = "BSO AddEventArtistTest";

            System.Xml.Linq.XElement node = new System.Xml.Linq.XElement("eventItem", new System.Xml.Linq.XElement("eventOrchestra",
                new System.Xml.Linq.XElement("eventOrchestraID", "1"),
                new System.Xml.Linq.XElement("eventOrchestraName", "TestOrchestraName"),
                new System.Xml.Linq.XElement("eventOrchestraURL", "TestOrchestraURL"),
                new System.Xml.Linq.XElement("eventOrchestraNotes", "TestOrchestraNotes")
                ));

            var orchestra = importOPAS.AddEventOrchestra(evt, node);
            Assert.IsTrue(evt.Orchestra == orchestra);
        }

        /// <summary>
        /// Test AddEventConductor method
        /// </summary>
        /// <remarks>
        /// Tests the functionality of the AddEventOrchestra method in the 
        /// ImportOPASDate class.
        /// </remarks>
        [TestMethod()]
        public void AddEventConductorTest()
        {
            ImportOPASData importOPAS = new ImportOPASData();

            Event evt = new Event();
            evt.EventDate = DateTime.Today;
            evt.EventNote = "BSO AddEventConductorTest";

            System.Xml.Linq.XElement node = new System.Xml.Linq.XElement("eventItem", new System.Xml.Linq.XElement("eventConductor",
                new System.Xml.Linq.XElement("eventConductorID", "1"),
                new System.Xml.Linq.XElement("eventConductorFirstname", "TestFName"),
                new System.Xml.Linq.XElement("eventConductorLastName", "TestLCode"),
                new System.Xml.Linq.XElement("eventConductorNotes", "TestNotes")
                ));

            var conductor = importOPAS.AddEventConductor(evt, node);
            Assert.IsTrue(evt.Conductor == conductor);
        }


        /// <summary>
        /// Add EventTypeGroup Test
        /// </summary>
        /// <remarks>
        /// Tests the functionality of the AddEventTypeGroup method 
        /// in the ImportOPASData class.
        /// </remarks>
        [TestMethod()]
        public void AddEventTypeGroupTest()
        {
            ImportOPASData importOPAS = new ImportOPASData();

            Event evt = new Event();
            evt.EventNote = "BSO AddEventConductorTest";

            System.Xml.Linq.XElement node = new System.Xml.Linq.XElement("eventItem", new System.Xml.Linq.XElement("eventTypeGroup",
                new System.Xml.Linq.XElement("eventTypeGroupID", "1"),
                new System.Xml.Linq.XElement("eventTypeGroupName", "TestTypeGroupName"),
                new System.Xml.Linq.XElement("eventTypeGroupName2", "TestTypeGroupName2")));

            var eventTypeGroup = importOPAS.AddEventTypeGroup(evt, node);

            Assert.IsTrue(evt.EventTypeGroup == eventTypeGroup);
        }

        /// <summary>
        /// Add EventType Test
        /// </summary>
        /// <remarks>
        /// Tests the functionality of the AddEventType method in 
        /// the ImportOPASData class.
        /// </remarks>
        [TestMethod()]
        public void AddEventTypeTest()
        {
            ImportOPASData importOPAS = new ImportOPASData();

            Event evt = new Event();
            evt.EventNote = "BSO AddEventConductorTest";

            System.Xml.Linq.XElement node = new System.Xml.Linq.XElement("eventItem", new System.Xml.Linq.XElement("eventType",
                new System.Xml.Linq.XElement("eventTypeID", "1"),
                new System.Xml.Linq.XElement("eventTypeName", "TestTypeName"),
                new System.Xml.Linq.XElement("eventTypeName2", "TestTypeName2"),
                new System.Xml.Linq.XElement("eventTypePerformance", "1")));

            var eventType = importOPAS.AddEventType(evt, node);

            Assert.IsTrue(evt.EventType == eventType);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void AddEventSeasonTest()
        {
            ImportOPASData importOPAS = new ImportOPASData();

            Event evt = new Event();
            evt.EventNote = "BSO AddEventConductorTest";

            System.Xml.Linq.XElement node = new System.Xml.Linq.XElement("eventItem", new System.Xml.Linq.XElement("eventSeason",
                new System.Xml.Linq.XElement("eventSeasonId", "1"),
                new System.Xml.Linq.XElement("eventSeasonName", "TestSeasonName"),
                new System.Xml.Linq.XElement("eventSeasonCode", "TestSeasonCode")));

            var season = importOPAS.AddEventSeason(evt, node);

            Assert.IsTrue(evt.Season == season);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void AddEventProjectTest()
        {
            ImportOPASData importOPAS = new ImportOPASData();

            Event evt = new Event();
            evt.EventNote = "BSO AddEventConductorTest";

            System.Xml.Linq.XElement node = new System.Xml.Linq.XElement("eventItem", new System.Xml.Linq.XElement("eventProject",
                new System.Xml.Linq.XElement("eventProjectID", "1"),
                new System.Xml.Linq.XElement("eventProjectName", ""),
                new System.Xml.Linq.XElement("eventProjectName2", "TestProjectName"),
                new System.Xml.Linq.XElement("eventProjectTypeName", "TestProjectTypeName")));

            var project = importOPAS.AddEventProject(evt, node);

            Assert.IsTrue(evt.Project == project);
        }

        [TestMethod()]
        public void AddEventWorkTest()
        {
            var xmlTestPath = "C:\\working\\BSO\\BSO.Archive\\OPASData\\WorkItemTest.xml";

            ImportOPASData testOPAS = new ImportOPASData();

            Event evt = new Event();
            evt.EventDate = DateTime.Today;

            System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Load(xmlTestPath);
            System.Xml.Linq.XElement node = doc.Root.Element("eventItem");
            testOPAS.AddEventWorkItems(evt, node);


            Assert.IsTrue(evt.EventWorks.Count == 3);
        }

        [TestMethod()]
        public void TestOPASInput()
        {
            ImportOPASData importOPAS = new ImportOPASData();
            importOPAS.Import();
        }
    }
}
