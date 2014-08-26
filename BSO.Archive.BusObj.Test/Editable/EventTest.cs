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
            Event testEvent = Event.GetEventByID(-1);
            if (testEvent.IsNew)
            {
                testEvent.EventID = -1;
            }
            testEvent.EventDate = DateTime.Now;
            testEvent.EventText = "Adage";
            BsoArchiveEntities.Current.Save();

            var eventID = Helper.CreateXElement(Constants.Event.eventIDElement, "-1");
            var eventText = Helper.CreateXElement(Constants.Event.eventTextElement, "Test");
            var eventElement = new System.Xml.Linq.XElement(Constants.Event.eventElement, eventID, eventText);
            var rootElement = new System.Xml.Linq.XElement("Root", eventElement);
            var doc = new System.Xml.Linq.XDocument(rootElement);

            Event eventItem = Event.NewEvent();
            eventItem.UpdateData(doc, "EventText", "eventText");

            Assert.IsTrue(testEvent.EventText == "Test");
            BsoArchiveEntities.Current.DeleteObject(testEvent);
            BsoArchiveEntities.Current.DeleteObject(eventItem);
            BsoArchiveEntities.Current.Save();
        }

        /// <summary>
        /// Tests the GetSeriesFromNode method in the Event Class
        /// </summary>
        [TestMethod()]
        public void GetSeriesFromNodeTest()
        {
            string testString = "Open Rehearsal - AM";
            var eventSeriesNameNode = Helper.CreateXElement(Constants.Series.seriesName, testString);
            var eventSeriesNode = new System.Xml.Linq.XElement(Constants.Series.seriesElement, eventSeriesNameNode);

            var eventSeriesNameNode2 = Helper.CreateXElement(Constants.Series.seriesName, testString);
            var eventSeriesNode2 = new System.Xml.Linq.XElement(Constants.Series.seriesElement, eventSeriesNameNode);

            var eventNode = new System.Xml.Linq.XElement(Constants.Event.eventElement, eventSeriesNode, eventSeriesNode2);

            Assert.IsTrue(String.Compare(String.Format("{0}; {1}", testString, testString), Event.GetSeriesFromNode(eventNode)) == 0);
        }

        /// <summary>
        /// Tests the GetEventByID method in the Event class
        /// </summary>

        [TestMethod]
        public void GetEventFromNodeItemTest()
        {
            var eventID = Helper.CreateXElement(Constants.Event.eventIDElement, "-1");
            var eventDate = Helper.CreateXElement(Constants.Event.eventDateElement, "10/25/2001");
            var nodeRoot = new System.Xml.Linq.XElement(Constants.Event.eventElement, eventID, eventDate);
            
            Event evt = Event.GetEventFromNodeItem(nodeRoot);
            DateTime actualDate = new DateTime(2001, 10, 25);

            Assert.IsNotNull(evt);
            Assert.IsTrue(evt.EventID == -1);
        }
       

        /// <summary>
        /// Test that the AddEventWork method adds an EventWork with 
        /// the correct Work and Event objects.
        /// </summary>
        [TestMethod]
        public void AddEventWorkTest_NewWorkTest()
        {
            Event myEvent = Event.NewEvent();
            myEvent.EventID = -1;
            Assert.IsTrue(myEvent.EventWorks.Count == 0);

            Work work = Work.NewWork();
            work.WorkID = -1;

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
            myEvent.EventID = -1;
            Assert.IsTrue(myEvent.EventWorks.Count == 0);

            EventWork eventWork = EventWork.NewEventWork();
            eventWork.EventWorkID = 1;

            Work work = Work.NewWork();
            work.WorkID = -1;

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
            myEvent.EventID = -1;

            Assert.IsTrue(myEvent.EventArtists.Count == 0);

            Artist artist = Artist.NewArtist();
            artist.ArtistID = -1;

            Instrument instrument = Instrument.NewInstrument();
            instrument.InstrumentID = -1;

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
            myEvent.EventID = -1;

            Artist artist = Artist.NewArtist();
            artist.ArtistID = -1;

            Instrument instrument = Instrument.NewInstrument();
            instrument.InstrumentID = -1;

            EventArtist eventArtist = EventArtist.NewEventArtist();
            eventArtist.EventArtistID = -1;

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
