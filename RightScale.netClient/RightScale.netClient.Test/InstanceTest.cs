using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class InstanceTest
    {
        private string deploymentID;
        private string cloudID;
        private string serverArrayID;

        public InstanceTest()
        {
            deploymentID = ConfigurationManager.AppSettings["InstanceTest_deploymentID"].ToString();
            cloudID = ConfigurationManager.AppSettings["InstanceTest_cloudID"].ToString();
            serverArrayID = ConfigurationManager.AppSettings["InstanceTest_serverArrayID"].ToString();
        }

        #region Instance.index tests

        [TestMethod]
        public void indexSimpleTest()
        {
            List<Instance> instanceList = Instance.index(cloudID);
            Assert.IsNotNull(instanceList);
            Assert.IsTrue(instanceList.Count > 0);
        }

        [TestMethod]
        public void indexServerArrayTest()
        {
            List<Instance> instanceList = Instance.index_serverArray(serverArrayID);
            Assert.IsNotNull(instanceList);
        }

        [TestMethod]
        public void indexDeploymentTest()
        {
            List<KeyValuePair<string, string>> filters = new List<KeyValuePair<string, string>>();
            filters.Add(new KeyValuePair<string, string>("deployment_href", Utility.deploymentHref(deploymentID)));
            List<Instance> instanceList = Instance.index(cloudID, filters);
            Assert.IsNotNull(instanceList);
            Assert.IsTrue(instanceList.Count > 0);
        }

        #endregion

    }
}
