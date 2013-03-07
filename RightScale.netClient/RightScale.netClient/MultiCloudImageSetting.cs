using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
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
        #region MultiCloudImageSetting.index methods

        public static List<MultiCloudImageSetting> index()
        {
            return index(null);
        }

        public static List<MultiCloudImageSetting> index(List<KeyValuePair<string, string>> filter)
        {
            List<string> validFilters = new List<string>() { "cloud_href", "multi_cloud_image_href" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement MultiCloudImageSetting.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
