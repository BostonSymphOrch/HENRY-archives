using Bso.Archive.BusObj;
using Bso.Archive.BusObj.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BSO.Archive.BusObj.Test.Editable
{
    [TestClass]
    public class EventTypeTest
    {
        [Ignore]
        [TestMethod]
        public void UpdateTypeTest()
        {
            Bso.Archive.BusObj.EventType testType = Bso.Archive.BusObj.EventType.GetEventTypeByID(-1);
            if (testType.IsNew)
            {
                testType.TypeID = -1;
            }
            testType.TypeName = "Adage";
            BsoArchiveEntities.Current.Save();

            var typeID = Helper.CreateXElement(Constants.EventType.typeIDElement, "-1");
            var typeName2 = Helper.CreateXElement(Constants.EventType.typeName2Element, "Test");

            var typeItem = new System.Xml.Linq.XElement(Constants.EventType.typeElement, typeID, typeName2);
            var eventItem = new System.Xml.Linq.XElement(Constants.Event.eventElement, typeItem);
            var doc = new System.Xml.Linq.XDocument(eventItem);

            Bso.Archive.BusObj.EventType typeGroup = Bso.Archive.BusObj.EventType.NewEventType();

            typeGroup.UpdateData(doc, "TypeName2", "eventTypeName2");
            Assert.IsTrue(testType.TypeName2 == "Test");

            BsoArchiveEntities.Current.DeleteObject(testType);
            BsoArchiveEntities.Current.DeleteObject(typeGroup);
            BsoArchiveEntities.Current.Save();
        }

        /// <summary>
        /// Tests GetTypeFromNodeItem by creating an eventItem XElement element and passing it
        /// to the method. Checks that the object was created and with the correct values.
        /// </summary>
        [TestMethod]
        public void GetTypeFromNodeTest()
        {
            var typeID = Helper.CreateXElement(Constants.EventType.typeIDElement, "-1");
            var typeName = Helper.CreateXElement(Constants.EventType.typeNameElement, "Test EventType Name");
            var typeName2 = Helper.CreateXElement(Constants.EventType.typeName2Element, "Test EventType Name 2");
            var typeItem = new System.Xml.Linq.XElement(Constants.EventType.typeElement, typeID, typeName, typeName2);
            var node = new System.Xml.Linq.XElement(Constants.Event.eventElement, typeItem);

            Bso.Archive.BusObj.EventType EventType = Bso.Archive.BusObj.EventType.NewEventType();
            EventType = Bso.Archive.BusObj.EventType.GetEventTypeFromNode(node);

            Assert.IsNotNull(EventType);
            Assert.IsTrue(EventType.TypeID == -1);
        }
    }
}