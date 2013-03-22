using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// Account object represents a specific RightScale Account
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeAccount.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceAccounts.html
    /// </summary>
    public class Account : Core.RightScaleObjectBase<Account>
    {
        #region Account Properties

        /// <summary>
        /// Name of this account
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Timestamp representing when this account was created
        /// </summary>
        public string created_at { get; set; }

        /// <summary>
        /// Timestamp representing when this account was last updated
        /// </summary>
        public string updated_at { get; set; }

        #endregion

        /// <summary>
        /// Associated tags for this object
        /// </summary>
        public List<string> Tags
        {
            get
            {
                return Tag.byResource(getLinkValue("self"));
            }
        }

        #region ID Properties

        /// <summary>
        /// Owner of this account
        /// </summary>
        public Account Owner
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("owner"));
                return Account.deserialize(jsonString);
            }
        }
        
        /// <summary>
        /// Cluster ID for this instance of Account
        /// </summary>
        public string ClusterID
        {
            get
            {
                return getLinkIDValue("cluster");
            }
        }

        #endregion

        #region Account.ctor()

        /// <summary>
        /// Default constructor for Account Object
        /// </summary>
        public Account()
            : base()
        {

        }

        #endregion

        #region Account.show methods

        /// <summary>
        /// Show method returns an instance of Account object based on account ID passed in
        /// </summary>
        /// <param name="accountID">ID of account object to return</param>
        /// <returns>instance of Account object</returns>
        public static Account show(string accountID)
        {
            Utility.CheckStringIsNumeric(accountID);

            string getURL = string.Format(APIHrefs.AccountByID, accountID);

            string jsonString = Core.APIClient.Instance.Get(getURL);

            return deserialize(jsonString);
        }

        #endregion
    }
}
