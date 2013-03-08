using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// Servers represent the notion of a server/machine from the RightScale's perspective. A Server, does not always have a corresponding VM running or provisioned in a cloud. Some clouds use the word "servers" to refer to created VM's. These allocated VM's are not called Servers in the RightScale API, they are called Instances.
    /// A Server always has a next_instance association, which will define the configuration to apply to a new instance when the server is launched or started (starting servers is not yet supported through this API). Once a Server is launched/started a currentinstance relationship will exist. Accessing the currentinstance of a server results in immediate runtime modification of this running server. Changes to the next_instance association prepares the configuration for the next instance launch/start (therefore they have no effect until such operation is performed).
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeServer.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceServers.html
    /// </summary>
    public class Server:Core.RightScaleObjectBase<Server>
    {
        public string name { get; set; }
        public string created_at { get; set; }
        public Instance next_instance { get; set; }
        public string updated_at { get; set; }
        public Instance current_instance { get; set; }
        public string description { get; set; }
        public string state { get; set; }

        #region Get link ID public instance methods

        /// <summary>
        /// Instance ID of the next instance
        /// </summary>
        public string NextInstanceID
        {
            get
            {
                return getLinkIDValue("next_intance");
            }
        }

        /// <summary>
        /// Deployment ID of the deployment this server belongs to
        /// </summary>
        public string DeploymentID
        {
            get
            {
                return getLinkIDValue("deployment");
            }
        }

        #endregion

        #region Server.ctor()
        /// <summary>
        /// Default Constructor for Server
        /// </summary>
        public Server()
            : base()
        {

        }

        /// <summary>
        /// Constructor for Server object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public Server(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for Server object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public Server(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {

        }

        #endregion

        #region Server.index methods

        /// <summary>
        /// Lists servers within a Deployment. By using the available filters, it is possible to retrieve servers that have common characteristics. 
        /// </summary>
        /// <param name="deploymentID">ID of the deployment to index Server objects in</param>
        /// <returns>Collection of Server objects</returns>
        public static List<Server> index_deployment(string deploymentID)
        {
            return index_deployment(deploymentID, null, null);
        }

        /// <summary>
        /// Lists servers within a Deployment. By using the available filters, it is possible to retrieve servers that have common characteristics. 
        /// </summary>
        /// <param name="deploymentID">ID of the deployment to index Server objects in</param>
        /// <param name="filter"></param>
        /// <returns>Collection of Server objects</returns>
        public static List<Server> index_deployment(string deploymentID, List<KeyValuePair<string, string>> filter)
        {
            return index_deployment(deploymentID, filter, null);
        }

        /// <summary>
        /// Lists servers within a Deployment. By using the available filters, it is possible to retrieve servers that have common characteristics. 
        /// </summary>
        /// <param name="deploymentID">ID of the deployment to index Server objects in</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>Collection of Server objects</returns>
        public static List<Server> index_deployment(string deploymentID, string view)
        {
            return index_deployment(deploymentID, null, view);
        }

        /// <summary>
        /// Lists servers within a Deployment. By using the available filters, it is possible to retrieve servers that have common characteristics. 
        /// </summary>
        /// <param name="deploymentID">ID of the deployment to index Server objects in</param>
        /// <param name="filter"></param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>Collection of Server objects</returns>
        public static List<Server> index_deployment(string deploymentID, List<KeyValuePair<string, string>> filter, string view)
        {
            string getHref = string.Format("/api/deployments/{0}/servers", deploymentID);
            return indexGet(filter, view, getHref);
        }

        /// <summary>
        /// Lists servers. By using the available filters, it is possible to retrieve servers that have common characteristics. 
        /// </summary>
        /// <returns>Collection of Server objects</returns>
        public static List<Server> index()
        {
            return index(null, null);
        }

        /// <summary>
        /// Lists servers. By using the available filters, it is possible to retrieve servers that have common characteristics. 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>Collection of Server objects</returns>
        public static List<Server> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        /// <summary>
        /// Lists servers. By using the available filters, it is possible to retrieve servers that have common characteristics. 
        /// </summary>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>Collection of Server objects</returns>
        public static List<Server> index(string view)
        {
            return index(null, view);
        }

        /// <summary>
        /// Lists servers. By using the available filters, it is possible to retrieve servers that have common characteristics. 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>Collection of Server objects</returns>
        public static List<Server> index(List<KeyValuePair<string, string>> filter, string view)
        {
            string getHref = "/api/servers";
            return indexGet(filter, view, getHref);
        }

        /// <summary>
        /// Private static method responsible for calling http client and performing get for all index calls
        /// </summary>
        /// <param name="filter">list of keyvaluepairs for filtering get request</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <param name="getHref">API href for GET to be performed on</param>
        /// <returns>Collection of Server objects</returns>
        private static List<Server> indexGet(List<KeyValuePair<string, string>> filter, string view, string getHref)
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

            List<string> validFilters = new List<string>() { "cloud_href", "deployment_href", "name" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            string jsonString = Core.APIClient.Instance.Get(getHref);
            return deserializeList(jsonString);
        }
        #endregion

        #region Server.show methods

        /// <summary>
        /// Shows the information of a single server.
        /// </summary>
        /// <param name="serverID">ID of the server to be retrieved</param>
        /// <param name="deploymentID">Deployment ID of the server to be retrieved</param>
        /// <returns>Populated Server object</returns>
        public static Server show_deployment(string serverID, string deploymentID)
        {
            return show_deployment(serverID, deploymentID, "default");
        }

        /// <summary>
        /// Shows the information of a single server.
        /// </summary>
        /// <param name="serverid">ID of the server to be retrieved</param>
        /// <param name="deploymentID">Deployment ID of the server to be retrieved</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include.</param>
        /// <returns>Populated Server object</returns>
        public static Server show_deployment(string serverid, string deploymentID, string view)
        {
            string getHref = string.Format("/api/deployments/{0}/servers/{1}", deploymentID, serverid);
            return showGet(getHref, view);
        }

        /// <summary>
        /// Shows the information of a single server.
        /// </summary>
        /// <param name="serverID">ID of the server to be retrieved</param>
        /// <returns>Populated Server object</returns>
        public static Server show(string serverID)
        {
            return show(serverID, "default");
        }

        /// <summary>
        /// Shows the information of a single server.
        /// </summary>
        /// <param name="serverid">ID of the server to be retrieved</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include.</param>
        /// <returns>Populated Server object</returns>
        public static Server show(string serverid, string view)
        {
            string getHref = string.Format("/api/servers/{0}", serverid);
            return showGet(getHref, view);
        }

        /// <summary>
        /// Internal implementation of show for both deployment and non-deployment calls.  
        /// </summary>
        /// <param name="getHref"></param>
        /// <param name="view"></param>
        /// <returns>Server object with data</returns>
        private static Server showGet(string getHref, string view)
        {
            List<string> validViews = new List<string>() { "default", "instance_detail" };
            Utility.CheckStringInput("view", validViews, view);

            string queryString = string.Empty;

            if(!string.IsNullOrWhiteSpace(view))
            {
                queryString += string.Format("view={0}", view);
            }

            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserialize(jsonString);
        }

        #endregion

        #region Server.create methods

        /// <summary>
        /// Creates a new server and configures its corresponding "next" instane with the received parameters
        /// </summary>
        /// <param name="deploymentID">ID of the deployment which the server will be added</param>
        /// <param name="description">The Server Description</param>
        /// <param name="cloudID">ID of the cloud that the server should be added to</param>
        /// <param name="datacenterID">ID of the Datacenter/zone</param>
        /// <param name="imageID">ID of the image to use</param>
        /// <param name="inputs">collection of inputs in name/value format</param>
        /// <param name="instanceTypeID">ID of the instance type</param>
        /// <param name="kernelImageID">ID of the kernel image</param>
        /// <param name="multiCloudImageID">ID of the multiCloudImage to use</param>
        /// <param name="ramdiskImageID">ID of the ramdisk image</param>
        /// <param name="securityGroupIDs">collection of security group IDs</param>
        /// <param name="serverTemplateID">ID of the ServerTemplate</param>
        /// <param name="sshKeyID">ID of the SSH key to use</param>
        /// <param name="userData">USer data that RightScale automaticall passes to your instanece at boot time</param>
        /// <param name="name">The name of the server</param>
        /// <param name="optimized">A flag indicating whether instances of this Server should support optimized Volumes</param>
        /// <returns>ID of the newly created server</returns>
        public static string create_deployment(string deploymentID, string description, string cloudID, string datacenterID, string imageID, List<KeyValuePair<string, string>> inputs, string instanceTypeID, string kernelImageID, string multiCloudImageID, string ramdiskImageID, List<string> securityGroupIDs, string serverTemplateID, string sshKeyID, string userData, string name, bool optimized)
        {
            return create(cloudID, deploymentID, serverTemplateID, name, description, cloudID, datacenterID, imageID, inputs, instanceTypeID, kernelImageID, multiCloudImageID, ramdiskImageID, securityGroupIDs, sshKeyID, userData, optimized);
        }

        /// <summary>
        /// Creates a new server, and configures its corresponding "next" instance with only the required parameters.
        /// </summary>
        /// <param name="deploymentID"></param>
        /// <param name="cloudID"></param>
        /// <param name="serverTemplateID"></param>
        /// <param name="serverName"></param>
        /// <returns>ID of the newly created server</returns>
        public static string create_deployment(string deploymentID, string cloudID, string serverTemplateID, string serverName)
        {
            string postHref = string.Format("/api/deployments/{0}/servers", deploymentID);
            List<KeyValuePair<string, string>> parameters = createGetParameterSet(deploymentID, null, cloudID, null, null, null, null, null, null, null, null, serverTemplateID, null, null, serverName, false);
            return createPost(postHref, parameters);
        }

        /// <summary>
        /// Creates a new server and configures its corresponding "next" instane with the received parameters
        /// </summary>
        /// <param name="deploymentID">ID of the deployment which the server will be added</param>
        /// <param name="description">The Server Description</param>
        /// <param name="cloudID">ID of the cloud that the server should be added to</param>
        /// <param name="datacenterID">ID of the Datacenter/zone</param>
        /// <param name="imageID">ID of the image to use</param>
        /// <param name="inputs">collection of inputs in name/value format</param>
        /// <param name="instanceTypeID">ID of the instance type</param>
        /// <param name="kernelImageID">ID of the kernel image</param>
        /// <param name="multiCloudImageID">ID of the multiCloudImage to use</param>
        /// <param name="ramdiskImageID">ID of the ramdisk image</param>
        /// <param name="securityGroupIDs">collection of security group IDs</param>
        /// <param name="serverTemplateID">ID of the ServerTemplate</param>
        /// <param name="sshKeyID">ID of the SSH key to use</param>
        /// <param name="userData">USer data that RightScale automaticall passes to your instanece at boot time</param>
        /// <param name="name">The name of the server</param>
        /// <param name="optimized">A flag indicating whether instances of this Server should support optimized Volumes</param>
        /// <returns>ID of the newly created server</returns>
        public static string create(string cloudid, string deploymentID, string serverTemplateID, string serverName, string description, string cloudID, string datacenterID, string imageID, List<KeyValuePair<string, string>> inputs, string instanceTypeID, string kernelImageID, string multiCloudImageID, string ramdiskImageID, List<string> securityGroupIDs, string sshKeyID, string userData, bool optimized)
        {
            string postHref = "/api/servers";
            List<KeyValuePair<string, string>> parameters = createGetParameterSet(deploymentID, description, cloudID, datacenterID, imageID, inputs, instanceTypeID, kernelImageID, multiCloudImageID, ramdiskImageID, securityGroupIDs, serverTemplateID, sshKeyID, userData, serverName, optimized);
            return createPost(postHref, parameters);
        }

        /// <summary>
        /// Creates a new server, and configures its corresponding "next" instance with only the required parameters.
        /// </summary>
        /// <param name="cloudID">ID of the cloud that the server should be added to</param>
        /// <param name="serverTemplateID">ID of the SErverTemplate</param>
        /// <param name="serverName">The name of the server</param>
        /// <returns>ID of the newly created server</returns>
        public static string create(string cloudID, string deploymentID, string serverTemplateID, string serverName)
        {
            string postHref = "/api/servers";
            List<KeyValuePair<string, string>> parameters = createGetParameterSet(deploymentID, null, cloudID, null, null, null, null, null, null, null, null, serverTemplateID, null, null, serverName, false);
            return createPost(postHref, parameters);
        }

        /// <summary>
        /// Private method to centralize buildout of parameters to post to RightScale API when creating a server
        /// </summary>
        /// <param name="deploymentID">ID of the deployment which the server will be added</param>
        /// <param name="description">The Server Description</param>
        /// <param name="cloudID">ID of the cloud that the server should be added to</param>
        /// <param name="datacenterID">ID of the Datacenter/zone</param>
        /// <param name="imageID">ID of the image to use</param>
        /// <param name="inputs">collection of inputs in name/value format</param>
        /// <param name="instanceTypeID">ID of the instance type</param>
        /// <param name="kernelImageID">ID of the kernel image</param>
        /// <param name="multiCloudImageID">ID of the multiCloudImage to use</param>
        /// <param name="ramdiskImageID">ID of the ramdisk image</param>
        /// <param name="securityGroupIDs">collection of security group IDs</param>
        /// <param name="serverTemplateID">ID of the ServerTemplate</param>
        /// <param name="sshKeyID">ID of the SSH key to use</param>
        /// <param name="userData">USer data that RightScale automaticall passes to your instanece at boot time</param>
        /// <param name="name">The name of the server</param>
        /// <param name="optimized">A flag indicating whether instances of this Server should support optimized Volumes</param>
        /// <returns>Collection of parameters for post process to create server</returns>
        private static List<KeyValuePair<string,string>> createGetParameterSet(string deploymentid, string description, string cloudID, string datacenterID, string imageID, List<KeyValuePair<string, string>> inputs, string instanceTypeID, string kernelImageID, string multiCloudImageID, string ramdiskImageID, List<string> securityGroupIDs, string serverTemplateID, string sshKeyID, string userData, string name, bool optimized)
        {
            //check required inputs

            string errorString = string.Empty;

            if(string.IsNullOrWhiteSpace(cloudID))
            {
                errorString += "CloudID is a required input" + Environment.NewLine;
            }
            if(string.IsNullOrWhiteSpace(name))
            {
                errorString += "Name is a required input" + Environment.NewLine;
            }
            if(string.IsNullOrWhiteSpace(serverTemplateID))
            {
                errorString += "ServerTemplateID is a required input" + Environment.NewLine;
            }
            if (string.IsNullOrWhiteSpace(deploymentid))
            {
                errorString += "DeploymentID is a required input" + Environment.NewLine;
            }

            if(!string.IsNullOrWhiteSpace(errorString))
            {
                throw new ArgumentException("Errors were found when parsing inputs for Server.create() : " + Environment.NewLine + errorString);
            }
            
            //populate return value
            List<KeyValuePair<string, string>> retVal = new List<KeyValuePair<string, string>>();

            if (!string.IsNullOrWhiteSpace(deploymentid))
            {
                retVal.Add(new KeyValuePair<string, string>("server[deployment_href]", Utility.deploymentHref(deploymentid)));
            }
            if (!string.IsNullOrWhiteSpace(description))
            {
                retVal.Add(new KeyValuePair<string, string>("server[description]", description));
            }
            if(!string.IsNullOrWhiteSpace(cloudID))
            {
                retVal.Add(new KeyValuePair<string,string>("server[instance][cloud_href]", Utility.cloudHref(cloudID)));
            }
            if(!string.IsNullOrWhiteSpace(datacenterID))
            {
                retVal.Add(new KeyValuePair<string,string>("server[instance][datacenter_href]", Utility.datacenterHref(cloudID, datacenterID)));
            }
            if(!string.IsNullOrWhiteSpace(imageID))
            {
                retVal.Add(new KeyValuePair<string,string>("server[instance][image_href]", Utility.imageHref(cloudID, imageID)));
            }
            if(inputs != null && inputs.Count > 0)
            {
                foreach(KeyValuePair<string, string> kvp in inputs)
                {
                    retVal.Add(new KeyValuePair<string, string>("server[instance][inputs][][name]", kvp.Key));
                    retVal.Add(new KeyValuePair<string, string>("server[instance][inputs][][value]", kvp.Value));
                }
            }
            if(!string.IsNullOrWhiteSpace(instanceTypeID))
            {
                retVal.Add(new KeyValuePair<string, string>("server[instance][instance_type_href]", Utility.instanceTypeHref(cloudID, instanceTypeID)));
            }
            if(!string.IsNullOrWhiteSpace(kernelImageID))
            {
                retVal.Add(new KeyValuePair<string,string>("server[instance][kernel_image_href]", Utility.kernelImageHref(cloudID, kernelImageID)));
            }
            if(!string.IsNullOrWhiteSpace(multiCloudImageID))
            {
                retVal.Add(new KeyValuePair<string,string>("server[instance][multi_cloud_image_href]", Utility.multiCloudImageHref(multiCloudImageID)));
            }
            if(!string.IsNullOrWhiteSpace(ramdiskImageID))
            {
                retVal.Add(new KeyValuePair<string,string>("server[instance][ramdisk_image_href]", Utility.ramdiskImageHref(cloudID, ramdiskImageID)));
            }
            if(securityGroupIDs != null && securityGroupIDs.Count > 0)
            {
                foreach(string s in securityGroupIDs)
                {
                    retVal.Add(new KeyValuePair<string,string>("server[instance][security_group_hrefs]", Utility.securityGroupHref(cloudID, s)));
                }
            }
            if(!string.IsNullOrWhiteSpace(serverTemplateID))
            {
                retVal.Add(new KeyValuePair<string,string>("server[instance][server_template_href]", Utility.serverTemplateHref(serverTemplateID)));
            }
            if(!string.IsNullOrWhiteSpace(sshKeyID))
            {
                retVal.Add(new KeyValuePair<string, string>("server[instance][ssh_key_href]", Utility.sshKeyHref(cloudID, sshKeyID)));
            }
            if(!string.IsNullOrWhiteSpace(name))
            {
                retVal.Add(new KeyValuePair<string,string>("server[name]", name));
            }
            if(optimized)
            {
                retVal.Add(new KeyValuePair<string,string>("server[optimized]", "false"));
            }
            else
            {
                retVal.Add(new KeyValuePair<string,string>("server[optimized]", "true"));
            }
                return retVal;
        }

        /// <summary>
        /// Private method containing centralized process of creating a server via the variety of methods available
        /// </summary>
        /// <param name="postHref">RightScale API HREF to pust to</param>
        /// <param name="parameterSet">parameter set to be posted to the RS API</param>
        /// <returns></returns>
        private static string createPost(string postHref, List<KeyValuePair<string, string>> parameterSet)
        {
            List<string> resultSet = Core.APIClient.Instance.Create(postHref, parameterSet, "location");
            return resultSet[0].Split('/').Last<string>();
        }

        #endregion

        #region Server.update methods

        /// <summary>
        /// Updates attributes of a single server
        /// </summary>
        /// <param name="serverID">ID of the server to be updated</param>
        /// <param name="description">The Updated Description for the server</param>
        /// <param name="name">The updated Server name</param>
        /// <param name="optimized">A flag indicating whether Instances of this Server should support optimized Volumes (e.g. Volumes supporting a specified number of IOPS). Not supported in all Clouds.</param>
        /// <returns></returns>
        public static bool update(string serverID, string description, string name, bool optimized)
        {
            string putHref = string.Format("/api/servers/{0}", serverID);
            return updatePut(putHref, description, name, optimized);
        }

        /// <summary>
        /// Updates attributes of a single server
        /// </summary>
        /// <param name="serverID">ID of the server to be updated</param>
        /// <param name="deploymentID">ID of the deployment where the server to be updated can be found</param>
        /// <param name="description">The Updated Description for the server</param>
        /// <param name="name">The updated Server name</param>
        /// <param name="optimized">A flag indicating whether Instances of this Server should support optimized Volumes (e.g. Volumes supporting a specified number of IOPS). Not supported in all Clouds.</param>
        /// <returns></returns>
        public static bool update_deployment(string serverID, string deploymentID, string description, string name, bool optimized)
        {
            string putHref = string.Format("/api/deployments/{0}/servers/{1}", deploymentID, serverID);
            return updatePut(putHref, description, name, optimized);
        }

        /// <summary>
        /// Private method to centrally handle PUT calls to Rightscale API
        /// </summary>
        /// <param name="putHref">API href to all for action</param>
        /// <param name="description">The Updated Description for the server</param>
        /// <param name="name">The updated Server name</param>
        /// <param name="optimized">A flag indicating whether Instances of this Server should support optimized Volumes (e.g. Volumes supporting a specified number of IOPS). Not supported in all Clouds.</param>
        /// <returns></returns>
        private static bool updatePut(string putHref, string description, string name, bool optimized)
        {
            List<KeyValuePair<string, string>> paramSet = new List<KeyValuePair<string,string>>();
            if(!string.IsNullOrWhiteSpace(description))
            {
                paramSet.Add(new KeyValuePair<string, string>("server[description]", description));
            }
            if(!string.IsNullOrWhiteSpace(name))
            {
                paramSet.Add(new KeyValuePair<string,string>("server[name]", name));
            }
            if(optimized)
            {
                paramSet.Add(new KeyValuePair<string,string>("server[optimized]", "true"));
            }
            else
            {
                paramSet.Add(new KeyValuePair<string,string>("server[optimized]", "false"));
            }
            return Core.APIClient.Instance.Put(putHref, paramSet);
        }

        #endregion

        #region Server.clone methods

        /// <summary>
        /// Clones a given server
        /// </summary>
        /// <param name="serverID">ID of the server to be cloned</param>
        /// <returns>ID of the newly created server</returns>
        public static string clone(string serverID)
        {
            string postHref = string.Format("/api/servers/{0}/clone", serverID);
            List<string> createResults =  Core.APIClient.Instance.Create(postHref, new List<KeyValuePair<string, string>>(), "location");
            return createResults.Last<string>().Split('/').Last<string>();
        }

        #endregion

        #region Server.destroy methods

        /// <summary>
        /// Deletes a given server.
        /// </summary>
        /// <param name="serverID">ID of the server to delete</param>
        /// <param name="deploymentID">ID of deployment where server to delete can be found</param>
        /// <returns>true if success, false if not</returns>
        public static bool destroy_deployment(string serverID, string deploymentID)
        {
            string deleteHref = string.Format("/api/deployments/{0}/servers/{1}", deploymentID, serverID);
            return destroyDelete(deleteHref);
        }

        /// <summary>
        /// Deletes a given server.
        /// </summary>
        /// <param name="serverID">ID of the server to delete</param>
        /// <returns>true if success, false if not</returns>
        public static bool destroy(string serverID)
        {
            string deleteHref = string.Format("/api/servers/{0}", serverID);
            return destroyDelete(deleteHref);
        }

        /// <summary>
        /// Centralized method to manage api delete calls for destroy() method
        /// </summary>
        /// <param name="deleteHref">href to call delete on</param>
        /// <returns>true if success, false if not</returns>
        private static bool destroyDelete(string deleteHref)
        {
            return Core.APIClient.Instance.Delete(deleteHref);
        }

        #endregion

        #region Server.launch() methods

        /// <summary>
        /// Launches the "next" instance of this server. This function is equivalent to invoking the launch action on the URL of this servers next_instance. See Instances#launch for details
        /// </summary>
        /// <param name="serverID">ID of server whose next instance will be launched</param>
        /// <returns>True if success, false if not</returns>
        public static bool launch(string serverID)
        {
            return launch(serverID, new List<KeyValuePair<string, string>>());
        }

        /// <summary>
        /// Launches the "next" instance of this server. This function is equivalent to invoking the launch action on the URL of this servers next_instance. See Instances#launch for details
        /// </summary>
        /// <param name="serverID">ID of the server whose next instance will be launched</param>
        /// <param name="inputs">collection of inputs to be passed into the launch process</param>
        /// <returns>True if success, false if not</returns>
        public static bool launch(string serverID, List<KeyValuePair<string, string>> inputs)
        {
            string postHref = string.Format("/api/servers/{0}/launch", serverID);
            return Core.APIClient.Instance.Post(postHref, inputs);
        }

        #endregion

        #region Server.terminate() methods

        /// <summary>
        /// Terminates the current instance of this server. This function is equivalent to invoking the terminate action on the URL of this servers current_instance.
        /// </summary>
        /// <param name="serverID">ID of the server whose current_instance will be terminated</param>
        /// <returns>true if success, false if not</returns>
        public static bool terminate(string serverID)
        {
            string postHref = string.Format("/api/servers/{0}/terminate", serverID);
            return Core.APIClient.Instance.Post(postHref);
        }
        
        #endregion
    }
}
