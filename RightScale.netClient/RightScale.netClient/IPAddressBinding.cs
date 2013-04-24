using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class IPAddressBinding:Core.RightScaleObjectBase<IPAddressBinding>
    {
        public int private_port { get; set; }
        public string created_at { get; set; }
        public int public_port { get; set; }
        public string protocol { get; set; }
        public bool recurring { get; set; }

		#region IPAddressBinding.ctor
		/// <summary>
        /// Default Constructor for IPAddressBinding
        /// </summary>
		public IPAddressBinding():base()
        {
        }
		
		#endregion

        #region IPAddressBinding Relationships

        /// <summary>
        /// Instance for this IPAddressBinding
        /// </summary>
        public Instance instance
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("instance"));
                return Instance.deserialize(jsonString);
            }
        }

        /// <summary>
        /// IPAddress for this IPAddressBinding
        /// </summary>
        public IPAddress ipAddress
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("ip_address"));
                return IPAddress.deserialize(jsonString);
            }
        }

        #endregion

        #region IPAddressBinding.index methods

        /// <summary>
        /// Lists the ip address bindings available to this account
        /// </summary>
        /// <param name="cloudID">ID of the cloud to look for IPAddressBinding objects</param>
        /// <returns>list of IPAddressBinding objects</returns>
        public static List<IPAddressBinding> index(string cloudID)
        {
            return index(cloudID, new List<Filter>());
        }

        /// <summary>
        /// Lists the ip address bindings available to this account
        /// </summary>
        /// <param name="cloudID">ID of the cloud to look for IPAddressBinding objects</param>
        /// <param name="filter">Set of filters for getting specific IPAddressBinding objects</param>
        /// <returns>list of IPAddressBinding objects</returns>
        public static List<IPAddressBinding> index(string cloudID, List<Filter> filter)
        {
            string getHref = string.Format(@"/api/clouds/{0}/ip_address_bindings", cloudID);
            return indexGet(filter, getHref);
        }

        /// <summary>
        /// Lists the ip address bindings available to this account
        /// </summary>
        /// <param name="cloudID">ID of the cloud to look for IPAddressBinding objects</param>
        /// <param name="ipAddressID">ID of the IPAddress to find IPAddressBindings for</param>
        /// <returns>list of IPAddressBinding objects</returns>
        public static List<IPAddressBinding> index(string cloudID, string ipAddressID)
        {
            return index(cloudID, ipAddressID, null);
        }

        /// <summary>
        /// Lists the ip address bindings available to this account
        /// </summary>
        /// <param name="cloudID">ID of the cloud to look for IPAddressBinding objects</param>
        /// <param name="ipAddressID">ID of the IPAddress to find IPAddressBindings for</param>
        /// <param name="filter">Set of filters for getting specific IPAddressBinding objects</param>
        /// <returns>list of IPAddressBinding objects</returns>
        public static List<IPAddressBinding> index(string cloudID, string ipAddressID, List<Filter> filter)
        {
            string getHref = string.Format(@"/api/clouds/{0}/ip_addresses/{1}/ip_address_bindings", cloudID, ipAddressID);
            return indexGet(filter, getHref);
        }

        /// <summary>
        /// internal method to handle GET call to RightScale API for IPAddressBindings
        /// </summary>
        /// <param name="filter">Collection of filters</param>
        /// <param name="getHref">HREF for api GET call</param>
        /// <returns>list of IPAddressBinding objects</returns>
        private static List<IPAddressBinding> indexGet(List<Filter> filter, string getHref)
        {

            string queryString = string.Empty;

            if (filter != null && filter.Count > 0)
            {
                foreach (Filter f in filter)
                {
                    queryString += f.ToString() + "&";
                }
            }

            queryString = queryString.TrimEnd('&');

            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserializeList(jsonString);
        }
        #endregion

        #region IPAddressBinding.show methods

        /// <summary>
        /// Show information about a single IP Address Binding
        /// </summary>
        /// <param name="cloudID">ID of the Cloud where the IPAddressBinding can be found</param>
        /// <param name="ipAddressBindingID">ID of IPAddressBinding to return</param>
        /// <returns>populated instance of IPAddressBinding object</returns>
        public static IPAddressBinding show(string cloudID, string ipAddressBindingID)
        {
            string getHref = string.Format(@"/api/clouds/{0}/ip_addresses_bindings/{1}", cloudID, ipAddressBindingID);
            return showGet(getHref);
        }

        /// <summary>
        /// Show information about a single IP Address Binding
        /// </summary>
        /// <param name="cloudID">ID of the Cloud where the IPAddressBinding can be found</param>
        /// <param name="ipAddressBindingID">ID of IPAddressBinding to return</param>
        /// <param name="ipAddressID">ID of IPAddress belonging to IPAddressBinding</param>
        /// <returns>populated instance of IPAddressBinding object</returns>
        public static IPAddressBinding show(string cloudID, string ipAddressBindingID, string ipAddressID)
        {
            string getHref = string.Format(@"/api/clouds/{0}/ip_addresses/{1}/ip_address_bindings/{2}", cloudID, ipAddressID, ipAddressBindingID);
            return showGet(getHref);
        }

        /// <summary>
        /// Internal method to manage api GET call to retrieve IPAddressBinding 
        /// </summary>
        /// <param name="getHref">HREF for GET call to RS API</param>
        /// <returns>populated instance of IPAddressBinding object</returns>
        private static IPAddressBinding showGet(string getHref)
        {
            string jsonString = Core.APIClient.Instance.Get(getHref);
            return deserialize(jsonString);
        }

        #endregion
    }
}
