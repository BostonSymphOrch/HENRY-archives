using System;
using Bso.Archive.BusObj;
using Bso.Archive.BusObj.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BSO.Archive.BusObj.Test.Editable
{
    [TestClass]
    public class OrchestraTest
    {
        [Ignore]
        [TestMethod]
        public void UpdateOrchestraTest()
        {
            Orchestra testOrchestra = Orchestra.GetOrchestraByID(-1);
            if (testOrchestra.IsNew)
            {
                testOrchestra.OrchestraID = -1;
            }
            testOrchestra.OrchestraName = "Adage";
            BsoArchiveEntities.Current.Save();

            var orchestraID = Helper.CreateXElement(Constants.Orchestra.orchestraIDElement, "-1");
            var orchestraNotes = Helper.CreateXElement(Constants.Orchestra.orchestraNotesElement, "Test");
            var orchestraItem = new System.Xml.Linq.XElement(Constants.Orchestra.orchestraElement, orchestraID, orchestraNotes);
            var eventItem = new System.Xml.Linq.XElement(Constants.Event.eventElement, orchestraItem);
            var doc = new System.Xml.Linq.XDocument(eventItem);

            Orchestra orchestra = Orchestra.NewOrchestra();
            orchestra.UpdateData(doc, "OrchestraNote", "eventOrchestraNotes");
            Assert.IsTrue(testOrchestra.OrchestraNote == "Test");

            BsoArchiveEntities.Current.DeleteObject(testOrchestra);
            BsoArchiveEntities.Current.DeleteObject(orchestra);
            BsoArchiveEntities.Current.Save();
        }

        /// <summary>
        /// Tests GetProjectFromNodeItem by creating an eventItem XElement element and passing it
        /// to the method. Checks that the object was created and with the correct values.
        /// </summary>
        [TestMethod]
        public void GetOrchestraFromNodeItemTest()
        {
            var orchestraID = Helper.CreateXElement(Constants.Orchestra.orchestraIDElement, "-1");
            var orchestraName = Helper.CreateXElement(Constants.Orchestra.orchestraNameElement, "TestOrchestraName");
            var orchestraURL = Helper.CreateXElement(Constants.Orchestra.orchestraURLElement, "TestOrchestraURL");
            var orchestraNote = Helper.CreateXElement(Constants.Orchestra.orchestraNotesElement, "TestOrchestraNotes");
            var orchestraItem = new System.Xml.Linq.XElement(Constants.Orchestra.orchestraElement, orchestraID, orchestraName, orchestraURL, orchestraNote);
            var node = new System.Xml.Linq.XElement(Constants.Event.eventElement, orchestraItem);

            Orchestra orchestra = Orchestra.GetOrchestraFromNode(node);
            Assert.IsNotNull(orchestra);
            Assert.IsTrue(orchestra.OrchestraID == -1 && orchestra.OrchestraName == "TestOrchestraName");
        }
    }
}