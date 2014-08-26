using Bso.Archive.BusObj.Interface;
using System.Collections.Generic;
using System.Linq;
using Bso.Archive.BusObj.Utility;
using System;

namespace Bso.Archive.BusObj
{
    partial class Work : IOPASData
    {
        #region IOPASData


        /// <summary>
        /// Updates the existing database Work on the column name using the 
        /// XML document parsed using the tagName.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="columnName"></param>
        /// <param name="tagName"></param>
        public void UpdateData(System.Xml.Linq.XDocument doc, string columnName, string tagName)
        {
            IEnumerable<System.Xml.Linq.XElement> eventElements = doc.Descendants(Constants.Event.eventElement);
            foreach (System.Xml.Linq.XElement element in eventElements)
            {
                //System.Xml.Linq.XElement workNode = element.Element(Constants.Work.workElement);
                var workNodes = element.Descendants(Constants.Work.workElement);
                foreach (var workNode in workNodes)
                {
                    Work updateWork = Work.GetWorkFromNode(workNode);

                    if (updateWork == null) continue;

                    object newValue = (string)workNode.GetXElement(tagName);

                    BsoArchiveEntities.UpdateObject(updateWork, newValue, columnName);
                }
            }
        }
        #endregion

        /// <summary>
        /// Add workArtists from XElement workItem and add to Work object
        /// </summary>
        /// <param name="node"></param>
        /// <param name="work"></param>
        /// <remarks>
        /// Gets the WorkArtist objects information from the XElement node and 
        /// gets the WorkArtist object. Then adds it to Work's WorkArtists collection.
        /// </remarks>
        /// <returns></returns>
        public static Work GetWorkArtists(System.Xml.Linq.XElement node, Work work)
        {
            IEnumerable<System.Xml.Linq.XElement> workArtistElements = node.Descendants("workArtist");
            foreach (System.Xml.Linq.XElement workArtist in workArtistElements)
            {
                WorkArtist artist = WorkArtist.GetWorkArtistFromNode(workArtist);

                if (artist == null) continue;

                Work.AddWorkArtist(work, artist);
            }
            return work;
        }

        /// <summary>
        /// Add WorkArtist object to WorkArtists in Work
        /// </summary>
        /// <param name="work"></param>
        /// <param name="artist"></param>
        /// <remarks>
        /// Checks to see if the Work object's collection of WorkArtists contains 
        /// the WorkArtist object passed. If yes then just return, otherwise Add.
        /// </remarks>
        internal static void AddWorkArtist(Work work, WorkArtist artist)
        {
            var wArtist = work.WorkArtists.FirstOrDefault(wa => wa.ArtistID == artist.ArtistID && wa.WorkID == work.WorkID);

            if (wArtist != null) return;

            work.WorkArtists.Add(artist);
        }

        /// <summary>
        /// Get a Work object from XElement node
        /// </summary>
        /// <remarks>
        /// Gets the workItem data from the XElement and checks if the 
        /// Work object already exists. If yes then return that work item. 
        /// Otherwise create a new Work object and assign its values the 
        /// data extracted from the XElement node.
        /// </remarks>
        /// <param name="node"></param>
        /// <returns></returns>
        public static Work GetWorkFromNode(System.Xml.Linq.XElement node)
        {
            if(node == null || node.Element(Constants.Work.workIDElement) == null)
                return null;

            int workID = 0;
            int.TryParse(node.GetXElement(Constants.Work.workIDElement), out workID);

            Work work = GetWorkByID(workID);

            if (!work.IsNew)
            {
                work.WorkPremiere = (string)node.GetXElement(Constants.Work.workPremiereElement);
                
                return work;
            }
                
            work.WorkID = workID;

            work = SetWorkTitles(node, work);

            work = GetComposers(node, work);

            work = SetWorkData(node, workID, work);

            work = GetWorkInstruments(node, work);

            work = GetWorkArtists(node, work);

            
            BsoArchiveEntities.Current.Save();
            
            return work;
        }



