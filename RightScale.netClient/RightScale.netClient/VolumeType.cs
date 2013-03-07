using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class VolumeType :Core.RightScaleObjectBase<VolumeType>
    {
        public string name { get; set; }
        public string resource_uid { get; set; }
        public string created_at { get; set; }
        public string size { get; set; }
        public string updated_at { get; set; }
        public string description { get; set; }

        #region VolumeType.ctor
        /// <summary>
        /// Default Constructor for VolumeType
        /// </summary>
        public VolumeType()
            : base()
        {
        }

        /// <summary>
        /// Constructor for VolumeType object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public VolumeType(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for VolumeType object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public VolumeType(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		

        public static VolumeType show(string cloudID, string volumeTypeID)
        {
            //TODO: implement VolumeType.show 
            throw new NotImplementedException();
        }


        #region VolumeType.index methods

        public static List<VolumeType> index()
        {
            return index(null, null);
        }

        public static List<VolumeType> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        public static List<VolumeType> index(string view)
        {
            return index(null, view);
        }

        public static List<VolumeType> index(List<KeyValuePair<string, string>> filter, string view)
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

            List<string> validFilters = new List<string>() { "name", "resource_uid" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement VolumeType.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
