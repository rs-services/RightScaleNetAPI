using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class CloudTest
    {
        string awsCloudID;
        string azureCloudID;
        string openstackCloudID;

        public CloudTest()
        {
            awsCloudID = ConfigurationManager.AppSettings["CloudTest_awsCloudID"].ToString();
            azureCloudID = ConfigurationManager.AppSettings["CloudTest_azureCloudID"].ToString();
            openstackCloudID = ConfigurationManager.AppSettings["CloudTest_openstackCloudID"].ToString();
        }

        #region Cloud.show tests

        [TestMethod]
        public void AzureCloudShowTest()
        {
            Cloud azureCloud = Cloud.show(azureCloudID);
            Assert.IsNotNull(azureCloud);
        }

        [TestMethod]
        public void OpenStackCloudShowTest()
        {
            Cloud openstackCloud = Cloud.show(openstackCloudID);
            Assert.IsNotNull(openstackCloud);
        }

        #endregion
    }
}
