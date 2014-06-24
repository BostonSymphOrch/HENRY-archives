using Bso.Archive.BusObj.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Bso.Archive.BusObj.Utility;

namespace Bso.Archive.BusObj
{
    partial class Event : IOPASData
    {
        public string EventStartDate
        {
            get
            {
                return String.Concat(EventDate.ToShortDateString(), " ", EventStart);
            }
        }

        [System.ComponentModel.DataObjectField(false)]
        public string ConductorFullName
        {
            get
            {
                return this.Conductor.ConductorFullName;
            }
        }

        #region Import
        #region IOPASData

        /// <summary>
        /// Updates the existing database Event on the column name using the 
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
                Event updateEvent = Event.GetEventFromNodeItem(element);

                if (updateEvent == null) continue;

                object newValue = element.GetXElement(tagName);

                BsoArchiveEntities.UpdateObject(updateEvent, newValue, columnName);
            }
        }

        #endregion


        /// <summary>
        /// Add an event work
        /// </summary>
        /// <param name="work"></param>
        /// <remarks>
        /// Check if the event work exists. If it does then return the event work object, otherwise
        /// create a new event work object and return it.
        /// </remarks>
        /// <returns></returns>
        public EventWork AddEventWork(Work work)
        {
            var eventWork = this.EventWorks.FirstOrDefault(ep => ep.WorkID == work.WorkID && ep.WorkPremiere == work.WorkPremiere);

            if (eventWork != null) return eventWork;

            eventWork = EventWork.AddEventWork(this, work);

            return eventWork;
        }

        /// <summary>
        /// Add an event artist
        /// </summary>
        /// <param name="artist"></param>
        /// <param name="instrument"></param>
        /// <remarks>
        /// Check if the event artist exists. If it does then return the event artist object, otherwise
        /// create a new event artist object and return it.
        /// </remarks>
        /// <returns></returns>
        public EventArtist AddEventArtist(Artist artist, Instrument instrument)
        {
            var eventArtist = this.EventArtists.FirstOrDefault(ea => ea.ArtistID == artist.ArtistID && ea.InstrumentID == instrument.InstrumentID);

            if (eventArtist != null) return eventArtist;

            eventArtist = EventArtist.AddEventArtist(this, artist, instrument);

            return eventArtist;
        }

        /// <summary>
        /// Add and event Participant
        /// </summary>
        /// <param name="participant"></param>
        /// <remarks>
        /// Check if the event participant exists. If it does then return the event participant object, otherwise 
        /// create a new event participant object and return it.
        /// </remarks>
        /// <returns></returns>
        public EventParticipant AddEventParticipant(Participant participant)
        {
            var eventParticipant = this.EventParticipants.FirstOrDefault(ep => ep.ParticipantID == participant.ParticipantID);

            if (eventParticipant != null) return eventParticipant;

            eventParticipant = EventParticipant.AddEventParticipant(this, participant);

            return eventParticipant;
        }

        /// <summary>
        /// Add an EventParticipantType
        /// </summary>
        /// <param name="participant"></param>
        /// <remarks>Check if an event participant type exists. If yes return it, otherwise create a 
        /// new event participant type object and return it.</remarks>
        /// <returns></returns>
        public EventParticipantType AddEventParticipantType(Participant participant)
        {
            var eventParticipantType = this.EventParticipantTypes.FirstOrDefault(ept => ept.ParticipantID == participant.ParticipantID);

            if (eventParticipantType != null) return eventParticipantType;

            eventParticipantType = EventParticipantType.AddParticipantType(this, participant);

            return eventParticipantType;
        }

        /// <summary>
        /// Get Event object based on ID
        /// </summary>
        /// <remarks>
        /// Searches the current entity context for an Event with the same ID. If it exists
        /// return it. Otherwise create a new Event object in the entity context and return
        /// it.
        /// </remarks>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public static Event GetEventByID(int eventID)
        {
            var eventById = BsoArchiveEntities.Current.Events.FirstOrDefault(e => e.EventID == eventID) ??
                   Event.NewEvent();

            return eventById;

        }

        /// <summary>
        /// Get Event object from XmlNode
        /// </summary>
        /// <param name="eventItemNode"></param>
        /// <remarks>
        /// Takes a event Node from the list of node events and gets the values of 
        /// its child elements and creates a new Event object with the values then 
        /// returns the Event object.
        /// </remarks>
        /// <returns></returns>
        public static Event GetEventFromNodeItem(System.Xml.Linq.XElement eventItemNode)
        {
            int eventID;
            int.TryParse((string)eventItemNode.GetXElement(Constants.Event.eventIDElement), out eventID);

            Event eventItem = Event.GetEventByID(eventID);
            if (!eventItem.IsNew)
                return eventItem;

            eventItem = BuildEventItem(eventItemNode, eventID, eventItem);

            return eventItem;
        }

        /// <summary>
        /// Extracts the Event data from a node and assigns the values to the Event object.
        /// </summary>
        /// <param name="eventItemNode"></param>
        /// <param name="eventID"></param>
        /// <param name="eventItem"></param>
        /// <returns></returns>
        private static Event BuildEventItem(System.Xml.Linq.XElement eventItemNode, int eventID, Event eventItem)
        {
            int eventLevel, eventProgramNo;
            int.TryParse((string)eventItemNode.GetXElement(Constants.Event.eventLevelElement), out eventLevel);
            int.TryParse((string)eventItemNode.GetXElement(Constants.Event.eventProgramNoElement), out eventProgramNo);

            string eventText = (string)eventItemNode.GetXElement(Constants.Event.eventTextElement);
            string eventNote = (string)eventItemNode.GetXElement(Constants.Event.eventNoteElement);
            string programTitle = (string)eventItemNode.GetXElement(Constants.Event.eventProgramTitleElement);
            string eventDate = (string)eventItemNode.GetXElement(Constants.Event.eventDateElement);
            string eventStart = (string)eventItemNode.GetXElement(Constants.Event.eventStartElement);
            string eventEnd = (string)eventItemNode.GetXElement(Constants.Event.eventEndElement);
            eventItem.SetEventData(eventID, eventLevel, eventProgramNo, eventText, eventNote, programTitle, eventDate, eventStart, eventEnd);

            return eventItem;
        }

        /// <summary>
        /// Sets the variables for an Event object
        /// </summary>
        /// <remarks>
        /// Takes an event object and its values and assigns them to that object.
        /// </remarks>
        /// <param name="eventID"></param>
        /// <param name="eventLevel"></param>
        /// <param name="eventProgramNo"></param>
        /// <param name="eventText"></param>
        /// <param name="eventNote"></param>
        /// <param name="programTitle"></param>
        /// <param name="eventDate"></param>
        /// <param name="eventStart"></param>
        /// <param name="eventEnd"></param>
        private void SetEventData(int eventID, int eventLevel, int eventProgramNo, string eventText, string eventNote, string programTitle, string eventDate, string eventStart, string eventEnd)
        {
            this.EventID = eventID;
            this.EventText = eventText;
            this.EventNote = eventNote;
            this.EventLevel = eventLevel;
            this.EventProgramTitle = programTitle;
            this.EventProgramNumber = eventProgramNo;
            this.EventStart = eventStart;
            this.EventEnd = eventEnd;
            
            DateTime eventDateParsed;
            DateTime.TryParse(eventDate, out eventDateParsed);
            this.EventDate = eventDateParsed;
        }
        #endregion

        

        public static EventDetail GetEventDetailByID(int eventDetailID)
        {
            var eventDetailByID = BsoArchiveEntities.Current.EventDetails.FirstOrDefault(ed => ed.EventDetailID == eventDetailID) ?? EventDetail.NewEventDetail();

            return eventDetailByID;
        }
    }
}
