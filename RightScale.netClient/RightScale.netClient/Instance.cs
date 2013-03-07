using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// Instances represent an entity that is runnable in the cloud.
    /// An instance of type "next" is a container of information that expresses how to configure a future instance when we decide to launch or start it. A "next" instance generally only exists in the RightScale realm, and usually doesn't have any corresponding representation existing in the cloud. However, if an instance is not of type "next", it will generally represent an existing running (or provisioned) virtual machine existing in the cloud.
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeInstance.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeInstance.html
    /// </summary>
    public class Instance : Core.RightScaleObjectBase<Instance>
    {
        public string name { get; set; }
        public string resource_uid { get; set; }
        public string created_at { get; set; }
        public List<Input> inputs { get; set; }
        public List<string> private_dns_names { get; set; }
        public string monitoring_id { get; set; }
        public string os_platform { get; set; }
        public string updated_at { get; set; }
        public List<string> public_ip_addresses { get; set; }
        public string monitoring_server { get; set; }
        public string terminated_at { get; set; }
        public string description { get; set; }
        public string pricing_type { get; set; }
        public List<string> private_ip_addresses { get; set; }
        public string user_data { get; set; }
        public string state { get; set; }
        public List<SecurityGroup> security_groups { get; set; }
        public List<string> public_dns_names { get; set; }

        #region Get link ID public instance methods

        /// <summary>
        /// Get instance id from links collection
        /// </summary>
        /// <returns>Instance ID</returns>
        public string getInstanceID()
        {
            return getLinkIDValue("self");
        }

        /// <summary>
        /// Get Cloud ID from links collection
        /// </summary>
        /// <returns>Cloud ID</returns>
        public string getCloudID()
        {
            return getLinkIDValue("cloud");
        }

        /// <summary>
        /// Get ServerTemplate ID from links collection
        /// </summary>
        /// <returns></returns>
        public string getServerTemplateID()
        {
            return getLinkIDValue("server_template");
        }

        /// <summary>
        /// Get MultiCloudImage ID from links collection
        /// </summary>
        /// <returns>MultiCloudImage ID</returns>
        public string getMultiCloudImageID()
        {
            return getLinkIDValue("multi_cloud_image");
        }

        /// <summary>
        /// Get Image ID from links collection
        /// </summary>
        /// <returns>Image ID</returns>
        private string getImageID()
        {
            return getLinkIDValue("image");
        }

        /// <summary>
        /// Get RamDisk Image ID from links Collection
        /// </summary>
        /// <returns>RamDisk Image ID</returns>
        private string getRamDiskImageID()
        {
            return getLinkIDValue("ramdisk_image");
        }

        /// <summary>
        /// Get Kernel Image ID from links collection
        /// </summary>
        /// <returns>Kernel Image ID</returns>
        private string getKernelImageID()
        {
            return getLinkIDValue("kernel_image");
        }

        /// <summary>
        /// Get InstanceType ID from links collection
        /// </summary>
        /// <returns>InstanceType ID</returns>
        private string getInstanceTypeID()
        {
            return getLinkIDValue("instance_type");
        }

        /// <summary>
        /// Get Ssh Key ID from links collection
        /// </summary>
        /// <returns>SshKey ID</returns>
        private string getSshKeyID()
        {
            return getLinkIDValue("ssh_key");
        }

        /// <summary>
        /// Get Datacenter ID from links collection
        /// </summary>
        /// <returns>Datacenter ID</returns>
        private string getDatacenterID()
        {
            return getLinkIDValue("datacenter");
        }

        #endregion

        #region Instance.ctor
        /// <summary>
        /// Default Constructor for Instance
        /// </summary>
        public Instance()
            : base()
        {
        }

        /// <summary>
        /// Constructor for Instance object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public Instance(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for Instance object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public Instance(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		
        
        #region Instance.index methods

        /// <summary>
        /// Lists instances of a given ServerArray.
        /// </summary>
        /// <param name="serverArrayID">ID of ServerArray to query</param>
        /// <returns>collection of Instances within the given ServerArray</returns>
        public static List<Instance> index_serverArray(string serverArrayID)
        {
            return index_serverArray(serverArrayID, null, null);
        }

        /// <summary>
        /// Lists instances of a given cloud.
        /// </summary>
        /// <param name="cloudID">ID of Cloud to query for instances</param>
        /// <returns>collection of Instances within the given Cloud</returns>
        public static List<Instance> index(string cloudID)
        {
            return index(cloudID, null, null);
        }

        /// <summary>
        /// List instances of a given ServerArray
        /// </summary>
        /// <param name="serverArrayID">ID of ServerArray to query</param>
        /// <param name="filter">filters results of query to RightScale API</param>
        /// <returns>collection of Instances within the given ServerArray</returns>
        public static List<Instance> index_serverArray(string serverArrayID, List<KeyValuePair<string, string>> filter)
        {
            return index_serverArray(serverArrayID, filter, null);
        }

        /// <summary>
        /// Lists instances of a given cloud.
        /// </summary>
        /// <param name="cloudID">ID of Cloud to query for instances</param>
        /// <param name="filter">filters results of query to RightScale API</param>
        /// <returns>collection of Instances within the given Cloud</returns>
        public static List<Instance> index(string cloudID, List<KeyValuePair<string, string>> filter)
        {
            return index(cloudID, filter, null);
        }


        /// <summary>
        /// List instances of a given ServerArray
        /// </summary>
        /// <param name="serverArrayID">ID of ServerArray to query</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>collection of Instances within the given ServerArray</returns>
        public static List<Instance> index_serverArray(string serverArrayID, string view)
        {
            return index_serverArray(serverArrayID, null, view);
        }

        /// <summary>
        /// Lists instances of a given cloud.
        /// </summary>
        /// <param name="cloudID">ID of Cloud to query for instances</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>collection of Instances within the given Cloud</returns>
        public static List<Instance> index(string cloudID, string view)
        {
            return index(cloudID, null, view);
        }

        /// <summary>
        /// Lists instances of a given ServerArray.
        /// </summary>
        /// <param name="serverArrayID">ID of ServerArray to query</param>
        /// <param name="filter">filters results of query to RightScale API</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns></returns>
        public static List<Instance> index_serverArray(string serverArrayID, List<KeyValuePair<string, string>> filter, string view)
        {
            string getHref = string.Format("/api/server_arrays/{0}/current_instances", serverArrayID);
            return indexGet(getHref, filter, view);
        }

        /// <summary>
        /// Lists instances of a given cloud.
        /// </summary>
        /// <param name="cloudID">ID of Cloud to query for instances</param>
        /// <param name="filter">filters results of query to RightScale API</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>collection of Instances within the given Cloud</returns>
        public static List<Instance> index(string cloudID, List<KeyValuePair<string, string>> filter, string view)
        {
            string getHref = string.Format("/api/clouds/{0}/instances", cloudID);
            return indexGet(getHref, filter, view);
        }

        /// <summary>
        /// private static method encapsulates logic for all Instance.index() calls
        /// </summary>
        /// <param name="getHref">API Href to use when accessing RSAPI</param>
        /// <param name="filter">filters results of query to RightScale API</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns></returns>
        private static List<Instance> indexGet(string getHref, List<KeyValuePair<string, string>> filter, string view)
        {
            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default", "extended", "full", "full_inputs_2_0" };
                Utility.CheckStringInput("view", validViews, view);
            }

            List<string> validFilters = new List<string>() { "datacenter_href", "deployment_href", "name", "os_platform", "parent_href", "private_dns_name", "private_ip_address", "public_dns_name", "public_ip_address", "resource_uid", "server_template_href", "state" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            string queryString = string.Empty;
            if (filter != null && filter.Count > 0)
            {
                foreach (KeyValuePair<string, string> kvp in filter)
                {
                    queryString += string.Format("{0}={1}&", kvp.Key, kvp.Value);
                }
            }
            
            queryString += string.Format("view={0}", view);
            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserializeList(jsonString);
        }
        #endregion

        #region Instance.terminate

        /// <summary>
        /// Terminates a running instance.
        /// Note that this action can succeed only if the instance is running. One cannot terminate instances of type "next".
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the Instance is running</param>
        /// <param name="instanceID">Instance ID to be terminated</param>
        /// <returns></returns>
        public bool terminate(string cloudID, string instanceID)
        {
            string postHref = string.Format("/api/clouds/{0}/instances/{1}/terminate", cloudID, instanceID);
            return Core.APIClient.Instance.Post(postHref);
        }

        #endregion

    }
}
