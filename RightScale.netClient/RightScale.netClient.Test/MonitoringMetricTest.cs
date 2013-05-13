using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class MonitoringMetricTest : RSAPITestBase
    {
        Instance currentInstance;
        string testPeriod;
        string testTitle;
        string testSize;
        string testTimeZone;

        public MonitoringMetricTest()
        {
            currentInstance = Server.show(liveTestServerID).currentInstance;
            Assert.IsNotNull(currentInstance);
            Assert.IsTrue(currentInstance.ID.Length > 0);

            this.testTimeZone = @"America/New_York";
            this.testPeriod = "now";
            this.testTitle = "This is a title";
            this.testSize = "large";
        }

        #region MontioringMetric.index tests

        [TestMethod]
        public void monitoringMetricIndexSimple()
        {
            List<MonitoringMetric> mmListFull = MonitoringMetric.index(azureCloudID, currentInstance.ID);
            Assert.IsNotNull(mmListFull);
            Assert.IsTrue(mmListFull.Count > 0);
        }

        [TestMethod]
        public void monitoringMetricServerIndexSimple()
        {
            List<MonitoringMetric> mmListFull = MonitoringMetric.index(liveTestServerID);
            Assert.IsNotNull(mmListFull);
            Assert.IsTrue(mmListFull.Count > 0);

            foreach (MonitoringMetric mm in mmListFull)
            {
                Assert.IsTrue(!string.IsNullOrWhiteSpace(mm.graph_href));
            }
        }

        [TestMethod]
        public void monitoringMetricFilteredSimple()
        {
            List<Filter> filter = new List<Filter>();
            filter.Add(new Filter("view", FilterOperator.Equal, "users"));

            List<MonitoringMetric> mmListFull = MonitoringMetric.index(currentInstance.cloud.ID, currentInstance.ID);
            Assert.IsNotNull(mmListFull);
            Assert.IsTrue(mmListFull.Count > 0);

            List<MonitoringMetric> mmListFiltered = MonitoringMetric.index(currentInstance.cloud.ID, currentInstance.ID, filter);
            Assert.IsNotNull(mmListFiltered);
            Assert.IsTrue(mmListFiltered.Count > 0);

            Assert.IsTrue(mmListFull.Count > mmListFiltered.Count);

            foreach (MonitoringMetric mm in mmListFull)
            {
                Assert.IsTrue(!string.IsNullOrWhiteSpace(mm.graph_href));
            }

            foreach (MonitoringMetric mm in mmListFiltered)
            {
                Assert.IsTrue(!string.IsNullOrWhiteSpace(mm.graph_href));
            }
        }

        [TestMethod]
        public void monitoringMetricServerFilteredSimple()
        {
            List<Filter> filter = new List<Filter>();
            filter.Add(new Filter("view", FilterOperator.Equal, "users"));

            List<MonitoringMetric> mmListFull = MonitoringMetric.index(liveTestServerID);
            Assert.IsNotNull(mmListFull);
            Assert.IsTrue(mmListFull.Count > 0);

            List<MonitoringMetric> mmListFiltered = MonitoringMetric.index(liveTestServerID, filter);
            Assert.IsNotNull(mmListFiltered);
            Assert.IsTrue(mmListFiltered.Count > 0);

            Assert.IsTrue(mmListFull.Count > mmListFiltered.Count);

            foreach (MonitoringMetric mm in mmListFull)
            {
                Assert.IsTrue(!string.IsNullOrWhiteSpace(mm.graph_href));
            }

            foreach (MonitoringMetric mm in mmListFiltered)
            {
                Assert.IsTrue(!string.IsNullOrWhiteSpace(mm.graph_href));
            }
        }

        [TestMethod]
        public void monitoringMetricServerFull()
        {
            List<Filter> filter = new List<Filter>();
            filter.Add(new Filter("view", FilterOperator.Equal, "users"));
            List<MonitoringMetric> mmList = MonitoringMetric.index(liveTestServerID, filter, testPeriod, testSize, testTitle, testTimeZone);
            Assert.IsNotNull(mmList);
            Assert.IsTrue(mmList.Count > 0);

            foreach (MonitoringMetric mm in mmList)
            {
                Assert.IsTrue(!string.IsNullOrWhiteSpace(mm.graph_href));
            }
        }

        [TestMethod]
        public void monitoringMetricFull()
        {
            List<Filter> filter = new List<Filter>();
            filter.Add(new Filter("view", FilterOperator.Equal, "users"));
            List<MonitoringMetric> mmList = MonitoringMetric.index(currentInstance.cloud.ID, currentInstance.ID, filter, testPeriod, testSize, testTitle, testTimeZone);
            Assert.IsNotNull(mmList);
            Assert.IsTrue(mmList.Count > 0);

            foreach (MonitoringMetric mm in mmList)
            {
                Assert.IsTrue(!string.IsNullOrWhiteSpace(mm.graph_href));
            }
        }

        #endregion

        #region MonitoringMetric.show tests

        [TestMethod]
        public void monitoringMetricShowSimple()
        {
            List<MonitoringMetric> mmListFull = MonitoringMetric.index(azureCloudID, currentInstance.ID);
            Assert.IsNotNull(mmListFull);
            Assert.IsTrue(mmListFull.Count > 0);
            //List<MonitoringMetricData> data = mmListFull[0].monitoringMetricData;
        }

        #endregion
    }
}
