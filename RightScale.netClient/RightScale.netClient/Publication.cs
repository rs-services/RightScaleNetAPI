using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Publication:Core.RightScaleObjectBase<Publication>
    {
        public string name { get; set; }
        public string commit_message { get; set; }
        public List<Action> actions { get; set; }
        public int created_at { get; set; }
        public string content_type { get; set; }
        public int updated_at { get; set; }
        public string publisher { get; set; }
        public int revision { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }
        public string revision_notes { get; set; }


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

        public static List<Publication> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        public static List<Publication> index(string view)
        {
            return index(null, view);
        }

        public static List<Publication> index(List<KeyValuePair<string, string>> filter, string view)
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
