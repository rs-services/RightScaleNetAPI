using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class User
    {
        public string company { get; set; }
        public List<Action> actions { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public List<Link> links { get; set; }
        public string last_name { get; set; }
        public string phone { get; set; }
        public string first_name { get; set; }
        public string email { get; set; }


        #region User.index methods

        public static List<User> index()
        {
            return index(null);
        }

        public static List<User> index(List<KeyValuePair<string, string>> filter)
        {
            List<string> validFilters = new List<string>() { "email", "first_name", "last_name" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement User.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
