using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class SessionTest
    {

        public SessionTest()
        {
        }
        
        #region Cloud.index tests
        [TestMethod]
        public void SessionIndexSimple()
        {
            List<Session> listofSessions = Session.index();
            
            Assert.IsNotNull(listofSessions);
            //Assert.IsTrue(listofSessions.count > 0);
        }
        #endregion
    }
}
