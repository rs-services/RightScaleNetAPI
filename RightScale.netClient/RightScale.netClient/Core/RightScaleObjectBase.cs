using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient.Core
{
    public abstract class RightScaleObjectBase
    {
        public RightScaleObjectBase(string userName, string password, string accountNo)
        {
            APIClient.Instance.userName = userName;
            APIClient.Instance.password = password;
            APIClient.Instance.accountId = accountNo;
        }

        public RightScaleObjectBase(string oAuthRefreshToken)
        {
            APIClient.Instance.oauthRefreshToken = oAuthRefreshToken;
        }

        public RightScaleObjectBase()
        {

        }
    }
}
