using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class InstanceType
    {
        public string name { get; set; }
        public List<Action> actions { get; set; }
        public string resource_uid { get; set; }
        public string cpu_architecture { get; set; }
        public string local_disks { get; set; }
        public string memory { get; set; }
        public string local_disk_size { get; set; }
        public string cpu_count { get; set; }
        public string cpu_speed { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }

        
        #region InstanceType.index methods

        public static List<InstanceType> index()
        {
            return index(null, null);
        }

        public static List<InstanceType> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        public static List<InstanceType> index(string view)
        {
            return index(null, view);
        }

        public static List<InstanceType> index(List<KeyValuePair<string, string>> filter, string view)
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

            List<string> validFilters = new List<string>() { "cpu_architecture", "description", "name", "resource_uid" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement InstanceType.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
