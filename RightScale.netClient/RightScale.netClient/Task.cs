using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Task:Core.RightScaleObjectBase<Task>
    {
        public string detail { get; set; }
        public string summary { get; set; }

        #region Task.ctor
        /// <summary>
        /// Default Constructor for Task
        /// </summary>
        public Task()
            : base()
        {
        }

        /// <summary>
        /// Constructor for Task object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public Task(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for Task object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public Task(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		
    }
}
