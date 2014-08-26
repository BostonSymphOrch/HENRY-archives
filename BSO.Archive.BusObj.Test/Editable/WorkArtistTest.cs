using System;
using Bso.Archive.BusObj;
using Bso.Archive.BusObj.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BSO.Archive.BusObj.Test
{
    [TestClass]
    public class WorkArtistTest
    {
        [Ignore]
        [TestMethod]
        public void UpdateWorkArtistTest()
        {
            WorkArtist testArtist = WorkArtist.GetWorkArtistByID(-1, -1);
            if (testArtist.IsNew)
            {
                testArtist.ArtistID = -1;
                testArtist.WorkID = -1;
            }

            testArtist.Artist.ArtistFirstName = "Adage";
            BsoArchiveEntities.Current.Save();

            WorkArtist testWorkArtist = new WorkArtist();
            var workArtistId = Helper.CreateXElement(Constants.WorkArtist.workArtistIDElement, "-1");
            var workArtistFirstName = Helper.CreateXElement(Constants.WorkArtist.workArtistFirstNameElement, "Test");
            var workArtistItem = new System.Xml.Linq.XElement(Constants.WorkArtist.workArtistElement, workArtistId, workArtistFirstName);
            var workID = new System.Xml.Linq.XElement(Constants.Work.workIDElement, "-1");
            var workGroupID = new System.Xml.Linq.XElement(Constants.Work.workGroupIDElement, "-1");
            var workItem = new System.Xml.Linq.XElement(Constants.Work.workElement, workID, workGroupID, workArtistItem);
            var eventItem = new System.Xml.Linq.XElement(Constants.Event.eventElement, workItem);

            System.Xml.Linq.XDocument doc = new System.Xml.Linq.XDocument(eventItem);

            testWorkArtist.UpdateData(doc, "WorkArtistFirstName", "workArtistFirstname");

            Assert.IsTrue(testArtist.Artist.ArtistFirstName == "Test");
            BsoArchiveEntities.Current.DeleteObject(testWorkArtist);
            BsoArchiveEntities.Current.DeleteObject(testArtist);
            BsoArchiveEntities.Current.DeleteObject(Work.GetWorkByID(-1));
            BsoArchiveEntities.Current.Save();
        }

        /// <summary>
        /// Tests to Verify that GetWorkArtistFromNode correctly extracts the data
        /// from a XElement node of type workArtist and creates a WorkArtist object
        /// and returns it.
        /// </summary>
        [TestMethod]
        public void GetWorkArtistFromNodeTest()
        {
            var workArtistId = Helper.CreateXElement(Constants.WorkArtist.workArtistIDElement, "-1");
            var workArtistFirstName = Helper.CreateXElement(Constants.WorkArtist.workArtistFirstNameElement, "TestFName");
            var workArtistLastName = Helper.CreateXElement(Constants.WorkArtist.workArtistLastNameElement, "TestLCode");
            var workArtistInstrument = Helper.CreateXElement(Constants.WorkArtist.workArtistInstrumentElement, "TestI");
            var workArtistInstrument2 = Helper.CreateXElement(Constants.WorkArtist.workArtistInstrument2Element, "TestI2");
            var workArtistNotes = Helper.CreateXElement(Constants.WorkArtist.workArtistNoteElement, "TestNote");
            var workArtistName4 = Helper.CreateXElement(Constants.WorkArtist.workArtistName4Element, "Test4Name");
            var workArtistName5 = Helper.CreateXElement(Constants.WorkArtist.workArtistName5Element, "Test5Name");

            System.Xml.Linq.XElement node = new System.Xml.Linq.XElement(Constants.WorkArtist.workArtistElement, workArtistId, workArtistFirstName,
                workArtistLastName, workArtistInstrument, workArtistInstrument2, workArtistNotes, workArtistName4, workArtistName5);

            WorkArtist workArtist = WorkArtist.GetWorkArtistFromNode(node);

            Assert.IsNotNull(workArtist);

            Assert.IsTrue(workArtist.Artist.ArtistFirstName == "TestFName");
            Assert.IsTrue(workArtist.Artist.ArtistID == -1);
            Assert.IsTrue(workArtist.Instrument.Instrument1 == "TestI");
        }
    }
}