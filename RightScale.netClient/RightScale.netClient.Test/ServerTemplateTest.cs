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
            Guid stNameID = Guid.NewGuid();
            string newServerTemplateID = ServerTemplate.clone(servertemplateid, "this is a new servertemplate " + stNameID.ToString());
            Assert.IsNotNull(newServerTemplateID);
            Assert.IsTrue(newServerTemplateID.Length > 0);
            bool destroyResult = ServerTemplate.destroy(newServerTemplateID);
            Assert.IsTrue(destroyResult);
        }

        [TestMethod]
        public void serverTemplateCloneFullDestroyTest()
        {
            Guid stNameID = Guid.NewGuid();
            string newServerTemplateID = ServerTemplate.clone(servertemplateid, "this is a new servertemplate  " + stNameID.ToString(), "this is a description");
            Assert.IsNotNull(newServerTemplateID);
            Assert.IsTrue(newServerTemplateID.Length > 0);
            bool destroyResult = ServerTemplate.destroy(newServerTemplateID);
            Assert.IsTrue(destroyResult);
        }
        #endregion

    }
}
