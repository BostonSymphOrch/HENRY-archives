using Bso.Archive.BusObj.Utility;
using System;
using System.Text.RegularExpressions;

namespace BSO.Archive.WebApp.Controls
{
    public partial class EmailForm : System.Web.UI.UserControl
    {
        public int searchID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            sendEmail.Click += sendEmail_Click;
        }

        protected void sendEmail_Click(object sender, EventArgs e)
        {
            //Regex emailRgx = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            if (!Page.IsValid)
                return;
            
            string recipientName = toName.Text.Trim();
            string recipientEmailAddress = toEmail.Text.Trim();
            string senderName = fromName.Text.Trim();
            string senderEmail = fromEmail.Text.Trim();
            string emailMessage = message.Text.Trim();

            if (String.IsNullOrEmpty(senderEmail) || String.IsNullOrEmpty(recipientEmailAddress))
                return;

            string id = searchIdForEmail.Value;
            string type = searchTypeForEmail.Value;

            string tabType = Helper.GetTabType(type);
            string link = String.Format("{0}Search.aspx?searchType={1}&searchId={2}{3}", SettingsHelper.SiteURL, type, id, tabType);
            EmailFunction.ShareSearchResult(recipientName, recipientEmailAddress, senderName, senderEmail, emailMessage, link);
            ResetTextBoxes();
        }

        private void ResetTextBoxes()
        {
            toName.Text = String.Empty;
            toEmail.Text = String.Empty;
            fromName.Text = String.Empty;
            fromEmail.Text = String.Empty;
            message.Text = String.Empty;
            searchIdForEmail.Value = String.Empty;

        }
    }
}