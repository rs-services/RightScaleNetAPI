using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class ServerTemplateMultiCloudImageTest
    {
        string serverTemplateID;
        string multiCloudImageID;
        string serverTemplateMultiCloudImageID;
        string newMciID;

        public ServerTemplateMultiCloudImageTest()
        {
            serverTemplateID = ConfigurationManager.AppSettings["ServerTemplateMultiCloudImageTest_serverTempalteID"].ToString();
            multiCloudImageID = ConfigurationManager.AppSettings["ServerTemplateMultiCloudImageTest_multiCloudImageID"].ToString();
            serverTemplateMultiCloudImageID = ConfigurationManager.AppSettings["ServerTemplateMultiCloudImageTest_ID"].ToString();
            newMciID = ConfigurationManager.AppSettings["ServerTemplateMultiCloudImageTest_NewMCIID"].ToString();
        }

        #region ServerTemplateMultiCloudImageTest index tests

        [TestMethod]
        public void indexSimpleTest()
        {
            List<ServerTemplateMultiCloudImage> stmcis = ServerTemplateMultiCloudImage.index();
            Assert.IsNotNull(stmcis);
            Assert.IsTrue(stmcis.Count > 0);
        }

        [TestMethod]
        public void indexViewTest()
        {
            List<ServerTemplateMultiCloudImage> stmcis = ServerTemplateMultiCloudImage.index("default");
            Assert.IsNotNull(stmcis);
            Assert.IsTrue(stmcis.Count > 0);            
        }

        [TestMethod]
        public void indexBadViewTest()
        {
            try
            {
                List<ServerTemplateMultiCloudImage> stmcis = ServerTemplateMultiCloudImage.index("thisinotaview");
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void indexFilterMCITest()
        {
            List<Filter> filters = new List<Filter>();
            filters.Add(new Filter("multi_cloud_image_href", FilterOperator.Equal, Utility.multiCloudImageHref(multiCloudImageID)));
            List<ServerTemplateMultiCloudImage> stmcis = ServerTemplateMultiCloudImage.index(filters);
            Assert.IsNotNull(stmcis);
            Assert.IsTrue(stmcis.Count > 0);
        }
        
        [TestMethod]
        public void indexFilterSTTest()
        {
            List<Filter> filters = new List<Filter>();
            filters.Add(new Filter("server_template_href", FilterOperator.Equal, Utility.serverTemplateHref(serverTemplateID)));
            List<ServerTemplateMultiCloudImage> stmcis = ServerTemplateMultiCloudImage.index(filters);
            Assert.IsNotNull(stmcis);
            Assert.IsTrue(stmcis.Count > 0);
        }
        
        [TestMethod]
        public void indexFilterIsDefaultTest()
        {
            List<Filter> filters = new List<Filter>();
            filters.Add(new Filter("is_default", FilterOperator.Equal, true.ToString().ToLower()));
            List<ServerTemplateMultiCloudImage> stmcis = ServerTemplateMultiCloudImage.index(filters);
            Assert.IsNotNull(stmcis);
            Assert.IsTrue(stmcis.Count > 0);
        }
        
        [TestMethod]
        public void indexFilterIsNotDefaultTest()
        {
            List<Filter> filters = new List<Filter>();
            filters.Add(new Filter("is_default", FilterOperator.Equal, false.ToString().ToLower()));
            List<ServerTemplateMultiCloudImage> stmcis = ServerTemplateMultiCloudImage.index(filters);
            Assert.IsNotNull(stmcis);
            Assert.IsTrue(stmcis.Count > 0);
        }

        #endregion

        #region ServerTemplateMultiCloudImageTest show tests

        [TestMethod]
        public void showTest()
        {
            ServerTemplateMultiCloudImage stmci = ServerTemplateMultiCloudImage.show(serverTemplateMultiCloudImageID);
            Assert.IsNotNull(stmci);
        }

        #endregion

        #region ServerTemplateMultiCloudImageTest Create Delete tests

        [TestMethod]
        public void createDeleteTest()
        {
            prepSTMCI();

            string retVal = ServerTemplateMultiCloudImage.create(newMciID, serverTemplateID);
            Assert.IsNotNull(retVal);
            Assert.IsTrue(retVal.Length > 0);
            bool isDeleted = ServerTemplateMultiCloudImage.destroy(retVal);
            Assert.IsTrue(isDeleted);
        }
        
        #endregion

        #region ServerTemplateMultiCloudImageTest Update tests

        [TestMethod]
        public void createUpdateDeleteTest()
        {
            prepSTMCI();

            string retVal = ServerTemplateMultiCloudImage.create(newMciID, serverTemplateID);
            Assert.IsNotNull(retVal);
            Assert.IsTrue(retVal.Length > 0);

            bool isUpdated = ServerTemplateMultiCloudImage.make_default(retVal);
            Assert.IsTrue(isUpdated);
            bool isUpdatedAgain = ServerTemplateMultiCloudImage.make_default(serverTemplateMultiCloudImageID);
            Assert.IsTrue(isUpdatedAgain);

            bool isDeleted = ServerTemplateMultiCloudImage.destroy(retVal);
            Assert.IsTrue(isDeleted);
        }
        #endregion


        private void prepSTMCI()
        {
            List<Filter> filter = new List<Filter>();
            filter.Add(new Filter("multi_cloud_image_href", FilterOperator.Equal, Utility.multiCloudImageHref(newMciID)));
            filter.Add(new Filter("server_template_href", FilterOperator.Equal, Utility.serverTemplateHref(serverTemplateID)));
            List<ServerTemplateMultiCloudImage> stmci = ServerTemplateMultiCloudImage.index(filter);
            if (stmci.Count == 1)
            {
                bool predelete = ServerTemplateMultiCloudImage.destroy(stmci[0].ID);
                Assert.IsTrue(predelete);
            }
        }
    }
}
