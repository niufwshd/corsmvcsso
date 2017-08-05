using System.Web.Http;
using System.Web;

namespace www.mvcsso.com.Controllers
{
    public class AutherizeController : ApiController
    {
        [HttpGet]
        public string Value()
        {
            return "hello";
        }

        /// <summary>
        /// Authenticates user by UserName and Password
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public WebUser Authenticate(string UserName, string Password)
        {
            WebUser user = UserManager.AuthenticateUser(UserName, Password);
            if (user != null)
            {
                //Store the user object in the Application scope, to mark the user as logged onto the SSO site
                //Along with the cookie, this is a supportive way to trak user's logged in status
                //In order to track a user as logged onto the SSO site user token has to be presented in the cookie as well as 
                //he/she has to be presented in teh Application scope
                HttpContext.Current.Application[user.Token] = user;
            }
            return user;

        }

        /// <summary>
        /// Retrieves user by UniqueId
        /// </summary>
        /// <param name="UniqueId"></param>
        /// <returns></returns>
        [HttpGet]
        public WebUser GetUserByUniqueId(string UniqueId)
        {
            return UserManager.GetWebUserByUniqueId(UniqueId);
        }

        /// <summary>
        /// Retrieves user by Token
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [HttpGet]
        public WebUser GetUserByToken(string Token)
        {
            if (HttpContext.Current.Application[Token] == null)
            {
                return null;
            }
            return HttpContext.Current.Application[Token] as WebUser;
        }

        /// <summary>
        /// Determines whether user is still logged onto the site
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public bool IsUserLoggedIn(string Token)
        {
            return HttpContext.Current.Application[Token] == null ? false : true;
        }

        /// <summary>
        /// Determines whether the current request is valid or not
        /// </summary>
        /// <param name="RedirectId"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public bool IsValidRequest(string RedirectId)
        {
            if ((string) HttpContext.Current.Application[RedirectId] == RedirectId)
            {
                HttpContext.Current.Application[RedirectId] = null;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether the current request is valid or not
        /// </summary>
        /// <param name="RedirectId"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public UserStatus GetUserStauts(string Token, string RequestId)
        {
            UserStatus userStatus = new UserStatus();

            if (!string.IsNullOrEmpty(RequestId))
            {
                if ((string)HttpContext.Current.Application[RequestId] == RequestId)
                {
                    HttpContext.Current.Application[RequestId] = null;
                    userStatus.RequestIdValid = true;
                }
            }
            userStatus.UserLoggedIn = HttpContext.Current.Application[Token] == null ? false : true;
            return userStatus;
        }

    }
}

