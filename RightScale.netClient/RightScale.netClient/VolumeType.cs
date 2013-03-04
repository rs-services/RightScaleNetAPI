using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class VolumeType
    {
        public string name { get; set; }
        public List<Action> actions { get; set; }
        public string resource_uid { get; set; }
        public string created_at { get; set; }
        public string size { get; set; }
        public string updated_at { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }

        public static VolumeType show(string cloudID, string volumeTypeID)
        {
            //TODO: implement VolumeType.show 
            throw new NotImplementedException();
        }


        #region VolumeType.index methods

        public static List<VolumeType> index()
        {
            return index(null, null);
        }

        public static List<VolumeType> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        public static List<VolumeType> index(string view)
        {
            return index(null, view);
        }

        public static List<VolumeType> index(List<KeyValuePair<string, string>> filter, string view)
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

            //TODO: implement VolumeType.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
