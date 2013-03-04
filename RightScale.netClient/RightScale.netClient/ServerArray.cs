using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    class ServerArray
    {
        public string name { get; set; }
        public List<Action> actions { get; set; }
        public ElasticityParams elasticity_params { get; set; }
        public NextInstance next_instance { get; set; }
        public string array_type { get; set; }
        public int instances_count { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }
        public string state { get; set; }

        
        #region ServerArray.index methods

        public static List<ServerArray> index()
        {
            return index(null, null);
        }

        public static List<ServerArray> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        public static List<ServerArray> index(string view)
        {
            return index(null, view);
        }

        public static List<ServerArray> index(List<KeyValuePair<string, string>> filter, string view)
        {
            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default", "instance_detail" };
                Utility.CheckStringInput("view", validViews, view);
            }

            List<string> validFilters = new List<string>() { "cloud_href", "deployment_href", "name" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement ServerArray.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
