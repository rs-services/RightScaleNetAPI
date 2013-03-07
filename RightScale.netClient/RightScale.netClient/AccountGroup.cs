using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace RightScale.netClient
{
    /// <summary>
    /// An Account Group specifies which RightScale accounts will have access to import a shared RightScale component (e.g. ServerTemplate, RightScript, etc.) from the MultiCloud Marketplace.
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeAccountGroup.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceAccountGroups.html
    /// </summary>
    public class AccountGroup : Core.RightScaleObjectBase<AccountGroup>
    {
        public string name { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string description { get; set; }

        #region AccountGroup.ctor()

        /// <summary>
        /// Default constructor for AccountGroup object
        /// </summary>
        public AccountGroup()
            : base()
        {
        }

        /// <summary>
        /// Constructor for AccountGroup object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public AccountGroup(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for AccountGroup object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public AccountGroup(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion

        #region AccountGroup.index methods

        /// <summary>
        /// Lists the AccountGroups owned by this Account.
        /// </summary>
        /// <returns>List of AccountGroups</returns>
        public static List<AccountGroup> index()
        {
            return index(new List<KeyValuePair<string, string>>(), string.Empty);
        }

        /// <summary>
        /// Lists the AccountGroups owned by this acount
        /// </summary>
        /// <param name="filter">Set of filters to modify query to return AccountGroups from RightScale API</param>
        /// <returns>Filtered list of AccountGroups</returns>
        public static List<AccountGroup> index(Hashtable filter)
        {
            return index(Utility.convertToKVP(filter));
        }

        /// <summary>
        /// Lists the AccountGroups owned by this Account.
        /// </summary>
        /// <param name="filter">Set of filters to modify query to return AccountGroups from RightScale API</param>
        /// <returns>Filtered list of AccountGroups</returns>
        public static List<AccountGroup> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        /// <summary>
        /// Lists the AccountGroups owned by this Account.
        /// </summary>
        /// <param name="view">Defines specific view to limit the AccountGroups returned from RightScale API</param>
        /// <returns>Filtered list of AccountGroups based on view input</returns>
        public static List<AccountGroup> index(string view)
        {
            return index(new List<KeyValuePair<string, string>(), view);
        }

        /// <summary>
        /// Lists the AccountGroups owned by this Account.
        /// </summary>
        /// <param name="filter">Set of filters to modify query to return AccountGroups from RightScale API</param>
        /// <param name="view">Defines specific view to limit the AccountGroups returned from RightScale API</param>
        /// <returns>Filtered list of AccuntGroups based on filter and view input</returns>
        public static List<AccountGroup> index(Hashtable filter, string view)
        {
            return index(Utility.convertToKVP(filter), view);
        }
        
        /// <summary>
        /// Lists the AccountGroups owned by this Account.
        /// </summary>
        /// <param name="filter">Set of filters to modify query to return AccountGroups from RightScale API</param>
        /// <param name="view">Defines specific view to limit the AccountGroups returned from RightScale API</param>
        /// <returns>Filtered list of AccuntGroups based on filter and view input</returns>
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

            List<string> validFilters = new List<string>() { "name" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            string getUrl = "/api/account_groups";
            string queryString = string.Empty;

            if (filter != null && filter.Count > 0)
            {
                queryString += Utility.BuildFilterString(filter) + "&";
            }

            queryString += string.Format("view={0}", view);

            string jsonString = Core.APIClient.Instance.Get(getUrl, queryString);

            return deserializeList(jsonString);
        }
        #endregion

        #region AccountGroup.show methods

        /// <summary>
        /// Show information about a single AccountGroup
        /// </summary>
        /// <param name="accountGroupID">ID of the AccountGroup to retrieve</param>
        /// <returns></returns>
        public static AccountGroup show(string accountGroupID)
        {
            return show(accountGroupID, null);
        }

        /// <summary>
        /// Show information about a single AccountGroup
        /// </summary>
        /// <param name="accountGroupID">ID of the AccountGroup to retrieve</param>
        /// <param name="view">Specific view of AccountGroup to filter result set</param>
        /// <returns>instance of AccountGroup based on inputs</returns>
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
