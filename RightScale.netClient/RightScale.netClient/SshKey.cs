using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class SshKey
    {
        public List<object> actions { get; set; }
        public string resource_uid { get; set; }
        public List<Link> links { get; set; }

        
        #region SshKey.index methods

        public static List<SshKey> index()
        {
            return index(null, null);
        }

        public static List<SshKey> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        public static List<SshKey> index(string view)
        {
            return index(null, view);
        }

        public static List<SshKey> index(List<KeyValuePair<string, string>> filter, string view)
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

            List<string> validFilters = new List<string>() { "resource_uid" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement SshKey.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
