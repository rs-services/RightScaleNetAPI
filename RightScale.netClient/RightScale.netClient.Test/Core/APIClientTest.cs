using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RightScale.netClient.Core;
using System.Threading.Tasks;
using System.Configuration;

namespace RightScale.netClient.Test.Core
{
    [TestClass]
    public class APIClientTest : RSAPITestBase
    {

        public APIClientTest()
        {
        }

        [TestMethod]
        public void OAuthAuthenticationTest()
        {
            APIClient.Instance.InitWebClient();
            bool result =  APIClient.Instance.Authenticate(authRefreshToken);
            Assert.IsTrue(result, "RSAPI Failed to authenticate with OAtuth2 Refresh Token");
            APIClient.Instance.InitWebClient();
        }

        [TestMethod]
        public void UsernamePasswordAccountIDAuthenticationTest()
        {
            APIClient.Instance.InitWebClient();
            bool result = APIClient.Instance.Authenticate(authUserName, authPassword, authAccountID);
            Assert.IsTrue(result, "RSAPI Failed to authenticate with username, password and account ID");
            APIClient.Instance.InitWebClient();
        }

        [TestMethod]
        public void UsernamePasswordAccountIDShardAuthenticationTest()
        {
            APIClient.Instance.InitWebClient();
            bool result = APIClient.Instance.Authenticate(authUserName, authPassword, "60604");

        }

        [TestMethod]
        public void DefaultAuthenticationTest()
        {
            APIClient.Instance.InitWebClient();
            bool result = APIClient.Instance.Authenticate();
            Assert.IsTrue(result, "RSAPI Failed to authenticate with default, configuration based constructor");
            APIClient.Instance.InitWebClient();
        }

        [TestMethod]
        public void UsernamePassDoubleAuth()
        {
            APIClient.Instance.InitWebClient();
            bool result = APIClient.Instance.Authenticate(authUserName, authPassword, authAccountID);
            Assert.IsTrue(result, "RSAPI Failed to authenticate with username, password and account ID");
            bool result2 = APIClient.Instance.Authenticate(authUserName, authPassword, authAccountID);
            Assert.IsTrue(result2, "Second RSAPI Failed to authenticate with username, password and account ID");
            APIClient.Instance.InitWebClient();
        }

        [TestMethod]
        public void OauthDoubleAuth()
        {
            APIClient.Instance.InitWebClient();
            bool result = APIClient.Instance.Authenticate(authRefreshToken);
            Assert.IsTrue(result, "RSAPI Failed to authenticate with OAtuth2 Refresh Token");
            bool result2 = APIClient.Instance.Authenticate(authRefreshToken);
            Assert.IsTrue(result2, "Second RSAPI Failed to authenticate with OAtuth2 Refresh Token");
            APIClient.Instance.InitWebClient();
        }

        [TestMethod]
        public void OauthUsernamePasswordAuth()
        {
            APIClient.Instance.InitWebClient();
            bool result = APIClient.Instance.Authenticate(authRefreshToken);
            Assert.IsTrue(result, "RSAPI Failed to authenticate with OAtuth2 Refresh Token");
            bool result2 = APIClient.Instance.Authenticate(authUserName, authPassword, authAccountID);
            Assert.IsTrue(result2, "Second RSAPI Failed to authenticate with username, password and account ID");
            APIClient.Instance.InitWebClient();
        }

        [TestMethod]
        public void UsernamePasswordOauthAuth()
        {
            APIClient.Instance.InitWebClient();
            bool result = APIClient.Instance.Authenticate(authUserName, authPassword, authAccountID);
            Assert.IsTrue(result, "RSAPI Failed to authenticate with username, password and account ID");
            bool result2 = APIClient.Instance.Authenticate(authRefreshToken);
            Assert.IsTrue(result2, "Second RSAPI Failed to authenticate with OAtuth2 Refresh Token");
            APIClient.Instance.InitWebClient();
        }

        [TestMethod]
        public void badPasswordAuth()
        {
            APIClient.Instance.InitWebClient();
            bool result = APIClient.Instance.Authenticate(authUserName, "thisisnotapassword", authAccountID);
            Assert.IsFalse(result);
            //clean up after ourselves
            APIClient.Instance.InitWebClient();
        }

        [TestMethod]
        public void badPasswordGoodPasswordAuth()
        {
            APIClient.Instance.InitWebClient();
            bool result = APIClient.Instance.Authenticate(authUserName, "thisisnotapassword", authAccountID);
            Assert.IsFalse(result);
            bool result2 = APIClient.Instance.Authenticate(authUserName, authPassword, authAccountID);
            Assert.IsTrue(result2, "Second RSAPI Failed to authenticate with username, password and account ID");
            APIClient.Instance.InitWebClient();
        }

        [TestMethod]
        public void badPasswordGoodOauthAuth()
        {
            APIClient.Instance.InitWebClient();
            bool result = APIClient.Instance.Authenticate(authUserName, "thisisnotapassword", authAccountID);
            Assert.IsFalse(result);
            bool result2 = APIClient.Instance.Authenticate(authRefreshToken);
            Assert.IsTrue(result2, "Second RSAPI Failed to authenticate with OAtuth2 Refresh Token");
            APIClient.Instance.InitWebClient();
        }

        [TestMethod]
        public void badOauthGoodPasswordAuth()
        {
            APIClient.Instance.InitWebClient();
            bool result = APIClient.Instance.Authenticate("thisisnotanoauthtoken");
            Assert.IsFalse(result);
            bool result2 = APIClient.Instance.Authenticate(authUserName, authPassword, authAccountID);
            Assert.IsTrue(result2, "Second RSAPI Failed to authenticate with username, password and account ID");
            APIClient.Instance.InitWebClient();
        }

        [TestMethod]
        public void badOauthGoodOauthAuth()
        {
            APIClient.Instance.InitWebClient();
            bool result = APIClient.Instance.Authenticate("thisisnotanoauthtoken");
            Assert.IsFalse(result);
            bool result2 = APIClient.Instance.Authenticate(authRefreshToken);
            Assert.IsTrue(result2, "Second RSAPI Failed to authenticate with OAtuth2 Refresh Token");
            APIClient.Instance.InitWebClient();
        }
    }
}
