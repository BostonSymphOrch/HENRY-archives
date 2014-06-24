using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bso.Archive.BusObj;
using Bso.Archive.BusObj.Utility;

namespace BSO.Archive.BusObj.Test.Editable
{
    [TestClass]
    public class ConductorTest
    {
        [TestMethod]
        public void UpdateConductorTest()
        {
            Conductor testConductor = Conductor.GetConductorByID(830);
            Assert.IsTrue(testConductor.ConductorFirstName == "Joel");

            var conductorID = Helper.CreateXElement(Constants.Conductor.conductorIDElement, "830");
            var conductorFirstName = Helper.CreateXElement(Constants.Conductor.conductorFirstNameElement, "TestFName");
            var conductorItem = new System.Xml.Linq.XElement(Constants.Conductor.conductorElement, conductorID, conductorFirstName);
            var eventItem = new System.Xml.Linq.XElement(Constants.Event.eventElement, conductorItem);
            var doc = new System.Xml.Linq.XDocument(eventItem);

            Conductor conductor = Conductor.NewConductor();
            conductor.UpdateData(doc, "ConductorFirstName", "eventConductorFirstname");
            Assert.IsTrue(testConductor.ConductorFirstName == "TestFName");
        }

        /// <summary>
        /// Tests GetConductorFromNodeItem by creating an eventItem XElement element and passing it 
        /// to the method. Checks that the object was created and with the correct values. 
        /// </summary>
        [TestMethod]
        public void GetConductorFromNodeTest()
        {
            var conductorID = Helper.CreateXElement(Constants.Conductor.conductorIDElement, "0");
            var conductorFirstName = Helper.CreateXElement(Constants.Conductor.conductorFirstNameElement, "TestFName");
            var conductorLastName = Helper.CreateXElement(Constants.Conductor.conductorLastNameElement, "TestLName");
            var conductorNote = Helper.CreateXElement(Constants.Conductor.conductorNoteElement, "TestNotes");
            var conductorName4 = Helper.CreateXElement(Constants.Conductor.conductorName4Element, "Test4Name");
            var conductorName5 = Helper.CreateXElement(Constants.Conductor.conductorName5Element, "Test5Name");
            var conductorItem = new System.Xml.Linq.XElement(Constants.Conductor.conductorElement, conductorID, conductorFirstName, conductorLastName, conductorNote, conductorName4, conductorName5);
            var node = new System.Xml.Linq.XElement(Constants.Event.eventElement, conductorItem);


            Conductor conductor = Conductor.GetConductorFromNode(node);
            Assert.IsNotNull(conductor);
            Assert.IsTrue(conductor.ConductorID == 0 && conductor.ConductorFirstName == "TestFName");
            Assert.IsTrue(conductor.ConductorName4 == "Test4Name");
            Assert.IsTrue(conductor.ConductorName5 == "Test5Name");
        }

        /// <summary>
        /// Tests the GetConductorByID method
        /// </summary>
        [TestMethod]
        public void GetConductorByIDTest()
        {
            Conductor conductor1 = Conductor.GetConductorByID(1);
            if(conductor1.IsNew)
                conductor1.ConductorID = 1;

            BsoArchiveEntities.Current.Save();
            Conductor conductor2 = Conductor.GetConductorByID(1);
            Assert.IsNotNull(conductor2);
            Assert.IsTrue(conductor1.Equals(conductor2));
        }

    }
}
