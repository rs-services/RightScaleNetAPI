using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// A Volume provides a highly reliable, efficient and persistent storage solution that can be mounted to a cloud instance (in the same datacenter / zone).
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeVolume.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceVolumes.html
    /// </summary>
    public class Volume:Core.RightScaleObjectBase<Volume>
    {
        #region Volume properties

        /// <summary>
        /// Name of this Volume
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// RightScale Resource UID for this Volume
        /// </summary>
        public string resource_uid { get; set; }

        /// <summary>
        /// Datetime when this Volume was created
        /// </summary>
        public string created_at { get; set; }

        /// <summary>
        /// Size of this volume (in GB)
        /// </summary>
        public int size { get; set; }

        /// <summary>
        /// Datetime when this Volume was last updated
        /// </summary>
        public string updated_at { get; set; }

        /// <summary>
        /// Description of this Volume
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// VolumeType of this Volume
        /// </summary>
        public VolumeType volume_type { get; set; }

        /// <summary>
        /// Current Status of this Volume
        /// </summary>
        public string status { get; set; }

        #endregion

        #region Volume relationships

        /// <summary>
        /// The volume snapshot from which the volume was created, if any
        /// </summary>
        public VolumeSnapshot parentVolumeSnapshot
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("parent_volume_snapshot"));
                return VolumeSnapshot.deserialize(jsonString);
            }
        }

        /// <summary>
        /// list of associated volume snapshots
        /// </summary>
        public List<VolumeSnapshot> volumeSnapshot
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("volume_snapshots"));
                return VolumeSnapshot.deserializeList(jsonString);
            }
        }

        /// <summary>
        /// Associated DataCenter
        /// </summary>
        public DataCenter datacenter
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("datacenter"));
                return DataCenter.deserialize(jsonString);
            }
        }

        /// <summary>
        /// List of associated recurring volume attachments
        /// </summary>
        public List<RecurringVolumeAttachment> recurringVolumeAttachments
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("recurring_volume_attachments"));
                return RecurringVolumeAttachment.deserializeList(jsonString);
            }
        }

        /// <summary>
        /// List of associated volume attachments.  Describes where the volume is attached to and the attachment parameters
        /// </summary>
        public List<VolumeAttachment> currentVolumeAttachments
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("current_volume_attachments"));
                return VolumeAttachment.deserializeList(jsonString);
            }
        }

        /// <summary>
        /// Associated Cloud
        /// </summary>
        public Cloud cloud
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("cloud"));
                return Cloud.deserialize(jsonString);
            }
        }

        #endregion

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
    }
}
