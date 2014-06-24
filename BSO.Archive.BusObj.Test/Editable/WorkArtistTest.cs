using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bso.Archive.BusObj;
using Bso.Archive.BusObj.Utility;

namespace BSO.Archive.BusObj.Test
{
    [TestClass]
    public class WorkArtistTest
    {
        [TestMethod]
        public void UpdateWorkArtistTest()
        {
            Artist testArtist = Artist.GetArtistByID(836);
            Assert.IsTrue(testArtist.ArtistFirstName == "John");

            WorkArtist testWorkArtist = new WorkArtist();
            var workArtistId = Helper.CreateXElement(Constants.WorkArtist.workArtistIDElement, "836");
            var workArtistFirstName = Helper.CreateXElement(Constants.WorkArtist.workArtistFirstNameElement, "Adage Text Name");
            var workArtistItem = new System.Xml.Linq.XElement(Constants.WorkArtist.workArtistElement, workArtistId, workArtistFirstName);
            var workID = new System.Xml.Linq.XElement(Constants.Work.workIDElement, "13435");
            var workGroupID = new System.Xml.Linq.XElement(Constants.Work.workGroupIDElement, "6360");
            var workItem = new System.Xml.Linq.XElement(Constants.Work.workElement, workID, workGroupID, workArtistItem);
            var eventItem = new System.Xml.Linq.XElement(Constants.Event.eventElement, workItem);

            System.Xml.Linq.XDocument doc = new System.Xml.Linq.XDocument(eventItem);

            testWorkArtist.UpdateData(doc, "WorkArtistFirstName", "workArtistFirstname");

            Assert.IsTrue(testArtist.ArtistFirstName == "Adage Text Name");
            testArtist.ArtistFirstName = "Max";
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
            var workArtistId = Helper.CreateXElement(Constants.WorkArtist.workArtistIDElement, "1");
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
            Assert.IsTrue(workArtist.Artist.ArtistID == 1);
            Assert.IsTrue(workArtist.Instrument.Instrument1 == "TestI");
            Assert.IsTrue(workArtist.Artist.ArtistName4 == "Test4Name");
            Assert.IsTrue(workArtist.Artist.ArtistName5 == "Test5Name");
        }

        /// <summary>
        /// Method to test GetWorkArtistByID method. Calls the method with an id to get 
        /// a WorkArtist object then calls the same method again and compares the two 
        /// return arguements to verify they are the same.
        /// </summary>
        [TestMethod]
        public void GetWorkArtistByIDTest()
        {
            WorkArtist workArtist = null; // WorkArtist.GetWorkArtistByID(838);
            if (workArtist.IsNew)
            {
                workArtist.WorkArtistID = 1;
                workArtist.WorkID = 2;
            }
            BsoArchiveEntities.Current.Save();
            WorkArtist workArtist2 = null; // WorkArtist.GetWorkArtistByID(838);
            Assert.IsTrue(workArtist2.Equals(workArtist));
        }

    }
}
