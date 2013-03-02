using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class MonitoringMetric
    {
        public List<Action> actions { get; set; }
        public string plugin { get; set; }
        public string graph_href { get; set; }
        public string view { get; set; }
        public List<Link> links { get; set; }
    }
}
