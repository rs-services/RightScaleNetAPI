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

        public string InstanceID
        {
            get
            {
                return getLinkIDValue("self");
            }
        }
        
        public string CloudID
        {
            get
            {
                return getLinkIDValue("cloud");
            }
        }

        public string ServerTemplateID
        {
            get
            {
                return getLinkIDValue("server_template");
            }
        }

        public string MultiCloudImageID
        {
            get
            {
                return getLinkIDValue("multi_cloud_image");
            }
        }

        public string ImageID
        {
            get
            {
                return getLinkIDValue("image");
            }
        }

        public string RamdiskImageID
        {
            get
            {
                return getLinkIDValue("ramdisk_image");
            }
        }

        public string KernelImageID
        {
            get
            {
                return getLinkIDValue("kernel_image");
            }
        }

        public string InstanceTypeID
        {
            get
            {
                return getLinkIDValue("instance_type");
            }
        }

        public string SshKeyID
        {
            get
            {
                return getLinkIDValue("ssh_key");
            }
        }

        public string DataCenterID
        {
            get
            {
                return getLinkIDValue("datacenter");
            }
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

        #region Instance.show

        /// <summary>
        /// Shows attributes of a single instance.
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the instance can be found</param>
        /// <param name="instanceID">ID of the instance being queried</param>
        /// <returns>Instance object as specified</returns>
        public Instance show(string cloudID, string instanceID)
        {
            return show(cloudID, instanceID, "full");
        }

        /// <summary>
        /// Shows attributes of a single instance.
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the instance can be found</param>
        /// <param name="instanceID">ID of the instance being queried</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>Instance object as specified</returns>
        public Instance show(string cloudID, string instanceID, string view)
        {
            string getHref = string.Format("/api/clouds/{0}/instances/{1}", cloudID, instanceID);
            string queryString = string.Empty;
            if (!string.IsNullOrWhiteSpace(view))
            {
                List<string> validViews = new List<string>() { "default", "extended", "full", "full_inputs_2_0" };
                Utility.CheckStringInput("view", validViews, view);
                queryString += string.Format("view={0}&", view);
            }
            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserialize(jsonString);
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
        public static List<Instance> index_serverArray(string serverArrayID, List<Filter> filter)
        {
            return index_serverArray(serverArrayID, filter, null);
        }

        /// <summary>
        /// Lists instances of a given cloud.
        /// </summary>
        /// <param name="cloudID">ID of Cloud to query for instances</param>
        /// <param name="filter">filters results of query to RightScale API</param>
        /// <returns>collection of Instances within the given Cloud</returns>
        public static List<Instance> index(string cloudID, List<Filter> filter)
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
        public static List<Instance> index_serverArray(string serverArrayID, List<Filter> filter, string view)
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
        public static List<Instance> index(string cloudID, List<Filter> filter, string view)
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
        private static List<Instance> indexGet(string getHref, List<Filter> filter, string view)
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

            queryString += Utility.BuildFilterString(filter);
            if (!string.IsNullOrWhiteSpace(view))
            {
                queryString += string.Format("view={0}", view);
            }

            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserializeList(jsonString);
        }
        #endregion

        #region Instance.update

        #endregion

        #region Instance.launch

        #endregion

        #region Instance.multi_run_executable

        #endregion

        #region Instance.multi_terminate

        #endregion

        #region Instance.run_executable

        #endregion

        #region Instance.set_custom_lodgement

        #endregion

        #region Instance.start

        /// <summary>
        /// Starts an instance that has been stopped, resuming it to its previously saved volume state.
        /// After an instance is started, the reference to your instance will have a different id.
        /// The new id can be found by performing an index query with the appropriate filters on the Instances resource, performing a show action on the Server resource for Server Instances, or performing a current_instances action on the ServerArray resource for ServerArray Instances.
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the instance is located</param>
        /// <param name="instanceID">ID of the instance to be stopped</param>
        /// <returns>True if success, false if failure</returns>
        public static bool start(string cloudID, string instanceID)
        {
            string postUrl = string.Format("/api/clouds/{0}/instances/{1}/start", cloudID, instanceID);
            return Core.APIClient.Instance.Post(postUrl);
        }

        #endregion

        #region Instance.stop

        /// <summary>
        /// Stores the instance's current volume state to resume later using the 'start' action.
        /// After an instance is stopped, the reference to your instance will have a different id.  
        /// The new id can be found by performing an index query with the appropriate filters on the Instances resource, performing a show action on the Server resource for Server Instances, or performing a current_instances action on the ServerArray resource for ServerArray Instances.
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the instance is located</param>
        /// <param name="instanceID">ID of the instance to be stopped</param>
        /// <returns>True if success, false if failure</returns>
        public static bool stop(string cloudID, string instanceID)
        {
            string postHref = string.Format("/api/clouds/{0}/instances/{1}/stop", cloudID, instanceID);
            return Core.APIClient.Instance.Post(postHref);
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
