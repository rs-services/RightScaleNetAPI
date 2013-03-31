using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class VolumeAttachmentTest
    {
        private string volumeAttachmentCloudID;

        public VolumeAttachmentTest()
        {
            volumeAttachmentCloudID = ConfigurationManager.AppSettings["VolumeAttachmentTest_cloudID"].ToString();
        }

        [TestMethod]
        public void volumeAttachmentIndex()
        {
            List<VolumeAttachment> volAttachments = VolumeAttachment.index(volumeAttachmentCloudID);
            Assert.IsNotNull(volAttachments);
        }
    }
}
