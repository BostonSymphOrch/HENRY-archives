using Bso.Archive.BusObj;
using BSO.Archive.DTO;
using System;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Bso.Archive.BusObj.Utility;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using System.Data;
using System.Text;
using System.Web;

namespace BSO.Archive.WebApp
{
    /// <summary>
    /// Summary description for ArchiveWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ArchiveWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string RunImport(bool update)
        {
            try
            {
                if (update)
                {
                    var updateData = new OPASUpdate();
                    updateData.UpdateOPASData();
                }
                else
                {
                    var importData = new ImportOPASData();
                    importData.Initialize();
                    importData.Import();
                    //importData.Finalize();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "Success";
        }


        /// <summary>
        /// Given a string of EventDetailIDs returns a serialized list of EventDTOs
        /// </summary>
        /// <param name="eventDetailIds"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = false)]
        public string GetEventDetails(string eventDetailIds)
        {
            if (string.IsNullOrEmpty(eventDetailIds))
                return string.Empty;
            var eventDetail = EventDTO.GetEventDTOByEventDetailIDs(eventDetailIds);

            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(eventDetail);
        }

        /// <summary>
        /// Given a list of EventDetailIDs returns a list of WorkDTOs
        /// </summary>
        /// <param name="repertoireIds"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod]
        public string GetRepertoires(string repertoireIds)
        {
            var works = RepertoireDTO.GetWorkDTOByEventDetailIDs(repertoireIds);

            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(works);
        }

        /// <summary>
        /// Takes a string of ArtistDetailIDs and returns a serialized list of ArtistDTOs
        /// </summary>
        /// <param name="artistDetailIds"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod]
        public string GetArtistDetails(string artistDetailIds)
        {
            var artistDetail = ArtistDTO.GetArtistDTOByArtistIDs(artistDetailIds);

            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(artistDetail);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public void SaveExportDataToSession(string exportData)
        {
            exportData = HttpUtility.HtmlDecode(exportData);

            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.AddHeader("content-disposition", "attachment; filename=SearchResult.xls");
            context.Response.ContentType = "application/ms-excel";
            context.Response.ContentEncoding = System.Text.Encoding.Unicode;
            context.Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

            var parameters = context.Request.Params;

            var data = exportData;
            //Convert <div><ul><li> to <table><tr><td>
            data = data.Replace("<div>", "<table><tr>").Replace("<ul", "<td rowspan='2'").Replace("<li>", String.Empty)
                       .Replace("</li>", String.Empty).Replace("</ul>", "</td>").Replace("</div>", "</tr></table>");


            // Look for invalid HTML (td inside td renders as td after td)
            data = data.Replace("<td class=\"tableColumn\">\n\t\t\t\t<td rowspan='2'>", "<td class=\"tableColumn\">");
            data = data.Replace("</td>\n\t\t\t</td>", "</td>");

            if (!String.IsNullOrEmpty(data))
            {
                var output = "<head><style>td{border:1px solid #000;}</style></head>" + data;
                context.Response.Write(output);
            }            

            context.Response.End();
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public string[] GetComposers(string prefixText, int count)
        {
            ICacheManager CacheManager = CacheFactory.GetCacheManager();
            
            string key = Constants.Composer.EntityName;
            var composers = CacheManager.GetData(key) as List<ArchiveAutocomplete.AutoCompleteKeyValue>;

            if (composers == null)
            {
                composers = ArchiveAutocomplete.GetDistinctComposers();

                CacheManager.Add(key, composers);
            }
            var items = BuildAutoCompleteItemList(composers, prefixText);

            return items.ToArray();
        }

        

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public string[] GetWorks(string prefixText, int count)
        {
            ICacheManager CacheManager = CacheFactory.GetCacheManager();

            string key = Constants.Work.EntityName;
            var works = CacheManager.GetData(key) as List<ArchiveAutocomplete.AutoCompleteKeyValue>;
            
            if (works == null)
            {
                works = ArchiveAutocomplete.GetDistinctWorks();
                
                CacheManager.Add(key, works);
            }

            var items = BuildAutoCompleteItemList(works, prefixText);

            return items.ToArray();
        }
        
        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public string[] GetCountries(string prefixText, int count)
        {
            ICacheManager CacheManager = CacheFactory.GetCacheManager();

            String key = Constants.Venue.VenueCountry;

            var countries = CacheManager.GetData(key) as IQueryable<String>;

            if (countries == null)
            {
                countries = ArchiveAutocomplete.GetDistinctCountries();

                CacheManager.Add(key, countries);
            }

            return countries.Where(w => w.Contains(prefixText)).ToArray();
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public string[] GetStates(string prefixText, int count)
        {
            ICacheManager CacheManager = CacheFactory.GetCacheManager();

            String key = Constants.Venue.VenueState;

            var states = CacheManager.GetData(key) as IQueryable<String>;

            if (states == null)
            {
                states = ArchiveAutocomplete.GetDistinctStates();

                CacheManager.Add(key, states);
            }
            return states.Where(w => w.Contains(prefixText)).ToArray();
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public string[] GetCities(string prefixText, int count)
        {
            ICacheManager CacheManager = CacheFactory.GetCacheManager();

            String key = Constants.Venue.VenueCity;

            var cities = CacheManager.GetData(key) as IQueryable<String>;

            if (cities == null)
            {
                cities = ArchiveAutocomplete.GetDistinctCities();

                CacheManager.Add(key, cities);
            }

            return cities.Where(w => w.Contains(prefixText)).ToArray();
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public string[] GetVenues(string prefixText, int count)
        {
            ICacheManager CacheManager = CacheFactory.GetCacheManager();

            String key = Constants.Venue.EntityName;

            var venues = CacheManager.GetData(key) as IQueryable<String>;

            if (venues == null)
            {
                venues = ArchiveAutocomplete.GetDistinctVenues();

                CacheManager.Add(key, venues);
            }

            return venues.Where(w => w.Contains(prefixText)).ToArray();
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public string[] GetTitles(string prefixText, int count)
        {
            ICacheManager CacheManager = CacheFactory.GetCacheManager();
            String key = Constants.Event.EntityName;
            var eventTitles = CacheManager.GetData(key) as IQueryable<String>;

            if (eventTitles == null)
            {
                eventTitles = ArchiveAutocomplete.GetDistinctEventTitles();

                CacheManager.Add(key, eventTitles);
            }

            return eventTitles.Where(w => w.Contains(prefixText)).ToArray();
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public string[] GetArrangements(string prefixText, int count)
        {
            ICacheManager CacheManager = CacheFactory.GetCacheManager();
            String key = Constants.Work.WorkArrangement;
            var arrangers = CacheManager.GetData(key) as IQueryable<String>;

            if (arrangers == null)
            {
                arrangers = ArchiveAutocomplete.GetDistinctArrangers();

                CacheManager.Add(key, arrangers);
            }

            return arrangers.Where(w => w.Contains(prefixText)).ToArray();
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public string[] GetInstruments(string prefixText, int count)
        {
            ICacheManager CacheManager = CacheFactory.GetCacheManager();
            String key = Constants.Instrument.EntityName;
            var instruments = CacheManager.GetData(key) as IQueryable<String>;

            if (instruments == null)
            {
                instruments = ArchiveAutocomplete.GetDistinctPerformanceInstruments();

                CacheManager.Add(key, instruments);
            }
            return instruments.Where(w => w.Contains(prefixText)).ToArray();
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public string[] GetSoloists(string prefixText, int count)
        {
            ICacheManager CacheManager = CacheFactory.GetCacheManager();
            String key = Constants.Artist.EntityName;

            var soloists = CacheManager.GetData(key) as List<ArchiveAutocomplete.AutoCompleteKeyValue>;

            if (soloists == null)
            {
                soloists = ArchiveAutocomplete.GetDistinctSoloists();

                CacheManager.Add(key, soloists);
            }

            var items = BuildAutoCompleteItemList(soloists, prefixText);

            return items.ToArray();
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public string[] GetConductors(string prefixText, int count)
        {
            ICacheManager CacheManager = CacheFactory.GetCacheManager();
            String key = Constants.Conductor.EntityName;
            var conductors = CacheManager.GetData(key) as List<ArchiveAutocomplete.AutoCompleteKeyValue>;

            if (conductors == null)
            {
                conductors = ArchiveAutocomplete.GetDistinctConductors();

                CacheManager.Add(key, conductors);
            }

            var items = BuildAutoCompleteItemList(conductors, prefixText);

            return items.ToArray();
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public string[] GetSeasons(string prefixText, int count)
        {
            ICacheManager CacheManager = CacheFactory.GetCacheManager();
            String key = Constants.Season.EntityName;
            var seasons = CacheManager.GetData(key) as IQueryable<String>;

            if (seasons == null)
            {
                seasons = ArchiveAutocomplete.GetDistinctSeasons();

                CacheManager.Add(key, seasons);
            }

            return seasons.Where(w => w.Contains(prefixText)).ToArray();
        }
        
        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public string[] GetOrchestras(string prefixText, int count)
        {
            ICacheManager CacheManager = CacheFactory.GetCacheManager();
            String key = Constants.Orchestra.EntityName;
            var orchestras = CacheManager.GetData(key) as IQueryable<String>;

            if (orchestras == null)
            {
                orchestras = ArchiveAutocomplete.GetDistinctOrchestras();

                CacheManager.Add(key, orchestras);
            }

            return orchestras.Where(w => w.Contains(prefixText)).ToArray();
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public string[] GetEnsembleTypes(string prefixText, int count)
        {
            ICacheManager CacheManager = CacheFactory.GetCacheManager();
            String key = Constants.ArtistDetail.EnsembleType;
            var ensembleTypes = CacheManager.GetData(key) as IQueryable<String>;

            if (ensembleTypes == null)
            {
                ensembleTypes = ArchiveAutocomplete.GetDistinctInstruments();

                CacheManager.Add(key, ensembleTypes);
            }

            return ensembleTypes.Where(w => w.Contains(prefixText)).ToArray();
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public string[] GetEnsembles(string prefixText, int count)
        {
            ICacheManager CacheManager = CacheFactory.GetCacheManager();

            String key = Constants.ArtistDetail.EnsembleName;
            var ensembles = CacheManager.GetData(key) as List<ArchiveAutocomplete.AutoCompleteKeyValue>;
            
            if (ensembles == null)
            {
                ensembles = ArchiveAutocomplete.GetDistinctEnsembles();
                
                CacheManager.Add(key, ensembles);
            }

            var items = BuildAutoCompleteItemList(ensembles, prefixText);

            return items.ToArray();
        }

        private static List<string> BuildAutoCompleteItemList(List<ArchiveAutocomplete.AutoCompleteKeyValue> list, string prefix)
        {
            var items = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                //if (list[i].Key.Contains("Symphony No.   2"))
                //    throw new NotImplementedException();

                //if (list[i].Value.Contains("Symphony No.   2"))
                //    throw new NotImplementedException();

                if (!string.IsNullOrEmpty(list[i].Value)/* && !list[i].Key.IsNormalized(NormalizationForm.FormD)*/)
                {
                    if (list[i].Value.ToUpper().Contains(prefix.ToUpper()))
                        items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(list[i].Key, list[i].Value));
                }
                else 
                {
                    if(list[i].Key.ToUpper().Contains(prefix.ToUpper()))
                        items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(list[i].Key, list[i].Key));
                }
                    
            }
            return items;
        }
    }
}
