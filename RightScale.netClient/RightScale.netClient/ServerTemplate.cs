using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class ServerTemplate
    {
        public string name { get; set; }
        public List<Action> actions { get; set; }
        public List<Input> inputs { get; set; }
        public int revision { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }

        
        #region ServerTemplate.index methods

        public static List<ServerTemplate> index()
        {
            return index(null, null);
        }

        public static List<ServerTemplate> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        public static List<ServerTemplate> index(string view)
        {
            return index(null, view);
        }

        public static List<ServerTemplate> index(List<KeyValuePair<string, string>> filter, string view)
        {
            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default", "inputs", "inputs_2_0" };
                Utility.CheckStringInput("view", validViews, view);
            }

            List<string> validFilters = new List<string>() { "description", "multi_cloud_image_href", "name", "revision" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement ServerTemplate.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
