using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Bso.Archive.BusObj.Interface;
using Bso.Archive.BusObj.Utility;

namespace Bso.Archive.BusObj
{
    partial class Participant : IOPASData
    {
        #region IOPASData

        /// <summary>
        /// Updates the existing database Participant on the column name using the 
        /// XML document parsed using the tagName.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="columnName"></param>
        /// <param name="tagName"></param>
        public void UpdateData(System.Xml.Linq.XDocument doc, string columnName, string tagName)
        { 
            IEnumerable<System.Xml.Linq.XElement> eventElements = doc.Descendants(Constants.Event.eventElement);
            foreach (System.Xml.Linq.XElement element in eventElements)
            {
                Participant updateParticipant = Participant.GetParticipantFromNode(element);

                if (updateParticipant == null) continue;

                System.Xml.Linq.XElement participantNode = element.Element(Constants.Participant.participantElement);

                object newValue = participantNode.GetXElement(tagName);

                BsoArchiveEntities.UpdateObject(updateParticipant, newValue, columnName);
            }
        }
        #endregion

        /// <summary>
        /// Get Participant object from XElement node
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// Takes an XElement node of eventItem and extracts the data for that events 
        /// Participant element. Checks to see if that Participant already exists, if yes than return, 
        /// if no then create a new Artist from data in node.
        /// </remarks>
        /// <returns></returns>
        public static Participant GetParticipantFromNode(System.Xml.Linq.XElement node)
        {
            if (node == null || string.IsNullOrEmpty((string)node.GetXElement(Constants.Participant.participantIDElement)))
                return null;

            int participantID;
            int.TryParse(node.GetXElement(Constants.Participant.participantIDElement), out participantID);
            Participant participant = Participant.GetParticipantByID(participantID);
            if (!participant.IsNew)
                return participant;

            string participantFirstName = node.GetXElement(Constants.Participant.participantFirstNameElement);
            string participantLastName = node.GetXElement(Constants.Participant.participantLastNameElement);
            string participantGroup = node.GetXElement(Constants.Participant.participantGroupNameElement);

            int participantStatus, participantStatusID;
            int.TryParse(node.GetXElement(Constants.Participant.participantStatusIDElement), out participantStatusID);
            int.TryParse(node.GetXElement(Constants.Participant.participantStatusElement), out participantStatus);

            SetParticipantData(participantID, participant, participantFirstName, participantLastName, participantGroup, participantStatus, participantStatusID);
            
            return participant;
        }

        private static void SetParticipantData(int participantID, Participant participant, string participantFirstName, string participantLastName, string participantGroup, int participantStatus, int participantStatusID)
        {
            participant.ParticipantID = participantID;
            participant.ParticipantFirstName = participantFirstName;
            participant.ParticipantLastName = participantLastName;
            participant.ParticipantGroup = participantGroup;
            participant.ParticipantStatus = participantStatus;
            participant.ParticipantStatusID = participantStatusID;
        }

        /// <summary>
        /// Return participant based on id
        /// </summary>
        /// <param name="artistID"></param>
        /// <remarks>
        /// Takes an int id and checks to see if an participant with that id already exists in the entity. 
        /// If it does then return it. Otherwise create a new instance of participant and return it.
        /// </remarks>
        /// <returns></returns>
        internal static Participant GetParticipantByID(int participantID)
        {
            Participant participant = BsoArchiveEntities.Current.Participants.FirstOrDefault(a => a.ParticipantID == participantID) ?? 
                Participant.NewParticipant();

            return participant;
        }

    }
}
