using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using System.Web;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class ServerArrayTest
    {
        private string filterListString;
        private string serverarrayView;
        private string serverarrayID;

        public ServerArrayTest()
        {
           
            filterListString = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["ServerArrayTest_filterListString"].ToString());
            serverarrayView = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["ServerArrayTest_serverarrayview"].ToString());
            serverarrayID = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["ServerArrayTest_serverarrayid"].ToString());
        }

        [TestMethod]
        public void serverArrayAlertSpecs()
        {
            ServerArray serverarray = ServerArray.show(serverarrayID, null);
            Assert.IsNotNull(serverarray);
            List<AlertSpec> alertSpecs = serverarray.alertSpecs;
            Assert.IsNotNull(alertSpecs);
        }

        [TestMethod]
        public void serverArrayNextInstance()
        {
            ServerArray serverarray = ServerArray.show(serverarrayID, null);
            Assert.IsNotNull(serverarray);
            Instance inst = serverarray.nextInstance;
            Assert.IsNotNull(inst);
        }

        [TestMethod]
        public void serverArrayCurrentInstances()
        {            
            ServerArray serverarray = ServerArray.show(serverarrayID, null);
            Assert.IsNotNull(serverarray);
            List<Instance> currInstances = serverarray.currentInstances;
            Assert.IsNotNull(currInstances);
        }

        [TestMethod]
        public void serverArrayDeployment()
        {
            ServerArray serverarray = ServerArray.show(serverarrayID, null);
            Assert.IsNotNull(serverarray);
            Deployment dep = serverarray.deployment;
            Assert.IsNotNull(dep);
            Assert.IsTrue(dep.name.Length > 0);
        }

        [TestMethod]
        public void serverArrayTags()
        {
            ServerArray serverarray = ServerArray.show(serverarrayID, null);
            Assert.IsNotNull(serverarray);
            List<Tag> tags = serverarray.tags;
            Assert.IsTrue(true);
        }

        #region ServerArray.index tests
        [TestMethod]
        public void ServerArrayIndexSimple()
        {
            List<ServerArray> serverarrayList = ServerArray.index();
            Assert.IsNotNull(serverarrayList);
            Assert.IsTrue(serverarrayList.Count > 0);

        }

        [TestMethod]
        public void ServerArrayIndexFilteredString()
        {
            List<Filter> filter = Filter.parseFilterList(filterListString);
            
            List<ServerArray> serverarrayList = ServerArray.index(filter);
            Assert.IsNotNull(serverarrayList);
        }
        #endregion

        #region ServerArray.show tests
        [TestMethod]
        public void ServerArrayShow()
        {
            ServerArray serverarray = ServerArray.show(serverarrayID, null);
            Assert.IsNotNull(serverarray);
        }



        #endregion

        [TestMethod]
        public void serverArrayCloneDestroy()
        {
            string newServerArrayID = ServerArray.clone(serverarrayID);
            Assert.IsNotNull(newServerArrayID);
            bool retVal = ServerArray.destroy(newServerArrayID);
            Assert.IsTrue(retVal);
        }
    }
}
