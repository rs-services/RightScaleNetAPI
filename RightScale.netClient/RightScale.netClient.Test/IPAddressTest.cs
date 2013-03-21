using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class IPAddressTest
    {
        string cloudID;

        public IPAddressTest()
        {
            cloudID = ConfigurationManager.AppSettings["IPAdressTest_cloudID"].ToString();
        }

        [TestMethod]
        public void IPAddressIPAddressBinding()
        {
            List<IPAddress> ipAddressList = IPAddress.index(cloudID);
            Assert.IsNotNull(ipAddressList);
            List<IPAddressBinding> ipBinding = ipAddressList[0].ipAddressBindings;
            Assert.IsTrue(ipBinding.Count > 0);            
        }

        [TestMethod]
        public void IPAddressIndexTest()
        {
            List<IPAddress> ipAddressList = IPAddress.index(cloudID);
            Assert.IsNotNull(ipAddressList);
        }
    }
}
