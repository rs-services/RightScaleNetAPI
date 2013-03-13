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

        public MultiCloudImageTest()
        {
            filterListString = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["MultiCloudImageTest_filterListString"].ToString());
            multicloudimageid = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["MultiCloudImageTest_imageid"].ToString());
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
    }
}
