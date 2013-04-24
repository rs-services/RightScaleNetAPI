using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// Represents a permission object within the RightScale system
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypePermission.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourcePermissions.html
    /// </summary>
    public class Permission:Core.RightScaleObjectBase<Permission>
    {
        #region Permission Properties

        /// <summary>
        /// Timestamp representing when this permission object was created
        /// </summary>
        public string created_at { get; set; }

        /// <summary>
        /// Title/role of this permission object
        /// </summary>
        public string role_title { get; set; }

        #endregion

        #region Permission Relationships

        /// <summary>
        /// Account associated with this Permission object
        /// </summary>
        public Account account
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("account"));
                return Account.deserialize(jsonString);
            }
        }

        /// <summary>
        /// User associated with this Permission object
        /// </summary>
        public User user
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("user"));
                return User.deserialize(jsonString);
            }
        }

        #endregion

        #region Permission.ctor
        /// <summary>
        /// Default Constructor for Permission
        /// </summary>
        public Permission()
            : base()
        {
        }

        #endregion
        
        #region Permission.index methods

        /// <summary>
        /// List all permissions for all users of the current account
        /// </summary>
        /// <returns>List of Permission objects</returns>
        public static List<Permission> index()
        {
            return index(null);
        }

        /// <summary>
        /// List all permissions for all users of the current account
        /// </summary>
        /// <param name="filter">List of filters for query to RightScale API</param>
        /// <returns>List of Permission objects</returns>
        public static List<Permission> index(List<Filter> filter)
        {     
            List<string> validFilters = new List<string>() { "user_href" };
            Utility.CheckFilterInput("filter", validFilters, filter);
            string queryString = string.Empty;

            if (filter != null && filter.Count > 0)
            {
                foreach (Filter f in filter)
                {
                    queryString += f.ToString() + "&";
                }
            }

            queryString = queryString.TrimEnd('&');

            string jsonString = Core.APIClient.Instance.Get(APIHrefs.Permission, queryString);
            return deserializeList(jsonString);
        }
        #endregion
		
    }
}
