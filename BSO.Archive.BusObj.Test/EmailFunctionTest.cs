using Bso.Archive.BusObj;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bso.Archive.BusObj.Utility;

namespace BSO.Archive.BusObj.Test
{


    /// <summary>
    ///This is a test class for EmailFunctionTest and is intended
    ///to contain all EmailFunctionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EmailFunctionTest
    {
        /// <summary>
        ///A test for GetFileText
        ///</summary>
        [TestMethod()]
        public void GetFileTextTest()
        {
            string path = "Bso.Archive.BusObj.EmailTemplates";

            string filename = "ShareSearchResult.htm";

            string fileContent = EmailFunction.GetFileText(path, filename);

            Assert.IsTrue(fileContent.Contains("#RECIPIENT_NAME"));
        }

        [TestMethod()]
        public void GetFileText_NonExistentFile_Test()
        {
            string path = "Bso.Archive.BusObj.EmailTemplates";

            string filename = "NonExistent.htm";

            string fileContent = EmailFunction.GetFileText(path, filename);

            Assert.IsTrue(string.IsNullOrEmpty(fileContent));
        }

        /// <summary>
        ///A test for ShareSearchResult
        ///</summary>
        [TestMethod()]
        public void ShareSearchResultTest()
        {
            string recipientName = "David Boland";
            string recipientEmailAddress = "dboland@adagetechnologies.com";
            string senderName = "Roshan Tandukar";
            string senderEmail = "rtandukar@adagetechnologies.com";
            string message = "This is a test email";
            string link = "http://www.bso.org";
            EmailFunction.ShareSearchResult(recipientName, recipientEmailAddress, senderName, senderEmail, message, link);
        }
    }
}
