using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class MultiCloudImageSettingTest
    {
        public string multiCloudImageID;
        private string multiCloudImageSettingID;

        public MultiCloudImageSettingTest()
        {
            multiCloudImageID = ConfigurationManager.AppSettings["MultiCloudImageSettingTest_multiCloudImageID"].ToString();
            multiCloudImageSettingID = ConfigurationManager.AppSettings["MultiCloudImageSettingTest_multiCloudImageSettingID"].ToString();
        }

        [TestMethod]
        public void indexMultiCloudImageSettingSimple()
        {
            List<MultiCloudImageSetting> mcis = MultiCloudImageSetting.index(multiCloudImageID);
            Assert.IsNotNull(mcis);
            Assert.IsTrue(mcis.Count > 0);
        }

        [TestMethod]
        public void indexMultiCloudImageSettingFiltered()
        {
            List<Filter> filters = new List<Filter>();
            filters.Add(new Filter("cloud_href", FilterOperator.NotEqual, "/api/clouds/2432"));
            List<MultiCloudImageSetting> mcis = MultiCloudImageSetting.index(multiCloudImageID, filters);
            Assert.IsNotNull(mcis);
            Assert.IsTrue(mcis.Count>0);
        }

        [TestMethod]
        public void showMultiCloudImageSetting()
        {
            MultiCloudImageSetting mcis = MultiCloudImageSetting.show(multiCloudImageID, multiCloudImageSettingID);
            Assert.IsNotNull(mcis);
            Assert.IsTrue(mcis.links.Count > 0);
        }
    }
}
