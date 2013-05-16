using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class PermissionUnitTest : RSAPITestBase
    {
        Permission testPermission;

        public PermissionUnitTest()
        {
            List<Permission> permList = Permission.index();
            Assert.IsNotNull(permList);
            Assert.IsTrue(permList.Count > 0);
            testPermission = permList[0];
        }

        [TestMethod]
        public void permissionIndexTest()
        {
            List<Permission> permList = Permission.index();
            Assert.IsNotNull(permList);
            Assert.IsTrue(permList.Count > 0);
        }

        [TestMethod]
        public void permissionIndexTestFull()
        {
            List<Permission> fullPermList = Permission.index();
            Assert.IsNotNull(fullPermList);
            Assert.IsTrue(fullPermList.Count > 0);

            List<Filter> filter = new List<Filter>();
            filter.Add(new Filter("user_href", FilterOperator.Equal, @"/api/users/14074"));
            List<Permission> permList = Permission.index(filter);
            Assert.IsNotNull(permList);
            Assert.IsTrue(permList.Count > 0);

            Assert.IsTrue(fullPermList.Count > permList.Count);
        }

        [TestMethod]
        public void permissionCreateDestroy()
        {
            //this call is stolen from the User.create tests
            string newUserID = User.create("fakeuser@rightscale.com", "Fake", "McUser", "McCompany", "8058675309", "jenny123!@#user");
            Assert.IsNotNull(newUserID);
            Assert.IsTrue(newUserID.Length > 0);
            
            List<Filter> filter = new List<Filter>();
            filter.Add(new Filter("user_href", FilterOperator.Equal, string.Format(APIHrefs.UserByID, newUserID)));
            List<Permission> permissionList = Permission.index(filter);
            if(permissionList.Count >0)
            {
                foreach(Permission p in permissionList)
                {
                    if(Permission.destroy(p.ID))
                    {
                        Assert.IsTrue(true,"Successfully deleted permission on test user with permission id of " + p.ID);
                    }
                    else
                    {
                        Assert.Fail("Failed to delete existing permission on test user with permission id of " + p.ID);
                    }
                }
            }

            string permissionID = Permission.create("observer", newUserID);
            Assert.IsNotNull(permissionID);
            Assert.IsTrue(permissionID.Length > 0);

            User user = User.show(newUserID);
            Assert.IsNotNull(user);
            Assert.IsTrue(user.ID == newUserID);

            bool isDestroyed = Permission.destroy(permissionID);
            Assert.IsTrue(isDestroyed);
            try
            {
                User badUser = User.show(newUserID);
                Assert.Fail("User should not be queryable at this point as no permissions exist for the user");
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

    }
}
