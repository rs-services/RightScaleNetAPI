using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Collections.Generic;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class UserUnitTest :RSAPITestBase
    {
        #region User.Index tests

        [TestMethod]
        public void userIndexSimple()
        {
            List<User> userList = User.index();
            Assert.IsNotNull(userList);
            Assert.IsTrue(userList.Count > 0);
        }

        [TestMethod]
        public void userIndexEmailFilteredTest()
        {
            List<Filter> filters = new List<Filter>();
            filters.Add(new Filter("email", FilterOperator.Equal, "patrick@rightscale.com"));
            List<User> userList = User.index(filters);
            Assert.IsNotNull(userList);
            Assert.IsTrue(userList.Count > 0);
        }

        [TestMethod]
        public void userIndexNameFilteredTest()
        {
            List<Filter> filters = new List<Filter>();
            filters.Add(new Filter("first_name", FilterOperator.Equal, "patr"));
            filters.Add(new Filter("last_name", FilterOperator.Equal, "mccl"));
            List<User> userList = User.index(filters);
            Assert.IsNotNull(userList);
            Assert.IsTrue(userList.Count > 0);
        }

        #endregion

        #region User.show tests

        [TestMethod]
        public void userShowTest()
        {
            User specificUser = User.show("57366");
            Assert.IsNotNull(specificUser);
            Assert.IsTrue(specificUser.first_name == "Patrick");
            Assert.IsTrue(specificUser.last_name == "McClory");
            Assert.IsTrue(specificUser.email == "patrick@rightscale.com");
        }

        #endregion

        #region User.create tests

        [TestMethod]
        public void userSimpleCreate()
        {
            //this call is idempotent -- 68701
            string newUserID = User.create("fakeuser@rightscale.com", "Fake", "McUser", "McCompany", "8058675309", "jenny123!@#user");
            Assert.IsNotNull(newUserID);
            Assert.IsTrue(newUserID.Length > 0);
        }

        [TestMethod]
        public void userFullCreate()
        {
            //this call is idempotent -- 68702
            string newUserID = User.create("fakecomplicateduser@rightscale.com", "Fake-er", "McUser", "McCompany", "1235551212", "Th15is@p@ssw0rD", null, null);
            Assert.IsNotNull(newUserID);
            Assert.IsTrue(newUserID.Length > 0);
        }

        #endregion

        #region User.update tests

        [TestMethod]
        public void userSimpleUpdate()
        {
            bool isUpdated = User.update("68701", "fakeuser@rightscale.com", "fakeuser2@rightscale.com", null, null, null, null, null, null);
            Assert.IsTrue(isUpdated);
            bool isUpdatedAgain = User.update("68701", "fakeuser2@rightscale.com", "fakeuser2@rightscale.com", null, null, null, null, null, null);
            Assert.IsTrue(isUpdatedAgain);
        }

        #endregion
    }
}
