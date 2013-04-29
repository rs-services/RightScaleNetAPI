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
    }
}
