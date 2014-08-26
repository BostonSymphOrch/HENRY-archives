using Bso.Archive.BusObj.Interface;
using System.Collections.Generic;
using System.Linq;
using System;
using Bso.Archive.BusObj.Utility;

namespace Bso.Archive.BusObj
{
    partial class Artist : IOPASData
    {
        #region IOPASData

        public void UpdateData(System.Xml.Linq.XDocument doc, string columnName, string tagName)
        {  
            IEnumerable<System.Xml.Linq.XElement> eventElements = doc.Descendants(Constants.Event.eventElement);
            foreach (System.Xml.Linq.XElement element in eventElements)
            {
                var artistElements = element.Descendants(Constants.Artist.artistElement);

                foreach (var artistElement in artistElements)
                {
                    Artist updateArtist = Artist.GetArtistFromNode(artistElement);

                    if (updateArtist == null) continue;

                    object newValue = (string)artistElement.GetXElement(tagName);

                    BsoArchiveEntities.UpdateObject(updateArtist, newValue, columnName);
                }
            }
        }

        #endregion

        /// <summary>
        /// Get Artist object from XElement node
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// Takes an XElement node of eventItem and extracts the data for that events 
        /// Artist element. Checks to see if that Artist already exists, if yes than return, 
        /// if no then create a new Artist from data in node.
        /// </remarks>
        /// <returns></returns>
        public static Artist GetArtistFromNode(System.Xml.Linq.XElement node)
        {
            if (node == null || node.Element(Constants.Artist.artistIDElement) == null)
                return null;

            int artistId = 0;
            var artistElement = node.GetXElement(Constants.Artist.artistIDElement);
            int.TryParse(artistElement, out artistId);

            Artist artist = Artist.GetArtistByID(artistId);
            if (!artist.IsNew)
                return artist;

            int artistStatusId, artistStatus;
            string artistFirstName = (string)node.GetXElement(Constants.Artist.artistFirstNameElement);
            string artistLastName = (string)node.GetXElement(Constants.Artist.artistLastNameElement);
            string artistName4 = (string)node.GetXElement(Constants.Artist.artistName4Element);
            string artistName5 = (string)node.GetXElement(Constants.Artist.artistName5Element);
            string artistNote = (string)node.GetXElement(Constants.Artist.artistNoteElement);

            int.TryParse((string)node.GetXElement(Constants.Artist.artistStatusElement), out artistStatus);
            int.TryParse((string)node.GetXElement(Constants.Artist.artistStatusIDElement), out artistStatusId);


            artist = SetArtistData(artist, artistId, artistFirstName, artistLastName, artistName4, artistName5, artistNote, artistStatus, artistStatusId);

            return artist;
        }

        private static Artist SetArtistData(Artist artist, int artistId, string artistFirstName, string artistLastName, string artistName4, string artistName5, string artistNote, int artistStatus, int artistStatusId)
        {
            artist.ArtistID = artistId;
            artist.ArtistStatus = artistStatus;
            artist.ArtistStatusID = artistStatusId;
            artist.ArtistFirstName = artistFirstName;
            artist.ArtistLastName = artistLastName;
            artist.ArtistNote = artistNote;
            artist.ArtistName4 = artistName4;
            artist.ArtistName5 = artistName5;
            return artist;
        }

        /// <summary>
        /// Return artist based on id
        /// </summary>
        /// <param name="artistId"></param>
        /// <remarks>
        /// Takes an int id and checks to see if an artist with that id already exists in the entity. 
        /// If it does then return it. Otherwise create a new instance of Artist and return it.
        /// </remarks>
        /// <returns></returns>
        public static Artist GetArtistByID(int artistId)
        {
            Artist artist = BsoArchiveEntities.Current.Artists.FirstOrDefault(a => a.ArtistID == artistId) ??
                            Artist.NewArtist();

            return artist;
        }



        public string ArtistFullName { 
            get 
            {
                return string.Concat(ArtistFirstName, " ", ArtistLastName);
            } 
        }
    }
}
