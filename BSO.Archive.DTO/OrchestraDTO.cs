using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSO.Archive.DTO
{
    public class OrchestraDTO
    {
        private Bso.Archive.BusObj.Orchestra orchestra;

        public OrchestraDTO(Bso.Archive.BusObj.Orchestra orchestra)
        {
            this.OrchestraName = orchestra.OrchestraName;
        }
        public string OrchestraName { get; set; }
    }
}
