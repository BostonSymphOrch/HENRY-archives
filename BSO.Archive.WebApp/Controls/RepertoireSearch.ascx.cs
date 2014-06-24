using Bso.Archive.BusObj;
using Bso.Archive.BusObj.Utility;
using BSO.Archive.WebApp.Classes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BSO.Archive.WebApp.Controls
{
    public partial class RepertoireSearch : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SearchButton.Click += SearchButton_Click;
            edcEvents.Selecting += edcEvents_Selecting;
            edcEvents.Selected += RepertoireedcEvents_Selected;

            StartDate.Attributes["PlaceHolder"] = SettingsHelper.FirstEventDate;

            if (!Page.IsPostBack)
            {
                BindCommission();

                RunSearch();
            }
        }

        private void BindCommission()
        {
            var commissions = BsoArchiveEntities.Current.CommissionTypes.Where(pt => pt.CommissionTypeID >= 0);
            WorkCommissions.DataSource = commissions.ToList();
            WorkCommissions.DataBind();
            WorkCommissions.Items.Insert(0, new ListItem("Select All", ""));
        }

        private void RunSearch()
        {
            if (!IsRepertoireSearch) return;

            bool didLoadFromSearchHistory = SearchHelper.LoadSearch("repertoire", RepertoireSearchPanel);

            bool didSelectData = FillDataFields();
            if (didLoadFromSearchHistory || didSelectData)
            {
                SearchButton_Click(null, null);
            }
        }


        private bool FillDataFields()
        {
            if (HasComposer) ComposerFullName.Text = ComposerValue;

            if (HasWork) WorkItem.Text = WorkValue;


            if (HasStartTime || HasEndTime)
            {
                EndDate.Text = EndTimeValue;
                StartDate.Text = StartTimeValue;
            }

            return HasComposer || HasWork || HasStartTime || HasEndTime;
        }


        private void LoadSearch()
        {
            if (Request.QueryString["search"] == null)
                return;


            var searchHistoryID = Request.QueryString["search"].ToString();
            int searchQueryID;
            int.TryParse(searchHistoryID, out searchQueryID);

            var searchEntry = BsoArchiveEntities.Current.Searches.FirstOrDefault(s => s.SearchID == searchQueryID);

            if (searchEntry != null)
            {
                StringDictionary dictionary = SearchEntryToDictionary(searchEntry);

                FormHelpers.LoadFormItems(dictionary, RepertoireSearchPanel);

            }

        }

        private StringDictionary SearchEntryToDictionary(Bso.Archive.BusObj.Search searchEntry)
        {
            var parameterArray = searchEntry.SearchParameters.Split('_');

            var dictionary = new StringDictionary();

            foreach (var parameter in parameterArray)
            {
                var searchItem = parameter.Split(':');
                if (searchItem.Length == 1)
                    dictionary.Add(searchItem[0], "");
                else
                    dictionary.Add(searchItem[0], searchItem[1]);
            }

            return dictionary;
        }

        void edcEvents_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
        {
            e.Cancel = CheckEmptySearch(RepertoireSearchPanel);
        }

        void RepertoireedcEvents_Selected(object sender, EntityDataSourceSelectedEventArgs e)
        {
            var results = e.Results.Cast<EventDetail>();

            var groupedResults = results.GroupBy(r => r.EventID);

            var resultCount = groupedResults.Count();

            var resultsTop = groupedResults.Take(resultCount);

            List<List<int>> lstEventDetails = new List<List<int>>();

            List<int> duplicateCheck = new List<int>();

            foreach (var result in groupedResults)
            {
                List<int> eventDetailIDs = new List<int>();
                foreach (var element in result)
                {
                    eventDetailIDs.Add(element.EventDetailID);
                }
                duplicateCheck.Add(result.Key);
                lstEventDetails.Add(eventDetailIDs);
            }

            var serializer = new JavaScriptSerializer();

            hfRepertoireSearchResultIds.Value = serializer.Serialize(lstEventDetails);
            var parameters = SearchHelper.SaveSearch("repertoire", RepertoireSearchPanel);

            PopulateEmailShareDialog();

            DisplaySearchParameters(parameters);
        }


        private void PopulateEmailShareDialog()
        {
            EmailForm emailForm = Page.Controls[0].FindControl("MainContent").FindControl("EmailForm") as EmailForm;

            if (emailForm != null)
            {
                HiddenField searchIdField = emailForm.FindControl("searchIdForEmail") as HiddenField;
                searchIdField.Value = SearchHelper.GetLastSearchID().ToString();

                HiddenField searchTypeField = emailForm.FindControl("searchTypeForEmail") as HiddenField;
                searchTypeField.Value = "Repertoire";
            }
        }

        protected void DisplaySearchParameters(string parameters)
        {
            var display = SearchHelper.BuildHistoryString(parameters);
            SearchParameterList.Text = String.Concat("Searched for: ", display);
        }

        void SearchButton_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
                return;

            dummyListView.DataSourceID = edcEvents.ID;

        }

    }
}