using System;
using System.Web;

namespace SSOLib
{
    /// <summary>
    /// Summary description for SessionAPI
    /// </summary>
    public class SessionAPI
    {
        public SessionAPI()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static bool RequestRedirectFlag
        {
            get
            {
                if (HttpContext.Current.Session[AppConstants.SessionParams.DO_NOT_REDIRECT] == null) return true;
                return Convert.ToBoolean(HttpContext.Current.Session[AppConstants.SessionParams.DO_NOT_REDIRECT]);
            }
            set
            {
                HttpContext.Current.Session[AppConstants.SessionParams.DO_NOT_REDIRECT] = value;
            }
        }

        public static void ClearRedirectFlag()
        {
            HttpContext.Current.Session.Remove(AppConstants.SessionParams.DO_NOT_REDIRECT);
        }


        public static WebUser CurrentUser { get; set; }

        public static bool IsPostBack { get; set; }
    }
}