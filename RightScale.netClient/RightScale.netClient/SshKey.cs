using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// Ssh Keys represent a created SSH Key that exists in the cloud.
    /// An ssh key might also contain the private part of the key, and can be used to login to instances launched with it.
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeSshKey.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceSshKeys.html
    /// </summary>
    public class SshKey:Core.RightScaleObjectBase<SshKey>
    {
        /// <summary>
        /// RightScale resource unique identifier for this SshKey
        /// </summary>
        public string resource_uid { get; set; }

        #region SshKey Relationships

        /// <summary>
        /// Associated Cloud
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

        #region SshKey.ctor
        /// <summary>
        /// Default Constructor for SshKey
        /// </summary>
        public SshKey()
            : base()
        {
        }

        /// <summary>
        /// Constructor for SshKey object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public SshKey(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for SshKey object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public SshKey(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
        
        #region SshKey.index methods

        public static List<SshKey> index()
        {
            return index(null, null);
        }

        public static List<SshKey> index(List<Filter> filter)
        {
            return index(filter, null);
        }

        public static List<SshKey> index(string view)
        {
            return index(null, view);
        }

        public static List<SshKey> index(List<Filter> filter, string view)
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

            List<string> validFilters = new List<string>() { "resource_uid" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement SshKey.index
            throw new NotImplementedException();
        }
        #endregion
    }
}
