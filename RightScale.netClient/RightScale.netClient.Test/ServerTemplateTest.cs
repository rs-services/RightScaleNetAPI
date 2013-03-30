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

        #region ServerTemplate relationship tests

        [TestMethod]
        public void serverTemplateTags()
        {
            ServerTemplate servertemplate = ServerTemplate.show(servertemplateid, null);
            Assert.IsNotNull(servertemplate);
            List<Tag> tags = servertemplate.tags;
            Assert.IsTrue(true);            
        }

        [TestMethod]
        public void serverTemplateMultiCloudImages()
        {
            ServerTemplate servertemplate = ServerTemplate.show(servertemplateid, null);
            Assert.IsNotNull(servertemplate);
            List<MultiCloudImage> mcis = servertemplate.multiCloudImages;
            Assert.IsNotNull(mcis);
            Assert.IsTrue(mcis.Count > 0);
        }

        [TestMethod]
        public void serverTemplateAlertSpecs()
        {
            ServerTemplate servertemplate = ServerTemplate.show(servertemplateid, null);
            Assert.IsNotNull(servertemplate);
            List<AlertSpec> aspecs = servertemplate.alertSpecs;
            Assert.IsNotNull(aspecs);            
        }

        [TestMethod]
        public void serverTemplatePublication()
        {
            ServerTemplate servertemplate = ServerTemplate.show(servertemplateid, null);
            Assert.IsNotNull(servertemplate);
            Publication pub = servertemplate.publication;
            Assert.IsTrue(true);//no exception            
        }

        [TestMethod]
        public void serverTemplateDefaultMCI()
        {
            ServerTemplate servertemplate = ServerTemplate.show(servertemplateid, null);
            Assert.IsNotNull(servertemplate);
            MultiCloudImage mci = servertemplate.defaultMultiCloudImage;
            Assert.IsNotNull(mci);
            Assert.IsTrue(mci.name.Length > 0);
        }

        #endregion

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

        #region ServerTemplate.create tests

        [TestMethod]
        public void serverTemplateCreateDestroy()
        {
            Guid stNameID = Guid.NewGuid();
            string newSTID = ServerTemplate.create("new server template " + stNameID.ToString(), "this is a new description");
            Assert.IsNotNull(newSTID);
            Assert.IsTrue(newSTID.Length > 0);
            bool destroyResult = ServerTemplate.destroy(newSTID);
            Assert.IsTrue(destroyResult);
        }

        [TestMethod]
        public void serverTemplateCreateDestroySimple()
        {
            Guid stNameID = Guid.NewGuid();
            string newSTID = ServerTemplate.create("new server template " + stNameID.ToString());
            Assert.IsNotNull(newSTID);
            Assert.IsTrue(newSTID.Length > 0);
            bool destroyResult = ServerTemplate.destroy(newSTID);
            Assert.IsTrue(destroyResult);
        }
        #endregion


        #region ServerTemplate.commit tests

        [TestMethod]
        public void serverTemplateCreateCommitDestroySimple()
        {
            Guid stNameID = Guid.NewGuid();
            string newSTID = ServerTemplate.create("new server template " + stNameID.ToString());
            Assert.IsNotNull(newSTID);
            Assert.IsTrue(newSTID.Length > 0);
            string newHeadSTID = ServerTemplate.commit(newSTID, true, "Committing for API Unit Tests", true);
            Assert.IsNotNull(newHeadSTID);
            Assert.IsTrue(newHeadSTID.Length > 0);
            Assert.IsTrue(newSTID != newHeadSTID);
            bool destroyResult = ServerTemplate.destroy(newHeadSTID);
            Assert.IsTrue(destroyResult);
        }
        
        #endregion
    }
}
