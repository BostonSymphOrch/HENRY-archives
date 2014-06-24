
namespace Adage.EF.BusObj
{

    /// <summary>
    /// This interface is used to provide centralized field level security mapping
    /// </summary>
    public interface IFieldSecurity
    {
        /// <summary>
        /// This method determines if the current user can read a field
        /// </summary>
        /// <param name="currObject">Object name to check security for</param>
        /// <param name="fieldName">Field name to check security on</param>
        /// <returns></returns>
        bool FieldReadAccess(Adage.EF.Interfaces.IGenericBusinessObj currObject, string fieldName);

        /// <summary>
        /// This method determines if the current user can write a field
        /// </summary>
        /// <param name="currObject">Object name to check security for</param>
        /// <param name="fieldName">Field name to check security on</param>
        /// <returns></returns>
        bool FieldWriteAccess(Adage.EF.Interfaces.IGenericBusinessObj currObject, string fieldName);
    }

    /// <summary>
    /// Default implemenatation of the ILocalized Time class
    /// </summary>
    public class FieldSecurity : IFieldSecurity
    {
        /// <summary>
        /// The string to be used in a session variable to use during time conversion
        /// </summary>
        public const string FieldSecuritySessionStr = "Adage.EF.BusObj.FieldSecuritySessionStr";

        /// <summary>
        /// The default instance of the local time conversion
        /// </summary>
        private static IFieldSecurity LocalFieldSecuritySingleton = new FieldSecurity();

        /// <summary>
        /// Gets the current security object for field level security
        /// </summary>
        public static IFieldSecurity CurrentSecurity
        {
            get
            {
                if (System.Web.HttpContext.Current == null
                    || System.Web.HttpContext.Current.Session == null
                    ||
                    (System.Web.HttpContext.Current.Session[FieldSecuritySessionStr] == null
                        && System.Web.HttpContext.Current.Items[FieldSecuritySessionStr] == null))
                {
                    return LocalFieldSecuritySingleton;
                }
                else
                {
                    if (System.Web.HttpContext.Current.Items[FieldSecuritySessionStr] != null)
                        return (IFieldSecurity)System.Web.HttpContext.Current.Items[FieldSecuritySessionStr];

                    return (IFieldSecurity)System.Web.HttpContext.Current.Session[FieldSecuritySessionStr];
                }
            }
        }

        /// <summary>
        /// This method allows for changing the default security object for test purposes
        /// </summary>
        /// <param name="newSecurityObj">Field Security object that will handle field level security</param>
        public void SetupUnitTestSecurityObject(IFieldSecurity newSecurityObj)
        {
            LocalFieldSecuritySingleton = newSecurityObj;
        }

        /// <summary>
        /// This method determines if the current user can read a field
        /// </summary>
        /// <param name="currObject"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public bool FieldReadAccess(Adage.EF.Interfaces.IGenericBusinessObj currObject, string fieldName)
        {
            return true;
        }

        /// <summary>
        /// This method determines if the current user can write a field
        /// </summary>
        /// <param name="currObject"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public bool FieldWriteAccess(Adage.EF.Interfaces.IGenericBusinessObj currObject, string fieldName)
        {
            return true;
        }
    }
}
