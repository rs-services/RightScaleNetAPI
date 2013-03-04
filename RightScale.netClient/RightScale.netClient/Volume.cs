using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Volume
    {
        public string name { get; set; }
        public List<Action> actions { get; set; }
        public string resource_uid { get; set; }
        public string created_at { get; set; }
        public int size { get; set; }
        public string updated_at { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }
        public VolumeType volume_type { get; set; }
        public string status { get; set; }

        public bool create()
        {
            //TODO: implement Volume.create
            throw new NotImplementedException();
        }

        public bool destroy()
        {
            //TODO: implement Volume.destroy
            throw new NotImplementedException();
        }

        
        #region Volume.index methods

        public static List<Volume> index()
        {
            return index(null, null);
        }

        public static List<Volume> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        public static List<Volume> index(string view)
        {
            return index(null, view);
        }

        public static List<Volume> index(List<KeyValuePair<string, string>> filter, string view)
        {
            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            List<string> validViews = new List<string>() { "default", "extended" };
            Utility.CheckStringInput("view", validViews, view);

            List<string> validFilters = new List<string>() { "datacenter_href", "description", "name", "parent_volume_snapshot_href", "resource_uid" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement Volume.index
            throw new NotImplementedException();
        }
        #endregion
		

        public static Volume show(string volumeId)
        {
            //TODO: implement Volume.show
            throw new NotImplementedException();
        }

        public static bool destroy(string cloudID, string volumeID)
        {
            //TODO: implement static Volume.destroy
            throw new NotImplementedException();
        }
    }
}
