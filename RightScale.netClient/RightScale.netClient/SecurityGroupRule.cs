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

        public static List<SecurityGroupRule> index()
        {
            return index(null);
        }


        public static List<SecurityGroupRule> index(string view)
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

            //TODO: implement SecurityGroupRule.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
