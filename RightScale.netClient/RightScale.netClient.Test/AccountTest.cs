using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RightScale.netClient;
using System.Configuration;
using System.Collections.Generic;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class AccountTest : RSAPITestBase
    {
        string accountID;

        public AccountTest()
        {
            accountID = ConfigurationManager.AppSettings["AccountTest_accountID"].ToString();
        }

        #region Account.show tests

        [TestMethod]
        public void showTest()
        {
            Account acct = Account.show(accountID);
            Assert.IsNotNull(acct, "Account came back as null - issue with API call");
        }

        #endregion

        #region Account Relationship tests

        [TestMethod]
        public void accountOwnerExists()
        {
            Account acct = Account.show(accountID);
            Assert.IsNotNull(acct, "Account came back as null - issue with API call");
            Account owner = acct.Owner;
            Assert.IsNotNull(owner);
        }

        [TestMethod]
        public void accountTags()
        {
            Account acct = Account.show(accountID);
            Assert.IsNotNull(acct, "Account came back as null - issue with API call");
            List<Tag> tags = acct.tags;
            Assert.IsTrue(true); //no exception is a good thing!
        }

        #endregion
    }
}
