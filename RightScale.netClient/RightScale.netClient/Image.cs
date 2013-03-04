using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Image
    {
        public string name { get; set; }
        public List<Action> actions { get; set; }
        public string resource_uid { get; set; }
        public string cpu_architecture { get; set; }
        public string image_type { get; set; }
        public string virtualization_type { get; set; }
        public string os_platform { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }
        public string visibility { get; set; }

        
        #region Image.index methods

        public static List<Image> index()
        {
            return index(null, null);
        }

        public static List<Image> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        public static List<Image> index(string view)
        {
            return index(null, view);
        }

        public static List<Image> index(List<KeyValuePair<string, string>> filter, string view)
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

            List<string> validFilters = new List<string>() { "cpu_architecture", "description", "image_type", "name", "os_platform", "resource_uid", "visibility" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement Image.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
