using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Bso.Archive.BusObj.Interface;
using Bso.Archive.BusObj.Utility;

namespace Bso.Archive.BusObj
{
    partial class Project : IOPASData
    {
        #region IOPASData

        /// <summary>
        /// Updates the existing database Project on the column name using the 
        /// XML document parsed using the tagName.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="columnName"></param>
        /// <param name="tagName"></param>
        public void UpdateData(System.Xml.Linq.XDocument doc, string columnName, string tagName)
        {
            IEnumerable<System.Xml.Linq.XElement> eventElements = doc.Descendants(Constants.Event.eventElement);
            foreach (System.Xml.Linq.XElement element in eventElements)
            {
                Project updateProject = Project.GetProjectFromNode(element);

                if (updateProject == null) continue;

                System.Xml.Linq.XElement projectNode = element.Element(Constants.Project.projectElement);

                object newValue = projectNode.GetXElement(tagName);

                BsoArchiveEntities.UpdateObject(updateProject, newValue, columnName);
            }
        }
        #endregion

        /// <summary>
        /// Returns a Project object based on the eventProject data passed by the
        /// XElement eventItem element. 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static Project GetProjectFromNode(System.Xml.Linq.XElement node)
        {
            System.Xml.Linq.XElement projectElement = node.Element(Constants.Project.projectElement);
            if (projectElement == null || string.IsNullOrEmpty(projectElement.GetXElement(Constants.Project.projectIDElement)))
                return null;

            int projectID;
            int.TryParse(projectElement.GetXElement(Constants.Project.projectIDElement), out projectID);

            Project project = GetProjectByID(projectID);
            if (!project.IsNew)
                return project;

            string projectName = projectElement.GetXElement(Constants.Project.projectNameElement);
            string projectName2 = projectElement.GetXElement(Constants.Project.projectName2Element);
            string projectTypeName = projectElement.GetXElement(Constants.Project.projectTypeNameElement);

            project = SetWorkData(projectID, project, projectName, projectName2, projectTypeName);

            return project;
        }

        /// <summary>
        /// Takes work item values and assigns them to work attributes.
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="project"></param>
        /// <param name="projectName"></param>
        /// <param name="projectName2"></param>
        /// <param name="projectTypeName"></param>
        /// <returns></returns>
        private static Project SetWorkData(int projectID, Project project, string projectName, string projectName2, string projectTypeName)
        {
            project.ProjectID = projectID;
            project.ProjectName = projectName;
            project.ProjectName2 = projectName2;
            project.ProjectTypeName = projectTypeName;

            return project;
        }

        /// <summary>
        /// Checks existing Project based upon the given ID. If the Project 
        /// already exists then return it. Otherwise create a new Project 
        /// and return it.
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        internal static Project GetProjectByID(int projectID)
        {
            Project project = BsoArchiveEntities.Current.Projects.FirstOrDefault(p => p.ProjectID == projectID) ?? 
                Project.NewProject();

            return project;
        }
    }
}
