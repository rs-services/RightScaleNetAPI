using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class DataCenterTest
    {
        string cloudID;
        string datacenterID;

        public DataCenterTest()
        {
            cloudID = ConfigurationManager.AppSettings["DataCenter_cloudID"].ToString();
            datacenterID = ConfigurationManager.AppSettings["DataCenter_datacenterID"].ToString();
        }

        [TestMethod]
        public void datacenterIndex()
        {
            List<DataCenter> dcList = DataCenter.index(cloudID);
            Assert.IsNotNull(dcList);
            Assert.IsTrue(dcList.Count > 0);
        }
    }
}
