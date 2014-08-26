using Bso.Archive.BusObj.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Bso.Archive.BusObj.Utility;

namespace Bso.Archive.BusObj
{
    partial class Composer : IOPASData
    {
        /// <summary>
        /// Update given column from xml given the tag name
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="columnName"></param>
        /// <param name="tagName"></param>
        /// <remarks>
        /// Read Composer information from OPAS XML and update the Composer Column based upon appropriate tagName
        /// </remarks>
        public void UpdateData(System.Xml.Linq.XDocument doc, string columnName, string tagName)
        {
            IEnumerable<System.Xml.Linq.XElement> eventElements = doc.Descendants(Constants.Event.eventElement);
            foreach (System.Xml.Linq.XElement element in eventElements)
            {
                IEnumerable<System.Xml.Linq.XElement> composerElements = element.Descendants(Constants.Work.workComposerElement);
                foreach (System.Xml.Linq.XElement composer in composerElements)
                {
                    Composer updateComposer = Composer.GetComposerFromNode(composer);

                    if (updateComposer == null) continue;

                    object newValue = composer.GetXElement(tagName);

                    BsoArchiveEntities.UpdateObject(updateComposer, newValue, columnName);
                }
            }
        }

        /// <summary>
        /// Given an XElement node extracts the composer information and creates a Composer object and returns it.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static Composer GetComposerFromNode(System.Xml.Linq.XElement node)
        {
            if (node == null || node.Element(Constants.Composer.composerIDElement) == null)
                return null;

            int composerID;
            int.TryParse(node.GetXElement(Constants.Composer.composerIDElement), out composerID);


            Composer composer = Composer.GetComposerByID(composerID);
            if (!composer.IsNew)
                return composer;

            composer.ComposerID = composerID;

            GetComposerData(node, composer);

            return composer;
        }

        /// <summary>
        /// Gets the conductors information from the given node.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="composer"></param>
        private static void GetComposerData(System.Xml.Linq.XElement node, Composer composer)
        {
            string composerLastName = node.GetXElement(Constants.Composer.composerLastNameElement);
            string composerFirstName = node.GetXElement(Constants.Composer.composerFirstNameElement);
            string composerName2 = node.GetXElement(Constants.Composer.composerName2Element);
            string composerName4 = node.GetXElement(Constants.Composer.composerName4Element);
            string composerName5 = node.GetXElement(Constants.Composer.composerName5Element);
            string composerBirthYear = node.GetXElement(Constants.Composer.composerBirthYearElement);
            string composerDeathYear = node.GetXElement(Constants.Composer.composerDeathYearElement);

            var composerAddElement = node.Element(Constants.Composer.composerAddNameElement);
            if (composerAddElement != null)
                GetComposerAddNames(composerAddElement, composer);

            SetComposerData(composer, composerLastName, composerFirstName, composerName2, composerName4, composerName5, composerBirthYear, composerDeathYear);
        }

        private static void GetComposerAddNames(System.Xml.Linq.XElement node, Composer composer)
        {
            composer.ComposerAddNameFirst = node.GetXElement(Constants.Composer.composerAddNameFirstElement);
            composer.ComposerAddNameLast = node.GetXElement(Constants.Composer.composerAddNameLastElement);
            composer.ComposerAddName2 = node.GetXElement(Constants.Composer.composerAddName2Element);
            composer.ComposerAddText = node.GetXElement(Constants.Composer.composerAddNameTextElement);   
        }

        /// <summary>
        /// Sets the values passed as parameters to the composer object.
        /// </summary>
        /// <param name="composer"></param>
        /// <param name="composerLastName"></param>
        /// <param name="composerFirstName"></param>
        /// <param name="composerName2"></param>
        /// <param name="composerBirthYear"></param>
        /// <param name="composerDeathYear"></param>
        private static void SetComposerData(Composer composer, string composerLastName, string composerFirstName, string composerName2, string composerName4, string composerName5,
            string composerBirthYear, string composerDeathYear)
        {
            composer.ComposerFirstName = composerFirstName;
            composer.ComposerLastName = composerLastName;
            composer.ComposerName4 = composerName4;
            composer.ComposerName5 = composerName5;
            composer.ComposerName2 = composerName2;
            composer.ComposerBirthYear = composerBirthYear;
            composer.ComposerDeathYear = composerDeathYear;
        }

        internal static Composer GetComposerByID(int composerID)
        {
            Composer composer = BsoArchiveEntities.Current.Composers.FirstOrDefault(a => a.ComposerID == composerID) ??
                                Composer.NewComposer();

            return composer;
        }

        public string FullName
        {
            get
            {
                if (String.IsNullOrEmpty(ComposerFirstName)) return ComposerLastName;
                if (String.IsNullOrEmpty(ComposerLastName)) return ComposerFirstName;
                return String.Concat(ComposerFirstName, " ", ComposerLastName);
            }
        }
        /// <summary>
        /// Composer Full Name Formatted for display
        /// </summary>
        public string ComposerFullName
        {
            get
            {
                return string.Concat(ComposerFirstName, " ", ComposerLastName);
            }
        }

        public string ComposerFullName2
        {
            get
            {
                return string.Concat(ComposerAddNameFirst, " ", ComposerAddNameLast);
            }
        }
    }
}
