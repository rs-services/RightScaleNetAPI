using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// A MultiCloudImage is a RightScale component that functions as a pointer to machine images in specific clouds (e.g. AWS US-East, Rackspace). Each ServerTemplate can reference many MultiCloudImages that defines which image should be used when a server is launched in a particular cloud.
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeMultiCloudImage.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceMultiCloudImages.html
    /// </summary>
    public class MultiCloudImage:Core.RightScaleObjectBase<MultiCloudImage>
    {
        #region MultiCloudImage Properties

        /// <summary>
        /// Name of this MultiCloudImage
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Revision of this MultiCloudImage
        /// </summary>
        public int revision { get; set; }

        /// <summary>
        /// Description of this MultiCloudImage
        /// </summary>
        public string description { get; set; }

        #endregion

        #region MultiCloudImage Relationships

        public List<MultiCloudImageSetting> settings
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkIDValue("settings"));
                return MultiCloudImageSetting.deserializeList(jsonString);
            }
        }


        #endregion


        #region MultiCloudImage.ctor
        /// <summary>
        /// Default Constructor for MultiCloudImage
        /// </summary>
        public MultiCloudImage()
            : base()
        {
        }

        /// <summary>
        /// Constructor for MultiCloudImage object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public MultiCloudImage(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for MultiCloudImage object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public MultiCloudImage(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		
        #region MultiCloudImage.index methods

        public static List<MultiCloudImage> index()
        {
            return index(new List<Filter>());
        }

        public static List<MultiCloudImage> index(string filter)
        {
            List<Filter> filterList = Filter.parseFilterList(filter);

            return index(filterList);
        }

        public static List<MultiCloudImage> index(List<Filter> filterList)
        {

            string getUrl = string.Format("/api/multi_cloud_images");
            string queryString = string.Empty;

            List<string> validFilters = new List<string>() { "name", "description", "revision" };
            Utility.CheckFilterInput("filter", validFilters, filterList);

            if (filterList != null && filterList.Count > 0)
            {
                queryString += Utility.BuildFilterString(filterList) + "&";
            }

            string jsonString = Core.APIClient.Instance.Get(getUrl, queryString);

            return deserializeList(jsonString);

        }
        #endregion

        #region MultiCloudImage.show methods
        /// <summary>
        /// Shows the information of a single multicloudimage.
        /// </summary>
        /// <param name="serverid">ID of the multicloudimage to be retrieved</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include.</param>
        /// <returns>Populated MultiCloudImage object</returns>
        public static MultiCloudImage show(string multicloudimageID)
        {
            string getHref = string.Format("/api/multi_cloud_images/{0}", multicloudimageID);
            return showGet(getHref, string.Empty);
        }

        /// <summary>
        /// Internal implementation of show for both deployment and non-deployment calls.  
        /// </summary>
        /// <param name="getHref"></param>
        /// <param name="view"></param>
        /// <returns>Image object with data</returns>
        private static MultiCloudImage showGet(string getHref, string view)
        {

            string jsonString = Core.APIClient.Instance.Get(getHref);
            return deserialize(jsonString);
        }

        #endregion

    }
}
