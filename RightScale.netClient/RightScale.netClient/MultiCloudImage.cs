using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class MultiCloudImage:Core.RightScaleObjectBase<MultiCloudImage>
    {
        public string name { get; set; }
        public List<Action> actions { get; set; }
        public int revision { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }



        #region MultiCloudImage.ctor
        /// <summary>
        /// Default Constructor for MultiCloudImage
        /// </summary>
        public MultiCloudImage()
            : base()
        {
        }

        /// <summary>
        /// Constructor for MultiCloudImage object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public MultiCloudImage(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for MultiCloudImage object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public MultiCloudImage(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		
        #region MultiCloudImage.index methods

        public static List<MultiCloudImage> index()
        {
            return index(null);
        }

        public static List<MultiCloudImage> index(List<KeyValuePair<string, string>> filter)
        {
            List<string> validFilters = new List<string>() { "name", "description", "revision" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement MultiCloudImage.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
