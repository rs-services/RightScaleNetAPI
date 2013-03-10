using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// RightScale User object
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeUser.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceUsers.html
    /// </summary>
    public class User:Core.RightScaleObjectBase<User>
    {
        #region User Properties

        /// <summary>
        /// Company name of specified user
        /// </summary>
        public string company { get; set; }

        /// <summary>
        /// Timestamp indicating when user was created
        /// </summary>
        public string created_at { get; set; }

        /// <summary>
        /// Timestamp indicating when user was last updated
        /// </summary>
        public string updated_at { get; set; }

        /// <summary>
        /// Last name of the user specified
        /// </summary>
        public string last_name { get; set; }

        /// <summary>
        /// Phone number of the user specified
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// First name fo the user specified
        /// </summary>
        public string first_name { get; set; }

        /// <summary>
        /// Email of the user specified
        /// </summary>
        public string email { get; set; }

        #endregion

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

        public static List<User> index(List<Filter> filter)
        {
            List<string> validFilters = new List<string>() { "email", "first_name", "last_name" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement User.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
