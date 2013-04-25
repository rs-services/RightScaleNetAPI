using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using System.Web;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class InstanceTest : RSAPITestBase
    {
        private string cloudID;
        private string filterListString;
        private string apiRefreshToken;
        private string runExecServerID;
        private string execRunScriptID;
        private string execRunScriptName;

        public InstanceTest()
        {
            apiRefreshToken = ConfigurationManager.AppSettings["RightScaleServicesAPIRefreshToken"].ToString();
            cloudID = this.azureCloudID;
            filterListString = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["InstanceTest_filterListString"].ToString());
            runExecServerID = ConfigurationManager.AppSettings["InstanceTest_runExecTestServer"].ToString();
            execRunScriptID = ConfigurationManager.AppSettings["InstanceTest_execScriptID"].ToString();
            execRunScriptName = ConfigurationManager.AppSettings["InstanceTest_execScriptName"].ToString();
        }

        #region Instance relationship tests

        [TestMethod]
        public void instanceTags()
        {
            List<Instance> instanceList = Instance.index(cloudID);
            Assert.IsNotNull(instanceList);
            Assert.IsTrue(instanceList.Count > 0);
            string instanceID = instanceList[0].ID;
            Instance testInstance = Instance.show(cloudID, instanceID, "full");
            Assert.IsNotNull(testInstance);
            List<Tag> tags = testInstance.tags;
            Assert.IsTrue(true);//no exception!
        }

        [TestMethod]
        public void instanceDatacenter()
        {
            List<Instance> instanceList = Instance.index(cloudID);
            Assert.IsNotNull(instanceList);
            Assert.IsTrue(instanceList.Count > 0);
            string instanceID = instanceList[0].ID;
            Instance testInstance = Instance.show(cloudID, instanceID, "full");
            Assert.IsNotNull(testInstance);
            DataCenter dc = testInstance.datacenter;
            Assert.IsTrue(true); //no exception
        }

        [TestMethod]
        public void instanceMultiCloudImage()
        {
            List<Instance> instanceList = Instance.index(cloudID);
            Assert.IsNotNull(instanceList);
            Assert.IsTrue(instanceList.Count > 0);
            string instanceID = instanceList[0].ID;
            Instance testInstance = Instance.show(cloudID, instanceID, "full");
            Assert.IsNotNull(testInstance);
            MultiCloudImage mci = testInstance.multiCloudImage;
            Assert.IsTrue(true); //no exception
        }

        [TestMethod]
        public void instanceServerTemplate()
        {            
            List<Instance> instanceList = Instance.index(cloudID);
            Assert.IsNotNull(instanceList);
            Assert.IsTrue(instanceList.Count > 0);
            string instanceID = instanceList[0].ID;
            Instance testInstance = Instance.show(cloudID, instanceID, "full");
            Assert.IsNotNull(testInstance);
            ServerTemplate st = testInstance.serverTemplate;
            Assert.IsNotNull(st);
            Assert.IsTrue(st.ID.Length > 0);
        }

        [TestMethod]
        public void instanceRamdiskImage()
        {
            List<Instance> instanceList = Instance.index(cloudID);
            Assert.IsNotNull(instanceList);
            Assert.IsTrue(instanceList.Count > 0);
            string instanceID = instanceList[0].ID;
            Instance testInstance = Instance.show(cloudID, instanceID, "full");
            Assert.IsNotNull(testInstance);
            Image ramdiskImage = testInstance.ramdiskImage;
            Assert.IsTrue(true);//no exception            
        }

        [TestMethod]
        public void instanceInstanceType()
        {
            List<Instance> instanceList = Instance.index(cloudID);
            Assert.IsNotNull(instanceList);
            Assert.IsTrue(instanceList.Count > 0);
            string instanceID = instanceList[0].ID;
            Instance testInstance = Instance.show(cloudID, instanceID, "full");
            Assert.IsNotNull(testInstance);
            InstanceType it = testInstance.instanceType;
            Assert.IsTrue(true);//no exception
        }

        [TestMethod]
        public void instanceMonitoringMetrics()
        {            
            List<Instance> instanceList = Instance.index(cloudID);
            Assert.IsNotNull(instanceList);
            Assert.IsTrue(instanceList.Count > 0);
            string instanceID = instanceList[0].ID;
            Instance testInstance = Instance.show(cloudID, instanceID, "full");
            Assert.IsNotNull(testInstance);
            List<MonitoringMetric> mms = testInstance.monitoringMetrics;
            Assert.IsTrue(true);//no exception
        }

        [TestMethod]
        public void instanceKernelImage()
        {
            List<Instance> instanceList = Instance.index(cloudID);
            Assert.IsNotNull(instanceList);
            Assert.IsTrue(instanceList.Count > 0);
            string instanceID = instanceList[0].ID;
            Instance testInstance = Instance.show(cloudID, instanceID, "full");
            Assert.IsNotNull(testInstance);
            Image kernelImage = testInstance.kernelImage;
            Assert.IsTrue(true);//no exception
        }

        [TestMethod]
        public void instaneSSHKey()
        {
            List<Instance> instanceList = Instance.index(cloudID);
            Assert.IsNotNull(instanceList);
            Assert.IsTrue(instanceList.Count > 0);
            string instanceID = instanceList[0].ID;
            Instance testInstance = Instance.show(cloudID, instanceID, "full");
            Assert.IsNotNull(testInstance);
            SshKey ssh = testInstance.sshKey;
            Assert.IsTrue(true);//no exception
        }

        [TestMethod]
        public void instanceImage()
        {            
            List<Instance> instanceList = Instance.index(cloudID);
            Assert.IsNotNull(instanceList);
            Assert.IsTrue(instanceList.Count > 0);
            string instanceID = instanceList[0].ID;
            Instance testInstance = Instance.show(cloudID, instanceID, "full");
            Assert.IsNotNull(testInstance);
            Image img = testInstance.image;
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void instanceDeployment()
        {
            List<Instance> instanceList = Instance.index(cloudID);
            Assert.IsNotNull(instanceList);
            Assert.IsTrue(instanceList.Count > 0);
            string instanceID = instanceList[0].ID;
            Instance testInstance = Instance.show(cloudID, instanceID, "full");
            Assert.IsNotNull(testInstance);
            Deployment dep = testInstance.deployment;
            Assert.IsNotNull(dep);
            Assert.IsTrue(dep.name.Length > 0);            
        }

        [TestMethod]
        public void instanceCloud()
        {
            List<Instance> instanceList = Instance.index(cloudID);
            Assert.IsNotNull(instanceList);
            Assert.IsTrue(instanceList.Count > 0);
            string instanceID = instanceList[0].ID;
            Instance testInstance = Instance.show(cloudID, instanceID, "full");
            Assert.IsNotNull(testInstance);
            Cloud cl = testInstance.cloud;
            Assert.IsNotNull(cl);
            Assert.IsTrue(cl.name.Length > 0);
            
        }

        #endregion

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
            List<Instance> instanceList = Instance.index_serverArray(liveTestServerArrayID);
            Assert.IsNotNull(instanceList);
        }

        [TestMethod]
        public void indexInstanceDeploymentTest()
        {
            List<Filter> filters = new List<Filter>();
            filters.Add(new Filter("deployment_href", FilterOperator.Equal, Utility.deploymentHref(liveTestDeploymentID)));
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

        [TestMethod]
        public void runExecutableByIDSimpleTest()
        {
            netClient.Core.APIClient.Instance.InitWebClient();
            netClient.Core.APIClient.Instance.Authenticate(apiRefreshToken);

            Server serverInfo = Server.show(runExecServerID);
            Instance currentInst = serverInfo.currentInstance;

            Task taskVal = Instance.run_executable(currentInst.cloud.ID, currentInst.ID, string.Empty, execRunScriptID, new List<Input>(), true);

            while (taskVal.summary.ToLower().StartsWith("complete"))
            {
                System.Threading.Thread.Sleep(10000);
                taskVal.Refresh();
            }

            netClient.Core.APIClient.Instance.InitWebClient();
        }

        //[TestMethod]
        public void runExecutableByNameSimpleTest()
        {
            netClient.Core.APIClient.Instance.InitWebClient();
            netClient.Core.APIClient.Instance.Authenticate(apiRefreshToken);

            Server serverInfo = Server.show(runExecServerID);
            Instance currentInst = serverInfo.currentInstance;

            Task taskVal = Instance.run_executable(currentInst.cloud.ID, currentInst.ID, execRunScriptName, string.Empty, new List<Input>(), true);

            while (taskVal.summary.ToLower().StartsWith("complete"))
            {
                System.Threading.Thread.Sleep(10000);
                taskVal.Refresh();
            }

            netClient.Core.APIClient.Instance.InitWebClient();
        }

    }
}
