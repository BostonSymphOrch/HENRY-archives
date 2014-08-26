using System;
using Bso.Archive.BusObj;
using Bso.Archive.BusObj.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BSO.Archive.BusObj.Test.Editable
{
    [TestClass]
    public class ConductorTest
    {
        [Ignore]
        [TestMethod]
        public void UpdateConductorTest()
        {
            Conductor testConductor = Conductor.GetConductorByID(-1);
            if (testConductor.IsNew)
            {
                testConductor.ConductorID = -1;
            }
            testConductor.ConductorFirstName = "Adage";
            testConductor.ConductorLastName = "Tech";
            BsoArchiveEntities.Current.Save();

            var conductorID = Helper.CreateXElement(Constants.Conductor.conductorIDElement, "-1");
            var conductorFirstName = Helper.CreateXElement(Constants.Conductor.conductorFirstNameElement, "Test");
            var conductorItem = new System.Xml.Linq.XElement(Constants.Conductor.conductorElement, conductorID, conductorFirstName);
            var eventItem = new System.Xml.Linq.XElement(Constants.Event.eventElement, conductorItem);
            var doc = new System.Xml.Linq.XDocument(eventItem);

            Conductor conductor = Conductor.NewConductor();
            conductor.UpdateData(doc, "ConductorFirstName", "eventConductorFirstname");
            Assert.IsTrue(testConductor.ConductorFirstName == "Test");

            BsoArchiveEntities.Current.DeleteObject(testConductor);
            BsoArchiveEntities.Current.DeleteObject(conductor);
            BsoArchiveEntities.Current.Save();
        }

        /// <summary>
        /// Tests GetConductorFromNodeItem by creating an eventItem XElement element and passing it
        /// to the method. Checks that the object was created and with the correct values.
        /// </summary>
        [TestMethod]
        public void GetConductorFromNodeTest()
        {
            var conductorID = Helper.CreateXElement(Constants.Conductor.conductorIDElement, "-1");
            var conductorFirstName = Helper.CreateXElement(Constants.Conductor.conductorFirstNameElement, "TestFName");
            var conductorLastName = Helper.CreateXElement(Constants.Conductor.conductorLastNameElement, "TestLName");
            var conductorNote = Helper.CreateXElement(Constants.Conductor.conductorNoteElement, "TestNotes");
            var conductorName4 = Helper.CreateXElement(Constants.Conductor.conductorName4Element, "Test4Name");
            var conductorName5 = Helper.CreateXElement(Constants.Conductor.conductorName5Element, "Test5Name");
            var conductorItem = new System.Xml.Linq.XElement(Constants.Conductor.conductorElement, conductorID, conductorFirstName, conductorLastName, conductorNote, conductorName4, conductorName5);
            var node = new System.Xml.Linq.XElement(Constants.Event.eventElement, conductorItem);

            Conductor conductor = Conductor.GetConductorFromNode(node);
            Assert.IsNotNull(conductor);
            Assert.IsTrue(conductor.ConductorID == -1 && conductor.ConductorFirstName == "TestFName");
            Assert.IsTrue(conductor.ConductorName4 == "Test4Name");
            Assert.IsTrue(conductor.ConductorName5 == "Test5Name");
        }
    }
}