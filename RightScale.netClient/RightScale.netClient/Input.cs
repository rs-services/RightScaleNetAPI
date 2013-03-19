using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// Inputs help extract dynamic information, usually specified at runtime, from repeatable configuration operations that can be codified. Inputs are variables defined in and used by RightScripts/Recipes. The two main attributes of an input are 'name' and 'value'. The 'name' identifies the input and the 'value', although a string encodes what type it is. It could be a text encoded as 'text:myvalue' or a credential encoded as 'cred:MY_CRED' or a key etc. Please see support.rightscale.com for more info on input hierarchies and their different types.
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeInput.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceInputs.html
    /// </summary>
    public class Input :Core.RightScaleObjectBase<Input>
    {
        /// <summary>
        /// Name of this input
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Value of this input
        /// </summary>
        public string value { get; set; }

        #region Input.ctor
        /// <summary>
        /// Default Constructor for Input
        /// </summary>
        public Input()
            : base()
        {
        }

        public Input(string inputName, string inputValue):base()
        {
            this.name = inputName;
            this.value = inputValue;
        }

        /// <summary>
        /// Constructor for Input object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public Input(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for Input object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public Input(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		
        #region Input.index methods

        //public static List<Input> index()
        //{
        //   return index(null,null);
        //}

        public static List<Input> index_deployment(string deploymentid, string view)
        {
            string getURL = string.Format(APIHrefs.DeploymentInput, deploymentid);

            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default", "inputs_2_0" };
                Utility.CheckStringInput("view", validViews, view);
            }

            string queryString = string.Empty;

            if (!string.IsNullOrWhiteSpace(view))
            {
                queryString += string.Format("view={0}", view);
            }

            string jsonString = Core.APIClient.Instance.Get(getURL, queryString);

            return deserializeList(jsonString);
        }

        public static List<Input> index_servertemplate(string servertemplateid, string view)
        {
            string getURL = string.Format(APIHrefs.ServerTemplateInput, servertemplateid);

            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default", "inputs_2_0" };
                Utility.CheckStringInput("view", validViews, view);
            }

            string queryString = string.Empty;

            if (!string.IsNullOrWhiteSpace(view))
            {
                queryString += string.Format("view={0}", view);
            }

            string jsonString = Core.APIClient.Instance.Get(getURL, queryString);

            return deserializeList(jsonString);
        }
        #endregion

        //TODO:  Add multi_update method
		
    }
}
