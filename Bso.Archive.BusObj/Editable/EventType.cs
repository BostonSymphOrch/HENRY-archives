using Bso.Archive.BusObj.Interface;
using System.Collections.Generic;
using System.Linq;
using Bso.Archive.BusObj.Utility;

namespace Bso.Archive.BusObj
{
    partial class EventType : IOPASData
    {
        #region IOPASData
        /// <summary>
        /// Updates the existing database Type on the column name using the 
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
                EventType updateType = EventType.GetEventTypeFromNode(element);

                if (updateType == null) continue;

                System.Xml.Linq.XElement typeNode = element.Element(Constants.EventType.typeElement);

                object newValue = typeNode.GetXElement(tagName);

                BsoArchiveEntities.UpdateObject(updateType, newValue, columnName);
            }
        }

        #endregion
        /// <summary>
        /// Get Type object from XElement eventItem node
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// Takes an XElement node which represents an eventItem 
        /// and takes the Type data from it and checks to see if 
        /// the Type object exists. If yes then returns it. Otherwise
        /// create a new Type object.
        /// </remarks>
        /// <returns></returns>
        public static EventType GetEventTypeFromNode(System.Xml.Linq.XElement node)
        {
            System.Xml.Linq.XElement typeElement = node.Element(Constants.EventType.typeElement);
            if (typeElement == null || string.IsNullOrEmpty((string)typeElement.GetXElement(Constants.EventType.typeIDElement)))
                return null;

            int typeID;
            int.TryParse(typeElement.GetXElement(Constants.EventType.typeIDElement), out typeID);
            EventType type = GetEventTypeByID(typeID);
            if (!type.IsNew)
                return type;

            string typeName = typeElement.GetXElement(Constants.EventType.typeNameElement);
            string typeName2 = typeElement.GetXElement(Constants.EventType.typeNameElement);

            type = SetEventTypeData(typeID, type, typeName, typeName2);

            return type;
        }

        /// <summary>
        /// Set Data for Type object
        /// </summary>
        /// <param name="typeID"></param>
        /// <param name="type"></param>
        /// <param name="typeName"></param>
        /// <param name="typeName2"></param>
        /// <remarks>
        /// Takes a Type object along with variables and assigns those variables to 
        /// the Type objects fields.
        /// </remarks>
        /// <returns></returns>
        private static EventType SetEventTypeData(int typeID, EventType type, string typeName, string typeName2)
        {
            type.TypeID = typeID;
            type.TypeName = typeName;
            type.TypeName2 = typeName2;

            return type;
        }

        /// <summary>
        /// Takes an id and checks to see if a Type object already 
        /// exists.
        /// </summary>
        /// <param name="typeID"></param>
        /// <remarks>
        /// Takes an id and checks to see if a Type object already 
        /// exists with the given id. If yes then return it. Otherwise,
        /// create a new Type object to return.
        /// </remarks>
        /// <returns></returns>
        internal static EventType GetEventTypeByID(int typeID)
        {
            EventType type = BsoArchiveEntities.Current.EventTypes.FirstOrDefault(t => t.TypeID == typeID) ??
                EventType.NewEventType();

            return type;
        }
    }
}