        /// <summary>
        /// Get the instrument variables from the XElement node.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="work"></param>
        /// <remarks>
        /// Extracts the values representing the instruments in the XElement node. Then 
        /// it calls SetWorkInstruments to assign them to the Work object.
        /// </remarks>
        /// <returns></returns>
        private static Work GetWorkInstruments(System.Xml.Linq.XElement node, Work work)
        {
            int flute, oboe, clarinet, bassoon, horn, trumpet, trombone, tuba, timpani,
                percussion, harp, keyboard, extra, violin1, violin2, viola, cello, bass;
            int.TryParse((string)node.GetXElement(Constants.Work.workFluteElement), out flute);
            int.TryParse((string)node.GetXElement(Constants.Work.workOboeElement), out oboe);
            int.TryParse((string)node.GetXElement(Constants.Work.workClarinetElement), out clarinet);
            int.TryParse((string)node.GetXElement(Constants.Work.workBassoonElement), out bassoon);
            int.TryParse((string)node.GetXElement(Constants.Work.workHornElement), out horn);
            int.TryParse((string)node.GetXElement(Constants.Work.workTrumpetElement), out trumpet);
            int.TryParse((string)node.GetXElement(Constants.Work.workTromboneElement), out trombone);
            int.TryParse((string)node.GetXElement(Constants.Work.workTubaElement), out tuba);
            int.TryParse((string)node.GetXElement(Constants.Work.workTimpaniElement), out timpani);
            int.TryParse((string)node.GetXElement(Constants.Work.workPercussionElement), out percussion);
            int.TryParse((string)node.GetXElement(Constants.Work.workHarpElement), out harp);
            int.TryParse((string)node.GetXElement(Constants.Work.workKeyboardElement), out keyboard);
            int.TryParse((string)node.GetXElement(Constants.Work.workExtraElement), out extra);
            int.TryParse((string)node.GetXElement(Constants.Work.workViolin1Element), out violin1);
            int.TryParse((string)node.GetXElement(Constants.Work.workViolin2Element), out violin2);
            int.TryParse((string)node.GetXElement(Constants.Work.workViolaElement), out viola);
            int.TryParse((string)node.GetXElement(Constants.Work.workCelloElement), out cello);
            int.TryParse((string)node.GetXElement(Constants.Work.workBassElement), out bass);
                    
            work = SetWorkInstruments(work, flute, oboe, clarinet, bassoon, horn, trumpet, trombone, tuba, timpani, percussion, harp, keyboard, extra, violin1, violin2, viola, bass);

            return work;
        }

        /// <summary>
        /// Sets the instrument variables to the Work object.
        /// </summary>
        /// <param name="work"></param>
        /// <param name="flute"></param>
        /// <param name="oboe"></param>
        /// <param name="clarinet"></param>
        /// <param name="bassoon"></param>
        /// <param name="horn"></param>
        /// <param name="trumpet"></param>
        /// <param name="trombone"></param>
        /// <param name="tuba"></param>
        /// <param name="timpani"></param>
        /// <param name="percussion"></param>
        /// <param name="harp"></param>
        /// <param name="keyboard"></param>
        /// <param name="extra"></param>
        /// <param name="violin1"></param>
        /// <param name="violin2"></param>
        /// <param name="viola"></param>
        /// <param name="bass"></param>
        /// <returns></returns>
        private static Work SetWorkInstruments(Work work, int flute, int oboe, int clarinet, int bassoon, int horn, int trumpet, int trombone, int tuba, int timpani, int percussion, int harp, int keyboard, int extra, int violin1, int violin2, int viola, int bass)
        {
            work.WorkFlute = flute;
            work.WorkOboe = oboe;
            work.WorkClarinet = clarinet;
            work.WorkBassoon = bassoon;
            work.WorkHorn = horn;
            work.WorkTrumpet = trumpet;
            work.WorkTrombone = trombone;
            work.WorkTuba = tuba;
            work.WorkTimpani = timpani;
            work.WorkPercussion = percussion;
            work.WorkHarp = harp;
            work.WorkKeyboard = keyboard;
            work.WorkExtra = extra;
            work.WorkViolin1 = violin1;
            work.WorkViolin2 = violin2;
            work.WorkViola = viola;
            work.WorkBass = bass;

            return work;
        }

