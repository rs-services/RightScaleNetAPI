using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class IPAddress
    {
        public string address { get; set; }
        public string name { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public List<Link> links { get; set; }

        
        #region IPAddress.index methods

        public static List<IPAddress> index()
        {
            return index(null);
        }

        public static List<IPAddress> index(List<KeyValuePair<string, string>> filter)
        {
            List<string> validFilters = new List<string>() { "name" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement IPAddress.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
