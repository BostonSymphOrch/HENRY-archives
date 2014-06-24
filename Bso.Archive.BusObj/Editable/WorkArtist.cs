using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Bso.Archive.BusObj.Interface;
using Bso.Archive.BusObj.Utility;

namespace Bso.Archive.BusObj
{
    partial class WorkArtist : IOPASData
    {
        #region IOPASData

        /// <summary>
        /// Updates the existing database WorkArtist on the column name using the 
        /// XML document parsed using the tagName.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="columnName"></param>
        /// <param name="tagName"></param>
        public void UpdateData(System.Xml.Linq.XDocument doc, string columnName, string tagName)
        {

            IEnumerable<System.Xml.Linq.XElement> eventElements = doc.Descendants(Constants.Event.eventElement);
            foreach (System.Xml.Linq.XElement eventElement in eventElements)
            {
                var workElements = eventElement.Descendants(Constants.Work.workElement);
                foreach (var workElement in workElements)
                {
                    Work workItem = Work.GetWorkFromNode(workElement);

                    IEnumerable<System.Xml.Linq.XElement> workArtistElements = workElement.Descendants(Constants.WorkArtist.workArtistElement);
                    foreach (var workArtistElement in workArtistElements)
                    {
                        int artistID = 0;
                        int.TryParse((string)workArtistElement.GetXElement(Constants.WorkArtist.workArtistIDElement), out artistID);

                        WorkArtist updateWorkArtist = WorkArtist.GetWorkArtistByID(artistID, workItem.WorkID);

                        updateWorkArtist = WorkArtist.BuildWorkArtist(workArtistElement, artistID, updateWorkArtist);

                        if (updateWorkArtist == null) continue;

                        object newValue = (string)workArtistElement.GetXElement(tagName);

                        BsoArchiveEntities.UpdateObject(updateWorkArtist, newValue, columnName);

                        BsoArchiveEntities.UpdateObject(updateWorkArtist.Artist, newValue, columnName);

                        BsoArchiveEntities.UpdateObject(updateWorkArtist.Instrument, newValue, columnName);

                        BsoArchiveEntities.Current.Save();
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// Returns a WorkArtist object from a workItem XElement node
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// Takes a XElement node and extracts the WorkArtist information 
        /// and creates a new WorkArtist object and returns it.
        /// </remarks>
        /// <returns></returns>
        public static WorkArtist GetWorkArtistFromNode(System.Xml.Linq.XElement node)
        {
            if (node == null || node.Element(Constants.WorkArtist.workArtistIDElement) == null)
                return null;
            
            int workArtistID = 0;
            int.TryParse((string)node.GetXElement(Constants.WorkArtist.workArtistIDElement), out workArtistID);

            WorkArtist workArtist = WorkArtist.NewWorkArtist();

            return BuildWorkArtist(node, workArtistID, workArtist);
        }

        private static WorkArtist BuildWorkArtist(System.Xml.Linq.XElement node, int workArtistID, WorkArtist workArtist)
        {
            Artist artist = Artist.GetArtistByID(workArtistID);
            if (artist.IsNew)
            {
                artist.ArtistID = workArtistID;
                artist.ArtistLastName = (string)node.GetXElement(Constants.WorkArtist.workArtistLastNameElement);
                artist.ArtistFirstName = (string)node.GetXElement(Constants.WorkArtist.workArtistFirstNameElement);
                artist.ArtistName4 = (string)node.GetXElement(Constants.WorkArtist.workArtistName4Element);
                artist.ArtistName5 = (string)node.GetXElement(Constants.WorkArtist.workArtistName5Element);
            }
            workArtist.Artist = artist;

            int workArtistStatus, workArtistStatusID, instrumentID;

            int.TryParse((string)node.GetXElement(Constants.WorkArtist.workArtistInstrumentIDElement), out instrumentID);
            int.TryParse((string)node.GetXElement(Constants.WorkArtist.workArtistStatusElement), out workArtistStatus);
            int.TryParse((string)node.GetXElement(Constants.WorkArtist.workArtistStatusIDElement), out workArtistStatusID);
            int.TryParse((string)node.GetXElement(Constants.WorkArtist.workArtistStatusIDElement), out workArtistStatusID);
            string workArtistNote = (string)node.GetXElement(Constants.WorkArtist.workArtistNoteElement);

            workArtist = SetWorkArtistData(workArtist, workArtistNote, workArtistStatus, workArtistStatusID);

            string workArtistInstrument = (string)node.GetXElement(Constants.WorkArtist.workArtistInstrumentElement);
            string workArtistInstrument2 = (string)node.GetXElement(Constants.WorkArtist.workArtistInstrument2Element);
           
            CreateWorkArtistInstrument(workArtist, instrumentID, workArtistInstrument, workArtistInstrument2);

            return workArtist;
        }

        private static void CreateWorkArtistInstrument(WorkArtist workArtist, int instrumentID, string workArtistInstrument, string workArtistInstrument2)
        {
            if (String.IsNullOrEmpty(workArtistInstrument) && String.IsNullOrEmpty(workArtistInstrument2))
                return;

            Instrument instrument = Instrument.GetInstrumentByNames(workArtistInstrument, workArtistInstrument2);

            if (instrument.IsNew)
            {
                instrument.Instrument1 = workArtistInstrument;
                instrument.Instrument2 = workArtistInstrument2;
                instrument.InstrumentID = instrumentID;
            }

            workArtist.Instrument = instrument;
        }

        /// <summary>
        /// Sets the data passed to the WorkArtist object
        /// </summary>
        /// <param name="workArtistID"></param>
        /// <param name="workArtist"></param>
        /// <param name="workArtistNote"></param>
        /// <param name="workArtistStatus"></param>
        /// <param name="workArtistStatusID"></param>
        /// <returns></returns>
        private static WorkArtist SetWorkArtistData(WorkArtist workArtist, string workArtistNote, int workArtistStatus, int workArtistStatusID)
        {
            workArtist.WorkArtistStatus = workArtistStatus;
            workArtist.WorkArtistStatusID = workArtistStatusID;
            workArtist.WorkArtistNote = workArtistNote;

            return workArtist;
        }

        /// <summary>
        /// Check if WorkArtist object already exists with WorkArtists.
        /// </summary>
        /// <param name="workArtistID"></param>
        /// <returns></returns>
        internal static WorkArtist GetWorkArtistByID(int artistID, int workID)
        {
            WorkArtist workArtist = BsoArchiveEntities.Current.WorkArtists.FirstOrDefault(wa => wa.ArtistID == artistID && wa.WorkID == workID) ??
                WorkArtist.NewWorkArtist();

            return workArtist;
        }
    }
}
