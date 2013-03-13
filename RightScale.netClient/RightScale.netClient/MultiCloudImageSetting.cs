using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// A MultiCloudImageSetting defines which settings should be used when a server is launched in a cloud.
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeMultiCloudImageSetting.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceMultiCloudImageSettings.html
    /// </summary>
    public class MultiCloudImageSetting:Core.RightScaleObjectBase<MultiCloudImageSetting>
    {

        #region MultiCloudImageSetting.ctor
        /// <summary>
        /// Default Constructor for MultiCloudImageSetting
        /// </summary>
        public MultiCloudImageSetting()
            : base()
        {
        }

        /// <summary>
        /// Constructor for MultiCloudImageSetting object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public MultiCloudImageSetting(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for MultiCloudImageSetting object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public MultiCloudImageSetting(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion

        #region MultiCloudImageSetting Relationships

        /// <summary>
        /// MultiCloudImage associated with this MultiCloudImageSetting
        /// </summary>
        public MultiCloudImage multiCloudImage
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkIDValue("multi_cloud_image"));
                return MultiCloudImage.deserialize(jsonString);
            }
        }

        /// <summary>
        /// InstanceType associated with this MultiCloudImageSetting
        /// </summary>
        public InstanceType instanceType
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkIDValue("instance_type"));
                return InstanceType.deserialize(jsonString);
            }
        }

        /// <summary>
        /// Image associated with this MultiCloudImageSetting
        /// </summary>
        public Image image
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkIDValue("image"));
                return Image.deserialize(jsonString);
            }
        }

        /// <summary>
        /// Cloud associated with this MultiCloudImageSetting
        /// </summary>
        public Cloud cloud
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkIDValue("cloud"));
                return Cloud.deserialize(jsonString);
            }
        }

        #endregion

        #region MultiCloudImageSetting.index methods

        public static List<MultiCloudImageSetting> index()
        {
            return index(null);
        }

        public static List<MultiCloudImageSetting> index(List<Filter> filter)
        {
            List<string> validFilters = new List<string>() { "cloud_href", "multi_cloud_image_href" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement MultiCloudImageSetting.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
