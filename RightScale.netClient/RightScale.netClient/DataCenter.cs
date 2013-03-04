using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class DataCenter
    {
        public string name { get; set; }
        public List<Action> actions { get; set; }
        public string resource_uid { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }

        
        #region DataCenter.index methods

        public static List<DataCenter> index()
        {
            return index(null, null);
        }

        public static List<DataCenter> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        public static List<DataCenter> index(string view)
        {
            return index(null, view);
        }

        public static List<DataCenter> index(List<KeyValuePair<string, string>> filter, string view)
        {
            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default" };
                Utility.CheckStringInput("view", validViews, view);
            }

            List<string> validFilters = new List<string>() { "name", "resource_uid" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement DataCenter.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
