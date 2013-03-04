using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class NextInstance
    {
        public string name { get; set; }
        public List<Action> actions { get; set; }
        public string resource_uid { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public List<string> public_ip_addresses { get; set; }
        public List<Link> links { get; set; }
        public string pricing_type { get; set; }
        public List<string> private_ip_addresses { get; set; }
        public string state { get; set; }
    }
}
