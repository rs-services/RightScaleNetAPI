using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class IdentityProvider : Core.RightScaleObjectBase<IdentityProvider>
    {
        //TODO: need to write this class



        #region IdentityProvider.ctor
        /// <summary>
        /// Default Constructor for IdentityProvider
        /// </summary>
        public IdentityProvider()
            : base()
        {
        }

        /// <summary>
        /// Constructor for IdentityProvider object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public IdentityProvider(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for IdentityProvider object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public IdentityProvider(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		
        
        #region IdentityProvider.index methods

        public static List<IdentityProvider> index()
        {
            return index(null, null);
        }

        public static List<IdentityProvider> index(List<Filter> filter)
        {
            return index(filter, null);
        }

        public static List<IdentityProvider> index(string view)
        {
            return index(null, view);
        }

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

            //TODO: implement IdentityProvider.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
