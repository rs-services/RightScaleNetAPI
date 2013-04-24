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
            string getHref = string.Format(APIHrefs.SecurityGroupRule, cloudID, securityGroupID);
            return indexGet(ref view, getHref);
        }

        /// <summary>
        /// Internal get process for indexing SecurityGroupRules
        /// </summary>
        /// <param name="view"></param>
        /// <param name="getHref"></param>
        /// <returns></returns>
        private static List<SecurityGroupRule> indexGet(ref string view, string getHref)
        {
            view = getValidView(view);

            string queryString = string.Format("view={0}", view);
            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserializeList(jsonString);
        }

        #endregion

        #region SecurityGroupRule.show methods

        /// <summary>
        /// Displays information about a single SecurityGroupRule
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the SecurityGroup belongs</param>
        /// <param name="securityGroupID">ID of the SecurityGroup where the SecurityGroupRule belongs</param>
        /// <param name="securityGroupRuleID">ID of the SecurityGroupRule being returned</param>
        /// <returns>Populated SecurityGroupRule object</returns>
        public static SecurityGroupRule show(string cloudID, string securityGroupID, string securityGroupRuleID)
        {
            return show(cloudID, securityGroupID, securityGroupRuleID, null);
        }

        /// <summary>
        /// Displays information about a single SecurityGroupRule
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the SecurityGroup belongs</param>
        /// <param name="securityGroupID">ID of the SecurityGroup where the SecurityGroupRule belongs</param>
        /// <param name="securityGroupRuleID">ID of the SecurityGroupRule being returned</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>Populated SecurityGroupRule object</returns>
        public static SecurityGroupRule show(string cloudID, string securityGroupID, string securityGroupRuleID, string view)
        {
            string getHref = string.Format(APIHrefs.SecurityGroupRuleByID, cloudID, securityGroupID, securityGroupRuleID);
            return showGet(getHref, view);
        }

        /// <summary>
        /// Displays information about a single SecurityGroupRule
        /// </summary>
        /// <param name="securityGroupRuleHref">Fuly formed href for SecurityGroupRule</param>
        /// <returns>Populated SecurityGroupRule object</returns>
        public static SecurityGroupRule show(string securityGroupRuleHref)
        {
            return show(securityGroupRuleHref, null);
        }

        /// <summary>
        /// Displays information about a single SecurityGroupRule
        /// </summary>
        /// <param name="securityGroupRuleHref">Fuly formed href for SecurityGroupRule</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>Populated SecurityGroupRule object</returns>
        public static SecurityGroupRule show(string securityGroupRuleHref, string view)
        {
            return showGet(securityGroupRuleHref, view);
        }

        /// <summary>
        /// Internal method handles the API call for GET calls to RS API for retrieving a SecurityGroupRule
        /// </summary>
        /// <param name="getHref">Href to use to retrieve the object</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>Populated SecurityGroupRule object</returns>
        private static SecurityGroupRule showGet(string getHref, string view)
        {
            view = getValidView(view);

            string queryString = string.Format("view={0}", view);
            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserialize(jsonString);
        }
        #endregion

        /// <summary>
        /// Helper method to return a valid view value for SecurityGroupRule api calls
        /// </summary>
        /// <param name="view">view value to be tested</param>
        /// <returns>value of input view or default view if input is null or empty</returns>
        private static string getValidView(string view)
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
            return view;
        }
    }
}
