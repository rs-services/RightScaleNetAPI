using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class IPAddressBinding:Core.RightScaleObjectBase<IPAddressBinding>
    {
        public int private_port { get; set; }
        public string created_at { get; set; }
        public int public_port { get; set; }
        public string protocol { get; set; }
        public bool recurring { get; set; }

		#region IPAddressBinding.ctor
		/// <summary>
        /// Default Constructor for IPAddressBinding
        /// </summary>
		public IPAddressBinding():base()
        {
        }
		
        /// <summary>
        /// Constructor for IPAddressBinding object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public IPAddressBinding(string oAuthRefreshToken):base(oAuthRefreshToken)
        {
        }
		
        /// <summary>
        /// Cosntructor for IPAddressBinding object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public IPAddressBinding(string userName, string password, string accountNo):base(userName, password, accountNo)
        {
        }
		
		#endregion
		
    }
}
