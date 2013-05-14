using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class MonitoringMetricData:Core.RightScaleObjectBase<MonitoringMetricData>
    {
        public List<VariablesData> variables_data { get; set; }
        public string start { get; set; }
        public string end { get; set; }

    }
}
