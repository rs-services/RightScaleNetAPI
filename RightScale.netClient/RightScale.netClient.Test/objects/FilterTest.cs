using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Collections.Generic;

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
            singleFilterString = @"isTestClass==yes";
            singleFullFilterString = @"filter[]=isTestClass==yes";
            multipleFilterString = @"name==Patrick&isAwesome==true&worksForRightScale==yep&location<>airport";
            multipleFullFilterString = @"filter[]=name==Patrick&filter[]=isAwesome==true&filter[]=worksForRightScale==yep&filter[]=location<>airport";
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
