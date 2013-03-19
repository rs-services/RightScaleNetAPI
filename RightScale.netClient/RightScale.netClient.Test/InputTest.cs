using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Configuration;
using System.Collections.Generic;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class InputTest
    {

        private string deploymentID;
        private string servertemplateID;
        private string serverID;

        public InputTest()
        {
           
            deploymentID = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["InputTest_deploymentid"].ToString());
            servertemplateID = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["InputTest_servertemplateid"].ToString());
            serverID = ConfigurationManager.AppSettings["InputTest_serverid"].ToString();
        }

        #region Input.index tests

        [TestMethod]
        public void index_deploymentSimpleTest()
        {

            List<Input> inputlist = Input.index_deployment(deploymentID,string.Empty);
            Assert.IsNotNull(inputlist);
            Assert.IsTrue(inputlist.Count > 0);

        }


        [TestMethod]
        public void index_servertemplateSimpleTest()
        {

            List<Input> inputlist = Input.index_servertemplate(servertemplateID, string.Empty);
            Assert.IsNotNull(inputlist);
            Assert.IsTrue(inputlist.Count > 0);

        }
        #endregion

        #region Input.multi_update tests

        [TestMethod]
        public void deploymentInputMultiUpdate()
        {
            List<Input> newInputs = new List<Input>();
            newInputs.Add(new Input("DB_NAME", "text:MileageStatsData"));
            newInputs.Add(new Input("DB_NEW_LOGIN_NAME", "text:patrick"));
            newInputs.Add(new Input("DB_NEW_LOGIN_PASSWORD", "text:P@ssword1"));
            bool retval = Input.multi_update_deployment(deploymentID, newInputs);
            Assert.IsTrue(retval);
        }

        [TestMethod]
        public void instanceInputMultiUpdate()
        {
            List<Input> newInputs = new List<Input>();
            newInputs.Add(new Input("DB_NAME", "text:MileageStatsData"));
            newInputs.Add(new Input("DB_NEW_LOGIN_NAME", "text:patrick"));
            newInputs.Add(new Input("DB_NEW_LOGIN_PASSWORD", "text:P@ssword1"));
            Server svr = Server.show(serverID);
            Assert.IsNotNull(svr);
            string nextInstanceID = svr.nextInstance.ID;
            Assert.IsNotNull(nextInstanceID);
            Assert.IsTrue(nextInstanceID.Length > 0);
            bool retval = Input.multi_update_instance(svr.nextInstance.cloud.ID, nextInstanceID, newInputs);
            Assert.IsTrue(retval);
        }

        [TestMethod]
        public void serverTemplateInputMultiUpdate()
        {
            List<Input> newInputs = new List<Input>();
            newInputs.Add(new Input("DB_NAME", "text:"));
            newInputs.Add(new Input("DB_NEW_LOGIN_NAME", "text:"));
            newInputs.Add(new Input("DB_NEW_LOGIN_PASSWORD", "text:"));
            newInputs.Add(new Input("LOGS_VOLUME_SIZE", "text:10"));
            bool retVal = Input.multi_update_serverTemplate(servertemplateID, newInputs);
            Assert.IsTrue(retVal);
        }

        #endregion
    }
}
