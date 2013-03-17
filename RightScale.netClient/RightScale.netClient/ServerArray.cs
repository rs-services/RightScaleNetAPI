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


        private static ServerArray createPost(string postHref, string array_type, List<DataCenterPolicy> dataCenterPolicy, string deploymentID, string description, List<ElasticityParams> elasticityParams, string cloudID, string dataCenterID, List<Input> inputs, string instanceTypeID, string kernelImageID, string multiCloudImageID, string ramdiskImageID, List<string> securityGroupIDs, string serverTemplateID, string sshKeyID, string userData, bool optimized, string state)
        {
            List<string> validStateValues = new List<string>() { "enabled", "disabled" };



            throw new NotImplementedException();
        }

        #endregion

        #region ServerArray.update methods

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

        

        #endregion

        #region ServerArray.multi_terminate methods
        
        

        #endregion

    }
}
