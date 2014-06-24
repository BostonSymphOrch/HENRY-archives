using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bso.Archive.BusObj;


namespace BSO.Archive.DTO
{
    public class RepertoireDTO
    {
        public List<WorkDTO> works;
        

        public RepertoireDTO()
        {
            works = new List<WorkDTO>();
        }


        /// <summary>
        /// Given string of eventDetailIDs, buildls a List of works that populates the repertoire search results.
        /// </summary>
        /// <param name="eventDetailIDs"></param>
        /// <returns></returns>
        public static List<WorkDTO> GetWorkDTOByEventDetailIDs(string eventDetailIDs)
        {
            var repertoireDTO = new RepertoireDTO();

            repertoireDTO.ConstructDTO(eventDetailIDs);

            return repertoireDTO.works;
        }

        private void ConstructDTO(string eventDetailIDs)
        {
            foreach (string eventId in eventDetailIDs.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                int eventDetailID = int.Parse(eventId);
                
                var work = BuildRepertoireDTOByEventDetailID(eventDetailID);
            }
        }

        /// <summary>
        /// Given an EventDetailID builds a WorkDTO based on work from given EventDetail WorkID
        /// </summary>
        /// <param name="eventDetailID"></param>
        /// <returns></returns>
        public WorkDTO BuildRepertoireDTOByEventDetailID(int eventDetailID)
        {
            var eventDetail = Event.GetEventDetailByID(eventDetailID);
            var workID = eventDetail.WorkID;
            int eventDetailWorkGroupId = eventDetail.WorkGroupId ?? 0;

            if (workID == null || Work.WorkShouldBeExcludedByGroupId(eventDetailWorkGroupId))
                return null;

            var work = works.FirstOrDefault(w => w.WorkTitle == eventDetail.WorkTitle && w.ComposerFullName == eventDetail.ComposerFullName);
            
            if (work == null)
            {
                var evt = Event.GetEventByID(eventDetail.EventID);
                var workItem = Work.GetWorkByID((int)workID);
                var count = BsoArchiveEntities.Current.Works.Count(w => w.WorkGroupID == workItem.WorkGroupID);
                work = new WorkDTO
                {
                    WorkTitle = eventDetail.WorkTitle,
                    workID = workItem.WorkID,
                    workGroupID = workItem.WorkGroupID,
                    Arranger = workItem.WorkArrangement,
                    ComposerFullName = workItem.Composer.ComposerFullName,
                    PerformanceCount = count
                };

                work.WorkLink = string.Concat("Search.aspx?searchType=Performance&Composer=", work.ComposerFullName, "&Work=", workItem.WorkTitle.Replace("&", "%26"));

                works.Add(work);
            }
            
            return work;
        }
    }
}
