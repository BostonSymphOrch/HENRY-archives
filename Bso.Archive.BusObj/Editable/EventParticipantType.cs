using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bso.Archive.BusObj
{
    partial class EventParticipantType
    {
        /// <summary>
        /// Takes event and participant objects and creates a new EventParticipantType and returns it.
        /// </summary>
        /// <param name="evt"></param>
        /// <param name="participant"></param>
        /// <returns></returns>
        public static EventParticipantType AddParticipantType(Event evt, Participant participant)
        {
            var eventParticipantType = EventParticipantType.NewEventParticipantType();
            eventParticipantType.Event = evt;
            eventParticipantType.ParticipantID = participant.ParticipantID;
            eventParticipantType.EventParticipantTypeName = participant.ParticipantGroup;
            return eventParticipantType;
        }
    }
}
