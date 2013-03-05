using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RightScale.netClient.Core
{
    public class RightScaleObjectBase<T>
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

        public static T deserialize(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static List<T> deserializeList(string jsonString)
        {
            return JsonConvert.DeserializeObject<List<T>>(jsonString);
        }
    }
}
