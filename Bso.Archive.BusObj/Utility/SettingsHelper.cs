using System.Collections.Generic;
using Bso.Archive.BusObj.Properties;

namespace Bso.Archive.BusObj.Utility
{
    public static class SettingsHelper
    {
        /// <summary>
        /// Content DM Landing URL
        /// </summary>
        public static string ContentDMLandingURL
        {
            get { return Settings.Default.ContentDMLandingURL; }
        }
        /// <summary>
        /// Content DM Program Link
        /// </summary>
        public static string ContentDMProgramLink
        {
            get { return Settings.Default.ContentDMProgramLink; }
        }
        /// <summary>
        /// Number of Results
        /// </summary>
        public static int NumberOfResults
        {
            get { return Settings.Default.NumberofResults; }
        }
        /// <summary>
        /// Site base URL
        /// </summary>
        public static string SiteURL
        {
            get { return Settings.Default.SiteURL; }
        }
        /// <summary>
        /// First Event Date
        /// </summary>
        public static string FirstEventDate
        {
            get { return Settings.Default.FirstEventDate; }
        }
        /// <summary>
        /// Error Message
        /// </summary>
        public static string ErrorPageHeader
        {
            get { return Settings.Default.ErrorPageHeader; }
        }

        public static string ExcludedWorkGroupIds
        {
            get
            {
                return Settings.Default.ExludedWorkGroupIds;
            }
        } 
    }
}
