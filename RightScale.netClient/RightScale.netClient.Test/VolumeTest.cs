using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Configuration;
using System.Collections.Generic;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class VolumeTest : RSAPITestBase
    {
        private string cloudID;
        private string volumeID;
        private string apiRefreshToken;
        private string volumeTypeID;
        private string childVolumeID;

        public VolumeTest():base()
        {

            cloudID = this.rackSpaceOpenCloudID;
            volumeID = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["VolumeTest_volumeID"].ToString());
            apiRefreshToken = ConfigurationManager.AppSettings["RightScaleServicesAPIRefreshToken"].ToString();
            volumeTypeID = ConfigurationManager.AppSettings["VolumeTest_volumeTypeID"].ToString();
            childVolumeID = ConfigurationManager.AppSettings["VolumeTest_childVolumeID"].ToString();
        }

        #region Volume Relationship Tests

        [TestMethod]
        public void volumeTags()
        {
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
            RightScale.netClient.Core.APIClient.Instance.Authenticate(apiRefreshToken);
            List<Volume> volumeList = Volume.index(cloudID);
            Assert.IsNotNull(volumeList);
            List<Tag> tags = volumeList[0].tags;
            Assert.IsTrue(true);
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
        }

        [TestMethod]
        public void volumeParentVolume()
        {
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
            RightScale.netClient.Core.APIClient.Instance.Authenticate(apiRefreshToken);
            Volume vol = Volume.show(cloudID, childVolumeID);
            Assert.IsNotNull(vol);
            VolumeSnapshot parVol = vol.parentVolumeSnapshot;
            Assert.IsTrue(true);//no exception
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
        }

        [TestMethod]
        public void volumeVolumeSnapshots()
        {
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
            RightScale.netClient.Core.APIClient.Instance.Authenticate(apiRefreshToken);
            Volume vol = Volume.show(cloudID, volumeID);
            Assert.IsNotNull(vol);
            List<VolumeSnapshot> vsnaps = vol.volumeSnapshot;
            Assert.IsNotNull(vsnaps);
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
        }

        [TestMethod]
        public void volumeDatacenter()
        {
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
            RightScale.netClient.Core.APIClient.Instance.Authenticate(apiRefreshToken);
            Volume vol = Volume.show(cloudID, volumeID);
            Assert.IsNotNull(vol);
            DataCenter dc = vol.datacenter;
            Assert.IsTrue(true);
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
        }

        [TestMethod]
        public void volumeRecurringVolumeAttachments()
        {
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
            RightScale.netClient.Core.APIClient.Instance.Authenticate(apiRefreshToken);
            Volume vol = Volume.show(cloudID, volumeID);
            Assert.IsNotNull(vol);
            List<RecurringVolumeAttachment> rva = vol.recurringVolumeAttachments;
            Assert.IsNotNull(rva);
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
        }

        [TestMethod]
        public void volumeCurrentVolumeAttachments()
        {
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
            RightScale.netClient.Core.APIClient.Instance.Authenticate(apiRefreshToken);
            Volume vol = Volume.show(cloudID, volumeID);
            Assert.IsNotNull(vol);
            List<VolumeAttachment> vas = vol.currentVolumeAttachments;
            Assert.IsNotNull(vas);
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
        }

        [TestMethod]
        public void volumeCloud()
        {
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
            RightScale.netClient.Core.APIClient.Instance.Authenticate(apiRefreshToken);
            Volume vol = Volume.show(cloudID, volumeID);
            Assert.IsNotNull(vol);
            Cloud cl = vol.cloud;
            Assert.IsNotNull(cl);
            Assert.IsTrue(cl.name.Length > 0);
            RightScale.netClient.Core.APIClient.Instance.InitWebClient();
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