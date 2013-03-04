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

        //TODO: AWS not enabled in API 1.5 yet
        //[TestMethod]
        //public void AWSCloudTest()
        //{
        //    Cloud awsCloud = Cloud.show(awsCloudID);
        //    Assert.IsNotNull(awsCloud);
        //}

        [TestMethod]
        public void AzureCloudTest()
        {
            Cloud azureCloud = Cloud.show(azureCloudID);
            Assert.IsNotNull(azureCloud);
        }

        [TestMethod]
        public void OpenStackCloudTest()
        {
            Cloud openstackCloud = Cloud.show(openstackCloudID);
            Assert.IsNotNull(openstackCloud);
        }
    }
}
