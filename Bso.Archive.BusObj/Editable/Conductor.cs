using Bso.Archive.BusObj.Interface;
using System.Collections.Generic;
using System.Linq;
using System;
using Bso.Archive.BusObj.Utility;

namespace Bso.Archive.BusObj
{
    partial class Conductor : IOPASData
    {
        #region IOPASData

        public void UpdateData(System.Xml.Linq.XDocument doc, string columnName, string tagName)
        {
            IEnumerable<System.Xml.Linq.XElement> eventElements = doc.Descendants(Constants.Event.eventElement);
            foreach (System.Xml.Linq.XElement element in eventElements)
            {
                Conductor updateConductor = Conductor.GetConductorFromNode(element);
                System.Xml.Linq.XElement conductorNode = element.Element(Constants.Conductor.conductorElement);
                if (conductorNode == null) continue;

                object newValue = conductorNode.GetXElement(tagName);
                BsoArchiveEntities.UpdateObject(updateConductor, newValue, columnName);
            }
        }

        #endregion
        /// <summary>
        /// Get Conductor object from XmlNode
        /// </summary>
        /// <param name="conductorItemNode"></param>
        /// <remarks>
        /// Takes an eventConductor child element from an eventItem and gets all its 
        /// child elements to create a Conductor object, then returns that object.
        /// </remarks>
        /// <returns></returns>
        public static Conductor GetConductorFromNode(System.Xml.Linq.XElement conductorItemNode)
        {
            System.Xml.Linq.XElement conductorElement = conductorItemNode.Element(Constants.Conductor.conductorElement);
            if (conductorElement == null || string.IsNullOrEmpty((string)conductorElement.GetXElement(Constants.Conductor.conductorIDElement)))
                return null;

            int conductorID;

            int.TryParse((string)conductorElement.GetXElement(Constants.Conductor.conductorIDElement), out conductorID);

            var conductor = Conductor.GetConductorByID(conductorID);
            if (!conductor.IsNew)
                return conductor;

            var conductorFirstName = conductorElement.GetXElement(Constants.Conductor.conductorFirstNameElement);
            var conductorLastName = conductorElement.GetXElement(Constants.Conductor.conductorLastNameElement);
            var conductorNote = conductorElement.GetXElement(Constants.Conductor.conductorNoteElement);
            var conductorName4 = conductorElement.GetXElement(Constants.Conductor.conductorName4Element);
            var conductorName5 = conductorElement.GetXElement(Constants.Conductor.conductorName5Element);
         
            conductor = SetConductorData(conductorID, conductor, conductorFirstName, conductorLastName, conductorName4, conductorName5, conductorNote);

            return conductor;
        }

        /// <summary>
        /// Sets the values of the Conductor object
        /// </summary>
        /// <remarks>
        /// Takes a conductor object along with variable and assigns those variabls to the Conductor object 
        /// </remarks>
        /// <param name="conductorID"></param>
        /// <param name="conductor"></param>
        /// <param name="conductorFirstName"></param>
        /// <param name="conductorLastName"></param>
        /// <param name="conductorNote"></param>
        /// <returns></returns>
        private static Conductor SetConductorData(int conductorID, Conductor conductor, string conductorFirstName, string conductorLastName, string conductorName4, string  conductorName5, string conductorNote)
        {
            conductor.ConductorID = conductorID;
            conductor.ConductorFirstName = conductorFirstName;
            conductor.ConductorLastName = conductorLastName;
            conductor.ConductorName4 = conductorName4;
            conductor.ConductorName5 = conductorName5;
            conductor.ConductorNote = conductorNote;
            return conductor;
        }

        internal static Conductor GetConductorByID(int conductorID)
        {
            var conductor =
                BsoArchiveEntities.Current.Conductors.FirstOrDefault(c => c.ConductorID == conductorID) ??
                Conductor.NewConductor();

            return conductor;
        }

        public string ConductorFullName
         { 
            get 
            {
                return string.Concat(ConductorFirstName, " ", ConductorLastName);
            } 
        }
    }
}
