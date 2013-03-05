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
        public List<Action> actions { get; set; }
        public string created_at { get; set; }
        public NextInstance next_instance { get; set; }
        public string updated_at { get; set; }
        public CurrentInstance current_instance { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }
        public string state { get; set; }

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

        public static string create_deployment(string deploymentID, string cloudID, string serverTemplateID, string serverName)
        {
            string postHref = string.Format("/api/deployments/{0}/servers");
            throw new NotImplementedException();
        }

        public static string create(string cloudID, string serverTemplateID, string serverName)
        {
            string postHref = "/api/servers";
            throw new NotImplementedException
        }

        #endregion
    }
}
