using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// Object defines the data related to the attachment of a volume to an instance
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeVolumeAttachment.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceVolumeAttachments.html
    /// </summary>
    public class VolumeAttachment:Core.RightScaleObjectBase<VolumeAttachment>
    {
        #region VolumeAttachment properties

        /// <summary>
        /// Device for this VolumeAttachment
        /// </summary>
        public string device { get; set; }

        /// <summary>
        /// RightScale resource unique identifier for this Volume Attachment
        /// </summary>
        public string resource_uid { get; set; }

        /// <summary>
        /// Datetime when this VolumeAttachment was created
        /// </summary>
        public string created_at { get; set; }

        /// <summary>
        /// Datetime when this VolumeAttachment was last updated
        /// </summary>
        public string updated_at { get; set; }

        /// <summary>
        /// Device ID of this volume Attachment
        /// </summary>
        public string device_id { get; set; }

        /// <summary>
        /// Current state of this Volume Attachment
        /// </summary>
        public string state { get; set; }

        #endregion

        #region VolumeAttachment relationships

        /// <summary>
        /// Associated Volume
        /// </summary>
        public Volume volume
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("volume"));
                return Volume.deserialize(jsonString);
            }
        }

        /// <summary>
        /// Associated Instance
        /// </summary>
        public Instance instance
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("instance"));
                return Instance.deserialize(jsonString);
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

        /// <summary>
        /// Lists all volume attachments
        /// </summary>
        /// <param name="cloudID">ID of the cloud to query</param>
        /// <returns>List of VolumeAttachment objects</returns>
        public static List<VolumeAttachment> index(string cloudID)
        {
            return index(cloudID, null, null);
        }

        /// <summary>
        /// Lists all volume attachments
        /// </summary>
        /// <param name="cloudID">ID fo the cloud to query</param>
        /// <param name="filter">Set of filters for query</param>
        /// <returns>List of VolumeAttachment objects</returns>
        public static List<VolumeAttachment> index(string cloudID, List<Filter> filter)
        {
            return index(cloudID, filter, null);
        }

        /// <summary>
        /// Lists all volume attachments
        /// </summary>
        /// <param name="cloudID">ID of the cloud to query</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>List of VolumeAttachment objects</returns>
        public static List<VolumeAttachment> index(string cloudID, string view)
        {
            return index(cloudID, null, view);
        }

        /// <summary>
        /// Lists all volume attachments
        /// </summary>
        /// <param name="cloudID">ID of the cloud to query</param>
        /// <param name="filter">Set of filters for query</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>List of VolumeAttachment objects</returns>
        public static List<VolumeAttachment> index(string cloudID, List<Filter> filter, string view)
        {
            string getHref = string.Format(APIHrefs.VolumeAttachment, cloudID);
            return indexGet(getHref, filter, view);
        }

        /// <summary>
        /// Lists all Volume Attachments for an Instance
        /// </summary>
        /// <param name="cloudID">ID of the Cloud being queried</param>
        /// <param name="instanceID">Id of the Instance being queried</param>
        /// <returns>List of VolumeAttachment objects</returns>
        public static List<VolumeAttachment> index_Instance(string cloudID, string instanceID)
        {
            return index_Instance(cloudID, instanceID, new List<Filter>(), string.Empty);
        }

        /// <summary>
        /// Lists all Volume Attachments for an Instance
        /// </summary>
        /// <param name="cloudID">ID of the Cloud being queried</param>
        /// <param name="instanceID">Id of the Instance being queried</param>
        /// <param name="filter">Set of filters for query</param>
        /// <returns>List of VolumeAttachment objects</returns>
        public static List<VolumeAttachment> index_Instance(string cloudID, string instanceID, List<Filter> filter)
        {
            return index_Instance(cloudID, instanceID, filter, string.Empty);
        }

        /// <summary>
        /// Lists all Volume Attachments for an Instance
        /// </summary>
        /// <param name="cloudID">ID of the Cloud being queried</param>
        /// <param name="instanceID">Id of the Instance being queried</param>
        /// <param name="view">Specifies how many attributes and/or expanded ne
        /// <returns>List of VolumeAttachment objects</returns>
        public static List<VolumeAttachment> index_Instance(string cloudID, string instanceID, string view)
        {
            return index_Instance(cloudID, instanceID, new List<Filter>(), view);
        }

        /// <summary>
        /// Lists all Volume Attachments for an Instance
        /// </summary>
        /// <param name="cloudID">ID of the Cloud being queried</param>
        /// <param name="instanceID">Id of the Instance being queried</param>
        /// <param name="filter">Set of filters for query</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>List of VolumeAttachment objects</returns>
        public static List<VolumeAttachment> index_Instance(string cloudID, string instanceID, List<Filter> filter, string view)
        {
            string getHref = string.Format(APIHrefs.InstanceVolumeAttachment, cloudID, instanceID);
            return indexGet(getHref, filter, view);
        }

        /// <summary>
        /// Private method managing central process for getting list of VolumeAttachment classes
        /// </summary>
        /// <param name="getHref">API Href fragment for RSAPI call</param>
        /// <param name="filter">collection of filters for this request</param>
        /// <param name="view">View specified for this request</param>
        /// <returns>List of VolumeAttachmemt objects</returns>
        private static List<VolumeAttachment> indexGet(string getHref, List<Filter> filter, string view)
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
            string queryString = string.Empty;

            foreach (Filter f in filter)
            {
                queryString += f.ToString() + "&";
            }
            queryString += string.Format("view={0}", view);

            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserializeList(jsonString);
        }
        #endregion
		
    }
}
