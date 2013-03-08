using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Volume:Core.RightScaleObjectBase<Volume>
    {
        public string name { get; set; }
        public string resource_uid { get; set; }
        public string created_at { get; set; }
        public int size { get; set; }
        public string updated_at { get; set; }
        public string description { get; set; }
        public VolumeType volume_type { get; set; }
        public string status { get; set; }

        #region Volume.ctor
        /// <summary>
        /// Default Constructor for Volume
        /// </summary>
        public Volume()
            : base()
        {
        }

        /// <summary>
        /// Constructor for Volume object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public Volume(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for Volume object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public Volume(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		

        public bool create()
        {
            //TODO: implement Volume.create
            throw new NotImplementedException();
        }

        public bool destroy()
        {
            //TODO: implement Volume.destroy
            throw new NotImplementedException();
        }

        
        #region Volume.index methods

        public static List<Volume> index()
        {
            return index(null, null);
        }

        public static List<Volume> index(List<Filter> filter)
        {
            return index(filter, null);
        }

        public static List<Volume> index(string view)
        {
            return index(null, view);
        }

        public static List<Volume> index(List<Filter> filter, string view)
        {
            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            List<string> validViews = new List<string>() { "default", "extended" };
            Utility.CheckStringInput("view", validViews, view);

            List<string> validFilters = new List<string>() { "datacenter_href", "description", "name", "parent_volume_snapshot_href", "resource_uid" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement Volume.index
            throw new NotImplementedException();
        }
        #endregion
		

        public static Volume show(string volumeId)
        {
            //TODO: implement Volume.show
            throw new NotImplementedException();
        }

        public static bool destroy(string cloudID, string volumeID)
        {
            //TODO: implement static Volume.destroy
            throw new NotImplementedException();
        }
    }
}
