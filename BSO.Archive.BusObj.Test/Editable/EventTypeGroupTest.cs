using Bso.Archive.BusObj;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BSO.Archive.BusObj.Test.Editable
{
    [TestClass]
    public class EventTypeGroupTest
    {
        [Ignore]
        [TestMethod]
        public void UpdateTypeGroupTest()
        {
            EventTypeGroup testTypeGroup = EventTypeGroup.GetTypeGroupByID(-1);
            if (testTypeGroup.IsNew)
            {
                testTypeGroup.TypeGroupID = -1;
            }
            testTypeGroup.EventTypeGroupName = "Adage";
            BsoArchiveEntities.Current.Save();

            System.Xml.Linq.XDocument doc = new System.Xml.Linq.XDocument(
            new System.Xml.Linq.XElement(new System.Xml.Linq.XElement("eventItem", new System.Xml.Linq.XElement("eventTypeGroup",
                new System.Xml.Linq.XElement("eventTypeGroupID", -1),
                new System.Xml.Linq.XElement("eventTypeGroupName", "Test")
                ))));

            EventTypeGroup typeGroup = EventTypeGroup.NewEventTypeGroup();
            typeGroup.UpdateData(doc, "EventTypeGroupName", "eventTypeGroupName");

            Assert.IsTrue(testTypeGroup.EventTypeGroupName == "Test");
            BsoArchiveEntities.Current.DeleteObject(testTypeGroup);
            BsoArchiveEntities.Current.DeleteObject(typeGroup);
            BsoArchiveEntities.Current.Save();
        }

        /// <summary>
        /// Tests GetTypeGroupFromNodeItem by creating an eventItem XElement element and passing it
        /// to the method. Checks that the object was created and with the correct values.
        /// </summary>
        [TestMethod]
        public void GetTypeGroupFromNodeItemTest()
        {
            System.Xml.Linq.XElement node = new System.Xml.Linq.XElement(Constants.Event.eventElement, new System.Xml.Linq.XElement(Constants.EventTypeGroup.typeGroupElement,
                new System.Xml.Linq.XElement(Constants.EventTypeGroup.typeGroupIDElement, "-1"),
                new System.Xml.Linq.XElement(Constants.EventTypeGroup.typeGroupNameElement, "TestTypeGroupName"),
                new System.Xml.Linq.XElement(Constants.EventTypeGroup.typeGroupName2Element, "TestTypeGroupName2")));

            EventTypeGroup typeGroup = EventTypeGroup.NewEventTypeGroup();
            typeGroup = EventTypeGroup.GetEventTypeGroupFromNode(node);

            Assert.IsNotNull(typeGroup);
            Assert.IsTrue(typeGroup.TypeGroupID == -1);
        }
    }
}