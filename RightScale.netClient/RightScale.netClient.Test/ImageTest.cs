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

        private string filterListString;

        public ImageTest()
        {
           
            filterListString = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["ImageTest_filterListString"].ToString());
        }

        [TestMethod]
        public void ImageIndexSimple()
        {

            List<Image> imageList = Image.index();
            Assert.IsNotNull(imageList);
            Assert.IsTrue(imageList.Count > 0);

        }

        [TestMethod]
        public void ImageIndexFilteredString(string filter)
        {
            List<Image> imageList = Image.index(filterListString);
            Assert.IsNotNull(imageList);
        }
    }
}
