using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Tag:Core.RightScaleObjectBase<Tag>
    {
        public string name { get; set; }

        #region Tag.ctor
        /// <summary>
        /// Default Constructor for Tag
        /// </summary>
        public Tag()
            : base()
        {
        }

        /// <summary>
        /// Constructor for Tag object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public Tag(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for Tag object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public Tag(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		
    }
}
