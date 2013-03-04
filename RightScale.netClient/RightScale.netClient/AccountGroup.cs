using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class AccountGroup
    {
        public string name { get; set; }
        public List<Action> actions { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }

        
        #region AccountGroup.index methods

        public static List<AccountGroup> index()
        {
            return index(null, null);
        }

        public static List<AccountGroup> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        public static List<AccountGroup> index(string view)
        {
            return index(null, view);
        }

        public static List<AccountGroup> index(List<KeyValuePair<string, string>> filter, string view)
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

            //TODO: validate potential inputs with engineering
            List<string> validFilters = new List<string>() { "name" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement AccountGroup.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
