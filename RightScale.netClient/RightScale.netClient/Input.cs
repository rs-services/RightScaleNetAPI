using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Input :Core.RightScaleObjectBase<Input>
    {
        public string name { get; set; }
        public string value { get; set; }



        #region Input.ctor
        /// <summary>
        /// Default Constructor for Input
        /// </summary>
        public Input()
            : base()
        {
        }

        /// <summary>
        /// Constructor for Input object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public Input(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for Input object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public Input(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		
        
        #region Input.index methods

        public static List<Input> index()
        {
            return index(null);
        }

        public static List<Input> index(string view)
        {
            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default", "inputs_2_0" };
                Utility.CheckStringInput("view", validViews, view);
            }

            //TODO: implement Input.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
