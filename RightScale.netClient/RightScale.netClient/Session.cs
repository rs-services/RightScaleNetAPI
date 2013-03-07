using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Session:Core.RightScaleObjectBase<Session>
    {
        public string message { get; set; }

        #region Session.ctor
        /// <summary>
        /// Default Constructor for Session
        /// </summary>
        public Session()
            : base()
        {
        }

        /// <summary>
        /// Constructor for Session object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public Session(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for Session object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public Session(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		

        public static List<Session> index()
        {
            //TODO: implement Session.index
            throw new NotImplementedException();
        }
    }
}
