using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class SubnetTest : RSAPITestBase
    {

        string cloudID;
        string servicesOauthToken;
        string subnetID;

        public SubnetTest()
        {
            this.cloudID = cloudStackCloudID;
            this.servicesOauthToken = ConfigurationManager.AppSettings["RightScaleServicesAPIRefreshToken"].ToString();
            this.subnetID = ConfigurationManager.AppSettings["SubnetTest_SubnetID"].ToString();
        }

        #region Subnet.index() tests

        [TestMethod]
        public void subnetIndexSimple()
        {
            netClient.Core.APIClient.Instance.InitWebClient();
            netClient.Core.APIClient.Instance.Authenticate(servicesOauthToken);

            List<Subnet> subnets = Subnet.index(this.cloudID);
            Assert.IsNotNull(subnets);
            Assert.IsTrue(subnets.Count > 0);

            netClient.Core.APIClient.Instance.InitWebClient();
        }

        [TestMethod]
        public void subnetIndexFiltered()
        {
            netClient.Core.APIClient.Instance.InitWebClient();
            netClient.Core.APIClient.Instance.Authenticate(servicesOauthToken);

            List<Filter> filter = new List<Filter>();
            filter.Add(new Filter("visibility", FilterOperator.Equal, "private"));

            List<Subnet> subnets = Subnet.index(this.cloudID, filter);
            Assert.IsNotNull(subnets);
            Assert.IsTrue(subnets.Count > 0);

            netClient.Core.APIClient.Instance.InitWebClient();
        }

        #endregion

        #region Subnet.show() tests

        [TestMethod]
        public void subnetShowTest()
        {
            netClient.Core.APIClient.Instance.InitWebClient();
            netClient.Core.APIClient.Instance.Authenticate(servicesOauthToken);

            Subnet testSubnet = Subnet.show(this.cloudID, this.subnetID);
            Assert.IsNotNull(testSubnet);
            Assert.IsTrue(testSubnet.ID == this.subnetID);
            Assert.IsTrue(testSubnet.resource_uid.Length > 0);

            netClient.Core.APIClient.Instance.InitWebClient();
        }

        #endregion
    }
}
