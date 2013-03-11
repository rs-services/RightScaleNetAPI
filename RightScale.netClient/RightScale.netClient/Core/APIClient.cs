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
using System.Dynamic;
using Newtonsoft.Json;

namespace RightScale.netClient.Core
{
    /// <summary>
    /// Singleton instance API Client manages HTTP connections, authentication caching and all REST calls to the RightScale API
    /// </summary>
    public sealed class APIClient : IDisposable
    {

        #region APIClient Properties 

        /// <summary>
        /// RightScale OAuth Refresh token from RightScale dashboard
        /// </summary>
        public string oauthRefreshToken { get; set; }

        /// <summary>
        /// Instance token for authenticating an instance only
        /// </summary>
        public string instanceToken { get; set; }
        
        /// <summary>
        /// RightScale OAuth Bearer Token retrieved when authenticating with oauthRefreshToken
        /// </summary>
        public string oauthBearerToken { get; set; }

        /// <summary>
        /// Rightscale email used for authenticating to the RightScale API with username, password and accountid
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// Corresponding password used for authenticating to the RightScale API with username, password and accountid
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// AccountID used for authenticating to the RightScale API with username, password and accountid
        /// </summary>
        public string accountId { get; set; }

        /// <summary>
        /// Singleton instance implementation of APIClient
        /// </summary>
        private static APIClient instance;

        /// <summary>
        /// boolean indicating if the instance of this object is currently authenticating
        /// </summary>
        private bool isAuthenticated;

        /// <summary>
        /// boolean indicating whether or not this instance has authenticated to the RightScale API gateway
        /// </summary>
        private bool isAuthenticating;
        
        /// <summary>
        /// System.Net.HttpClient object used for executing all HTTP calls to RightScale API
        /// </summary>
        HttpClient webClient;

        /// <summary>
        /// CookieContainer to be added to webClient object for ease of accessibility to determine authentication status in username/password/accountid authenticated users
        /// </summary>
        CookieContainer cookieContainer;
        
        /// <summary>
        /// ClientHandler manages persisting headers from request to request and is used to hold default API header as well as oAuth authentication headers
        /// </summary>
        HttpClientHandler clientHandler;

        /// <summary>
        /// private member to hold the api base address
        /// </summary>
        private string apiBaseAddress;

        #endregion

        /// <summary>
        /// Base constructor initialies http client objects and initializes base url for RightScale API
        /// </summary>
        private APIClient()
        {
            InitWebClient();
            if (ConfigurationManager.AppSettings["RightScaleAPI_BaseAddress"] != null)
            {
                apiBaseAddress = ConfigurationManager.AppSettings["RightScaleAPI_baseAddress"].ToString();
            }
            else
            {
                apiBaseAddress = @"https://my.rightscale.com/";
            }
        }

        /// <summary>
        /// internal method to init web client
        /// </summary>
        public void InitWebClient()
        {
            this.isAuthenticated = false;
            this.isAuthenticating = false;
            this.cookieContainer = new CookieContainer();
            this.clientHandler = new HttpClientHandler() { CookieContainer = this.cookieContainer };
            this.webClient = new HttpClient(this.clientHandler);
            this.webClient.DefaultRequestHeaders.Add("X_API_Version", "1.5");
        }

        /// <summary>
        /// Public instance for singleton access
        /// </summary>
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

        /// <summary>
        /// Public GET process to hit RightScale API
        /// </summary>
        /// <param name="apiHref">Rightscale API Href</param>
        /// <returns>string content from RSAPI</returns>
        internal string Get(string apiHref)
        {
            return Get(apiHref, string.Empty);
        }

