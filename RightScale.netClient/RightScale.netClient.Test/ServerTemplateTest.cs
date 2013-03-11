using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Configuration;
using System.Collections.Generic;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class ServerTemplateTest
    {
        private string filterListString;

        public ServerTemplateTest()
        {
           
            filterListString = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["ServerTemplateTest_filterListString"].ToString());
        }

        #region Instance.index tests

        [TestMethod]
        public void indexSimpleTest()
        {
            
            List<ServerTemplate> servertemplateList = ServerTemplate.index();
            Assert.IsNotNull(servertemplateList);
            Assert.IsTrue(servertemplateList.Count > 0);
        }


        #endregion

    }
}
