using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RightScale.netClient.Test.objects
{
    [TestClass]
    public class PacingTest
    {
        [TestMethod]
        public void validTest()
        {
            Pacing p = new Pacing("2", "2", "15");
            Assert.IsNotNull(p);
        }

        [TestMethod]
        public void errorTest()
        {
            try
            {
                Pacing p = new Pacing("-1", "-1", "-2");
                Assert.Fail();
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }
    }
}
