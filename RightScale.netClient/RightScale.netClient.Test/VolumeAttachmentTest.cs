using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using System.Web;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class VolumeAttachmentTest
    {
        private string volumeAttachmentCloudID;
        private string cloudID;
        private string apiRefreshToken;

        public VolumeAttachmentTest()
        {
            volumeAttachmentCloudID = ConfigurationManager.AppSettings["VolumeAttachmentTest_cloudID"].ToString();
            cloudID = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["VolumeType_raxID"].ToString());
            apiRefreshToken = ConfigurationManager.AppSettings["RightScaleServicesAPIRefreshToken"].ToString();
        }

        [TestMethod]
        public void volumeAttachmentIndexSimple()
        {
            List<VolumeAttachment> volAttachments = VolumeAttachment.index(volumeAttachmentCloudID);
            Assert.IsNotNull(volAttachments);
        }

        //TODO: need to set up cinder locally
        //[TestMethod]
        public void volumeAttachmentIndexComplicated()
        {
            netClient.Core.APIClient.Instance.InitWebClient();
            netClient.Core.APIClient.Instance.Authenticate(apiRefreshToken);
            List<VolumeAttachment> volAttachments = VolumeAttachment.index(cloudID);
            //Assert.IsNotNull(volAttachments);
            netClient.Core.APIClient.Instance.InitWebClient();
        }
    }
}
