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
        public string os_platform { get; set; }
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

        
        #region Instance.index methods

        public static List<Instance> index()
        {
            return index(null, null);
        }

        public static List<Instance> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        public static List<Instance> index(string view)
        {
            return index(null, view);
        }

        public static List<Instance> index(List<KeyValuePair<string, string>> filter, string view)
        {
            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default", "extended", "full", "full_inputs_2_0" };
                Utility.CheckStringInput("view", validViews, view);
            }

            List<string> validFilters = new List<string>() { "datacenter_href", "deployment_href", "name", "os_platform", "parent_href", "private_dns_name", "private_ip_address", "public_dns_name", "public_ip_address", "resource_uid", "server_template_href", "state" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement Instance.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
