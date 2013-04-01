using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using System.Web;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class MultiCloudImageTest
    {
        private string filterListString;
        private string multicloudimageid;

        private string multicloudimageidupdate;
        private string multicloudimageiddestroy;
        private string serverTemplateID;

        public MultiCloudImageTest()
        {
            filterListString = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["MultiCloudImageTest_filterListString"].ToString());
            multicloudimageid = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["MultiCloudImageTest_imageid"].ToString());
            multicloudimageidupdate = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["MultiCloudImageTestUpdate_imageid"].ToString());
            multicloudimageiddestroy = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["MultiCloudImageTestDestroy_imageid"].ToString());

            serverTemplateID = ConfigurationManager.AppSettings["MultiCloudImageTest_ServerTemplateID"].ToString();

        }

        [TestMethod]
        public void multiCloudImageTags()
        {
            MultiCloudImage multicloudimage = MultiCloudImage.show(multicloudimageid);
            Assert.IsNotNull(multicloudimage);
            List<Tag> tags = multicloudimage.tags;
            Assert.IsTrue(true);//no exception            
        }

        [TestMethod]
        public void multiCloudImageMultiCloudImageSettings()
        {
            
            MultiCloudImage multicloudimage = MultiCloudImage.show(multicloudimageid);
            Assert.IsNotNull(multicloudimage);
            List<MultiCloudImageSetting> settings = multicloudimage.settings;
            Assert.IsTrue(true);
        }

        #region MultiCloudImage.Index tests

        [TestMethod]
        public void MultiCloudImageIndexSimple()
        {
            List<MultiCloudImage> mciImageList = MultiCloudImage.index(new List<Filter>());
            Assert.IsNotNull(mciImageList);
            Assert.IsTrue(mciImageList.Count > 0);
        }

        [TestMethod]
        public void MultiCloudImageIndexFilteredString()
        {
            List<Filter> filters = new List<Filter>();
            filters.AddRange(Filter.parseFilterList(filterListString));
            List<MultiCloudImage> multicloudImageList = MultiCloudImage.index(filters);

            Assert.IsNotNull(multicloudImageList);
        }

        #endregion

        #region MultiCloudImage.show tests
        [TestMethod]
        public void MultiCloudImageShow()
        {
            MultiCloudImage multicloudimage = MultiCloudImage.show(multicloudimageid);
            Assert.IsNotNull(multicloudimage);
        }

        #endregion

        #region MultiCloudImage.create tests
        [TestMethod]
        public void MultiCloudImageCreate()
        {
            string resltVal = MultiCloudImage.create("MCIUnitTest");
            Assert.IsNotNull(resltVal);
            Assert.IsFalse(string.IsNullOrEmpty(resltVal));
            Assert.IsFalse(string.IsNullOrWhiteSpace(resltVal));           
        }


        #endregion

        #region MultiCloudImage.update tests
        [TestMethod]
        public void MultiCloudImageUpdate()
        {
            bool resltVal = MultiCloudImage.update(multicloudimageidupdate,"MCIUpdateTest","MCI Update Unit Test");

            Assert.IsNotNull(resltVal);
            Assert.IsTrue(resltVal);
        }


        #endregion

        #region MultiCloudImage.clone tests
        //MCI clone test
        [TestMethod]
        public void MultiCloudImageClone()
       {
           string guidID = Guid.NewGuid().ToString();
            string resltVal = MultiCloudImage.clone(multicloudimageidupdate,"MCI Clone Test " + guidID);

            Assert.IsNotNull(resltVal);
            Assert.IsFalse(string.IsNullOrEmpty(resltVal));
            Assert.IsFalse(string.IsNullOrWhiteSpace(resltVal));
        }

        [TestMethod]
        public void MultiCloudImageCloneFull()
        {
            string guidID = Guid.NewGuid().ToString();
            string resltVal = MultiCloudImage.clone(multicloudimageidupdate, "MCI Clone Test " + guidID, "this is a description");

            Assert.IsNotNull(resltVal);
            Assert.IsFalse(string.IsNullOrEmpty(resltVal));
            Assert.IsFalse(string.IsNullOrWhiteSpace(resltVal));
        }


        #endregion

        #region MultiCloudImage.commit tests
        [TestMethod]
        public void MultiCloudImageCommit()
        {
            string resltVal = MultiCloudImage.commit(multicloudimageidupdate, "MCI Unit Test Commit");

            Assert.IsNotNull(resltVal);
            Assert.IsFalse(string.IsNullOrEmpty(resltVal));
            Assert.IsFalse(string.IsNullOrWhiteSpace(resltVal));
        }
        #endregion

        #region MultiCloudImage.destroy tests
        [TestMethod]
        public void MultiCloudImageDestroy()
        {
            bool resltVal = MultiCloudImage.destroy(multicloudimageiddestroy);

            Assert.IsNotNull(resltVal);
            Assert.IsTrue(resltVal);
        }


        #endregion

        #region MultiCloudImage.create .destroy tests

        [TestMethod]
        public void createDestroyMCISimple()
        {
            string mciID = MultiCloudImage.create("new MCI");
            Assert.IsNotNull(mciID);
            Assert.IsTrue(mciID.Length > 0);
            bool isDeleted = MultiCloudImage.destroy(mciID);
            Assert.IsTrue(isDeleted);
        }

        [TestMethod]
        public void createDestroyMCITest()
        {
            string mciID = MultiCloudImage.create("new MCI", "this is a description");
            Assert.IsNotNull(mciID);
            Assert.IsTrue(mciID.Length > 0);
            bool isDeleted = MultiCloudImage.destroy(mciID);
            Assert.IsTrue(isDeleted);
        }

        [TestMethod]
        public void createDestroySTMCISimple()
        {
            string mciID = MultiCloudImage.create_serverTemplate(serverTemplateID, "new MCI");
            Assert.IsNotNull(mciID);
            Assert.IsTrue(mciID.Length > 0);
            bool isDeleted = MultiCloudImage.destroy_ServerTemplate(serverTemplateID, mciID);
            Assert.IsTrue(isDeleted);
        }

        [TestMethod]
        public void createDestroySTMCITest()
        {
            string mciID = MultiCloudImage.create_serverTemplate(serverTemplateID, "new MCI", "this is a description");
            Assert.IsNotNull(mciID);
            Assert.IsTrue(mciID.Length > 0);
            bool isDeleted = MultiCloudImage.destroy_ServerTemplate(serverTemplateID, mciID);
            Assert.IsTrue(isDeleted);
        }

        [TestMethod]
        public void createSTMCIdestroyMCISimple()
        {
            string mciID = MultiCloudImage.create_serverTemplate(serverTemplateID, "new MCI");
            Assert.IsNotNull(mciID);
            Assert.IsTrue(mciID.Length > 0);
            bool isDeleted = MultiCloudImage.destroy(mciID);
            Assert.IsTrue(isDeleted);
        }
       
        [TestMethod]
        public void createSTMCIdestroyMCITest()
        {
            string mciID = MultiCloudImage.create_serverTemplate(serverTemplateID, "new MCI", "this is a description");
            Assert.IsNotNull(mciID);
            Assert.IsTrue(mciID.Length > 0);
            bool isDeleted = MultiCloudImage.destroy(mciID);
            Assert.IsTrue(isDeleted);
        }

        #endregion

        #region MultiCloudImage.clone .destroy tests

        [TestMethod]
        public void mciCloneDestroy()
        {
            string mciID = MultiCloudImage.clone(multicloudimageid, "this is a new MCI");
            Assert.IsNotNull(mciID);
            Assert.IsTrue(mciID.Length > 0);
            bool isDeleted = MultiCloudImage.destroy(mciID);
            Assert.IsTrue(isDeleted);
        }

        [TestMethod]
        public void mciCloneDescriptionDestroy()
        {    
            string mciID = MultiCloudImage.clone(multicloudimageid, "this is a new MCI", "here's the description");
            Assert.IsNotNull(mciID);
            Assert.IsTrue(mciID.Length > 0);
            bool isDeleted = MultiCloudImage.destroy(mciID);
            Assert.IsTrue(isDeleted);
        }

        #endregion

        #region MultiCloudImage.create .update .destroy tests

        [TestMethod]
        public void createUpdateDestroyMCISimple()
        {
            string mciID = MultiCloudImage.create("new MCI");
            Assert.IsNotNull(mciID);
            Assert.IsTrue(mciID.Length > 0);
            MultiCloudImage mci1 = MultiCloudImage.show(mciID);
            Assert.IsNotNull(mci1);
            bool isUpdated = MultiCloudImage.update(mciID, "new name", "new description");
            Assert.IsTrue(isUpdated);
            MultiCloudImage mci2 = MultiCloudImage.show(mciID);
            Assert.IsNotNull(mci2);
            Assert.AreNotEqual(mci1.name, mci2.name);
            Assert.AreNotEqual(mci1.description, mci2.description);
            bool isDeleted = MultiCloudImage.destroy(mciID);
            Assert.IsTrue(isDeleted);
        }

        [TestMethod]
        public void createUpdateDestroyMCITest()
        {
            string mciID = MultiCloudImage.create("new MCI", "this is a description");
            Assert.IsNotNull(mciID);
            Assert.IsTrue(mciID.Length > 0);
            MultiCloudImage mci1 = MultiCloudImage.show(mciID);
            Assert.IsNotNull(mci1);
            bool isUpdated = MultiCloudImage.update(mciID, "new name", "new description");
            Assert.IsTrue(isUpdated);
            MultiCloudImage mci2 = MultiCloudImage.show(mciID);
            Assert.IsNotNull(mci2);
            Assert.AreNotEqual(mci1.name, mci2.name);
            Assert.AreNotEqual(mci1.description, mci2.description);
            bool isDeleted = MultiCloudImage.destroy(mciID);
            Assert.IsTrue(isDeleted);
        }

        [TestMethod]
        public void createUpdateDestroySTMCISimple()
        {
            string mciID = MultiCloudImage.create_serverTemplate(serverTemplateID, "new MCI");
            Assert.IsNotNull(mciID);
            Assert.IsTrue(mciID.Length > 0);
            MultiCloudImage mci1 = MultiCloudImage.show(mciID);
            Assert.IsNotNull(mci1);
            bool isUpdated = MultiCloudImage.update(mciID, "new name", "new description");
            Assert.IsTrue(isUpdated);
            MultiCloudImage mci2 = MultiCloudImage.show(mciID);
            Assert.IsNotNull(mci2);
            Assert.AreNotEqual(mci1.name, mci2.name);
            Assert.AreNotEqual(mci1.description, mci2.description);
            bool isDeleted = MultiCloudImage.destroy_ServerTemplate(serverTemplateID, mciID);
            Assert.IsTrue(isDeleted);
        }

        [TestMethod]
        public void createUpdateDestroySTMCITest()
        {
            string mciID = MultiCloudImage.create_serverTemplate(serverTemplateID, "new MCI", "this is a description");
            Assert.IsNotNull(mciID);
            Assert.IsTrue(mciID.Length > 0);
            MultiCloudImage mci1 = MultiCloudImage.show(mciID);
            Assert.IsNotNull(mci1);
            bool isUpdated = MultiCloudImage.update(mciID, "new name", "new description");
            Assert.IsTrue(isUpdated);
            MultiCloudImage mci2 = MultiCloudImage.show(mciID);
            Assert.IsNotNull(mci2);
            Assert.AreNotEqual(mci1.name, mci2.name);
            Assert.AreNotEqual(mci1.description, mci2.description);
            bool isDeleted = MultiCloudImage.destroy_ServerTemplate(serverTemplateID, mciID);
            Assert.IsTrue(isDeleted);
        }

        [TestMethod]
        public void createUpdateSTMCIdestroyMCISimple()
        {
            string mciID = MultiCloudImage.create_serverTemplate(serverTemplateID, "new MCI");
            Assert.IsNotNull(mciID);
            Assert.IsTrue(mciID.Length > 0);
            MultiCloudImage mci1 = MultiCloudImage.show(mciID);
            Assert.IsNotNull(mci1);
            bool isUpdated = MultiCloudImage.update(mciID, "new name", "new description");
            Assert.IsTrue(isUpdated);
            MultiCloudImage mci2 = MultiCloudImage.show(mciID);
            Assert.IsNotNull(mci2);
            Assert.AreNotEqual(mci1.name, mci2.name);
            Assert.AreNotEqual(mci1.description, mci2.description);
            bool isDeleted = MultiCloudImage.destroy(mciID);
            Assert.IsTrue(isDeleted);
        }

        [TestMethod]
        public void createUpdateSTMCIdestroyMCITest()
        {
            string mciID = MultiCloudImage.create_serverTemplate(serverTemplateID, "new MCI", "this is a description");
            Assert.IsNotNull(mciID);
            Assert.IsTrue(mciID.Length > 0);
            MultiCloudImage mci1 = MultiCloudImage.show(mciID);
            Assert.IsNotNull(mci1);
            bool isUpdated = MultiCloudImage.update(mciID, "new name", "new description");
            Assert.IsTrue(isUpdated);
            MultiCloudImage mci2 = MultiCloudImage.show(mciID);
            Assert.IsNotNull(mci2);
            Assert.AreNotEqual(mci1.name, mci2.name);
            Assert.AreNotEqual(mci1.description, mci2.description);
            bool isDeleted = MultiCloudImage.destroy(mciID);
            Assert.IsTrue(isDeleted);
        }


        #endregion

    }
}

