using System;
using Bso.Archive.BusObj;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bso.Archive.BusObj.Utility;

namespace BSO.Archive.BusObj.Test
{
    /// <summary>
    /// Summary description for EventTest
    /// </summary>
    [TestClass]
    public class EventTest
    {

        [TestMethod]
        public void UpdateEventTest()
        {
            Event testEvent = Event.GetEventByID(4);
            Assert.IsTrue(testEvent.EventText == "");

            var eventID = Helper.CreateXElement(Constants.Event.eventIDElement, "4");
            var eventText = Helper.CreateXElement(Constants.Event.eventTextElement, "Test Text ADAGE");
            var eventElement = new System.Xml.Linq.XElement(Constants.Event.eventElement, eventID, eventText);
            var rootElement = new System.Xml.Linq.XElement("Root", eventElement);
            var doc = new System.Xml.Linq.XDocument(rootElement);

            Event eventItem = Event.NewEvent();
            eventItem.UpdateData(doc, "EventText", "eventText");

            Assert.IsTrue(testEvent.EventText == "Test Text ADAGE");
        }

        /// <summary>
        /// Tests the GetEventByID method in the Event class
        /// </summary>
        [TestMethod()]
        public void TestGetEventByID()
        {
            Event evt1 = Event.GetEventByID(1);
            if (evt1.IsNew)
            {
                evt1.EventID = 1;
                evt1.EventDate = DateTime.Today;
            }

            BsoArchiveEntities.Current.Save();

            Event evt2 = Event.GetEventByID(1);
            Assert.IsNotNull(evt2);
            Assert.IsTrue(evt1.Equals(evt2));
            BsoArchiveEntities.Current.DeleteObject(evt1);
        }

        [TestMethod]
        public void GetEventFromNodeItemTest()
        {
            var xmlTestPath = "C:\\working\\BSO\\BSO.Archive\\OPASData\\EventItemTest.xml";

            System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Load(xmlTestPath);
            System.Xml.Linq.XElement nodeRoot = doc.Root.Element("eventItem");


            Event evt = Event.GetEventFromNodeItem(nodeRoot);
            DateTime actualDate = new DateTime(1882, 1, 27);

            Assert.IsNotNull(evt);
            Assert.IsTrue(evt.EventID == -1);
            Assert.IsTrue(evt.EventDate.CompareTo(actualDate) == 0);
            Assert.IsTrue(evt.EventProgramTitle == string.Empty);
        }
       

        /// <summary>
        /// Test that the AddEventWork method adds an EventWork with 
        /// the correct Work and Event objects.
        /// </summary>
        [TestMethod]
        public void AddEventWorkTest_NewWorkTest()
        {
            Event myEvent = Event.NewEvent();
            myEvent.EventID = 1;
            Assert.IsTrue(myEvent.EventWorks.Count == 0);

            Work work = Work.NewWork();
            work.WorkID = 1;

            myEvent.AddEventWork(work);
            Assert.IsTrue(myEvent.EventWorks.Count == 1);
        }

        /// <summary>
        /// Test whether when adding a Work to an EventWork, where the Work already 
        /// exists, will returns the existing Work or add the second.
        /// </summary>
        [TestMethod]
        public void AddEventWorkTest_ExistingTest()
        {
            Event myEvent = Event.NewEvent();
            myEvent.EventID = 1;
            Assert.IsTrue(myEvent.EventWorks.Count == 0);

            EventWork eventWork = EventWork.NewEventWork();
            eventWork.EventWorkID = 1;

            Work work = Work.NewWork();
            work.WorkID = 1;

            eventWork.Event = myEvent;
            eventWork.Work = work;
            myEvent.EventWorks.Add(eventWork);

            Assert.IsTrue(myEvent.EventWorks.Count == 1);

            myEvent.AddEventWork(work);

            Assert.IsTrue(myEvent.EventWorks.Count == 1);
        }

        /// <summary>
        /// Test that the AddEventArtist method adds an EventArtist with 
        /// the correct Artist and Event objects.
        /// </summary>
        [TestMethod]
        public void AddEventArtist_NewArtist()
        {
            Event myEvent = Event.NewEvent();
            myEvent.EventID = 1;

            Assert.IsTrue(myEvent.EventArtists.Count == 0);

            Artist artist = Artist.NewArtist();
            artist.ArtistID = 9999;

            Instrument instrument = Instrument.NewInstrument();
            instrument.InstrumentID = 9999;

            myEvent.AddEventArtist(artist, instrument);

            Assert.IsTrue(myEvent.EventArtists.Count == 1);
        }

        /// <summary>
        /// Test whether when adding a Artist to an EventArtist, where the Artist already 
        /// exists, will returns the existing Artist or add the second.
        /// </summary>
        [TestMethod]
        public void AddEventArtist_ExistingTest()
        {
            Event myEvent = Event.NewEvent();
            myEvent.EventID = 1;

            Artist artist = Artist.NewArtist();
            artist.ArtistID = 9999;

            Instrument instrument = Instrument.NewInstrument();
            instrument.InstrumentID = 9999;

            EventArtist eventArtist = EventArtist.NewEventArtist();
            eventArtist.EventArtistID = 1;

            eventArtist.Artist = artist;
            eventArtist.Event = myEvent;
            eventArtist.Instrument = instrument;
            myEvent.EventArtists.Add(eventArtist);
            Assert.IsTrue(myEvent.EventArtists.Count == 1);


            myEvent.AddEventArtist(artist, instrument);
            Assert.IsTrue(myEvent.EventArtists.Count == 1);
        }

    }
}
