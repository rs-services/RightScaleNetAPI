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
        public void indexInstanceSimpleTest()
        {
            List<Instance> instanceList = Instance.index(cloudID);
            Assert.IsNotNull(instanceList);
            Assert.IsTrue(instanceList.Count > 0);
        }

        [TestMethod]
        public void indexInstanceServerArrayTest()
        {
            List<Instance> instanceList = Instance.index_serverArray(serverArrayID);
            Assert.IsNotNull(instanceList);
        }

        [TestMethod]
        public void indexInstanceDeploymentTest()
        {
            List<Filter> filters = new List<Filter>();
            filters.Add(new Filter("deployment_href", FilterOperator.Equal, Utility.deploymentHref(deploymentID)));
            List<Instance> instanceList = Instance.index(cloudID, filters);
            Assert.IsNotNull(instanceList);
            Assert.IsTrue(instanceList.Count > 0);
        }

        [TestMethod]
        public void indexInstanceFilteredTest()
        {
            List<Filter> indexFilter = Filter.parseFilterList(filterListString);
            Assert.IsNotNull(indexFilter);
            List<Instance> instanceList = Instance.index(cloudID, indexFilter);
            Assert.IsNotNull(instanceList);
            Assert.IsTrue(instanceList.Count > 0);            
        }
        
        [TestMethod]
        public void indexInstanceViewExtendedTest()
        {
            indexInstanceViewTest("extended");
        }

        [TestMethod]
        public void indexInstanceViewFullTest()
        {
            indexInstanceViewTest("full");
        }

        [TestMethod]
        public void indexInstanceViewInputs20Test()
        {
            indexInstanceViewTest("full_inputs_2_0");
        }

        private void indexInstanceViewTest(string viewName)
        {
            List<Instance> instanceList = Instance.index(cloudID, viewName);
            Assert.IsNotNull(instanceList);
        }

        #endregion

        #region Instance.show tests

        [TestMethod]
        public void showInstanceSimple()
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
        public void showInstanceExtended()
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
        public void showInstanceFull()
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
        public void showInstanceFullInputs20()
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
