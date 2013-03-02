using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.IO;
using System.Configuration;
using System.Diagnostics;

namespace RightScale.netClient.Core
{
    public class APIClient
    {

        private static APIClient instance;
        private bool isAuthenticated;
        private bool isAuthenticating;
        private object authLock;
        WebHeaderCollection headerCollection;
        HttpClient webClient;
        CookieContainer cookieContainer;
        HttpClientHandler clientHandler;
        HttpMessageHandler messageHandler;

        private const string apiBaseAddress = @"https://my.rightscale.com/api";

        private APIClient()
        {
            this.isAuthenticated = false;
            this.isAuthenticating = false;
            this.cookieContainer = new CookieContainer();
            this.clientHandler = new HttpClientHandler() { CookieContainer = this.cookieContainer };
            this.webClient = new HttpClient(this.clientHandler);
            //this.webClient.BaseAddress = new Uri(apiBaseAddress);
            this.webClient.DefaultRequestHeaders.Add("X_API_VERSION", "1.5");
        }

        public static APIClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new APIClient();
                }
                return instance;
            }
        }

        #region API Call Wrappers

        public void Get()
        {
            throw new NotImplementedException();
        }

        public void Put()
        {
            throw new NotImplementedException();
        }

        public void Post()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Request formatting helpers

        public static string BuildFilterString(List<KeyValuePair<string, string>> filterSet)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Static method takes a collection of name/value pairs and creates a properly formatted string representing a set of inputs for a server or deployment within the RightScale system
        /// </summary>
        /// <param name="inputSet">list of key value pairs to be built into an input string when passing inputs to the RightScale API</param>
        /// <returns>properly formatted string for input collection</returns>
        public static string BuildInputString(List<KeyValuePair<string, string>> inputSet)
        {
            string retVal = string.Empty;

            foreach (KeyValuePair<string, string> kvp in inputSet)
            {
                retVal += string.Format("inputs[][name]={0}&inputs[][value]={1}&", kvp.Key, kvp.Value);
            }

            retVal = retVal.TrimEnd('&');

            return retVal;
        }

        #endregion

        #region Authentication Methods

        /// <summary>
        /// Default and base authentication call which pulls authentication data from the app.config or web.config for the specified keys.  OAuth2 token is prioritized in front of username/password/accountid if specified.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Authenticate()
        {
            if (ConfigurationManager.AppSettings["RightScaleAPIRefreshToken"] != null)
            {
                return await Authenticate(ConfigurationManager.AppSettings["RightScaleAPIRefreshToken"].ToString());
            }
            else if (ConfigurationManager.AppSettings["RightScaleAPIUserName"]!= null && ConfigurationManager.AppSettings["RightScaleAPIPassword"] != null && ConfigurationManager.AppSettings["RightScaleAPIAccountId"] != null)
            {
                string apiUserName = ConfigurationManager.AppSettings["RightScaleAPIUserName"].ToString();
                string apiPassword = ConfigurationManager.AppSettings["RightScaleAPIPassword"].ToString();
                string apiAccountId = ConfigurationManager.AppSettings["RightScaleAPIAccountId"].ToString();
                return await Authenticate(apiUserName, apiPassword, apiAccountId);
            }
            else
            {
                throw new NotSupportedException("API Credentials were not found in the application configuration file.  The default/no parameter authentication method can only be used if authentication credentials are set within the aplications app.config or web.config.");
            }
        }

        public async Task<bool> Authenticate(string oAuthRefreshToken)
        {
            bool retVal = false;
            if (!this.isAuthenticating)
            {
                this.isAuthenticating = true;

                var postData = new List<KeyValuePair<string, string>>();

                postData.Add(new KeyValuePair<string, string>("grant_type", "refresh_token"));
                postData.Add(new KeyValuePair<string, string>("refresh_token", oAuthRefreshToken));
                HttpContent postContent = new FormUrlEncodedContent(postData);
                HttpResponseHeaders respHeaders;
                

                HttpResponseMessage response = await webClient.PostAsync(@"https://my.rightscale.com/api/oauth2", postContent);
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();

                dynamic result = SimpleJson.DeserializeObject(content);

                if (result["access_token"] != null)
                {
                    webClient.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", result["access_token"].ToString()));
                    this.isAuthenticated = true;
                }
                this.isAuthenticating = false;
            }
            if (this.isAuthenticated)
            {
                retVal = true;
            }
            return retVal;
        }

        public async Task<bool> Authenticate(string userName, string password, string accountID)
        {
            bool retVal = false;

            if (!this.isAuthenticating)
            {
                this.isAuthenticating = true;

                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("email", userName));
                postData.Add(new KeyValuePair<string, string>("password", password));
                postData.Add(new KeyValuePair<string, string>("account_href", string.Format(@"/api/accounts/{0}", accountID)));
                HttpContent postContent = new FormUrlEncodedContent(postData);

                HttpResponseMessage response = await webClient.PostAsync("https://my.rightscale.com/api/session", postContent);
                response.EnsureSuccessStatusCode();

                if (this.cookieContainer.Count > 1)
                {
                    this.isAuthenticated = true;
                }
            }
            if (this.isAuthenticated)
            {
                retVal = true;
            }
            return retVal;
        }

        /// <summary>
        /// Private method used to validate authentication status of singletion instance of API class
        /// </summary>
        /// <returns>true if authenticated, false if not</returns>
        private bool CheckAuthenticationStatus()
        {
            if (!this.isAuthenticated && !this.isAuthenticating)
            {
                return Authenticate().Result;
            }
            else
            {
                return this.isAuthenticated;
            }
        }

        #endregion
    }
}
