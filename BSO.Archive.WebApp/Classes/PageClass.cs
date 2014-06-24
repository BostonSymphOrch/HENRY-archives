using System.Web.UI;

namespace BSO.Archive.WebApp.Classes
{
    public class PageClass : Page
    {

        public SiteMaster BaseMasterPage
        {
            get { return (SiteMaster)Master; }
        }

        public MessageBox PageMessageBox
        {
            get { return BaseMasterPage.PageMessageBox; }
        }
    }
}