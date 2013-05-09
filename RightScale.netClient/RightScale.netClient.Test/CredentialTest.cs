using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class CredentialTest
    {
        [TestMethod]
        public void credentialIndexSimple()
        {
            Credential.index();
        }
    }
}
