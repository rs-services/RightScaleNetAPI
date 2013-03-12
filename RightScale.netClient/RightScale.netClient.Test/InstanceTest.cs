using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using System.Web;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class InstanceTest
    {
        private string deploymentID;
        private string cloudID;
        private string serverArrayID;
        private string filterListString;

        public InstanceTest()
        {
            deploymentID = ConfigurationManager.AppSettings["InstanceTest_deploymentID"].ToString();
            cloudID = ConfigurationManager.AppSettings["InstanceTest_cloudID"].ToString();
            serverArrayID = ConfigurationManager.AppSettings["InstanceTest_serverArrayID"].ToString();
            filterListString = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["InstanceTest_filterListString"].ToString());
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
            List<Filter> filters = new List<Filter>();
            filters.Add(new Filter("deployment_href", FilterOperator.Equal, Utility.deploymentHref(deploymentID)));
            List<Instance> instanceList = Instance.index(cloudID, filters);
            Assert.IsNotNull(instanceList);
            Assert.IsTrue(instanceList.Count > 0);
        }

        [TestMethod]
        public void indexFilteredTest()
        {
            List<Filter> indexFilter = Filter.parseFilterList(filterListString);
            Assert.IsNotNull(indexFilter);
            List<Instance> instanceList = Instance.index(cloudID, indexFilter);
            Assert.IsNotNull(instanceList);
            Assert.IsTrue(instanceList.Count > 0);            
        }
        
        [TestMethod]
        public void indexViewExtendedTest()
        {
            indexViewTest("extended");
        }

        [TestMethod]
        public void indexViewFullTest()
        {
            indexViewTest("full");
        }

        [TestMethod]
        public void indexViewInputs20Test()
        {
            indexViewTest("full_inputs_2_0");
        }

        private void indexViewTest(string viewName)
        {
            List<Instance> instanceList = Instance.index(cloudID, viewName);
            Assert.IsNotNull(instanceList);
        }

        #endregion

        #region Instance.show tests

        [TestMethod]
        public void showSimple()
        {
            List<Instance> instanceList = Instance.index(cloudID);
            Assert.IsNotNull(instanceList);
            Assert.IsTrue(instanceList.Count > 0);
            string instanceID = instanceList[0].ID;
            Instance testInstance = Instance.show(cloudID, instanceID);
            Assert.IsNotNull(testInstance);
            Assert.AreEqual(instanceList[0].name, testInstance.name);
            Assert.AreEqual(instanceList[0].os_platform, testInstance.os_platform);
            Assert.AreEqual(instanceList[0].pricing_type, testInstance.pricing_type);
        }

        [TestMethod]
        public void showExtended()
        {
            List<Instance> instanceList = Instance.index(cloudID);
            Assert.IsNotNull(instanceList);
            Assert.IsTrue(instanceList.Count > 0);
            string instanceID = instanceList[0].ID;
            Instance testInstance = Instance.show(cloudID, instanceID, "extended");
            Assert.IsNotNull(testInstance);
            Assert.AreEqual(instanceList[0].name, testInstance.name);
            Assert.AreEqual(instanceList[0].os_platform, testInstance.os_platform);
            Assert.AreEqual(instanceList[0].pricing_type, testInstance.pricing_type);
        }

        [TestMethod]
        public void showFull()
        {
            List<Instance> instanceList = Instance.index(cloudID);
            Assert.IsNotNull(instanceList);
            Assert.IsTrue(instanceList.Count > 0);
            string instanceID = instanceList[0].ID;
            Instance testInstance = Instance.show(cloudID, instanceID, "full");
            Assert.IsNotNull(testInstance);
            Assert.AreEqual(instanceList[0].name, testInstance.name);
            Assert.AreEqual(instanceList[0].os_platform, testInstance.os_platform);
            Assert.AreEqual(instanceList[0].pricing_type, testInstance.pricing_type);
        }

        [TestMethod]
        public void showFullInputs20()
        {
            List<Instance> instanceList = Instance.index(cloudID);
            Assert.IsNotNull(instanceList);
            Assert.IsTrue(instanceList.Count > 0);
            string instanceID = instanceList[0].ID;
            Instance testInstance = Instance.show(cloudID, instanceID, "full_inputs_2_0");
            Assert.IsNotNull(testInstance);
            Assert.AreEqual(instanceList[0].name, testInstance.name);
            Assert.AreEqual(instanceList[0].os_platform, testInstance.os_platform);
            Assert.AreEqual(instanceList[0].pricing_type, testInstance.pricing_type);
        }
        #endregion


    }
}
