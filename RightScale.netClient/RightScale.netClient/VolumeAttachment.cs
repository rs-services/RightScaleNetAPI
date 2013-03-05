using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class VolumeAttachment:Core.RightScaleObjectBase<VolumeAttachment>
    {
        public string device { get; set; }
        public List<Action> actions { get; set; }
        public string resource_uid { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string device_id { get; set; }
        public List<Link> links { get; set; }
        public string state { get; set; }

        #region VolumeAttachment.ctor
        /// <summary>
        /// Default Constructor for VolumeAttachment
        /// </summary>
        public VolumeAttachment()
            : base()
        {
        }

        /// <summary>
        /// Constructor for VolumeAttachment object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public VolumeAttachment(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for VolumeAttachment object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public VolumeAttachment(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		


        #region VolumeAttachment.index methods

        public static List<VolumeAttachment> index()
        {
            return index(null, null);
        }

        public static List<VolumeAttachment> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        public static List<VolumeAttachment> index(string view)
        {
            return index(null, view);
        }

        public static List<VolumeAttachment> index(List<KeyValuePair<string, string>> filter, string view)
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

            List<string> validFilters = new List<string>() { "instance_href", "resource_uid", "volume_href" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement VolumeAttachment.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
