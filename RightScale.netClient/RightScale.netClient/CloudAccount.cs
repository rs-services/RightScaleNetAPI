using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class CloudAccount : Core.RightScaleObjectBase<CloudAccount>
    {
        //TODO: need to write this class



        #region CloudAccount.ctor
        /// <summary>
        /// Default Constructor for CloudAccount
        /// </summary>
        public CloudAccount()
            : base()
        {
        }

        /// <summary>
        /// Constructor for CloudAccount object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public CloudAccount(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for CloudAccount object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public CloudAccount(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		
        
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
