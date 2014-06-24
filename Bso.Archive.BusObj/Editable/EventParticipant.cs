using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bso.Archive.BusObj
{
    partial class EventParticipant
    {
        public static EventParticipant AddEventParticipant(Event evt, Participant participant)
        {
            var eventParticipant = EventParticipant.NewEventParticipant();
            eventParticipant.Event = evt;
            eventParticipant.Participant = participant;
            return eventParticipant;
        }
    }
}
