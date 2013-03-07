using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// An IpAddress provides an abstraction for IPv4 addresses bindable to Instance resources running in a Cloud.
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeIpAddress.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeIpAddress.html
    /// </summary>
    public class IPAddress:Core.RightScaleObjectBase<IPAddress>
    {
        public string address { get; set; }
        public string name { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

        #region IPAddress.ctor
        /// <summary>
        /// Default Constructor for IPAddress
        /// </summary>
        public IPAddress()
            : base()
        {
        }

        /// <summary>
        /// Constructor for IPAddress object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public IPAddress(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for IPAddress object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public IPAddress(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		
        #region IPAddress.index methods

        /// <summary>
        /// An IpAddress provides an abstraction for IPv4 addresses bindable to Instance resources running in a Cloud.
        /// </summary>
        /// <param name="cloudID">ID of the Cloud where IP addresses are to be retrieved from</param>
        /// <returns>Collection of IPAddress objects</returns>
        public static List<IPAddress> index(string cloudID)
        {
            return index(cloudID, new List<KeyValuePair<string, string>>());
        }

        /// <summary>
        /// An IpAddress provides an abstraction for IPv4 addresses bindable to Instance resources running in a Cloud.
        /// </summary>
        /// <param name="cloudID">ID of the Cloud where IP addresses are to be retrieved from</param>
        /// <param name="filter">Set of filters for querying IP Addresses</param>
        /// <returns>Collection of IPAddress objects</returns>
        public static List<IPAddress> index(string cloudID, Hashtable filter)
        {
            return index(cloudID, Utility.convertToKVP(filter));
        }

        /// <summary>
        /// An IpAddress provides an abstraction for IPv4 addresses bindable to Instance resources running in a Cloud.
        /// </summary>
        /// <param name="cloudID">ID of the Cloud where IP addresses are to be retrieved from</param>
        /// <param name="filter">Set of filters for querying IP Addresses</param>
        /// <returns>Collection of IPAddress objects</returns>
        public static List<IPAddress> index(string cloudID, List<KeyValuePair<string, string>> filter)
        {
            string getHref = string.Format("/get/clouds/{0}/ip_addresses", cloudID);

            List<string> validFilters = new List<string>() { "name" };
            Utility.CheckFilterInput("filter", validFilters, filter);
            string queryString = string.Empty;
            if (filter != null && filter.Count > 0)
            {
                queryString = Utility.BuildFilterString(filter);
            }
            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserializeList(jsonString);
        }
        #endregion

        #region IPAddress.show methods

        /// <summary>
        /// Show information about a single ip address.
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the IPAddress can be found</param>
        /// <param name="ipAddressID">ID of the IPAddress to be shown</param>
        /// <returns>IPAddress object</returns>
        public static IPAddress show(string cloudID, string ipAddressID)
        {
            string getHref = string.Format("/api/clouds/{0}/ip_addresses/{1}", cloudID, ipAddressID);

            string jsonString = Core.APIClient.Instance.Get(getHref);

            return deserialize(jsonString);
        }

        #endregion

        #region IPAddress.create methods

        /// <summary>
        /// Creates a new IpAddress with the given parameters.
        /// </summary>
        /// <param name="cloudID">ID of the cloud to create the IP address</param>
        /// <param name="name">The name of the IPAddress to be created</param>
        /// <returns>ID of the newly created IPAddress</returns>
        public static string create(string cloudID, string name)
        {
            string postHref = string.Format("/api/clouds/{0}/ip_addresses", cloudID);
            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            postParams.Add(new KeyValuePair<string, string>("ip_address[name]", name));
            List<string> returnList = Core.APIClient.Instance.Create(postHref, postParams, "location");
            return returnList.Last<string>().Split('/').Last<string>();
        }

        #endregion

        #region IPAddress.update methods

        /// <summary>
        /// Updates attributes of a given IpAddress.
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the given IP Address can be found</param>
        /// <param name="ipAddressID">ID of the IPAddress to be updated</param>
        /// <param name="name">New name for IPAddress object</param>
        /// <returns>True if successful, false if not</returns>
        public static bool update(string cloudID, string ipAddressID, string name)
        {
            string putHref = string.Format("/api/clouds/{0}/ip_addresses/{1}", cloudID, ipAddressID);
            List<KeyValuePair<string, string>> putParams = new List<KeyValuePair<string, string>>();
            putParams.Add(new KeyValuePair<string, string>("ip_address[name]", name));
            return Core.APIClient.Instance.Put(putHref, putParams);
        }

        #endregion

        #region IPAddress.destroy methods

        /// <summary>
        /// Deletes a given IpAddress.
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the given IP address can be found</param>
        /// <param name="ipAddressID">ID of the IPAddress to be updated</param>
        /// <returns>True if successful, false if not</returns>
        public static bool destroy(string cloudID, string ipAddressID)
        {
            string deleteHref = string.Format("/api/clouds/{0}/ip_addresses/{1}", cloudID, ipAddressID);
            return Core.APIClient.Instance.Delete(deleteHref);
        }

        #endregion
    }
}
