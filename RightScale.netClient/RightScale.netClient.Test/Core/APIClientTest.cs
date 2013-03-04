using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RightScale.netClient.Core;
using System.Threading.Tasks;
using System.Configuration;

namespace RightScale.netClient.Test.Core
{
    [TestClass]
    public class APIClientTest
    {
        private string refreshToken = "";
        private string userName = "";
        private string password = "";
        private string accountID = "";

        public APIClientTest()
        {
            refreshToken = ConfigurationManager.AppSettings["RightScaleAPIRefreshToken"].ToString();
            accountID = ConfigurationManager.AppSettings["RightScaleAPIAccountId"].ToString();
            password = ConfigurationManager.AppSettings["RightScaleAPIPassword"].ToString();
            userName = ConfigurationManager.AppSettings["RightScaleAPIUserName"].ToString();
        }

        [TestMethod]
        public void OAuthAuthenticationTest()
        {
            APIClient.Instance.InitWebClient();
            Task<bool> testTask =  APIClient.Instance.Authenticate(refreshToken);
            bool result = testTask.Result;
            Assert.IsTrue(result, "RSAPI Failed to authenticate with OAtuth2 Refresh Token");
        }

        [TestMethod]
        public void UsernamePasswordAccountIDAuthenticationTest()
        {
            APIClient.Instance.InitWebClient();
            Task<bool> testTask = APIClient.Instance.Authenticate(userName, password, accountID);
            bool result = testTask.Result;
            Assert.IsTrue(result, "RSAPI Failed to authenticate with username, password and account ID");
        }

        [TestMethod]
        public void DefaultAuthenticationTest()
        {
            APIClient.Instance.InitWebClient();
            Task<bool> testTask = APIClient.Instance.Authenticate();
            bool result = testTask.Result;
            Assert.IsTrue(result, "RSAPI Failed to authenticate with default, configuration based constructor");
        }
    }
}
