using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace RightScale.netClient
{
    public class Image : Core.RightScaleObjectBase<Image>
    {
        public string name { get; set; }
        public string resource_uid { get; set; }
        public string cpu_architecture { get; set; }
        public string image_type { get; set; }
        public string virtualization_type { get; set; }
        public string os_platform { get; set; }
        public string description { get; set; }
        public string visibility { get; set; }

        #region Image Relationships

        /// <summary>
        /// Cloud associated with this Image
        /// </summary>
        public Cloud cloud
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkIDValue("cloud"));
                return Cloud.deserialize(jsonString);
            }
        }

        #endregion


        #region Image.ctor
        /// <summary>
        /// Default Constructor for Image
        /// </summary>
        public Image()
            : base()
        {
        }

        /// <summary>
        /// Constructor for Image object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public Image(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for Image object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public Image(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion

        #region Image.index methods

        /// <summary>
        /// Lists the Images owned by this Account.
        /// </summary>
        /// <returns>List of Images</returns>
        public static List<Image> index()
        {

            return index(new List<Filter>(), null);

        }

        /// <summary>
        /// Lists the Images owned by this Account.
        /// </summary>
        /// <param name="filterList">Set of filters to modify query to return Images from RightScale API</param>
        /// <returns>Filtered list of Images</returns>
        public static List<Image> index(List<Filter> filterList)
        {
            return index(filterList, string.Empty);
        }

        /// <summary>
        /// Lists the Images owned by this Account.
        /// </summary>
        /// <param name="view">Defines specific view to limit the Images returned from RightScale API</param>
        /// <returns>Filtered list of Images based on view input</returns>
        public static List<Image> index(string view)
        {
            return index(new List<Filter>(), view);

        }

        /// <summary>
        /// This method is intended for use within PowerShell.
        /// Lists the Images owned by this Account.
        /// </summary>
        /// <param name="filterList">Set of filters to modify query to return Images from RightScale API</param>
        /// <param name="view">Defines specific view to limit the Images returned from RightScale API</param>
        /// <returns>Filtered list of Images based on filter and view input</returns>
        public static List<Image> index(string filterList, string view)
        {
           List<Filter> filter = Filter.parseFilterList(filterList);

           return index(filter,view);
        }

        /// <summary>
        /// Lists the Images owned by this Account.
        /// </summary>
        /// <param name="filterList">Set of filters to modify query to return Images from RightScale API</param>
        /// <param name="view">Defines specific view to limit the Images returned from RightScale API</param>
        /// <returns>Filtered list of Images based on filter and view input</returns>
        public static List<Image> index(List<Filter> filterList, string view)
        {
            string getUrl = "/api/multi_cloud_images";
            string queryString = string.Empty;
  
           if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default" };
                Utility.CheckStringInput("view", validViews, view);
            }

            List<string> validFilters = new List<string>() { "cpu_architecture", "description", "image_type", "name", "os_platform", "resource_uid", "visibility" };
            Utility.CheckFilterInput("filter", validFilters, filterList);


            if (filterList != null && filterList.Count > 0)
            {
                queryString += Utility.BuildFilterString(filterList) + "&";
            }
            if (!string.IsNullOrWhiteSpace(view))
            {
                queryString += string.Format("view={0}", view);
            }

            string jsonString = Core.APIClient.Instance.Get(getUrl, queryString);

            return deserializeList(jsonString);
        }
        #endregion

        #region Image.show methods

        /// <summary>
        /// Shows the information of a single image.
        /// </summary>
        /// <param name="serverid">ID of the image to be retrieved</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include.</param>
        /// <returns>Populated Image object</returns>
        public static Image show(string imageid, string view)
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

            string getHref = string.Format("/api/multi_cloud_images/images/{0}", imageid);
            return showGet(getHref, view);
        }

        /// <summary>
        /// Internal implementation of show for both deployment and non-deployment calls.  
        /// </summary>
        /// <param name="getHref"></param>
        /// <param name="view"></param>
        /// <returns>Image object with data</returns>
        private static Image showGet(string getHref, string view)
        {
            List<string> validViews = new List<string>() { "default"};
            Utility.CheckStringInput("view", validViews, view);

            string queryString = string.Empty;

            if (!string.IsNullOrWhiteSpace(view))
            {
                queryString += string.Format("view={0}", view);
            }

            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserialize(jsonString);
        }

        
        #endregion

    }
}
