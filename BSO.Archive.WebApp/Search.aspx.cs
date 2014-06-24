using Bso.Archive.BusObj.Utility;
using BSO.Archive.WebApp.Classes;
using System;


namespace BSO.Archive.WebApp
{
    public partial class Search : PageClass
    {
        string SearchValue
        {
            get
            {
                if (String.IsNullOrEmpty(Request.QueryString["searchId"])) return String.Empty;

                return Request.QueryString["searchId"].ToString();
            }
        }

        bool HasSearchID
        {
            get
            {
                int searchId;
                int.TryParse(SearchValue, out searchId);
                return searchId > 0;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            MaxSearchResults.Value = SettingsHelper.NumberOfResults.ToString();

            // Search Information
            if (Page.IsPostBack || Request.QueryString.Count > 0 || (Request.Cookies["searchHistory"] != null))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "searchInfoInitialHide", "$(\".info_show\").hide();", true);
            }

            if (!Page.IsPostBack)
            {
                SetSearchId();
            }
        }

        private void SetSearchId()
        {
            if (HasSearchID)
                SearchID.Value = SearchValue;
        }
    }
}