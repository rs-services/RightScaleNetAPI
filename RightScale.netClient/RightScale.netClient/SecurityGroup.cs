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
	
        #region object.index methods

        /// <summary>
        /// Lists Security Groups
        /// </summary>
        /// <returns>List of SecurityGroup objects</returns>
        public static List<SecurityGroup> index()
        {
            return index(null, null);
        }

        /// <summary>
        /// Lists Security Groups
        /// </summary>
        /// <param name="filter">Filter for searching for Security Groups</param>
        /// <returns>List of SecurityGroup objects</returns>
        public static List<SecurityGroup> index(List<Filter> filter)
        {
            return index(filter, null);
        }

        /// <summary>
        /// Lists Security Groups
        /// </summary>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>List of SecurityGroup objects</returns>
        public static List<SecurityGroup> index(string view)
        {
            return index(null, view);
        }

        /// <summary>
        /// Lists Security Groups
        /// </summary>
        /// <param name="filter">Filter for searching for Security Groups</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>List of SecurityGroup objects</returns>
        public static List<SecurityGroup> index(List<Filter> filter, string view)
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

            List<string> validFilters = new List<string>() { "name", "resource_uid" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            string queryString = string;
            if(filter != null && filter.Count > 0)
            {
                foreach(Filter f in filter)
                {
                    queryString += f.ToString() + "&";
                }
            }
            queryString += string.Format("view={0}", view);
            string jsonString = Core.APIClient.Instance.Get(APIHrefs.SecurityGroup);
            return deserializeList(jsonString);
        }
        #endregion
    }
}
