using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Collections;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class SecurityGroupRuleTest : RSAPITestBase
    {
        string accountID;

        public SecurityGroupRuleTest()
        {
            this.accountID = ConfigurationManager.AppSettings["SecurityGroupTest_accountID"].ToString();
        }

        [TestMethod]
        public void TestMethod1()
        {
            netClient.Core.APIClient.Instance.InitWebClient();
            netClient.Core.APIClient.Instance.Authenticate(this.authUserName, this.authPassword, this.accountID);

            

            netClient.Core.APIClient.Instance.InitWebClient();
        }
    }
}
