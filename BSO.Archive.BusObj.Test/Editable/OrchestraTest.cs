using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bso.Archive.BusObj;
using Bso.Archive.BusObj.Utility;

namespace BSO.Archive.BusObj.Test.Editable
{
    [TestClass]
    public class OrchestraTest
    {
        [TestMethod]
        public void UpdateOrchestraTest()
        {
            Orchestra testOrchestra = Orchestra.GetOrchestraByID(2196);
            Assert.IsTrue(testOrchestra.OrchestraNote == "");

            var orchestraID = Helper.CreateXElement(Constants.Orchestra.orchestraIDElement, "2196");
            var orchestraNotes = Helper.CreateXElement(Constants.Orchestra.orchestraNotesElement, "Test Note ADAGE");
            var orchestraItem = new System.Xml.Linq.XElement(Constants.Orchestra.orchestraElement, orchestraID, orchestraNotes);
            var eventItem = new System.Xml.Linq.XElement(Constants.Event.eventElement, orchestraItem);
            var doc = new System.Xml.Linq.XDocument(eventItem);

            Orchestra orchestra = Orchestra.NewOrchestra();
            orchestra.UpdateData(doc, "OrchestraNote", "eventOrchestraNotes");
            Assert.IsTrue(testOrchestra.OrchestraNote == "Test Note ADAGE");
        }

        /// <summary>
        /// Tests GetProjectFromNodeItem by creating an eventItem XElement element and passing it 
        /// to the method. Checks that the object was created and with the correct values.
        /// </summary>
        [TestMethod]
        public void GetOrchestraFromNodeItemTest()
        {
            var orchestraID = Helper.CreateXElement(Constants.Orchestra.orchestraIDElement, "2");
            var orchestraName = Helper.CreateXElement(Constants.Orchestra.orchestraNameElement, "TestOrchestraName");
            var orchestraURL = Helper.CreateXElement(Constants.Orchestra.orchestraURLElement, "TestOrchestraURL");
            var orchestraNote = Helper.CreateXElement(Constants.Orchestra.orchestraNotesElement, "TestOrchestraNotes");
            var orchestraItem = new System.Xml.Linq.XElement(Constants.Orchestra.orchestraElement, orchestraID, orchestraName, orchestraURL, orchestraNote);
            var node = new System.Xml.Linq.XElement(Constants.Event.eventElement, orchestraItem);

            Orchestra orchestra = Orchestra.GetOrchestraFromNode(node);
            Assert.IsNotNull(orchestra);
            Assert.IsTrue(orchestra.OrchestraID == 2 && orchestra.OrchestraName == "TestOrchestraName");
        }

        /// <summary>
        /// Test GetOrchestraByID method
        /// </summary>
        [TestMethod]
        public void GetOrchestraByIDTest()
        {
            Orchestra orchestra1 = Orchestra.GetOrchestraByID(1);
            if(orchestra1.IsNew)
                orchestra1.OrchestraID = 1;

            BsoArchiveEntities.Current.Save();
            Orchestra orchestra2 = Orchestra.GetOrchestraByID(1);
            Assert.IsNotNull(orchestra2);
            Assert.IsTrue(orchestra1.Equals(orchestra2));
        }

    }
}
