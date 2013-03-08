using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Deployment : Core.RightScaleObjectBase<Deployment>
    {
        public string name { get; set; }
        public string server_tag_scope { get; set; }
        public List<Input> inputs { get; set; }
        public string description { get; set; }

        public string DeploymentID
        {
            get
            {
                return getLinkIDValue("self");
            }
        }


        #region Deployment.ctor
        /// <summary>
        /// Default Constructor for Deployment
        /// </summary>
        public Deployment()
            : base()
        {
        }

        /// <summary>
        /// Constructor for Deployment object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public Deployment(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for Deployment object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public Deployment(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion

        /// <summary>
        /// Method gets the list of servers that belong to this specific deployment
        /// </summary>
        /// <returns>collection of servers that belong to this deployment</returns>
        public List<Server> getServers()
        {
            return Server.index_deployment(this.DeploymentID);
        }

        #region RSAPI Static Implementation

        #region Deployment.index methods

        /// <summary>
        /// Lists deployments of the account.
        /// Using the available filters, one can select or group which deployments to retrieve. The 'inputs20' view is for retrieving inputs in 2.0 serialization 
        /// </summary>
        /// <returns>collection of Deployment objects</returns>
        public static List<Deployment> index()
        {
            return index(null, null);
        }

        /// <summary>
        /// Lists deployments of the account.
        /// Using the available filters, one can select or group which deployments to retrieve. The 'inputs20' view is for retrieving inputs in 2.0 serialization 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>collection of Deployment objects</returns>
        public static List<Deployment> index(List<Filter> filter)
        {
            return index(filter, null);
        }

        /// <summary>
        /// Lists deployments of the account.
        /// Using the available filters, one can select or group which deployments to retrieve. The 'inputs20' view is for retrieving inputs in 2.0 serialization 
        /// </summary>
        /// <param name="view"></param>
        /// <returns>collection of Deployment objects</returns>
        public static List<Deployment> index(string view)
        {
            return index(null, view);
        }

        /// <summary>
        /// Lists deployments of the account.
        /// Using the available filters, one can select or group which deployments to retrieve. The 'inputs20' view is for retrieving inputs in 2.0 serialization 
        /// </summary>
        /// <param name="filter">list of KeyValuePair(string,string) to use as filters to query for deployments</param>
        /// <param name="view">name of the view to be returned</param>
        /// <returns>collection of Deployment objects</returns>
        public static List<Deployment> index(List<Filter> filter, string view)
        {
            string getHref = "/api/deployments";

            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default", "inputs", "inputs_2_0" };
                Utility.CheckStringInput("view", validViews, view);
            }

            List<string> validFilters = new List<string>() { "description", "name", "server_tag_scope" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            string queryStringValue = string.Empty;
            queryStringValue += string.Format("view={0}&", view);
            queryStringValue += Utility.BuildFilterString(filter);

            string jsonString = Core.APIClient.Instance.Get(getHref, queryStringValue);
            return deserializeList(jsonString);
        }
        #endregion

        #region Deployment.show

        /// <summary>
        /// Lists the attributes of a given deployment
        /// </summary>
        /// <param name="deploymentID">ID of the deployment to show</param>
        /// <returns>Deployment specified by ID</returns>
        public static Deployment show(string deploymentID)
        {
            return show(deploymentID, "default");
        }

        /// <summary>
        /// Lists the attributes of a given deployment
        /// </summary>
        /// <param name="deploymentID">ID of the deployment to show</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>Deployment specified by ID</returns>
        public static Deployment show(string deploymentID, string view)
        {
            string getHref = string.Format("/api/deployments/{0}", deploymentID);
            string queryString = string.Empty;
            if (!string.IsNullOrWhiteSpace(view))
            {
                List<string> validViews = new List<string>() { "default", "inputs", "inputs_2_0" };
                Utility.CheckStringInput("view", validViews, view);
                queryString += string.Format("view={0}", view);
            }
            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserialize(jsonString);

        }

        #endregion

        #region Deployment.create

        /// <summary>
        /// Creates a new deployment with the given parameters
        /// </summary>
        /// <param name="name">The name of the deployment to be created</param>
        /// <returns>ID of the deployment created</returns>
        public static string create(string name)
        {
            return create(name, string.Empty, string.Empty);
        }

        /// <summary>
        /// Creates a new deployment with the given parameters
        /// </summary>
        /// <param name="name">The name of the deployment to be created.</param>
        /// <param name="description">The description of the deployment to be created.</param>
        /// <param name="server_tag_scope">The routing scope for tags for servers in the deployment.</param>
        /// <returns>ID of the deployment created</returns>
        public static string create(string name, string description, string server_tag_scope)
        {
            string postHref = "/api/deployments";

            if (string.IsNullOrWhiteSpace(server_tag_scope))
            {
                server_tag_scope = "deployment";
            }

            List<KeyValuePair<string, string>> apiParams = new List<KeyValuePair<string, string>>();
            
            if (!string.IsNullOrWhiteSpace(description))
            {
                apiParams.Add(new KeyValuePair<string, string>("deployment[description]", description));
            }
            apiParams.Add(new KeyValuePair<string, string>("deployment[name]", name));
            apiParams.Add(new KeyValuePair<string, string>("deployment[server_tag_scope]", server_tag_scope));

            List<string> locations = Core.APIClient.Instance.Create(postHref, apiParams, "location");
            return locations.Last<string>().Split('/').Last<string>();
        }

        #endregion

        #region Deployment.update

        /// <summary>
        /// Updates attributes of a given deployment.
        /// </summary>
        /// <param name="deploymentID">ID of the deployment to update</param>
        /// <param name="name">The updated name for the deployment</param>
        /// <param name="description">The updated description for the deployment</param>
        /// <param name="server_tag_scope">The routing scope for tags for servers in the deployment</param>
        /// <returns>True if update successful, false if not</returns>
        public static bool update(string deploymentID, string name, string description, string server_tag_scope)
        {
            Utility.CheckStringHasValue(deploymentID);
            string putHref = string.Format("/api/deployments/{0}", deploymentID);

            List<KeyValuePair<string, string>> updateParams = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrWhiteSpace(name))
            {
                updateParams.Add(new KeyValuePair<string, string>("deployment[name]", name));
            }
            if (!string.IsNullOrWhiteSpace(description))
            {
                updateParams.Add(new KeyValuePair<string, string>("deployment[description]", description));
            }
            if (!string.IsNullOrWhiteSpace(server_tag_scope))
            {
                List<string> validValues = new List<string>() { "deployment", "account" };
                Utility.CheckStringInput("server_tag_scope", validValues, server_tag_scope);
                updateParams.Add(new KeyValuePair<string, string>("deployment[server_tag_scope]", server_tag_scope));
            }
            if (updateParams.Count > 0)
            {
                return Core.APIClient.Instance.Put(putHref, updateParams);
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Deployment.clone
        /// <summary>
        /// Clones a given Deployment
        /// </summary>
        /// <param name="deploymentID">ID of the deployment to clone</param>
        /// <returns>ID of the newly created deployment</returns>
        public static string clone(string deploymentID)
        {
            string postHref = string.Format("/api/deployments/{0}/clone", deploymentID);
            List<string> locationHrefs = Core.APIClient.Instance.Create(postHref, null, "location");
            return locationHrefs.Last<string>().Split('/').Last<string>();
        }

        #endregion

        #region Deployment.destroy
        /// <summary>
        /// Deletes a given deployment
        /// </summary>
        /// <param name="deploymentID">ID of the deployment to destroy</param>
        /// <returns>true if successful, false if not</returns>
        public static bool destroy(string deploymentID)
        {
            string deleteHref = string.Format("/api/deployments/{0}", deploymentID);
            return Core.APIClient.Instance.Delete(deleteHref);
        }

        #endregion

        #region Deployment.servers

        /// <summary>
        /// Lists the servers belonging to this deployment.
        /// </summary>
        /// <param name="deploymentID">ID of deployment to retrieve servers for</param>
        /// <returns>List of server objects belonging to the specified Deployment</returns>
        public static List<Server> servers(string deploymentID)
        {
            return Server.index_deployment(deploymentID);
        }

        #endregion

        #endregion
    }
}
