using Bso.Archive.BusObj;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BSO.Archive.BusObj.Test.Editable
{
    [TestClass]
    public class EventTypeGroupTest
    {
        [TestMethod]
        public void UpdateTypeGroupTest()
        {
            EventTypeGroup testTypeGroup = EventTypeGroup.GetTypeGroupByID(2);
            Assert.IsTrue(testTypeGroup.EventTypeGroupName == "Performance");
            System.Xml.Linq.XDocument doc = new System.Xml.Linq.XDocument(
            new System.Xml.Linq.XElement(new System.Xml.Linq.XElement("eventItem", new System.Xml.Linq.XElement("eventTypeGroup",
                new System.Xml.Linq.XElement("eventTypeGroupID", 2),
                new System.Xml.Linq.XElement("eventTypeGroupName", "ADAGE Test Name")
                ))));
            EventTypeGroup typeGroup = EventTypeGroup.NewEventTypeGroup();
            typeGroup.UpdateData(doc, "EventTypeGroupName", "eventTypeGroupName");
            Assert.IsTrue(testTypeGroup.EventTypeGroupName == "ADAGE Test Name");
        }

        /// <summary>
        /// Tests GetTypeGroupFromNodeItem by creating an eventItem XElement element and passing it 
        /// to the method. Checks that the object was created and with the correct values.
        /// </summary>
        [TestMethod]
        public void GetTypeGroupFromNodeItemTest()
        {
            System.Xml.Linq.XElement node = new System.Xml.Linq.XElement("eventItem", new System.Xml.Linq.XElement("eventTypeGroup",
                new System.Xml.Linq.XElement("eventTypeGroupID", "1"),
                new System.Xml.Linq.XElement("eventTypeGroupName", "TestTypeGroupName"),
                new System.Xml.Linq.XElement("eventTypeGroupName2", "TestTypeGroupName2")));

            EventTypeGroup typeGroup = EventTypeGroup.NewEventTypeGroup();
            typeGroup = EventTypeGroup.GetEventTypeGroupFromNode(node);

            Assert.IsNotNull(typeGroup);
            Assert.IsTrue(typeGroup.TypeGroupID == 1);
        }

        /// <summary>
        /// Tests the GetTypeGroupByID method.
        /// </summary>
        [TestMethod]
        public void GetTypeGroupByIDTest()
        {
            EventTypeGroup typeGroup1 = EventTypeGroup.GetTypeGroupByID(1);
            if (typeGroup1.IsNew)
                typeGroup1.TypeGroupID = 1;

            BsoArchiveEntities.Current.Save();

            EventTypeGroup typeGroup2 = EventTypeGroup.GetTypeGroupByID(1);
            Assert.IsTrue(typeGroup1.Equals(typeGroup2));
        }

    }
}
