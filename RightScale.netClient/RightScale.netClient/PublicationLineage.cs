using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// A Publication Lineage contains lineage information for a Publication. It is shared among all revisions of a Publication.
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypePublicationLineage.html
    /// Resources Reference: http://reference.rightscale.com/api1.5/resources/ResourcePublicationLineages.html
    /// </summary>
    public class PublicationLineage:Core.RightScaleObjectBase<PublicationLineage>
    {
        #region PublicationLineage Properties

        /// <summary>
        /// Name of this PublicationLineage
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Datestamp when this PublicationLineage was created
        /// </summary>
        public int created_at { get; set; }

        /// <summary>
        /// Boolean indicating that comments are enabled for this PublicationLineage
        /// </summary>
        public bool comments_enabled { get; set; }

        /// <summary>
        /// Short Description for this PublicationLineage
        /// </summary>
        public string short_description { get; set; }

        /// <summary>
        /// Datestamp when this PublicationLineage was last updated
        /// </summary>
        public int updated_at { get; set; }

        /// <summary>
        /// Long Description for this PublicationLineage
        /// </summary>
        public string long_description { get; set; }

        /// <summary>
        /// Boolean indicating that comments will be emailed to the creator of this PublicationLineage
        /// </summary>
        public bool comments_emailed { get; set; }

        #endregion

        #region PublicationLineage.ctor
        /// <summary>
        /// Default Constructor for PublicationLineage
        /// </summary>
        public PublicationLineage()
            : base()
        {
        }

        #endregion

        #region PublicationLineage.show method

        /// <summary>
        /// Show information about a single publication lineage. Only non-HEAD revisions are possible.
        /// </summary>
        /// <param name="publicationLineageID">ID of Publication Lineage to retrieve</param>
        /// <returns>Instance of a PublicationLineage object for the ID passed in</returns>
        public PublicationLineage show(string publicationLineageID)
        {
            return show(publicationLineageID, null);
        }

        /// <summary>
        /// Show information about a single publication lineage. Only non-HEAD revisions are possible.
        /// </summary>
        /// <param name="publicationLineageID">ID of Publication Lineage to retrieve</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>Instance of a PublicationLineage object for the ID passed in</returns>
        public PublicationLineage show(string publicationLineageID, string view)
        {
            string getHref = string.Format(APIHrefs.PublicationLineageByID, publicationLineageID);

            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default" };
                Utility.CheckStringInput("view", validViews, view);
            }
            string queryString = string.Format("view={0}", view);

            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserialize(jsonString);
        }

        #endregion

    }
}
