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

        public MultiCloudImageTest()
        {
            filterListString = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["MultiCloudImageTest_filterListString"].ToString());
            multicloudimageid = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["MultiCloudImageTest_imageid"].ToString());
            multicloudimageidupdate = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["MultiCloudImageTestUpdate_imageid"].ToString());
            multicloudimageiddestroy = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["MultiCloudImageTestDestroy_imageid"].ToString());

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
        [TestMethod]
        public void MultiCloudImageClone()
        {
            string resltVal = MultiCloudImage.clone(multicloudimageidupdate,"MCI Clone Test");

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


    }
}
