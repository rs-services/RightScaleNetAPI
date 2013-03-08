using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Cloud : Core.RightScaleObjectBase<Cloud>
    {
        public string name { get; set; }
        public string cloud_type { get; set; }
        public string description { get; set; }


        #region Cloud.ctor
        /// <summary>
        /// Default Constructor for Cloud
        /// </summary>
        public Cloud()
            : base()
        {
        }

        /// <summary>
        /// Constructor for Cloud object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public Cloud(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for Cloud object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public Cloud(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		
        #region Cloud.show() methods

        public static Cloud show(string cloudID)
        {
            Utility.CheckStringIsNumeric(cloudID);

            string getURL = string.Format("/api/clouds/{0}", cloudID);

            string jsonString = Core.APIClient.Instance.Get(getURL);

            return deserialize(jsonString);
        }

        #endregion

        #region Cloud.index methods

        public static List<Cloud> index()
        {
            return index(null);
        }

        public static List<Cloud> index(List<Filter> filter)
        {

            List<string> validFilters = new List<string>() { "cloud_type", "description", "name" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement Cloud.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
