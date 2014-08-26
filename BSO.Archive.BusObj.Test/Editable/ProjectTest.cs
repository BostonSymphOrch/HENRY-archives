using System;
using Bso.Archive.BusObj;
using Bso.Archive.BusObj.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BSO.Archive.BusObj.Test.Editable
{
    [TestClass]
    public class ProjectTest
    {
        [Ignore]
        [TestMethod]
        public void UpdateProjectTest()
        {
            Project testProject = Project.GetProjectByID(-1);
            if (testProject.IsNew)
            {
                testProject.ProjectID = -1;
            }
            testProject.ProjectName2 = "Adage";
            BsoArchiveEntities.Current.Save();

            var projectID = Helper.CreateXElement(Constants.Project.projectIDElement, "-1");
            var projectName2 = Helper.CreateXElement(Constants.Project.projectName2Element, "Test");
            var projectItem = new System.Xml.Linq.XElement(Constants.Project.projectElement, projectName2, projectID);
            var eventItem = new System.Xml.Linq.XElement(Constants.Event.eventElement, projectItem);
            System.Xml.Linq.XDocument doc = new System.Xml.Linq.XDocument(eventItem);

            Project project = Project.NewProject();
            project.UpdateData(doc, "ProjectName2", "eventProjectName2");
            Assert.IsTrue(testProject.ProjectName2 == "Test");

            BsoArchiveEntities.Current.DeleteObject(testProject);
            BsoArchiveEntities.Current.DeleteObject(project);
            BsoArchiveEntities.Current.Save();
        }

        /// <summary>
        /// Tests GetProjectFromNodeItem by creating an eventItem XElement element and passing it
        /// to the method. Checks that the object was created and with the correct values.
        /// </summary>
        [TestMethod]
        public void GetProjectFromNodeTest()
        {
            var projectID = Helper.CreateXElement(Constants.Project.projectIDElement, "-1");
            var projectName = Helper.CreateXElement(Constants.Project.projectNameElement, "Test Name");
            var projectName2 = Helper.CreateXElement(Constants.Project.projectName2Element, "TestProjectName");
            var projectTypeName = Helper.CreateXElement(Constants.Project.projectTypeNameElement, "TestProjectTypeName");
            var projectItem = new System.Xml.Linq.XElement(Constants.Project.projectElement, projectID, projectName, projectName2, projectTypeName);
            var node = new System.Xml.Linq.XElement(Constants.Event.eventElement, projectItem);

            Project project = Project.GetProjectFromNode(node);
            Assert.IsNotNull(project);
            Assert.IsTrue(project.ProjectID == -1);
        }
    }
}