using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// This resource represents links between ServerTemplates and MultiCloud Images, it enables you to effectively add MultiCloud Images to ServerTemplates and make them the default one
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeServerTemplateMultiCloudImage.html
    /// Resources Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeServerTemplateMultiCloudImage.html
    /// </summary>
    public class ServerTemplateMultiCloudImage:Core.RightScaleObjectBase<ServerTemplateMultiCloudImage>
    {
        #region ServerTemplateMultiCloudImage Properties

        /// <summary>
        /// Datetime when this ServerTemplateMultiCloudImage was created
        /// </summary>
        public string created_at { get; set; }

        /// <summary>
        /// Boolean indicating that this image is the default ServerTemplateMultiCloudImage
        /// </summary>
        public bool is_default { get; set; }

        /// <summary>
        /// Datetime when this ServerTemplateMultiCloudImage was last updated
        /// </summary>
        public string updated_at { get; set; }

        #endregion

        #region ServerTemplateMultiCloudImage Relationships

        /// <summary>
        /// MultiCloudImage associated with this ServerTemplateMultiCloudImage
        /// </summary>
        public MultiCloudImage multiCloudImage
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("multi_cloud_image"));
                return MultiCloudImage.deserialize(jsonString);
            }
        }

        /// <summary>
        /// ServerTemplate associated with this ServerTemplateMultiCloudImage
        /// </summary>
        public ServerTemplate serverTemplate
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("server_template"));
                return ServerTemplate.deserialize(jsonString);
            }
        }

        #endregion

        #region ServerTemplateMultiCloudImage.ctor
        /// <summary>
        /// Default Constructor for ServerTemplateMultiCloudImage
        /// </summary>
        public ServerTemplateMultiCloudImage()
            : base()
        {
        }

        /// <summary>
        /// Constructor for ServerTemplateMultiCloudImage object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public ServerTemplateMultiCloudImage(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for ServerTemplateMultiCloudImage object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public ServerTemplateMultiCloudImage(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion

        #region ServerTemplateMultiCloudImage.index methods

        public static List<ServerTemplateMultiCloudImage> index()
        {
            return index(null, null);
        }

        public static List<ServerTemplateMultiCloudImage> index(List<Filter> filter)
        {
            return index(filter, null);
        }

        public static List<ServerTemplateMultiCloudImage> index(string view)
        {
            return index(null, view);
        }

        public static List<ServerTemplateMultiCloudImage> index(List<Filter> filter, string view)
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

            List<string> validFilters = new List<string>() { "is_default", "multi_cloud_image_href", "server_template_href" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement ServerTemplateMultiCloudImage.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
