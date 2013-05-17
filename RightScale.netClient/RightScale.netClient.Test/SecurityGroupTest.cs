using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Collections.Generic;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class SecurityGroupTest : RSAPITestBase
    {
        string cloudID;
        string apiRefreshToken;
        string accountID;

        public SecurityGroupTest()
        {
            this.cloudID = this.cloudStackCloudID;
            this.apiRefreshToken = ConfigurationManager.AppSettings["RightScaleServicesAPIRefreshToken"].ToString();
            this.accountID = ConfigurationManager.AppSettings["SecurityGroupTest_accountID"].ToString();
        }

        [TestMethod]
        public void securityGroupIndexSimple()
        {
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
            RightScale.netClient.Core.APIClient.Instance.Authenticate(this.apiRefreshToken);

            List<SecurityGroup> sgList = SecurityGroup.index(this.cloudID);
            Assert.IsNotNull(sgList);
            Assert.IsTrue(sgList.Count > 0);

            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
        }

        [TestMethod]
        public void securityGroupIndexFiltered()
        {
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
            RightScale.netClient.Core.APIClient.Instance.Authenticate(this.apiRefreshToken);

            List<SecurityGroup> sgList = SecurityGroup.index(this.cloudID);
            Assert.IsNotNull(sgList);
            Assert.IsTrue(sgList.Count > 0);

            List<Filter> filter = new List<Filter>();
            filter.Add(new Filter("name", FilterOperator.Equal, "default"));
            List<SecurityGroup> filteredSgList = SecurityGroup.index(this.cloudID, filter);
            Assert.IsNotNull(filteredSgList);
            Assert.IsTrue(filteredSgList.Count > 0);

            Assert.IsTrue(sgList.Count > filteredSgList.Count);

            RightScale.netClient.Core.APIClient.Instance.InitWebClient();

        }

        [TestMethod]
        public void securityGroupViewTest()
        {
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
            RightScale.netClient.Core.APIClient.Instance.Authenticate(this.apiRefreshToken);

            List<string> views = new List<string>() { "tiny", "default" };
            foreach (string v in views)
            {
                List<SecurityGroup> sgList = SecurityGroup.index(this.cloudID, v);
                Assert.IsNotNull(sgList);
                Assert.IsTrue(sgList.Count > 0);
            }
            
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
        }

        [TestMethod]
        public void securityGroupCreateDelete()
        {
            netClient.Core.APIClient.Instance.InitWebClient();
            netClient.Core.APIClient.Instance.Authenticate(this.authUserName, this.authPassword, this.accountID);
            string sgID =string.Empty;
            try
            {
                sgID = SecurityGroup.create(this.awsUSEastCloudID, Guid.NewGuid().ToString().Substring(0,10));
                Assert.IsNotNull(sgID);
                Assert.IsTrue(sgID.Length > 0);

                bool isDeleted = SecurityGroup.destroy(this.awsUSEastCloudID, sgID);
                Assert.IsTrue(isDeleted);
                sgID = string.Empty;
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(sgID))
                {
                    SecurityGroup.destroy(this.cloudID, sgID);
                }
            }
            netClient.Core.APIClient.Instance.InitWebClient();
        }

    }
}
