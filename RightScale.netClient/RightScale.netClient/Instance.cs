using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Instance
    {
        public string name { get; set; }
        public List<Action> actions { get; set; }
        public string resource_uid { get; set; }
        public string created_at { get; set; }
        public List<Input> inputs { get; set; }
        public List<string> private_dns_names { get; set; }
        public string monitoring_id { get; set; }
        public object os_platform { get; set; }
        public string updated_at { get; set; }
        public List<string> public_ip_addresses { get; set; }
        public string monitoring_server { get; set; }
        public string terminated_at { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }
        public string pricing_type { get; set; }
        public List<string> private_ip_addresses { get; set; }
        public string user_data { get; set; }
        public string state { get; set; }
        public List<SecurityGroup> security_groups { get; set; }
        public List<string> public_dns_names { get; set; }
    }
}
