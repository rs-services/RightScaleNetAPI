using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RightScale.netClient;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class AccountTest
    {
        string accountID;

        public AccountTest()
        {
            accountID = ConfigurationManager.AppSettings["AccountTest_accountID"].ToString();
        }

        [TestMethod]
        public void showTest()
        {
            Account acct = Account.show(accountID);
            Assert.IsNotNull(acct, "Account came back as null - issue with API call");
        }
    }
}
