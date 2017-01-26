using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSO.Archive.DTO
{
    public class WorkDocumentDTO
    {
        public int WorkDocumentID { get; set; }
        public string WorkDocumentName { get; set; }
        public string WorkDocumentSummary { get; set; }
        public string WorkDocumentNotes { get; set; }
        public string WorkDocumentFileLocation { get; set; }

    }
}
