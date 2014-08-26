using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Bso.Archive.BusObj.Interface;
using Bso.Archive.BusObj.Utility;

namespace Bso.Archive.BusObj
{
    partial class Orchestra : IOPASData
    {
        #region IOPASData

        /// <summary>
        /// Updates the existing database Orchestra on the column name using the 
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
                Orchestra updateOrchestra = Orchestra.GetOrchestraFromNode(element);

                if (updateOrchestra == null) continue;

                System.Xml.Linq.XElement orchestraNode = element.Element(Constants.Orchestra.orchestraElement);

                object newValue = orchestraNode.GetXElement(tagName);

                BsoArchiveEntities.UpdateObject(updateOrchestra, newValue, columnName);
            }
        }
        #endregion

        /// <summary>
        /// Returns a Orchestra object based on the eventOrchestra data passed by the
        /// XElement eventItem element. 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static Orchestra GetOrchestraFromNode(System.Xml.Linq.XElement node)
        {
            System.Xml.Linq.XElement orchestraElement = node.Element(Constants.Orchestra.orchestraElement);
            if (orchestraElement == null || string.IsNullOrEmpty(orchestraElement.GetXElement(Constants.Orchestra.orchestraIDElement)))
                return null;

            int orchestraID;
            int.TryParse(orchestraElement.GetXElement(Constants.EventRoot + Constants.Orchestra.OrchestraID), out orchestraID);

            Orchestra orchestra = Orchestra.GetOrchestraByID(orchestraID);
            if (!orchestra.IsNew)
                return orchestra;

            string orchestraName = orchestraElement.GetXElement(Constants.Orchestra.orchestraNameElement); 
            string orchestraURL = orchestraElement.GetXElement(Constants.Orchestra.orchestraURLElement);
            string orchestraNote = orchestraElement.GetXElement(Constants.Orchestra.orchestraNotesElement);

            orchestra = SetOrchestraData(orchestraID, orchestra, orchestraName, orchestraURL, orchestraNote);

            return orchestra;
        }

        private static Orchestra SetOrchestraData(int orchestraID, Orchestra orchestra, string orchestraName, string orchestraURL, string orchestraNote)
        {
            orchestra.OrchestraID = orchestraID;
            orchestra.OrchestraName = orchestraName;
            orchestra.OrchestraURL = orchestraURL;
            orchestra.OrchestraNote = orchestraNote;

            return orchestra;
        }

        /// <summary>
        /// Checks existing Orchestra based upon the given ID. If the Orchestra 
        /// already exists then return it. Otherwise create a new Orchestra 
        /// and return it.
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        internal static Orchestra GetOrchestraByID(int orchestraID)
        {
            Orchestra orchestra = BsoArchiveEntities.Current.Orchestras.FirstOrDefault(o => o.OrchestraID == orchestraID)
                ?? Orchestra.NewOrchestra();

            return orchestra;
        }

    }
}
