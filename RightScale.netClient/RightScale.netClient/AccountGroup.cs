using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class AccountGroup : Core.RightScaleObjectBase<AccountGroup>
    {
        public string name { get; set; }
        public List<Action> actions { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }


        public AccountGroup()
            : base()
        {
        }

        public AccountGroup(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        public AccountGroup(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }
        
        #region AccountGroup.index methods

        public static List<AccountGroup> index()
        {
            return index(null, null);
        }

        public static List<AccountGroup> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        public static List<AccountGroup> index(string view)
        {
            return index(null, view);
        }

        public static List<AccountGroup> index(List<KeyValuePair<string, string>> filter, string view)
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

            //TODO: validate potential inputs with engineering
            List<string> validFilters = new List<string>() { "name" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement AccountGroup.index
            throw new NotImplementedException();
        }
        #endregion

        public static AccountGroup show(string accountGroupID)
        {
            return show(accountGroupID, null);
        }

        #region AccountGroup.show methods

        public static AccountGroup show(string accountGroupID, string view)
        {
            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            List<string> validViews = new List<string>() { "default" };
            Utility.CheckStringInput("view", validViews, view);

            Utility.CheckStringIsNumeric(accountGroupID);

            string getURL = string.Format("/api/account_groups/{0}", accountGroupID);
            string queryString = string.Format("view={0}", view);

            string jsonString = Core.APIClient.Instance.Get(getURL, queryString);

            return deserialize(jsonString);
        }

        #endregion
    }
}
