using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class DeploymentTest
    {
        string deploymentID;

        public DeploymentTest()
        {
            deploymentID = ConfigurationManager.AppSettings["DeploymentTest_deploymentID"].ToString();
        }

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
            List<Filter> filterSet = new List<Filter>();
            filterSet.Add(new Filter("name", FilterOperator.Equal, "API"));
            List<Deployment> listOfDeployments = Deployment.index();
            Assert.IsNotNull(listOfDeployments);
            Assert.IsTrue(listOfDeployments.Count > 0);
            List<Deployment> filteredListOfDeployments = Deployment.index(filterSet);
            Assert.IsNotNull(filteredListOfDeployments);
            Assert.IsTrue(filteredListOfDeployments.Count > 0);
            Assert.IsTrue(filteredListOfDeployments.Count < listOfDeployments.Count);
        }

        [TestMethod]
        public void indexFullTest()
        {
            List<Filter> filter = new List<Filter>();
            filter.Add(new Filter("name", FilterOperator.Equal, "API"));
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

        #region Deployment.show tests

        [TestMethod]
        public void showDeploymentSimple()
        {
            Deployment testDeployment = Deployment.show(deploymentID);
            Assert.IsNotNull(testDeployment);
        }

        [TestMethod]
        public void showDeploymentView()
        {
            Deployment testDeployment = Deployment.show(deploymentID, "inputs");
            Assert.IsNotNull(testDeployment);
            Assert.IsTrue(testDeployment.inputs.Count > 0);
        }

        #endregion

        #region Deployment.create tests

        [TestMethod]
        public void deploymentCreateDestroy()
        {
            string newDeploymentID = Deployment.create("simple name for a deployment");
            Assert.IsNotNull(newDeploymentID);
            bool isDestroyed = Deployment.destroy(newDeploymentID);
            Assert.IsTrue(isDestroyed);
        }

        [TestMethod]
        public void deploymentCreateFullDestroy()
        {
            string newDeploymentID = Deployment.create("this is a deployment name", "this is a description", "deployment");
            Assert.IsNotNull(newDeploymentID);
            bool isDestroyed = Deployment.destroy(newDeploymentID);
            Assert.IsTrue(isDestroyed);
        }
        #endregion

        #region Deployment.update tests

        [TestMethod]
        public void deploymentCreateUpdateDestroy()
        {
            string newDeploymentID = Deployment.create("simple name for a deployment");
            Assert.IsNotNull(newDeploymentID);
            Deployment initialObject = Deployment.show(newDeploymentID);
            Assert.IsNotNull(initialObject);

            bool isUpdated = Deployment.update(newDeploymentID, "this is a new name", "this is a new desription", null);
            Assert.IsTrue(isUpdated);
            Deployment updatedObject = Deployment.show(newDeploymentID);
            Assert.IsNotNull(updatedObject);

            Assert.AreNotEqual(updatedObject.name, initialObject.name);
            Assert.AreNotEqual(updatedObject.description, initialObject.description);

            bool isDestroyed = Deployment.destroy(newDeploymentID);
            Assert.IsTrue(isDestroyed);
        }

        #endregion

        #region Deployment.clone tests

        [TestMethod]
        public void deploymentCloneDestroyTest()
        {
            string newDeploymentID = Deployment.clone(deploymentID);
            Assert.IsNotNull(newDeploymentID);
            bool isDestroyed = Deployment.destroy(newDeploymentID);
            Assert.IsTrue(isDestroyed);
        }

        #endregion

        #region Deployment.servers tests

        [TestMethod]
        public void deploymentServersStatic()
        {
            List<Server> listOfDeploymentServers = Deployment.servers(deploymentID);
            Assert.IsNotNull(listOfDeploymentServers);
            Assert.IsTrue(listOfDeploymentServers.Count > 0);
        }

        [TestMethod]
        public void deploymentServersInstance()
        {
            Deployment deploymentInstance = Deployment.show(deploymentID);
            Assert.IsNotNull(deploymentInstance);
            List<Server> serverList = deploymentInstance.getServers();
            Assert.IsNotNull(serverList);
            Assert.IsTrue(serverList.Count > 0);
        }

        #endregion
    }
}
