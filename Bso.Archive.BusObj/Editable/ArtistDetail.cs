using Bso.Archive.BusObj.Interface;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Bso.Archive.BusObj
{
    partial class ArtistDetail
    {

        public static ArtistDetail GetArtistDetailByID(int artistDetailId)
        {
            var artistDetail = BsoArchiveEntities.Current.ArtistDetails.FirstOrDefault(ad => ad.ArtistDetailID == artistDetailId) ?? ArtistDetail.NewArtistDetail();

            return artistDetail;
        }
    }
}
