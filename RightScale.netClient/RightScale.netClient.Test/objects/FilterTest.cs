using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Collections.Generic;
using System.Web;

namespace RightScale.netClient.Test.objects
{
    [TestClass]
    public class FilterTest
    {
        string singleFilterString;
        string singleFullFilterString;
        string multipleFilterString;
        string multipleFullFilterString;

        public FilterTest()
        {
            singleFilterString = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["FilterTest_singleFilterString"].ToString());
            singleFullFilterString = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["FilterTest_singleFullFilterString"].ToString());
            multipleFilterString = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["FilterTest_multipleFilterString"].ToString());
            multipleFullFilterString = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["FilterTest_multipleFullFilterString"].ToString());
        }

        [TestMethod]
        public void singleFilterParse()
        {
            Filter testFilter = Filter.parseFilter(singleFilterString);
            Assert.IsNotNull(testFilter);
        }

        [TestMethod]
        public void singleFullFilterParse()
        {
            Filter testFilter = Filter.parseFilter(singleFullFilterString);
            Assert.IsNotNull(testFilter);
        }

        [TestMethod]
        public void filterListParse()
        {
            List<Filter> testFilterList = Filter.parseFilterList(multipleFilterString);
            Assert.IsNotNull(testFilterList);
        }

        [TestMethod]
        public void filterFullListParse()
        {
            List<Filter> testFilterFullList = Filter.parseFilterList(multipleFullFilterString);
            Assert.IsNotNull(testFilterFullList);
        }
    }
}
