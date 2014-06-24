using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;

namespace Bso.Archive.BusObj.Utility
{
    public static class EFUtility
    {
        public static object MapType(object value, EdmType type)
        {
            switch (type.ToString())
            {
                case "Edm.Int32":
                    int castedValue;
                    int.TryParse(value.ToString(), out castedValue);
                    return castedValue; 
                case "Edm.DateTime":
                    DateTime castedDate = Convert.ToDateTime(value.ToString());
                    return castedDate;
                case "Edm.DateTimeLong":
                    return value;
                    break;
                case "Edm.Boolean":
                    Boolean castedBoolean = Convert.ToBoolean(value);
                    return castedBoolean;
                default://String
                    return value;
            }
        }

        
    }
}
