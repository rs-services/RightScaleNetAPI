using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class User:Core.RightScaleObjectBase<User>
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

        #region User.ctor
        /// <summary>
        /// Default Constructor for User
        /// </summary>
        public User()
            : base()
        {
        }

        /// <summary>
        /// Constructor for User object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public User(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for User object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public User(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		


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
