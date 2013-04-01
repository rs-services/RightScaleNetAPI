using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class ResourceTagTest
    {
        private string serverID;
        string deploymentID;
        string multiCloudImageID;

        public ResourceTagTest()
        {

            serverID = ConfigurationManager.AppSettings["ServerTest_serverID"].ToString();
            deploymentID = ConfigurationManager.AppSettings["DeploymentTest_deploymentID"].ToString();
            multiCloudImageID = ConfigurationManager.AppSettings["ResourceTagTest_MultiCloudImage"].ToString();
        }

        [TestMethod]
        public void ServerTagTest()
        {
            Server serverobj = Server.show(serverID);
            serverobj.populateObject();
            Assert.IsNotNull(serverobj);
        }

        [TestMethod]
        public void DeploymentTagTest()
        {            
            Deployment testDeployment = Deployment.show(deploymentID);
            testDeployment.populateObject();
            Assert.IsNotNull(testDeployment);
        }

        [TestMethod]
        public void MultiCloudImageTagTest()
        {
            MultiCloudImage mci = MultiCloudImage.show(multiCloudImageID);
            mci.populateObject();
            Assert.IsNotNull(mci);
        }
    }
}
