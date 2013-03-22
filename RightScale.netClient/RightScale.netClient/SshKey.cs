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
