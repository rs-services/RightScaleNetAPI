using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
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

        public static List<InstanceType> index(string cloudID)
        {
            return index(cloudID, null, null);
        }

        public static List<InstanceType> index(string cloudID, List<KeyValuePair<string, string>> filter)
        {
            return index(cloudID, filter, null);
        }

        public static List<InstanceType> index(string cloudID, string view)
        {
            return index(cloudID, null, view);
        }

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
		
    }
}
