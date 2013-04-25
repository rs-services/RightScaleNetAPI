using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class ServerTest : RSAPITestBase
    {      
        private string cloudID;
        private string serverTemplateID;
        private string multiCloudImageID;
        private string instanceTypeID;
        private string launchTestServerID;
        private string windowsLaunchTestServerID;
        private int maxWaitLoops;

        public ServerTest()
        {
            cloudID = this.azureCloudID;
            serverTemplateID = ConfigurationManager.AppSettings["ServerTest_serverTemplateID"].ToString();
            multiCloudImageID = ConfigurationManager.AppSettings["ServerTest_multiCloudImageID"].ToString();
            instanceTypeID = ConfigurationManager.AppSettings["ServerTest_instanceTypeID"].ToString();
            launchTestServerID = ConfigurationManager.AppSettings["ServerTest_launchTerminateServerID"].ToString();
            windowsLaunchTestServerID = ConfigurationManager.AppSettings["ServerTest_winLaunchTerminateServerID"].ToString();
            maxWaitLoops = 10;
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
            List<Server> serverIndexDeploymentTest = Server.index_deployment(liveTestDeploymentID);
            Assert.IsNotNull(serverIndexDeploymentTest);
            Assert.IsTrue(serverIndexDeploymentTest.Count > 0);
        }

        #endregion
        
        #region Server.show tests

        [TestMethod]
        public void serverShowTest()
        {
            Server serverobj = Server.show(liveTestServerID);
            Assert.IsNotNull(serverobj);
        }

        [TestMethod]
        public void serverDeploymentShowTest()
        {
            Server serverobj = Server.show_deployment(liveTestServerID, liveTestDeploymentID);
            Assert.IsNotNull(serverobj);
        }

        #endregion 

        #region Server Relationship tests

        [TestMethod]
        public void serverTags()
        {
            Server serverobj = Server.show_deployment(liveTestServerID, liveTestDeploymentID);
            Assert.IsNotNull(serverobj);
            List<Tag> tags = serverobj.tags;
            Assert.IsTrue(true);//no exception
        }

        [TestMethod]
        public void serverNextInstanceExist()
        {
            Server serverobj = Server.show_deployment(liveTestServerID, liveTestDeploymentID);
            Assert.IsNotNull(serverobj);
            Instance instance = serverobj.nextInstance;
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void serverAlertSpecsExist()
        {
            Server serverobj = Server.show_deployment(liveTestServerID, liveTestDeploymentID);
            Assert.IsNotNull(serverobj);
            List<AlertSpec> alertSpecs = serverobj.alertSpecs;
            Assert.IsNotNull(alertSpecs);
        }

        [TestMethod]
        public void serverCurrentInstanceExists()
        {
            Server serverobj = Server.show(this.liveTestServerID);
            Assert.IsNotNull(serverobj);
            Instance instance = serverobj.currentInstance;
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void serverDeploymentExists()
        {
            Server serverobj = Server.show_deployment(liveTestServerID, liveTestDeploymentID);
            Assert.IsNotNull(serverobj);
            Deployment d = serverobj.deployment;
            Assert.IsNotNull(d);
        }

        #endregion

        [TestMethod]
        public void serverCloneDestroyTest()
        {
            string newServerID = Server.clone(liveTestServerID);
            Assert.IsNotNull(newServerID);
            bool destroyResult = Server.destroy(newServerID);
            Assert.IsTrue(destroyResult);
        }

        [TestMethod]
        public void serverCloneUpdateDestroyTest()
        {
            string newServerID = Server.clone(liveTestServerID);
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
            string newServerID = Server.create_deployment(liveTestDeploymentID, cloudID, serverTemplateID, "this is a server name");
            Assert.IsNotNull(newServerID);
            bool destroyRetVal = Server.destroy_deployment(newServerID, liveTestDeploymentID);
            Assert.IsTrue(destroyRetVal);
        }

        [TestMethod]
        public void serverCreateDeploymentDestroySimpleTest()
        {
            string newServerID = Server.create_deployment(liveTestDeploymentID, cloudID, serverTemplateID, "this is a server name");
            Assert.IsNotNull(newServerID);
            bool destroyRetVal = Server.destroy(newServerID);
            Assert.IsTrue(destroyRetVal);
        }

        [TestMethod]
        public void serverCreateDestroySimpleTest()
        {
            string newServerID = Server.create(cloudID, liveTestDeploymentID, serverTemplateID, "this is another test server name");
            Assert.IsNotNull(newServerID);
            bool destroyRetVal = Server.destroy(newServerID);
            Assert.IsTrue(destroyRetVal);
        }

        [TestMethod]
        public void serverCreateDestroyDeploymentSimpleTest()
        {
            string newServerID = Server.create(cloudID, liveTestDeploymentID, serverTemplateID, "this is another test server name");
            Assert.IsNotNull(newServerID);
            bool destroyRetVal = Server.destroy_deployment(newServerID, liveTestDeploymentID);
            Assert.IsTrue(destroyRetVal);
        }


        [TestMethod]
        public void serverCreateComplicatedDestroySimpleTest()
        {
            List<Input> inputs = new List<Input>();
            inputs.Add(new Input("ADMIN_PASSWORD", "text:thisisapassword!@#$%^"));
            string newServerID = Server.create(cloudID, liveTestDeploymentID, serverTemplateID, "complicated Server Instance", "this is a description...", cloudID, null, null, inputs, instanceTypeID, null, multiCloudImageID, null, null, null, null, false);
            Assert.IsNotNull(newServerID);
            bool delRetVal = Server.destroy(newServerID);
            Assert.IsTrue(delRetVal);
        }

        [TestMethod]
        public void serverCreateUpdateDestroySimpleTest()
        {
            string newServerID = Server.create(cloudID, liveTestDeploymentID, serverTemplateID, "this is another test server name");
            Assert.IsNotNull(newServerID);
            Server initialTest = Server.show(newServerID);
            Assert.IsNotNull(initialTest);
            bool updated = Server.update(newServerID, "new description", string.Empty, false);
            Assert.IsTrue(updated);
            Server updatedTest = Server.show(newServerID);
            Assert.IsNotNull(updatedTest);
            Assert.AreNotEqual(updatedTest.description, initialTest.description);
            bool destroyRetVal = Server.destroy_deployment(newServerID, liveTestDeploymentID);
            Assert.IsTrue(destroyRetVal);
        }

        [TestMethod]
        public void serverLaunchTerminate()
        {
            string currentState = Server.show(launchTestServerID).state;
            int waitLoops = 0;

            if (currentState != "inactive")
            {
                try
                {
                    Server.terminate(launchTestServerID);
                }
                catch
                {

                }
            }

            while (currentState != "inactive" && (waitLoops <= maxWaitLoops || maxWaitLoops < 0))
            {
                Thread.Sleep(5000);
                currentState = Server.show(launchTestServerID).state;
                waitLoops++;
            }
            if (waitLoops >= maxWaitLoops && maxWaitLoops >= 0)
            {
                Assert.Fail("Cannot start test because server is not in an available state.  Test has not started");
            }

            bool result = Server.launch(launchTestServerID);
            Assert.IsTrue(result);

            currentState = Server.show(launchTestServerID).state;

            while (currentState == "queued")
            {
                Thread.Sleep(5000);
                currentState = Server.show(launchTestServerID).state;
            }
            
            bool terminateResult = Server.terminate(launchTestServerID);
            Assert.IsTrue(terminateResult);

            currentState = Server.show(launchTestServerID).state;

            while (currentState != "inactive")
            {
                Thread.Sleep(5000);
                currentState = Server.show(launchTestServerID).state;
            }
        }

        [TestMethod]
        public void windowsServerLaunchTerminate()
        {
            string currentState = Server.show(windowsLaunchTestServerID).state;
            int waitLoops = 0;
            if (currentState != "inactive" && waitLoops <= maxWaitLoops) // check to make sure machine is in an inactive state
            {
                Thread.Sleep(30000);
                currentState = Server.show(windowsLaunchTestServerID).state;
                waitLoops++;
            }
            if (waitLoops >= maxWaitLoops)
            {
                Assert.Fail("Cannot start test because server is not in an available state.  Test has not started");
            }

            List<Input> inputs = new List<Input>();
            inputs.Add(new Input("REMOTE_STORAGE_ACCOUNT_ID_APP", "cred:azureStorage_AccountName"));
            inputs.Add(new Input("REMOTE_STORAGE_ACCOUNT_PROVIDER_APP", "text:Windows_Azure_Storage"));
            inputs.Add(new Input("REMOTE_STORAGE_ACCOUNT_SECRET_APP", "cred:azureStorage_AccountKey"));
            inputs.Add(new Input("REMOTE_STORAGE_CONTAINER_APP", "text:media"));
            inputs.Add(new Input("ZIP_FILE_NAME", "text:Build_20130219094040.zip"));
            inputs.Add(new Input("BACKUP_FILE_NAME", "text:mileagestatsdata_sql2012.bak"));
            inputs.Add(new Input("DB_LINEAGE_NAME", "text:thisisadblineage"));
            inputs.Add(new Input("DB_NAME", "text:MileageStatsData"));
            inputs.Add(new Input("DB_NEW_LOGIN_NAME", "text:patrick"));
            inputs.Add(new Input("DB_NEW_LOGIN_PASSWORD", "text:P@ssword1"));
            inputs.Add(new Input("LOGS_VOLUME_SIZE", "text:10"));
            inputs.Add(new Input("MSSQL_PRODUCT_KEY", "cred:mssql_SQLStandardKey"));
            inputs.Add(new Input("REMOTE_STORAGE_ACCOUNT_ID", "cred:azureStorage_AccountName"));
            inputs.Add(new Input("REMOTE_STORAGE_ACCOUNT_PROVIDER", "text:Windows_Azure_Storage"));
            inputs.Add(new Input("REMOTE_STORAGE_ACCOUNT_SECRET", "cred:azureStorage_AccountKey"));
            inputs.Add(new Input("REMOTE_STORAGE_CONTAINER", "text:media"));
            inputs.Add(new Input("ADMIN_PASSWORD", "text:P@ssword1"));
            inputs.Add(new Input("SYS_WINDOWS_TZINFO", "text:Eastern Standard Time"));

            bool result = Server.launch(windowsLaunchTestServerID, inputs);
            Assert.IsTrue(result);

            currentState = Server.show(windowsLaunchTestServerID).state;

            while (currentState == "queued")
            {
                Thread.Sleep(5000);
                currentState = Server.show(launchTestServerID).state;
            }
            Assert.IsTrue(currentState != "queued");

            bool terminateResult = Server.terminate(windowsLaunchTestServerID);
            Assert.IsTrue(terminateResult);

            currentState = Server.show(windowsLaunchTestServerID).state;

            while (currentState != "inactive")
            {
                Thread.Sleep(5000);
                currentState = Server.show(windowsLaunchTestServerID).state;
            }
            Assert.IsTrue(currentState == "inactive");
        }
    }
}
