using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// A server array represents a logical group of instances and allows to resize(grow/shrink) that group based on certain elasticity parameters.
    /// A server array just like a server always has a next_instance association, which will define the configuration to apply when a new instance is launched. But unlike a server which has a "currentinstance" relationship, the server array has a "currentinstances" relationship that gives the information about all the running instances in the array. Changes to the next_instance association prepares the configuration for the next instance that is to be launched in the array and will therefore not affect any of the currently running instances.
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeServerArray.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceServerArrays.html
    /// </summary>
    public class ServerArray : Core.RightScaleObjectBase<ServerArray>
    {
        #region ServerArray properties

        /// <summary>
        /// Name of this server array
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Elasticity parameters for this server array
        /// </summary>
        public ElasticityParams elasticity_params { get; set; }

        /// <summary>
        /// Instance object defining the next instance within this ServerArray
        /// </summary>
        public Instance next_instance { get; set; }

        /// <summary>
        /// Type of this server array
        /// </summary>
        public string array_type { get; set; }

        /// <summary>
        /// Count of interfaces for this ServerArray
        /// </summary>
        public int instances_count { get; set; }

        /// <summary>
        /// Description of this ServerArray
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Current state of this ServerArray
        /// </summary>
        public string state { get; set; }

        #endregion

        #region ServerArray Relationships

        /// <summary>
        /// List of AlertSpecs associated with this ServerArray
        /// </summary>
        public List<AlertSpec> alertSpecs
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("alert_specs"));
                return AlertSpec.deserializeList(jsonString);
            }
        }

        /// <summary>
        /// Next instance associated with this ServerArray
        /// </summary>
        public Instance nextInstance
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("next_instance"));
                return Instance.deserialize(jsonString);
            }
        }

        /// <summary>
        /// List of current instances associated with this ServerArray
        /// </summary>
        public List<Instance> currentInstances
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("current_instances"));
                return Instance.deserializeList(jsonString);
            }
        }

        /// <summary>
        /// Deployment associated with this ServerArray
        /// </summary>
        public Deployment deployment
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("deployment"));
                return Deployment.deserialize(jsonString);
            }
        }

        #endregion

        #region ServerArray.ctor
        /// <summary>
        /// Default Constructor for ServerArray
        /// </summary>
        public ServerArray()
            : base()
        {
        }

        /// <summary>
        /// Constructor for ServerArray object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public ServerArray(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for ServerArray object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public ServerArray(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		        
        #region ServerArray.index methods

        public static List<ServerArray> index()
        {
            return index(null, null);
        }

        public static List<ServerArray> index(List<Filter> filter)
        {
            return index(filter, null);
        }

        public static List<ServerArray> index(string view)
        {
            return index(null, view);
        }

        public static List<ServerArray> index(List<Filter> filterlist, string view)
        {
            string getUrl = APIHrefs.ServerArray;
            string queryString = string.Empty;

            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default", "instance_detail" };
                Utility.CheckStringInput("view", validViews, view);
            }


            List<string> validFilters = new List<string>() { "cloud_href", "deployment_href", "name" };
            Utility.CheckFilterInput("filter", validFilters, filterlist);

            if (filterlist != null && filterlist.Count > 0)
            {
                queryString += Utility.BuildFilterString(filterlist) + "&";
            }
            if (!string.IsNullOrWhiteSpace(view))
            {
                queryString += string.Format("view={0}", view);
            }

            string jsonString = Core.APIClient.Instance.Get(getUrl, queryString);

            return deserializeList(jsonString);

        }
        #endregion

        #region ServerArray.show methods

        /// <summary>
        /// Shows the information of a single image.
        /// </summary>
        /// <param name="serverid">ID of the image to be retrieved</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include.</param>
        /// <returns>Populated ServerArray object</returns>
        public static ServerArray show(string serverarrayid, string view)
        {
            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default", "instance_detail" };
                Utility.CheckStringInput("view", validViews, view);
            }

            string getHref = string.Format(APIHrefs.ServerArrayById, serverarrayid);
            return showGet(getHref, view);
        }

        /// <summary>
        /// Internal implementation of show for both deployment and non-deployment calls.  
        /// </summary>
        /// <param name="getHref"></param>
        /// <param name="view"></param>
        /// <returns>Image object with data</returns>
        private static ServerArray showGet(string getHref, string view)
        {
            List<string> validViews = new List<string>() { "default" };
            Utility.CheckStringInput("view", validViews, view);

            string queryString = string.Empty;

            if (!string.IsNullOrWhiteSpace(view))
            {
                queryString += string.Format("view={0}", view);
            }

            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserialize(jsonString);
        }


        #endregion

        #region ServerArray.create methods

        /// <summary>
        /// Create a ServerArray with the specified parameters using the Deployment-specific href
        /// </summary>
        /// <param name="array_type">Type of array (alert/queue)</param>
        /// <param name="dataCenterPolicy">DataCenterPolicy object defining ServerArray launch target behavior</param>
        /// <param name="deploymentID">ID of deployment this ServerArray should be created in</param>
        /// <param name="description">Description of this ServerArray</param>
        /// <param name="elasticityParams">ElasticityParams object defininig how the ServerArray will grow under load/demand</param>
        /// <param name="cloudID">Id of Cloud this Array is being deployed to</param>
        /// <param name="dataCenterID">ID of DataCenter to be deployed in if multiple datacenters are not available</param>
        /// <param name="inputs">Collection of inputs to be passed to servers as they're being deployed</param>
        /// <param name="instanceTypeID">ID of InstanceType to be launched</param>
        /// <param name="imageID">ID of OS Image to be used</param>
        /// <param name="kernelImageID">ID of Kernel Image to be used</param>
        /// <param name="multiCloudImageID">ID of MultiCloud Image to be used</param>
        /// <param name="ramdiskImageID">ID of Ramdisk image to be used</param>
        /// <param name="securityGroupIDs">Collection of SecurityGroup IDs to be applied to each instance when launched</param>
        /// <param name="serverTemplateID">ID of ServerTemplate to be used when launching instances</param>
        /// <param name="sshKeyID">ID of SSH Key to be used when launching instances</param>
        /// <param name="userData">user data to be passed to each instance at launch time</param>
        /// <param name="name">name of the ServerArray</param>
        /// <param name="optimized">boolean indicating if this server is to utilized optimized IO features if available on the cloud being deployed to</param>
        /// <param name="state">State of the server (enabled/disabled)</param>
        /// <returns>ID of the newly created ServerArray</returns>
        public static string create_deployment(string array_type, List<DataCenterPolicy> dataCenterPolicy, string deploymentID, string description, List<ElasticityParams> elasticityParams, string cloudID, string dataCenterID, List<KeyValuePair<string, string>> inputs, string instanceTypeID, string imageID, string kernelImageID, string multiCloudImageID, string ramdiskImageID, List<string> securityGroupIDs, string serverTemplateID, string sshKeyID, string userData, string name, bool optimized, string state)
        {
            string postString = string.Format(APIHrefs.DeploymentServerArray, deploymentID);
            return createPost(postString, array_type, dataCenterPolicy, deploymentID, description, elasticityParams, cloudID, dataCenterID, inputs, instanceTypeID, imageID, kernelImageID, multiCloudImageID, ramdiskImageID, securityGroupIDs, serverTemplateID, sshKeyID, userData, name, optimized, state);
        }

        /// <summary>
        /// Minimal implementation of ServerArray.create_deployment
        /// </summary>
        /// <param name="array_type"></param>
        /// <param name="dataCenterPolicy"></param>
        /// <param name="elasticityParams"></param>
        /// <param name="cloudID"></param>
        /// <param name="deploymentID"></param>
        /// <param name="serverTemplateID"></param>
        /// <param name="name"></param>
        /// <param name="state"></param>
        /// <returns>ID of the newly created ServerArray</returns>
        public static string create_deployment(string array_type, List<DataCenterPolicy> dataCenterPolicy, List<ElasticityParams> elasticityParams, string cloudID, string deploymentID, string serverTemplateID, string name, string state)
        {
            string postString = string.Format(APIHrefs.DeploymentServerArray, deploymentID);
            return createPost(postString, array_type, dataCenterPolicy, deploymentID, string.Empty, elasticityParams, cloudID, string.Empty, new List<KeyValuePair<string, string>>(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, new List<string>(), serverTemplateID, string.Empty, string.Empty, name, false, state);
        }

        /// <summary>
        /// Create a ServerArray with the specified parameters
        /// </summary>
        /// <param name="array_type">Type of array (alert/queue)</param>
        /// <param name="dataCenterPolicy">DataCenterPolicy object defining ServerArray launch target behavior</param>
        /// <param name="deploymentID">ID of deployment this ServerArray should be created in</param>
        /// <param name="description">Description of this ServerArray</param>
        /// <param name="elasticityParams">ElasticityParams object defininig how the ServerArray will grow under load/demand</param>
        /// <param name="cloudID">Id of Cloud this Array is being deployed to</param>
        /// <param name="dataCenterID">ID of DataCenter to be deployed in if multiple datacenters are not available</param>
        /// <param name="inputs">Collection of inputs to be passed to servers as they're being deployed</param>
        /// <param name="instanceTypeID">ID of InstanceType to be launched</param>
        /// <param name="imageID">ID of OS Image to be used</param>
        /// <param name="kernelImageID">ID of Kernel Image to be used</param>
        /// <param name="multiCloudImageID">ID of MultiCloud Image to be used</param>
        /// <param name="ramdiskImageID">ID of Ramdisk image to be used</param>
        /// <param name="securityGroupIDs">Collection of SecurityGroup IDs to be applied to each instance when launched</param>
        /// <param name="serverTemplateID">ID of ServerTemplate to be used when launching instances</param>
        /// <param name="sshKeyID">ID of SSH Key to be used when launching instances</param>
        /// <param name="userData">user data to be passed to each instance at launch time</param>
        /// <param name="name">name of the ServerArray</param>
        /// <param name="optimized">boolean indicating if this server is to utilized optimized IO features if available on the cloud being deployed to</param>
        /// <param name="state">State of the server (enabled/disabled)</param>
        /// <returns>ID of the newly created ServerArray</returns>
        public static string create(string array_type, List<DataCenterPolicy> dataCenterPolicy, string deploymentID, string description, List<ElasticityParams> elasticityParams, string cloudID, string dataCenterID, List<KeyValuePair<string, string>> inputs, string instanceTypeID, string imageID, string kernelImageID, string multiCloudImageID, string ramdiskImageID, List<string> securityGroupIDs, string serverTemplateID, string sshKeyID, string userData, string name, bool optimized, string state)
        {
            return createPost(APIHrefs.ServerArray, array_type, dataCenterPolicy, deploymentID, description, elasticityParams, cloudID, dataCenterID, inputs, instanceTypeID, imageID, kernelImageID, multiCloudImageID, ramdiskImageID, securityGroupIDs, serverTemplateID, sshKeyID, userData, name, optimized, state);
        }

        /// <summary>
        /// Minimal implementation of ServerArray.create 
        /// </summary>
        /// <param name="array_type">Type of array (alert/queue)</param>
        /// <param name="dataCenterPolicy">DataCenterPolicy object defining ServerArray launch target behavior</param>
        /// <param name="elasticityParams">ElasticityParams object defininig how the ServerArray will grow under load/demand</param>
        /// <param name="cloudID">Id of Cloud this Array is being deployed to</param>
        /// <param name="serverTemplateID"></param>
        /// <param name="name">name of the ServerArray</param>
        /// <param name="state">State of the server (enabled/disabled)</param>
        /// <returns>ID of the newly created ServerArray</returns>
        public static string create(string array_type, List<DataCenterPolicy> dataCenterPolicy, List<ElasticityParams> elasticityParams, string cloudID, string deploymentID, string serverTemplateID, string name, string state)
        {
            return createPost(APIHrefs.ServerArray, array_type, dataCenterPolicy, deploymentID, string.Empty, elasticityParams, cloudID, string.Empty, new List<KeyValuePair<string, string>>(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, new List<string>(), serverTemplateID, string.Empty, string.Empty, name, false, state);
        }

        /// <summary>
        /// Private method performs server array create with the specified parameters
        /// </summary>
        /// <param name="postHref">API fragment to post data to when creating this ServerArray</param>
        /// <param name="array_type">Type of array (alert/queue)</param>
        /// <param name="dataCenterPolicy">DataCenterPolicy object defining ServerArray launch target behavior</param>
        /// <param name="deploymentID">ID of deployment this ServerArray should be created in</param>
        /// <param name="description">Description of this ServerArray</param>
        /// <param name="elasticityParams">ElasticityParams object defininig how the ServerArray will grow under load/demand</param>
        /// <param name="cloudID">Id of Cloud this Array is being deployed to</param>
        /// <param name="dataCenterID">ID of DataCenter to be deployed in if multiple datacenters are not available</param>
        /// <param name="inputs">Collection of inputs to be passed to servers as they're being deployed</param>
        /// <param name="instanceTypeID">ID of InstanceType to be launched</param>
        /// <param name="imageID">ID of OS Image to be used</param>
        /// <param name="kernelImageID">ID of Kernel Image to be used</param>
        /// <param name="multiCloudImageID">ID of MultiCloud Image to be used</param>
        /// <param name="ramdiskImageID">ID of Ramdisk image to be used</param>
        /// <param name="securityGroupIDs">Collection of SecurityGroup IDs to be applied to each instance when launched</param>
        /// <param name="serverTemplateID">ID of ServerTemplate to be used when launching instances</param>
        /// <param name="sshKeyID">ID of SSH Key to be used when launching instances</param>
        /// <param name="userData">user data to be passed to each instance at launch time</param>
        /// <param name="name">name of the ServerArray</param>
        /// <param name="optimized">boolean indicating if this server is to utilized optimized IO features if available on the cloud being deployed to</param>
        /// <param name="state">State of the server (enabled/disabled)</param>
        /// <returns>ID of the newly created ServerArray</returns>
        private static string createPost(string postHref, string array_type, List<DataCenterPolicy> dataCenterPolicy, string deploymentID, string description, List<ElasticityParams> elasticityParams, string cloudID, string dataCenterID, List<KeyValuePair<string, string>> inputs, string instanceTypeID, string imageID, string kernelImageID, string multiCloudImageID, string ramdiskImageID, List<string> securityGroupIDs, string serverTemplateID, string sshKeyID, string userData, string name, bool optimized, string state)
        {
            validateRequiredServerArrayCreateInputs(array_type, dataCenterPolicy, elasticityParams, cloudID, serverTemplateID, name, state);

            List<KeyValuePair<string, string>> paramSet = serverArrayParams(array_type, dataCenterPolicy, deploymentID, description, elasticityParams, cloudID, dataCenterID, inputs, instanceTypeID, imageID, kernelImageID, multiCloudImageID, ramdiskImageID, securityGroupIDs, serverTemplateID, sshKeyID, userData, name, optimized, state);

            List<string> retVal = Core.APIClient.Instance.Post(APIHrefs.ServerArray, paramSet, "location");

            return retVal.Last<string>().Split('/').Last<string>(); //id get hack
        }

        /// <summary>
        /// Private method validates required inputs per the documentation provided within RS API 1.5 rdoc
        /// </summary>
        /// <param name="array_type">Type of array (alert/queue)</param>
        /// <param name="dataCenterPolicy">DataCenterPolicy object defining ServerArray launch target behavior</param>
        /// <param name="elasticityParams">ElasticityParams object defininig how the ServerArray will grow under load/demand</param>
        /// <param name="cloudID">Id of Cloud this Array is being deployed to</param>
        /// <param name="serverTemplateID"></param>
        /// <param name="name">name of the ServerArray</param>
        /// <param name="state">State of the server (enabled/disabled)</param>
        private static void validateRequiredServerArrayCreateInputs(string array_type, List<DataCenterPolicy> dataCenterPolicy, List<ElasticityParams> elasticityParams, string cloudID, string serverTemplateID, string name, string state)
        {
            Utility.CheckStringHasValue(array_type);

            if (dataCenterPolicy != null && dataCenterPolicy.Count > 0)
            {
                foreach (var dcp in dataCenterPolicy)
                {
                    Utility.CheckStringHasValue(dcp.dataCenterId);
                    Utility.CheckStringHasValue(dcp.max);
                    Utility.CheckStringHasValue(dcp.weight);
                }
            }

            if (elasticityParams != null && elasticityParams.Count > 0)
            {
                foreach (var ep in elasticityParams)
                {
                    if (ep.schedule_entries != null && ep.schedule_entries.Count > 0)
                    {
                        foreach (var se in ep.schedule_entries)
                        {
                            Utility.CheckStringHasValue(se.day);
                            Utility.CheckStringHasValue(se.max_count);
                            Utility.CheckStringHasValue(se.min_count);
                            Utility.CheckStringHasValue(se.time);
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("elasticityParams mst be populated to create a new ServerArray");
            }

            Utility.CheckStringHasValue(cloudID);
            Utility.CheckStringHasValue(serverTemplateID);
            Utility.CheckStringHasValue(name);
            Utility.CheckStringHasValue(state);
        }

        #endregion

        #region ServerArray shared private helper methods

        /// <summary>
        /// Private helper method takes all available inputs for a ServerArray and creates a collection of parameters to pass into the RightScale API
        /// </summary>
        /// <param name="array_type">Type of array (alert/queue)</param>
        /// <param name="dataCenterPolicy">DataCenterPolicy object defining ServerArray launch target behavior</param>
        /// <param name="deploymentID">ID of deployment this ServerArray should be created in</param>
        /// <param name="description">Description of this ServerArray</param>
        /// <param name="elasticityParams">ElasticityParams object defininig how the ServerArray will grow under load/demand</param>
        /// <param name="cloudID">Id of Cloud this Array is being deployed to</param>
        /// <param name="dataCenterID">ID of DataCenter to be deployed in if multiple datacenters are not available</param>
        /// <param name="inputs">Collection of inputs to be passed to servers as they're being deployed</param>
        /// <param name="instanceTypeID">ID of InstanceType to be launched</param>
        /// <param name="imageID">ID of OS Image to be used</param>
        /// <param name="kernelImageID">ID of Kernel Image to be used</param>
        /// <param name="multiCloudImageID">ID of MultiCloud Image to be used</param>
        /// <param name="ramdiskImageID">ID of Ramdisk image to be used</param>
        /// <param name="securityGroupIDs">Collection of SecurityGroup IDs to be applied to each instance when launched</param>
        /// <param name="serverTemplateID">ID of ServerTemplate to be used when launching instances</param>
        /// <param name="sshKeyID">ID of SSH Key to be used when launching instances</param>
        /// <param name="userData">user data to be passed to each instance at launch time</param>
        /// <param name="name">name of the ServerArray</param>
        /// <param name="optimized">boolean indicating if this server is to utilized optimized IO features if available on the cloud being deployed to</param>
        /// <param name="state">State of the server (enabled/disabled)</param>
        /// <returns>Collection of KeyValuePairs to be passed to the RightScale API to create or update a ServerArray</returns>
        private static List<KeyValuePair<string, string>> serverArrayParams(string array_type, List<DataCenterPolicy> dataCenterPolicy, string deploymentID, string description, List<ElasticityParams> elasticityParams, string cloudID, string dataCenterID, List<KeyValuePair<string, string>> inputs, string instanceTypeID, string imageID, string kernelImageID, string multiCloudImageID, string ramdiskImageID, List<string> securityGroupIDs, string serverTemplateID, string sshKeyID, string userData, string name, bool optimized, string state)
        {
            List<KeyValuePair<string, string>> retVal = new List<KeyValuePair<string, string>>();
            List<string> validArrayTypes = new List<string>() { "alert", "queue" };
            List<string> validStateValues = new List<string>() { "enabled", "disabled" };

            if (Utility.CheckStringInput("array_type", validArrayTypes, array_type))
            {
                Utility.addParameter(array_type, "server_array[array_type]", retVal);
            }

            //datacenter policy section
            if (dataCenterPolicy.Count > 1)
            {
                foreach (var dcp in dataCenterPolicy)
                {
                    Utility.addParameter(Utility.datacenterHref(dcp.cloudID, dcp.dataCenterId), "server_array[datacenter_policy][][datacenter_href]", retVal);
                    Utility.addParameter(dcp.max, "server_array[datacenter_policy][][max]", retVal);
                    Utility.addParameter(dcp.weight, "server_array[datacenter_policy][][weight]", retVal);
                }
            }
            
            Utility.addParameter(Utility.deploymentHref(deploymentID), "server_array[deployment_href", retVal);

            Utility.addParameter(description, "server_array[description]", retVal);

            if (elasticityParams != null && elasticityParams.Count > 0)
            {
                foreach (var ep in elasticityParams)
                {
                    if (ep.alert_specific_params != null)
                    {
                        Utility.addParameter(ep.alert_specific_params.decision_threshold, "server_array[elasticity_params][alert_specific_params][decision_threshold]", retVal);
                        Utility.addParameter(ep.alert_specific_params.voters_tag_predicate, "server_array[elasticity_params][alert_specific_params][voters_tag_predicate]", retVal);
                    }
                    else if (ep.queue_specific_params != null)
                    {
                        Utility.addParameter(ep.queue_specific_params.collect_audit_entries, "server_array[elasticity_params][queue_specific_params][collect_audit_entries]", retVal);
                        if (ep.queue_specific_params.item_age != null)
                        {
                            Utility.addParameter(ep.queue_specific_params.item_age.algorithm, "server_array[elasticity_params][queue_specific_params][item_age][algorithm]", retVal);
                            Utility.addParameter(ep.queue_specific_params.item_age.max_age, "server_array[elasticity_params][queue_specific_params][item_age][max_age]", retVal);
                            Utility.addParameter(ep.queue_specific_params.item_age.regexp, "server_array[elasticity_params][queue_specific_params][item_age][regexp]", retVal);
                        }

                        if (ep.queue_specific_params.queue_size != null)
                        {
                            Utility.addParameter(ep.queue_specific_params.queue_size.items_per_instance, "server_array[elasticity_params][queue_specific_params][queue_size][items_per_instance]", retVal);
                        }
                    }

                    if (ep.bounds != null)
                    {
                        Utility.addParameter(ep.bounds.max_count, "server_array[elasticity_params][bounds][max_count]", retVal);
                        Utility.addParameter(ep.bounds.min_count, "server_array[elasticity_params][bounds][min_count]", retVal);
                    }

                    if (ep.pacing != null)
                    {
                        Utility.addParameter(ep.pacing.resize_calm_time, "server_array[elasticity_params][pacing][resize_calm_time]", retVal);
                        Utility.addParameter(ep.pacing.resize_down_by, "server_array[elasticity_params][pacing][resize_down_by]", retVal);
                        Utility.addParameter(ep.pacing.resize_up_by, "server_array[elasticity_params][pacing][resize_up_by]", retVal);
                    }

                    if (ep.schedule_entries != null && ep.schedule_entries.Count > 0)
                    {
                        foreach (var se in ep.schedule_entries)
                        {
                            Utility.addParameter(se.day, "server_array[elasticity_params][schedule][][day]", retVal);
                            Utility.addParameter(se.max_count, "server_array[elasticity_params][schedule][][max_count]", retVal);
                            Utility.addParameter(se.min_count, "server_array[elasticity_params][schedule][][min_count]", retVal);
                            Utility.addParameter(se.time, "server_array[elasticity_params][schedule][][time]", retVal);
                        }
                    }
                }
            }
            
            Utility.addParameter(Utility.datacenterHref(cloudID, dataCenterID), "server_array[instance][datacenter_href]", retVal);

            Utility.addParameter(Utility.imageHref(cloudID, imageID), "server_array[instance][image_href]", retVal);

            if (inputs != null && inputs.Count > 0)
            {
                foreach (var kvp in inputs)
                {
                    Utility.addParameter(kvp.Key, "server_array[instance][inputs][][name]", retVal);
                    Utility.addParameter(kvp.Value, "server_array[instance][inputs][][value]", retVal);
                }
            }

            Utility.addParameter(Utility.instanceTypeHref(cloudID, instanceTypeID), "server_array[instance][instance_type_href]", retVal);
            
            Utility.addParameter(Utility.imageHref(cloudID, kernelImageID), "server_array[instance][kernel_image_href]", retVal);
            
            Utility.addParameter(Utility.multiCloudImageHref(multiCloudImageID), "server_array[instance][multi_cloud_image_href]", retVal);
            
            Utility.addParameter(Utility.imageHref(cloudID, ramdiskImageID), "server_array[instance][ramdisk_image_href]", retVal);
            
            if (securityGroupIDs != null && securityGroupIDs.Count > 0)
            {
                foreach(var sgid in securityGroupIDs)
                {
                    Utility.addParameter(Utility.securityGroupHref(cloudID, sgid), "server_array[instance][security_group_hrefs]", retVal);
                }
            }

            Utility.addParameter(Utility.serverTemplateHref(serverTemplateID), "server_array[instance][server_template_href]", retVal);

            Utility.addParameter(Utility.sshKeyHref(cloudID, sshKeyID), "server_array[instance][ssh_key_href]", retVal);

            Utility.addParameter(userData, "server_array[instance][user_data]", retVal);

            Utility.addParameter(name, "server_array[name]", retVal);

            Utility.addParameter(optimized.ToString().ToLower(), "server_array[optimized]", retVal);

            if (Utility.CheckStringInput("state", validStateValues, state))
            {
                Utility.addParameter(state, "server_array[state]", retVal);
            }

            return retVal;
        }

        #endregion

        #region ServerArray.update methods

        /// <summary>
        /// Method updates the properties of a given ServerArray within the context of a Deployment
        /// </summary>
        /// <param name="serverArrayID">ID of the ServerArray being updated</param>
        /// <param name="array_type">Type of array (alert/queue)</param>
        /// <param name="dataCenterPolicy">DataCenterPolicy object defining ServerArray launch target behavior</param>
        /// <param name="deploymentID">ID of deployment this ServerArray should be created in</param>
        /// <param name="description">Description of this ServerArray</param>
        /// <param name="elasticityParams">ElasticityParams object defininig how the ServerArray will grow under load/demand</param>
        /// <param name="cloudID">Id of Cloud this Array is being deployed to</param>
        /// <param name="dataCenterID">ID of DataCenter to be deployed in if multiple datacenters are not available</param>
        /// <param name="inputs">Collection of inputs to be passed to servers as they're being deployed</param>
        /// <param name="instanceTypeID">ID of InstanceType to be launched</param>
        /// <param name="imageID">ID of OS Image to be used</param>
        /// <param name="kernelImageID">ID of Kernel Image to be used</param>
        /// <param name="multiCloudImageID">ID of MultiCloud Image to be used</param>
        /// <param name="ramdiskImageID">ID of Ramdisk image to be used</param>
        /// <param name="securityGroupIDs">Collection of SecurityGroup IDs to be applied to each instance when launched</param>
        /// <param name="serverTemplateID">ID of ServerTemplate to be used when launching instances</param>
        /// <param name="sshKeyID">ID of SSH Key to be used when launching instances</param>
        /// <param name="userData">user data to be passed to each instance at launch time</param>
        /// <param name="name">name of the ServerArray</param>
        /// <param name="optimized">boolean indicating if this server is to utilized optimized IO features if available on the cloud being deployed to</param>
        /// <param name="state">State of the server (enabled/disabled)</param>
        /// <returns>True if updated, false if not</returns>
        public static bool update(string serverArrayID, string array_type, List<DataCenterPolicy> dataCenterPolicy, string deploymentID, string description, List<ElasticityParams> elasticityParams, string cloudID, string dataCenterID, List<KeyValuePair<string, string>> inputs, string instanceTypeID, string imageID, string kernelImageID, string multiCloudImageID, string ramdiskImageID, List<string> securityGroupIDs, string serverTemplateID, string sshKeyID, string userData, string name, bool optimized, string state)
        {
            string putHref = string.Format(APIHrefs.ServerArrayById, serverArrayID);
            return updatePut(putHref, array_type, dataCenterPolicy, deploymentID, description, elasticityParams, cloudID, dataCenterID, inputs, instanceTypeID, imageID, kernelImageID, multiCloudImageID, ramdiskImageID, securityGroupIDs, serverTemplateID, sshKeyID, userData, name, optimized, state);
        }

        /// <summary>
        /// Method updates the properties of a given ServerArray
        /// </summary>
        /// <param name="serverArrayID">ID of the ServerArray being updated</param>
        /// <param name="array_type">Type of array (alert/queue)</param>
        /// <param name="dataCenterPolicy">DataCenterPolicy object defining ServerArray launch target behavior</param>
        /// <param name="deploymentID">ID of deployment this ServerArray should be created in</param>
        /// <param name="description">Description of this ServerArray</param>
        /// <param name="elasticityParams">ElasticityParams object defininig how the ServerArray will grow under load/demand</param>
        /// <param name="cloudID">Id of Cloud this Array is being deployed to</param>
        /// <param name="dataCenterID">ID of DataCenter to be deployed in if multiple datacenters are not available</param>
        /// <param name="inputs">Collection of inputs to be passed to servers as they're being deployed</param>
        /// <param name="instanceTypeID">ID of InstanceType to be launched</param>
        /// <param name="imageID">ID of OS Image to be used</param>
        /// <param name="kernelImageID">ID of Kernel Image to be used</param>
        /// <param name="multiCloudImageID">ID of MultiCloud Image to be used</param>
        /// <param name="ramdiskImageID">ID of Ramdisk image to be used</param>
        /// <param name="securityGroupIDs">Collection of SecurityGroup IDs to be applied to each instance when launched</param>
        /// <param name="serverTemplateID">ID of ServerTemplate to be used when launching instances</param>
        /// <param name="sshKeyID">ID of SSH Key to be used when launching instances</param>
        /// <param name="userData">user data to be passed to each instance at launch time</param>
        /// <param name="name">name of the ServerArray</param>
        /// <param name="optimized">boolean indicating if this server is to utilized optimized IO features if available on the cloud being deployed to</param>
        /// <param name="state">State of the server (enabled/disabled)</param>
        /// <returns>True if updated, false if not</returns>
        public static bool update_deployment(string serverArrayID, string array_type, List<DataCenterPolicy> dataCenterPolicy, string deploymentID, string description, List<ElasticityParams> elasticityParams, string cloudID, string dataCenterID, List<KeyValuePair<string, string>> inputs, string instanceTypeID, string imageID, string kernelImageID, string multiCloudImageID, string ramdiskImageID, List<string> securityGroupIDs, string serverTemplateID, string sshKeyID, string userData, string name, bool optimized, string state)
        {
            string putHref = string.Format(APIHrefs.DeploymentServerArrayByID, deploymentID, serverArrayID);
            return updatePut(putHref, array_type, dataCenterPolicy, deploymentID, description, elasticityParams, cloudID, dataCenterID, inputs, instanceTypeID, imageID, kernelImageID, multiCloudImageID, ramdiskImageID, securityGroupIDs, serverTemplateID, sshKeyID, userData, name, optimized, state);
        }

        /// <summary>
        /// Private method centralizes logic for managing ServerArray update calls
        /// </summary>
        /// <param name="putHref">href fragment to put data to</param>
        /// <param name="array_type">Type of array (alert/queue)</param>
        /// <param name="dataCenterPolicy">DataCenterPolicy object defining ServerArray launch target behavior</param>
        /// <param name="deploymentID">ID of deployment this ServerArray should be created in</param>
        /// <param name="description">Description of this ServerArray</param>
        /// <param name="elasticityParams">ElasticityParams object defininig how the ServerArray will grow under load/demand</param>
        /// <param name="cloudID">Id of Cloud this Array is being deployed to</param>
        /// <param name="dataCenterID">ID of DataCenter to be deployed in if multiple datacenters are not available</param>
        /// <param name="inputs">Collection of inputs to be passed to servers as they're being deployed</param>
        /// <param name="instanceTypeID">ID of InstanceType to be launched</param>
        /// <param name="imageID">ID of OS Image to be used</param>
        /// <param name="kernelImageID">ID of Kernel Image to be used</param>
        /// <param name="multiCloudImageID">ID of MultiCloud Image to be used</param>
        /// <param name="ramdiskImageID">ID of Ramdisk image to be used</param>
        /// <param name="securityGroupIDs">Collection of SecurityGroup IDs to be applied to each instance when launched</param>
        /// <param name="serverTemplateID">ID of ServerTemplate to be used when launching instances</param>
        /// <param name="sshKeyID">ID of SSH Key to be used when launching instances</param>
        /// <param name="userData">user data to be passed to each instance at launch time</param>
        /// <param name="name">name of the ServerArray</param>
        /// <param name="optimized">boolean indicating if this server is to utilized optimized IO features if available on the cloud being deployed to</param>
        /// <param name="state">State of the server (enabled/disabled)</param>
        /// <returns>True if updated, false if not</returns>
        private static bool updatePut(string putHref, string array_type, List<DataCenterPolicy> dataCenterPolicy, string deploymentID, string description, List<ElasticityParams> elasticityParams, string cloudID, string dataCenterID, List<KeyValuePair<string, string>> inputs, string instanceTypeID, string imageID, string kernelImageID, string multiCloudImageID, string ramdiskImageID, List<string> securityGroupIDs, string serverTemplateID, string sshKeyID, string userData, string name, bool optimized, string state)
        {
            List<KeyValuePair<string, string>> putParams = serverArrayParams(array_type, dataCenterPolicy, deploymentID, description, elasticityParams, cloudID, dataCenterID, inputs, instanceTypeID, imageID, kernelImageID, multiCloudImageID, ramdiskImageID, securityGroupIDs, serverTemplateID, sshKeyID, userData, name, optimized, state);
            return Core.APIClient.Instance.Put(putHref, putParams);
        }

        #endregion

        #region ServerArray.clone methods

        /// <summary>
        /// Clones a given ServerArray 
        /// </summary>
        /// <param name="serverArrayID">ID of the ServerArray to clone</param>
        /// <returns>ID of the newly created ServerArray</returns>
        public static string clone(string serverArrayID)
        {
            string postHref = string.Format(APIHrefs.ServerArrayClone, serverArrayID);
            string retVal = string.Empty;
            Core.APIClient.Instance.Post(postHref, "location", out retVal);
            return retVal;
        }

        #endregion

        #region ServerArray.current_instances methods

        /// <summary>
        /// Method returns a list of current instances within a given ServerArray based on the given filters
        /// </summary>
        /// <param name="serverArrayID">ID of the ServerArray to show current instances for</param>
        /// <param name="filter">set of filters defining which instances to include or exclude</param>
        /// <param name="view">view specifying the level of detail to return</param>
        /// <returns>List of instances in the given ServerArray</returns>
        public static List<Instance> current_instances(string serverArrayID, List<Filter> filter, string view)
        {
            return Instance.index_serverArray(serverArrayID, filter, view);
        }

        /// <summary>
        /// Method returns a list of current instances within a given ServerArray based on the given filters
        /// </summary>
        /// <param name="serverArrayID">ID of the ServerArray to show current instances for</param>
        /// <param name="filter">set of filters defining which instances to include or exclude</param>
        /// <returns>List of instances in the given ServerArray</returns>
        public static List<Instance> current_instances(string serverArrayID, List<Filter> filter)
        {
            return Instance.index_serverArray(serverArrayID, filter);
        }

        /// <summary>
        /// Method returns a list of current instances within a given ServerArray
        /// </summary>
        /// <param name="serverArrayID">ID of the ServerArray to show current instances for</param>
        /// <param name="view">view specifying the level of detail to return</param>
        /// <returns>List of instances in the given ServerArray</returns>
        public static List<Instance> current_instances(string serverArrayID, string view)
        {
            return Instance.index_serverArray(serverArrayID, view);
        }

        /// <summary>
        /// Method returns a list of current instances within a given ServerArray
        /// </summary>
        /// <param name="serverArrayID">ID of the ServerArray to show current instances for</param>
        /// <returns>List of instances in the given ServerArray</returns>
        public static List<Instance> current_instances(string serverArrayID)
        {
            return Instance.index_serverArray(serverArrayID);
        }

        #endregion

        #region ServerArray.destroy methods

        /// <summary>
        /// Destroys/deletes a given ServerArray
        /// </summary>
        /// <param name="serverArrayID">ID of the ServerArray to destroy/delete</param>
        /// <returns>true if successful, false if not</returns>
        public static bool destroy(string serverArrayID)
        {
            string deleteHref = string.Format(APIHrefs.ServerArrayDestroy, serverArrayID);
            return Core.APIClient.Instance.Delete(deleteHref);
        }

        #endregion

        #region ServerArray.launch methods

        /// <summary>
        /// Launches a new instance within the specified ServerArray
        /// </summary>
        /// <param name="serverArrayID">ServerArray ID</param>
        /// <returns>Instance ID of the newly created Instance</returns>
        public static string launch(string serverArrayID)
        {
            return Instance.launch_serverArray(serverArrayID);
        }

        /// <summary>
        /// Launches a new instance within the specified ServerArray
        /// </summary>
        /// <param name="serverArrayID">ServerArray ID</param>
        /// <param name="inputs">Hashtable of inputs</param>
        /// <returns>Instance ID of the newly created Instance</returns>
        public static string launch(string serverArrayID, Hashtable inputs)
        {
            return Instance.launch_serverArray(serverArrayID, inputs);
        }

        /// <summary>
        /// Launches a new instance within the specified ServerArray
        /// </summary>
        /// <param name="serverArrayID">ServerArray ID</param>
        /// <param name="inputs">List of KeyValuePairs of inputs</param>
        /// <returns>Instance ID of the newly created Instance</returns>
        public static string launch(string serverArrayID, List<KeyValuePair<string, string>> inputs)
        {
            return Instance.launch_serverArray(serverArrayID, inputs);
        }

        #endregion

        #region ServerArray.multi_run_executable methods

        /// <summary>
        /// Run an executable on all instances of this array
        /// </summary>
        /// <param name="serverArrayID">ID of the ServerArray</param>
        /// <param name="ignoreLock">Boolean indicating whether locks will be ignored or not</param>
        /// <param name="inputs">collection of inputs for this execution</param>
        /// <param name="recipeName">name of recipe to execute</param>
        /// <param name="rightScriptID">ID of RightScript to execute</param>
        /// <returns>List of Tasks reporting status of execution process</returns>
        public static List<Task> multi_run_executable(string serverArrayID, bool ignoreLock, List<KeyValuePair<string,string>> inputs, string recipeName, string rightScriptID)
        {
            return Instance.multi_run_executableServerArray(serverArrayID, ignoreLock, inputs, recipeName, rightScriptID);
        }

        #endregion

        #region ServerArray.multi_terminate methods

        /// <summary>
        /// Terminate all instances of this array.
        /// </summary>
        /// <param name="serverArrayID">ID of the ServerArray whose instances will be terminated</param>
        /// <param name="terminateAll">boolean confirming that all instances will be terminated</param>
        /// <returns>List of Tasks to report terminate progress on</returns>
        public static List<Task> multi_terminate(string serverArrayID, bool terminateAll)
        {
            return Instance.multi_terminateServerArray(serverArrayID, terminateAll, new List<Filter>());
        }

        /// <summary>
        /// Terminate all instances of this array within the given filters.
        /// </summary>
        /// <param name="serverArrayID">ID of the ServerArray whose instances will be terminated</param>
        /// <param name="terminateAll">boolean confirming that all instances will be terminated</param>
        /// <param name="filters">set of filters limiting which instances are to be terminated</param>
        /// <returns>List of Tasks to report terminate progress on</returns>
        public static List<Task> multi_terminate(string serverArrayID, bool terminateAll, List<Filter> filters)
        {
            return Instance.multi_terminateServerArray(serverArrayID, terminateAll, filters);
        }

        #endregion

    }
}
