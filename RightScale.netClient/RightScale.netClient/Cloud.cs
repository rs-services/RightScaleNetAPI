using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Cloud : Core.RightScaleObjectBase<Cloud>
    {
        public string name { get; set; }
        public string cloud_type { get; set; }
        public string description { get; set; }


        #region Cloud.ctor
        /// <summary>
        /// Default Constructor for Cloud
        /// </summary>
        public Cloud()
            : base()
        {
        }

        /// <summary>
        /// Constructor for Cloud object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public Cloud(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for Cloud object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public Cloud(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		
        #region Cloud.show() methods

        public static Cloud show(string cloudID)
        {
            Utility.CheckStringIsNumeric(cloudID);

            string getURL = string.Format("/api/clouds/{0}", cloudID);

            string jsonString = Core.APIClient.Instance.Get(getURL);

            return deserialize(jsonString);
        }

        #endregion

        #region Cloud Relationships

        /// <summary>
        /// list of datacenters associated with this cloud
        /// </summary>
        public List<DataCenter> datacenters
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("datacenters"));
                return DataCenter.deserializeList(jsonString);
            }
        }

        /// <summary>
        /// list of volume snapshots associated with this cloud
        /// </summary>
        public List<VolumeSnapshot> volumeSnapshots
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("volume_snapshots"));
                return VolumeSnapshot.deserializeList(jsonString);
            }
        }

        /// <summary>
        /// List of instances associated with this cloud
        /// </summary>
        public List<Instance> instances
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("instances"));
                return Instance.deserializeList(jsonString);
            }
        }

        /// <summary>
        /// List of Voulume Types associated with this cloud
        /// </summary>
        public List<VolumeType> volumeTypes
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("volume_types"));
                return VolumeType.deserializeList(jsonString);
            }
        }

        /// <summary>
        /// List of SSH keys associated with this cloud
        /// </summary>
        public List<SshKey> sshKeys
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("ssh_keys"));
                return SshKey.deserializeList(jsonString);
            }
        }

        /// <summary>
        /// List of recurring volume attachments associated with this cloud
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
        /// List of Volume Attachments associated with this cloud
        /// </summary>
        public List<VolumeAttachment> volumeAttachments
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("volume_attachments"));
                return VolumeAttachment.deserializeList(jsonString);
            }
        }

        /// <summary>
        /// list of Volumes associated with this cloud
        /// </summary>
        public List<Volume> volumes
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("volumes"));
                return Volume.deserializeList(jsonString);
            }
        }

        /// <summary>
        /// List of IP Address Bindings associated with this cloud
        /// </summary>
        public List<IPAddressBinding> ipAddressBindings
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("ip_address_bindings"));
                return IPAddressBinding.deserializeList(jsonString);
            }
        }

        /// <summary>
        /// List of images associated with this cloud
        /// </summary>
        public List<Image> images
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("images"));
                return Image.deserializeList(jsonString);
            }
        }
        
        /// <summary>
        /// List of instance types associated with this cloud
        /// </summary>
        public List<InstanceType> instanceTypes
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("instance_types"));
                return InstanceType.deserializeList(jsonString);
            }
        }

        /// <summary>
        /// List of IP Addressess associated with this cloud
        /// </summary>
        public List<IPAddress> ipAddresses
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("ip_addresses"));
                return IPAddress.deserializeList(jsonString);
            }
        }

        #endregion

        #region Cloud.index methods

        public static List<Cloud> index()
        {
            return index(null);
        }

        public static List<Cloud> index(List<Filter> filter)
        {

            List<string> validFilters = new List<string>() { "cloud_type", "description", "name" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement Cloud.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
