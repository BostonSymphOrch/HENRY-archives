using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bso.Archive.BusObj;
using Bso.Archive.BusObj.Utility;

namespace BSO.Archive.BusObj.Test.Editable
{
    [TestClass]
    public class ParticipantTest
    {
        [TestMethod]
        public void UpdateParticipantTest()
        {
            Participant testParticipant = Participant.GetParticipantByID(1997);
            Assert.IsTrue(testParticipant.ParticipantStatus == 0);

            var participantID = Helper.CreateXElement(Constants.Participant.participantIDElement, "1997");
            var participantStatus = Helper.CreateXElement(Constants.Participant.participantStatusElement, "1");
            var participantItem = new System.Xml.Linq.XElement(Constants.Participant.participantElement, participantID, participantStatus);
            var eventItem = new System.Xml.Linq.XElement(Constants.Event.eventElement, participantItem);
            System.Xml.Linq.XDocument doc = new System.Xml.Linq.XDocument(eventItem);

            Participant participant = Participant.NewParticipant();
            participant.UpdateData(doc, "ParticipantStatus", "eventParticipantStatus");
            Assert.IsTrue(testParticipant.ParticipantStatus == 1);
        }

        /// <summary>
        /// Tests GetParticipantFromNodeItem by creating an eventItem XElement element and passing it 
        /// to the method. Checks that the object was created and with the correct values. 
        /// </summary>
        [TestMethod]
        public void GetParticipantFromNodeItemTest()
        {
            var participantID = Helper.CreateXElement(Constants.Participant.participantIDElement, "1");
            var participantFirstName = Helper.CreateXElement(Constants.Participant.participantFirstNameElement, "TestFName");
            var participantLastName = Helper.CreateXElement(Constants.Participant.participantLastNameElement, "TestLName");
            var participantGroup = Helper.CreateXElement(Constants.Participant.participantGroupNameElement, "TestGroup");
            var participantItem = new System.Xml.Linq.XElement(Constants.Participant.participantElement, participantID, participantFirstName, participantLastName, participantGroup);
            var node = new System.Xml.Linq.XElement(Constants.Event.eventElement, participantItem);


            Participant participant = Participant.GetParticipantFromNode(node);
            Assert.IsNotNull(participant);
            Assert.IsTrue(participant.ParticipantID == 1 && participant.ParticipantFirstName == "TestFName");
        }

        /// <summary>
        /// Test the GetParticipantByID method from the Participant class
        /// </summary>
        [TestMethod]
        public void GetParticipantByIDTest()
        {
            Participant participant1 = Participant.GetParticipantByID(2);

            if (participant1.IsNew)
                participant1.ParticipantID = 2;

            BsoArchiveEntities.Current.Save();

            Participant participant2 = Participant.GetParticipantByID(2);
            Assert.IsNotNull(participant2);

            Assert.IsTrue(participant1.Equals(participant2));
        }

    }
}
