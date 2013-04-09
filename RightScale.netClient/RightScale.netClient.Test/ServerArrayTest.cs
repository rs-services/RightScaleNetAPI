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

        private string cloudID;
        private string serverTemplateID;
        private string deploymentID;

        public ServerArrayTest()
        {
            filterListString = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["ServerArrayTest_filterListString"].ToString());
            serverarrayView = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["ServerArrayTest_serverarrayview"].ToString());
            serverarrayID = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["ServerArrayTest_serverarrayid"].ToString());
            cloudID = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["ServerArrayTest_cloudID"].ToString());
            serverTemplateID = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["ServerArrayTest_serverTemplateID"].ToString());
            deploymentID = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["ServerArrayTest_deploymentID"].ToString());
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
        
        [TestMethod]
        public void serverArrayCreateDestroy()
        {
            string array_type = "alert";
            List<ElasticityParam> elasticityParams = new List<ElasticityParam>();
            elasticityParams.Add(new ElasticityParam(new AlertSpecificParam("voterTagPredicate", "80"), new Bound(1, 2), new Pacing(1, 1, 15), new List<ScheduleEntry>()));
            string newArrayID = ServerArray.create(array_type, new List<DataCenterPolicy>(), elasticityParams, cloudID, deploymentID, serverTemplateID, "API Test Array", "disabled");
            Assert.IsNotNull(newArrayID);
            Assert.IsTrue(newArrayID.Length > 0);
            bool isDeleted = ServerArray.destroy(newArrayID);
            Assert.IsTrue(isDeleted);
        }

        //this is just a cleanup method--leaving it in place so that if/when I need to clean out a bunch of extra arrays from my acct it's here.
        //[TestMethod]
        public void cleanOutOldServerArrays()
        {
            int i = 0;
            List<ServerArray> serverArrays = ServerArray.index();
            foreach (ServerArray sa in serverArrays)
            {
                if (sa.name.Contains("API Testing ST"))
                {
                    if (ServerArray.destroy(sa.ID))
                    {
                        i++;
                    }
                    else
                    {

                    }
                }
            }
            Assert.IsTrue(i > 0);
        }
    }
}
