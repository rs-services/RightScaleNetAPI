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

        public SecurityGroupTest()
        {
            cloudID = this.cloudStackCloudID;
            apiRefreshToken = ConfigurationManager.AppSettings["RightScaleServicesAPIRefreshToken"].ToString();
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
        public void MyTestMethod()
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
    }
}
