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
        public List<Action> actions { get; set; }
        public string server_tag_scope { get; set; }
        public List<Input> inputs { get; set; }
        public List<Link> links { get; set; }
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

        public static List<Deployment> index()
        {
            return index(null, null);
        }

        public static List<Deployment> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        public static List<Deployment> index(string view)
        {
            return index(null, view);
        }

        public static List<Deployment> index(List<KeyValuePair<string, string>> filter, string view)
        {
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

            //TODO: implement Deployment.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
