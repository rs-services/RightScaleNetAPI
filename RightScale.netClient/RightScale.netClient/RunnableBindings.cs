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
    public class RunnableBindings :Core.RightScaleObjectBase<RunnableBindings>
    {

            public string recipe { get; set; }
            public List<object> actions { get; set; }
            public List<Link> links { get; set; }
            public int position { get; set; }
            public string sequence { get; set; }
            public string bindingid { get; set; }
            public RightScript right_script { get; set; }

        public class Link
        {
            public string rel { get; set; }
            public string href { get; set; }
        }

        public class RightScript
        {
            public string lineage { get; set; }
            public int revision { get; set; }
            public string name { get; set; }
            public string bindingscriptid { get; set; }
        }

        #region RunnableBinding.ctor
        public RunnableBindings()
            : base()
        {
        }

        public RunnableBindings(string bindingID, string inputValue):base()
        {
            this.bindingid = bindingID;
            //this.value = inputValue;
        }
        
        #endregion
		
        #region RunnableBindings.index methods

        //public static List<Input> index()
        //{
        //   return index(null,null);
        //}

        public static List<RunnableBindings> index_servertemplate(string serverTemplatetId, string view)
        {
            string getURL = string.Format(APIHrefs.ServerTemplateRunnableBindings, serverTemplatetId);
     

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

    }
}
