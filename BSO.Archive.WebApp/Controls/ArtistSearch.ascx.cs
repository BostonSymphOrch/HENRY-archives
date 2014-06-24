using Bso.Archive.BusObj;
using Bso.Archive.BusObj.Utility;
using BSO.Archive.WebApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace BSO.Archive.WebApp.Controls
{
    public partial class ArtistSearch : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SearchButton.Click += ArtistSearchButton_Click;
            edcArtists.Selecting += edcArtists_Selecting;
            edcArtists.Selected += edcArtists_Selected;

            StartDate.Attributes["PlaceHolder"] = SettingsHelper.FirstEventDate;

            if (!Page.IsPostBack)
            {
                RunSearch();
            }
        }


        private void RunSearch()
        {
            if (!IsArtistSearch) return;

            bool didLoadFromSearchHistory = SearchHelper.LoadSearch("artist", ArtistSearchPanel);

            bool didSelectData = FillDataFields();

            if (didLoadFromSearchHistory || didSelectData)
            {
                ArtistSearchButton_Click(null, null);
            }
        }

        private bool FillDataFields()
        {
            if (HasEnsemble) EnsembleName.Text = EnsembleValue;

            if (HasInstrument) Instrument.Text = InstrumentValue;

            if (HasStartTime || HasEndTime)
            {
                EndDate.Text = EndTimeValue;
                StartDate.Text = StartTimeValue;
            }

            return HasEnsemble || HasInstrument || HasStartTime || HasEndTime;
        }

        void ArtistSearchButton_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
                return;

            ResultsListView.DataSourceID = edcArtists.ID;
        }

        void edcArtists_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
        {
            e.Cancel = CheckEmptySearch(ArtistSearchPanel);
        }

        void edcArtists_Selected(object sender, EntityDataSourceSelectedEventArgs e)
        {
            var results = e.Results.Cast<ArtistDetail>();

            var groupedResults = results.GroupBy(r => r.ArtistID);

            var resultsTop = groupedResults.Take(SettingsHelper.NumberOfResults);

            List<int> listResults = new List<int>();
            foreach (var result in resultsTop)
            {
                foreach (var element in result)
                {
                    if (!listResults.Contains((int)element.ArtistDetailID))
                        listResults.Add((int)element.ArtistDetailID);
                }
            }


            var serializer = new JavaScriptSerializer();
            hfArtistSearchResultIds.Value = serializer.Serialize(listResults);
            var parameters = SearchHelper.SaveSearch("artist", ArtistSearchPanel);

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
                searchTypeField.Value = "Artist";
            }
        }


        protected void DisplaySearchParameters(string parameters)
        {
            var display = SearchHelper.BuildHistoryString(parameters);
            SearchParameterList.Text = String.Concat("Searched for: ", display);
        }

        protected void ArtistQuery(object sender, System.Web.UI.WebControls.Expressions.CustomExpressionEventArgs e)
        {
            var results = from v in e.Query.Cast<EventDetail>()
                          group v by v.Instrument1;
            var conductorCount = results.FirstOrDefault(r => r.Key == "Conductor").GroupBy(g => g.EventID).Count();
        }

    }
}