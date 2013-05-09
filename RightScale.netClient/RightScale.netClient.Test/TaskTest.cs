using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Configuration;
using System.Collections.Generic;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class TaskTest
    {
        [TestMethod]
        public void simpleTaskTest()
        {
            Instance currentInstance = Server.show("742698001").currentInstance;
            Task scriptRunTask = Instance.run_rightScript(currentInstance.cloud.ID, currentInstance.ID, "412893001");
            string results = string.Empty;

            while (true)
            {
                scriptRunTask.Refresh();
                results += DateTime.Now.ToString() + ": " + scriptRunTask.summary + "|" + scriptRunTask.detail + Environment.NewLine;
                if (scriptRunTask.summary.ToLower().StartsWith("completed"))
                {
                    Assert.IsTrue(true);
                    break;
                }
                System.Threading.Thread.Sleep(500);
            }
        }
    }
}
