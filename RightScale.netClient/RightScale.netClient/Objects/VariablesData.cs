using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// Internal object for MonitoringMetricData class that holds individual metric data sets
    /// </summary>
    public class VariablesData
    {
        /// <summary>
        /// data points for this given data collection
        /// </summary>
        public List<double> points { get; set; }

        /// <summary>
        /// Name of the data collection being presented
        /// </summary>
        public string variable { get; set; }
    }
}
