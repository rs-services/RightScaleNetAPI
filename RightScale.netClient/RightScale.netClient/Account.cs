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
        public string name { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

        #region Account.ctor()

        /// <summary>
        /// Default constructor for Account Object
        /// </summary>
        public Account()
            : base()
        {

        }

        /// <summary>
        /// Constructor for Account object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public Account(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Constructor for Account object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public Account(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
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

            string getURL = string.Format("/api/accounts/{0}", accountID);

            string jsonString = Core.APIClient.Instance.Get(getURL);

            return deserialize(jsonString);
        }

        #endregion
    }
}
