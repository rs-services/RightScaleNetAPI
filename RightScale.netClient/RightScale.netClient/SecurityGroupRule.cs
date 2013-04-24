using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// Security Group Rules represent the ingress/egress rules that define a security group.
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeSecurityGroupRule.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceSecurityGroupRules.html
    /// </summary>
    public class SecurityGroupRule : Core.RightScaleObjectBase<SecurityGroupRule>
    {
        #region SecurityGroupRule Properties

        /// <summary>
        /// CIDR IPs for this SecurityGroupRule
        /// </summary>
        public string cidr_ips { get; set; }

        /// <summary>
        /// Protocol for this SecurityGroupRule
        /// </summary>
        public string protocol { get; set; }

        /// <summary>
        /// End Port for this SecurityGroupRule
        /// </summary>
        public string end_port { get; set; }

        /// <summary>
        /// Start Port for this SecurityGroupRule
        /// </summary>
        public string start_port { get; set; }

        #endregion

        #region SecurityGroupRule Relationships

        /// <summary>
        /// SecurityGroup associated with this SecurityGroupRule
        /// </summary>
        public SecurityGroup securityGroup
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("security_group"));
                return SecurityGroup.deserialize(jsonString);
            }
        }


        #endregion

        #region SecurityGroupRule.ctor
        /// <summary>
        /// Default Constructor for SecurityGroupRule
        /// </summary>
        public SecurityGroupRule()
            : base()
        {
        }

        #endregion
		        
        #region SecurityGroupRule.index methods

        /// <summary>
        /// Lists SecurityGroupRules
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the SecurityGroup belongs</param>
        /// <param name="securityGroupID">ID of the SecurityGroup where the rules belong</param>
        /// <returns>List of SecurityGroupRule objects</returns>
        public static List<SecurityGroupRule> index(string cloudID, string securityGroupID)
        {
            return index(cloudID, securityGroupID, null);
        }

        /// <summary>
        /// Lists SecurityGroupRules
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the SecurityGroup belongs</param>
        /// <param name="securityGroupID">ID of the SecurityGroup where the rules belong</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>List of SecurityGroupRule objects</returns>
        public static List<SecurityGroupRule> index(string cloudID, string securityGroupID, string view)
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

            string getHref = string.Format(APIHrefs.SecurityGroupRule, cloudID, securityGroupID);

            string queryString = string.Format("view={0}", view);
            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserializeList(jsonString);
        }
        #endregion
		
    }
}
