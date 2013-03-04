using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class ServerTemplateMultiCloudImage
    {
        public List<Action> actions { get; set; }
        public string created_at { get; set; }
        public bool is_default { get; set; }
        public string updated_at { get; set; }
        public List<Link> links { get; set; }


        #region ServerTemplateMultiCloudImage.index methods

        public static List<ServerTemplateMultiCloudImage> index()
        {
            return index(null, null);
        }

        public static List<ServerTemplateMultiCloudImage> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        public static List<ServerTemplateMultiCloudImage> index(string view)
        {
            return index(null, view);
        }

        public static List<ServerTemplateMultiCloudImage> index(List<KeyValuePair<string, string>> filter, string view)
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

            List<string> validFilters = new List<string>() { "is_default", "multi_cloud_image_href", "server_template_href" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement ServerTemplateMultiCloudImage.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
