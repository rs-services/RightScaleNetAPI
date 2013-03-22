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

        public static List<object> index()
        {
            return index(null, null);
        }

        public static List<object> index(List<Filter> filter)
        {
            return index(filter, null);
        }

        public static List<object> index(string view)
        {
            return index(null, view);
        }

        public static List<object> index(List<Filter> filter, string view)
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

            //TODO: implement object.index
            throw new NotImplementedException();
        }
        #endregion
    }
}
