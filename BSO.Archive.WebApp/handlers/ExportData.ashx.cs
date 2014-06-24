using Bso.Archive.BusObj;
using System;
using System.Web;
using System.Web.SessionState;

namespace BSO.Archive.WebApp.handlers
{
    /// <summary>
    /// Summary description for ExportData
    /// </summary>
    public class ExportData : IHttpHandler, IReadOnlySessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();
            context.Response.AddHeader("content-disposition", "attachment; filename=SearchResult.xls");
            context.Response.ContentType = "application/ms-excel";
            context.Response.ContentEncoding = System.Text.Encoding.Unicode;
            context.Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

            var parameters = context.Request.Params;

            var data = SessionContext.Current.ExportData;
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
            SessionContext.Current.ExportData = String.Empty;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}