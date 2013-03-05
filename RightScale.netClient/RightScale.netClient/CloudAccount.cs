using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    class CloudAccount
    {
        //TODO: need to write this class

        
        #region CloudAccount.index methods

        public static List<CloudAccount> index()
        {
            return index(null);
        }

        public static List<CloudAccount> index(List<KeyValuePair<string, string>> filter)
        {
            List<string> validFilters = new List<string>() { "name" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement CloudAccount.index
            throw new NotImplementedException();
        }

        #endregion
		
    }
}
