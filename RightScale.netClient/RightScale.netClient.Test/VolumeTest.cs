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
        private string apiRefreshToken;
        private string volumeTypeID;

        public VolumeTest()
        {

            cloudID = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["VolumeTest_cloudID"].ToString());
            volumeID = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["VolumeTest_volumeID"].ToString());
            apiRefreshToken = ConfigurationManager.AppSettings["RightScaleServicesAPIRefreshToken"].ToString();
            volumeTypeID = ConfigurationManager.AppSettings["VolumeTest_volumeTypeID"].ToString();
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
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
            RightScale.netClient.Core.APIClient.Instance.Authenticate(apiRefreshToken);
            List<Volume> volumeList = Volume.index(cloudID);
            Assert.IsNotNull(volumeList);
            Assert.IsTrue(volumeList.Count > 0);
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
        }

        [TestMethod]
        public void index_volumeFilteredTest()
        {
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
            RightScale.netClient.Core.APIClient.Instance.Authenticate(apiRefreshToken);
            List<Filter> filterSet = new List<Filter>();
            filterSet.Add(new Filter("name", FilterOperator.Equal, "dev"));

            List<Volume> volumelist = Volume.index(cloudID,filterSet,null);

            Assert.IsNotNull(volumelist);
            Assert.IsTrue(volumelist.Count > 0);
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();

        }

        [TestMethod]
        public void index_volumeViewTest()
        {

            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
            RightScale.netClient.Core.APIClient.Instance.Authenticate(apiRefreshToken);
            List<Volume> volumelist = Volume.index(cloudID,"extended");

            Assert.IsNotNull(volumelist);
            Assert.IsTrue(volumelist.Count > 0);
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
        }

        
        [TestMethod]
        public void index_volumeFilterViewTest()
        {
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
            RightScale.netClient.Core.APIClient.Instance.Authenticate(apiRefreshToken);
            List<Filter> filterSet = new List<Filter>();
            filterSet.Add(new Filter("name", FilterOperator.Equal, "dev"));

            List<Volume> volumelist = Volume.index(cloudID,filterSet,"extended");

            Assert.IsNotNull(volumelist);
            Assert.IsTrue(volumelist.Count > 0);
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
        }
        #endregion


        #region Volume.show tests

        [TestMethod]
        public void show_volumeShowTest()
        {
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
            RightScale.netClient.Core.APIClient.Instance.Authenticate(apiRefreshToken);
            Volume volumelist = Volume.show(cloudID, volumeID);
            Assert.IsNotNull(volumelist);
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
        }

        #endregion

        #region Volume.create .delete tests

        [TestMethod]
        public void create_volumeSimple()
        {
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
            RightScale.netClient.Core.APIClient.Instance.Authenticate(apiRefreshToken);
            string volumeID = Volume.create(cloudID, "api unit test volume", string.Empty, "this is a volume created by the .net api client unit test project at " + DateTime.Now.ToString(), string.Empty, string.Empty, string.Empty, "100", volumeTypeID);
            Assert.IsNotNull(volumeID);
            Assert.IsTrue(volumeID.Length > 0);
            bool isDeleted = Volume.destroy(cloudID, volumeID);
            Assert.IsTrue(isDeleted);
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
        }

        #endregion
    }
}