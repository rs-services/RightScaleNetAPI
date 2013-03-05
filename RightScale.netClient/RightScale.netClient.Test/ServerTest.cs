using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class ServerTest
    {
        private string deploymentID;

        public ServerTest()
        {
            deploymentID = ConfigurationManager.AppSettings["ServerTest_deploymentID"].ToString();
        }

        [TestMethod]
        public void serverIndexTest()
        {
            List<Server> serverIndexTest = Server.index();
            Assert.IsNotNull(serverIndexTest);
            Assert.IsTrue(serverIndexTest.Count > 0);
        }

        [TestMethod]
        public void serverIndexDeploymentTest()
        {
            List<Server> serverIndexDeploymentTest = Server.index_deployment(deploymentID);
            Assert.IsNotNull(serverIndexDeploymentTest);
            Assert.IsTrue(serverIndexDeploymentTest.Count > 0);
        }
    }
}
