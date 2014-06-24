using System.Collections.Generic;

namespace BSO.Archive.DTO
{
    public class WorkDTO
    {
        public int workID { get; set; }
        public int workGroupID { get; set; }
        public string WorkTitle { get; set; }
        public string ComposerFullName { get; set; }
        public string Arranger { get; set; }
        public int PerformanceCount { get; set; }
        public int ConductorCount { get; set; }
        public int SoloistCount { get; set; }
        public int ParticipantCount { get; set; }
        public int EnsembleCount { get; set; }
        public string WorkLink { get; set; }
        public List<ArtistDTO> WorkArtists { get; set; }
    }
}
