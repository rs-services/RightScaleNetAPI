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
		
		#endregion

        #region IPAddressBinding Relationships

        /// <summary>
        /// Instance for this IPAddressBinding
        /// </summary>
        public Instance instance
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("instance"));
                return Instance.deserialize(jsonString);
            }
        }

        /// <summary>
        /// IPAddress for this IPAddressBinding
        /// </summary>
        public IPAddress ipAddress
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("ip_address"));
                return IPAddress.deserialize(jsonString);
            }
        }

        #endregion
    }
}
