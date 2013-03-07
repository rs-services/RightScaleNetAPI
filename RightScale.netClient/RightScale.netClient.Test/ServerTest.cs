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
        private string serverID;
        private string cloudID;
        private string serverTemplateID;

        public ServerTest()
        {
            deploymentID = ConfigurationManager.AppSettings["ServerTest_deploymentID"].ToString();
            serverID = ConfigurationManager.AppSettings["ServerTest_serverID"].ToString();
            cloudID = ConfigurationManager.AppSettings["ServerTest_cloudID"].ToString();
            serverTemplateID = ConfigurationManager.AppSettings["ServerTest_serverTemplateID"].ToString();
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

        [TestMethod]
        public void serverCloneDestroyTest()
        {
            string newServerID = Server.clone(serverID);
            Assert.IsNotNull(newServerID);
            bool destroyResult = Server.destroy(newServerID);
            Assert.IsTrue(destroyResult);
        }

        [TestMethod]
        public void serverCloneUpdateDestroyTest()
        {
            string newServerID = Server.clone(serverID);
            Assert.IsNotNull(newServerID);
            Server firstObject = Server.show(newServerID);
            Assert.IsNotNull(firstObject);
            bool updateRetVal = Server.update(newServerID, "this is a new description", "this is a new name", false);
            Assert.IsTrue(updateRetVal);
            Server secondObject = Server.show(newServerID);
            Assert.IsNotNull(secondObject);
            Assert.AreNotEqual(firstObject.description, secondObject.description);
            Assert.AreNotEqual(firstObject.name, secondObject.name);
            bool deleteRetVal = Server.destroy(newServerID);
            Assert.IsTrue(deleteRetVal);
        }

        [TestMethod]
        public void serverCreateDeploymentDestroySimpleTest()
        {
            string newServerID = Server.create_deployment(deploymentID, cloudID, serverTemplateID, "this is a server name");
            Assert.IsNotNull(newServerID);
            bool destroyRetVal = Server.destroy_deployment(newServerID, deploymentID);
            Assert.IsTrue(destroyRetVal);
        }

        [TestMethod]
        public void serverShowTest()
        {
            Server serverobj = Server.show(serverID);
            Assert.IsNotNull(serverobj);
        }
        
        [TestMethod]
        public void serverDeploymentShowTest()
        {
            Server serverobj = Server.show_deployment(serverID, deploymentID);
            Assert.IsNotNull(serverobj);
        }
    }
}
