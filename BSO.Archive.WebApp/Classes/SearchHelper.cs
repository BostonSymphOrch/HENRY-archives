using Bso.Archive.BusObj;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace BSO.Archive.WebApp.Classes
{
    public class SearchHelper : BaseUserControl
    {
        private bool HasSearchHistory
        {
            get { return HttpContext.Current.Request.Cookies["searchHistory"] != null; }
        }

        #region SaveSearch
        public static String SaveSearch(String searchType, params Panel[] panels)
        {
            var searchHelper = new SearchHelper();
             return searchHelper.SaveSearchHistory(searchType, panels);
        }

        private String SaveSearchHistory(string searchType, Panel[] panels)
        {
            StringDictionary dictionary = new StringDictionary();

            foreach (var panel in panels)
            {
                FormHelpers.SaveFormItems(dictionary, panel);
            }

            var basicSearchParameters = DictionaryToString(dictionary);

            var search = Bso.Archive.BusObj.Search.AddSearchEntry(basicSearchParameters, searchType);

            if (search.IsNew)
            {
                BsoArchiveEntities.Current.Save();
            }
            SessionContext.Current.LastSearchID = search.SearchID;

            AddCookie(search.SearchID);

            return basicSearchParameters;
        }

        private void AddCookie(int searchID)
        {
            var currentHistory = GetCurrentHistory(searchID);
            HttpCookie historyCookie = new HttpCookie("searchHistory");
            historyCookie.Values["parameters"] = currentHistory;
            historyCookie.Expires = DateTime.Now.AddDays(1);
            HttpContext.Current.Response.Cookies.Add(historyCookie);
        }

        private string GetCurrentHistory(int searchID)
        {
            if (!HasSearchHistory)
                return searchID.ToString();

            string currentHistory = HttpContext.Current.Request.Cookies["searchHistory"]["parameters"].ToString();
            if (String.IsNullOrEmpty(currentHistory))
                currentHistory = searchID.ToString();
            else
                currentHistory = String.Concat(currentHistory, ",", searchID);
            return currentHistory;
        }

        private string DictionaryToString(System.Collections.Specialized.StringDictionary dictionary)
        {
            String search = String.Empty;
            foreach (System.Collections.DictionaryEntry entry in dictionary)
            {
                search += "_" + entry.Key + ":" + entry.Value.ToString().Trim();
            }
            return search;
        }

        #endregion

        #region LoadSearch

        public static bool LoadSearch(String searchType, params Panel[] panels)
        {
            if (HttpContext.Current.Request.QueryString["searchId"] != null)
            {
                var searchHistoryID = HttpContext.Current.Request.QueryString["searchId"].ToString();
                int searchQueryID;
                int.TryParse(searchHistoryID, out searchQueryID);

                var searchEntry = Bso.Archive.BusObj.BsoArchiveEntities.Current.Searches.FirstOrDefault(s => s.SearchID == searchQueryID && s.SearchType == searchType);

                if (searchEntry != null)
                {
                    StringDictionary dictionary = SearchEntryToDictionary(searchEntry);

                    foreach (var panel in panels)
                    {
                        FormHelpers.LoadFormItems(dictionary, panel);
                        //LoadDropDowns(searchType, panel, dictionary);
                    }
                }
                return true;
            }

            return false;
        }

        private static StringDictionary SearchEntryToDictionary(Bso.Archive.BusObj.Search searchEntry)
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

        public static string BuildHistoryString(string searchParameters)
        {
            var entries = searchParameters.Split('_');
            String displaySearch = String.Empty;
            foreach (var entry in entries)
            {
                var values = entry.Split(':');

                if (values.Length > 1 && !String.IsNullOrEmpty(values[1]) && !values[1].Equals("|"))
                    displaySearch = String.Concat(displaySearch, values[1], ", ");
            }
            int excessCommaSpace = 2;
            if (!String.IsNullOrEmpty(displaySearch))
                displaySearch = displaySearch.Remove(displaySearch.Length - excessCommaSpace);

            return displaySearch;
        }

        #endregion

        internal static int GetLastSearchID()
        {
            return SessionContext.Current.LastSearchID;
        }
    }
}