using Bso.Archive.BusObj;
using Bso.Archive.BusObj.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BSO.Archive.BusObj.Test.Editable
{
    [TestClass]
    public class ArtistTest
    {
        [Ignore]
        [TestMethod]
        public void UpdateArtistTest()
        {
            Artist testArtist = Artist.GetArtistByID(-1);
            if (testArtist.IsNew)
            {
                testArtist.ArtistID = -1;
            }
            testArtist.ArtistFirstName = "Adage";
            testArtist.ArtistLastName = "Tech";
            BsoArchiveEntities.Current.Save();

            var artistId = Helper.CreateXElement(Constants.Artist.artistIDElement, "-1");
            var artistFirstName = Helper.CreateXElement(Constants.Artist.artistFirstNameElement, "Test");
            var artistItem = new System.Xml.Linq.XElement("eventArtistItem", artistId, artistFirstName);
            var eventItem = new System.Xml.Linq.XElement("eventItem", artistItem);

            System.Xml.Linq.XDocument doc = new System.Xml.Linq.XDocument(new System.Xml.Linq.XElement(eventItem));

            Artist artist = Artist.NewArtist();
            artist.UpdateData(doc, "ArtistFirstName", "eventArtistFirstname");

            Assert.IsTrue(testArtist.ArtistFirstName == "Test");
            BsoArchiveEntities.Current.DeleteObject(artist);
            BsoArchiveEntities.Current.DeleteObject(testArtist);
            BsoArchiveEntities.Current.Save();
        }

        /// <summary>
        /// Tests GetArtistFromNodeItem by creating an eventItem XElement element and passing it
        /// to the method. Checks that the object was created and with the correct values.
        /// </summary>
        [TestMethod]
        public void GetArtistFromNodeTest()
        {
            var artistId = Helper.CreateXElement(Constants.Artist.artistIDElement, "-1");
            var artistFirstName = Helper.CreateXElement(Constants.Artist.artistFirstNameElement, "TestFName");
            var artistLastName = Helper.CreateXElement(Constants.Artist.artistLastNameElement, "TestLCode");
            var artistNotes = Helper.CreateXElement(Constants.Artist.artistNoteElement, "TestNotes");
            var artistName4 = Helper.CreateXElement(Constants.Artist.artistName4Element, "Test4Name");
            var artistName5 = Helper.CreateXElement(Constants.Artist.artistName5Element, "Test5Name");

            System.Xml.Linq.XElement node = new System.Xml.Linq.XElement(Constants.Artist.artistElement, artistId, artistFirstName, artistLastName, artistNotes, artistName4, artistName5);

            Artist artist = Artist.GetArtistFromNode(node);

            Assert.IsNotNull(artist);
            Assert.IsTrue(artist.ArtistID == -1);
        }
    }
}