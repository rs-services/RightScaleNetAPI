using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class ServerArray : Core.RightScaleObjectBase<ServerArray>
    {
        public string name { get; set; }
        public List<Action> actions { get; set; }
        public ElasticityParams elasticity_params { get; set; }
        public NextInstance next_instance { get; set; }
        public string array_type { get; set; }
        public int instances_count { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }
        public string state { get; set; }

        #region ServerArray.ctor
        /// <summary>
        /// Default Constructor for ServerArray
        /// </summary>
        public ServerArray()
            : base()
        {
        }

        /// <summary>
        /// Constructor for ServerArray object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public ServerArray(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for ServerArray object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public ServerArray(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		

        
        #region ServerArray.index methods

        public static List<ServerArray> index()
        {
            return index(null, null);
        }

        public static List<ServerArray> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        public static List<ServerArray> index(string view)
        {
            return index(null, view);
        }

        public static List<ServerArray> index(List<KeyValuePair<string, string>> filter, string view)
        {
            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default", "instance_detail" };
                Utility.CheckStringInput("view", validViews, view);
            }

            List<string> validFilters = new List<string>() { "cloud_href", "deployment_href", "name" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement ServerArray.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
