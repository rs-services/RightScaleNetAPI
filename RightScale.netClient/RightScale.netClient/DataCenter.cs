using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace RightScale.netClient
{
    /// <summary>
    /// Datacenters represent isolated facilities within a cloud. The level and type of isolation is cloud dependent. While Datacenters in large public clouds might correspond to different physical buildings, with different power, internet links...etc., Datacenters within the context of a private cloud might simply correspond to having different network providers.
    /// Spreading servers across distinct Datacenters helps minimize outtages.
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeDatacenter.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceDatacenters.html
    /// </summary>
    public class DataCenter : Core.RightScaleObjectBase<DataCenter>
    {
        public string name { get; set; }
        public string resource_uid { get; set; }
        public string description { get; set; }

        #region DataCenter.ctor
        /// <summary>
        /// Default Constructor for DataCenter
        /// </summary>
        public DataCenter()
            : base()
        {
        }

        /// <summary>
        /// Constructor for DataCenter object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public DataCenter(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for DataCenter object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public DataCenter(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion

        #region DataCenter.show() methods

        public static DataCenter show(string cloudID, string dataCenterID)
        {
            Utility.CheckStringIsNumeric(cloudID);
            Utility.CheckStringIsNumeric(dataCenterID);

            string getURL = string.Format("/api/clouds/{0}/datacenters/{1}", cloudID, dataCenterID);

            string jsonString = Core.APIClient.Instance.Get(getURL);

            return deserialize(jsonString);
        }

        #endregion

        #region DataCenter.index methods

        /// <summary>
        /// Lists all Datacenters for a particular cloud.
        /// </summary>
        /// <param name="cloudID">ID of cloud to enumerate DataCenters for</param>
        /// <returns>Collection of DataCenter objects</returns>
        public static List<DataCenter> index(string cloudID)
        {
            return index(cloudID, null, null);
        }

        /// <summary>
        /// Lists all Datacenters for a particular cloud.
        /// </summary>
        /// <param name="cloudID">ID of cloud to enumerate DataCenters for</param>
        /// <param name="filter">Filter set to limit return data</param>
        /// <returns>Collection of DataCenter objects</returns>
        public static List<DataCenter> index(string cloudID, List<KeyValuePair<string, string>> filter)
        {
            return index(cloudID, filter, null);
        }

        /// <summary>
        /// Lists all Datacenters for a particular cloud.
        /// </summary>
        /// <param name="cloudID">ID of cloud to enumerate DataCenters for</param>
        /// <param name="filter">Filter set to limit return data</param>
        /// <returns>Collection of DataCenter objects</returns>
        public static List<DataCenter> index(string cloudID, Hashtable filter)
        {
            return index(cloudID, Utility.convertToKVP(filter), null);
        }

        /// <summary>
        /// Lists all Datacenters for a particular cloud.
        /// </summary>
        /// <param name="cloudID">ID of cloud to enumerate DataCenters for</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include.</param>
        /// <returns>Collection of DataCenter objects</returns>
        public static List<DataCenter> index(string cloudID, string view)
        {
            return index(cloudID, null, view);
        }

        /// <summary>
        /// Lists all Datacenters for a particular cloud.
        /// </summary>
        /// <param name="cloudID">ID of cloud to enumerate DataCenters for</param>
        /// <param name="filter">Filter set to limit return data</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include.</param>
        /// <returns>Collection of DataCenter objects</returns>
        public static List<DataCenter> index(string cloudID, List<KeyValuePair<string, string>> filter, string view)
        {
            string getHref = string.Format("/api/clouds/{0}/datacenters", cloudID);

            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default" };
                Utility.CheckStringInput("view", validViews, view);
            }

            List<string> validFilters = new List<string>() { "name", "resource_uid" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            string queryString = string.Empty;

            if (filter != null && filter.Count > 0)
            {
                foreach(KeyValuePair<string, string> kvp in filter)
                {
                    queryString += string.Format("{0}={1}&", kvp.Key, kvp.Value);
                }
            }
            if (!string.IsNullOrWhiteSpace(view))
            {
                queryString += string.Format("view={0}", view);
            }
            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserializeList(jsonString);
        }
        #endregion
		
    }
}
