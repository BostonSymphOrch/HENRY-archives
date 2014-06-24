using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bso.Archive.BusObj;

namespace BSO.Archive.BusObj.Test.Editable
{
    [TestClass]
    public class EventParticipantTypeTest
    {
        /// <summary>
        /// Tests AddEventParticipant of the EventParticipantType class by creating an event and participant and calling
        /// the method then testing the eventParticipantType objects values to compare.
        /// </summary>
        [TestMethod]
        public void AddEventParticipantTest()
        {
            Event evt = Event.NewEvent();
            evt.EventID = 1;
            Participant participant = Participant.NewParticipant();
            participant.ParticipantID = 1;
            participant.ParticipantGroup = "Soloist";

            EventParticipantType eventParticipantType = EventParticipantType.AddParticipantType(evt, participant);

            Assert.IsNotNull(eventParticipantType);
            Assert.IsTrue(eventParticipantType.EventID == evt.EventID && eventParticipantType.ParticipantID == participant.ParticipantID && eventParticipantType.EventParticipantTypeName == participant.ParticipantGroup);
        }
    }
}
