using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RightScale.netClient.Core;
using System.Collections.Generic;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class SessionTest
    {

        private string refreshToken = "";
        private string userName = "";
        private string password = "";
        private string accountID = "";

        public SessionTest()
        {
            refreshToken = ConfigurationManager.AppSettings["RightScaleAPIRefreshToken"].ToString();
            accountID = ConfigurationManager.AppSettings["RightScaleAPIAccountId"].ToString();
            password = ConfigurationManager.AppSettings["RightScaleAPIPassword"].ToString();
            userName = ConfigurationManager.AppSettings["RightScaleAPIUserName"].ToString();
        }
        
        #region Cloud.index tests
        [TestMethod]
        public void SessionIndexSimple()
        {
            APIClient.Instance.InitWebClient();
            bool result = APIClient.Instance.Authenticate(userName, password, accountID);
            Assert.IsTrue(result, "RSAPI Failed to authenticate with username, password and account ID");

            Session sessionList = Session.index();

            Assert.IsNotNull(sessionList);
            //Assert.IsTrue(listofSessions.count > 0);
        }
        #endregion
    }
}
