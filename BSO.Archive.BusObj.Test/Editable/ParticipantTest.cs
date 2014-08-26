using System;
using Bso.Archive.BusObj;
using Bso.Archive.BusObj.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BSO.Archive.BusObj.Test.Editable
{
    [TestClass]
    public class ParticipantTest
    {
        [Ignore]
        [TestMethod]
        public void UpdateParticipantTest()
        {
            Participant testParticipant = Participant.GetParticipantByID(-1);
            if (testParticipant.IsNew)
            {
                testParticipant.ParticipantID = -1;
            }
            testParticipant.ParticipantFirstName = "Adage";
            BsoArchiveEntities.Current.Save();

            var participantID = Helper.CreateXElement(Constants.Participant.participantIDElement, "-1");
            var participantFirstName = Helper.CreateXElement(Constants.Participant.participantFirstNameElement, "Test");
            var participantItem = new System.Xml.Linq.XElement(Constants.Participant.participantElement, participantID, participantFirstName);
            var eventItem = new System.Xml.Linq.XElement(Constants.Event.eventElement, participantItem);
            System.Xml.Linq.XDocument doc = new System.Xml.Linq.XDocument(eventItem);

            Participant participant = Participant.NewParticipant();
            participant.UpdateData(doc, "ParticipantFirstName", Constants.Participant.participantFirstNameElement);
            Assert.IsTrue(testParticipant.ParticipantFirstName == "Test");
            BsoArchiveEntities.Current.DeleteObject(testParticipant);
            BsoArchiveEntities.Current.DeleteObject(participant);
            BsoArchiveEntities.Current.Save();
        }

        /// <summary>
        /// Tests GetParticipantFromNodeItem by creating an eventItem XElement element and passing it
        /// to the method. Checks that the object was created and with the correct values.
        /// </summary>
        [TestMethod]
        public void GetParticipantFromNodeItemTest()
        {
            var participantID = Helper.CreateXElement(Constants.Participant.participantIDElement, "-1");
            var participantFirstName = Helper.CreateXElement(Constants.Participant.participantFirstNameElement, "TestFName");
            var participantLastName = Helper.CreateXElement(Constants.Participant.participantLastNameElement, "TestLName");
            var participantGroup = Helper.CreateXElement(Constants.Participant.participantGroupNameElement, "TestGroup");
            var participantItem = new System.Xml.Linq.XElement(Constants.Participant.participantElement, participantID, participantFirstName, participantLastName, participantGroup);

            Participant participant = Participant.GetParticipantFromNode(participantItem);
            Assert.IsNotNull(participant);
            Assert.IsTrue(participant.ParticipantID == -1);
        }
    }
}