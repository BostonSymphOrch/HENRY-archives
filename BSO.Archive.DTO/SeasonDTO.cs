using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSO.Archive.DTO
{
    public class SeasonDTO
    {
        private Bso.Archive.BusObj.Season season;

        public SeasonDTO(Bso.Archive.BusObj.Season season)
        {
            this.SeasonName = season.SeasonName;
        }
        public string SeasonName { get; set; }
    }
}
