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

        /// <summary>
        /// Lists the enterprise ChildAccounts available for this Account.
        /// </summary>
        /// <returns>Collection of ChildAccount objects</returns>
        public static List<ChildAccount> index()
        {
            return index(null);
        }

        /// <summary>
        /// Lists the enterprise ChildAccounts available for this Account.
        /// </summary>
        /// <param name="filter">Colletion of filters to limit return ste of API</param>
        /// <returns>Collection of ChildAccount objects</returns>
        public static List<ChildAccount> index(List<Filter> filter)
        {

            List<string> validFilters = new List<string>() { "name" };
            Utility.CheckFilterInput("filter", validFilters, filter);
            string queryString = string.Empty;
            if (filter != null && filter.Count > 0)
            {
                foreach (Filter f in filter)
                {
                    queryString += f.ToString() + "&";
                }
            }

            string jsonString = Core.APIClient.Instance.Get(APIHrefs.ChildAccount, queryString);
            return deserializeList(jsonString);
        }
        #endregion

        #region ChildAccount.create() methods

        /// <summary>
        /// Create an enterprise ChildAccount for this Account. The User will by default get an 'admin' role on the ChildAccount to enable him/her to add, delete Users and Permissions
        /// </summary>
        /// <param name="name">Name of the child account to be created</param>
        /// <returns>ID of the newly created child account</returns>
        public static string create(string name)
        {
            Utility.CheckStringHasValue(name);
            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            Utility.addParameter(name, "child_account[name]", postParams);
            return Core.APIClient.Instance.Post(APIHrefs.ChildAccount, postParams, "location").Last<string>().Split('/').Last<string>();
        }

        #endregion

        #region ChildAccount.update() methods

        /// <summary>
        /// Update an enterprise ChildAccount for this Account.
        /// </summary>
        /// <param name="childAccountID">ID of the ChildAccount to be updates</param>
        /// <param name="name">New name for ChildAccount specified</param>
        /// <returns>true if successful, false if not</returns>
        public static bool update(string childAccountID, string name)
        {
            Utility.CheckStringHasValue(childAccountID);
            string putHref = string.Format(APIHrefs.ChildAccountByID, childAccountID);
            List<KeyValuePair<string, string>> putParams = new List<KeyValuePair<string, string>>();
            Utility.addParameter(name, "child_account[name]", putParams);
            return Core.APIClient.Instance.Put(putHref, putParams);
        }

        #endregion

    }
}
