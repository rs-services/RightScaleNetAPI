using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// An InstanceType represents a basic hardware configuration for an Instance.
    /// Combining all possible configurations of hardware into a smaller, well-known set of options makes instances easier to manage, and allows better allocation efficiency into physical hosts
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeInstanceType.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceInstanceTypes.html
    /// </summary>
    public class InstanceType:Core.RightScaleObjectBase<InstanceType>
    {
        public string name { get; set; }
        public string resource_uid { get; set; }
        public string cpu_architecture { get; set; }
        public string local_disks { get; set; }
        public string memory { get; set; }
        public string local_disk_size { get; set; }
        public string cpu_count { get; set; }
        public string cpu_speed { get; set; }
        public string description { get; set; }
        
        #region InstanceType.ctor
        /// <summary>
        /// Default Constructor for InstanceType
        /// </summary>
        public InstanceType()
            : base()
        {
        }

        /// <summary>
        /// Constructor for InstanceType object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public InstanceType(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for InstanceType object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public InstanceType(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		
        #region InstanceType.index methods

        /// <summary>
        /// Lists instance types.
        /// </summary>
        /// <param name="cloudID">ID of the cloud to enumerate instance types for</param>
        /// <returns>Collection of InstanceTypes</returns>
        public static List<InstanceType> index(string cloudID)
        {
            return index(cloudID, null, null);
        }

        /// <summary>
        /// Lists instance types.
        /// </summary>
        /// <param name="cloudID">ID of the cloud to enumerate instance types for</param>
        /// <param name="filter">Collection of filters for limiting the return set</param>
        /// <returns>Collection of InstanceTypes</returns>
        public static List<InstanceType> index(string cloudID, List<KeyValuePair<string, string>> filter)
        {
            return index(cloudID, filter, null);
        }

        /// <summary>
        /// Lists instance types.
        /// </summary>
        /// <param name="cloudID">ID of the cloud to enumerate instance types for</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>Collection of InstanceTypes</returns>
        public static List<InstanceType> index(string cloudID, string view)
        {
            return index(cloudID, null, view);
        }

        /// <summary>
        /// Lists instance types.
        /// </summary>
        /// <param name="cloudID">ID of the cloud to enumerate instance types for</param>
        /// <param name="filter">Collection of filters for limiting the return set</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>Collection of InstanceTypes</returns>
        public static List<InstanceType> index(string cloudID, List<KeyValuePair<string, string>> filter, string view)
        {
            string getHref = string.Format("/api/clouds/{0}/instance_types", cloudID);

            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default" };
                Utility.CheckStringInput("view", validViews, view);
            }

            List<string> validFilters = new List<string>() { "cpu_architecture", "description", "name", "resource_uid" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            string queryString = string.Empty;
            if (filter != null && filter.Count > 0)
            {
                queryString += Utility.BuildGetQueryString(filter) + "&";
            }
            queryString += string.Format("view={0}", view);
            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserializeList(jsonString);
        }
        #endregion

        #region InstanceType.show methods

        /// <summary>
        /// Displays information about a single Instance type.
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the InstanceType can be found</param>
        /// <param name="instanceTypeID">ID of the specific InstanceType to be returned</param>
        /// <returns>Specific instance of InstanceType</returns>
        public static InstanceType show(string cloudID, string instanceTypeID)
        {
            return show(cloudID, instanceTypeID, null);
        }

        /// <summary>
        /// Displays information about a single Instance type.
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the InstanceType can be found</param>
        /// <param name="instanceTypeID">ID of the specific InstanceType to be returned</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>Specific instance of InstanceType</returns>
        public static InstanceType show(string cloudID, string instanceTypeID, string view)
        {
            string getHref = string.Format("/api/clouds/{0}/instance_types/{1}", cloudID, instanceTypeID);
            string queryString = string.Empty;

            if (!string.IsNullOrWhiteSpace(view))
            {
                List<string> validViews = new List<string>() { "default" };
                Utility.CheckStringInput("view", validViews, view);
                queryString += string.Format("view={0}", view);
            }   
            
            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserialize(jsonString);
        }

        #endregion

    }
}
