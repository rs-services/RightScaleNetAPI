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

        #region Permission.show methods

        /// <summary>
        /// Show information about a single permission
        /// </summary>
        /// <param name="permissionID">ID of the permission to be returned</param>
        /// <returns>Populated instance of Permission object</returns>
        public static Permission show(string permissionID)
        {
            string getHref = string.Format(APIHrefs.PermissionByID, permissionID);
            string jsonString = Core.APIClient.Instance.Get(getHref);
            return deserialize(jsonString);
        }

        #endregion

        #region Permission.create methods

        /// <summary>
        /// Create a permission, thereby granting some user a particular role with respect to the current account.
        /// The 'observer' role has a special status; it must be granted before a user is eligible for any other permission in a given account.
        /// When provisioning users, always create the observer permission FIRST; creating any other permission before it will result in an error.
        /// For more information about the roles available and the privileges they confer, please refer to the following page of the RightScale support portal: http://support.rightscale.com/15-References/Lists/ListofUser_Roles
        /// </summary>
        /// <param name="roleTitle">Title of the role to be granted to the specified user</param>
        /// <param name="userID">ID of the User to be granted the role specifieed</param>
        /// <returns>ID of the newly created permission</returns>
        public static string create(string roleTitle, string userID)
        {
            string userHref = string.Format(APIHrefs.UserByID, userID);
            Utility.CheckStringHasValue(roleTitle);
            Utility.CheckStringHasValue(userID);
            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            Utility.addParameter(roleTitle, "permission[role_title]", postParams);
            Utility.addParameter(userHref, "permission[user_href]", postParams);
            return Core.APIClient.Instance.Post(APIHrefs.Permission, postParams, "location").Last<string>().Split('/').Last<string>();
        }

        #endregion

        #region Permission.destroy methods

        /// <summary>
        /// Destroy a permission, thereby revoking a user's role with respect to the current account.   
        /// The 'observer' role has a special status; it cannot be revoked if a user has any other roles, because other roles become useless without being able to read data pertaining to the account.
        /// When deprovisioning user, always destroy the observer permission LAST; destroying it while the user has other permissions will result in an error.
        /// </summary>
        /// <param name="permissionID">ID of the Permission object to destroy</param>
        /// <returns>true if destroyed, false if not</returns>
        public static bool destroy(string permissionID)
        {
            string destroyHref = string.Format(APIHrefs.PermissionByID, permissionID);
            return Core.APIClient.Instance.Delete(destroyHref);
        }

        #endregion
    }
}
