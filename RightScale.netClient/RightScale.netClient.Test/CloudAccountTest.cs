using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class CloudAccountTest
    {
        string cloudAccountID;

        public CloudAccountTest()
        {
            cloudAccountID = ConfigurationManager.AppSettings["CloudAccount_cloudAccountID"].ToString();
        }

        [TestMethod]
        public void cloudAccountIndexSimple()
        {
            List<CloudAccount> caList = CloudAccount.index();
            Assert.IsNotNull(caList);
            Assert.IsTrue(caList.Count > 0);
        }

        [TestMethod]
        public void cloudAccountShowSimple()
        {
            CloudAccount ca = CloudAccount.show(cloudAccountID);
            Assert.IsNotNull(ca);
            Assert.IsTrue(ca.ID.Length > 0);
            Assert.IsTrue(ca.ID == cloudAccountID);
        }

        [TestMethod]
        public void cloudAccountRelationshipsTest()
        {
            CloudAccount ca = CloudAccount.show(cloudAccountID);
            Assert.IsNotNull(ca);
            Assert.IsTrue(ca.ID.Length > 0);
            Assert.IsTrue(ca.ID == cloudAccountID);
            Assert.IsNotNull(ca.account);
            Assert.IsTrue(ca.account.ID.Length > 0);
            Assert.IsNotNull(ca.cloud);
            Assert.IsNotNull(ca.cloud.ID.Length > 0);
        }
    }
}
