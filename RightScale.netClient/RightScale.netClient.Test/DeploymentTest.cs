using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class DeploymentTest
    {
        #region Deployment.index tests

        [TestMethod]
        public void indexSimpleTest()
        {
            List<Deployment> listOfDeployments = Deployment.index();
            Assert.IsNotNull(listOfDeployments);
            Assert.IsTrue(listOfDeployments.Count > 0);
        }

        [TestMethod]
        public void indexViewTest()
        {
            List<Deployment> listOfDeployments = Deployment.index("inputs");
            Assert.IsNotNull(listOfDeployments);
            Assert.IsTrue(listOfDeployments.Count > 0);
            int totalInputs = 0;
            foreach (Deployment d in listOfDeployments)
            {
                totalInputs += d.inputs.Count;
            }
            Assert.IsTrue(totalInputs > 0);
        }

        [TestMethod]
        public void indexFilterTest()
        {
            List<KeyValuePair<string, string>> filterSet = new List<KeyValuePair<string, string>>();
            filterSet.Add(new KeyValuePair<string, string>("name", "API"));
            List<Deployment> listOfDeployments = Deployment.index();
            Assert.IsNotNull(listOfDeployments);
            Assert.IsTrue(listOfDeployments.Count > 0);
            List<Deployment> filteredListOfDeployments = Deployment.index(filterSet);
            Assert.IsNotNull(filteredListOfDeployments);
            Assert.IsTrue(filteredListOfDeployments.Count > 0);
            Assert.IsTrue(filteredListOfDeployments.Count < listOfDeployments.Count);
        }

        [TestMethod]
        public void indesFullTest()
        {
            List<KeyValuePair<string, string>> filter = new List<KeyValuePair<string, string>>();
            filter.Add(new KeyValuePair<string, string>("name", "API"));
            List<Deployment> simpleList = Deployment.index();
            Assert.IsNotNull(simpleList);
            Assert.IsTrue(simpleList.Count > 0);
            List<Deployment> viewList = Deployment.index("inputs");
            Assert.IsNotNull(viewList);
            Assert.IsTrue(viewList.Count > 0);
            int viewInputs = 0;
            foreach (Deployment d in viewList)
            {
                viewInputs += d.inputs.Count;
            }
            Assert.IsTrue(viewInputs > 0);
            List<Deployment> filterList = Deployment.index(filter);
            Assert.IsNotNull(filterList);
            Assert.IsTrue(filterList.Count > 0);
            List<Deployment> viewFilterList = Deployment.index(filter, "inputs");
            Assert.IsNotNull(viewFilterList);
            Assert.IsTrue(viewFilterList.Count > 0);
            int viewFilterInputs = 0;
            foreach (Deployment d in viewFilterList)
            {
                viewFilterInputs += d.inputs.Count;
            }
            Assert.IsTrue(viewFilterInputs > 0);
            Assert.IsTrue(viewFilterInputs < viewInputs);
            Assert.IsTrue(filterList.Count == viewFilterList.Count);
        }
        
        #endregion
    }
}
