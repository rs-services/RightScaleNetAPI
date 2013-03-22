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
        private string volumeID;

        public VolumeTest()
        {

            cloudID = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["VolumeTest_cloudid"].ToString());
            volumeID = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["VolumeTest_volumeid"].ToString());
        }

        #region Volume Relationship Tests

        [TestMethod]
        public void volumeTags()
        {
            List<Volume> volumeList = Volume.index(cloudID);
            Assert.IsNotNull(volumeList);
            List<Tag> tags = volumeList[0].tags;
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void volumeParentVolume()
        {
            Volume vol = Volume.show(cloudID, volumeID);
            Assert.IsNotNull(vol);
            VolumeSnapshot parVol = vol.parentVolumeSnapshot;
            Assert.IsTrue(true);//no exception
        }

        [TestMethod]
        public void volumeVolumeSnapshots()
        {
            Volume vol = Volume.show(cloudID, volumeID);
            Assert.IsNotNull(vol);
            List<VolumeSnapshot> vsnaps = vol.volumeSnapshot;
            Assert.IsNotNull(vsnaps);
        }

        [TestMethod]
        public void volumeDatacenter()
        {
            Volume vol = Volume.show(cloudID, volumeID);
            Assert.IsNotNull(vol);
            DataCenter dc = vol.datacenter;
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void volumeRecurringVolumeAttachments()
        {
            Volume vol = Volume.show(cloudID, volumeID);
            Assert.IsNotNull(vol);
            List<RecurringVolumeAttachment> rva = vol.recurringVolumeAttachments;
            Assert.IsNotNull(rva);
        }

        [TestMethod]
        public void volumeCurrentVolumeAttachments()
        {
            Volume vol = Volume.show(cloudID, volumeID);
            Assert.IsNotNull(vol);
            List<VolumeAttachment> vas = vol.currentVolumeAttachments;
            Assert.IsNotNull(vas);
        }

        [TestMethod]
        public void volumeCloud()
        {
            Volume vol = Volume.show(cloudID, volumeID);
            Assert.IsNotNull(vol);
            Cloud cl = vol.cloud;
            Assert.IsNotNull(cl);
            Assert.IsTrue(cl.name.Length > 0);
        }

        #endregion

        #region Volume.index tests

        [TestMethod]
        public void index_volumeSimpleTest()
        {
            List<Volume> volumeList = Volume.index(cloudID);
            Assert.IsNotNull(volumeList);
            Assert.IsTrue(volumeList.Count > 0);
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


        #region Volume.show tests

        //TODO:  Need to get valid volume cloudID
        [TestMethod]
        public void index_volumeShowTest()
        {
            Volume volumelist = Volume.show(cloudID, volumeID);
            Assert.IsNotNull(volumelist);
        }

        #endregion
        #region Volume.show tests


        #endregion
    }
}