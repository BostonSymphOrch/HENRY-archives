using Bso.Archive.BusObj;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BSO.Archive.DTO
{
    public class ArtistDTO
    {
        public string ArtistFullName { get; set; }
        public string ArtistInstrument { get; set; }
        public int ArtistID { get; set; }
        public int ConductorCount { get; set; }
        public int SoloistCount { get; set; }
        public int EnsembleCount { get; set; }

        public string ConductorLink { get; set; }
        public string SoloistLink { get; set; }
        public string OrchestraLink { get; set; }

        public WorkDTO work;
        public List<InstrumentDTO> artistInstruments = new List<InstrumentDTO>();

        /// <summary>
        /// Return List of ArtistDTOs build from string of ArtistDetailIDs
        /// </summary>
        /// <param name="artistDetailIds"></param>
        /// <returns></returns>
        public static List<ArtistDTO> GetArtistDTOByArtistIDs(string artistDetailIds)
        {
            List<ArtistDTO> lstArtistDTOs = new List<ArtistDTO>();
            foreach (string artistDetailId in artistDetailIds.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                int detailId = int.Parse(artistDetailId);

                var artistDTO = GetArtistDTOByArtistDetailID(detailId, lstArtistDTOs);

            }

            return lstArtistDTOs;
        }

        /// <summary>
        /// Get Artist DTO by Artist Id
        /// </summary>
        /// <param name="artistDetailId"></param>
        /// <returns></returns>
        public static ArtistDTO GetArtistDTOByArtistDetailID(int artistDetailId, List<ArtistDTO> artistDTOList)
        {
            var artistDetail = ArtistDetail.GetArtistDetailByID(artistDetailId);

            var artist = artistDTOList.FirstOrDefault(a => a.ArtistFullName == artistDetail.EnsembleName && a.work.WorkTitle == artistDetail.WorkTitle);


            if (artist == null)
            {
                artist = new ArtistDTO
                {
                    ArtistFullName = artistDetail.EnsembleName ?? "",
                    artistInstruments = new List<InstrumentDTO>(),
                    ConductorCount = 0,
                    SoloistCount = 0,
                    EnsembleCount = 0
                };

                var artistsWork = new WorkDTO
                {
                    WorkTitle = artistDetail.WorkTitle ?? "",
                    ComposerFullName = artistDetail.ComposerFullName ?? ""
                };

                artist.work = artistsWork;

                String composerLink = String.Concat("&Composer=", artist.work.ComposerFullName, "&Work=", artist.work.WorkTitle);
                String linkBase = "Search.aspx?searchType=Performance";
                artist.ConductorLink = String.Concat(linkBase, "&Conductor=", artist.ArtistFullName, composerLink);
                artist.SoloistLink = String.Concat(linkBase, "&Soloist=", artist.ArtistFullName, composerLink);
                artist.OrchestraLink = String.Concat(linkBase, "&Orchestra=", artist.ArtistFullName, composerLink);

                int artistDetailWorkId = artistDetail.WorkId ?? 0;

                if (!Work.WorkShouldBeExcludedById(artistDetailWorkId) && artistDetailWorkId != 0)
                    artistDTOList.Add(artist);
            }

            if (artistDetail.EnsembleType == "Orchestra")
            {
                artist.EnsembleCount++;
                AddRoleToArtist(artistDetail, artist);
            }

            if (artistDetail.EnsembleType == "Conductor")
            {
                artist.ConductorCount++;
                AddRoleToArtist(artistDetail, artist);
            }

            if (artistDetail.EnsembleType == "Soloist")
            {
                artist.SoloistCount++;

                var instrumentID = artistDetail.InstrumentID;

                var instrument = new InstrumentDTO(instrumentID);
                var artistInstrument = artist.artistInstruments.FirstOrDefault(i => i.Instrument1 == instrument.Instrument1);

                if (artistInstrument == null)
                {
                    artistInstrument = instrument;
                    artist.artistInstruments.Add(artistInstrument);
                }
            }

            return artist;
        }

        private static void AddRoleToArtist(ArtistDetail artistDetail, ArtistDTO artist)
        {
            var role = artistDetail.Instrument1;

            var instrument = new InstrumentDTO(role);
            var artistInstrument = artist.artistInstruments.FirstOrDefault(i => i.Instrument1 == instrument.Instrument1);

            if (artistInstrument == null)
            {
                artistInstrument = instrument;
                artist.artistInstruments.Add(artistInstrument);
            }
        }
    }
}
