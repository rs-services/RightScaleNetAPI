using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Permission:Core.RightScaleObjectBase<Permission>
    {
        public List<Action> actions { get; set; }
        public string created_at { get; set; }
        public string role_title { get; set; }
        public List<Link> links { get; set; }


        #region Permission.ctor
        /// <summary>
        /// Default Constructor for Permission
        /// </summary>
        public Permission()
            : base()
        {
        }

        /// <summary>
        /// Constructor for Permission object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public Permission(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for Permission object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public Permission(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		
        
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
