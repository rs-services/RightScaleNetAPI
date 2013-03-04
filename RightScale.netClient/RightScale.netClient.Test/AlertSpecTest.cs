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
        string serverAlertSpecID;
        string serverTemplateID;
        string serverTemplateAlertSpecID;
        string serverArrayID;
        string serverArrayAlertSpecID;

        public AlertSpecTest()
        {
            serverID = ConfigurationManager.AppSettings["AlertSpecTest_serverID"].ToString();
            serverAlertSpecID = ConfigurationManager.AppSettings["AlertSpecTest_serverAlertSpecID"].ToString();
            serverTemplateID = ConfigurationManager.AppSettings["AlertSpecTest_serverTemplateID"].ToString();
            serverTemplateAlertSpecID = ConfigurationManager.AppSettings["AlertSpecTest_serverTemplateAlertSpecID"].ToString();
            serverArrayID = ConfigurationManager.AppSettings["AlertSpecTest_serverArrayID"].ToString();
            serverArrayAlertSpecID = ConfigurationManager.AppSettings["AlertSpecTest_serverArrayAlertSpecID"].ToString();
        }

        [TestMethod]
        public void showServerAlertSpec()
        {
            AlertSpec alertSpec = AlertSpec.show_server(serverID, serverAlertSpecID);
            Assert.IsNotNull(alertSpec, "Server-based alert spec is null");
        }

        [TestMethod]
        public void showServerTemplateAlertSpec()
        {
            AlertSpec alertSpec = AlertSpec.show_serverTemplate(serverTemplateID, serverTemplateAlertSpecID);
            Assert.IsNotNull(alertSpec, "ServerTemplate-based alert spec is null");
        }

        [TestMethod]
        public void showServerArrayAlertSpec()
        {
            AlertSpec alertSpec = AlertSpec.show_serverArray(serverArrayID, serverArrayAlertSpecID);
            Assert.IsNotNull(alertSpec, "ServerArray-based alert spec is null");
        }

        [TestMethod]
        public void showAlertSpec()
        {
            AlertSpec alertSpec_serverArray = AlertSpec.show(serverArrayAlertSpecID);
            Assert.IsNotNull(alertSpec_serverArray, "Default AlertSpec for ServerArray is null");
            AlertSpec alertSpec_serverTemplate = AlertSpec.show(serverTemplateAlertSpecID);
            Assert.IsNotNull(alertSpec_serverTemplate, "Default AlertSpec for ServerTemplate is null");
            AlertSpec alertSpec_server = AlertSpec.show(serverAlertSpecID);
            Assert.IsNotNull(alertSpec_server, "Default AlertSpec for Server is null");
        }
    }
}
