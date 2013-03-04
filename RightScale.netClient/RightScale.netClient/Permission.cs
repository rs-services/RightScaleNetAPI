using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Permission
    {
        public List<Action> actions { get; set; }
        public string created_at { get; set; }
        public string role_title { get; set; }
        public List<Link> links { get; set; }

        
        #region Permission.index methods

        public static List<Permission> index()
        {
            return index(null);
        }

        public static List<Permission> index(List<KeyValuePair<string, string>> filter)
        {     
            List<string> validFilters = new List<string>() { "user_href" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement Permission.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
