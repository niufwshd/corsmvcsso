using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AppConstants
/// </summary>
public partial class AppConstants
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

    /// <summary>
    /// Constant class for Cookie
    /// </summary>
    public class Cookie
    {
        public const string AUTH_COOKIE = "AUTH_COOKIE";
        public const string SLIDING_EXPIRATION = "SLIDING_EXPIRATION";
    }

    /// <summary>
    /// Constant class for Parameter values
    /// </summary>
    public class ParamValues
    {
        public const string LOGOUT = "Logout";
    }

    /// <summary>
    /// Constant class for Configuration keys
    /// </summary>
    public class Configuration
    {
        public const string AUTH_COOKIE_TIMEOUT_IN_MINUTES = "AUTH_COOKIE_TIMEOUT_IN_MINUTES";
        public const string SLIDING_EXPIRATION = "SLIDING_EXPIRATION";
        
    }

    /// <summary>
    /// Constant class for Session keys
    /// </summary>
    public class SessionParams
    {
        public const string DO_NOT_REDIRECT = "DO_NOT_REDIRECT";
    }
    
}