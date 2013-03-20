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

        /// <summary>
        /// Associated tags for this object
        /// </summary>
        public List<string> Tags
        {
            get
            {
                return Tag.byResource(getLinkValue("self"));
            }
        }

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

        /// <summary>
        /// Lists volumes in a specific cloud
        /// </summary>
        /// <param name="cloudID">ID of the cloud to query for volumes</param>
        /// <returns>Returns a list of volumes in a given cloud</returns>
        public static List<Volume> index(string cloudID)
        {
            return index(cloudID, null, null);
        }

        /// <summary>
        /// Lists volumes in a specific cloud
        /// </summary>
        /// <param name="cloudID">ID of the cloud to query for volumes</param>
        /// <param name="filter">Set of filters limits which volumes are returned</param>
        /// <returns>Returns a list of volumes in a given cloud</returns>
        public static List<Volume> index(string cloudID, List<Filter> filter)
        {
            return index(cloudID, filter, null);
        }
        
        /// <summary>
        /// Lists volumes in a specific cloud
        /// </summary>
        /// <param name="cloudID">ID of the cloud to query for volumes</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>Returns a list of volumes in a given cloud</returns>
        public static List<Volume> index(string cloudID, string view)
        {
            return index(cloudID, null, view);
        }

        /// <summary>
        /// Lists volumes in a specific cloud
        /// </summary>
        /// <param name="cloudID">ID of the cloud to query for volumes</param>
        /// <param name="filter">Set of filters limits which volumes are returned</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>Returns a list of volumes in a given cloud</returns>
        public static List<Volume> index(string cloudID, List<Filter> filter, string view)
        {
            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            List<string> validViews = new List<string>() { "default", "extended" };
            Utility.CheckStringInput("view", validViews, view);

            List<string> validFilters = new List<string>() { "datacenter_href", "description", "name", "parent_volume_snapshot_href", "resource_uid" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            string getHref = string.Format(APIHrefs.Volume, cloudID);

            string queryString = string.Empty;

            foreach (Filter f in filter)
            {
                queryString += filter.ToString() + "&";
            }
            queryString += string.Format("view={0}", view);

            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserializeList(jsonString);
        }
        #endregion

        #region Volume.show methods

        /// <summary>
        /// Displays information about a single volume
        /// </summary>
        /// <param name="cloudID">Cloud ID where the volume can be found</param>
        /// <param name="volumeID">ID of volume to show</param>
        /// <returns>populated instance of Volume object</returns>
        public static Volume show(string cloudID, string volumeID)
        {
            return show(cloudID, volumeID, null);
        }

        /// <summary>
        /// Displays information about a single volume
        /// </summary>
        /// <param name="cloudID">Cloud ID where the volume can be found</param>
        /// <param name="volumeID">ID of the volume to show</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>populated instance of Volume object</returns>
        public static Volume show(string cloudID, string volumeID, string view)
        {
            string getHref = string.Format(APIHrefs.VolumeByID, cloudID, volumeID);
            List<string> validViews = new List<string>() { "default", "extended" };
            Utility.CheckStringInput("view", validViews, view);
            string queryString = string.Format("view={0}", view);
            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserialize(jsonString);
        }

        #endregion

        #region Volume.create method

        /// <summary>
        /// Creates a new volume
        /// </summary>
        /// <param name="name">The name for the Volume to be created</param>
        /// <returns>Volume ID</returns>
        public static string create(string cloudID, string name)
        {
            return create(cloudID, name, null, null, null, null, null, null, null);
        }

        /// <summary>
        /// Creates a new volume
        /// </summary>
        /// <param name="cloudID">The Cloud ID of the cloud where this volume will be created</param>
        /// <param name="name">The name for the Volume to be created</param>
        /// <param name="datacenterID">The ID of the datacenter that this Volume will be in</param>
        /// <param name="description">The description of the Volume to be created</param>
        /// <param name="iops">The number of IOPS this volume should support</param>
        /// <param name="parentVolumeID">ID of parent volume</param>
        /// <param name="parentVolumeSnapshotID">ID of parent volume snapshot</param>
        /// <param name="size">Size of volume in GB</param>
        /// <param name="volumeTypeID">VolumeType ID</param>
        /// <returns>Volume ID</returns>
        public static string create(string cloudID, string name, string datacenterID, string description, string iops, string parentVolumeID, string parentVolumeSnapshotID, string size, string volumeTypeID)
        {
            Utility.CheckStringHasValue(name);
            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            Utility.addParameter(name, "volume[name]", postParams);
            Utility.addParameter(Utility.datacenterHref(cloudID, datacenterID), "volume[datacenter_href]", postParams);
            Utility.addParameter(description, "volume[description]", postParams);
            Utility.addParameter(iops, "volume[iops]", postParams);
            Utility.addParameter(size, "volume[size]", postParams);
            Utility.addParameter(Utility.volumeSnapshotHref(cloudID, parentVolumeID, parentVolumeSnapshotID), "volume[parent_volume_snapshot_href]", postParams);
            Utility.addParameter(Utility.volumeTypeHrefByID(cloudID, volumeTypeID), "volume[volume_type_href]", postParams);

            string postString = string.Format(APIHrefs.VolumeType, cloudID);
            List<string> retVals = Core.APIClient.Instance.Post(postString, postParams, "location");
            return retVals.Last<string>().Split('/').Last<string>();

        }

        #endregion

        #region Volume.destroy methods

        /// <summary>
        /// Deletes a given volume
        /// </summary>
        /// <param name="cloudID">Cloud ID where the volume can be found</param>
        /// <param name="volumeID">ID of the Volume being deleted</param>
        /// <returns>true if successful, false if not</returns>
        public static bool destroy(string cloudID, string volumeID)
        {
            string deleteHref = string.Format(APIHrefs.VolumeByID, cloudID, volumeID);
            return Core.APIClient.Instance.Delete(deleteHref);
        }

        #endregion
    }
}
