using Bso.Archive.BusObj.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Bso.Archive.BusObj.Utility;

namespace Bso.Archive.BusObj
{
    public partial class Venue : IOPASData
    {
        public string Location { get { return String.Concat(VenueCity, ", ", VenueState, ", ", VenueCountry); } }

        #region IOPASData

        /// <summary>
        /// Updates the existing database Venue on the column name using the 
        /// XML document parsed using the tagName.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="columnName"></param>
        /// <param name="tagName"></param>
        public void UpdateData(System.Xml.Linq.XDocument doc, string columnName, string tagName)
        {
            IEnumerable<System.Xml.Linq.XElement> eventElements = doc.Descendants(Constants.Event.eventElement);
            foreach (System.Xml.Linq.XElement element in eventElements)
            {
                Venue updateVenue = Venue.GetVenueFromNode(element);

                if (updateVenue == null) continue;

                System.Xml.Linq.XElement venueNode = element.Element(Constants.Venue.venueElement);

                object newValue = (string)venueNode.GetXElement(tagName);

                BsoArchiveEntities.UpdateObject(updateVenue, newValue, columnName);
            }
        }
        #endregion


        /// <summary>
        /// Get Venue object from XmlNode
        /// </summary>
        /// <param name="venueItemNode"></param>
        /// <remarks>
        /// Takes a eventVenue child element from an eventItem and 
        /// gets its child elements to create a Venue object, then returns
        /// that object.
        /// </remarks>
        /// <returns></returns>
        public static Venue GetVenueFromNode(System.Xml.Linq.XElement venueItemNode)
        {
            System.Xml.Linq.XElement venueElement = venueItemNode.Element(Constants.Venue.venueElement);
            if (venueElement == null || string.IsNullOrEmpty((string)venueElement.GetXElement(Constants.Venue.venueIDElement)))
                return null;

            int venueID;
            int.TryParse(venueElement.GetXElement(Constants.Venue.venueIDElement), out venueID);

            Venue venue = Venue.GetVenueByID(venueID);
            if (!venue.IsNew)
                return venue;

            string venueName = (string)venueElement.GetXElement(Constants.Venue.venueNameElement);
            string venueCode = (string)venueElement.GetXElement(Constants.Venue.venueCodeElement);
            string venueCountry = (string)venueElement.GetXElement(Constants.Venue.venueCountryElement);
            string venueCity = (string)venueElement.GetXElement(Constants.Venue.venueCityElement);
            string venueZipCode = (string)venueElement.GetXElement(Constants.Venue.venueZipCodeElement);
            string venueState = (string)venueElement.GetXElement(Constants.Venue.venueStateElement);
            string venueStreet = (string)venueElement.GetXElement(Constants.Venue.venueStreetElement);

            SetVenueData(venue, venueID, venueName, venueCode, venueCountry, venueCity, venueZipCode, venueState, venueStreet);

            return venue;
        }

        /// <summary>
        /// Set the data for a Venue object
        /// </summary>
        /// <remarks>
        /// Takes the variable data of a Venue object and assigns those values to the objects variables.
        /// </remarks>
        /// <param name="venue"></param>
        /// <param name="venueID"></param>
        /// <param name="venueName"></param>
        /// <param name="venueCode"></param>
        /// <param name="venueCountry"></param>
        /// <param name="venueCity"></param>
        /// <param name="venueZipCode"></param>
        /// <param name="venueState"></param>
        /// <param name="venueStreet"></param>
        private static void SetVenueData(Venue venue, int venueID, string venueName, string venueCode, string venueCountry, string venueCity, string venueZipCode, string venueState, string venueStreet)
        {
            venue.VenueID = venueID;
            venue.VenueName = venueName;
            venue.VenueCode = venueCode;
            venue.VenueCountry = venueCountry;
            venue.VenueCity = venueCity;
            venue.VenueZipCode = venueZipCode;
            venue.VenueState = venueState;
            venue.VenueStreet = venueStreet;
        }

        /// <summary>
        /// Checks entity for Venue
        /// </summary>
        /// <remarks>
        /// Checks the entity Venues for a venue by a specific ID. If found return
        /// that venue, otherwise return null;
        /// </remarks>
        /// <param name="venueID"></param>
        /// <returns></returns>
        public static Venue GetVenueByID(int venueID)
        {
            Venue venue = BsoArchiveEntities.Current.Venues.FirstOrDefault(v => v.VenueID == venueID) ??
                Venue.NewVenue();


            return venue;
        }
    }
}
