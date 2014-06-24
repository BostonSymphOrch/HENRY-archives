using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bso.Archive.BusObj;

namespace BSO.Archive.BusObj.Test.Editable
{
    [TestClass]
    public class EventArtistTest
    {
        /// <summary>
        /// Tests the AddEventArtist method of the EventArtist class
        /// </summary>
        [TestMethod]
        public void AddEventArtistTest()
        {
            Event evt = Event.NewEvent();
            evt.EventID = 1;

            Artist artist = Artist.NewArtist();
            artist.ArtistID = 1;

            EventArtist eventArtist = null;//EventArtist.AddEventArtist(evt, artist);
            Assert.IsNotNull(eventArtist);
            Assert.IsTrue(eventArtist.EventID == evt.EventID && eventArtist.ArtistID == artist.ArtistID);
        }
    }
}
