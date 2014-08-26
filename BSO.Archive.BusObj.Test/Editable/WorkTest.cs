using System;
using Bso.Archive.BusObj;
using Bso.Archive.BusObj.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BSO.Archive.BusObj.Test
{
    [TestClass]
    public class WorkTest
    {
        /// <summary>
        /// Method to update a particular WorkArtist table field
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="newValue"></param>
        /// <param name="workArtist"></param>
        /// <remarks>
        /// Takes the name of the column and the new value to update to
        /// and updates the databse.
        /// </remarks>
        [Ignore]
        [TestMethod]
        public void UpdateWorkTest()
        {
            Work testWork = Work.GetWorkByID(-1);
            if (testWork.IsNew)
            {
                testWork.WorkID = -1;
            }
            testWork.WorkTitle = "Adage";
            BsoArchiveEntities.Current.Save();

            var workId = Helper.CreateXElement(Constants.Work.workIDElement, "-1");
            var workGroupID = new System.Xml.Linq.XElement(Constants.Work.workTitleElement, "Test");
            var workItem = new System.Xml.Linq.XElement(Constants.Work.workElement, workId, workGroupID);
            var eventElement = new System.Xml.Linq.XElement(Constants.Event.eventElement, workItem);

            System.Xml.Linq.XDocument doc = new System.Xml.Linq.XDocument(eventElement);
            Work work = Work.NewWork();
            work.UpdateData(doc, "WorkTitle", Constants.Work.workTitleElement);

            Assert.IsTrue(testWork.WorkTitle == "Test");
            BsoArchiveEntities.Current.DeleteObject(testWork);
            BsoArchiveEntities.Current.DeleteObject(work);
            BsoArchiveEntities.Current.Save();
        }

        /// <summary>
        /// Tests the GetWorkArtist method by passing testing whether or not a WorkArtist object is added to WorkArtists
        /// when passed the Work object and an XElement node.
        /// </summary>
        [TestMethod]
        public void GetWorkArtistTest()
        {
            var workArtistID = Helper.CreateXElement(Constants.WorkArtist.workArtistIDElement, "-1");
            var workArtistLastName = Helper.CreateXElement(Constants.WorkArtist.workArtistLastNameElement, "TestL");
            var workArtistFirstName = Helper.CreateXElement(Constants.WorkArtist.workArtistFirstNameElement, "TestF");
            var workArtistInstrument = Helper.CreateXElement(Constants.WorkArtist.workArtistInstrumentElement, "TestI");
            var workArtistInstrument2 = Helper.CreateXElement(Constants.WorkArtist.workArtistInstrument2Element, "TestI2");
            var workArtistNotes = Helper.CreateXElement(Constants.WorkArtist.workArtistNoteElement, "Test Notes");
            var workArtistCommission = Helper.CreateXElement(Constants.Work.workCommissionElement, "Commission Test");

            System.Xml.Linq.XElement artistNode = new System.Xml.Linq.XElement(Constants.WorkArtist.workArtistElement, workArtistID, workArtistLastName, workArtistFirstName,
                workArtistLastName, workArtistInstrument, workArtistInstrument2, workArtistNotes);
            System.Xml.Linq.XElement node = new System.Xml.Linq.XElement(Constants.Work.workElement, artistNode);

            Work work = Work.GetWorkByID(-1);
            work.WorkArtists.Clear();
            Assert.IsTrue(work.WorkArtists.Count == 0);
            work = Work.GetWorkArtists(node, work);
            Assert.IsTrue(work.WorkArtists.Count == 1);
        }

        /// <summary>
        /// Test The AddWorkArtistMethod to ensure that it adds the WorkArtist object to the Work object's collection
        /// of WorkArtists.
        /// </summary>
        [TestMethod]
        public void AddWorkArtistTest_New()
        {
            Work work = Work.NewWork();
            work.WorkID = -1;
            Assert.IsTrue(work.WorkArtists.Count == 0);

            WorkArtist workArtist = WorkArtist.NewWorkArtist();
            workArtist.WorkArtistID = -1;
            Work.AddWorkArtist(work, workArtist);
            Assert.IsTrue(work.WorkArtists.Count == 1);
        }

        /// <summary>
        /// Test to Verify that the AddWorkArtist method will not add a
        /// WorkArtist if one with that ID already exists.
        /// </summary>
        [TestMethod]
        public void AddWorkArtistTest_Existing()
        {
            Work work = Work.NewWork();
            work.WorkID = -1;

            WorkArtist workArtist = WorkArtist.NewWorkArtist();
            workArtist.WorkArtistID = -1;

            work.WorkArtists.Add(workArtist);
            Assert.IsTrue(work.WorkArtists.Count == 1);

            Work.AddWorkArtist(work, workArtist);
            Assert.IsTrue(work.WorkArtists.Count == 1);
        }

        /// <summary>
        /// Tests to verify that when extracting the information from the XElement node
        /// that the Work object is created correctly with the correct data.
        /// </summary>
        //[TestMethod]
        //public void GetWorkItemFromNodeTest()
        //{
        //    var workId = Helper.CreateXElement(Constants.Work.workIDElement, "-100");
        //    var workGroupID = new System.Xml.Linq.XElement(Constants.Work.workGroupIDElement, "-100");
        //    var workItem = new System.Xml.Linq.XElement(Constants.Work.workElement, workId, workGroupID);

        //    Work work = Work.GetWorkFromNode(workItem);
        //    Assert.IsNotNull(work);
        //    Assert.IsTrue(work.WorkID == -100);
        //    BsoArchiveEntities.Current.DeleteObject(work);
        //    BsoArchiveEntities.Current.Save();
        //}
    }
}