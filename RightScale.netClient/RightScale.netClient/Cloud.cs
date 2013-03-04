using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Cloud:Core.RightScaleObjectBase<Cloud>
    {
        public string name { get; set; }
        public string cloud_type { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }

        #region Cloud.show() methods

        public static Cloud show(string cloudID)
        {
            Utility.CheckStringIsNumeric(cloudID);

            string getURL = string.Format("/api/clouds/{0}", cloudID);

            string jsonString = Core.APIClient.Instance.Get(getURL);

            return deserialize(jsonString);
        }

        #endregion

        #region Cloud.index methods

        public static List<Cloud> index()
        {
            return index(null);
        }

        public static List<Cloud> index(List<KeyValuePair<string, string>> filter)
        {

            List<string> validFilters = new List<string>() { "cloud_type", "description", "name" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement Cloud.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
