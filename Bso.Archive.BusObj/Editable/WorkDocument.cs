using Bso.Archive.BusObj.Interface;
using Bso.Archive.BusObj.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Bso.Archive.BusObj
{
    partial class WorkDocument : IOPASData
    {
        public void UpdateData(XDocument doc, string columnName, string tagName)
        {
            IEnumerable<System.Xml.Linq.XElement> eventElements = doc.Descendants(Constants.Event.eventElement);
            foreach (System.Xml.Linq.XElement eventElement in eventElements)
            {
                var workElements = eventElement.Descendants(Constants.Work.workElement);
                foreach (var workElement in workElements)
                {
                    Work workItem = Work.GetWorkFromNode(workElement);

                    IEnumerable<System.Xml.Linq.XElement> workDocumentElements = workElement.Descendants(Constants.WorkDocument.workDocumentElement);
                    foreach (var workDocumentElement in workDocumentElements)
                    {
                        int documentID = 0;
                        int.TryParse((string)workDocumentElement.GetXElement(Constants.WorkArtist.workArtistIDElement), out documentID);

                        WorkDocument updateWorkDocument = WorkDocument.GetDocumentByID(documentID, workItem.WorkID);

                        updateWorkDocument = WorkDocument.BuildWorkDocument(workDocumentElement, documentID, updateWorkDocument);

                        if (updateWorkDocument == null) continue;

                        object newValue = (string)workDocumentElement.GetXElement(tagName);

                        BsoArchiveEntities.UpdateObject(updateWorkDocument, newValue, columnName);

                        BsoArchiveEntities.Current.Save();
                    }
                }
            }
        }

        public static WorkDocument GetWorkDocumentFromNode(System.Xml.Linq.XElement node)
        {
            if (node == null || node.Element(Constants.WorkDocument.workDocumentIDElement) == null)
                return null;

            int workDocumentID = 0;
            int.TryParse((string)node.GetXElement(Constants.WorkDocument.workDocumentIDElement), out workDocumentID);

            WorkDocument workDocument = WorkDocument.NewWorkDocument();

            return BuildWorkDocument(node, workDocumentID, workDocument);
        }

        private static WorkDocument BuildWorkDocument(System.Xml.Linq.XElement node, int workDocumentID, WorkDocument workDocument)
        {
            WorkDocument doc = WorkDocument.NewWorkDocument();
            doc.WorkDocumentID = workDocumentID;
            doc.WorkDocumentName = (string)node.GetXElement(Constants.WorkDocument.workDocumentNameElement);
            doc.WorkDocumentNotes = (string)node.GetXElement(Constants.WorkDocument.workDocumentNotesElement);
            doc.WorkDocumentSummary = (string)node.GetXElement(Constants.WorkDocument.workDocumentSummaryElement);
            doc.WorkDocumentFileLocation = (string)node.GetXElement(Constants.WorkDocument.workDocumentFileLocationElement);
            return doc;
        }

        internal static WorkDocument GetDocumentByID(int documentID, int workID)
        {
            WorkDocument workDocument = BsoArchiveEntities.Current.WorkDocuments.FirstOrDefault(wa => wa.WorkDocumentID == documentID) ??
                WorkDocument.NewWorkDocument();

            return workDocument;
        }
        public static WorkDocument GetDocumentByID(int documentID)
        {
            WorkDocument workDocument = BsoArchiveEntities.Current.WorkDocuments.FirstOrDefault(wa => wa.WorkDocumentID == documentID) ??
                WorkDocument.NewWorkDocument();

            return workDocument;
        }
    }
}
