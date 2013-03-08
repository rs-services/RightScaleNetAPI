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
        /// ID of the cloud that this Instance is running in
        /// </summary>
        public string CloudID
        {
            get
            {
                return getLinkIDValue("cloud");
            }
        }

        /// <summary>
        /// ID of the ServerTemplate that this instance was launched from
        /// </summary>
        public string ServerTemplateID
        {
            get
            {
                return getLinkIDValue("server_template");
            }
        }

        /// <summary>
        /// ID of the MultiCloudImage that this instance was launched from
        /// </summary>
        public string MultiCloudImageID
        {
            get
            {
                return getLinkIDValue("multi_cloud_image");
            }
        }

        /// <summary>
        /// Specific Image ID used to launch this instance
        /// </summary>
        public string ImageID
        {
            get
            {
                return getLinkIDValue("image");
            }
        }

        /// <summary>
        /// Ramdisk Image ID for this instance
        /// </summary>
        public string RamdiskImageID
        {
            get
            {
                return getLinkIDValue("ramdisk_image");
            }
        }

        /// <summary>
        /// Kernel Image ID for this instance
        /// </summary>
        public string KernelImageID
        {
            get
            {
                return getLinkIDValue("kernel_image");
            }
        }

        /// <summary>
        /// InstanceType ID for this instance
        /// </summary>
        public string InstanceTypeID
        {
            get
            {
                return getLinkIDValue("instance_type");
            }
        }

        /// <summary>
        /// SshKey ID for this instance
        /// </summary>
        public string SshKeyID
        {
            get
            {
                return getLinkIDValue("ssh_key");
            }
        }

        /// <summary>
        /// Datacenter ID for this instance
        /// </summary>
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

        #region Instance.launch

        /// <summary>
        /// Launches an instance using the parameters that this instance has been configured with.
        /// Note that this action can only be performed in "next" instances, and not on instances that are already running.
        /// </summary>
        /// <param name="cloudid">ID of the cloud where the instance is configured be launched</param>
        /// <param name="instanceid">ID of the instance to be launched</param>
        /// <returns>true if successful, false if not</returns>
        public static bool launch(string cloudid, string instanceid)
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
        public static bool launch(string cloudid, string instanceid, Hashtable inputs)
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
        public static bool launch(string cloudid, string instanceid, List<KeyValuePair<string, string>> inputs)
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
        public static bool launch_server(string serverid)
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
        public static bool launch_server(string serverid, Hashtable inputs)
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
        public static bool launch_server(string serverid, List<KeyValuePair<string, string>> inputs)
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
        public static bool launch_serverArray(string serverArrayID)
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
        public static bool launch_serverArray(string serverArrayID, Hashtable inputs)
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
        public static bool launch_serverArray(string serverArrayID, List<KeyValuePair<string, string>> inputs)
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
        private static bool launchPost(string postHref, List<KeyValuePair<string, string>> inputs)
        {
            return Core.APIClient.Instance.Post(postHref, inputs);
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
        /// <param name="cloudID"></param>
        /// <param name="instanceID"></param>
        /// <param name="name"></param>
        /// <param name="instanceTypeID"></param>
        /// <param name="serverTemplateID"></param>
        /// <param name="multiCloudImageID"></param>
        /// <param name="securityGroupIDs"></param>
        /// <param name="dataCenterID"></param>
        /// <param name="imageID"></param>
        /// <param name="kernelImageID"></param>
        /// <param name="ramdiskImageID"></param>
        /// <param name="sshKeyID"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
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

        #endregion

        #region Instance.multi_terminate

        #endregion

        #region Instance.run_executable

        /// <summary>
        /// Runs a recipe in the running instance.
        /// This is an asynchronous function, which returns immediately after queuing the executable for execution. Status of the execution can be tracked at the URL returned in the "Location" header. Note that this can only be performed on running instances
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the instance is configured be launched</param>
        /// <param name="instanceID">ID of the instance to be launched</param>
        /// <param name="recipeName">Name of Recipe to execute</param>
        /// <returns>True if queued, false if not</returns>
        public bool run_recipe(string cloudID, string instanceID, string recipeName)
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
        /// <returns>True if queued, false if not</returns>
        public bool run_recipe(string cloudID, string instanceID, string recipeName, Hashtable inputs)
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
        /// <returns>True if queued, false if not</returns>
        public bool run_recipe(string cloudID, string instanceID, string recipeName, List<KeyValuePair<string, string>> inputs)
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
        /// <returns>True if queued, false if not</returns>
        public bool run_recipe(string cloudID, string instanceID, string recipeName, Hashtable inputs, bool ignoreLock)
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
        /// <returns>True if queued, false if not</returns>
        public bool run_recipe(string cloudID, string instanceID, string recipeName, List<KeyValuePair<string, string>> inputs, bool ignoreLock)
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
        /// <returns>True if queued, false if not</returns>
        public bool run_rightScript(string cloudID, string instanceID, string rightScriptID)
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
        /// <returns>True if queued, false if not</returns>
        public bool run_rightScript(string cloudID, string instanceID, string rightScriptID, List<KeyValuePair<string, string>> inputs)
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
        /// <returns>True if queued, false if not</returns>
        public bool run_rightScript(string cloudID, string instanceID, string rightScriptID, Hashtable inputs)
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
        /// <returns>True if queued, false if not</returns>
        public bool run_rightScript(string cloudID, string instanceID, string rightScriptID, List<KeyValuePair<string, string>> inputs, bool ignoreLock)
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
        /// <returns>True if queued, false if not</returns>
        public bool run_rightScript(string cloudID, string instanceID, string rightScriptID, Hashtable inputs, bool ignoreLock)
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
        /// <returns>True if queued, false if not</returns>
        public bool run_executable(string cloudID, string instanceID, string recipeName, string rightScriptID, List<KeyValuePair<string, string>> inputs, bool ignoreLock)
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
        /// <returns>True if queued, false if not</returns>
        public bool run_executable(string cloudID, string instanceID, string recipeName, string rightScriptID, Hashtable inputs, bool ignoreLock)
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
        /// <returns>True if queued, false if not</returns>
        private static bool run_executablePost(string cloudID, string instanceID, string recipeName, string rightScriptID, List<KeyValuePair<string, string>> inputs, bool ignoreLock)
        {
            string postHref = string.Format("/api/clouds/{0}/instances/{1}/run_executable", cloudID, instanceID);

            List<KeyValuePair<string, string>> postParameters = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrWhiteSpace(recipeName))
            {
                postParameters.Add(new KeyValuePair<string, string>("recipe_name", recipeName));
            }
            if (!string.IsNullOrWhiteSpace(rightScriptID))
            {
                postParameters.Add(new KeyValuePair<string, string>("right_script_href", Utility.rightScriptHref(rightScriptID)));
            }
            if (inputs != null && inputs.Count > 0)
            {
                postParameters.AddRange(Utility.FormatInputCollection(inputs));
            }

            if (ignoreLock)
            {
                postParameters.Add(new KeyValuePair<string, string>("ignore_lock", "true"));
            }
            else
            {
                postParameters.Add(new KeyValuePair<string, string>("ignore_lock", "false"));
            }
            return Core.APIClient.Instance.Post(postHref, postParameters);
        }

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
