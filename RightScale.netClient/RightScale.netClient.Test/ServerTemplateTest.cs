using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Configuration;
using System.Collections.Generic;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class ServerTemplateTest
    {
        private string filterListString;
        private string servertemplateid;

        public ServerTemplateTest()
        {
           
            filterListString = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["ServerTemplateTest_filterListString"].ToString());
            servertemplateid = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["ServerTemplateTest_servertemplateid"].ToString());
        }

        #region Instance.index tests

        [TestMethod]
        public void indexSimpleTest()
        {
            
            List<ServerTemplate> servertemplateList = ServerTemplate.index();
            Assert.IsNotNull(servertemplateList);
            Assert.IsTrue(servertemplateList.Count > 0);
        }
        #endregion

        #region ServerTemplate.show tests

        [TestMethod]
        public void ServerTemplateShow()
        {
            ServerTemplate servertemplate = ServerTemplate.show(servertemplateid, null);
            Assert.IsNotNull(servertemplate);
        }

        #endregion

        #region ServerTemplate.clone tests
        [TestMethod]
        public void serverTemplateCloneDestroyTest()
        {
            string newServerTemplateID = ServerTemplate.clone(servertemplateid);
            Assert.IsNotNull(newServerTemplateID);

            bool destroyResult = ServerTemplate.destroy(newServerTemplateID);
            Assert.IsTrue(destroyResult);
        }

        #endregion

    }
}
