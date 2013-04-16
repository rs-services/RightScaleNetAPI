using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class ChildAccount : Core.RightScaleObjectBase<ChildAccount>
    {
        public string name { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

        #region ID Properties

        /// <summary>
        /// ID of associated account
        /// </summary>
        public Account ParentAccount
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("account"));
                return Account.deserialize(jsonString);
            }
        }

        /// <summary>
        /// ID of associated cluster
        /// </summary>
        public string ClusterID
        {
            get
            {
                return getLinkIDValue("cluster");
            }
        }

        #endregion

        #region ChildAccount.ctor
        /// <summary>
        /// Default Constructor for ChildAccount
        /// </summary>
        public ChildAccount()
            : base()
        {
        }

        #endregion
		
        
        #region ChildAccount.index methods

        public static List<ChildAccount> index()
        {
            return index(null);
        }

        public static List<ChildAccount> index(List<Filter> filter)
        {

            List<string> validFilters = new List<string>() { "name" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement ChildAccount.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
