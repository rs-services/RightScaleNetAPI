using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// A VolumeSnapshot represents a Cloud storage volume at a particular point in time. One can create a snapshot regardless of whether or not a volume is attached to an Instance. When a snapshot is created, various meta data is retained such as a Created At timestamp, a unique Resource UID (e.g. vol-52EF05A9), the Volume Owner and Visibility (e.g. private or public). Snapshots consist of a series of data blocks that are incrementally saved.
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeVolumeSnapshot.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceVolumeSnapshots.html
    /// </summary>
    public class VolumeSnapshot:Core.TaggableResourceBase<VolumeSnapshot>
    {
        #region VolumeSnapshot properties

        /// <summary>
        /// Name of this VolumeSnapshot
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// RightScale Resource UID for this VolumeSnapshot
        /// </summary>
        public string resource_uid { get; set; }

        /// <summary>
        /// Datetime when this VolumeSnapshot was created
        /// </summary>
        public string created_at { get; set; }

        /// <summary>
        /// Size (in GB) of this VolumeSnapshot
        /// </summary>
        public string size { get; set; }

        /// <summary>
        /// Datetime when this VolumeSnapshot was last updated
        /// </summary>
        public string updated_at { get; set; }

        /// <summary>
        /// Description of this VolumeSnapshot
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Current state of this VolumeSnapshot
        /// </summary>
        public string state { get; set; }

        #endregion
        
        #region VolumeSnapshot Relationships

        /// <summary>
        /// List of associated RecurringVolumeAttachments
        /// </summary>
        public List<RecurringVolumeAttachment> recurringVolumeAttachments
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("recurring_volumeAttachments"));
                return RecurringVolumeAttachment.deserializeList(jsonString);
            }
        }

        /// <summary>
        /// The Volume from which the snapshot was created
        /// </summary>
        public Volume parentVolume
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("parent_volume"));
                return Volume.deserialize(jsonString);
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

        #region VolumeSnapshot.ctor
        /// <summary>
        /// Default Constructor for VolumeSnapshot
        /// </summary>
        public VolumeSnapshot()
            : base()
        {
        }
        
        #endregion
        
        #region VolumeSnapshot.index methods

        public static List<VolumeSnapshot> index()
        {
            return index(null, null);
        }

        public static List<VolumeSnapshot> index(List<Filter> filter)
        {
            return index(filter, null);
        }

        public static List<VolumeSnapshot> index(string view)
        {
            return index(null, view);
        }

        public static List<VolumeSnapshot> index(List<Filter> filter, string view)
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
