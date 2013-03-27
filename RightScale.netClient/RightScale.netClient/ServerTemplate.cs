using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// erverTemplates allow you to pre-configure servers by starting from a base image and adding scripts that run during the boot, operational, and shutdown phases. A ServerTemplate is a description of how a new instance will be configured when it is provisioned by your cloud provider.
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeServerTemplate.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceServerTemplates.html
    /// </summary>
    public class ServerTemplate:Core.TaggableResourceBase<ServerTemplate>
    {
        #region ServerTemplate properties

        /// <summary>
        /// Name of this ServerTemplate
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Collection of inputs for this ServerTemplate
        /// </summary>
        public List<Input> inputs { get; set; }

        /// <summary>
        /// Revision ID for this instance of a ServerTempalte
        /// </summary>
        public int revision { get; set; }

        /// <summary>
        /// Description of this SeverTemplate
        /// </summary>
        public string description { get; set; }

        #endregion

        #region ServerTemplate Relationships

        /// <summary>
        /// Associated MultiCloud Images
        /// </summary>
        public List<MultiCloudImage> multiCloudImages
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("multi_cloud_images"));
                return MultiCloudImage.deserializeList(jsonString);
            }
        }

        /// <summary>
        /// Associated AlertSpecs
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
        /// Associated privately shared Publication
        /// </summary>
        public Publication publication
        {
            get
            {
                //TODO: test ServerTemplate.Publication to make sure this shouldn't return a list
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("publication"));
                return Publication.deserialize(jsonString);
            }
        }

        /// <summary>
        /// Default MultiCloudImage associated with this ServerTemplate
        /// </summary>
        public MultiCloudImage defaultMultiCloudImage
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("default_multi_cloud_image"));
                return MultiCloudImage.deserialize(jsonString);
            }
        }

        #endregion

        #region ServerTemplate.ctor
        /// <summary>
        /// Default Constructor for ServerTemplate
        /// </summary>
        public ServerTemplate()
            : base()
        {
        }

        #endregion
        
        #region ServerTemplate.index methods

        public static List<ServerTemplate> index()
        {
            return index(new List<Filter>(), null);
        }

        public static List<ServerTemplate> index(List<Filter> filter)
        {
            return index(filter, null);
        }

        public static List<ServerTemplate> index(string view)
        {
            return index(new List<Filter>(), view);
        }

        public static List<ServerTemplate> index(string filterlist, string view)
        {
            List<Filter> filter = Filter.parseFilterList(view);

            return index(filter, view);
        }

        public static List<ServerTemplate> index(List<Filter> filter, string view)
        {
            string getUrl = APIHrefs.ServerTemplate ;
            string queryString = string.Empty;

            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default", "inputs", "inputs_2_0" };
                Utility.CheckStringInput("view", validViews, view);
            }

            List<string> validFilters = new List<string>() { "description", "multi_cloud_image_href", "name", "revision" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            string jsonString = Core.APIClient.Instance.Get(getUrl, queryString);

            return deserializeList(jsonString);
        }
        #endregion

        #region ServerTemplate.show methods

        /// <summary>
        /// Shows the information of a single image.
        /// </summary>
        /// <param name="serverid">ID of the image to be retrieved</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include.</param>
        /// <returns>Populated Image object</returns>
        public static ServerTemplate show(string servertemplateid, string view)
        {
            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default" };
                Utility.CheckStringInput("view", validViews, view);
            }

            string getHref = string.Format(APIHrefs.ServerTemplateByID, servertemplateid);
            return showGet(getHref, view);
        }

        /// <summary>
        /// Internal implementation of show for both deployment and non-deployment calls.  
        /// </summary>
        /// <param name="getHref"></param>
        /// <param name="view"></param>
        /// <returns>ServerTemplate object with data</returns>
        private static ServerTemplate showGet(string getHref, string view)
        {

            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }

            List<string> validViews = new List<string>() { "default", "inputs", "inputs_2_0" };
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

        #region ServerTemplate.clone methods

        /// <summary>
        /// Clones a given servertemplate
        /// </summary>
        /// <param name="serverID">ID of the servertemplate to be cloned</param>
        /// <returns>ID of the newly created servertemplate</returns>
        public static string clone(string servertemplateID, string name)
        {
            return clone(servertemplateID, name, string.Empty);
        }

        /// <summary>
        /// Clones a given servertemplate and assigns a new name and description as specified
        /// </summary>
        /// <param name="serverID">ID of the servertemplate to be cloned</param>
        /// <param name="name">Name of the new ServerTemplate</param>
        /// <param name="description">Description of the new ServerTemplate</param>
        /// <returns>ID of the newly created servertemplate</returns>
        public static string clone(string servertemplateID, string name, string description)
        {
            Utility.CheckStringHasValue(name);
            string postHref = string.Format(APIHrefs.ServerTemplateClone, servertemplateID);
            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            Utility.addParameter(name, "server_template[name]", postParams);
            Utility.addParameter(description, "server_template[description]", postParams);
            return clonePost(postHref, postParams);
        }

        /// <summary>
        /// Private method handles post for ServerTemplate clones
        /// </summary>
        /// <param name="postHref">href fragment to post to</param>
        /// <param name="postParams">collection of POST parameters</param>
        /// <returns>ID of the newly created ServerTemplate</returns>
        private static string clonePost(string postHref, List<KeyValuePair<string, string>> postParams)
        {
            string cloneResults = string.Empty;
            List<string> returnVal = Core.APIClient.Instance.Post(postHref, postParams, "location", out cloneResults);
            return returnVal.Last<string>().Split('/').Last<string>();
        }
        #endregion

        #region ServerTemplate.destroy methods
        /// <summary>
        /// Deletes a given servertemplate.
        /// </summary>
        /// <param name="serverID">ID of the servertemplate to delete</param>
        /// <param name="deploymentID">ID of deployment where servertemplate to delete can be found</param>
        /// <returns>true if success, false if not</returns>
        public static bool destroy(string servertemplateID)
        {
            string deleteHref = string.Format(APIHrefs.ServerTemplateByID, servertemplateID);

            return Core.APIClient.Instance.Delete(deleteHref);
        }
        #endregion

        #region ServerTemplate.create methods

        #endregion

        #region ServerTemplate.update methods

        #endregion

        #region ServerTemplate.commit methods

        /// <summary>
        /// Commits a given ServerTemplate.  Only HEAD revisions (revision 0) that are owned by the account can be committed.
        /// </summary>
        /// <param name="serverTemplateID">ID of the ServerTemplate to be committed</param>
        /// <param name="commit_head_dependencies">Commit all HEAD revisions (if any) of the associated MultiCloud images, RightScripts and Chef repo sequences.</param>
        /// <param name="commit_message">The message associated with the commit.</param>
        /// <param name="freeze_repositories">Freeze the repositories</param>
        /// <returns>ID of the committed ServerTemplate</returns>
        public static string commit(string serverTemplateID, bool commit_head_dependencies, string commit_message, bool freeze_repositories)
        {
            string postHref = string.Format(APIHrefs.ServerTemplateCommit, serverTemplateID);
            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            Utility.addParameter(commit_head_dependencies.ToString().ToLower(), "commit_head_dependencies", postParams);
            Utility.addParameter(commit_message, "commit_message", postParams);
            Utility.addParameter(freeze_repositories.ToString().ToLower(), "freeze_repositories", postParams);
            return Core.APIClient.Instance.Post(postHref, postParams, "location").Last<string>().Split('/').Last<string>();
        }

        #endregion

        #region ServerTemplate.publish methods

        #endregion
    }
}
