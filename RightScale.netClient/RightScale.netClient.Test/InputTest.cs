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

        public InputTest()
        {
           
            deploymentID = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["InputTest_deploymentid"].ToString());
            servertemplateID = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["InputTest_servertemplateid"].ToString());
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

    }
}
