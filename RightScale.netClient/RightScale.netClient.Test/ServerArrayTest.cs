using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using System.Web;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class ServerArrayTest
    {
        private string filterListString;
        private string serverarrayView;
        private string serverarrayID;

        public ServerArrayTest()
        {
           
            filterListString = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["ServerArrayTest_filterListString"].ToString());
            serverarrayView = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["ServerArrayTest_serverarrayview"].ToString());
            serverarrayID = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["ServerArrayTest_serverarrayid"].ToString());
        }

        #region ServerArray.index tests
        [TestMethod]
        public void ServerArrayIndexSimple()
        {
            List<ServerArray> serverarrayList = ServerArray.index();
            Assert.IsNotNull(serverarrayList);
            Assert.IsTrue(serverarrayList.Count > 0);

        }

        [TestMethod]
        public void ServerArrayIndexFilteredString()
        {
            List<Filter> filter = Filter.parseFilterList(filterListString);
            
            List<ServerArray> serverarrayList = ServerArray.index(filter);
            Assert.IsNotNull(serverarrayList);
        }
        #endregion

        #region ServerArray.show tests
        [TestMethod]
        public void ServerArrayShow()
        {
            ServerArray serverarray = ServerArray.show(serverarrayID, null);
            Assert.IsNotNull(serverarray);
        }



        #endregion
    }
}
