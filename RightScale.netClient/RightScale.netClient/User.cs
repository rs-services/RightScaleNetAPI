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
