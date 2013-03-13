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
                string jsonString = Core.APIClient.Instance.Get(getLinkIDValue("security_group"));
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

        /// <summary>
        /// Constructor for SecurityGroupRule object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public SecurityGroupRule(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for SecurityGroupRule object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public SecurityGroupRule(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
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
