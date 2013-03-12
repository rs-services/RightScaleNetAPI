using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
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
        #region Instance properties

        /// <summary>
        /// Friendly name of this instance
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Rightscale UID for this instance
        /// </summary>
        public string resource_uid { get; set; }

        /// <summary>
        /// Timestamp representing when this instance was created
        /// </summary>
        public string created_at { get; set; }

        /// <summary>
        /// Collection of inputs for this instance
        /// </summary>
        public List<Input> inputs { get; set; }

        /// <summary>
        /// collection of private DNS names for this instance
        /// </summary>
        public List<string> private_dns_names { get; set; }

        /// <summary>
        /// Monitoring ID for this instance
        /// </summary>
        public string monitoring_id { get; set; }

        /// <summary>
        /// OS platform for this instance
        /// </summary>
        public string os_platform { get; set; }

        /// <summary>
        /// Timestamp representing when this instance was last updated
        /// </summary>
        public string updated_at { get; set; }

        /// <summary>
        /// Collection of public IP addresses assigned to this instance
        /// </summary>
        public List<string> public_ip_addresses { get; set; }

        /// <summary>
        /// Monitoring server for this instance
        /// </summary>
        public string monitoring_server { get; set; }

        /// <summary>
        /// Timestamp representing when this server was terminated
        /// </summary>
        public string terminated_at { get; set; }

        /// <summary>
        /// Detailed description for this instance
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Pricing model/type for this instance
        /// </summary>
        public string pricing_type { get; set; }

        /// <summary>
        /// Collection of private ip addresses assigned to this instance
        /// </summary>
        public List<string> private_ip_addresses { get; set; }

        /// <summary>
        /// User Data specified when this instance was launched
        /// </summary>
        public string user_data { get; set; }

        /// <summary>
        /// Current state of this instance
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// Collection of security groups for this instance
        /// </summary>
        public List<SecurityGroup> security_groups { get; set; }

        /// <summary>
        /// Collection of public dns names for this instance
        /// </summary>
        public List<string> public_dns_names { get; set; }
        
        #endregion

        #region Get link ID public instance methods

        public DataCenter datacenter
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkIDValue("datacenter"));
                return DataCenter.deserialize(jsonString);
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
        public static Instance show(string cloudID, string instanceID)
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
        public static Instance show(string cloudID, string instanceID, string view)
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

        #region Instance.launch

        /// <summary>
        /// Launches an instance using the parameters that this instance has been configured with.
        /// Note that this action can only be performed in "next" instances, and not on instances that are already running.
        /// </summary>
        /// <param name="cloudid">ID of the cloud where the instance is configured be launched</param>
        /// <param name="instanceid">ID of the instance to be launched</param>
        /// <returns>true if successful, false if not</returns>
        public static string launch(string cloudid, string instanceid)
        {
            return launch(cloudid, instanceid, new List<KeyValuePair<string, string>>());
        }

        /// <summary>
        /// Launches an instance using the parameters that this instance has been configured with.
        /// Note that this action can only be performed in "next" instances, and not on instances that are already running.
        /// </summary>
        /// <param name="cloudid">ID of the cloud where the instance is configured be launched</param>
        /// <param name="instanceid">ID of the instance to be launched</param>
        /// <param name="inputs">Hashtable for inputs to the launch process</param>
        /// <returns>true if successful, false if not</returns>
        public static string launch(string cloudid, string instanceid, Hashtable inputs)
        {
            return launch(cloudid, instanceid, Utility.convertToKVP(inputs));
        }

        /// <summary>
        /// Launches an instance using the parameters that this instance has been configured with.
        /// Note that this action can only be performed in "next" instances, and not on instances that are already running.
        /// </summary>
        /// <param name="cloudid">ID of the cloud where the instance is configured be launched</param>
        /// <param name="instanceid">ID of the instance to be launched</param>
        /// <param name="inputs">List of KeyValuePairs for inputs to the launch process</param>
        /// <returns>true if successful, false if not</returns>
        public static string launch(string cloudid, string instanceid, List<KeyValuePair<string, string>> inputs)
        {
            string postHref = string.Format("/api/clouds/{0}/instances/{1}/launch", cloudid, instanceid);
            return launchPost(postHref, inputs);
        }

        /// <summary>
        /// Launches an instance using the parameters that this instance has been configured with.
        /// Note that this action can only be performed in "next" instances, and not on instances that are already running.
        /// </summary>
        /// <param name="serverid">ID of server whose 'next' instance will be launched</param>
        /// <returns>true if successful, false if not</returns>
        public static string launch_server(string serverid)
        {
            return launch_server(serverid, new List<KeyValuePair<string, string>>());
        }

        /// <summary>
        /// Launches an instance using the parameters that this instance has been configured with.
        /// Note that this action can only be performed in "next" instances, and not on instances that are already running.
        /// </summary>
        /// <param name="serverid">ID of server whose 'next' instance will be launched</param>
        /// <param name="inputs">Hashtable for inputs to the launch process</param>
        /// <returns>true if successful, false if not</returns>
        public static string launch_server(string serverid, Hashtable inputs)
        {
            return launch_server(serverid, Utility.convertToKVP(inputs));
        }

        /// <summary>
        /// Launches an instance using the parameters that this instance has been configured with.
        /// Note that this action can only be performed in "next" instances, and not on instances that are already running.
        /// </summary>
        /// <param name="serverid">ID of server whose 'next' instance will be launched</param>
        /// <param name="inputs">List of KeyValuePairs for inputs to the launch process</param>
        /// <returns>true if successful, false if not</returns>
        public static string launch_server(string serverid, List<KeyValuePair<string, string>> inputs)
        {
            string postHref = string.Format("/api/servers/{0}/launch", serverid);
            return launchPost(postHref, inputs);
        }

        /// <summary>
        /// Launches an instance using the parameters that this instance has been configured with.
        /// Note that this action can only be performed in "next" instances, and not on instances that are already running.
        /// </summary>
        /// <param name="serverArrayID">ID of the ServerArray where an instance will be launched</param>
        /// <returns>true if successful, false if not</returns>
        public static string launch_serverArray(string serverArrayID)
        {
            return launch_serverArray(serverArrayID, new List<KeyValuePair<string, string>>());
        }

        /// <summary>
        /// Launches an instance using the parameters that this instance has been configured with.
        /// Note that this action can only be performed in "next" instances, and not on instances that are already running.
        /// </summary>
        /// <param name="serverArrayID">ID of the ServerArray where an instance will be launched</param>
        /// <param name="inputs">Hashtable for inputs to the launch process</param>
        /// <returns>true if successful, false if not</returns>
        public static string launch_serverArray(string serverArrayID, Hashtable inputs)
        {
            return launch_serverArray(serverArrayID, Utility.convertToKVP(inputs));
        }

        /// <summary>
        /// Launches an instance using the parameters that this instance has been configured with.
        /// Note that this action can only be performed in "next" instances, and not on instances that are already running.
        /// </summary>
        /// <param name="serverArrayID">ID of the ServerArray where an instance will be launched</param>
        /// <param name="inputs">List of KeyValuePairs for inputs to the launch process</param>
        /// <returns>true if successful, false if not</returns>
        public static string launch_serverArray(string serverArrayID, List<KeyValuePair<string, string>> inputs)
        {
            string postHref = string.Format("/api/server_arrays/{0}/launch", serverArrayID);
            return launchPost(postHref, inputs);
        }

        /// <summary>
        /// Centralized caller to RightScale API for all launch commands on instances
        /// </summary>
        /// <param name="postHref">API Href fragment for REST POST call</param>
        /// <param name="inputs">list of keyvalepairs to be used as inputs for this given instance</param>
        /// <returns>True if successful, false if not</returns>
        private static string launchPost(string postHref, List<KeyValuePair<string, string>> inputs)
        {
            List<string> collectionArray =  Core.APIClient.Instance.Create(postHref, inputs, "location");
            return collectionArray.Last<string>().Split('/').Last<string>();
        }

        #endregion

        #region Instance.reboot

        /// <summary>
        /// Reboot a running instance.
        /// Note that this action can only succeed if the instance is running. One cannot reboot instances of type "next".
        /// </summary>
        /// <param name="serverID">ID of the server whose current instance is to be</param>
        /// <returns>true if successful, false if not</returns>
        public static bool reboot_server(string serverID)
        {
            string postHref = string.Format("/api/servers/{0}/reboot", serverID);
            return rebootPost(postHref);
        }

        /// <summary>
        /// Reboot a running instance.
        /// Note that this action can only succeed if the instance is running. One cannot reboot instances of type "next".
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the instance can be found</param>
        /// <param name="instanceID">ID of the Instance to be rebooted</param>
        /// <returns>true if successful, false if not</returns>
        public static bool reboot(string cloudID, string instanceID)
        {
            string postHref = string.Format("/api/clouds/{0}/instances/{1}", cloudID, instanceID);
            return rebootPost(postHref);
        }

        /// <summary>
        /// Private method centralizes calls to reboot process
        /// </summary>
        /// <param name="postHref">RightScale API url fragment for this action</param>
        /// <returns>true if successful, false if not</returns>
        private static bool rebootPost(string postHref)
        {
            return Core.APIClient.Instance.Post(postHref);
        }

        #endregion

        #region Instance.update
        
        /// <summary>
        /// Updates attributes of a single instance.
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the instance to be updated resides</param>
        /// <param name="instanceID">ID of the instance to be updated</param>
        /// <param name="name">Updated name for Instance</param>
        /// <param name="instanceTypeID">Updated InstanceTypeID for Instance</param>
        /// <param name="serverTemplateID">Updated ServerTemplateID for Instance</param>
        /// <param name="multiCloudImageID">Updated MultiCloudImageID for Instance</param>
        /// <param name="securityGroupIDs">Updated SecutiryGroupID array for Instance</param>
        /// <param name="dataCenterID">Updated DataCenterID for Instance</param>
        /// <param name="imageID">Updated ImageID for Instance</param>
        /// <param name="kernelImageID">Updated KernelImageID for Instance</param>
        /// <param name="ramdiskImageID">Updated RamdiskImageID for Instance</param>
        /// <param name="sshKeyID">Updated SshKeyID for Instance</param>
        /// <param name="userData">Updated UserData for Instance</param>
        /// <returns>True if successful, false if not</returns>
        public static bool update(string cloudID, string instanceID, string name, string instanceTypeID, string serverTemplateID, string multiCloudImageID, List<string> securityGroupIDs, string dataCenterID, string imageID, string kernelImageID, string ramdiskImageID, string sshKeyID, string userData)
        {
            string putHref = string.Format("/api/clouds/{0}/instances/{1}", cloudID, instanceID);

            List<KeyValuePair<string, string>> putParameters = new List<KeyValuePair<string, string>>();

            if (securityGroupIDs != null && securityGroupIDs.Count<string>() > 0)
            {
                foreach (string securityGroupID in securityGroupIDs)
                {
                    putParameters.Add(new KeyValuePair<string, string>("server[instance][security_group_hrefs][]", Utility.securityGroupHref(cloudID, securityGroupID)));
                }
            }

            Utility.addParameter(name, "instance[name]", putParameters);
            Utility.addParameter(Utility.instanceTypeHref(cloudID, instanceTypeID), "instance[instance_type_href]", putParameters);
            Utility.addParameter(Utility.serverTemplateHref(serverTemplateID), "instance[server_template_href]", putParameters);
            Utility.addParameter(Utility.datacenterHref(cloudID, dataCenterID), "instance[data_center_href]", putParameters);
            Utility.addParameter(Utility.imageHref(cloudID, imageID), "instnace[image_href]", putParameters);
            Utility.addParameter(Utility.kernelImageHref(cloudID, kernelImageID), "instance[kernel_image_href]", putParameters);
            Utility.addParameter(Utility.ramdiskImageHref(cloudID, ramdiskImageID), "instance[ramdisk_image_href]", putParameters);
            Utility.addParameter(Utility.sshKeyHref(cloudID, sshKeyID), "instance[ssh_key_href]", putParameters);
            Utility.addParameter(userData, "instance[user_data]", putParameters);
            
            return Core.APIClient.Instance.Put(putHref, putParameters);
        }

        #endregion

        #region Instance.multi_run_executable

        /// <summary>
        /// Runs a script or a recipe in the running instances.
        /// This is an asynchronous function, which returns immediately after queuing the executable for execution. Status of the execution can be tracked at the URL returned in the "Location" header.
        /// </summary>
        /// <param name="serverArrayID">ID of the server array to run this executable on</param>
        /// <param name="ignore_lock">boolean indicating if server/deployment locks should be ignored</param>
        /// <param name="inputs">collection of inputs for execution</param>
        /// <param name="recipeName">name of recipe to execute</param>
        /// <param name="rightScriptID">ID of RightScript to execute</param>
        /// <returns>List of Task objects for tracking asynchronous proces sstatus </returns>
        public List<Task> multi_run_executableServerArray(string serverArrayID, bool ignoreLock, List<KeyValuePair<string, string>> inputs, string recipeName, string rightScriptID)
        {
            string postHref = string.Format("/api/server_arrays/{0}/multi_run_executable", serverArrayID);
            return multi_run_executablePost(postHref, ignoreLock, inputs, recipeName, rightScriptID);
        }


        /// <summary>
        /// Runs a script or a recipe in the running instances.
        /// This is an asynchronous function, which returns immediately after queuing the executable for execution. Status of the execution can be tracked at the URL returned in the "Location" header.
        /// </summary>
        /// <param name="cloudID">ID of the cloud to run this executable on</param>
        /// <param name="ignore_lock">boolean indicating if server/deployment locks should be ignored</param>
        /// <param name="inputs">collection of inputs for execution</param>
        /// <param name="recipeName">name of recipe to execute</param>
        /// <param name="rightScriptID">ID of RightScript to execute</param>
        /// <returns>List of Task objects for tracking asynchronous proces sstatus </returns>
        public List<Task> multi_run_executable(string cloudID, bool ignoreLock, List<KeyValuePair<string, string>> inputs, string recipeName, string rightScriptID)
        {
            string postHref = string.Format("/api/clouds/{0}/instances/multi_run_executable", cloudID);
            return multi_run_executablePost(postHref, ignoreLock, inputs, recipeName, rightScriptID);
        }

        /// <summary>
        /// Private method to manage calls to execute multi_run_executable
        /// </summary>
        /// <param name="postHref">href to post data to in order to execute multi_run_executable</param>
        /// <param name="ignore_lock">boolean indicating if server/deployment locks should be ignored</param>
        /// <param name="inputs">collection of inputs for execution</param>
        /// <param name="recipeName">name of recipe to execute</param>
        /// <param name="rightScriptID">ID of RightScript to execute</param>
        /// <returns>list of task objects for tracking asynchronous process status</returns>
        private static List<Task> multi_run_executablePost(string postHref, bool ignore_lock, List<KeyValuePair<string, string>> inputs, string recipeName, string rightScriptID)
        {
            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            postParams.AddRange(Utility.FormatInputCollection(inputs));
            Utility.addParameter(ignore_lock.ToString().ToLower(), "ignore_lock", postParams);
            Utility.addParameter(recipeName, "recipe_name", postParams);
            Utility.addParameter(Utility.rightScriptHref(rightScriptID), "right_script_href", postParams);
            List<string> retVals = Core.APIClient.Instance.Create(postHref, postParams, "location");
            return Task.GetTaskList(retVals);
        }

        #endregion

        #region Instance.multi_terminate

        /// <summary>
        /// Terminates running instances. Either a filter or the parameter 'terminate_all' must be provided.
        /// </summary>
        /// <param name="serverArrayID">ID of a ServerArray to terminate instances in</param>
        /// <param name="terminateAll">Boolean indicating that all instances should be terminated</param>
        /// <param name="filters">Set of filters to limit the number of instances terminated</param>
        /// <returns>true if process is queued successfully, false if not</returns>
        public List<Task> multi_terminate(string cloudID, bool terminateAll, List<Filter> filters)
        {
            string postHref = string.Format("/api/clouds/{0}/instances/multi_terminate", cloudID);
            return multi_terminatePost(terminateAll, filters, postHref);
        }

        /// <summary>
        /// Terminates running instances. Either a filter or the parameter 'terminate_all' must be provided.
        /// </summary>
        /// <param name="serverArrayID">ID of a ServerArray to terminate instances in</param>
        /// <param name="terminateAll">Boolean indicating that all instances should be terminated</param>
        /// <param name="filters">Set of filters to limit the number of instances terminated</param>
        /// <returns>true if process is queued successfully, false if not</returns>
        public List<Task> multi_terminateServerArray(string serverArrayID, bool terminateAll, List<Filter> filters)
        {
            string postHref = string.Format("/api/server_arrays/{0}/multi_terminate");
            return multi_terminatePost(terminateAll, filters, postHref);
        }

        /// <summary>
        /// Centralized method to handle all multi_terminate calls
        /// </summary>
        /// <param name="terminateAll">boolean indicating that all instances should be terminated</param>
        /// <param name="filters">Set of filters to limit the number of instances terminated</param>
        /// <param name="postHref">API Href fragment for posting to RS API</param>
        /// <returns>true if process queued successfully, false if not</returns>
        private static List<Task> multi_terminatePost(bool terminateAll, List<Filter> filters, string postHref)
        {

            if (!terminateAll && (filters == null || filters.Count == 0))
            {
                throw new RightScaleAPIException("Cannot issue command to multi_terminate instances without either specifying that you wish to terminate all or you specify a filter");
            }

            List<string> validFilters = new List<string>() { "datacenter_href", "deployment_href", "name", "os_platform", "parent_href", "private_dns_name", "private_ip_address", "publis_dns_name", "public_ip_address", "resource_uid", "server_template_href", "state" };
            Utility.CheckFilterInput("filters", validFilters, filters);

            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            Utility.addParameter(terminateAll.ToString().ToLower(), "terminate_all", postParams);

            foreach (Filter f in filters)
            {
                Utility.addParameter(f.ToFilterOnlyString(), "filter[]", postParams);
            }

            List<string> taskHrefs = Core.APIClient.Instance.Create(postHref, postParams, "location");
            return Task.GetTaskList(taskHrefs);
        }

        #endregion

        #region Instance.run_executable

        /// <summary>
        /// Runs a recipe in the running instance.
        /// This is an asynchronous function, which returns immediately after queuing the executable for execution. Status of the execution can be tracked at the URL returned in the "Location" header. Note that this can only be performed on running instances
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the instance is configured be launched</param>
        /// <param name="instanceID">ID of the instance to be launched</param>
        /// <param name="recipeName">Name of Recipe to execute</param>
        /// <returns>Task instance for tracking progress of asynchronous process</returns>
        public static Task run_recipe(string cloudID, string instanceID, string recipeName)
        {
            return run_executable(cloudID, instanceID, recipeName, null, new List<KeyValuePair<string, string>>(), false);
        }

        /// <summary>
        /// Runs a recipe in the running instance.  All inputs are inherited from the server and its current settings.
        /// This is an asynchronous function, which returns immediately after queuing the executable for execution. Status of the execution can be tracked at the URL returned in the "Location" header. Note that this can only be performed on running instances
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the instance is configured be launched</param>
        /// <param name="instanceID">ID of the instance to be launched</param>
        /// <param name="recipeName">Name of Recipe to execute</param>
        /// <param name="inputs">Hashtable of inputs for script or recipe</param>
        /// <returns>Task instance for tracking progress of asynchronous process</returns>
        public static Task run_recipe(string cloudID, string instanceID, string recipeName, Hashtable inputs)
        {
            return run_executable(cloudID, instanceID, recipeName, null, inputs, false);
        }

        /// <summary>
        /// Runs a recipe in the running instance.
        /// This is an asynchronous function, which returns immediately after queuing the executable for execution. Status of the execution can be tracked at the URL returned in the "Location" header. Note that this can only be performed on running instances
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the instance is configured be launched</param>
        /// <param name="instanceID">ID of the instance to be launched</param>
        /// <param name="recipeName">Name of Recipe to execute</param>
        /// <param name="inputs">collection of inputs for script or recipe</param>
        /// <returns>Task instance for tracking progress of asynchronous process</returns>
        public static Task run_recipe(string cloudID, string instanceID, string recipeName, List<KeyValuePair<string, string>> inputs)
        {
            return run_executable(cloudID, instanceID, recipeName, null, inputs, false);
        }

        /// <summary>
        /// Runs a recipe in the running instance.
        /// This is an asynchronous function, which returns immediately after queuing the executable for execution. Status of the execution can be tracked at the URL returned in the "Location" header. Note that this can only be performed on running instances
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the instance is configured be launched</param>
        /// <param name="instanceID">ID of the instance to be launched</param>
        /// <param name="recipeName">Name of Recipe to execute</param>
        /// <param name="inputs">Hashtable of inputs for script or recipe</param>
        /// <param name="ignoreLock">Specifies the ability to ignore the lock on the Instance.</param>
        /// <returns>Task instance for tracking progress of asynchronous process</returns>
        public static Task run_recipe(string cloudID, string instanceID, string recipeName, Hashtable inputs, bool ignoreLock)
        {
            return run_executable(cloudID, instanceID, recipeName, null, inputs, ignoreLock);
        }

        /// <summary>
        /// Runs a recipe in the running instance.
        /// This is an asynchronous function, which returns immediately after queuing the executable for execution. Status of the execution can be tracked at the URL returned in the "Location" header. Note that this can only be performed on running instances
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the instance is configured be launched</param>
        /// <param name="instanceID">ID of the instance to be launched</param>
        /// <param name="recipeName">Name of Recipe to execute</param>
        /// <param name="inputs">collection of inputs for script or recipe</param>
        /// <param name="ignoreLock">Specifies the ability to ignore the lock on the Instance.</param>
        /// <returns>Task instance for tracking progress of asynchronous process</returns>
        public static Task run_recipe(string cloudID, string instanceID, string recipeName, List<KeyValuePair<string, string>> inputs, bool ignoreLock)
        {
            return run_executable(cloudID, instanceID, recipeName, null, inputs, ignoreLock);
        }

        /// <summary>
        /// Runs a script in the running instance.  All inputs are inherited from the server and its current settings.
        /// This is an asynchronous function, which returns immediately after queuing the executable for execution. Status of the execution can be tracked at the URL returned in the "Location" header. Note that this can only be performed on running instances
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the instance is configured be launched</param>
        /// <param name="instanceID">ID of the instance to be launched</param>
        /// <param name="rightScriptID">ID of RightScript to execute</param>
        /// <returns>Task instance for tracking progress of asynchronous process</returns>
        public static Task run_rightScript(string cloudID, string instanceID, string rightScriptID)
        {
            return run_executable(cloudID, instanceID, null, rightScriptID, new List<KeyValuePair<string, string>>(), false);
        }

        /// <summary>
        /// Runs a script in the running instance.
        /// This is an asynchronous function, which returns immediately after queuing the executable for execution. Status of the execution can be tracked at the URL returned in the "Location" header. Note that this can only be performed on running instances
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the instance is configured be launched</param>
        /// <param name="instanceID">ID of the instance to be launched</param>
        /// <param name="rightScriptID">ID of RightScript to execute</param>
        /// <param name="inputs">collection of inputs for script or recipe</param>
        /// <returns>Task instance for tracking progress of asynchronous process</returns>
        public static Task run_rightScript(string cloudID, string instanceID, string rightScriptID, List<KeyValuePair<string, string>> inputs)
        {
            return run_executable(cloudID, instanceID, null, rightScriptID, inputs, false);
        }

        /// <summary>
        /// Runs a script in the running instance.
        /// This is an asynchronous function, which returns immediately after queuing the executable for execution. Status of the execution can be tracked at the URL returned in the "Location" header. Note that this can only be performed on running instances
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the instance is configured be launched</param>
        /// <param name="instanceID">ID of the instance to be launched</param>
        /// <param name="rightScriptID">ID of RightScript to execute</param>
        /// <param name="inputs">Hashtable of inputs for script or recipe</param>
        /// <returns>Task instance for tracking progress of asynchronous process</returns>
        public static Task run_rightScript(string cloudID, string instanceID, string rightScriptID, Hashtable inputs)
        {
            return run_executable(cloudID, instanceID, null, rightScriptID, inputs, false);
        }

        /// <summary>
        /// Runs a script in the running instance.
        /// This is an asynchronous function, which returns immediately after queuing the executable for execution. Status of the execution can be tracked at the URL returned in the "Location" header. Note that this can only be performed on running instances
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the instance is configured be launched</param>
        /// <param name="instanceID">ID of the instance to be launched</param>
        /// <param name="rightScriptID">ID of RightScript to execute</param>
        /// <param name="inputs">collection of inputs for script or recipe</param>
        /// <param name="ignoreLock">Specifies the ability to ignore the lock on the Instance.</param>
        /// <returns>Task instance for tracking progress of asynchronous process</returns>
        public static Task run_rightScript(string cloudID, string instanceID, string rightScriptID, List<KeyValuePair<string, string>> inputs, bool ignoreLock)
        {
            return run_executable(cloudID, instanceID, null, rightScriptID, inputs, ignoreLock);
        }

        /// <summary>
        /// Runs a script in the running instance.
        /// This is an asynchronous function, which returns immediately after queuing the executable for execution. Status of the execution can be tracked at the URL returned in the "Location" header. Note that this can only be performed on running instances
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the instance is configured be launched</param>
        /// <param name="instanceID">ID of the instance to be launched</param>
        /// <param name="rightScriptID">ID of RightScript to execute</param>
        /// <param name="inputs">Hashtable of inputs for script or recipe</param>
        /// <param name="ignoreLock">Specifies the ability to ignore the lock on the Instance.</param>
        /// <returns>Task instance for tracking progress of asynchronous process</returns>
        public static Task run_rightScript(string cloudID, string instanceID, string rightScriptID, Hashtable inputs, bool ignoreLock)
        {
            return run_executable(cloudID, instanceID, null, rightScriptID, inputs, ignoreLock);
        }

        /// <summary>
        /// Runs a script or a recipe in the running instance.
        /// This is an asynchronous function, which returns immediately after queuing the executable for execution. Status of the execution can be tracked at the URL returned in the "Location" header. Note that this can only be performed on running instances
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the instance is configured be launched</param>
        /// <param name="instanceID">ID of the instance to be launched</param>
        /// <param name="recipeName">Name of Recipe to execute</param>
        /// <param name="rightScriptID">ID of RightScript to execute</param>
        /// <param name="inputs">collection of inputs for script or recipe</param>
        /// <param name="ignoreLock">Specifies the ability to ignore the lock on the Instance.</param>
        /// <returns>Task instance for tracking progress of asynchronous process</returns>
        public static Task run_executable(string cloudID, string instanceID, string recipeName, string rightScriptID, List<KeyValuePair<string, string>> inputs, bool ignoreLock)
        {
            return run_executablePost(cloudID, instanceID, recipeName, rightScriptID, inputs, ignoreLock);
        }

        /// <summary>
        /// Runs a script or a recipe in the running instance.
        /// This is an asynchronous function, which returns immediately after queuing the executable for execution. Status of the execution can be tracked at the URL returned in the "Location" header. Note that this can only be performed on running instances
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the instance is configured be launched</param>
        /// <param name="instanceID">ID of the instance to be launched</param>
        /// <param name="recipeName">Name of Recipe to execute</param>
        /// <param name="rightScriptID">ID of RightScript to execute</param>
        /// <param name="inputs">Hashtable of inputs for script or recipe</param>
        /// <param name="ignoreLock">Specifies the ability to ignore the lock on the Instance.</param>
        /// <returns>Task instance for tracking progress of asynchronous process</returns>
        public static Task run_executable(string cloudID, string instanceID, string recipeName, string rightScriptID, Hashtable inputs, bool ignoreLock)
        {
            return run_executablePost(cloudID, instanceID, recipeName, rightScriptID, Utility.convertToKVP(inputs), ignoreLock);
        }

        /// <summary>
        /// Centralized method to handle all run_executable based POST calls
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the instance is configured be launched</param>
        /// <param name="instanceID">ID of the instance to be launched</param>
        /// <param name="recipeName">Name of Recipe to execute</param>
        /// <param name="rightScriptID">ID of RightScript to execute</param>
        /// <param name="inputs">collection of inputs for script or recipe</param>
        /// <param name="ignoreLock">Specifies the ability to ignore the lock on the Instance.</param>
        /// <returns>Task instance for tracking progress of asynchronous process</returns>
        private static Task run_executablePost(string cloudID, string instanceID, string recipeName, string rightScriptID, List<KeyValuePair<string, string>> inputs, bool ignoreLock)
        {
            string postHref = string.Format("/api/clouds/{0}/instances/{1}/run_executable", cloudID, instanceID);

            List<KeyValuePair<string, string>> postParameters = new List<KeyValuePair<string, string>>();

            if (inputs != null && inputs.Count > 0)
            {
                postParameters.AddRange(Utility.FormatInputCollection(inputs));
            }

            Utility.addParameter(recipeName, "recipe_name", postParameters);
            Utility.addParameter(Utility.rightScriptHref(rightScriptID), "right_script_href", postParameters);
            Utility.addParameter(ignoreLock.ToString().ToLower(), "ignore_lock", postParameters);

            List<string> taskList = Core.APIClient.Instance.Create(postHref, postParameters, "location");
            return Task.GetTask(taskList.Last<string>());
        }

        #endregion

        #region Instance.set_custom_lodgement

        /// <summary>
        /// This method is deprecated. Please use InstanceCustomLodgement.
        /// </summary>
        /// <param name="quantity">At least one name/value pair must be specified. Currently, a maximum of 2 name/value pairs is supported.</param>
        /// <param name="timeFrame">The timeframe (either a month or a single day) for which the quantity value is valid (currently for the PDT timezone only).</param>
        /// <returns>True if successfully submitted to RSAPI, false if not</returns>
        public bool set_custom_lodgement(string cloudID, string instanceID, List<KeyValuePair<string, string>> quantity, string timeFrame)
        {
            string postHref = string.Format("/api/clouds/{0}/instances/{1}/set_custom_lodgement", cloudID, instanceID);
            if (quantity.Count > 2 || quantity.Count < 1)
            {
                throw new RightScaleAPIException("Currently, a maximum of 2 name/value pairs is supported.  " + quantity.Count.ToString() + " were specified.");
            }

            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();

            foreach (KeyValuePair<string, string> kvp in quantity)
            {
                Utility.CheckStringRegex("quantity[][name]", @"^(\w|\/|\ )+$", kvp.Key);
                Utility.CheckStringRegex("quantity[][value]", @"^-?\d+$", kvp.Value);
                Utility.addParameter(kvp.Key, "quantity[][name]", postParams);
                Utility.addParameter(kvp.Value, "quantity[][value]", postParams);
            }

            Utility.CheckStringRegex("timeframe", @"^\d{4}\/\d{2}(\/\d{2})?$", timeFrame);
            Utility.addParameter(timeFrame, "timeframe", postParams);
            return Core.APIClient.Instance.Post(postHref, postParams);
        }

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
