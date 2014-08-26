using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bso.Archive.BusObj;

namespace BSO.Archive.BusObj.Test.Editable
{
    [TestClass]
    public class EventParticipantTest
    {
        /// <summary>
        /// Tests the AddEventParticipant method of the EventParticipant class
        /// </summary>
        [TestMethod]
        public void AddEventParticipantTest()
        {
            Event evt = Event.NewEvent();
            evt.EventID = -1;

            Participant participant = Participant.GetParticipantByID(-1);
            if (!participant.IsNew)
            {
                participant.ParticipantID = -1;
            }

            EventParticipant eventParticipant = EventParticipant.AddEventParticipant(evt, participant);
            Assert.IsNotNull(eventParticipant);
            Assert.IsTrue(eventParticipant.EventID == evt.EventID && eventParticipant.ParticipantID == participant.ParticipantID);
          
        }
    }
}
