using Bso.Archive.BusObj.Interface;
using System.Collections.Generic;
using System.Linq;
using Bso.Archive.BusObj.Utility;

namespace Bso.Archive.BusObj
{
    partial class EventTypeGroup : IOPASData
    {
        #region IOPASData
        /// <summary>
        /// Updates the existing database TypeGroup on the column name using the 
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
                EventTypeGroup updateTypeGroup = EventTypeGroup.GetEventTypeGroupFromNode(element);

                if (updateTypeGroup == null) continue;

                System.Xml.Linq.XElement typeGroupNode = element.Element(Constants.EventTypeGroup.typeGroupElement);

                object newValue = typeGroupNode.GetXElement(tagName);

                BsoArchiveEntities.UpdateObject(updateTypeGroup, newValue, columnName);
            }
        }
        #endregion

        /// <summary>
        /// Get TypeGroup object based on an XElement node
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// Takes a eventItem node and extracts the TypeGroup information 
        /// for that event. It then returns either an existing TypeGroup if one 
        /// exists with the same id, or creates a new one to return.
        /// </remarks>
        /// <returns></returns>
        public static EventTypeGroup GetEventTypeGroupFromNode(System.Xml.Linq.XElement node)
        {
            System.Xml.Linq.XElement typeGroupElement = node.Element(Constants.EventTypeGroup.typeGroupElement);
            if (typeGroupElement == null || string.IsNullOrEmpty((string)typeGroupElement.GetXElement(Constants.EventTypeGroup.typeGroupIDElement)))
                return null;

            int typeGroupID;
            int.TryParse(typeGroupElement.GetXElement(Constants.EventTypeGroup.typeGroupIDElement), out typeGroupID);
            if (typeGroupID == 0)
                typeGroupID = 1000;

            EventTypeGroup typeGroup = GetTypeGroupByID(typeGroupID);
            if (!typeGroup.IsNew)
                return typeGroup;

            string typeGroupName = typeGroupElement.GetXElement(Constants.EventTypeGroup.typeGroupNameElement);
            string typeGroupName2 = typeGroupElement.GetXElement(Constants.EventTypeGroup.typeGroupName2Element);

            typeGroup = SetTypeGroupData(typeGroupID, typeGroup, typeGroupName, typeGroupName2);

            return typeGroup;
        }

        /// <summary>
        /// Set variables for TypeGroup object
        /// </summary>
        /// <param name="typeGroupID"></param>
        /// <param name="typeGroup"></param>
        /// <param name="typeGroupName"></param>
        /// <param name="typeGroupName2"></param>
        /// <remarks>
        /// Takes a TypeGroup object along with variables and assigns the
        /// given variables to the parameters of the TypeGroup object.
        /// </remarks>
        /// <returns></returns>
        private static EventTypeGroup SetTypeGroupData(int typeGroupID, EventTypeGroup typeGroup, string typeGroupName, string typeGroupName2)
        {
            typeGroup.TypeGroupID = typeGroupID;
            typeGroup.EventTypeGroupName = typeGroupName;
            typeGroup.EventTypeGroupName2 = typeGroupName2;

            return typeGroup;
        }

        /// <summary>
        /// Get a TypeGroup object based off given id.
        /// </summary>
        /// <param name="typeGroupID"></param>
        /// <remarks>
        /// Takes a TypeGroup id and checks to see if it already exists. If so return it.
        /// Otherwise create a new TypeGroup object and return it.
        /// </remarks>
        /// <returns></returns>
        internal static EventTypeGroup GetTypeGroupByID(int typeGroupID)
        {
            EventTypeGroup typeGroup = BsoArchiveEntities.Current.EventTypeGroups.FirstOrDefault(t => t.TypeGroupID == typeGroupID) ??
                EventTypeGroup.NewEventTypeGroup();

            return typeGroup;
        }

    }
}