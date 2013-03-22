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

        /// <summary>
        /// Constructor for Publication object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public Publication(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for Publication object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public Publication(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion		
        
        #region Publication.index methods

        public static List<Publication> index()
        {
            return index(null, null);
        }

        public static List<Publication> index(List<Filter> filter)
        {
            return index(filter, null);
        }

        public static List<Publication> index(string view)
        {
            return index(null, view);
        }

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

            //TODO: implement Publication.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
