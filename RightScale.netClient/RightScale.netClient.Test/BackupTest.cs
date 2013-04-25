using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class BackupTest : RSAPITestBase
    {
        string backupID;

        public BackupTest()
        {
            backupID = ConfigurationManager.AppSettings["BackupTest_backupID"].ToString();
        }

    }
}
