using Bso.Archive.BusObj.Properties;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Bso.Archive.BusObj
{
    public class ImportOPASData
    {
        /// <summary>
        /// Initialize Import Process
        /// </summary>
        public void Initialize()
        {
            BsoArchiveEntities.Current.ImportProcessInitialize();
        }
        /// <summary>
        /// Finalize Import Process
        /// </summary>
        public void Finalize()
        {
            BsoArchiveEntities.Current.ImportProcessFinalize();
        }
        /// <summary>
        /// A method that takes a bool and either by default populated the database 
        /// from an xml file or updates the database from an xml file.
        /// </summary>
        /// <param name="update"></param>
        public void Import()
        {
            PopulateDatabase();

            BsoArchiveEntities.Current.Save();

        }

        /// <summary>
        /// Method to populate the databse.
        /// </summary>
        /// <remarks>
        /// Provides the default functionality of reading from xml and adding to the database.
        /// </remarks>
        private void PopulateDatabase()
        {
            var xmlPath = Settings.Default.XMLPath;
            XDocument doc = null;
            

            doc = XDocument.Load(xmlPath);

            IEnumerable<XElement> events = doc.Root.Descendants(Constants.Event.eventElement);

            foreach (XElement node in events)
            {
                Event eventItem = Event.GetEventFromNodeItem(node);
                try
                {
                    AddEventVenue(eventItem, node);
                    AddEventConductor(eventItem, node);
                    AddEventArtist(eventItem, node);
                    AddEventOrchestra(eventItem, node);
                    AddEventTypeGroup(eventItem, node);
                    AddEventType(eventItem, node);
                    AddEventSeason(eventItem, node);
                    AddEventProject(eventItem, node);
                    AddEventWorkItems(eventItem, node);
                    AddEventParticipant(eventItem, node);
                    AddEventSeries(eventItem, node);
                }
                catch (System.Exception e)
                {

                }
                BsoArchiveEntities.Current.Save();
            }
        }

        /// <summary>
        /// Create Event Series object
        /// </summary>
        /// <remarks>
        /// Takes an Even object along with an XElement node and assigns the Event Series information to
        ///  the series field of the Event object, then returns the event series information as a string.
        /// </remarks>
        /// <param name="eventItem"></param>
        /// <param name="node"></param>
        public string AddEventSeries(Event eventItem, XElement node)
        {
            string eventSeries = Event.GetSeriesFromNode(node);
            if (!string.IsNullOrEmpty(eventSeries))
                eventItem.EventSeries = eventSeries;
            
            return eventSeries;
        }

        /// <summary>
        /// Takes a file path to an xml document and returns an XmlDocument 
        /// which has loaded the file at the given path.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private XmlDocument GetXmlDocumentFromFile(string filePath)
        {
            XmlDocument dateXMLDocument = new XmlDocument();
            dateXMLDocument.Load(filePath);
            return dateXMLDocument;
        }



        /// <summary>
        /// Create EventVenue object
        /// </summary>
        /// <param name="eventItem"></param>
        /// <remarks>
        /// Takes an Event object along with an XElement node and returns an 
        /// EventVenue item based on the Even object and the Venue object created 
        /// from the given node.
        /// </remarks>
        /// <param name="node"></param>
        public Venue AddEventVenue(Event eventItem, XElement node)
        {
            Venue venueItem = Venue.GetVenueFromNode(node);

            if (venueItem != null && venueItem.VenueID != 0)
                eventItem.Venue = venueItem;

            return venueItem;
        }

        /// <summary>
        /// Takes an Event object along with an eventItem node and 
        /// returns a EventConductor object
        /// </summary>
        /// <param name="eventItem"></param>
        /// <remarks>
        /// Takes in an Event object along with an eventItem XElement node
        /// and returns an EventConductor object with the
        /// </remarks>
        /// <param name="node"></param>
        public Conductor AddEventConductor(Event eventItem, XElement node)
        {
            Conductor conductorItem = Conductor.GetConductorFromNode(node);

            if (conductorItem != null && conductorItem.ConductorID != 0)
                eventItem.Conductor = conductorItem;

            return conductorItem;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventItem"></param>
        /// <param name="node"></param>
        public void AddEventArtist(Event eventItem, XElement node)
        {
            IEnumerable<XElement> artistElements = node.Descendants(Constants.Artist.artistElement);
            foreach (XElement artistElement in artistElements)
            {
                Artist artistItem = Artist.GetArtistFromNode(artistElement);
                Instrument instrumentItem = Instrument.GetInstrumentFromNode(artistElement);
                if (artistItem != null && instrumentItem != null && artistItem.ArtistID != 0 && instrumentItem.InstrumentID != 0)
                {
                    eventItem.AddEventArtist(artistItem, instrumentItem);
                    BsoArchiveEntities.Current.Save();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventItem"></param>
        /// <param name="node"></param>
        public void AddEventParticipant(Event eventItem, XElement node)
        {
            IEnumerable<XElement> participantElements = node.Descendants("eventParticipants");
            foreach (XElement participantElement in participantElements)
            {
                Participant participant = Participant.GetParticipantFromNode(participantElement);
                if (participant != null && participant.ParticipantID != 0)
                {
                    eventItem.AddEventParticipant(participant);
                    eventItem.AddEventParticipantType(participant);
                    BsoArchiveEntities.Current.Save();
                }
            }
        }

        /// <summary>
        /// Create EventOrchestra object
        /// </summary>
        /// <param name="eventItem"></param>
        /// <remarks>
        /// Takes an Event object along with an XElement node and returns 
        /// an EventOrchestra object based on the Event object and the Orchestra
        /// object created from the XElement node.
        /// </remarks>
        /// <param name="node"></param>
        public Orchestra AddEventOrchestra(Event eventItem, XElement node)
        {
            Orchestra orchestraItem = Orchestra.GetOrchestraFromNode(node);

            if (orchestraItem != null && orchestraItem.OrchestraID != 0)
                eventItem.Orchestra = orchestraItem;

            return orchestraItem;
        }

        /// <summary>
        /// Create EventTypeGroup object
        /// </summary>
        /// <param name="eventItem"></param>
        /// <remarks>
        /// Takes an Event object along with an XElement node and returns 
        /// an EventTypeGroup item based on the Event object and the TypeGroup 
        /// object created from the given node.
        /// </remarks>
        /// <param name="node"></param>
        public EventTypeGroup AddEventTypeGroup(Event eventItem, XElement node)
        {
            EventTypeGroup typeGroupItem = EventTypeGroup.GetEventTypeGroupFromNode(node);

            if (typeGroupItem != null && typeGroupItem.TypeGroupID != 0)
                eventItem.EventTypeGroup = typeGroupItem;

            return typeGroupItem;
        }

        /// <summary>
        /// Create EventType object
        /// </summary>
        /// <remarks>
        /// Takes an Event object along with an XElement node which 
        /// represents an eventItem and gets the Type data from it 
        /// and create a new EventType object with that Type and the 
        /// Event object.
        /// </remarks>
        public EventType AddEventType(Event eventItem, XElement node)
        {
            EventType typeItem = EventType.GetEventTypeFromNode(node);

            if (typeItem != null && typeItem.TypeID != 0)
                eventItem.EventType = typeItem;

            return typeItem;
        }

        /// <summary>
        /// Tkes an Event object along with an XElement and creates an EventType 
        /// object.
        /// </summary>
        /// <param name="eventItem"></param>
        /// <remarks>
        /// Tkes an Event object along with an XElement node which represents an 
        /// eventItem and gets the Type data from the descendant eventType element 
        /// and creates a new EventType with the Event and Type objects.
        /// </remarks>
        /// <param name="node"></param>
        public Season AddEventSeason(Event eventItem, XElement node)
        {
            Season seasonItem = Season.GetSeasonFromNode(node);

            if (seasonItem != null && seasonItem.SeasonID != 0)
                eventItem.Season = seasonItem;

            return seasonItem;
        }

        /// <summary>
        /// Takes an Event object along with an XElement and creates an EventProject 
        /// object.
        /// </summary>
        /// <remarks>
        /// Takes an Event object along with an XElement node which represents 
        /// an eventItem and gets the Project data from the descendent eventProject 
        /// element and creates a new EventProject object with the Event and Project 
        /// objects.
        /// </remarks>
        /// <param name="eventItem"></param>
        /// <param name="node"></param>
        public Project AddEventProject(Event eventItem, XElement node)
        {
            Project projectItem = Project.GetProjectFromNode(node);

            if (projectItem != null && projectItem.ProjectID != 0)
                eventItem.Project = projectItem;

            return projectItem;
        }

        /// <summary>
        /// Create EventWork objects
        /// </summary>
        /// <remarks>
        /// Takes an Event object along with an XElement node which represents
        /// an eventItem and gets all the workItem child elements from it and 
        /// creates EventWork item objects for each one.
        /// </remarks>
        /// <param name="eventItem"></param>
        /// <param name="node"></param>
        public void AddEventWorkItems(Event eventItem, XElement node)
        {
            IEnumerable<XElement> workElements = node.Descendants("workItem");
            foreach (XElement workElement in workElements)
            {
                Work workItem = Work.GetWorkFromNode(workElement);
                if (workItem != null && workItem.WorkID != 0)
                    eventItem.AddEventWork(workItem);
            }
        }
    }
}
