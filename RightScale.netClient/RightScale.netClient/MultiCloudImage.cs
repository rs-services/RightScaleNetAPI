using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class MultiCloudImage
    {
        public string name { get; set; }
        public List<Action> actions { get; set; }
        public int revision { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }


        #region MultiCloudImage.index methods

        public static List<MultiCloudImage> index()
        {
            return index(null);
        }

        public static List<MultiCloudImage> index(List<KeyValuePair<string, string>> filter)
        {
            List<string> validFilters = new List<string>() { "name", "description", "revision" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement MultiCloudImage.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
