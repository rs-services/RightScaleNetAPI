using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Collections.Generic;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class AuditEntryTest : RSAPITestBase
    {
        string auditEntryID;

        public AuditEntryTest()
        {
            auditEntryID = ConfigurationManager.AppSettings["AuditEntryTest_auditEntryID"].ToString();
        }

        #region AuditEntry.show tests

        [TestMethod]
        public void showTest()
        {
            AuditEntry ae = AuditEntry.show(auditEntryID);
            Assert.IsNotNull(ae, "Audit Entry is null - object did not get returned properly");
        }

        #endregion

        #region AuditEntry.index tests

        [TestMethod]
        public void indexSimpleTest()
        {
            List<AuditEntry> auditEntries = AuditEntry.index(DateTime.Now.AddDays(-10));
            Assert.IsNotNull(auditEntries);
        }

        #endregion

        #region AuditEntry.create tests

        [TestMethod]
        public void createTest()
        {
            string auditEntryID = AuditEntry.create(string.Format(APIHrefs.ServerArrayById, liveTestServerArrayID), "this is a summary");
            Assert.IsNotNull(auditEntryID);
        }

        [TestMethod]
        public void createDeploymentAuditEntryTest()
        {
            string auditEntryID = AuditEntry.create(Utility.deploymentHref(liveTestDeploymentID), "this is an audit entry " + DateTime.Now.ToString());
            Assert.IsNotNull(auditEntryID);
        }

        [TestMethod]
        public void createDetailedTest()
        {
            string auditEntryID = AuditEntry.create(string.Format(APIHrefs.ServerArrayById, liveTestServerArrayID), "this is a summary, fool!", "here are the deets...");
            Assert.IsNotNull(auditEntryID);
        }

        [TestMethod]
        public void createAndUpdateTest()
        {
            string auditEntryID = AuditEntry.create(string.Format(APIHrefs.ServerArrayById, liveTestServerArrayID), "this is a summary");
            Assert.IsNotNull(auditEntryID);
            AuditEntry ae1 = AuditEntry.show(auditEntryID);
            Assert.IsNotNull(ae1);
            bool updateResult = AuditEntry.update(auditEntryID, "this is a new summary");
            Assert.IsTrue(updateResult);
            AuditEntry ae2 = AuditEntry.show(auditEntryID);
            Assert.IsNotNull(ae2);
            Assert.AreNotEqual(ae1.summary, ae2.summary);
        }

        [TestMethod]
        public void createAndAppendTest()
        {
            string auditEntryID = AuditEntry.create(string.Format(APIHrefs.ServerArrayById, liveTestServerArrayID), "this is a summary", "here are some details");
            Assert.IsNotNull(auditEntryID);
            string detailResults1 = AuditEntry.detail(auditEntryID);
            Assert.IsNotNull(detailResults1);
            bool appendResult = AuditEntry.append(auditEntryID, "this is a more audit detail", "1");
            Assert.IsTrue(appendResult);
            string detailResults2 = AuditEntry.detail(auditEntryID);
            Assert.IsNotNull(detailResults2);
            Assert.AreNotEqual(detailResults1, detailResults2);
        }

        #endregion
    }
}
