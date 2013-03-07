using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class InstanceTypeTest
    {
        string cloudID;

        public InstanceTypeTest()
        {
            cloudID = ConfigurationManager.AppSettings["InstanceTypeTest_cloudID"].ToString();
        }

        #region InstanceType.index tests

        [TestMethod]
        public void InstanceTypeIndexTest()
        {
            List<InstanceType> resultSet = InstanceType.index(cloudID);
            Assert.IsNotNull(resultSet);
        }

        #endregion
    }
}
