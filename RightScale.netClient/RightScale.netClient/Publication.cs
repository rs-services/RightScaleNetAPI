using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// A Publication is a revisioned component shared with a set of Account Groups. If shared with your account, it can be imported in to your account.
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypePublication.html
    /// Resources Reference: http://reference.rightscale.com/api1.5/resources/ResourcePublications.html
    /// </summary>
    public class Publication:Core.RightScaleObjectBase<Publication>
    {
        #region Publication Properties

        /// <summary>
        /// Name of this Publication
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Commit message for this Publication
        /// </summary>
        public CommitMessage commit_message { get; set; }

        /// <summary>
        /// Datestamp representing when this Publication was created
        /// </summary>
        public string created_at { get; set; }

        /// <summary>
        /// Content Type for this Publication
        /// </summary>
        public string content_type { get; set; }

        /// <summary>
        /// Datestamp representing when this Publication was last updated
        /// </summary>
        public string updated_at { get; set; }

        /// <summary>
        /// Publisher for this Publication
        /// </summary>
        public string publisher { get; set; }

        /// <summary>
        /// Revision number for this Publication
        /// </summary>
        public int revision { get; set; }

        /// <summary>
        /// Description for this Publication
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Revision Notes for this Publication
        /// </summary>
        public string revision_notes { get; set; }

        #endregion

        #region Publication.ctor
        /// <summary>
        /// Default Constructor for Publication
        /// </summary>
        public Publication()
            : base()
        {
        }
        
        #endregion		
        
        #region Publication.index methods

        /// <summary>
        /// Lists the publications available to this account. Only non-HEAD revisions are possible.
        /// </summary>
        /// <returns>List of RightScale publications</returns>
        public static List<Publication> index()
        {
            return index(null, null);
        }

        /// <summary>
        /// Lists the publications available to this account. Only non-HEAD revisions are possible.
        /// </summary>
        /// <param name="filter">List of filters to modify the return set from API</param>
        /// <returns>List of RightScale publications</returns>
        public static List<Publication> index(List<Filter> filter)
        {
            return index(filter, null);
        }

        /// <summary>
        /// Lists the publications available to this account. Only non-HEAD revisions are possible.
        /// </summary>
        /// <param name="view">Specifies how many attributes and/or expanded nexted relationships to include.</param>
        /// <returns>List of RightScale publications</returns>
        public static List<Publication> index(string view)
        {
            return index(null, view);
        }

        /// <summary>
        /// Lists the publications available to this account. Only non-HEAD revisions are possible.
        /// </summary>
        /// <param name="filter">List of filters to modify the return set from API</param>
        /// <param name="view">Specifies how many attributes and/or expanded nexted relationships to include.</param>
        /// <returns>List of RightScale publications</returns>
        public static List<Publication> index(List<Filter> filter, string view)
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

            List<string> validFilters = new List<string>() { "description", "name", "publisher", "revision" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            string queryString = string.Empty;

            if (filter != null && filter.Count > 0)
            {
                foreach (Filter f in filter)
                {
                    queryString += f.ToString() + "&";
                }
            }

            queryString += string.Format("view={0}", view);

            string jsonString = Core.APIClient.Instance.Get(APIHrefs.Publication, queryString);
            return deserializeList(jsonString);
        }
        #endregion

        #region Publication.show methods

        /// <summary>
        /// Show information about a single publication.  Only non-HEAD revisions are possible
        /// </summary>
        /// <param name="publicationID">ID of the publication to show</param>
        /// <returns>Specific Publication object being queried</returns>
        public static Publication show(string publicationID)
        {
            string getHref = string.Format(APIHrefs.PublicationByID, publicationID);
            string jsonString = Core.APIClient.Instance.Get(getHref);
            return deserialize(jsonString);
        }

        #endregion

        #region Publication.import methods

        /// <summary>
        /// Imports the given publication and its subordinates into this account.  Only non-HEAD revisions that are shared with the account can be imported.
        /// </summary>
        /// <param name="publicationID">ID of the publication to be imported</param>
        /// <returns></returns>
        public static ServerTemplate import(string publicationID)
        {
            string postHref = string.Format(APIHrefs.PublicationImport, publicationID);
            string postContent = string.Empty;
            string serverTemplateID = Core.APIClient.Instance.Post(postHref, new List<KeyValuePair<string, string>>(), "location", out postContent).Last<string>().Split('/').Last<string>();
            return ServerTemplate.show(serverTemplateID);
        }

        #endregion
    }
}
