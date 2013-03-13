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

        /// <summary>
        /// Constructor for PublicationLineage object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public PublicationLineage(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for PublicationLineage object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public PublicationLineage(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		
    }
}
