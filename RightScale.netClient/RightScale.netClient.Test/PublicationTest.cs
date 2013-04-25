using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RightScale.netClient;
using System.Configuration;
using System.Collections.Generic;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class PublicationTest : RSAPITestBase
    {
        string servicesOAuthToken;
        string publicationID;

        public PublicationTest()
        {
            servicesOAuthToken = ConfigurationManager.AppSettings["RightScaleServicesAPIRefreshToken"].ToString();
            publicationID = ConfigurationManager.AppSettings["PublicationTest_publicationID"].ToString();
        }

        #region Publication.index() tests

        [TestMethod]
        public void publicationIndexTest()
        {
            List<Publication> returnCollection = Publication.index();
            Assert.IsNotNull(returnCollection);
            Assert.IsTrue(returnCollection.Count > 0);
        }

        [TestMethod]
        public void publicationIndexWindowsTest()
        {
            List<Filter> filters = new List<Filter>();
            filters.Add(new Filter("name", FilterOperator.Equal, "windows"));
            List<Publication> returnCollection = Publication.index(filters);
            Assert.IsNotNull(returnCollection);
            Assert.IsTrue(returnCollection.Count > 0);
        }

        [TestMethod]
        public void publicationIndexLinuxTest()
        {
            List<Filter> filters = new List<Filter>();
            filters.Add(new Filter("name", FilterOperator.Equal, "linux"));
            List<Publication> returnCollection = Publication.index(filters);
            Assert.IsNotNull(returnCollection);
            Assert.IsTrue(returnCollection.Count > 0);
        }

        [TestMethod]
        public void publicationIndexWindowsV2Test()
        {
            List<Filter> filters = new List<Filter>();
            filters.Add(new Filter("name", FilterOperator.Equal, "windows"));
            filters.Add(new Filter("revision", FilterOperator.Equal, "2"));
            List<Publication> returnCollection = Publication.index(filters);
            Assert.IsNotNull(returnCollection);
            Assert.IsTrue(returnCollection.Count > 0);
        }

        [TestMethod]
        public void publicationIndexLinuxV2Test()
        {
            List<Filter> filters = new List<Filter>();
            filters.Add(new Filter("name", FilterOperator.Equal, "linux"));
            filters.Add(new Filter("revision", FilterOperator.Equal, "2"));
            List<Publication> returnCollection = Publication.index(filters);
            Assert.IsNotNull(returnCollection);
            Assert.IsTrue(returnCollection.Count > 0);
        }

        #endregion

        #region Publication.show() tests

        [TestMethod]
        public void publicationShow()
        {
            Publication pubObj = Publication.show(publicationID);
            Assert.IsNotNull(pubObj);
        }

        #endregion

        #region Publication.import() tests

        [TestMethod]
        public void publicationImport()
        {
            netClient.Core.APIClient.Instance.InitWebClient();
            netClient.Core.APIClient.Instance.Authenticate(servicesOAuthToken);
            ServerTemplate testST = Publication.import(publicationID);
            Assert.IsNotNull(testST);
            netClient.Core.APIClient.Instance.InitWebClient(); //reclean auth on the instance
        }

        #endregion
    }
}
