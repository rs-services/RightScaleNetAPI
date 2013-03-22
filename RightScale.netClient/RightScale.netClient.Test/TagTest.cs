using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Configuration;
using System.Collections.Generic;


namespace RightScale.netClient.Test
{
    [TestClass]
    public class TagTest
    {
        private string deploymenthref;
        private string serverhref;

        public TagTest()
        {
            deploymenthref = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["TagTest_deploymentHref"].ToString());
            serverhref = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["TagTest_serverHref"].ToString());
        }


        //[TestMethod]
        public void TagbyResourceTest()
        {
            List<string> arrHrefs = new List<string>() { serverhref, deploymenthref };
            
            List<Tag> resHref = Tag.byResource(arrHrefs);

            Assert.IsNotNull(resHref);
            
        }
    }
}
