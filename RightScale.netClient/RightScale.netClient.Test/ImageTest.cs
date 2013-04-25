using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using System.Web;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class ImageTest : RSAPITestBase
    {

        private string cloudID;
        private string filterListString;
        private string imageid;

        public ImageTest():base()
        {
            cloudID = this.azureCloudID;
            filterListString = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["ImageTest_filterListString"].ToString());
            imageid = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["ImageTest_imageid"].ToString());
        }

        #region Image relationship tests

        [TestMethod]
        public void imageCloudTest()
        {
            Image img = Image.show(cloudID, imageid, "default");
            Assert.IsNotNull(img);
            Cloud c = img.cloud;
            Assert.IsNotNull(c);
            Assert.IsTrue(c.name.Length > 0);
        }

        [TestMethod]
        public void imageTags()
        {
            Image img = Image.show(cloudID, imageid, "default");
            Assert.IsNotNull(img);
            List<Tag> tags = img.tags;
            Assert.IsTrue(true); //no exception!
        }

        #endregion

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

            List<Filter> filters = new List<Filter>();
            filters.AddRange(Filter.parseFilterList(filterListString));
            List<Image> imageList = Image.index(cloudID,filters);

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
