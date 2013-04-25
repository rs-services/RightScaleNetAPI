using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// Represents a Cloud Account (An association between the account and a cloud).
    /// MediaType reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeCloudAccount.html
    /// Resource reference: http://reference.rightscale.com/api1.5/resources/ResourceCloudAccounts.html
    /// </summary>
    public class CloudAccount : Core.RightScaleObjectBase<CloudAccount>
    {
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

        /// <summary>
        /// Lists the CloudAccounts (non-aws) available to this Account
        /// </summary>
        /// <returns>List of CloudAccounts</returns>
        public static List<CloudAccount> index()
        {
            string jsonString = Core.APIClient.Instance.Get(APIHrefs.CloudAccount);
            return deserializeList(jsonString);
        }

        //TODO: API documentation doesn't reflect any valid filters, so I'm not including the ability to query with filters--will need to update this once documentation is updated

        #endregion

        #region CloudAccount.show methods

        /// <summary>
        /// Show information about a single CloudAccount
        /// </summary>
        /// <param name="cloudAccountID">ID of the CloudAccount to return</param>
        /// <returns>Populated instance of a CloudAccount object</returns>
        public static CloudAccount show(string cloudAccountID)
        {
            string getHref = string.Format(APIHrefs.CloudAccountByID, cloudAccountID);
            string jsonString = Core.APIClient.Instance.Get(getHref);
            return deserialize(jsonString);
        }

        #endregion

        #region CloudAccount.create methods

        /// <summary>
        /// Create a CloudAccount by passing in the respective credentials for each cloud.
        /// If the cloud is a public non-aws cloud, the cloud_href can be accessed from the 'Clouds' resource. If the cloud is an aws cloud, simply pass the string 'aws'.
        /// </summary>
        /// <param name="cloudID">ID of the Cloud from the 'Clouds' resource - if AWS pass the string 'aws'</param>
        /// <param name="creds">Collection of parameters for registering the cloud.  Specific required fields per cloud can be found on the resource page for CloudAccounts</param>
        /// <returns>ID of newly created CloudAccount</returns>
        public static string create(string cloudID, Dictionary<string, string> creds)
        {
            return create(cloudID, creds, null);
        }

        /// <summary>
        /// Create a CloudAccount by passing in the respective credentials for each cloud.
        /// If the cloud is a public non-aws cloud, the cloud_href can be accessed from the 'Clouds' resource. If the cloud is an aws cloud, simply pass the string 'aws'.
        /// </summary>
        /// <param name="cloudID">ID of the Cloud from the 'Clouds' resource - if AWS pass the string 'aws'</param>
        /// <param name="creds">Collection of parameters for registering the cloud.  Specific required fields per cloud can be found on the resource page for CloudAccounts</param>
        /// <param name="token">The cloud token to identify a private cloud</param>
        /// <returns>ID of newly created CloudAccount</returns>
        public static string create(string cloudID, Dictionary<string, string> creds, string token)
        {
            Utility.CheckStringHasValue(cloudID);
            if (creds == null || creds.Count == 0)
            {
                throw new ArgumentException("CloudAccount.create 'creds' parameter must contain a value specific to the CloudAccount being registered");
            }

            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            string cloudIDParameter = string.Empty;
            if (cloudID.ToLower().Trim() == "aws")
            {
                cloudIDParameter = cloudID.ToLower();
            }
            else
            {
                cloudIDParameter = string.Format(APIHrefs.CloudByID, cloudID);
            }
            Utility.addParameter(cloudIDParameter, "cloud_account[cloud_href]", postParams);
            
            foreach (string k in creds.Keys)
            {
                Utility.addParameter(creds[k], string.Format("cloud_account[creds][{0}]", k), postParams);
            }

            Utility.addParameter(token, "cloud_account[token]", postParams);

            return Core.APIClient.Instance.Post(APIHrefs.CloudAccount, postParams, "location").Last<string>().Split('/').Last<string>();
        }

        #endregion

        #region CloudAccount.destroy methods

        /// <summary>
        /// Delete a CloudAccount
        /// </summary>
        /// <param name="cloudAccountID">ID of CloudAccount to delete</param>
        /// <returns>true if deleted, false if not</returns>
        public static bool destroy(string cloudAccountID)
        {
            string destroyHref = string.Format(APIHrefs.CloudAccountByID, cloudAccountID);
            return Core.APIClient.Instance.Delete(destroyHref);
        }

        #endregion
    }
}
