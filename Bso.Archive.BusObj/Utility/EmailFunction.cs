
using Bso.Archive.BusObj.Properties;
using System;
using System.IO;
using System.Net.Mail;

namespace Bso.Archive.BusObj.Utility
{
    public class EmailFunction
    {
        /// <summary>
        /// Share Search Results by Email
        /// </summary>
        /// <param name="recipientName">Recipient Name</param>
        /// <param name="recipientEmailAddress">Recipient Email Address</param>
        /// <param name="senderName">Sender Name</param>
        /// <param name="senderEmail">Sender Email</param>
        /// <param name="message">Email Message</param>
        /// <param name="link">Link to the Search</param>
        public static void ShareSearchResult(string recipientName, string recipientEmailAddress, string senderName,
                                        string senderEmail, string message, string link)
        {
            var emailContent = GetFileText(Settings.Default.EmailTemplates, "ShareSearchResult.htm");

            var content = emailContent.Replace("#RECIPIENT_NAME#", recipientName)
                                      .Replace("#CONTENTDM_LANDINGURL#", SettingsHelper.ContentDMLandingURL)
                                      .Replace("#EMAIL_MESSAGE#", message)
                                      .Replace("#SENDER_NAME#", senderName)
                                      .Replace("#SENDER_EMAIL#", senderEmail)
                                      .Replace("#SEARCH_LINK#", link);

            SendEmail(senderEmail, recipientEmailAddress, content, Settings.Default.ShareSearchEmailSubject);
        }

        private static void SendEmail(string senderEmail, string recipientEmailAddress, string content, string subject)
        {

            if (Settings.Default.IsTest)
            {
                recipientEmailAddress = Settings.Default.TestEmail;
            }

            var smtp = new SmtpClient(Settings.Default.SMTPServer);

            var message = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = subject,
                    IsBodyHtml = true,
                    Body = content
                };

            message.To.Add(recipientEmailAddress);

            smtp.Send(message);

        }



        /// <summary>
        /// Get Text from Template
        /// </summary>
        /// <param name="path">Namespace Path value eg. Bso.Archive.BusObj.EmailTemplates</param>
        /// <param name="fileName">Template name eg. ShareSearchResult.htm</param>
        /// <returns></returns>
        /// <remarks>
        /// Get the file from the assembly manifest using path and filename
        /// Since we use reflection, the Template file needs to be added to the project as Embedded Resources
        /// </remarks>
        public static string GetFileText(string path, string fileName)
        {
            string fullPath = String.Concat(path, ".", fileName);

            var typeOfEntity = typeof(Bso.Archive.BusObj.BsoArchiveEntities);

            var assembly = System.Reflection.Assembly.GetAssembly(typeOfEntity);

            Stream stream = assembly.GetManifestResourceStream(fullPath);

            if (stream == null) return String.Empty;

            StreamReader reader = new StreamReader(stream);

            string html = reader.ReadToEnd();

            reader.Close();

            return html;
        }
    }
}
