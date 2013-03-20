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

        #region MultiCloudImage Relationships

        public List<MultiCloudImageSetting> settings
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("settings"));
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

        /// <summary>
        /// Lists the MultiCloudImages for a given ServerTemplate
        /// </summary>
        /// <param name="serverTemplateID">ID of the ServerTemplate</param>
        /// <returns>List of MultiCloudImages</returns>
        public static List<MultiCloudImage> index_ServerTemplate(string serverTemplateID)
        {
            return index_ServerTemplate(serverTemplateID, new List<Filter>());
        }

        /// <summary>
        /// Lists the MultiCloudImages for a given ServerTemplate
        /// </summary>
        /// <param name="serverTemplateID">ID of the ServerTemplate</param>
        /// <param name="filter">String of filters to be applied to this index query</param>
        /// <returns>List of MultiCloudImages</returns>
        public static List<MultiCloudImage> index_ServerTemplate(string serverTemplateID, string filter)
        {
            List<Filter> filterList = Filter.parseFilterList(filter);
            return index_ServerTemplate(serverTemplateID, filterList);
        }

        /// <summary>
        /// Lists the MultiCloudImages for a given ServerTemplate
        /// </summary>
        /// <param name="serverTemplateID">ID of the ServerTemplate</param>
        /// <param name="filter">List of filters to be applied to this index query</param>
        /// <returns>List of MultiCloudImages</returns>
        public static List<MultiCloudImage> index_ServerTemplate(string serverTemplateID, List<Filter> filter)
        {
            string getHref = string.Format(APIHrefs.ServerTemplateMultiCloudImage, serverTemplateID);
            return indexGet(filter, getHref);
        }

        /// <summary>
        /// Lists the MultiCloudImages owned by this Account.
        /// </summary>
        /// <returns>List of MultiCloudImages</returns>
        public static List<MultiCloudImage> index()
        {
            return index(new List<Filter>());
        }

        /// <summary>
        /// This method is intended for use within PowerShell.
        /// Lists the MultiCloudImages owned by this Account.
        /// </summary>
        /// <param name="filterList">Set of filters to modify query to return MultiCloudImages from RightScale API</param>
        /// <returns>Filtered list of MultiCloudImages</returns>
        public static List<MultiCloudImage> index(string filter)
        {
            List<Filter> filterList = Filter.parseFilterList(filter);

            return index(filterList);
        }

        /// <summary>       
        /// Lists the MultiCloudImages owned by this Account.
        /// </summary>
        /// <param name="filterList">Set of filters to modify query to return MultiCloudImages from RightScale API</param>
        /// <param name="view">Defines specific view to limit the MultiCloudImages returned from RightScale API</param>
        /// <returns>Filtered list of MultiCloudImages based on filter and view input</returns>
        public static List<MultiCloudImage> index(List<Filter> filterList)
        {

            string getUrl = string.Format(APIHrefs.MultiCloudImage);
            return indexGet(filterList, getUrl);

        }

        private static List<MultiCloudImage> indexGet(List<Filter> filterList, string getUrl)
        {
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
            string getHref = string.Format(APIHrefs.MultiCloudImageByID, multicloudimageID);
            return showGet(getHref);
        }

        /// <summary>
        /// Internal implementation of show for both deployment and non-deployment calls.  
        /// </summary>
        /// <param name="getHref"></param>
        /// <param name="view"></param>
        /// <returns>Image object with data</returns>
        private static MultiCloudImage showGet(string getHref)
        {
            string jsonString = Core.APIClient.Instance.Get(getHref);
            return deserialize(jsonString);
        }

        /// <summary>
        /// Shows the information about a single MultiCloudImage in the context of a ServerTemplate
        /// </summary>
        /// <param name="serverTemplateID">ID of the ServerTemplate to query</param>
        /// <param name="multiCloudImageID">ID of the MultiCloudImage to show</param>
        /// <returns>Populated MultiCloudImage object</returns>
        public static MultiCloudImage show_ServerTemplate(string serverTemplateID, string multiCloudImageID)
        {
            string getHref = string.Format(APIHrefs.ServerTemplateMultiCloudImageByID, serverTemplateID, multiCloudImageID);
            return showGet(getHref);
        }
        
        #endregion

        #region MultiCloudImage.create methods

        /// <summary>
        /// Creates a new MultiCloudImage with the given parameters
        /// </summary>
        /// <param name="name">Name of the MultiCloud Image</param>
        /// <returns>ID of the newly created MultiCloud Image</returns>
        public static string create(string name)
        {
            return create(name, string.Empty);
        }

        /// <summary>
        /// Creates a new MultiCloudImage with the given parameters
        /// </summary>
        /// <param name="name">Name of the MultiCloud Image</param>
        /// <param name="description">Description for this MultiCloud Image</param>
        /// <returns>ID of the newly created MultiCloud Image</returns>
        public static string create(string name, string description)
        {
            string postHref = APIHrefs.MultiCloudImage;
            return createPost(postHref, name, description);
        }

        /// <summary>
        /// Creates a new MultiCloudImage with the given parameters
        /// </summary>
        /// <param name="serverTemplateID">ID of the ServerTemplate to associate with</param>
        /// <param name="name">Name of the MultiCloud Image</param>
        /// <returns>ID of the newly created MultiCloud Image</returns>
        public static string create_serverTemplate(string serverTemplateID, string name)
        {
            return create_serverTemplate(serverTemplateID, name, string.Empty);
        }

        /// <summary>
        /// Creates a new MultiCloudImage with the given parameters
        /// </summary>
        /// <param name="serverTemplateID">ID of the ServerTemplate to associate with</param>
        /// <param name="name">Name of the MultiCloud Image</param>
        /// <param name="description">Description for this MultiCloud Image</param>
        /// <returns>ID of the newly created MultiCloud Image</returns>
        public static string create_serverTemplate(string serverTemplateID, string name, string description)
        {
            string postHref = string.Format(APIHrefs.ServerTemplateMultiCloudImage, serverTemplateID);
            return createPost(postHref, name, description);
        }

        /// <summary>
        /// Centralized method to handle all create posts
        /// </summary>
        /// <param name="postHref">api href for making a request to create a new MultiCloud Image</param>
        /// <param name="name">Name of the MultiCloud Image</param>
        /// <param name="description">Description for this MultiCloud Image</param>
        /// <returns>ID of the newly created MultiCloud Image</returns>
        private static string createPost(string postHref, string name, string description)
        {
            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            Utility.addParameter(description, "multi_cloud_image[description]", postParams);
            Utility.addParameter(name, "multi_cloud_image[name]", postParams);
            string outStr = string.Empty;
            List<string> strList = Core.APIClient.Instance.Post(postHref, postParams, "location", out outStr);
            return strList.Last<string>().Split('/').Last<string>();
        }
        #endregion

        #region MultiCloudImage.update methods

        /// <summary>
        /// Updates attributes of a given MultiCloudImage.  Only HEAD revisions can be updated (revision 0). 
        /// </summary>
        /// <param name="multiCloudImageID">ID of the MultiCloud Image</param>
        /// <param name="name">updated name for the MultiCloudImage</param>
        /// <param name="description">updated description for the MultiCloudImage</param>
        /// <returns>true if updated, false if not</returns>
        public static bool update(string multiCloudImageID, string name, string description)
        {
            string putHref = string.Format(APIHrefs.MultiCloudImageByID, multiCloudImageID);
            return updatePut(putHref, name, description);
        }

        /// <summary>
        /// Updates attributes of a given MultiCloudImage.  Only HEAD revisions can be updated (revision 0). 
        /// </summary>
        /// <param name="serverTemplateID">ID of the ServerTemplate where the MultiCloudImage is located</param>
        /// <param name="multiCloudImageID">ID of the MultiCloud Image</param>
        /// <param name="name">updated name for the MultiCloudImage</param>
        /// <param name="description">updated description for the MultiCloudImage</param>
        /// <returns>true if updated, false if not</returns>
        public static bool update_ServerTemplate(string serverTemplateID, string multiCloudImageID, string name, string description)
        {
            string putHref = string.Format(APIHrefs.ServerTemplateMultiCloudImageByID, serverTemplateID, multiCloudImageID);
            return updatePut(putHref, name, description);
        }

        /// <summary>
        /// Private centralized caller for updating MultiCloudImage
        /// </summary>
        /// <param name="putHref">API Href fragment to perform PUT operation against</param>
        /// <param name="name">updated name for the MultiCloudImage</param>
        /// <param name="description">updated description for the MultiCloudImage</param>
        /// <returns>true if updated, false if not</returns>
        private static bool updatePut(string putHref, string name, string description)
        {
            List<KeyValuePair<string, string>> putParams = new List<KeyValuePair<string, string>>();
            Utility.addParameter(name, "multi_cloud_image[name]", putParams);
            Utility.addParameter(description, "multi_cloud_image[description]", putParams);
            return Core.APIClient.Instance.Put(putHref, putParams);
        }

        #endregion

        #region MultiCloudImage.clone methods

        /// <summary>
        /// Clones a given MultiCloudImage
        /// </summary>
        /// <param name="multiCloudImageID">ID of the MultiCloud Image to clone</param>
        /// <returns>ID of the newly created MultiCloud Image</returns>
        public static string clone(string multiCloudImageID)
        {
            string postHref = string.Format(APIHrefs.MultiCloudImageClone, multiCloudImageID);
            string outString = string.Empty;
            List<string> results = Core.APIClient.Instance.Post(postHref,new List<KeyValuePair<string,string>>(), "location", out outString);
            return results.Last<string>().Split('/').Last<string>();
        }

        #endregion

        #region MultiCloudImage.commit methods
       
        /// <summary>
        /// Commits a given MultiCloudImage.  Only HEAD revisions can be committed.
        /// </summary>
        /// <param name="multiCloudImageID">ID of the MultiCloudImage to be committed</param>
        /// <returns>ID fo the committed MultiCloud Image</returns>
        public static string commit(string multiCloudImageID)
        {
            string postHref = string.Format(APIHrefs.MultiCloudImageCommit, multiCloudImageID);
            string outString = string.Empty;
            List<string> results = Core.APIClient.Instance.Post(postHref, new List<KeyValuePair<string, string>>(), "location", out outString);
            return results.Last<string>().Split('/').Last<string>();
        }

        #endregion

        #region MultiCloudImage.destroy methods

        /// <summary>
        /// Deletes a given MultiCloudImage
        /// </summary>
        /// <param name="multiCloudImageID">ID of the MultiCloudImage to delete</param>
        /// <returns>True if success, false if not</returns>
        public static bool destroy(string multiCloudImageID)
        {
            string deleteHref = string.Format(APIHrefs.MultiCloudImageByID, multiCloudImageID);
            return Core.APIClient.Instance.Delete(deleteHref);
        }

        /// <summary>
        /// Deletes a given MultiCloudImage 
        /// </summary>
        /// <param name="serverTemplateID">ID of the ServerTemplate the MCI is associated with</param>
        /// <param name="multiCloudImageID">ID of the MultiCloudImage to delete</param>
        /// <returns>true if success, false if not</returns>
        public static bool destroy_ServerTemplate(string serverTemplateID, string multiCloudImageID)
        {
            string deleteHref = string.Format(APIHrefs.ServerTemplateMultiCloudImageByID, serverTemplateID, multiCloudImageID);
            return Core.APIClient.Instance.Delete(deleteHref);
        }

        #endregion
    }
}
