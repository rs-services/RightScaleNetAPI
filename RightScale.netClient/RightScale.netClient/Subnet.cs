using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// A Subnet is a logical grouping of network devices. An Instance can have many Subnets.
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeSubnet.html
    /// Resources Reference: http://reference.rightscale.com/api1.5/resources/ResourceSubnets.html
    /// </summary>
    public class Subnet : Core.RightScaleObjectBase<Subnet>
    {

        #region Subnet Properties

        /// <summary>
        /// Name of the subnet
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Visibility parameter for the given subnet
        /// </summary>
        public string visibility { get; set; }

        /// <summary>
        /// Assigned resource unique identifier for the given subnet
        /// </summary>
        public string resource_uid { get; set; }

        /// <summary>
        /// Description for the given subnet
        /// </summary>
        public string description { get; set; }

        #endregion

        #region Subnet Relationships

        /// <summary>
        /// Datacenter for this instance
        /// </summary>
        public DataCenter datacenter
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("datacenter"));
                return DataCenter.deserialize(jsonString);
            }
        }

        #endregion

        #region Subnet.index methods

        /// <summary>
        /// Lists subnets of a given cloud.
        /// </summary>
        /// <param name="cloudID">ID of the cloud where subnets are to be queried from</param>
        /// <returns>List of populated Subnet objects</returns>
        public static List<Subnet> index(string cloudID)
        {
            return index(cloudID, null);
        }

        /// <summary>
        /// Lists subnets of a given cloud.  
        /// </summary>
        /// <param name="cloudID">ID of the cloud where subnets are to be queried from</param>
        /// <param name="filter">Set of filters to limit the number of subnets returned</param>
        /// <returns>List of populated Subnet objects based on input filters</returns>
        public static List<Subnet> index(string cloudID, List<Filter> filter)
        {
            Utility.CheckStringHasValue(cloudID);
            string getHref = string.Format(APIHrefs.Subnet, cloudID);
            return indexGet(filter, getHref);
        }

        /// <summary>
        /// Lists subnets of a given cloud for a specific Instance.
        /// </summary>
        /// <param name="cloudID">ID of the cloud where subnets are to be queried from</param>
        /// <param name="instanceID">ID of the Instance where the subnet(s) is attached</param>
        /// <returns>List of populated Subnet objects based on input filters</returns>
        public static List<Subnet> index_Instance(string cloudID, string instanceID)
        {
            return index_Instance(cloudID, instanceID, null);
        }

        /// <summary>
        /// Lists subnets of a given cloud for a specific Instance.
        /// </summary>
        /// <param name="cloudID">ID of the cloud where subnets are to be queried from</param>
        /// <param name="instanceID">ID of the Instance where the subnet(s) is attached</param>
        /// <param name="filter">Set of filters to limit the number of subnets returned</param>
        /// <returns>List of populated Subnet objects based on input filters</returns>
        public static List<Subnet> index_Instance(string cloudID, string instanceID, List<Filter> filter)
        {
            Utility.CheckStringHasValue(cloudID);
            Utility.CheckStringHasValue(instanceID);
            string getHref = string.Format(APIHrefs.InstanceSubnetByID, cloudID, instanceID);
            return indexGet(filter, getHref);
        }

        /// <summary>
        /// Internal method to manage index get calls
        /// </summary>
        /// <param name="filter">Set of filters to limit the number of subnets returned</param>
        /// <param name="getHref">API href for rest call</param>
        /// <returns>list of subnets to return</returns>
        private static List<Subnet> indexGet(List<Filter> filter, string getHref)
        {

            List<string> validFilters = new List<string>() { "datacenter_href", "name", "resource_uid", "visibility" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            string queryString = string.Empty;

            if (filter != null && filter.Count > 0)
            {
                foreach (Filter f in filter)
                {
                    queryString += f.ToString() + "&";
                }
            }

            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserializeList(jsonString);
        }

        #endregion

        #region Subnet.show methods

        /// <summary>
        /// Shows attributes of a signle subnet
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the subnet is located</param>
        /// <param name="subnetID">ID of the subnet to be displayed</param>
        /// <returns>Instance of a populated Subnet object</returns>
        public static Subnet show(string cloudID, string subnetID)
        {
            Utility.CheckStringHasValue(cloudID);
            Utility.CheckStringHasValue(subnetID);
            string getHref = string.Format(APIHrefs.SubnetByID, cloudID, subnetID);
            string jsonString = Core.APIClient.Instance.Get(getHref);
            return deserialize(jsonString);
        }

        /// <summary>
        /// Shows the attributes of a specific subnet for a given instance
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the subnet is located</param>
        /// <param name="instanceID">ID of the instance where the subnet is attached to</param>
        /// <param name="subnetID">ID of the subnet to be displayed</param>
        /// <returns>Instance of a populated Subnet object</returns>
        public static Subnet show_Instance(string cloudID, string instanceID, string subnetID)
        {
            Utility.CheckStringHasValue(cloudID);
            Utility.CheckStringHasValue(instanceID);
            Utility.CheckStringHasValue(subnetID);
            string getHref = string.Format(APIHrefs.InstanceSubnetByID, cloudID, instanceID, subnetID);
            string jsonString = Core.APIClient.Instance.Get(getHref);
            return deserialize(jsonString);
        }

        #endregion

    }
}
