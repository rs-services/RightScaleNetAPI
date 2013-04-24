using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// Security Groups represent network security profiles that contain lists of firewall rules for different ports and source IP addresses, as well as trust relationships amongst different security groups.
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeSecurityGroup.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceSecurityGroups.html
    /// </summary>
    public class SecurityGroup:Core.RightScaleObjectBase<SecurityGroup>
    {
        #region SecurityGroup Properties

        /// <summary>
        /// Name of this SecurityGroup
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// RightScale Resource UID of this Security Group
        /// </summary>
        public string resource_uid { get; set; }
        
        #endregion

        #region SecurityGroup Relationships

        /// <summary>
        /// Cloud associated with this SecurityGroup
        /// </summary>
        public Cloud cloud
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("cloud"));
                return Cloud.deserialize(jsonString);
            }
        }

        /// <summary>
        /// List of SecurityGroupRules associated with this SecurityGroup
        /// </summary>
        public List<SecurityGroupRule> securityGroupRules
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("security_group_rules"));
                return SecurityGroupRule.deserializeList(jsonString);
            }
        }

        #endregion

        #region SecurityGroup.ctor
        /// <summary>
        /// Default Constructor for SecurityGroup
        /// </summary>
        public SecurityGroup()
            : base()
        {
        }

        #endregion
	
        #region SecurityGroup.index methods

        /// <summary>
        /// Lists Security Groups
        /// </summary>
        /// <returns>List of SecurityGroup objects</returns>
        public static List<SecurityGroup> index(string cloudID)
        {
            return index(cloudID, null, null);
        }

        /// <summary>
        /// Lists Security Groups
        /// </summary>
        /// <param name="cloudID">ID of the cloud to query</param>
        /// <param name="filter">Filter for searching for Security Groups</param>
        /// <returns>List of SecurityGroup objects</returns>
        public static List<SecurityGroup> index(string cloudID, List<Filter> filter)
        {
            return index(cloudID, filter, null);
        }

        /// <summary>
        /// Lists Security Groups
        /// </summary>
        /// <param name="cloudID">ID of the cloud to query</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>List of SecurityGroup objects</returns>
        public static List<SecurityGroup> index(string cloudID, string view)
        {
            return index(cloudID, null, view);
        }

        /// <summary>
        /// Lists Security Groups
        /// </summary>
        /// <param name="cloudID">ID of the cloud to query</param>
        /// <param name="filter">Filter for searching for Security Groups</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>List of SecurityGroup objects</returns>
        public static List<SecurityGroup> index(string cloudID, List<Filter> filter, string view)
        {
            string getHref = string.Format(APIHrefs.SecurityGroup, cloudID);
            
            view = checkValidView(view);

            List<string> validFilters = new List<string>() { "name", "resource_uid" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            string queryString = string.Empty;
            if(filter != null && filter.Count > 0)
            {
                foreach(Filter f in filter)
                {
                    queryString += f.ToString() + "&";
                }
            }
            queryString += string.Format("view={0}", view);
            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserializeList(jsonString);
        }

        #endregion

        #region SecurityGroup.show methods

        /// <summary>
        /// Displays information about a single Security Group.
        /// </summary>
        /// <param name="cloudID">ID of the cloud to query</param>
        /// <param name="securityGroupID">ID of the Security Group to return</param>
        /// <returns>populated SecurityGroup object</returns>
        public static SecurityGroup show(string cloudID, string securityGroupID)
        {
            return show(cloudID, securityGroupID, null);
        }

        /// <summary>
        /// Displays information about a single Security Group.
        /// </summary>
        /// <param name="cloudID">ID of the cloud to query</param>
        /// <param name="securityGroupID">ID of the Security Group to return</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include.</param>
        /// <returns>populated SecurityGroup object</returns>
        public static SecurityGroup show(string cloudID, string securityGroupID, string view)
        {
            view = checkValidView(view);
            string getHref = string.Format(APIHrefs.SecurityGroupByID, cloudID, securityGroupID);
            string queryString = string.Format("view={0}", view);
            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserialize(jsonString);
        }

        #endregion

        #region SecurityGroup.create methods

        /// <summary>
        /// Creates a new Security Group in the specified cloud
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the Security Group will be created</param>
        /// <param name="name">Name of the Security Group to be created</param>
        /// <returns>ID of the newly created Security Group</returns>
        public string create(string cloudID, string name)
        {
            return create(cloudID, name, null);
        }

        /// <summary>
        /// Creates a new Security Group in the specified cloud
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the Security Group will be created</param>
        /// <param name="name">Name of the Security Group to be created</param>
        /// <param name="description">Description for the newly created security group</param>
        /// <returns>ID of the newly created Security Group</returns>
        public string create(string cloudID, string name, string description)
        {
            string postHref = string.Format(APIHrefs.SecurityGroup, cloudID);
            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            Utility.CheckStringHasValue(name);
            Utility.addParameter(name, "security_group[name]", postParams);
            Utility.addParameter(description, "security_group[description]", postParams);
            return Core.APIClient.Instance.Post(postHref, postParams, "location").Last<string>().Split('/').Last<string>();
        }

        #endregion

        /// <summary>
        /// Private helper method handles checking for and returning a valid view value for calls for SecutiryGroup info from the RS API
        /// </summary>
        /// <param name="view">view value to check</param>
        /// <returns>string representing the view itself or a valid value if null was passed in</returns>
        private static string checkValidView(string view)
        {
            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default", "tiny" };
                Utility.CheckStringInput("view", validViews, view);
            }
            return view;
        }
    }
}
