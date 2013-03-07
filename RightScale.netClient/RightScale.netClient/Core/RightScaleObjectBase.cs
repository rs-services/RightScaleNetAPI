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

        public List<Action> actions { get; set; }
        public List<Link> links { get; set; }

        /// <summary>
        /// Centralized method to pull ID's from link values within the links collection of this object
        /// </summary>
        /// <param name="linkName">name of the link to be queried</param>
        /// <returns>ID at the back end of the href for the given link</returns>
        protected string getLinkIDValue(string linkName)
        {
            var idToReturn = from link in links where link.rel == linkName select link.href;
            if (idToReturn.Count<string>() != 1)
            {
                return null;
            }
            return idToReturn.Last<string>().Split('/').Last<string>();
        }

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
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch (JsonReaderException jre)
            {
                throw new RightScaleAPIException("Error Deserializing String.  See error data property for the string that was attempted to be deserialized.", null, jsonString, jre);
            }
        }

        public static List<T> deserializeList(string jsonString)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(jsonString);
            }
            catch (JsonReaderException jre)
            {
                throw new RightScaleAPIException("Error Deserializing String.  See error data property for the string that was attempted to be deserialized.", null, jsonString, jre);
            }
        }
    }
}
