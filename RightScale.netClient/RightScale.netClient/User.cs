using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// RightScale User object
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeUser.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceUsers.html
    /// </summary>
    public class User:Core.RightScaleObjectBase<User>
    {
        #region User Properties

        /// <summary>
        /// Company name of specified user
        /// </summary>
        public string company { get; set; }

        /// <summary>
        /// Timestamp indicating when user was created
        /// </summary>
        public string created_at { get; set; }

        /// <summary>
        /// Timestamp indicating when user was last updated
        /// </summary>
        public string updated_at { get; set; }

        /// <summary>
        /// Last name of the user specified
        /// </summary>
        public string last_name { get; set; }

        /// <summary>
        /// Phone number of the user specified
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// First name fo the user specified
        /// </summary>
        public string first_name { get; set; }

        /// <summary>
        /// Email of the user specified
        /// </summary>
        public string email { get; set; }

        #endregion

        #region User.ctor
        /// <summary>
        /// Default Constructor for User
        /// </summary>
        public User()
            : base()
        {
        }
        
        #endregion

        #region User.index methods

        /// <summary>
        /// List the users available to the the account the user is logged in. Therefore, to list the users of a child account, the user has to login to the child account first.
        /// </summary>
        /// <returns>List of User objects belonging to the logged in account</returns>
        public static List<User> index()
        {
            return index(null);
        }

        /// <summary>
        /// List the users available to the the account the user is logged in. Therefore, to list the users of a child account, the user has to login to the child account first.
        /// </summary>
        /// <param name="filter">Filters to limit the retun set</param>
        /// <returns>List of User objects belonging to the logged in account</returns>
        public static List<User> index(List<Filter> filter)
        {
            List<string> validFilters = new List<string>() { "email", "first_name", "last_name" };
            Utility.CheckFilterInput("filter", validFilters, filter);
            string queryStringValue = string.Empty;

            if (filter != null)
            {
                foreach (Filter f in filter)
                {
                    queryStringValue += f.ToString() + "&";
                }
            }

            string jsonString = Core.APIClient.Instance.Get(APIHrefs.User, queryStringValue);
            return deserializeList(jsonString);
        }
        #endregion

        #region User.show methods

        /// <summary>
        /// Show information about a single user.
        /// </summary>
        /// <param name="userID">numeric ID of user from user href</param>
        /// <returns>instance of a specific User object</returns>
        public static User show(string userID)
        {
            string getHref = string.Format(APIHrefs.UserByID, userID);
            string jsonString = Core.APIClient.Instance.Get(getHref);
            return deserialize(jsonString);
        }

        #endregion

        #region User.create methods

        /// <summary>
        /// Create a user. If a user already exists with the same email, that user will be returned.
        /// Creating a user alone will not enable the user to access this account. You have to create 'permissions' for that user before it can be used. Performing a 'show' on a new user will fail unless you immediately create an 'observer' permission on the current account.
        /// Note that information about users and their permissions must be propagated globally across all RightScale clusters, and this can take some time (less than 60 seconds under normal cirucmstances) so the users you create may not be able to login for a minute or two after you create them. However, you may create or destroy permissions for newly-created users with no delay.
        /// To create a user that will login using password authentication, include the 'password' parameter with your request.
        /// To create an SSO-enabled user, you must specify the identity_provider that will be vouching for this user's identity, as well as the principal_uid (SAML NameID or OpenID identity URL) that the identity provider will assert for this user. Identity providers should be specified by their API href; you can obtain a list of the identity providers available to your account by invoking the 'index' action of the identity_providers API resource.
        /// </summary>
        /// <param name="email">Email of user</param>
        /// <param name="firstName">First name of user</param>
        /// <param name="lastName">Last name of user</param>
        /// <param name="company">User's company name</param>
        /// <param name="phone">Phone number for user</param>
        /// <param name="password">The password of this user. Required to create a password-authenticated user</param>
        /// <returns>ID of newly created User</returns>
        public static string create(string email, string firstName, string lastName, string company, string phone,string password)
        {
            return create(email, firstName, lastName, company, phone, password, null, null);
        }

        /// <summary>
        /// Create a user. If a user already exists with the same email, that user will be returned.
        /// Creating a user alone will not enable the user to access this account. You have to create 'permissions' for that user before it can be used. Performing a 'show' on a new user will fail unless you immediately create an 'observer' permission on the current account.
        /// Note that information about users and their permissions must be propagated globally across all RightScale clusters, and this can take some time (less than 60 seconds under normal cirucmstances) so the users you create may not be able to login for a minute or two after you create them. However, you may create or destroy permissions for newly-created users with no delay.
        /// To create a user that will login using password authentication, include the 'password' parameter with your request.
        /// To create an SSO-enabled user, you must specify the identity_provider that will be vouching for this user's identity, as well as the principal_uid (SAML NameID or OpenID identity URL) that the identity provider will assert for this user. Identity providers should be specified by their API href; you can obtain a list of the identity providers available to your account by invoking the 'index' action of the identity_providers API resource.
        /// </summary>
        /// <param name="email">Email of user</param>
        /// <param name="firstName">First name of user</param>
        /// <param name="lastName">Last name of user</param>
        /// <param name="phone">Phone number for user</param>
        /// <param name="identityProviderID">The RightScale API href ID of the Identity Provider through which this user will login to RightScale. Required to create an SSO-authenticated user.</param>
        /// <param name="password">The password of this user. Required to create a password-authenticated user</param>
        /// <param name="principalUid">The principal identifier (SAML NameID or OpenID identity URL) of this user. Required to create an SSO-authenticated user</param>
        /// <returns></returns>
        public static string create(string email, string firstName, string lastName, string company, string phone, string password, string identityProviderID, string principalUid)
        {
            Utility.CheckStringHasValue(email);
            Utility.CheckStringHasValue(firstName);
            Utility.CheckStringHasValue(lastName);
            Utility.CheckStringHasValue(phone);
            Utility.CheckStringHasValue(company);
            List<KeyValuePair<string, string>> paramSet = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrWhiteSpace(phone))
            {
                string phoneValidationRegex = @"^\d+$";
                Utility.CheckStringRegex("phone", phoneValidationRegex, phone);
            }
            Utility.addParameter(email, "user[email]", paramSet);
            Utility.addParameter(firstName, "user[first_name]", paramSet);
            Utility.addParameter(company, "user[company]", paramSet);
            Utility.addParameter(lastName, "user[last_name]", paramSet);
            Utility.addParameter(phone, "user[phone]", paramSet);
            Utility.addParameter(string.Format(APIHrefs.IdentityProviderByID, identityProviderID), "user[identity_provider_href]", paramSet);
            Utility.addParameter(password, "user[password]", paramSet);
            Utility.addParameter(principalUid, "user[principal_uid]", paramSet);

            return Core.APIClient.Instance.Post(APIHrefs.User, paramSet, "location").Last<string>().Split('/').Last<string>();
        }
        
        #endregion

        #region User.update methods

        /// <summary>
        /// Update a user's information, including SSO information. In order to update a user, they must be linked to one of the identity
        /// </summary>
        /// <param name="userID">ID of the user being updated</param>
        /// <param name="currentEmail">The existing email of this user.</param>
        /// <param name="newEmail">The updated email of this user.</param>
        /// <param name="firstName">Updated First Name for user</param>
        /// <param name="lastName">Updated Last Name for user</param>
        /// <param name="phone">Updated phone for user</param>
        /// <param name="identityProviderID">The updated RightScale API href ID of the associated Identity Provider.</param>
        /// <param name="password">Updated password for user</param>
        /// <param name="principalUid">The updated principal identifier (SAML NameID or OpenID identity URL) of this user</param>
        /// <returns></returns>
        public static bool update(string userID, string currentEmail, string newEmail, string firstName, string lastName, string phone, string identityProviderID, string password, string principalUid)
        {
            string putHref = string.Format(APIHrefs.UserByID, userID);
            List<KeyValuePair<string, string>> paramSet = new List<KeyValuePair<string, string>>();
            Utility.CheckStringHasValue(currentEmail);
            Utility.addParameter(currentEmail, "user[current_email]", paramSet);
            Utility.addParameter(newEmail, "user[new_email]", paramSet);
            Utility.addParameter(firstName, "user[first_name]", paramSet);
            Utility.addParameter(lastName, "user[last_name]", paramSet);
            Utility.addParameter(phone, "user[phone]", paramSet);
            Utility.addParameter(Utility.identityProviderHref(identityProviderID), "user[identity_provider_href]", paramSet);
            Utility.addParameter(password, "user[password]", paramSet);
            Utility.addParameter(principalUid, "user[principal_uid]", paramSet);
            return Core.APIClient.Instance.Put(putHref, paramSet);
        }

        #endregion

    }
}
