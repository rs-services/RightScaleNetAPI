using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Account : Core.RightScaleObjectBase<Account>
    {
        public string name { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public List<Link> links { get; set; }

        public static Account show(string accountID)
        {
            Utility.CheckStringIsNumeric(accountID);

            string getURL = string.Format("/api/accounts/{0}", accountID);

            string jsonString = Core.APIClient.Instance.Get(getURL);

            return deserialize(jsonString);
        }
    }
}
