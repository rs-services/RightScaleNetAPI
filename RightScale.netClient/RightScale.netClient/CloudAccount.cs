using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class CloudAccount : Core.RightScaleObjectBase<CloudAccount>
    {
        //TODO: need to write this class



        #region CloudAccount.ctor
        /// <summary>
        /// Default Constructor for CloudAccount
        /// </summary>
        public CloudAccount()
            : base()
        {
        }
        
        #endregion

        #region CloudAccount Relationships

        /// <summary>
        /// Account associated with this CloudAccount
        /// </summary>
        public Account account
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("account"));
                return Account.deserialize(jsonString);
            }
        }

        /// <summary>
        /// Coud associated with this CloudAccount
        /// </summary>
        public Cloud cloud
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("cloud"));
                return Cloud.deserialize(jsonString);
            }
        }

        #endregion

        #region CloudAccount.index methods

        public static List<CloudAccount> index()
        {
            return index(null);
        }

        public static List<CloudAccount> index(List<Filter> filter)
        {
            List<string> validFilters = new List<string>() { "name" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement CloudAccount.index
            throw new NotImplementedException();
        }

        #endregion
		
    }
}
