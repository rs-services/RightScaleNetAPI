using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class IPAddress:Core.RightScaleObjectBase<IPAddress>
    {
        public string address { get; set; }
        public string name { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }



        #region IPAddress.ctor
        /// <summary>
        /// Default Constructor for IPAddress
        /// </summary>
        public IPAddress()
            : base()
        {
        }

        /// <summary>
        /// Constructor for IPAddress object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public IPAddress(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for IPAddress object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public IPAddress(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		
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
