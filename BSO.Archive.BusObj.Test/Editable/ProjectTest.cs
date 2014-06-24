using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bso.Archive.BusObj;
using Bso.Archive.BusObj.Utility;

namespace BSO.Archive.BusObj.Test.Editable
{
    [TestClass]
    public class ProjectTest
    {
        [TestMethod]
        public void UpdateProjectTest()
        {
            Project testProject = Project.GetProjectByID(2);
            Assert.IsTrue(testProject.ProjectName2 == "asdffdsa");

            var projectID = Helper.CreateXElement(Constants.Project.projectIDElement, "2");
            var projectName2 = Helper.CreateXElement(Constants.Project.projectName2Element, "ADAGE Test Name2");
            var projectItem = new System.Xml.Linq.XElement(Constants.Project.projectElement, projectName2, projectID);
            var eventItem = new System.Xml.Linq.XElement(Constants.Event.eventElement, projectItem);
            System.Xml.Linq.XDocument doc = new System.Xml.Linq.XDocument(eventItem);

            Project project = Project.NewProject();
            project.UpdateData(doc, "ProjectName2", "eventProjectName2");
            Assert.IsTrue(testProject.ProjectName2 == "ADAGE Test Name2");
        }


        /// <summary>
        /// Tests GetProjectFromNodeItem by creating an eventItem XElement element and passing it 
        /// to the method. Checks that the object was created and with the correct values. 
        /// </summary>
        [TestMethod]
        public void GetProjectFromNodeTest()
        {
            var projectID = Helper.CreateXElement(Constants.Project.projectIDElement, "0");
            var projectName = Helper.CreateXElement(Constants.Project.projectNameElement, "Test Name");
            var projectName2 = Helper.CreateXElement(Constants.Project.projectName2Element, "TestProjectName");
            var projectTypeName = Helper.CreateXElement(Constants.Project.projectTypeNameElement, "TestProjectTypeName");
            var projectItem = new System.Xml.Linq.XElement(Constants.Project.projectElement, projectID, projectName, projectName2, projectTypeName);
            var node = new System.Xml.Linq.XElement(Constants.Event.eventElement, projectItem);

            Project project = Project.GetProjectFromNode(node);
            Assert.IsNotNull(project);
            Assert.IsTrue(project.ProjectID == 0 && project.ProjectName2 == "TestProjectName");
        }

        /// <summary>
        /// Tests the GetProjectByID method
        /// </summary>
        [TestMethod]
        public void GetProjectByIDTest()
        {
            Project project1 = Project.GetProjectByID(2);
            if(project1.IsNew)
                project1.ProjectID = 2;

            BsoArchiveEntities.Current.Save();
            Project project2 = Project.GetProjectByID(2);
            Assert.IsNotNull(project2);
            Assert.IsTrue(project2.Equals(project1));
        }
    }
}
