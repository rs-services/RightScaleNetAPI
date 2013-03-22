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
        #region AccountGroup properties

        /// <summary>
        /// Name of this AccountGroup
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Timestamp representing when this AccountGroup was created
        /// </summary>
        public string created_at { get; set; }

        /// <summary>
        /// Timestamp representing when this AccountGroup was last updated
        /// </summary>
        public string updated_at { get; set; }

        /// <summary>
        /// Description for this AccountGroup
        /// </summary>
        public string description { get; set; }

        #endregion

        #region ID Properties

        /// <summary>
        /// Account for this instance of AccountGroup
        /// </summary>
        public Account Account
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("account"));
                return Account.deserialize(jsonString);
            }
        }

        #endregion

        #region AccountGroup.ctor()

        /// <summary>
        /// Default constructor for AccountGroup object
        /// </summary>
        public AccountGroup()
            : base()
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
            return index(new List<Filter>(), string.Empty);
        }
        
        /// <summary>
        /// Lists the AccountGroups owned by this Account.
        /// </summary>
        /// <param name="filter">Set of filters to modify query to return AccountGroups from RightScale API</param>
        /// <returns>Filtered list of AccountGroups</returns>
        public static List<AccountGroup> index(List<Filter> filter)
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
            return index(new List<Filter>(), view);
        }

        /// <summary>
        /// Lists the AccountGroups owned by this Account.
        /// </summary>
        /// <param name="filter">Set of filters to modify query to return AccountGroups from RightScale API</param>
        /// <param name="view">Defines specific view to limit the AccountGroups returned from RightScale API</param>
        /// <returns>Filtered list of AccuntGroups based on filter and view input</returns>
        public static List<AccountGroup> index(List<Filter> filter, string view)
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
                queryString += Utility.BuildFilterString(filter);
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

            string getURL = string.Format(APIHrefs.AccountGroupByID, accountGroupID);
            string queryString = string.Format("view={0}", view);

            string jsonString = Core.APIClient.Instance.Get(getURL, queryString);

            return deserialize(jsonString);
        }

        #endregion
    }
}
