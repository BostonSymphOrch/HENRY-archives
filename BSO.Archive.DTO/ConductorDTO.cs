using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSO.Archive.DTO
{
    public class ConductorDTO
    {
        private Bso.Archive.BusObj.Conductor conductor;

        public ConductorDTO(Bso.Archive.BusObj.Conductor conductor)
        {
            this.ConductorFullName = conductor.ConductorFullName ?? "";
        }
        public string ConductorFullName { get; set; }
    }
}
