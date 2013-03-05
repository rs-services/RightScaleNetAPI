using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RightScale.netClient;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class AlertSpecTest
    {
        string serverID;
        string serverTemplateID;
        string serverArrayID;
        public AlertSpecTest()
        {
            serverID = ConfigurationManager.AppSettings["AlertSpecTest_serverID"].ToString();
            serverTemplateID = ConfigurationManager.AppSettings["AlertSpecTest_serverTemplateID"].ToString();
            serverArrayID = ConfigurationManager.AppSettings["AlertSpecTest_serverArrayID"].ToString();
        }

        [TestMethod]
        public void createReadUpdateDeleteAlertSpec_forServerTemplate()
        {
            string alertSpecID = AlertSpec.create(">", "this is a description", "5", string.Empty, "cpu-0/cpu-idle", "alert spec name", "/api/server_templates/" + this.serverTemplateID, "5", "value", "vote tag", "grow");
            Assert.IsNotNull(alertSpecID);
            AlertSpec testAlertSpec = AlertSpec.show(alertSpecID);
            Assert.IsNotNull(testAlertSpec);
            bool updated = AlertSpec.update("", "this is a new description", "", "", "", "", "", "", "", "", "", alertSpecID);
            Assert.IsTrue(updated);
            AlertSpec testUpdatedAlertSpec = AlertSpec.show(alertSpecID);
            Assert.IsNotNull(testUpdatedAlertSpec);
            Assert.AreNotEqual<string>(testAlertSpec.description, testUpdatedAlertSpec.description);
            bool retVal = AlertSpec.destroy(alertSpecID);
            Assert.IsTrue(retVal);
        }

        [TestMethod]
        public void createReadUpdateDeleteAlertSpec_forServerArray()
        {
            string alertSpecID = AlertSpec.create(">", "this is a description", "5", string.Empty, "cpu-0/cpu-idle", "alert spec name", "/api/server_arrays/" + this.serverArrayID, "5", "value", "vote tag", "grow");
            Assert.IsNotNull(alertSpecID);
            AlertSpec testAlertSpec = AlertSpec.show(alertSpecID);
            Assert.IsNotNull(testAlertSpec);
            bool updated = AlertSpec.update("", "this is a new description", "", "", "", "", "", "", "", "", "", alertSpecID);
            Assert.IsTrue(updated);
            AlertSpec testUpdatedAlertSpec = AlertSpec.show(alertSpecID);
            Assert.IsNotNull(testUpdatedAlertSpec);
            Assert.AreNotEqual<string>(testAlertSpec.description, testUpdatedAlertSpec.description);
            bool retVal = AlertSpec.destroy(alertSpecID);
            Assert.IsTrue(retVal);
        }

        [TestMethod]
        public void createReadUpdateDeleteAlertSpec_forServer()
        {
            string alertSpecID = AlertSpec.create(">", "this is a description", "5", string.Empty, "cpu-0/cpu-idle", "alert spec name", "/api/servers/" + this.serverID, "5", "value", "vote tag", "grow");
            Assert.IsNotNull(alertSpecID);
            AlertSpec testAlertSpec = AlertSpec.show(alertSpecID);
            Assert.IsNotNull(testAlertSpec);
            bool updated = AlertSpec.update("", "this is a new description", "", "", "", "", "", "", "", "", "", alertSpecID);
            Assert.IsTrue(updated);
            AlertSpec testUpdatedAlertSpec = AlertSpec.show(alertSpecID);
            Assert.IsNotNull(testUpdatedAlertSpec);
            Assert.AreNotEqual<string>(testAlertSpec.description, testUpdatedAlertSpec.description);
            bool retVal = AlertSpec.destroy(alertSpecID);
            Assert.IsTrue(retVal);
        }

        [TestMethod]
        public void createReadUpdateDeleteAlertSpecServer()
        {
            string alertSpecID = AlertSpec.create_server(">", "this is a description", "5", string.Empty, "cpu-0/cpu-idle", "alert spec name", string.Empty, "5", "value", "vote tag", "grow", serverID);
            Assert.IsNotNull(alertSpecID);
            AlertSpec testAlertSpec = AlertSpec.show_server(alertSpecID, serverID);
            Assert.IsNotNull(testAlertSpec);
            bool updated = AlertSpec.update_server("", "this is a new description", "", "", "", "", "", "", "", "", "", alertSpecID, serverID);
            Assert.IsTrue(updated);
            AlertSpec testUpdatedAlertSpec = AlertSpec.show_serverTemplate(alertSpecID, serverID);
            Assert.IsNotNull(testUpdatedAlertSpec);
            Assert.AreNotEqual<string>(testAlertSpec.description, testUpdatedAlertSpec.description);
            bool retVal = AlertSpec.destroy_server(alertSpecID, serverID);
            Assert.IsTrue(retVal);
        }

        [TestMethod]
        public void createReadUpdateDeleteAlertSpecServerTemplate()
        {
            string alertSpecID = AlertSpec.create_serverTemplate(">", "this is a description", "5", string.Empty, "cpu-0/cpu-idle", "alert spec name", string.Empty, "5", "value", "vote tag", "grow", serverTemplateID);
            Assert.IsNotNull(alertSpecID);
            AlertSpec testAlertSpec = AlertSpec.show_serverTemplate(alertSpecID, serverTemplateID);
            Assert.IsNotNull(testAlertSpec);
            bool updated = AlertSpec.update_serverTemplate("", "this is a new description", "", "", "", "", "", "", "", "", "", alertSpecID, serverTemplateID);
            Assert.IsTrue(updated);
            AlertSpec testUpdatedAlertSpec = AlertSpec.show_serverTemplate(alertSpecID, serverTemplateID);
            Assert.IsNotNull(testUpdatedAlertSpec);
            Assert.AreNotEqual<string>(testAlertSpec.description, testUpdatedAlertSpec.description);
            bool retVal = AlertSpec.destroy_serverTemplate(alertSpecID, serverTemplateID);
            Assert.IsTrue(retVal);
        }

        [TestMethod]
        public void createReadUpdateDeleteAlertSpecServerArray()
        {
            string alertSpecID = AlertSpec.create_serverArray(">", "this is a description", "5", string.Empty, "cpu-0/cpu-idle", "alert spec name", string.Empty, "5", "value", "vote tag", "grow", serverArrayID);
            Assert.IsNotNull(alertSpecID);
            AlertSpec testAlertSpec = AlertSpec.show_serverArray(alertSpecID, serverArrayID);
            Assert.IsNotNull(testAlertSpec);
            bool updated = AlertSpec.update_serverArray("", "this is a new description", "", "", "", "", "", "", "", "", "", alertSpecID, serverArrayID);
            Assert.IsTrue(updated);
            AlertSpec testUpdatedAlertSpec = AlertSpec.show_serverTemplate(alertSpecID, serverArrayID);
            Assert.IsNotNull(testUpdatedAlertSpec);
            Assert.AreNotEqual<string>(testAlertSpec.description, testUpdatedAlertSpec.description);
            bool retVal = AlertSpec.destroy_serverArray(alertSpecID, serverTemplateID);
            Assert.IsTrue(retVal);
        }
    }
}
