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
        public string oauthRefreshToken { get; set; }
        public string oauthBearerToken { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string accountId { get; set; }

        private static APIClient instance;
        private bool isAuthenticated;
        private bool isAuthenticating;
        private object authLock;
        WebHeaderCollection headerCollection;
        HttpClient webClient;
        CookieContainer cookieContainer;
        HttpClientHandler clientHandler;
        HttpMessageHandler messageHandler;

        private const string apiBaseAddress = @"https://my.rightscale.com/";

        private APIClient()
        {
            InitWebClient();
        }

        public void InitWebClient()
        {
            this.isAuthenticated = false;
            this.isAuthenticating = false;
            this.cookieContainer = new CookieContainer();
            this.clientHandler = new HttpClientHandler() { CookieContainer = this.cookieContainer };
            this.webClient = new HttpClient(this.clientHandler);
            //this.webClient.BaseAddress = new Uri(apiBaseAddress);
            this.webClient.DefaultRequestHeaders.Add("X_API_Version", "1.5");
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

        public string Get(string apiHref)
        {
            return Get(apiHref, string.Empty);
        }

        public string Get(string apiHref, string queryStringValue)
        {
            if (CheckAuthenticationStatus())
            {
                string requestUrl = apiBaseAddress.Trim('/') + apiHref;

                if(!string.IsNullOrWhiteSpace(queryStringValue))
                {
                    requestUrl += "?" + queryStringValue;
                }
                
                return webClient.GetAsync(requestUrl).Result.Content.ReadAsStringAsync().Result;
            }
            else
            {
                throw new UnauthorizedAccessException("RightScale API calls could not be authenticated");
            }
            throw new NotImplementedException();
        }

        public bool Put(string putHref, List<KeyValuePair<string, string>> putData)
        {
            if (CheckAuthenticationStatus())
            {
                string putUrl = apiBaseAddress.Trim('/') + putHref;
                if (putData.Count > 0)
                {
                    HttpContent putContent = new FormUrlEncodedContent(putData);
                    HttpResponseMessage response = webClient.PutAsync(putUrl, putContent).Result;
                    response.EnsureSuccessStatusCode();
                    return true;
                }
                else
                {
                    throw new ArgumentException("Cannot PUT to RightScale API without including values in putData collection");
                }
            }
            else
            {
                throw new UnauthorizedAccessException("RightScale API calls could not be authenticated");
            }
            throw new NotImplementedException();
        }

        private string getQueryString(List<KeyValuePair<string, string>> dataForQs)
        {
            string retVal = string.Empty;

            foreach (KeyValuePair<string, string> kvp in dataForQs)
            {
                retVal += kvp.Key + "=" + kvp.Value + "&";
            }
            retVal = retVal.TrimEnd('&');
            return retVal;
        }

        /// <summary>
        /// Centralized method to handle post calls to RightScale API
        /// </summary>
        /// <param name="apiHref">api stub for posting to RightScale API</param>
        /// <param name="parameterSet">List<KeyValuePair<string, string>> of parameters to be posted to RightScale API</param>
        /// <returns>JSON string result to be parsed</returns>
        public List<string> Create(string apiHref, List<KeyValuePair<string, string>> parameterSet, string returnHeaderName)
        {
            if (CheckAuthenticationStatus())
            {
                string content = string.Empty;
                try
                {
                    HttpContent postContent = new FormUrlEncodedContent(parameterSet);
                    string requestUrl = apiBaseAddress.Trim('/') + apiHref;
                    HttpResponseMessage response = webClient.PostAsync(requestUrl, postContent).Result;
                    content = response.Content.ReadAsStringAsync().Result;
                    response.EnsureSuccessStatusCode();
                    return response.Headers.GetValues(returnHeaderName).ToList<string>();
                }
                catch (HttpRequestException hre)
                {
                    Exception ex = new Exception("RSAPI Exception: content" + Environment.NewLine + content, hre);
                    throw ex;
                }
            }
            return null;
        }

        public bool Delete(string apiHref)
        {
            return Delete(apiHref, string.Empty);
        }

        public bool Delete(string apiHref, string queryStringValue)
        {
            if (CheckAuthenticationStatus())
            {
                string requestUrl = apiBaseAddress.Trim('/') + apiHref;

                if(!string.IsNullOrWhiteSpace(queryStringValue))
                {
                    requestUrl += "?" + queryStringValue;
                }
                
                webClient.DeleteAsync(requestUrl);
                return true;
            }
            return false;
        }

        #endregion

        #region Request formatting helpers

        /// <summary>
        /// Static method takes a collection of name/value pairs and creates a properly formatted string representing a set of filters for a given RightScale API call
        /// </summary>
        /// <param name="filterSet">list of key value pairs to be built into a filter string when passing filters to the RightScale API</param>
        /// <returns>properly formatted string for filter collection</returns>
        public static string BuildFilterString(List<KeyValuePair<string, string>> filterSet)
        {
            string retVal = string.Empty;

            foreach (KeyValuePair<string, string> kvp in filterSet)
            {
                retVal += string.Format(@"filter[]=""{0}=={1}""&", kvp.Key, kvp.Value);
            }
            
            retVal = retVal.TrimEnd('&');

            return retVal;
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
        /// Public method takes in oauth bearer token and authenticates the object if a bearer token is passed in.  Ths process assumes that the bearer token is currently valid.
        /// </summary>
        /// <param name="bearerToken">RightScale API Bearer Token</param>
        /// <returns>true if authenticated, false if not</returns>
        public bool SetOauthBearerToken(string bearerToken)
        {
            this.isAuthenticating = true;

            this.oauthBearerToken = bearerToken;
            if (this.webClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                this.webClient.DefaultRequestHeaders.Remove("Authorization");
            }
            this.webClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + this.oauthBearerToken);
            
            this.isAuthenticated = true;
            this.isAuthenticating = false;

            return this.isAuthenticated;
        }

        /// <summary>
        /// Default and base authentication call which pulls authentication data from the app.config or web.config for the specified keys.  OAuth2 token is prioritized in front of username/password/accountid if specified.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Authenticate()
        {
            if (string.IsNullOrWhiteSpace(this.oauthRefreshToken) && ConfigurationManager.AppSettings["RightScaleAPIRefreshToken"] != null)
            {
                this.oauthRefreshToken = ConfigurationManager.AppSettings["RightScaleAPIRefreshToken"].ToString();
            }
            else if (string.IsNullOrWhiteSpace(this.userName) && string.IsNullOrWhiteSpace(this.password) && string.IsNullOrWhiteSpace(this.accountId) && ConfigurationManager.AppSettings["RightScaleAPIUserName"] != null && ConfigurationManager.AppSettings["RightScaleAPIPassword"] != null && ConfigurationManager.AppSettings["RightScaleAPIAccountId"] != null)
            {
                string apiUserName = ConfigurationManager.AppSettings["RightScaleAPIUserName"].ToString();
                string apiPassword = ConfigurationManager.AppSettings["RightScaleAPIPassword"].ToString();
                string apiAccountId = ConfigurationManager.AppSettings["RightScaleAPIAccountId"].ToString();
            }

            if (!string.IsNullOrWhiteSpace(this.oauthRefreshToken))
            {
                return await Authenticate(this.oauthRefreshToken);
            }
            else if (!string.IsNullOrWhiteSpace(this.userName) && !string.IsNullOrWhiteSpace(this.password) && !string.IsNullOrWhiteSpace(this.accountId))
            {
                return await Authenticate(this.userName, this.password, this.accountId);
            }
            else
            {
                throw new NotSupportedException("API Credentials were not found in the application configuration file.  The default/no parameter authentication method can only be used if authentication credentials are set within the aplications app.config or web.config.");
            }
        }

        /// <summary>
        /// Authentication method for http client that uses oAuth2 process for authenticating to RightScale API
        /// </summary>
        /// <param name="oAuthRefreshToken">OAuth2 Token taken from RightScale Dashboard</param>
        /// <returns>true if successfully authenticated, false if not</returns>
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

                HttpResponseMessage response = await webClient.PostAsync(@"https://my.rightscale.com/api/oauth2", postContent);
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();

                dynamic result = SimpleJson.DeserializeObject(content);

                if (result["access_token"] != null)
                {
                    webClient.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", result["access_token"].ToString()));
                    this.oauthBearerToken = result["access_token"].ToString();
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

        /// <summary>
        /// Legacy authentication method using username, password and accountID for authenticating to RightScale API
        /// </summary>
        /// <param name="userName">RightScale login user name</param>
        /// <param name="password">RightScale login password</param>
        /// <param name="accountID">RightScale Account ID to be programmatically accessed</param>
        /// <returns>True if authenticated successfully, false if not</returns>
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
