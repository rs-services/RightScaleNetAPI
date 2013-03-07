using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    //volume_snapshot
    public class VolumeSnapshot:Core.RightScaleObjectBase<VolumeSnapshot>
    {
        public string name { get; set; }
        public string resource_uid { get; set; }
        public string created_at { get; set; }
        public string size { get; set; }
        public string updated_at { get; set; }
        public string description { get; set; }
        public string state { get; set; }

        #region VolumeSnapshot.ctor
        /// <summary>
        /// Default Constructor for VolumeSnapshot
        /// </summary>
        public VolumeSnapshot()
            : base()
        {
        }

        /// <summary>
        /// Constructor for VolumeSnapshot object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public VolumeSnapshot(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for VolumeSnapshot object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public VolumeSnapshot(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		

        
        #region VolumeSnapshot.index methods

        public static List<VolumeSnapshot> index()
        {
            return index(null, null);
        }

        public static List<VolumeSnapshot> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        public static List<VolumeSnapshot> index(string view)
        {
            return index(null, view);
        }

        public static List<VolumeSnapshot> index(List<KeyValuePair<string, string>> filter, string view)
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

            List<string> validFilters = new List<string>() { "description", "name", "parent_volume_href", "resource_uid" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement VolumeSnapshot.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
