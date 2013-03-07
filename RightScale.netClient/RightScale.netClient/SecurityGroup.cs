using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class SecurityGroup:Core.RightScaleObjectBase<SecurityGroup>
    {
        public string name { get; set; }
        public string resource_uid { get; set; }

        #region SecurityGroup.ctor
        /// <summary>
        /// Default Constructor for SecurityGroup
        /// </summary>
        public SecurityGroup()
            : base()
        {
        }

        /// <summary>
        /// Constructor for SecurityGroup object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public SecurityGroup(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for SecurityGroup object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public SecurityGroup(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		

        #region object.index methods

        public static List<object> index()
        {
            return index(null, null);
        }

        public static List<object> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        public static List<object> index(string view)
        {
            return index(null, view);
        }

        public static List<object> index(List<KeyValuePair<string, string>> filter, string view)
        {
            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default", "tiny" };
                Utility.CheckStringInput("view", validViews, view);
            }

            List<string> validFilters = new List<string>() { "name", "resource_uid" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement object.index
            throw new NotImplementedException();
        }
        #endregion
    }
}
