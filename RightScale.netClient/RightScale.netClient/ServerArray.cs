using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// A server array represents a logical group of instances and allows to resize(grow/shrink) that group based on certain elasticity parameters.
    /// A server array just like a server always has a next_instance association, which will define the configuration to apply when a new instance is launched. But unlike a server which has a "currentinstance" relationship, the server array has a "currentinstances" relationship that gives the information about all the running instances in the array. Changes to the next_instance association prepares the configuration for the next instance that is to be launched in the array and will therefore not affect any of the currently running instances.
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeServerArray.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceServerArrays.html
    /// </summary>
    public class ServerArray : Core.RightScaleObjectBase<ServerArray>
    {
        public string name { get; set; }
        public ElasticityParams elasticity_params { get; set; }
        public Instance next_instance { get; set; }
        public string array_type { get; set; }
        public int instances_count { get; set; }
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

        public static List<ServerArray> index(List<Filter> filter)
        {
            return index(filter, null);
        }

        public static List<ServerArray> index(string view)
        {
            return index(null, view);
        }

        public static List<ServerArray> index(List<Filter> filter, string view)
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
