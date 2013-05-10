using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class CredentialTest
    {

        private string testCredentialID;

        public CredentialTest()
        {
            testCredentialID = ConfigurationManager.AppSettings["CredentialTest_credentialID"].ToString();
        }
        
        [TestMethod]
        public void credentialIndexSimple()
        {
            List<Credential> credList = Credential.index();
            Assert.IsNotNull(credList);
            Assert.IsTrue(credList.Count > 0);
        }

        [TestMethod]
        public void credentialShowSimple()
        {
            Credential cred = Credential.show(testCredentialID);
            Assert.IsNotNull(cred);
            Assert.IsTrue(cred.value.ToString().Length > 0);
        }

        [TestMethod]
        public void credentialCreateDeleteSimple()
        {
            string credID = Credential.create("testCredential", DateTime.Now.ToString());
            Assert.IsNotNull(credID);
            Assert.IsTrue(credID.Length > 0);
            bool credDeleted = Credential.destroy(credID);
            Assert.IsTrue(credDeleted);
        }

        [TestMethod]
        public void credentialCreateUpdateDelete()
        {
            string firstCredValue = DateTime.Now.ToString();
            string credID = Credential.create("testCredential", firstCredValue);
            Assert.IsNotNull(credID);
            Assert.IsTrue(credID.Length > 0);

            Credential firstCredSet = Credential.show(credID);
            
            string secondCredValue = DateTime.Now.ToString();
            bool credUpdated = Credential.update(credID, string.Empty, secondCredValue);
            Assert.IsTrue(credUpdated);
            
            Credential secondCredSet = Credential.show(credID);
            
            Assert.IsTrue(firstCredSet.href == secondCredSet.href);
            Assert.IsTrue(firstCredSet.name == secondCredSet.name);
            Assert.IsTrue(firstCredSet.value != secondCredSet.value);
            
            bool credDeleted = Credential.destroy(credID);
            Assert.IsTrue(credDeleted);
        }
    }
}
