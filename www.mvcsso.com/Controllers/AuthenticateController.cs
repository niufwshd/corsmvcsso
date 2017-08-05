using SSOLib;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace www.mvcsso.com.Controllers
{
    public class AuthenticateController : Controller
    {
        string Token = string.Empty;
        string Action = string.Empty;
        string ReturnUrl = string.Empty;

        // GET: Authenticate
        public ActionResult Index()
        {
            LoadRequestParams();

            if (Utility.StringEquals(Action, AppConstants.ParamValues.LOGOUT))
            {
                //A Request paramter value Logout indicates this is a request to log out the current user
                return LogoutUser();
            }
            else
            {
                if (Token != null)
                {
                    //Token is present in URL request. That means, user is authenticated at the client site using the Login screen and it redirected
                    //to the SSO site with the Token in the URL parameter, so set the Authentication Cookie
                    return SetAuthCookie();
                }
                else
                {
                    //User Token is not available in URL. So, check whether the authentication Cookie is available in the Request

                    HttpCookie AuthCookie = Request.Cookies[AppConstants.Cookie.AUTH_COOKIE];

                    if (AuthCookie != null)
                    {
                        //Authentication Cookie is available in Request. So, check whether it is expired or not.
                        //and redirect to appropriate location based upon the cookie status
                        return CheckCookie(AuthCookie, ReturnUrl);
                    }
                    else
                    {
                        //Authentication Cookie is not available in the Request. That means, user is logged out of the system
                        //So, mark user as being logged out 
                        return MarkUserLoggedOut();
                    }
                }
            }
            
        }
        private void LoadRequestParams()
        {
            Token = Request.Params[AppConstants.UrlParams.TOKEN];
            Action = Request.Params[AppConstants.UrlParams.ACTION];
            ReturnUrl = Request.Params[AppConstants.UrlParams.RETURN_URL];
        }

        /// <summary>
        /// Mark user as being logged out of the site
        /// </summary>
        private ActionResult MarkUserLoggedOut()
        {
            //Retrieve the Token from the ReturnUrl (If present)
            Uri uri = new Uri(ReturnUrl);
            NameValueCollection collection = HttpUtility.ParseQueryString(uri.Query);
            if (collection.Count > 0)
            {
                Token = collection[AppConstants.UrlParams.TOKEN];
                //Mark the user's presence as Null in the corresponding user stored in Application scope
                if (!string.IsNullOrEmpty(Token))
                {
                   HttpContext.Application[Token] = null;
                }
            }

            //Redirect user to the originally requested site URL
           return  RedirectEx(ReturnUrl);
        }

        /// <summary>
        /// Set authentication cookie in Response
        /// </summary>
        private ActionResult SetAuthCookie()
        {
            HttpCookie AuthCookie = new HttpCookie(AppConstants.Cookie.AUTH_COOKIE);

            //Set the Cookie's value with Expiry time and Token
            int CookieTimeoutInMinutes = Config.AUTH_COOKIE_TIMEOUT_IN_MINUTES;

            AuthCookie.Value = Utility.BuildCookueValue(Token, CookieTimeoutInMinutes);

            Response.Cookies.Add(AuthCookie);

            //Redirect to the original site request
            ReturnUrl = Utility.GetAppendedQueryString(ReturnUrl, AppConstants.UrlParams.TOKEN, Token);
            return RedirectEx(ReturnUrl);
        }

        /// <summary>
        /// Logs out current user;
        /// </summary>
        private ActionResult LogoutUser()
        {
            //This is a logout request. So, remove the authentication Cookie from the response
            if (Token != null)
            {
                HttpCookie Cookie = Request.Cookies[AppConstants.Cookie.AUTH_COOKIE];

                if (Cookie.Value == Token)
                {
                    RemoveCookie(Cookie);
                }

            }
            //Also, mark the user at the application scope as null
            HttpContext.Application[Token] = null;

            //Redirect user to the desired location
            //ReturnUrl = GetAppendedQueryString(ReturnUrl, AppConstants.UrlParams.ACTION, AppConstants.ParamValues.LOGOUT);
           return RedirectEx(ReturnUrl);
        }


        /// <summary>
        /// If the Cookie is available, check the expiry of the authentication Cookie.
        /// Also, add Token and redirect
        /// </summary>
        /// <param name="AuthCookie"></param>
        /// <param name="ReturnUrl"></param>
        private ActionResult CheckCookie(HttpCookie AuthCookie, string ReturnUrl)
        {
            //The authentication Cookie is available, read the Cookie value
            string Token = Utility.GetCookieValue(AuthCookie);
            DateTime expirytime = Utility.GetExpirationDate(AuthCookie);
            //Check if the Cookie is expired and Token is available

            if (CookieExpired(expirytime))
            {
                //Cookie is expired. So, remove the Cookie from the Response
                RemoveCookie(AuthCookie);
                //Mark the user's presence as Null in the application scope
                HttpContext.Application[Token] = null;
                //Redirect to the site URL
              return RedirectEx(ReturnUrl);

            }
            else
            {
                //Check if Sliding expiration is set to true in web.config
                if (Config.SLIDING_EXPIRATION)
                {
                    //Sliding expiration is set to true. So, increase the expiry time for the Cookie

                    AuthCookie = IncreaseCookieExpiryTime(AuthCookie);
                }
            }
            if (!string.IsNullOrEmpty(Token) && !ReturnUrl.Contains(Token))
            {
                //If Token is present in the QueryString, append it in the return URl and redirect
                ReturnUrl = Utility.GetAppendedQueryString(ReturnUrl, AppConstants.UrlParams.TOKEN, Token);
               return RedirectEx(ReturnUrl);
            }
            else
            {
                //Cookie is expired or Token is not available. So, redirect user to the ReturnUrl.
              return RedirectEx(ReturnUrl);
            }
        }


        /// <summary>
        /// Increases Cookie expiry time
        /// </summary>
        /// <param name="AuthCookie"></param>
        /// <returns></returns>
        private HttpCookie IncreaseCookieExpiryTime(HttpCookie AuthCookie)
        {
            string Token = Utility.GetCookieValue(AuthCookie);
            DateTime Expirytime = Utility.GetExpirationDate(AuthCookie);
            DateTime IncreasedExpirytime = Expirytime.AddMinutes(Config.AUTH_COOKIE_TIMEOUT_IN_MINUTES);

            Response.Cookies.Remove(AuthCookie.Name);

            HttpCookie NewCookie = new HttpCookie(AuthCookie.Name);
            NewCookie.Value = Utility.BuildCookueValue(Token, Config.AUTH_COOKIE_TIMEOUT_IN_MINUTES);

            Response.Cookies.Add(NewCookie);

            return NewCookie;
        }

        /// <summary>
        /// Removes Cookie from the response
        /// </summary>
        /// <param name="Cookie"></param>
        private void RemoveCookie(HttpCookie Cookie)
        {
            Response.Cookies.Remove(Cookie.Name);

            HttpCookie myCookie = new HttpCookie(Cookie.Name);
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(myCookie);
        }

        /// <summary>
        /// Determines whether the Cookie is expired or not
        /// </summary>
        /// <param name="expirytime"></param>
        /// <returns></returns>
        private bool CookieExpired(DateTime expirytime)
        {
            return expirytime.CompareTo(DateTime.Now) < 0;
        }

        /// <summary>
        /// Append a request ID to the URl and redirect
        /// </summary>
        /// <param name="Url"></param>
        private ActionResult RedirectEx(string Url)
        {
            //Generate a new RequestId and append to the Response URL. This is requred so that, the client site can always
            //determine whether the RequestId is originated from the SSO site or not
            string RequestId = Utility.GetGuidHash();
            string redirectUrl = Utility.GetAppendedQueryString(Url, AppConstants.UrlParams.REQUEST_ID, RequestId);

            //Save the RequestId in the Application 
            HttpContext.Application[RequestId] = RequestId;

            return Redirect(redirectUrl);
        }
    }
}