        /// <summary>
        /// Assigns data from XElement node to Work object.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="workID"></param>
        /// <param name="work"></param>
        /// <remarks>
        /// Extracts information about the workItem from the XElement 
        /// node then assigns it to the Work object variables.
        /// </remarks>
        /// <returns></returns>
        private static Work SetWorkData(System.Xml.Linq.XElement node, int workID, Work work)
        {
            int workGroupID = 0;
            int.TryParse((string)node.GetXElement(Constants.Work.workGroupIDElement), out workGroupID);
            string workArrangement = (string)node.GetXElement(Constants.Work.workArrangementElement);
            string workCompYear = (string)node.GetXElement(Constants.Work.workCompYearElement);
            string workCompYear2 = (string)node.GetXElement(Constants.Work.workCompYear2Element);
            string workPremiere = (string)node.GetXElement(Constants.Work.workPremiereElement);
            string workRegular = (string)node.GetXElement(Constants.Work.workRegularElement);
            string workIntermission = (string)node.GetXElement(Constants.Work.workIntermissionElement);
            string workNote = node.GetXElement(Constants.Work.workNoteElement);
            string workCommission = node.GetXElement(Constants.Work.workCommissionElement);

            work.WorkGroupID = workGroupID;
            work.WorkNote = workNote;
            work.WorkArrangement = workArrangement;
            work.WorkCompYear = workCompYear;
            work.WorkCompYear2 = workCompYear;
            work.WorkPremiere = workPremiere;
            work.WorkCommission = workCommission;
            work.WorkRegular = workRegular;
            work.WorkIntermission = workIntermission;
            work.WorkID = workID;

            return work;
        }
      

        /// <summary>
        /// Sets the titles of the Work object
        /// </summary>
        /// <param name="node"></param>
        /// <param name="work"></param>
        /// <remarks>
        /// Extracts the 3 Titles from the XElement node and assigns them 
        /// to the Work object then returns that object.
        /// </remarks>
        /// <returns></returns>
        private static Work SetWorkTitles(System.Xml.Linq.XElement node, Work work)
        {
            string workTitle = (string)node.GetXElement(Constants.Work.workTitleElement);
            string workTitle2 = (string)node.GetXElement(Constants.Work.workTitle2Element);
            string workTitle3 = (string)node.GetXElement(Constants.Work.workTitle3Element);
            work.WorkTitle = workTitle;
            work.WorkTitle2 = workTitle2;
            work.WorkTitle3 = workTitle3;

            var workAddTitle = node.Element(Constants.Work.workAddTitleElement);
            if(workAddTitle != null)
                SetAddedSecondaryTitles(workAddTitle, work);

            return work;
        }

        /// <summary>
        /// Set added titles of the Work object
        /// </summary>
        /// <remarks>
        /// Adds the alternate titles that will be used with titles that have diacritics
        /// </remarks>
        /// <param name="node"></param>
        /// <param name="work"></param>
        private static void SetAddedSecondaryTitles(System.Xml.Linq.XElement node, Work work)
        {
            work.WorkAddTitle1 = (string)node.GetXElement(Constants.Work.workAddTitleTitle1Element);
            work.WorkAddTitle2 = (string)node.GetXElement(Constants.Work.workAddTitleTitle2Element);
            work.WorkAddTitle3 = (string)node.GetXElement(Constants.Work.workAddTitleTitle3Element);
            work.WorkAddTitleText = (string)node.GetXElement(Constants.Work.workAddTitleTextElement);
        }

        /// <summary>
        /// Check if Work object exists based on ID
        /// </summary>
        /// <param name="workID"></param>
        /// <remarks>
        /// Checks if Work object exists based off given ID. If yes then return 
        /// that object. Otherwise create a new Work object and return it.
        /// </remarks>
        /// <returns></returns>
        public static Work GetWorkByID(int workID)
        {
            Work work = BsoArchiveEntities.Current.Works.FirstOrDefault(w => w.WorkID == workID) ?? 
                Work.NewWork();

            return work;
        }

        private static Work GetComposers(System.Xml.Linq.XElement node, Work work)
        {
            var composerNode = node.Element(Constants.Composer.composerElement);
            Composer composer = Composer.GetComposerFromNode(composerNode);

            work.Composer = composer;

            return work;
        }

        public static bool WorkShouldBeExcludedByGroupId(int workGroupId)
        {
            string excludedGroupIds = SettingsHelper.ExcludedWorkGroupIds;

            var excludedGroupIdsList = excludedGroupIds.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(e => int.Parse(e.Trim())).ToList();

            if (excludedGroupIdsList.Contains(workGroupId))
                return true;

            return false;
        }

        public static bool WorkShouldBeExcludedById(int workId)
        {
            Work work = BsoArchiveEntities.Current.Works.FirstOrDefault(w => w.WorkID == workId);

            if (WorkShouldBeExcludedByGroupId(work.WorkGroupID))
                return true;

            return false;
        }
    }
}
