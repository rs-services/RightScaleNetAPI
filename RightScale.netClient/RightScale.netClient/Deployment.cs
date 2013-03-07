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
        public static List<Deployment> index(List<KeyValuePair<string, string>> filter)
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
        public static List<Deployment> index(List<KeyValuePair<string, string>> filter, string view)
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
    }
}
