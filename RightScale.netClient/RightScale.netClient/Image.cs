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
        public List<Action> actions { get; set; }
        public string resource_uid { get; set; }
        public string cpu_architecture { get; set; }
        public string image_type { get; set; }
        public string virtualization_type { get; set; }
        public string os_platform { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }
        public string visibility { get; set; }

        //Image base URL
        static string getUrl = "/api/multi_cloud_images";

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

            string view = "default";
            string queryString = string.Empty;

            queryString += string.Format("view={0}", view);

            string jsonString = Core.APIClient.Instance.Get(getUrl, queryString);


            return deserializeList(jsonString);

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


            queryString += string.Format("view={0}", view);

            string jsonString = Core.APIClient.Instance.Get(getUrl, queryString);


            return deserializeList(jsonString);

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

    }
}
