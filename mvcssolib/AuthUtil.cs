using System.Net.Http;
using System.Threading.Tasks;

namespace SSOLib
{
    public class AuthUtil
    {
       
        const string baseUrl = @"http://www.mvcsso.com/api/Autherize";

        public static AuthUtil Instance
        {
            get
            {
                return new AuthUtil();
            }
        }

        /// <summary>
        /// Retrieves the user using the Token
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public async Task<WebUser> GetUserByToken(string Token)
        {
            HttpClient service = new HttpClient();
            var url = baseUrl + "/GetUserByToken?Token=" + Token;
            HttpResponseMessage response =   service.GetAsync(url).Result;
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<WebUser>();
            }
            return null;
        }

        /// <summary>
        /// Determines whether the current user is logged onto the SSO site
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public async Task<bool> IsUserLoggedIn(string Token)
        {
            HttpClient service = new HttpClient();
            var url = baseUrl + "/IsUserLoggedIn?Token=" + Token;
            HttpResponseMessage response = service.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<bool>();
            }
            return true;
        }

        public async Task<UserStatus> GetUserStauts(string Token, string RequestId)
        {
            HttpClient service = new HttpClient();
            var url = baseUrl + "/GetUserStauts?Token=" + Token + "&RequestId=" + RequestId.ToString();
            HttpResponseMessage response = service.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<UserStatus>();
            }
            return null;
        }

        /// <summary>
        /// Checks whether the redirect ID is expired or not
        /// </summary>
        /// <param name="RedirectId"></param>
        /// <returns></returns>
        public async Task<bool> IsValidRequest(string RequestId)
        {
            HttpClient service = new HttpClient();
            var url = baseUrl + "/IsValidRequest?RequestId=" + RequestId.ToString();
            HttpResponseMessage response = service.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<bool>();
            }
            return true;
        }
      
        public async Task<WebUser> Authenticate(string UserName, string Password)
        {
            HttpClient service = new HttpClient();
            var url = baseUrl + "/Authenticate?username=" + UserName + "&password=" + Password;
            HttpResponseMessage response = service.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<WebUser>();
            }
            return null;
        }
    }
}
