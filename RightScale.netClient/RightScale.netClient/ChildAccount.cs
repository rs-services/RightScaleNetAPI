using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class ChildAccount : Core.RightScaleObjectBase<ChildAccount>
    {
        public string name { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

        #region ChildAccount.ctor
        /// <summary>
        /// Default Constructor for ChildAccount
        /// </summary>
        public ChildAccount()
            : base()
        {
        }

        /// <summary>
        /// Constructor for ChildAccount object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public ChildAccount(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for ChildAccount object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public ChildAccount(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		
        
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
