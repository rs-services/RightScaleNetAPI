using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class SshKeyTest : RSAPITestBase
    {
        string servicesOauthToken;
        string accountID;
        string testSSHKeyName;

        public SshKeyTest()
        {
            this.servicesOauthToken = ConfigurationManager.AppSettings["RightScaleServicesAPIRefreshToken"].ToString();
            this.accountID = ConfigurationManager.AppSettings["SshKeyTest_accountID"].ToString(); //This is the Services Unified acct id
            this.testSSHKeyName = ConfigurationManager.AppSettings["SshKeyTest_sshKeyName"].ToString();
        }

        [TestMethod]
        public void sshIndexSimple()
        {
            netClient.Core.APIClient.Instance.InitWebClient();
            netClient.Core.APIClient.Instance.Authenticate(this.authUserName, this.authPassword, this.accountID);

            List<SshKey> sshKeys = SshKey.index(this.awsUSEastCloudID);
            Assert.IsNotNull(sshKeys);
            Assert.IsTrue(sshKeys.Count > 0);

            netClient.Core.APIClient.Instance.InitWebClient();
        }

        [TestMethod]
        public void sshCreateIndexShowDestroy()
        {
            netClient.Core.APIClient.Instance.InitWebClient();
            netClient.Core.APIClient.Instance.Authenticate(this.authUserName, this.authPassword, this.accountID);

            List<SshKey> sshKeys = SshKey.index(this.awsUSEastCloudID);
            Assert.IsNotNull(sshKeys);
            Assert.IsTrue(sshKeys.Count > 0);

            string sshID = SshKey.create(this.awsUSEastCloudID, this.testSSHKeyName);
            Assert.IsNotNull(sshID);
            Assert.IsTrue(sshID.Length > 0);

            SshKey showTest = SshKey.show(this.awsUSEastCloudID, sshID);
            Assert.IsNotNull(showTest);
            Assert.IsTrue(showTest.ID == sshID);

            List<SshKey> newSshKeys = SshKey.index(this.awsUSEastCloudID);
            Assert.IsNotNull(newSshKeys);
            Assert.IsTrue(newSshKeys.Count > 0);

            Assert.IsTrue(newSshKeys.Count > sshKeys.Count);

            bool isDestroyed = SshKey.destroy(this.awsUSEastCloudID, sshID);
            Assert.IsTrue(isDestroyed);

            List<SshKey> backToNormalSSHKeys = SshKey.index(this.awsUSEastCloudID);
            Assert.IsNotNull(backToNormalSSHKeys);
            Assert.IsTrue(backToNormalSSHKeys.Count > 0);

            Assert.IsTrue(backToNormalSSHKeys.Count == sshKeys.Count);

            netClient.Core.APIClient.Instance.InitWebClient();
        }
    }
}
