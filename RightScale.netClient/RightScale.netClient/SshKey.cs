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

        /// <summary>
        /// Lists ssh keys.
        /// </summary>
        /// <param name="cloudID">ID of cloud to query</param>
        /// <returns>List of SSHKey objects</returns>
        public static List<SshKey> index(string cloudID)
        {
            return index(cloudID, null, null);
        }

        /// <summary>
        /// Lists ssh keys.
        /// </summary>
        /// <param name="cloudID">ID of cloud to query</param>
        /// <param name="filter">Set of filters to limit return data set</param>
        /// <returns>List of SSHKey objects</returns>
        public static List<SshKey> index(string cloudID, List<Filter> filter)
        {
            return index(cloudID ,filter, null);
        }

        /// <summary>
        /// Lists ssh keys.
        /// </summary>
        /// <param name="cloudID">ID of cloud to query</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>List of SSHKey objects</returns>
        public static List<SshKey> index(string cloudID, string view)
        {
            return index(cloudID, null, view);
        }

        /// <summary>
        /// Lists ssh keys.
        /// </summary>
        /// <param name="cloudID">ID of cloud to query</param>
        /// <param name="filter">Set of filters to limit return data set</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>List of SSHKey objects</returns>
        public static List<SshKey> index(string cloudID, List<Filter> filter, string view)
        {
            view = validView(view);

            List<string> validFilters = new List<string>() { "resource_uid" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            string queryString = string.Empty;
            if (filter != null && filter.Count > 0)
            {
                foreach (Filter f in filter)
                {
                    queryString += f.ToString() + "&";
                }
            }

            queryString += string.Format("view={0}", view);
            string getHref = string.Format(APIHrefs.SshKey, cloudID);
            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserializeList(jsonString);

        }

        /// <summary>
        /// Internal helper method to get valid view string
        /// </summary>
        /// <param name="view">View string to test</param>
        /// <returns>Valid view string for Ssh Key object</returns>
        private static string validView(string view)
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
            return view;
        }
        #endregion

        #region SshKey.show methods

        /// <summary>
        /// Displays information about a single Ssh key
        /// </summary>
        /// <param name="cloudID">ID of cloud to query</param>
        /// <param name="sshKeyID">ID of SshKey to return</param>
        /// <returns>Single populated instance of SshKey object</returns>
        public static SshKey show(string cloudID, string sshKeyID)
        {
            return show(cloudID, sshKeyID, null);
        }

        /// <summary>
        /// Displays information about a single Ssh key
        /// </summary>
        /// <param name="cloudID">ID of cloud to query</param>
        /// <param name="sshKeyID">ID of SshKey to return</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>Single populated instance of SshKey object</returns>
        public static SshKey show(string cloudID, string sshKeyID, string view)
        {
            view = validView(view);
            string getHref = string.Format(APIHrefs.SshKeyByID, cloudID, sshKeyID);
            string queryString = string.Format("view={0}", view);
            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserialize(jsonString);
        }

        #endregion

        #region SshKey.create methods

        /// <summary>
        /// Creates a new ssh key
        /// </summary>
        /// <param name="cloudID">ID of cloud in which to create SSH key</param>
        /// <param name="name">name of Ssh key</param>
        /// <returns>ID of newly created SSH key</returns>
        public static string create(string cloudID, string name)
        {
            string postHref = string.Format(APIHrefs.SshKey, cloudID);
            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            Utility.addParameter(name, "ssh_key[name]", postParams);
            return Core.APIClient.Instance.Post(postHref, postParams, "location").Last<string>().Split('/').Last<string>();
        }

        #endregion

        #region SshKey.destroy methods

        /// <summary>
        /// Deletes a given ssh key
        /// </summary>
        /// <param name="cloudID">ID of cloud where SSH key exists</param>
        /// <param name="sshKeyID">ID of SSH key to delete</param>
        /// <returns>true if deleted, false if not</returns>
        public static bool destroy(string cloudID, string sshKeyID)
        {
            string deleteHref = string.Format(APIHrefs.SshKeyByID, cloudID, sshKeyID);
            return Core.APIClient.Instance.Delete(deleteHref);
        }

        #endregion
    }
}
