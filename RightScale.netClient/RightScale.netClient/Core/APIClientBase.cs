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
using System.Timers;

namespace RightScale.netClient.Core
{
    /// <summary>
    /// Singleton instance API Client manages HTTP connections, authentication caching and all REST calls to the RightScale API
    /// </summary>
    public abstract class APIClientBase : IDisposable
    {
        /// <summary>
        /// Timer to handle automatically refreshing the httpclient objects on a schedule inside of the API timeout window
        /// </summary>
        private Timer authTimer;

        /// <summary>
        /// Valid API Version values
        /// </summary>
        private List<string> validAPIVersions = new List<string>() { "1.0", "1.5" };

        #region APIClient Properties 
        
        /// <summary>
        /// private variable to hold value of this.apiVersion 
        /// </summary>
        private string _apiVersion;

        /// <summary>
        /// API Version being accessed - default is 1.5
        /// </summary>
        public string apiVersion
        {
            get
            {
                return _apiVersion;
            }
            set
            {
                if (validAPIVersions.Contains(value))
                {
                    this._apiVersion = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Value for apiVersion is not a valid string");
                }
            }
        }

        /// <summary>
        /// Boolean indicating that this session is an instance-facing session rather than a fully-fledged session.  Instance sessions can only utilize a limited portion of the API.
        /// </summary>
        public bool isInstanceAuthenticated { get; set; }

        /// <summary>
        /// RightScale Instance token from the given RS instance
        /// </summary>
        public string instanceToken { get; set; }

        /// <summary>
        /// RightScale OAuth Refresh token from RightScale dashboard
        /// </summary>
        public string oauthRefreshToken { get; set; }
        
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
        private HttpClient webClient;

        /// <summary>
        /// CookieContainer to be added to webClient object for ease of accessibility to determine authentication status in username/password/accountid authenticated users
        /// </summary>
        private CookieContainer cookieContainer;
        
        /// <summary>
        /// ClientHandler manages persisting headers from request to request and is used to hold default API header as well as oAuth authentication headers
        /// </summary>
        private HttpClientHandler clientHandler;

        /// <summary>
        /// public property to hold the api base address - must be changed when connecting to an account on a specific RigthScale shard
        /// </summary>
        private string apiBaseAddress;
        
        /// <summary>
        /// Authentication timeout in minutes - determines when API Client will reset its WebClient object proactively to force another authentication attempt
        /// </summary>
        public int authTimeoutMins { get; set; }

        #endregion

        #region APIClientBase client init processes
        
        /// <summary>
        /// internal method to init web client
        /// </summary>
        public void InitWebClient()
        {
            this.isAuthenticated = false;
            this.isAuthenticating = false;
            this.isInstanceAuthenticated = false;
            this.cookieContainer = new CookieContainer();
            this.clientHandler = new HttpClientHandler() { CookieContainer = this.cookieContainer, AllowAutoRedirect = false };
            this.webClient = new HttpClient(this.clientHandler);

            if (string.IsNullOrWhiteSpace(this.apiBaseAddress) && ConfigurationManager.AppSettings["RightScaleAPI_BaseAddress"] != null)
            {
                this.apiBaseAddress = ConfigurationManager.AppSettings["RightScaleAPI_BaseAddress"].ToString();
            }
            else if (string.IsNullOrWhiteSpace(this.apiBaseAddress))
            {
                this.apiBaseAddress = @"https://my.rightscale.com";
            }

            if (this.authTimeoutMins > 0 && ConfigurationManager.AppSettings["RightScaleAPI_AuthTimeoutMins"] != null && Utility.CheckStringIsNumeric(ConfigurationManager.AppSettings["RightScaleAPI_AuthTimeoutMins"].ToString()))
            {
                this.authTimeoutMins = int.Parse(ConfigurationManager.AppSettings["RightScaleAPI_AuthTimeoutMins"].ToString());
            }
            else if (this.authTimeoutMins < 1)
            {
                this.authTimeoutMins = 118;
            }

            if (string.IsNullOrEmpty(this.oauthRefreshToken) && ConfigurationManager.AppSettings["RightScaleAPI_AuthRefreshToken"] != null)
            {
                this.oauthRefreshToken = ConfigurationManager.AppSettings["RightScaleAPI_AuthRefreshToken"].ToString();
            }
            
            if (string.IsNullOrEmpty(this.userName) && ConfigurationManager.AppSettings["RightScaleAPI_AuthUserName"] != null)
            {
                this.userName = ConfigurationManager.AppSettings["RightScaleAPI_AuthUserName"].ToString();
            }
            
            if (string.IsNullOrEmpty(this.password) && ConfigurationManager.AppSettings["RightScaleAPI_AuthPassword"] != null)
            {
                this.password = ConfigurationManager.AppSettings["RightScaleAPI_AuthPassword"].ToString();
            }
            
            if (string.IsNullOrEmpty(this.accountId) && ConfigurationManager.AppSettings["RightScaleAPI_AuthAccountId"] != null)
            {
                this.accountId = ConfigurationManager.AppSettings["RightScaleAPI_AuthAccountId"].ToString();
            }

            if (string.IsNullOrWhiteSpace(this.apiVersion) && ConfigurationManager.AppSettings["RightScaleAPI_ApiVersion"] != null)
            {
                this.apiVersion = ConfigurationManager.AppSettings["RightScaleAPI_ApiVersion"].ToString();
            }
            else if (string.IsNullOrWhiteSpace(this.apiVersion))
            {
                this.apiVersion = "1.5";
            }

            this.webClient.DefaultRequestHeaders.Add("X_API_Version", this.apiVersion);
        }

