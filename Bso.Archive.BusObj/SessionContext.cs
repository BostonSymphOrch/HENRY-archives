using System;
using System.Web;

namespace Bso.Archive.BusObj
{
    [Serializable]
    public class SessionContext
    {
        public static SessionContext Current
        {
            get
            {
                if (System.Web.HttpContext.Current == null || System.Web.HttpContext.Current.Session == null)
                    return new SessionContext();
                else
                {
                    if (HttpContext.Current.Session["ArchiveSession"] == null)
                    {
                        SessionContext curr = new SessionContext();
                        HttpContext.Current.Session.Add("ArchiveSession", curr);
                    }

                    return (SessionContext)HttpContext.Current.Session["ArchiveSession"];
                }
            }
        }

        public string ExportData { get; set; }

        public int LastSearchID { get; set; }
    }
}
