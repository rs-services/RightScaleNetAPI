using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Collections.Generic;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class AuditEntryTest
    {
        string auditEntryID;
        string serverID;
        string serverArrayID;

        public AuditEntryTest()
        {
            auditEntryID = ConfigurationManager.AppSettings["AuditEntryTest_auditEntryID"].ToString();
            serverID = ConfigurationManager.AppSettings["AuditEntryTest_serverID"].ToString();
            serverArrayID = ConfigurationManager.AppSettings["AuditEntryTest_serverArrayID"].ToString();
        }

        [TestMethod]
        public void showTest()
        {
            AuditEntry ae = AuditEntry.show(auditEntryID);
            Assert.IsNotNull(ae, "Audit Entry is null - object did not get returned properly");
        }

        [TestMethod]
        public void indexSimpleTest()
        {
            List<AuditEntry> auditEntries = AuditEntry.index(DateTime.Now.AddDays(-10));
            Assert.IsNotNull(auditEntries);
        }

        [TestMethod]
        public void createTest()
        {
            string auditEntryID = AuditEntry.create(string.Format("/api/server_arrays/{0}", serverArrayID), "this is a summary");
            Assert.IsNotNull(auditEntryID);
        }

        [TestMethod]
        public void createDetailedTest()
        {
            string auditEntryID = AuditEntry.create(string.Format("/api/server_arrays/{0}", serverArrayID), "this is a summary, fool!", "here are the deets...");
            Assert.IsNotNull(auditEntryID);
        }
    }
}
