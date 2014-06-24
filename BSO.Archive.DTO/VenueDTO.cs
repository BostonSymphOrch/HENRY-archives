using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSO.Archive.DTO
{
    public class VenueDTO
    {
        private Bso.Archive.BusObj.Venue venue;

        public VenueDTO(Bso.Archive.BusObj.Venue venue)
        {
            this.VenueCity = venue.VenueCity;
            this.VenueState = venue.VenueState.Trim();
            this.VenueCountry = venue.VenueCountry;
            this.VenueName = venue.VenueName;
        }
        public string VenueLocation 
        {
            get
            {
                return string.Concat(VenueCity, ", ", VenueState, ", ", VenueCountry);
            }
        }
        public string VenueCity { get; set; }
        public string VenueState { get; set; }
        public string VenueCountry { get; set; }
        public string VenueName { get; set; }
    }
}
