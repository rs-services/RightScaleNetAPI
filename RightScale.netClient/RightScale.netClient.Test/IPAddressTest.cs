using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class IPAddressTest : RSAPITestBase
    {
        string cloudID;

        public IPAddressTest()
        {
            cloudID = this.openStackCloudID;
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
