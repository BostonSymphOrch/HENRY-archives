using Bso.Archive.BusObj.Utility;
using BSO.Archive.WebApp.Classes;
using System;

namespace BSO.Archive.WebApp
{
    public partial class Error : PageClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageMessageBox.ShowError(SettingsHelper.ErrorPageHeader);
        }
    }
}