        /// <summary>
        /// Public method to initialize the API caller specifically for an account that's on a distinct RightScale shard
        /// </summary>
        /// <param name="shardBaseUrl">base url of the shard the account is associated with</param>
        public void InitWebClient(string shardBaseUrl)
        {
            InitWebClient();
            this.apiBaseAddress = shardBaseUrl.TrimEnd('/');
        }

        #endregion

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
            if (string.IsNullOrWhiteSpace(apiHref))
            {
                return string.Empty;
            }
            else
            {
                if (CheckAuthenticationStatus())
                {
                    string requestUrl = apiBaseAddress.Trim('/') + apiHref;

                    if (!string.IsNullOrWhiteSpace(queryStringValue))
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
        }

        /// <summary>
        /// PUT wrapper to make calls via HTTP to the RightScale API
        /// </summary>
        /// <param name="putHref">RightScale API Href</param>
        /// <param name="putData">list of keyvaluepairs to serialize and PUT to the RS API</param>
        /// <returns>True if successful, false if not</returns>
        internal bool Put(string putHref, List<KeyValuePair<string,string>> putData)
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
        /// <param name="returnHeaderName">Name of the header whose content to return</param>
        /// <param name="contentOutput">Output parameter containing the content of this POST call</param>
        /// <returns>JSON string result to be parsed</returns>
        internal List<string> Post(string apiHref, List<KeyValuePair<string, string>> parameterSet, string returnHeaderName, out string contentOutput)
        {
            contentOutput = string.Empty;
            if (CheckAuthenticationStatus())
            {
                string content = string.Empty;
                try
                {
                    if (parameterSet == null)
                    {
                        parameterSet = new List<KeyValuePair<string,string>>();
                    }
                    HttpContent postContent = new FormUrlEncodedContent(parameterSet);
                    string requestUrl = apiBaseAddress.Trim('/') + apiHref;
                    HttpResponseMessage response = webClient.PostAsync(requestUrl, postContent).Result;
                    content = response.Content.ReadAsStringAsync().Result;
                    
                    if (!string.IsNullOrWhiteSpace(content))
                    {
                        contentOutput = content;
                    }
                    else
                    {
                        contentOutput = string.Empty;
                    }

                    response.EnsureSuccessStatusCode();
                    if (!string.IsNullOrWhiteSpace(returnHeaderName))
                    {
                        return response.Headers.GetValues(returnHeaderName).ToList<string>();
                    }
                }
                catch (HttpRequestException hre)
                {
                    throw new RightScaleAPIException(apiHref, content, "Exception from API Gateway, see error data", hre, parameterSet);
                }
            }
            return null;
        }

        /// <summary>
        /// Override to Post method without an output string 
        /// </summary>
        /// <param name="apiHref">api stub for posting to RightScale API</param>
        /// <param name="parameterSet">List< of KeyValuePair(string, string) of parameters to be posted to RightScale API</param>
        /// <param name="returnHeaderName">Name of the header whose content to return</param>
        /// <returns>JSON string result to be parsed</returns>
        internal List<string> Post(string apiHref, List<KeyValuePair<string, string>> parameterset, string returnHeaderName)
        {
            string outString = string.Empty;
            return Post(apiHref, parameterset, returnHeaderName, out outString);
        }

        /// <summary>
        /// API method to perform a POST request to the RightScale API 
        /// </summary>
        /// <param name="apiHref">API Href fragment corresponding to the API root</param>
        /// <param name="postData">list of keyvaluepair objects to serialize and post to API</param>
        /// <returns>returns true if successful, false if not</returns>
        internal bool Post(string apiHref, List<KeyValuePair<string, string>> postData)
        {
            if (Post(apiHref, postData, string.Empty) == null)
            {
                return true;
            }
            else
            {
                return false;
            }
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
        /// API method to perform a POST request to the RightScale API
        /// </summary>
        /// <param name="apiHref">API href fragment corresponding to the API root</param>
        /// <param name="headerName">header to return if there is content</param>
        /// <param name="outString">content of the header to return</param>
        /// <returns>true if successful, false if not</returns>
        internal bool Post(string apiHref, string headerName, out string outString)
        {
            if (Post(apiHref, new List<KeyValuePair<string, string>>(), headerName, out outString) == null)
            {
                return true;
            }
            else
            {
                outString = string.Empty;
                return false;
            }
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
                HttpResponseMessage response = webClient.DeleteAsync(requestUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    throw new RightScaleAPIException("Object failed to delete with status code " + response.StatusCode.ToString(), apiHref, response.Content.ReadAsStringAsync().Result);
                }
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

        /// <summary>
        /// Default and base authentication call which pulls authentication data from the app.config or web.config for the specified keys.  OAuth2 token is prioritized in front of username/password/accountid if specified.
        /// </summary>
        /// <returns></returns>
        public bool Authenticate()
        {
            if (!this.isAuthenticated)
            {
                bool authSuccessful = false;

                if (string.IsNullOrWhiteSpace(this.oauthRefreshToken) && ConfigurationManager.AppSettings["RightScaleAPI_AuthRefreshToken"] != null)
                {
                    this.oauthRefreshToken = ConfigurationManager.AppSettings["RightScaleAPI_AuthRefreshToken"].ToString();
                    string apiAccountId = ConfigurationManager.AppSettings["RightScaleAPI_AuthAccountId"].ToString();
                }
                else if (string.IsNullOrWhiteSpace(this.userName) && string.IsNullOrWhiteSpace(this.password) && string.IsNullOrWhiteSpace(this.accountId) && ConfigurationManager.AppSettings["RightScaleAPI_AuthUserName"] != null && ConfigurationManager.AppSettings["RightScaleAPI_AuthPassword"] != null && ConfigurationManager.AppSettings["RightScaleAPI_AuthAccountId"] != null)
                {
                    string apiUserName = ConfigurationManager.AppSettings["RightScaleAPI_AuthUserName"].ToString();
                    string apiPassword = ConfigurationManager.AppSettings["RightScaleAPI_AuthPassword"].ToString();
                    string apiAccountId = ConfigurationManager.AppSettings["RightScaleAPI_AuthAccountId"].ToString();
                }

                if (!string.IsNullOrWhiteSpace(this.oauthRefreshToken))
                {
                    authSuccessful = Authenticate(this.oauthRefreshToken);
                }
                else if (!string.IsNullOrWhiteSpace(this.userName) && !string.IsNullOrWhiteSpace(this.password) && !string.IsNullOrWhiteSpace(this.accountId))
                {
                    authSuccessful = Authenticate(this.userName, this.password, this.accountId);
                }
                else
                {
                    throw new RightScaleAPIException("API Credentials were not found in the application configuration file.  The default/no parameter authentication method can only be used if authentication credentials are set within the aplications app.config or web.config.");
                }
                if (authSuccessful)
                {
                    InitAuthTimer();
                }
            }
            return this.isAuthenticated;
        }

        /// <summary>
        /// Method manages centralized logic for initializing the proactive authentication timeout process
        /// </summary>
        private void InitAuthTimer()
        {
            authTimer = new Timer((double)(authTimeoutMins * 60 * 1000)); // 118 mins to account for a 120 min session timeout
            authTimer.AutoReset = false;
            authTimer.Elapsed += authTimer_Elapsed;
            authTimer.Start();
        }

        /// <summary>
        /// Tick method for AuthTimer resets authentication state of singleton api caller
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void authTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            authTimer.Stop();
            this.InitWebClient();
            authTimer.Dispose();
        }

        /// <summary>
        /// Authentication process for instance-based RSAPI authentication
        /// </summary>
        /// <param name="api_instance_token">$env:RS_API_TOKEN value</param>
        /// <returns>true if authenticated, false if not</returns>
        public bool Authenticate_Instance(string api_instance_token)
        {
            string[] instanceTokenSplit = api_instance_token.Split(':');
            if (instanceTokenSplit.Length != 2)
            {
                throw new ArgumentException("api_instance_token was not well formed.");
            }

            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("account_href", Utility.accountHref(instanceTokenSplit[0])));
            postData.Add(new KeyValuePair<string, string>("instance_token", instanceTokenSplit[1]));
            HttpContent postContent = new FormUrlEncodedContent(postData);
            HttpResponseMessage responseMessage = webClient.PostAsync(this.apiBaseAddress + APIHrefs.SessionInstance, postContent).Result;
            
            if (responseMessage.IsSuccessStatusCode)
            {
                if (this.cookieContainer.Count > 1)
                {
                    this.isAuthenticated = true;
                    this.isInstanceAuthenticated = true;
                    InitAuthTimer();
                }
            }
            else
            {
                this.isAuthenticated = false;
                this.isInstanceAuthenticated = false;
            }

            return this.isAuthenticated;
        }

        /// <summary>
        /// Authentication method for http client that uses oAuth2 process for authenticating to RightScale API
        /// </summary>
        /// <param name="oAuthRefreshToken">OAuth2 Token taken from RightScale Dashboard</param>
        /// <param name="accountID">Account ID used for API 1.0 calls</param>
        /// <returns>true if successfully authenticated, false if not</returns>
        public bool Authenticate(string oAuthRefreshToken, string accountID)
        {
            this.accountId = accountID;
            return Authenticate(oAuthRefreshToken);
        }

        /// <summary>
        /// Private helper method gets proper Oauth api href path
        /// </summary>
        /// <returns>Properly formatted oauth api href path</returns>
        private string getOauthHrefPath()
        {
            switch (this.apiVersion)
            {
                case "1.0":
                    if (!string.IsNullOrWhiteSpace(this.accountId))
                    {
                        return string.Format(APIHrefs.OAuthAPI10, this.accountId);
                    }
                    else
                    {
                        throw new ArgumentNullException("AccountID must be specified for OAuth requests to RightScale API");
                    }
                case "1.5":
                    return APIHrefs.OAuthAPI15;
                default:
                    throw new NotImplementedException("API Version " + this.apiVersion + " does not exist or has not yet been implemented");
            }
        }

        /// <summary>
        /// Authentication method for http client that uses oAuth2 process for authenticating to RightScale API
        /// </summary>
        /// <param name="oAuthRefreshToken">OAuth2 Token taken from RightScale Dashboard</param>
        /// <returns>true if successfully authenticated, false if not</returns>
        public bool Authenticate(string oAuthRefreshToken)
        {
            this.oauthRefreshToken = oAuthRefreshToken;
            if (!this.isAuthenticated)
            {
                if (!this.isAuthenticating)
                {
                    this.isAuthenticating = true;

                    var postData = new List<KeyValuePair<string, string>>();

                    postData.Add(new KeyValuePair<string, string>("grant_type", "refresh_token"));
                    postData.Add(new KeyValuePair<string, string>("refresh_token", oAuthRefreshToken));
                    HttpContent postContent = new FormUrlEncodedContent(postData);

                    HttpResponseMessage  responseMessage = webClient.PostAsync(this.apiBaseAddress + getOauthHrefPath(), postContent).Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        string content = responseMessage.Content.ReadAsStringAsync().Result;

                        dynamic result = JsonConvert.DeserializeObject<dynamic>(content);

                        if (result["access_token"] != null)
                        {
                            webClient.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", result["access_token"].ToString()));
                            this.oauthBearerToken = result["access_token"].ToString();
                            this.isAuthenticated = true;
                            InitAuthTimer();
                        }
                    }
                    else
                    {
                        this.isAuthenticated = false;
                    }
                }
            }

            this.isAuthenticating = false;
            return this.isAuthenticated;
        }

