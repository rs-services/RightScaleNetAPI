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
            //this call is idempotent -- 68701
            string newUserID = "69353";

            User currentUser = User.show(newUserID);
            Assert.IsNotNull(currentUser);
            Assert.IsTrue(currentUser.ID.Length > 0);

            List<Filter> permFilter = new List<Filter>();
            permFilter.Add(new Filter("user_href", FilterOperator.Equal, string.Format(APIHrefs.UserByID, newUserID)));
            string permissionID = string.Empty;

            List<Permission> perms = Permission.index(permFilter);
            if (perms.Count == 0)
            {
                permissionID = Permission.create("observer", newUserID);
                Assert.IsNotNull(permissionID);
                Assert.IsTrue(permissionID.Length > 0);
            }

            bool isUpdated = User.update(newUserID, currentUser.email, null, "Thomas", "McClory", null, null, null, null);
            Assert.IsTrue(isUpdated);

            User updatedUser = User.show(newUserID);
            Assert.IsNotNull(updatedUser);
            Assert.IsTrue(updatedUser.ID.Length > 0);
            Assert.IsTrue(updatedUser.email == "patrick.mcclory@rightscale.com");
            Assert.IsTrue(updatedUser.first_name == "Thomas");

            bool isUpdatedAgain = User.update(newUserID, updatedUser.email, null, "Patrick", "McClory", null, null, null, null);
            Assert.IsTrue(isUpdatedAgain);

            User revertedUser = User.show(newUserID);
            Assert.IsNotNull(revertedUser);
            Assert.IsTrue(revertedUser.ID.Length > 0);
            Assert.IsTrue(revertedUser.email == "patrick.mcclory@rightscale.com");
            Assert.IsTrue(revertedUser.first_name == "Patrick");

            if (!string.IsNullOrWhiteSpace(permissionID))
            {
                bool isDestroyed = Permission.destroy(permissionID);
                Assert.IsTrue(isDestroyed);
            }
        }

        #endregion
    }
}
