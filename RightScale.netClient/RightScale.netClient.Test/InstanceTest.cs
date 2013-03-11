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

        #endregion

    }
}
