using Bso.Archive.BusObj;
using Bso.Archive.BusObj.Utility;
using BSO.Archive.WebApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BSO.Archive.WebApp.Controls
{
    public partial class PerformanceSearch : BaseUserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            edcEvents.Selecting += edcEvents_Selecting;
            edcEvents.Selected += edcEvents_Selected;

            StartDate.Attributes["PlaceHolder"] = SettingsHelper.FirstEventDate;
            if (!Page.IsPostBack)
            {
                BindDropdownLists();

                RunSearch();
            }
        }

        private void BindDropdownLists()
        {
            var premieres = BsoArchiveEntities.Current.PremiereTypes.Where(pt => pt.PremiereTypeID >= 0);
            WorkPremiere.DataSource = premieres.ToList();
            WorkPremiere.DataBind();
            WorkPremiere.Items.Insert(0, new ListItem("Select All", ""));

            var commissions = BsoArchiveEntities.Current.CommissionTypes.Where(pt => pt.CommissionTypeID >= 0);
            WorkCommissions.DataSource = commissions.ToList();
            WorkCommissions.DataBind();
            WorkCommissions.Items.Insert(0, new ListItem("Select All", ""));
        }

        private void RunSearch()
        {
            if (!IsPerformanceSearch) return;

            bool didLoadFromSearchHistory = SearchHelper.LoadSearch("performance", basicPanel, advancedPanel);
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
            if (HasOrchestra) OrchestraName.Text = OrchestraValue;
            if (HasSeason) SeasonName.Text = SeasonValue;
            if (HasConductor) ConductorFullName.Text = ConductorValue;
            if (HasSoloist) Soloist.Text = SoloistValue;
            if (HasInstrument) ArtistInstrument.Text = InstrumentValue;
            if (HasArranger) WorkArrangement.Text = ArrangerValue;
            if (HasEventTitle) EventTitle.Text = EventTitleValue;
            if (HasVenueName) VenueName.Text = VenueNameValue;
            if (HasVenueCity) VenueCity.Text = VenueCityValue;
            if (HasVenueState) VenueState.Text = VenueStateValue;
            if (HasVenueCountry) VenueCountry.Text = VenueCountryValue;
            if (HasWorkPremiere) WorkPremiere.Text = WorkPremiereValue;
            if (HasWorkCommission) WorkCommissions.Text = WorkCommissionValue;
            if (HasStartTime || HasEndTime)
            {
                EndDate.Text = EndTimeValue;
                StartDate.Text = StartTimeValue;
            }

            if (HasComposer || HasWork || HasOrchestra || HasSeason || HasConductor || HasSoloist || HasInstrument || HasParticipant || HasArranger
                 || HasEventTitle || HasVenueName || HasVenueCity || HasVenueState || HasVenueCountry || HasWorkPremiere || HasWorkCommission || HasStartTime || HasEndTime)
                return true;

            return false;
        }

        void edcEvents_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
        {
            e.Cancel = CheckEmptySearch(basicPanel) && CheckEmptySearch(advancedPanel);
        }

        protected void edcEvents_Selected(object sender, EntityDataSourceSelectedEventArgs e)
        {
            var results = e.Results.Cast<EventDetail>();

            var groupedResults = results.GroupBy(r => r.EventID);

            var resultCount = groupedResults.Count();

            List<List<int>> lstEventDetails = new List<List<int>>();
            

            foreach (var result in groupedResults)
            {
                List<int> eventDetailIDs = new List<int>();
                foreach (var element in result)
                {
                    eventDetailIDs.Add(element.EventDetailID);
                }
                
                lstEventDetails.Add(eventDetailIDs);
            }

            var serializer = new JavaScriptSerializer();


            hfPerformanceSearchResultIds.Value = serializer.Serialize(lstEventDetails);
            var searchParameters = SearchHelper.SaveSearch("performance", basicPanel, advancedPanel);

            PopulateEmailShareDialog();
            

            DisplaySearchParameters(searchParameters);

            CurrentPage.PageMessageBox.ShowOK(String.Concat("Results Count: ", resultCount));
        }

        private void PopulateEmailShareDialog()
        {
            EmailForm emailForm = Page.Controls[0].FindControl("MainContent").FindControl("EmailForm") as EmailForm;

            if (emailForm != null)
            {
                HiddenField searchIdField = emailForm.FindControl("searchIdForEmail") as HiddenField;
                searchIdField.Value = SearchHelper.GetLastSearchID().ToString();

                HiddenField searchTypeField = emailForm.FindControl("searchTypeForEmail") as HiddenField;
                searchTypeField.Value = "Performance";
            }
        }

        protected void DisplaySearchParameters(string parameters)
        {
            var display = SearchHelper.BuildHistoryString(parameters);
            SearchParameterList.Text = String.Concat("Searched for: ", display);
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
                return;

            ResultsListView.DataSourceID = edcEvents.ID;
        }

    }
}