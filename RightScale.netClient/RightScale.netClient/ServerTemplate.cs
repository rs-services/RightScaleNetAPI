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
    public class ServerTemplate:Core.RightScaleObjectBase<ServerTemplate>
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

        #region ServerTemplate.ctor
        /// <summary>
        /// Default Constructor for ServerTemplate
        /// </summary>
        public ServerTemplate()
            : base()
        {
        }

        /// <summary>
        /// Constructor for ServerTemplate object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public ServerTemplate(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for ServerTemplate object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public ServerTemplate(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		

        
        #region ServerTemplate.index methods

        public static List<ServerTemplate> index()
        {
            return index(null, null);
        }

        public static List<ServerTemplate> index(List<Filter> filter)
        {
            return index(filter, null);
        }

        public static List<ServerTemplate> index(string view)
        {
            return index(null, view);
        }

        public static List<ServerTemplate> index(List<Filter> filter, string view)
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

            List<string> validFilters = new List<string>() { "description", "multi_cloud_image_href", "name", "revision" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement ServerTemplate.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
