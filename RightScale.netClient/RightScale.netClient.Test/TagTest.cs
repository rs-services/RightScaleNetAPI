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

        #region Tag parse tests

        [TestMethod]
        public void simpleTagParse()
        {
            Tag t = new Tag("scope:key=value");
            Assert.IsNotNull(t);
            Assert.IsTrue(t.scope == "scope");
            Assert.IsTrue(t.tagName == "key");
            Assert.IsTrue(t.tagValue == "value");
        }

        [TestMethod]
        public void colonTagParse()
        {
            Tag t = new Tag("scope:key:foo=value:bar");
            Assert.IsNotNull(t);
            Assert.IsTrue(t.scope=="scope");
            Assert.IsTrue(t.tagName=="key:foo");
            Assert.IsTrue(t.tagValue == "value:bar");
        }

        #endregion

        #region Tag.byresource tests

        [TestMethod]
        public void TagbyResourceTest()
        {
            List<string> arrHrefs = new List<string>() { serverhref, deploymenthref };
            
            List<Resource> resHref = Tag.byResource(arrHrefs);

            Assert.IsNotNull(resHref);
        }

        #endregion

        #region Tag.bytag tests

        [TestMethod]
        public void ByTagTestSingleTagStringMCI()
        {
            Tag searchTag = new Tag("provides:rs_agent_type=right_link");
            Assert.IsTrue(searchTag.scope == "provides");
            Assert.IsTrue(searchTag.tagName == "rs_agent_type");
            Assert.IsTrue(searchTag.tagValue == "right_link");
            Assert.IsTrue(searchTag.ToString() == "provides:rs_agent_type=right_link");
            List<Resource> resources = Tag.byTag(string.Empty, false, "multi_cloud_images", new List<Tag>() { searchTag });
            Assert.IsNotNull(resources);
            Assert.IsTrue(resources.Count > 0);
        }

        [TestMethod]
        public void ByTagTestTagByFieldMCI()
        {
            Tag searchTag = new Tag("provides", "rs_agent_type", "right_link");
            Assert.IsTrue(searchTag.scope == "provides");
            Assert.IsTrue(searchTag.tagName == "rs_agent_type");
            Assert.IsTrue(searchTag.tagValue == "right_link");
            Assert.IsTrue(searchTag.ToString() == "provides:rs_agent_type=right_link");
            List<Resource> resources = Tag.byTag(string.Empty, false, "multi_cloud_images", new List<Tag>() { searchTag });
            Assert.IsNotNull(resources);
            Assert.IsTrue(resources.Count > 0);
        }

        [TestMethod]
        public void ByTagTestSingleTagStringInstance()
        {
            Tag searchTag = new Tag("rs_monitoring:state=active");
            Assert.IsTrue(searchTag.scope == "rs_monitoring");
            Assert.IsTrue(searchTag.tagName == "state");
            Assert.IsTrue(searchTag.tagValue == "active");
            Assert.IsTrue(searchTag.ToString() == "rs_monitoring:state=active");
            List<Resource> resources = Tag.byTag(string.Empty, false, "instances", new List<Tag>() { searchTag });
            Assert.IsNotNull(resources);
            Assert.IsTrue(resources.Count > 0);
        }

        [TestMethod]
        public void ByTagTestTagByFieldInstance()
        {
            Tag searchTag = new Tag("rs_monitoring", "state", "active");
            Assert.IsTrue(searchTag.scope == "rs_monitoring");
            Assert.IsTrue(searchTag.tagName == "state");
            Assert.IsTrue(searchTag.tagValue == "active");
            Assert.IsTrue(searchTag.ToString() == "rs_monitoring:state=active");
            List<Resource> resources = Tag.byTag(string.Empty, false, "instances", new List<Tag>() { searchTag });
            Assert.IsNotNull(resources);
            Assert.IsTrue(resources.Count > 0);
        }

        #endregion

        #region Tag.multi_add and Tag.multi_delete tests

        [TestMethod]
        public void TagMultiAddMultiDeleteTest()
        {
            List<string> resourceHrefs = new List<string>() { deploymenthref, serverhref };
            List<Tag> tags = new List<Tag>();
            Tag testTag = new Tag("test:unittest=true"); //arbitrary tag value
            Tag unitTestTag = new Tag("unittest:fromvisualstudio=true"); //arbitrary tag value
            tags.Add(testTag);
            tags.Add(unitTestTag);
            bool success = Tag.multiAdd(resourceHrefs, tags);
            Assert.IsTrue(success);
            List<Resource> testTagServer = Tag.byTag(string.Empty, false, "servers", new List<Tag>() { testTag });
            Assert.IsNotNull(testTagServer);
            Assert.IsTrue(testTagServer.Count == 1);
            Assert.IsTrue(testTagServer[0].links[0].href == serverhref);
            List<Resource> unitTestTagServer = Tag.byTag(string.Empty, false, "servers", new List<Tag>() { unitTestTag });
            Assert.IsNotNull(unitTestTagServer);
            Assert.IsTrue(unitTestTagServer.Count == 1);
            Assert.IsTrue(unitTestTagServer[0].links[0].href == serverhref);
            List<Resource> testTagDeployment = Tag.byTag(string.Empty, false, "deployments", new List<Tag>() { testTag });
            Assert.IsNotNull(testTagDeployment);
            Assert.IsTrue(testTagDeployment.Count == 1);
            Assert.IsTrue(testTagDeployment[0].links[0].href == deploymenthref);
            List<Resource> unitTestTagDeployment = Tag.byTag(string.Empty, false, "deployments", new List<Tag>() { unitTestTag });
            Assert.IsNotNull(unitTestTagDeployment);
            Assert.IsTrue(unitTestTagDeployment.Count == 1);
            Assert.IsTrue(unitTestTagDeployment[0].links[0].href == deploymenthref);
            bool successDelete = Tag.multiDelete(resourceHrefs, tags);
            List<Resource> testTagServerDeleted = Tag.byTag(string.Empty, false, "servers", new List<Tag>() { testTag });
            Assert.IsNotNull(testTagServerDeleted);
            Assert.IsTrue(testTagServerDeleted.Count == 0);
            List<Resource> unitTestTagServerDeleted = Tag.byTag(string.Empty, false, "servers", new List<Tag>() { unitTestTag });
            Assert.IsNotNull(unitTestTagServerDeleted);
            Assert.IsTrue(unitTestTagServerDeleted.Count == 0);
            List<Resource> testTagDeploymentDeleted = Tag.byTag(string.Empty, false, "deployments", new List<Tag>() { testTag });
            Assert.IsNotNull(testTagDeploymentDeleted);
            Assert.IsTrue(testTagDeploymentDeleted.Count == 0);
            List<Resource> unitTestTagDeploymentDeleted = Tag.byTag(string.Empty, false, "deployments", new List<Tag>() { unitTestTag });
            Assert.IsNotNull(unitTestTagDeploymentDeleted);
            Assert.IsTrue(unitTestTagDeploymentDeleted.Count == 0);
        }

        #endregion

    }
}
