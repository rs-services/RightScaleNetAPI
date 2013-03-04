using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    class SecurityGroupRule
    {
        public List<object> actions { get; set; }
        public string cidr_ips { get; set; }
        public string protocol { get; set; }
        public List<Link> links { get; set; }
        public string end_port { get; set; }
        public string start_port { get; set; }

        
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
