using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RightScale.netClient.Core;
using System.Collections.Generic;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class SessionTest : RSAPITestBase
    {
        private string instanceToken = "";

        public SessionTest()
        {
            instanceToken = ConfigurationManager.AppSettings["RightScaleInstanceAPIToken"].ToString();
        }
        
        #region Session.index tests
        [TestMethod]
        public void SessionIndexSimple()
        {
            APIClient.Instance.InitWebClient();
            bool result = APIClient.Instance.Authenticate(authUserName, authPassword, authAccountID);
            Assert.IsTrue(result, "RSAPI Failed to authenticate with username, password and account ID");

            Session sessionList = Session.index();

            Assert.IsNotNull(sessionList);
            //Assert.IsTrue(listofSessions.count > 0);
        }
        #endregion

        #region Session.accounts test

        [TestMethod]
        public void SessionAccounts()
        {
            List<Account> accounts = Session.accounts(authUserName, authPassword);
            Assert.IsNotNull(accounts);
            Assert.IsTrue(accounts.Count > 0);
        }

        #endregion

        #region Session.create_instance_session test

        [TestMethod]
        public void CreateInstanceSession()
        {
            netClient.Core.APIClient.Instance.InitWebClient();
            bool isAuthenticated = Session.create_instance_session(instanceToken);
            Assert.IsTrue(netClient.Core.APIClient.Instance.isInstanceAuthenticated);
            Assert.IsTrue(isAuthenticated);
            netClient.Core.APIClient.Instance.InitWebClient();//clean up after ourselves
        }

        #endregion

        #region Session.index_instance_session test

        [TestMethod]
        public void IndexInstanceSession()
        {
            netClient.Core.APIClient.Instance.InitWebClient();
            bool isAuthenticated = Session.create_instance_session(instanceToken);
            Assert.IsTrue(netClient.Core.APIClient.Instance.isInstanceAuthenticated);
            Assert.IsTrue(isAuthenticated);
            Instance self = Session.index_instance_session();
            netClient.Core.APIClient.Instance.InitWebClient();//clean up after ourselves            
        }

        #endregion
    }
}
