using Bso.Archive.BusObj;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using Bso.Archive.BusObj.Utility;

namespace BSO.Archive.BusObj.Test
{
    [TestClass]
    public class ComposerTest
    {
        [TestMethod]
        public void UpdateComposerTest()
        {
            Composer testComposer = Composer.GetComposerByID(3);
            var workComposerId = Helper.CreateXElement(Constants.Work.workComposerIDElement, "3");
            var workComposerFirstName = Helper.CreateXElement(Constants.Work.workComposerFirstNameElement,
                                                               "Adage Text Name");
            var workComposerLastName = Helper.CreateXElement(Constants.Work.workComposerLastNameElement, "Last Name");
            var workComposer = new XElement(Constants.Work.workComposerElement, workComposerId,
                                                            workComposerFirstName, workComposerLastName);

            var workItem = new XElement("workItem", workComposer);
            var eventItem = new XElement("eventItem", workItem);

            var doc = new XDocument(eventItem);

            Composer composer = new Composer();
            composer.UpdateData(doc, "ComposerFirstName", Constants.Work.workComposerFirstNameElement);
            Assert.IsTrue(testComposer.ComposerFirstName == "Adage Text Name");
            testComposer.ComposerFirstName = "Adam";
            BsoArchiveEntities.Current.Save();     
            
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetComposerFromNodeTest()
        {
            var workComposerId = Helper.CreateXElement(Constants.Work.workComposerIDElement, "1");
            var workComposerFirstName = Helper.CreateXElement(Constants.Work.workComposerFirstNameElement,
                                                               "Adage First");
            var workComposerLastName = Helper.CreateXElement(Constants.Work.workComposerLastNameElement, "Adage Last");
            var workComposerName2 = Helper.CreateXElement(Constants.Work.workComposerName2Element, "Adage Name 2");
            var workComposerName4 = Helper.CreateXElement(Constants.Composer.composerName4Element, "Test4Name");
            var workComposerName5 = Helper.CreateXElement(Constants.Composer.composerName5Element, "Test5Name");
            var workComposerBirthDate = Helper.CreateXElement(Constants.Work.workComposerBirthYearElement, "1900");
            var workComposerDeathYear = Helper.CreateXElement(Constants.Work.workComposerDeathYearElement, "1980");
            var workComposerAddNameFirst = Helper.CreateXElement(Constants.Composer.composerAddNameFirstElement, "TestFName");
            var workComposerAddNameLast = Helper.CreateXElement(Constants.Composer.composerAddNameLastElement, "TestLName");
            var workComposerAddName = new XElement(Constants.Composer.composerAddNameElement, workComposerAddNameFirst, workComposerAddNameLast);


            var workComposer = new XElement(Constants.Work.workComposerElement, workComposerId,
                                                            workComposerFirstName, workComposerLastName, workComposerName2,
                                                            workComposerBirthDate, workComposerDeathYear, workComposerName4, workComposerName5, workComposerAddName);

            Composer composer = Composer.GetComposerFromNode(workComposer);
            Assert.IsNotNull(composer);
            Assert.IsTrue(composer.ComposerID == 1);
            Assert.IsTrue(composer.ComposerName4 == "Test4Name");
            Assert.IsTrue(composer.ComposerName5 == "Test5Name");
        }

        [TestMethod]
        public void GetComposerByIDTest()
        {
            Composer composer1 = Composer.GetComposerByID(4);
            if (composer1.IsNew)
                composer1.ComposerID = 4;

            BsoArchiveEntities.Current.Save();

            Composer composer2 = Composer.GetComposerByID(4);
            Assert.IsNotNull(composer2);
            Assert.IsTrue(composer1.Equals(composer2));

        }
    }
}
