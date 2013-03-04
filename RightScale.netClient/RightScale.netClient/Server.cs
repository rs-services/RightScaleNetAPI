using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Server
    {
        public string name { get; set; }
        public List<Action> actions { get; set; }
        public string created_at { get; set; }
        public NextInstance next_instance { get; set; }
        public string updated_at { get; set; }
        public CurrentInstance current_instance { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }
        public string state { get; set; }

        
        #region Server.index methods

        public static List<Server> index()
        {
            return index(null, null);
        }

        public static List<Server> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        public static List<Server> index(string view)
        {
            return index(null, view);
        }

        public static List<Server> index(List<KeyValuePair<string, string>> filter, string view)
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

            //TODO: implement Server.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
