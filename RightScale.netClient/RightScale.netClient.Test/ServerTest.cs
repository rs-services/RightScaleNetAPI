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
        private string multiCloudImageID;
        private string instanceTypeID;

        public ServerTest()
        {
            deploymentID = ConfigurationManager.AppSettings["ServerTest_deploymentID"].ToString();
            serverID = ConfigurationManager.AppSettings["ServerTest_serverID"].ToString();
            cloudID = ConfigurationManager.AppSettings["ServerTest_cloudID"].ToString();
            serverTemplateID = ConfigurationManager.AppSettings["ServerTest_serverTemplateID"].ToString();
            multiCloudImageID = ConfigurationManager.AppSettings["ServerTest_multiCloudImageID"].ToString();
            instanceTypeID = ConfigurationManager.AppSettings["ServerTest_instanceTypeID"].ToString();
        }
        
        #region Server.index tests

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

        #endregion
        
        #region Server.show tests

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

        #endregion

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
        public void serverCreateDeploymentDestroyDeploymentSimpleTest()
        {
            string newServerID = Server.create_deployment(deploymentID, cloudID, serverTemplateID, "this is a server name");
            Assert.IsNotNull(newServerID);
            bool destroyRetVal = Server.destroy_deployment(newServerID, deploymentID);
            Assert.IsTrue(destroyRetVal);
        }

        [TestMethod]
        public void serverCreateDeploymentDestroySimpleTest()
        {
            string newServerID = Server.create_deployment(deploymentID, cloudID, serverTemplateID, "this is a server name");
            Assert.IsNotNull(newServerID);
            bool destroyRetVal = Server.destroy(newServerID);
            Assert.IsTrue(destroyRetVal);
        }

        [TestMethod]
        public void serverCreateDestroySimpleTest()
        {
            string newServerID = Server.create(cloudID, deploymentID, serverTemplateID, "this is another test server name");
            Assert.IsNotNull(newServerID);
            bool destroyRetVal = Server.destroy(newServerID);
            Assert.IsTrue(destroyRetVal);
        }

        [TestMethod]
        public void serverCreateDestroyDeploymentSimpleTest()
        {
            string newServerID = Server.create(cloudID, deploymentID, serverTemplateID, "this is another test server name");
            Assert.IsNotNull(newServerID);
            bool destroyRetVal = Server.destroy_deployment(newServerID, deploymentID);
            Assert.IsTrue(destroyRetVal);
        }


        [TestMethod]
        public void serverCreateComplicatedDestroySimpleTest()
        {
            List<KeyValuePair<string, string>> inputs = new List<KeyValuePair<string, string>>();
            inputs.Add(new KeyValuePair<string, string>("ADMIN_PASSWORD", "text:thisisapassword!@#$%^"));
            string newServerID = Server.create(cloudID, deploymentID, serverTemplateID, "complicated Server Instance", "this is a description...", cloudID, null, null, inputs, instanceTypeID, null, multiCloudImageID, null, null, null, null, false);
            Assert.IsNotNull(newServerID);
            bool delRetVal = Server.destroy(newServerID);
            Assert.IsTrue(delRetVal);
        }

        [TestMethod]
        public void serverCreateUpdateDestroySimpleTest()
        {
            string newServerID = Server.create(cloudID, deploymentID, serverTemplateID, "this is another test server name");
            Assert.IsNotNull(newServerID);
            Server initialTest = Server.show(newServerID);
            Assert.IsNotNull(initialTest);
            bool updated = Server.update(newServerID, "new description", string.Empty, false);
            Assert.IsTrue(updated);
            Server updatedTest = Server.show(newServerID);
            Assert.IsNotNull(updatedTest);
            Assert.AreNotEqual(updatedTest.description, initialTest.description);
            bool destroyRetVal = Server.destroy_deployment(newServerID, deploymentID);
            Assert.IsTrue(destroyRetVal);
        }
    }
}
