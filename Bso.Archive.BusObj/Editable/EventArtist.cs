using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bso.Archive.BusObj
{
    partial class EventArtist
    {
        public static EventArtist AddEventArtist(Event evt, Artist artist, Instrument instrument){
            var eventArtist = EventArtist.NewEventArtist();
            eventArtist.Event = evt;
            eventArtist.Artist = artist;
            eventArtist.Instrument = instrument;
            return eventArtist;
        }
    }
}
