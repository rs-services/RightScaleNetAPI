using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class ElasticityParams
    {
        public AlertSpecificParams alert_specific_params { get; set; }
        public Bounds bounds { get; set; }
        public Pacing pacing { get; set; }
        public List<ScheduleEntry> schedule_entries { get; set; }
    }
}
