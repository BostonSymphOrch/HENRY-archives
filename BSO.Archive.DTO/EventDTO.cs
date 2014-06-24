using Bso.Archive.BusObj;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BSO.Archive.DTO
{
    [Serializable]
    public class EventHelper
    {
        public EventHelper()
        {
            lstEventDetailIds = new List<int>();
        }
        public int EventId { get; set; }
        public List<int> lstEventDetailIds { get; set; }
    }
    public class EventDTO
    {
        public string EventProgramTitle { get; set; }
        public int EventID { get; set; }
        public int EventProgramNo { get; set; }
        public DateTime EventDate { get; set; }
        public string EventDateString
        {
            get
            {
                return EventDate.ToShortDateString();
            }
        }

        public string EventStartTime { get; set; }
        public string EventEndTime { get; set; }

        public string DetailLink { get; set; }
        public string ProgramBookLink { get; set; }

        public List<ArtistDTO> artists = new List<ArtistDTO>();
        public List<WorkDTO> works = new List<WorkDTO>();
        public List<EventDTO> events = new List<EventDTO>();

        public ConductorDTO conductor;
        public VenueDTO venue;
        public OrchestraDTO orchestra;
        public SeasonDTO season;

        /// <summary>
        /// Returns a List of EventDTOs build by using EventIDs from EventDetail objects with an EventDetailID from given list of EventDetailIDs
        /// </summary>
        /// <param name="eventDetailIds"></param>
        /// <returns></returns>
        public static List<EventDTO> GetEventDTOByEventDetailIDs(string eventDetailIds)
        {
            var eventDTO = new EventDTO();

            eventDTO.ConstructEventDTO(eventDetailIds);

            return eventDTO.events;   
        }

        private void ConstructEventDTO(string eventDetailIds)
        {
            List<EventDTO> lstEventDTOs = new List<EventDTO>();
            foreach (string eventDetailId in eventDetailIds.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                int detailId = int.Parse(eventDetailId);

                GetEventDTOByEventID(detailId);
            }
        }

        private EventDTO GetEventDTOByDTOID(int eventID){
        
            var evtDTO = events.FirstOrDefault(ev => ev.EventID == eventID);

            if (evtDTO == null)
            {
                var evt = Event.GetEventByID(eventID);

                var eventDTO = new EventDTO
                {
                    EventProgramTitle = evt.EventProgramTitle,
                    EventDate = evt.EventDate,
                    EventStartTime = evt.EventStart,
                    EventEndTime = evt.EventEnd,
                    EventID = evt.EventID,
                    EventProgramNo = evt.EventProgramNumber
                };

                eventDTO.ProgramBookLink = String.Concat(Properties.Settings.Default.ContentDMLink, eventDTO.EventProgramNo);
                eventDTO.DetailLink = String.Concat("Detail.aspx?UniqueKey=", eventDTO.EventID.ToString());

                events.Add(eventDTO);

                return eventDTO;
            }

            return evtDTO;
        }

        /// <summary>
        /// Get Event DTO by Event Id
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public  EventDTO GetEventDTOByEventID(int eventDetailId)
        {
            var eventDetail = Event.GetEventDetailByID(eventDetailId);

            var evt = Event.GetEventByID(eventDetail.EventID);

            var eventDTO = GetEventDTOByDTOID(evt.EventID);
            
            AddEventArtistDTO(eventDetail, eventDTO);

            AddEventWorkDTO(eventDetail, eventDTO);

            //case where we exclude all works based on WorkGroupId criteria
            if (eventDTO.works.Count == 0)
                events.Remove(eventDTO);

            eventDTO.season = new SeasonDTO(evt.Season);

            eventDTO.conductor = new ConductorDTO(evt.Conductor);

            eventDTO.venue = new VenueDTO(evt.Venue);

            eventDTO.orchestra = new OrchestraDTO(evt.Orchestra);

            return eventDTO;
        }

        private void AddEventArtistDTO(EventDetail eventDetail, EventDTO eventDTO)
        {
            if (eventDetail.ArtistID == null)
                return;

            var artist = eventDTO.artists.FirstOrDefault(a => a.ArtistID == eventDetail.ArtistID);
            var instrumentID = eventDetail.InstrumentID ?? 0;
            string instrument;
            if (instrumentID == 0)
                instrument = "";
            else
                instrument = Instrument.GetInstrumentByID((int)eventDetail.InstrumentID).Instrument1;
                

            if (artist == null){
                var artistID = eventDetail.ArtistID;
                if (artistID == null)
                    return;

                var artistObj = Artist.GetArtistByID((int)eventDetail.ArtistID);

                artist = new ArtistDTO
                {
                    ArtistFullName = artistObj.ArtistFullName,
                    ArtistInstrument = instrument,
                    ArtistID = (int)eventDetail.ArtistID
                };

                eventDTO.artists.Add(artist);
            }
               
        }


        private void AddEventWorkDTO(EventDetail eventDetail, EventDTO eventDTO)
        {
            if (eventDetail.WorkID == null || Work.WorkShouldBeExcludedByGroupId((int)eventDetail.WorkGroupId))
                return;

            var work = eventDTO.works.FirstOrDefault(w => w.workID == (int)eventDetail.WorkID);
            
            if (work == null)
            {
                work = new WorkDTO
                {
                    WorkTitle = eventDetail.WorkTitle,
                    ComposerFullName = eventDetail.ComposerFullName,
                    workID = (int)eventDetail.WorkID,
                    Arranger = eventDetail.WorkArrangement,
                    WorkArtists = new List<ArtistDTO>()
                };

                eventDTO.works.Add(work);
            }

            var artistID = eventDetail.ArtistID;
            if (artistID == null)
                return;

            var workArtist = work.WorkArtists.FirstOrDefault(wa => wa.ArtistID == (int)eventDetail.ArtistID);

            var instrumentID = eventDetail.InstrumentID ?? 0;
            string instrument;
            if (instrumentID == 0)
                instrument = "";
            else
                instrument = Instrument.GetInstrumentByID((int)eventDetail.InstrumentID).Instrument1;

            if (workArtist == null)
            {
                var artist = Artist.GetArtistByID((int)artistID);

                workArtist = new ArtistDTO
                {
                    ArtistFullName = artist.ArtistFullName,
                    ArtistInstrument = instrument,
                    ArtistID = (int)eventDetail.ArtistID
                };

                work.WorkArtists.Add(workArtist);
            }
        }
    }

}
