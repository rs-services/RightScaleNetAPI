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

            List<Volume> volumeList = Volume.index(cloudID);
            Assert.IsNotNull(volumeList);
            Assert.IsTrue(volumeList.Count > 0);

        }

        #endregion
    }
}