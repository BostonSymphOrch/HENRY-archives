using System.Linq;

namespace Bso.Archive.BusObj
{
    partial class Search
    {
        /// <summary>
        /// Add a new entry into the database with given control parameters and type of search.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Search AddSearchEntry(string parameters, string type)
        {
            var searchEntry = BsoArchiveEntities.Current.Searches.FirstOrDefault(s => s.SearchParameters == parameters && s.SearchType == type);

            if (searchEntry != null)
                return searchEntry;

            searchEntry = Search.NewSearch();

            searchEntry.SearchType = type;
            searchEntry.SearchParameters = parameters;

            return searchEntry;
        }
    }
}
