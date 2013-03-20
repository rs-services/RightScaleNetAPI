using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Tag:Core.RightScaleObjectBase<Tag>
    {
        public string name { get; set; }

        #region Tag.ctor
        /// <summary>
        /// Default Constructor for Tag
        /// </summary>
        public Tag()
            : base()
        {
        }

        /// <summary>
        /// Constructor for Tag object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public Tag(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for Tag object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public Tag(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		

        #region Tag.by_resource methods

        /// <summary>
        /// Gets tag for a specific resource.
        /// </summary>
        /// <param name="href">href of the resource being queried for tags</param>        
        /// <returns>Not sure yet</returns>
        /// TODO:  change this to return Tag object []
        public static List<string> byResource(string href)
        {
            string[] hrefs = new string[] {href};
            return byResource(hrefs);
        }

        /// <summary>
        /// Gets tag for a specific resource.
        /// </summary>
        /// <param name="resource_hrefs">Set of hrefs to retrive tags from</param>
        /// <returns>Not sure yet</returns>
        /// TODO:  change this to return Tag object []
        public static List<string> byResource(string[] hrefs)
        {
            string queryString = string.Empty;

            List<KeyValuePair<string, string>> paramList = Utility.StringArrayToParameterSet(hrefs, "resource_hrefs");

            string retVal = "content";
            List<string> tags =  Core.APIClient.Instance.Post(APIHrefs.TagByResource, paramList,null,out retVal);

            return tags;

        }

        #endregion

        #region Tag.by_tag methods

        /// <summary>
        /// Search for resources having a list of tags in a specific resource_type. If the parameter match_all is not "true" then the search is performed as an "OR" operation i.e. resources having any of the tags are returned. If it is set to "true", only resources having all the tags are returned.
        /// The list of tags may contain plain tags ("my_db_server"), machine tags ("server:db=true"), namespace matches ("server:"), or namespace & predicate matches ("server:db="). The result set includes links to the resources, as well as optionally additional tags matching the include_tags_with_prefix expression.
        /// For example, a search with tag[]="server:db=true" and include_tags_with_prefix="backup:" will return resources that match the first expression and for each one return any tags matching "backup:".
        /// If the include_tags_with_prefix is not passed, the list of tags for each resource will be empty.
        /// </summary>
        /// <param name="includeTagsWithPrefix">If included, all tags matching this prefix will be returned. If not included, no tags will be returned</param>
        /// <param name="matchAll">If set to 'true', resources having all the tags specified in the 'tags' parameter are returned. Otherwise, resources having any of the tags are returned.</param>
        /// <param name="resourceTag">Search among a single resource type</param>
        /// <param name="tags">The tags which must be present on the resource.</param>
        public static void byTag(string includeTagsWithPrefix, bool matchAll, string resourceType, List<string> tags)
        {
            List<string> validResourceTypes = new List<string>() { "servers", "instances", "volumes", "volume_snapshots", "deployments", "server_templates", "multi_cloud_images", "images", "server_arrays", "accounts" };
            Utility.CheckStringInput("resource_type", validResourceTypes, resourceType);
        }

        #endregion

        #region Tag.multi_add methods

        #endregion

        #region Tag.multi_delete methods

        #endregion

    }
}
