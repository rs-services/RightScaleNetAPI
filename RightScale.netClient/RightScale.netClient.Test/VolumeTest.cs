using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Configuration;
using System.Collections.Generic;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class VolumeTest
    {
        private string cloudID;

        public VolumeTest()
        {

            cloudID = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["VolumeTest_cloudid"].ToString());
        }

        #region Volume.index tests

        [TestMethod]
        public void index_volumeSimpleTest()
        {

            List<Volume> volumelist = Volume.index(cloudID);

            Assert.IsNotNull(volumelist);
            Assert.IsTrue(volumelist.Count > 0);
        }

        [TestMethod]
        public void index_volumeFilteredTest()
        {
            List<Filter> filterSet = new List<Filter>();
            filterSet.Add(new Filter("name", FilterOperator.Equal, "dev"));

            List<Volume> volumelist = Volume.index(cloudID,filterSet,null);

            Assert.IsNotNull(volumelist);
            Assert.IsTrue(volumelist.Count > 0);

        }

        [TestMethod]
        public void index_volumeViewTest()
        {

            List<Volume> volumelist = Volume.index(cloudID,"extended");

            Assert.IsNotNull(volumelist);
            Assert.IsTrue(volumelist.Count > 0);
        }

        //---------------
        [TestMethod]
        public void index_volumeFilterViewTest()
        {
            List<Filter> filterSet = new List<Filter>();
            filterSet.Add(new Filter("name", FilterOperator.Equal, "dev"));

            List<Volume> volumelist = Volume.index(cloudID,filterSet,"extended");

            Assert.IsNotNull(volumelist);
            Assert.IsTrue(volumelist.Count > 0);
        }
        #endregion
    }
}