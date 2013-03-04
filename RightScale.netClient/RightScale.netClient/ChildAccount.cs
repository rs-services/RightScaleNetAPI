using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class ChildAccount
    {
        public string name { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public List<Link> links { get; set; }

        
        #region ChildAccount.index methods

        public static List<ChildAccount> index()
        {
            return index(null);
        }

        public static List<ChildAccount> index(List<KeyValuePair<string, string>> filter)
        {

            List<string> validFilters = new List<string>() { "name" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement ChildAccount.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
