using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class AuditEntryTest
    {
        string auditEntryID;

        public AuditEntryTest()
        {
            auditEntryID = ConfigurationManager.AppSettings["AuditEntrytest_auditEntryID"].ToString();
        }

        [TestMethod]
        public void showTest()
        {
            AuditEntry ae = AuditEntry.show(auditEntryID);
            Assert.IsNotNull(ae, "Audit Entry is null - object did not get returned properly");
        }
    }
}
