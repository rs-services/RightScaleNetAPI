using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// Tag for identifying or describing a specific object
    /// </summary>
    public class Tag:Core.RightScaleObjectBase<Tag>
    {
        #region Tag properties

        /// <summary>
        /// Private variable to hold the full tag strig if it does not conform to the proper tagging format
        /// </summary>
        private string _name;

        /// <summary>
        /// Full value of the given Tag
        /// </summary>
        public string name
        {
            get
            {
                return getTag();
            }
            set
            {
                parseTag(value);
            }
        }

        /// <summary>
        /// Private variable to hold value of this.scope
        /// </summary>
        private string _tagScope;

        /// <summary>
        /// Scope of this tag
        /// </summary>
        public string scope
        {
            get
            {
                return _tagScope;
            }
            set
            {
                _tagScope = value.ToLower();
            }
        }

        /// <summary>
        /// key name for this tag
        /// </summary>
        public string tagName { get; set; }

        /// <summary>
        /// value for this tag
        /// </summary>
        public string tagValue { get; set; }

        #endregion

        #region Tag.ctor
        /// <summary>
        /// Default Constructor for Tag
        /// </summary>
        public Tag()
            : base()
        {
            this._name = string.Empty;
        }

        /// <summary>
        /// Public constructor taking a full tag string
        /// </summary>
        /// <param name="tagContents"></param>
        public Tag(string tagContents)
        {
            parseTag(tagContents);
        }

        /// <summary>
        /// Public constructor taking each of the three parts of a tag individually
        /// </summary>
        /// <param name="tScope">Tag Scope</param>
        /// <param name="tName">Tag name</param>
        /// <param name="tValue">Tag value</param>
        public Tag(string tScope, string tName, string tValue)
        {
            this.scope = tScope;
            this.tagName = tName;
            this.tagValue = tValue;
        }

        #endregion

        #region private helper methods for parsing and working with tags

        /// <summary>
        /// Private method parses a tag and pulls scope, key and valud into their own variables, otherwise it will simply set _name to the fullTagValue
        /// </summary>
        /// <param name="fullTagValue">full tag string value</param>
        private void parseTag(string fullTagValue)
        {
            string[] scopeSplit = fullTagValue.Split(':');
            if (scopeSplit.Length == 2)
            {
                string[] kvpSplit = scopeSplit[1].Split('=');
                if (kvpSplit.Length == 2)
                {
                    scope = scopeSplit[0];
                    tagName = kvpSplit[0];
                    tagValue = kvpSplit[1];
                }
                else
                {
                    _name = fullTagValue;
                }                
            }
            else
            {
                _name = fullTagValue;
            }
        }

        /// <summary>
        /// Private method manages process of building tag data behind the scenes
        /// </summary>
        /// <returns>value of the tag specified</returns>
        private string getTag()
        {
            if (string.IsNullOrEmpty(this._name))
            {
                return this.scope + ":" + this.tagName + "=" + this.tagValue;
            }
            else
            {
                return _name;
            }
        }

        #endregion

        /// <summary>
        /// Method writes a tag out from the values specified
        /// </summary>
        /// <returns>String representation of this tag</returns>
        public override string ToString()
        {
            return getTag();
        }

        #region Tag.by_resource methods

        /// <summary>
        /// Method parses a list of strings and produces a list of tags
        /// </summary>
        /// <param name="tagStrings">list of strings to parse into tags</param>
        /// <returns>List of tags</returns>
        public static List<Tag> StringToTags(List<string> tagStrings)
        {
            List<Tag> retVal = new List<Tag>();
            foreach (string s in tagStrings)
            {
                retVal.Add(new Tag(s));
            }
            return retVal;
        }

        /// <summary>
        /// Gets tag for a specific resource.
        /// </summary>
        /// <param name="href">href of the resource being queried for tags</param>        
        /// <returns>Not sure yet</returns>
        public static List<Tag> byResource(string href)
        {
            string[] hrefs = new string[] {href};
            return byResource(hrefs.ToList<string>()).Last<Resource>().tags;
        }

        /// <summary>
        /// Gets tag for a specific resource.
        /// </summary>
        /// <param name="resource_hrefs">Set of hrefs to retrive tags from</param>
        /// <returns>Not sure yet</returns>
        public static List<Resource> byResource(List<string> resourceHrefs)
        {
            string queryString = string.Empty;

            List<KeyValuePair<string, string>> paramList = new List<KeyValuePair<string, string>>();
            foreach (string s in resourceHrefs)
            {
                Utility.addParameter(s, "resource_hrefs[]", paramList);
            }

            string retVal = "content";
            List<string> tags =  Core.APIClient.Instance.Post(APIHrefs.TagByResource, paramList,null,out retVal);
            return Resource.deserializeList(retVal);
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
        /// <param name="tags">The tags which must be present on the resource.</param>
        /// <param name="resourceType">Type of resource to query on</param>
        /// <returns>List of resources pertaining to the tags queried for</returns>
        public static List<Resource> byTag(string includeTagsWithPrefix, bool matchAll, string resourceType, List<Tag> tags)
        {
            List<string> validResourceTypes = new List<string>() { "servers", "instances", "volumes", "volume_snapshots", "deployments", "server_templates", "multi_cloud_images", "images", "server_arrays", "accounts" };
            Utility.CheckStringInput("resource_type", validResourceTypes, resourceType);
            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            Utility.addParameter(includeTagsWithPrefix, "include_tags_with_prefix", postParams);
            Utility.addParameter(matchAll.ToString().ToLower(), "match_all", postParams);
            Utility.addParameter(resourceType, "resource_type", postParams);

            foreach (Tag t in tags)
            {
                Utility.addParameter(t.ToString(), "tags[]", postParams);
            }

            string outString = string.Empty;
            Core.APIClient.Instance.Post(APIHrefs.TagByTag, postParams, string.Empty, out outString);
            return Resource.deserializeList(outString);
        }

        #endregion

        #region Tag.multiAdd methods

        /// <summary>
        /// Add a list of tags to a list of hrefs. The tags must be either plain_tags or machine_tags. The hrefs can belong to various resource types. If a resource for a href could not be found, an error is returned and no tags are added for any resource.
        /// No error will be raised if the resource already has the tag(s) you are trying to add.
        /// Note: At this point, tags on 'next_instance' are not supported and one has to add tags to the 'server'.
        /// </summary>
        /// <param name="resourceHrefs">Hrefs of the resources for which the tags are to be added</param>
        /// <param name="tags">Tags to be added</param>
        /// <returns>True if successful, false if not</returns>
        public static bool multiAdd(List<string> resourceHrefs, List<Tag> tags)
        {
            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            foreach (string s in resourceHrefs)
            {
                Utility.addParameter(s, "resource_hrefs[]", postParams);
            }
            foreach (Tag t in tags)
            {
                Utility.addParameter(t.ToString(), "tags[]", postParams);
            }
            return Core.APIClient.Instance.Post(APIHrefs.TagMultiAdd, postParams);
        }

        #endregion

        #region Tag.multiDelete methods

        /// <summary>
        /// Delete a list of tags on a list of hrefs. The tags must be either plain_tags or machine_tags. The hrefs can belong to various resource types. If a resource for a href could not be found, an error is returned and no tags are deleted for any resource.   
        /// Note that no error will be raised if the resource does not have the tag(s) you are trying to delete
        /// </summary>
        /// <param name="resourceHrefs">hrefs of the resources for which tags are to be deleted</param>
        /// <param name="tags">Tags to be deleted</param>
        /// <returns>True if successful, false if not</returns>
        public static bool multiDelete(List<string> resourceHrefs, List<Tag> tags)
        {
            string postHref = "/api/tags/multi_delete";
            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            foreach (string s in resourceHrefs)
            {
                Utility.addParameter(s, "resource_hrefs[]", postParams);
            }
            foreach (Tag t in tags)
            {
                Utility.addParameter(t.ToString(), "tags[]", postParams);
            }
            return Core.APIClient.Instance.Post(postHref, postParams);
        }

        #endregion

    }
}
