using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class VolumeAttachment
    {
        public string device { get; set; }
        public List<Action> actions { get; set; }
        public string resource_uid { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string device_id { get; set; }
        public List<Link> links { get; set; }
        public string state { get; set; }


        #region VolumeAttachment.index methods

        public static List<VolumeAttachment> index()
        {
            return index(null, null);
        }

        public static List<VolumeAttachment> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        public static List<VolumeAttachment> index(string view)
        {
            return index(null, view);
        }

        public static List<VolumeAttachment> index(List<KeyValuePair<string, string>> filter, string view)
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

            List<string> validFilters = new List<string>() { "instance_href", "resource_uid", "volume_href" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement VolumeAttachment.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
