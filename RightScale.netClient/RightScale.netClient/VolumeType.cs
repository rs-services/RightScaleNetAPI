using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// A VolumeType describes the type of volume particularly the size.
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeVolumeType.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceVolumeTypes.html
    /// </summary>
    public class VolumeType : Core.RightScaleObjectBase<VolumeType>
    {
        #region VolumeType properties

        /// <summary>
        /// Name of this VolumeType
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// RightScale Resource Unique Identifier for this VolumeType
        /// </summary>
        public string resource_uid { get; set; }

        /// <summary>
        /// Datetime when this VolumeType was created
        /// </summary>
        public string created_at { get; set; }

        /// <summary>
        /// Size of this VolumeType (in GB)
        /// </summary>
        public string size { get; set; }

        /// <summary>
        /// Datetime when this VolumeType was last updated
        /// </summary>
        public string updated_at { get; set; }

        /// <summary>
        /// Description of this VolumeType
        /// </summary>
        public string description { get; set; }

        #endregion

        #region VolumeType Relationships

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

        #region VolumeType.show

        /// <summary>
        /// Displays information about a single Volume
        /// </summary>
        /// <param name="cloudID">ID of the cloud</param>
        /// <param name="volumeTypeID">ID of the VolumeType</param>
        /// <returns>Popultated instance of VolumeType</returns>
        public static VolumeType show(string cloudID, string volumeTypeID)
        {
            return show(cloudID, volumeTypeID, null);
        }

        /// <summary>
        /// Displays information about a single Volume
        /// </summary>
        /// <param name="cloudID">ID of the cloud</param>
        /// <param name="volumeTypeID">ID of the VolumeType</param>
        /// <param name="view">Specifies how many attributes or relationships to include</param>
        /// <returns>Populated instance of VolumeType</returns>
        public static VolumeType show(string cloudID, string volumeTypeID, string view)
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
            string queryString = string.Format("view={0}", view);
            string getString = string.Format(APIHrefs.VolumeTypeByID, cloudID, volumeTypeID);
            string jsonString = Core.APIClient.Instance.Get(getString, queryString);
            return deserialize(jsonString);
        }

        #endregion

        #region VolumeType.index methods

        /// <summary>
        /// Lists Volume Types
        /// </summary>
        /// <param name="cloudID">ID of cloud where volume types are to be listed</param>
        /// <returns>List of VolumeTypes</returns>
        public static List<VolumeType> index(string cloudID)
        {
            return index(cloudID, null, null);
        }
        
        /// <summary>
        /// Lists Volume Types
        /// </summary>
        /// <param name="cloudID">ID of cloud where volume types are to be listed</param>
        /// <param name="filter">limites return set based on provided filter parameters</param>
        /// <returns>List of VolumeTypes</returns>
        public static List<VolumeType> index(string cloudID, List<Filter> filter)
        {
            return index(cloudID, filter, null);
        }

        /// <summary>
        /// Lists Volume Types
        /// </summary>
        /// <param name="cloudID">ID of cloud where volume types are to be listed</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>List of VolumeTypes</returns>
        public static List<VolumeType> index(string cloudID, string view)
        {
            return index(cloudID, null, view);
        }

        /// <summary>
        /// Lists Volume Types
        /// </summary>
        /// <param name="cloudID">ID of cloud where volume types are to be listed</param>
        /// <param name="filter">limites return set based on provided filter parameters</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>List of VolumeTypes</returns>
        public static List<VolumeType> index(string cloudID, List<Filter> filter, string view)
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

            string queryString = string.Format("view={0}&", view);

            foreach (Filter f in filter)
            {
                queryString += f.ToString() + "&";
            }

            queryString = queryString.TrimEnd('&');

            string getHref = string.Format(APIHrefs.VolumeType, cloudID);
            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserializeList(jsonString);
        }
        #endregion
    }
}
