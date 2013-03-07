using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class PublicationLineage:Core.RightScaleObjectBase<PublicationLineage>
    {
        public string name { get; set; }
        public int created_at { get; set; }
        public bool comments_enabled { get; set; }
        public string short_description { get; set; }
        public int updated_at { get; set; }
        public string long_description { get; set; }
        public bool comments_emailed { get; set; }



        #region PublicationLineage.ctor
        /// <summary>
        /// Default Constructor for PublicationLineage
        /// </summary>
        public PublicationLineage()
            : base()
        {
        }

        /// <summary>
        /// Constructor for PublicationLineage object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public PublicationLineage(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for PublicationLineage object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public PublicationLineage(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		
    }
}
