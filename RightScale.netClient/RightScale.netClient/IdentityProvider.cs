using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// An Identity Provider represents a SAML identity provider (IdP) that is linked to your RightScale Enterprise account, and is trusted by the RightScale dashboard to authenticate your enterprise's end users. To register an Identity Provider, contact your account manager.
    /// Once your provider is registered with RightScale and associated with your enterprise account, you can use the provisioning API to query the IdentityProviders resource and discover your provider's API href. You can use the href to create new users via provisioning API who are pre-linked to your SAML identity provider and can perform single sign-on immediately, without accepting an invitation or choosing a password.
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeIdentityProvider.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceIdentityProviders.html
    /// </summary>
    public class IdentityProvider : Core.RightScaleObjectBase<IdentityProvider>
    {
        #region IdentityProvider properties
        /// <summary>
        /// SAML or OAuth discovery hint
        /// </summary>
        public string discovery_hint { get; set; }

        /// <summary>
        /// Name of this Identity Provider
        /// </summary>
        public string name { get; set; }

        #endregion

        #region IdentityProvider.ctor
        /// <summary>
        /// Default Constructor for IdentityProvider
        /// </summary>
        public IdentityProvider()
            : base()
        {
        }
        #endregion
		        
        #region IdentityProvider.index methods

        /// <summary>
        /// Lists the Identity providers associated with this enterprise account
        /// </summary>
        /// <returns>List of IdentityProveder objects</returns>
        public static List<IdentityProvider> index()
        {
            return index(null, null);
        }

        /// <summary>
        /// Lists the Identity providers associated with this enterprise account
        /// </summary>
        /// <param name="filter">Set of filters for restricting the return set of Identity Provider objects</param>
        /// <returns>List of IdentityProveder objects</returns>
        public static List<IdentityProvider> index(List<Filter> filter)
        {
            return index(filter, null);
        }

        /// <summary>
        /// Lists the Identity providers associated with this enterprise account
        /// </summary>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>List of IdentityProveder objects</returns>
        public static List<IdentityProvider> index(string view)
        {
            return index(null, view);
        }

        /// <summary>
        /// Lists the Identity providers associated with this enterprise account
        /// </summary>
        /// <param name="filter">Set of filters for restricting the return set of Identity Provider objects</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>List of IdentityProveder objects</returns>
        public static List<IdentityProvider> index(List<Filter> filter, string view)
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

            string queryString = string.Empty;

            if (filter != null && filter.Count > 0)
            {
                foreach (Filter f in filter)
                {
                    queryString += f.ToString() + "&";
                }
            }

            queryString += string.Format("view={0}", view);

            string jsonString = Core.APIClient.Instance.Get(APIHrefs.IdentityProvider, queryString);
            return deserializeList(jsonString);
        }
        #endregion

        #region IdentityProvider.show methods

        /// <summary>
        /// Show the specified identity provider, if associated with this enterprise account.
        /// </summary>
        /// <param name="identityProviderID"></param>
        /// <returns>Populated instance of an IndentityProvider object</returns>
        public static IdentityProvider show(string identityProviderID)
        {
            string getHref = string.Format(APIHrefs.IdentityProviderByID, identityProviderID);
            string jsonString = Core.APIClient.Instance.Get(getHref);
            return deserialize(jsonString);
        }

        #endregion
    }
}
