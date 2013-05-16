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

        string monitoringMetricID;
        string monitoringMetricOverviewID;

        public MonitoringMetricTest()
        {
            currentInstance = Server.show(liveTestServerID).currentInstance;
            Assert.IsNotNull(currentInstance);
            Assert.IsTrue(currentInstance.ID.Length > 0);

            this.testTimeZone = @"America/New_York";
            this.testPeriod = "now";
            this.testTitle = "This is a title";
            this.testSize = "large";
            this.monitoringMetricID = "cpu-0:cpu-idle"; //this should be present on all servers.. hopefully
            this.monitoringMetricOverviewID = "cpu-0:cpu_overview"; //this should be present on all servers.. hopefully
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
        public void monitoringMetricShowInstance()
        {
            MonitoringMetric metric = MonitoringMetric.show(this.currentInstance, this.monitoringMetricID);
            Assert.IsNotNull(metric);
            Assert.IsTrue(metric.ID == this.monitoringMetricID);
        }

        [TestMethod]
        public void monitoringMetricShowServer()
        {
            MonitoringMetric metric = MonitoringMetric.show(this.liveTestServerID, this.monitoringMetricID);
            Assert.IsNotNull(metric);
            Assert.IsTrue(metric.ID == this.monitoringMetricID);
        }

        [TestMethod]
        public void monitoringMetricShowBase()
        {
            MonitoringMetric metric = MonitoringMetric.show(this.currentInstance.cloud.ID, this.currentInstance.ID, this.monitoringMetricID);
            Assert.IsNotNull(metric);
            Assert.IsTrue(metric.ID == this.monitoringMetricID);
        }

        [TestMethod]
        public void monitoringMetricShowInstanceFull()
        {
            MonitoringMetric metric = MonitoringMetric.show(this.currentInstance, this.monitoringMetricID, this.testPeriod, this.testSize, this.testTitle, this.testTimeZone);
            Assert.IsNotNull(metric);
            Assert.IsTrue(metric.ID == this.monitoringMetricID);
        }

        [TestMethod]
        public void monitoringMetricShowServerFull()
        {
            MonitoringMetric metric = MonitoringMetric.show(this.liveTestServerID, this.monitoringMetricID, this.testPeriod, this.testSize, this.testTitle, this.testTimeZone);
            Assert.IsNotNull(metric);
            Assert.IsTrue(metric.ID == this.monitoringMetricID);
        }

        [TestMethod]
        public void monitoringMetricShowBaseFull()
        {
            MonitoringMetric metric = MonitoringMetric.show(this.currentInstance.cloud.ID, this.currentInstance.ID, this.monitoringMetricID, this.testPeriod, this.testSize, this.testTitle, this.testTimeZone);
            Assert.IsNotNull(metric);
            Assert.IsTrue(metric.ID == this.monitoringMetricID);
        }

        #endregion

        #region MonitoringMetric.data tests

        [TestMethod]
        public void MonitoringMetricDataInstance()
        {
            MonitoringMetricData data = MonitoringMetric.data(this.currentInstance, this.monitoringMetricID, "0", "-3600");
            Assert.IsNotNull(data);
            Assert.IsTrue(data.variables_data.Count > 0);
        }

        [TestMethod]
        public void MonitoringMetricDataServer()
        {
            MonitoringMetricData data = MonitoringMetric.data(this.liveTestServerID, this.monitoringMetricID, "0", "-3600");
            Assert.IsNotNull(data);
            Assert.IsTrue(data.variables_data.Count > 0);
        }
        [TestMethod]
        public void MonitoringMetricDataSimple()
        {
            MonitoringMetricData data = MonitoringMetric.data(this.currentInstance.cloud.ID, this.currentInstance.ID, this.monitoringMetricID, "0", "-3600");
            Assert.IsNotNull(data);
            Assert.IsTrue(data.variables_data.Count > 0);
        }

        #endregion
    }
}