        /// <summary>
        /// GET process to hit RightScale API
        /// </summary>
        /// <param name="apiHref">Rightscale API Href</param>
        /// <param name="queryStringValue">query string to append to HTTP GET request</param>
        /// <returns>string content from RSAPI</returns>
        internal string Get(string apiHref, string queryStringValue)
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
                throw new RightScaleAPIException("RSAPI Authentication Error", apiHref, "Call to RightScale API could not be authenticated");
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// PUT wrapper to make calls via HTTP to the RightScale API
        /// </summary>
        /// <param name="putHref">RightScale API Href</param>
        /// <param name="putData">list of keyvaluepairs to serialize and PUT to the RS API</param>
        /// <returns>True if successful, false if not</returns>
        internal bool Put(string putHref, List<KeyValuePair<string, string>> putData)
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
                    throw new RightScaleAPIException("RSAPI Put Error", putHref, "Cannot PUT to RightScale API without including values in putData collection", null, putData);
                }
            }
            else
            {
                throw new RightScaleAPIException("RSAPI Authentication Error", putHref, "Call to RightScale API could not be authenticated", null, putData);
            }
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Centralized method to handle post calls to RightScale API
        /// </summary>
        /// <param name="apiHref">api stub for posting to RightScale API</param>
        /// <param name="parameterSet">List< of KeyValuePair(string, string) of parameters to be posted to RightScale API</param>
        /// <returns>JSON string result to be parsed</returns>
        internal List<string> Create(string apiHref, List<KeyValuePair<string, string>> parameterSet, string returnHeaderName)
        {
            if (CheckAuthenticationStatus())
            {
                string content = string.Empty;
                try
                {
                    if (parameterSet == null)
                    {
                        parameterSet = new List<KeyValuePair<string, string>>();
                    }
                    HttpContent postContent = new FormUrlEncodedContent(parameterSet);
                    string requestUrl = apiBaseAddress.Trim('/') + apiHref;
                    HttpResponseMessage response = webClient.PostAsync(requestUrl, postContent).Result;
                    content = response.Content.ReadAsStringAsync().Result;
                    response.EnsureSuccessStatusCode();
                    return response.Headers.GetValues(returnHeaderName).ToList<string>();
                }
                catch (HttpRequestException hre)
                {
                    throw new RightScaleAPIException(apiHref, content, "Exception from API Gateway, see error data", hre, parameterSet);
                }
            }
            return null;
        }

        /// <summary>
        /// API method to perform a POST request to the RightScale API 
        /// </summary>
        /// <param name="apiHref">API Href fragment corresponding to the API root</param>
        /// <param name="postData">list of keyvaluepair objects to serialize and post to API</param>
        /// <returns>returns true if successful, false if not</returns>
        internal bool Post(string apiHref, List<KeyValuePair<string, string>> postData)
        {
            if (CheckAuthenticationStatus())
            {
                string content = string.Empty;
                try
                {
                    HttpContent postContent = new FormUrlEncodedContent(postData);
                    string postUrl = apiBaseAddress.Trim('/') + apiHref;
                    HttpResponseMessage response = webClient.PostAsync(postUrl, postContent).Result;
                    content = response.Content.ReadAsStringAsync().Result;
                    response.EnsureSuccessStatusCode();
                    return true;
                }
                catch (HttpRequestException hre)
                {
                    throw new RightScaleAPIException("Exception from API Gateway, see error data", apiHref, content, hre, postData);
                }
            }
            return false;
        }

        /// <summary>
        /// API method to perform a POST request to the RightScale API 
        /// </summary>
        /// <param name="apiHref">API Href fragment corresponding to the API root</param>
        /// <returns>returns true if successful, false if not</returns>
        internal bool Post(string apiHref)
        {
            return Post(apiHref, new List<KeyValuePair<string, string>>());
        }

        /// <summary>
        /// API Method to Delete a record within the RightScale system
        /// </summary>
        /// <param name="apiHref">API Href fragment corresponding to the API root</param>
        /// <returns>true if deleted, false if not</returns>
        public bool Delete(string apiHref)
        {
            return Delete(apiHref, string.Empty);
        }

        /// <summary>
        /// API Method to Delete a record within the RightScale system
        /// </summary>
        /// <param name="apiHref">API Href fragment corresponding to the API root</param>
        /// <param name="queryStringValue">query string to append to HTTP GET request</param>
        /// <returns>true if deleted, false if not</returns>
        internal bool Delete(string apiHref, string queryStringValue)
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

        #region Authentication Methods

        /// <summary>
        /// Public method takes in oauth bearer token and authenticates the object if a bearer token is passed in.  Ths process assumes that the bearer token is currently valid.
        /// </summary>
        /// <param name="bearerToken">RightScale API Bearer Token</param>
        /// <returns>true if authenticated, false if not</returns>
        internal bool SetOauthBearerToken(string bearerToken)
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

        public async Task<bool> AuthenticateInstance()
        {
            throw new NotImplementedException();
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
                throw new RightScaleAPIException("API Credentials were not found in the application configuration file.  The default/no parameter authentication method can only be used if authentication credentials are set within the aplications app.config or web.config.");
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

                dynamic result = JsonConvert.DeserializeObject<dynamic>(content);

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

        public void Dispose()
        {
            if (this.webClient != null)
            {
                this.webClient.Dispose();
                this.webClient = null;
            }
            if (this.clientHandler != null)
            {
                this.clientHandler.Dispose();
                this.clientHandler = null;
            }
        }

        #endregion
    }
}
