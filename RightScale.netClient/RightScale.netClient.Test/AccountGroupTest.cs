using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RightScale.netClient;
using System.Configuration;
using System.Collections.Generic;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class AccountGroupTest
    {
        string accountGroupID;

        public AccountGroupTest()
        {
            accountGroupID = ConfigurationManager.AppSettings["AccountGroup_AccountGroupID"].ToString();
        }

        #region AccountGroup.show tests

        [TestMethod]
        public void showTestFull()
        {
            AccountGroup ag = AccountGroup.show(accountGroupID, "default");
            Assert.IsNotNull(ag, "Account Group is null--bad call to API");
        }

        [TestMethod]
        public void showTestMinimum()
        {
            AccountGroup ag = AccountGroup.show(accountGroupID);
            Assert.IsNotNull(ag, "Account Group is null--bad call to API");
        }

        #endregion

        #region AccountGroup.index tests

        [TestMethod]
        public void indexTestFull()
        {
            List<AccountGroup> acctGroupList = AccountGroup.index();
            Assert.IsNotNull(acctGroupList);
        }

        #endregion
    }
}
