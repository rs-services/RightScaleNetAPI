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

        public static List<Input> index_instance(string cloudid, string instanceid, string view)
        {
            string getURL = string.Format(APIHrefs.InstanceInput, cloudid, instanceid);

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

        #region Input.multi_update methods

        /// <summary>
        /// Multi_update inputs for a given ServerTemplate
        /// </summary>
        /// <param name="serverTemplateID">ID of ServerTemplate whose inputs should be updated</param>
        /// <param name="inputs">collection of inputs to update</param>
        /// <returns>true if successful, false if not</returns>
        public static bool multi_update_serverTemplate(string serverTemplateID, List<Input> inputs)
        {
            string putHref = string.Format(APIHrefs.ServerTemplateInputMultiUpdate, serverTemplateID);
            return multi_updatePut(putHref, inputs);
        }

        /// <summary>
        /// Multi_update inputs for a given Deployment
        /// </summary>
        /// <param name="deploymentid">ID of Deployment whose inputs should be updated</param>
        /// <param name="inputs">collection of inputs to update</param>
        /// <returns>true if successful, false if not</returns>
        public static bool multi_update_deployment(string deploymentid, List<Input> inputs)
        {
            string putHref = string.Format(APIHrefs.DeploymentInputMultiUpdate, deploymentid);
            return multi_updatePut(putHref, inputs);
        }

        /// <summary>
        /// Multi_update inputs for a given Instance
        /// </summary>
        /// <param name="cloudID">CloudId where instance is located</param>
        /// <param name="instanceID">ID of instance whose inputs should be updated</param>
        /// <param name="inputs">collection of inputs to update</param>
        /// <returns>true if successful, false if not</returns>
        public static bool multi_update_instance(string cloudID, string instanceID, List<Input> inputs)
        {
            string putHref = string.Format(APIHrefs.InstanceInputMultiUpdate, cloudID, instanceID);
            return multi_updatePut(putHref, inputs);
        }

        /// <summary>
        /// Private multi update method to centralize logic of handling input mutli_update
        /// </summary>
        /// <param name="putHref">href to put to</param>
        /// <param name="inputs">collection of inputs to update</param>
        /// <returns>true if successful, false if not</returns>
        private static bool multi_updatePut(string putHref, List<Input> inputs)
        {
            List<KeyValuePair<string, string>> putParams = Utility.FormatInputCollection(inputs);
            return Core.APIClient.Instance.Put(putHref, putParams);
        }

        #endregion

    }
}
