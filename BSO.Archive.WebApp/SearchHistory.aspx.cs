using Bso.Archive.BusObj;
using BSO.Archive.WebApp.Classes;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace BSO.Archive.WebApp
{
    public partial class SearchHistory : PageClass
    {
        enum SearchType
        {
            Performance,
            Artist,
            Repertoire
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                BindSearchHistory();
        }

        private void BindSearchHistory()
        {
            if (Request.Cookies["searchHistory"] == null) return;

            var history = Request.Cookies["searchHistory"]["parameters"].ToString();

            var searchIDs = history.Split(',').Select(s => int.Parse(s));

            var context = BsoArchiveEntities.Current;

            historyListView.DataSource =
                context.Searches.Where(s => searchIDs.Contains(s.SearchID) && s.SearchType == "performance");
            historyListView.DataBind();

            artistListView.DataSource =
                context.Searches.Where(s => searchIDs.Contains(s.SearchID) && s.SearchType == "artist");
            artistListView.DataBind();

            repertoireListView.DataSource =
                context.Searches.Where(s => searchIDs.Contains(s.SearchID) && s.SearchType == "repertoire");
            repertoireListView.DataBind();
        }

        protected void PerformanceListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var performanceHistoryLink = e.Item.FindControl("performanceHistoryLink") as HyperLink;

            BuildListItem(e, performanceHistoryLink, SearchType.Performance);
        }

        protected void ArtistListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var artistHistoryLink = e.Item.FindControl("artistHistoryLink") as HyperLink;

            BuildListItem(e, artistHistoryLink, SearchType.Artist);
        }

        protected void RepertoireListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var repertoireHistoryLink = e.Item.FindControl("repertoireHistoryLink") as HyperLink;

            BuildListItem(e, repertoireHistoryLink, SearchType.Repertoire);
        }


        private void BuildListItem(ListViewItemEventArgs e, HyperLink historyEntry, SearchType searchType)
        {
            var searchItem = e.Item.DataItem as Bso.Archive.BusObj.Search;

            if (searchItem == null)
                return;

            var searchTimeHistory = (Label)e.Item.FindControl("searchTimeEntry");

            var searchTypeTab = "#tabs-performance";
            if (searchType == SearchType.Artist)
                searchTypeTab = "#tabs-artist";
            else if (searchType == SearchType.Repertoire)
                searchTypeTab = "#tabs-repertoire";

            historyEntry.Text = ResultFromListItem(e);
            historyEntry.NavigateUrl = String.Format("~/Search.aspx?searchType={0}&searchId={1}{2}", searchType,
                                                     searchItem.SearchID, searchTypeTab);
            searchTimeHistory.Text = String.Format("{0:mm/dd/yyyy hh:MM:ss tt}", searchItem.CreatedOn);

        }

        private String ResultFromListItem(ListViewItemEventArgs e)
        {
            var search = (Bso.Archive.BusObj.Search)e.Item.DataItem;
            var searchParameters = search.SearchParameters;
            return SearchHelper.BuildHistoryString(searchParameters);
        }
    }
}