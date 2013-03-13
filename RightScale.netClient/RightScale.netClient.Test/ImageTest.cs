using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using System.Web;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class ImageTest
    {

        private string cloudID;
        private string filterListString;
        private string imageid;

        public ImageTest()
        {

            cloudID = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["ImageTest_cloudid"].ToString());
            filterListString = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["ImageTest_filterListString"].ToString());
            imageid = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["ImageTest_imageid"].ToString());
        }

        #region Image.index tests
        [TestMethod]
        public void ImageIndexSimple()
       {
            List<Image> imageList = Image.index(cloudID);
            Assert.IsNotNull(imageList);
            Assert.IsTrue(imageList.Count > 0);

        }

        [TestMethod]
        public void ImageIndexFilteredString()
        {
            List<Image> imageList = Image.index(cloudID,filterListString,"default");
            Assert.IsNotNull(imageList);
        }
        #endregion

        #region Image.show tests

        [TestMethod]
        public void ImageShow()
        {
            Image image = Image.show(cloudID, imageid, null);
            Assert.IsNotNull(image);
        }

        #endregion

    }
}