        /// <summary>
        /// Public method resets the API base address (shard)
        /// If it is changing, this will reinitialize the web client and force authentication again
        /// </summary>
        /// <param name="apiBaseUrl">Base URL to use for accessing RightScale API</param>
        public void setAPIBaseAddress(string apiBaseUrl)
        {
            if (this.apiBaseAddress != apiBaseUrl.Trim('/'))
            {
                InitWebClient(apiBaseUrl);
            }
        }

        /// <summary>
        /// Protected helper method for setting api version of the client
        /// </summary>
        /// <param name="apiVer">String representing the API version to be used</param>
        protected void setAPIVersion(string apiVer)
        {
            if (this.apiVersion != apiVer)
            {
                this.apiVersion = apiVer;
                InitWebClient();
            }
        }

        /// <summary>
        /// Legacy authentication method using username, password and accountID for authenticating to RightScale API
        /// </summary>
        /// <param name="userName">RightScale login user name</param>
        /// <param name="password">RightScale login password</param>
        /// <param name="accountID">RightScale Account ID to be programmatically accessed</param>
        /// <param name="baseUrl">Distinct base URL for calling RightScale API</param>
        /// <returns>True if authenticated successfully, false if not</returns>
        public bool Authenticate(string userName, string password, string accountID, string baseUrl)
        {
            setAPIBaseAddress(baseUrl);
            return Authenticate(userName, password, accountID);
        }


