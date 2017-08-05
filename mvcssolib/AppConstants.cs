using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSOLib
{
    /// <summary>
    /// Summary description for AppConstants
    /// </summary>
    public class AppConstants
    {
        public AppConstants()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Url Parameter constant class
        /// </summary>
        public class UrlParams
        {
            public const string TOKEN = "Token";
            public const string ACTION = "Action";
            public const string REQUEST_ID = "RequestId";
            public const string RETURN_URL = "ReturnUrl";
        }

        public class Cookie
        {
            public const string AUTH_COOKIE = "AUTH_COOKIE";
        }

        public class ParamValues
        {
            public const string LOGOUT = "Logout";
        }

        public class Urls
        {
            public const string DEFAULT_URL = "DEFAULT_URL";
            public const string LOGIN_URL = "LOGIN_URL";
            public const string SSO_SITE_URL = "SSO_SITE_URL";
        }

        public class SessionParams
        {
            public const string DO_NOT_REDIRECT = "DO_NOT_REDIRECT";
        }
    
    }
}