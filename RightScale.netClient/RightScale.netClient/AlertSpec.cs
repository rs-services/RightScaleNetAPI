using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    //alert_spec
    public class AlertSpec
    {
        public string name { get; set; }
        public int duration { get; set; }
        public List<object> actions { get; set; }
        public string created_at { get; set; }
        public string threshold { get; set; }
        public string updated_at { get; set; }
        public string condition { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }
        public string file { get; set; }
        public string variable { get; set; }
        public string escalation_name { get; set; }
    }
}