        /// <summary>
        /// Legacy authentication method using username, password and accountID for authenticating to RightScale API
        /// </summary>
        /// <param name="userName">RightScale login user name</param>
        /// <param name="password">RightScale login password</param>
        /// <param name="accountID">RightScale Account ID to be programmatically accessed</param>
        /// <returns>True if authenticated successfully, false if not</returns>
        public bool Authenticate(string userName, string password, string accountID)
        {
            if (!this.isAuthenticated)
            {
                if (!this.isAuthenticating)
                {
                    this.isAuthenticating = true;

                    this.userName = userName;
                    this.password = password;
                    this.accountId = accountID;

                    var postData = new List<KeyValuePair<string, string>>();
                    HttpResponseMessage response;
                    switch (this.apiVersion)
                    {
                        case "1.0":
                            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, this.apiBaseAddress + string.Format(APIHrefs.API10Login, this.accountId));
                            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", this.userName, this.password))));
                            response = webClient.SendAsync(requestMessage).Result;
                            break;
                        case "1.5":
                            postData.Add(new KeyValuePair<string, string>("email", userName));
                            postData.Add(new KeyValuePair<string, string>("password", password));
                            postData.Add(new KeyValuePair<string, string>("account_href", string.Format(@"/api/accounts/{0}", accountID)));
                            HttpContent postContent = new FormUrlEncodedContent(postData);
                            response = webClient.PostAsync(this.apiBaseAddress + APIHrefs.Session, postContent).Result;
                            break;
                        default:
                            throw new NotImplementedException("API Version " + this.apiVersion + " does not exist or has not yet been implemented");
                    }

                    
                    if (response.StatusCode == HttpStatusCode.Found)
                    {
                        if (response.Headers.Contains("location"))
                        {
                            List<string> locations = response.Headers.GetValues("location").ToList<string>();
                            response.Dispose();
                            this.isAuthenticating = false;
                            if (locations.Count == 1)
                            {
                                Uri newBaseUri = new Uri(locations[0]);
                                string newBaseUrl = newBaseUri.AbsoluteUri.Replace(newBaseUri.AbsolutePath, string.Empty);
                                Authenticate(userName, password, accountID, newBaseUrl);
                            }
                        }
                    }
                    else if (response.IsSuccessStatusCode)
                    {
                        if (this.cookieContainer.Count > 1)
                        {
                            this.isAuthenticated = true;
                            InitAuthTimer();
                        }
                    }
                    else
                    {
                        this.isAuthenticated = false;
                    }
                }
            }
            this.isAuthenticating = false;
            return this.isAuthenticated;
        }

        /// <summary>
        /// Private method used to validate authentication status of singletion instance of API class
        /// </summary>
        /// <returns>true if authenticated, false if not</returns>
        private bool CheckAuthenticationStatus()
        {
            if (!this.isAuthenticated && !this.isAuthenticating)
            {
                return Authenticate();
            }
            else
            {
                return this.isAuthenticated;
            }
        }

        #endregion

        /// <summary>
        /// Dispose handles dispose of custom objects before disposing of the remainder of the objcet
        /// </summary>
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
            this.Dispose();
        }
    }
}
