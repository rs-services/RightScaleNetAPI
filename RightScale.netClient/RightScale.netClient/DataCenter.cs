using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class DataCenter : Core.RightScaleObjectBase<DataCenter>
    {
        public string name { get; set; }
        public List<Action> actions { get; set; }
        public string resource_uid { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }

        #region DataCenter.ctor
        /// <summary>
        /// Default Constructor for DataCenter
        /// </summary>
        public DataCenter()
            : base()
        {
        }

        /// <summary>
        /// Constructor for DataCenter object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public DataCenter(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for DataCenter object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public DataCenter(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		

        #region DataCenter.show() methods

        public static DataCenter show(string cloudID, string dataCenterID)
        {
            Utility.CheckStringIsNumeric(cloudID);
            Utility.CheckStringIsNumeric(dataCenterID);

            string getURL = string.Format("/api/clouds/{0}/datacenters/{1}", cloudID, dataCenterID);

            string jsonString = Core.APIClient.Instance.Get(getURL);

            return deserialize(jsonString);
        }

        #endregion

        #region DataCenter.index methods

        public static List<DataCenter> index()
        {
            return index(null, null);
        }

        public static List<DataCenter> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        public static List<DataCenter> index(string view)
        {
            return index(null, view);
        }

        public static List<DataCenter> index(List<KeyValuePair<string, string>> filter, string view)
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

            List<string> validFilters = new List<string>() { "name", "resource_uid" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement DataCenter.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
