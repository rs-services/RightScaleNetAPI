using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RightScale.netClient.Core
{
    /// <summary>
    /// Base class for all RS-based API objects.  Centralizes logic for links and actions and handles constructor logic for authentication stuff
    /// </summary>
    /// <typeparam name="T">Type of the derived class--used to manage proper serialization</typeparam>
    public class RightScaleObjectBase<T>
    {
        #region RightScaleObjectBase common properties
        /// <summary>
        /// Generic collection of Action objects accompanying a given RightScale API Object
        /// Defines what actions are currently possible on the current object instance
        /// </summary>
        public List<Action> actions { get; set; }
        
        /// <summary>
        /// Generic collection of Link objects accompanying a given RightScale API Object
        /// Links contain references to other sets of objects and are where object ID's are derived from
        /// </summary>
        public List<Link> links { get; set; }

        /// <summary>
        /// ID of the object <typeparamref name="T"/>
        /// </summary>
        public string ID
        {
            get
            {
                return getLinkIDValue("self");
            }
        }

        /// <summary>
        /// Href of the object <typeparam name="T"/>
        /// </summary>
        public string href
        {
            get
            {
                return getLinkValue("self");
            }
        }

        #endregion

        #region Common get methods for all classes

        protected static T GetObjectByHref(string href)
        {
            return GetObjectByHref(href, string.Empty);
        }

        protected static T GetObjectByHref(string href, string queryString)
        {
            Utility.CheckStringHasValue(href);
            string jsonString = Core.APIClient.Instance.Get(href, queryString);
            return deserialize(jsonString);
        }

        protected static List<T> GetObjectListByHref(string href)
        {
            return GetObjectListByHref(href, string.Empty);
        }

        protected static List<T> GetObjectListByHref(string href, string queryString)
        {
            Utility.CheckStringHasValue(href);
            string jsonString = Core.APIClient.Instance.Get(href, queryString);
            return deserializeList(jsonString);
        }

        #endregion

        /// <summary>
        /// Method populates the values for a given instance of a class if a resource href is already provided
        /// </summary>
        public virtual void populateObject()
        {
            populateObject(getLinkValue("self"));
        }

        /// <summary>
        /// Method populates the values for a given instance of a class
        /// </summary>
        /// <param name="getHref">RightScale API Href to get the values for this class from</param>
        public virtual void populateObject(string getHref)
        {
            Utility.CheckStringHasValue(getHref);
            string jsonString = Core.APIClient.Instance.Get(getHref);
            JsonConvert.PopulateObject(jsonString, this);
        }

        /// <summary>
        /// Centralized method to pull ID's from link values within the links collection of this object
        /// </summary>
        /// <param name="linkName">name of the link to be queried</param>
        /// <returns>ID at the back end of the href for the given link</returns>
        internal string getLinkIDValue (string linkName)
        {
            var idToReturn = getLinkValue(linkName);
            return idToReturn.Split('/').Last<string>();
        }

        /// <summary>
        /// Centralized method to get specific link reference
        /// </summary>
        /// <param name="linkName">name of the link to retrieve</param>
        /// <returns>href value for link</returns>
        internal string getLinkValue(string linkName)
        {
            foreach (Link l in links)
            {
                if (l.rel == linkName)
                {
                    return l.href;
                }
            }
            return null;
        }

        /// <summary>
        /// Public method for setting username/password/accountNo authentication information in the API Caller from the context of the object
        /// </summary>
        /// <param name="userName">emmail address for logging into the RightScale API</param>
        /// <param name="password">password for the given account used for authenticating to the RightScale API</param>
        /// <param name="accountNo">Account Number to perform API actions against</param>
        public void SetAuthCreds(string userName, string password, string accountNo)
        {
            APIClient.Instance.InitWebClient();
            APIClient.Instance.userName = userName;
            APIClient.Instance.password = password;
            APIClient.Instance.accountId = accountNo;
        }

        /// <summary>
        /// Public method for setting OAuth2 authentication information in the API Caller from the context of the object
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale oAuth2 refresh token</param>
        public void SetAuthCreds(string oAuthRefreshToken)
        {
            APIClient.Instance.oauthRefreshToken = oAuthRefreshToken;
        }
        
        #region RightScaleObjectBase.ctor
        
        /// <summary>
        /// RightScaleObjectBase default constructor.  By using this constructor it is implied that authentication information will be placed in the application's configuration file
        /// </summary>
        public RightScaleObjectBase()
        {

        }

        #endregion

        #region Json deserialization helpers

        /// <summary>
        /// Method deserializes a Json string into a single instance of the derived class defined by <typeparamref name="T"/>
        /// </summary>
        /// <param name="jsonString">Json String to deserialize into an instance of <typeparamref name="T"/></param>
        /// <returns>Instance of type <typeparamref name="T"/></returns>
        protected static T deserialize(string jsonString)
        {
            return objectDeserialize(jsonString);
        }

        /// <summary>
        /// Method deserializes a Json string into a generic list of objects of the derived class defined by <typeparamref name="T"/>
        /// </summary>
        /// <param name="jsonString">Json string to deserialize into a list of <typeparamref name="T"/></param>
        /// <returns>Generic list of <typeparamref name="T"/></returns>
        protected static List<T> deserializeList(string jsonString)
        {
            return listDeserialize(jsonString);
        }

        /// <summary>
        /// Method deserializes a Json string into a single instance of the derived class defined by <typeparamref name="T"/>
        /// This method is accessible by other classes in this library for classes that return collections of other types of classes
        /// </summary>
        /// <param name="jsonString">Json string to deserialize into a list of <typeparamref name="T"/></param>
        /// <returns></returns>
        internal static T populateObjectFromJson(string jsonString)
        {
            return objectDeserialize(jsonString);
        }

        /// <summary>
        /// Method deserializes a Json string into a generic list of objects of the derived class defined by <typeparamref name="T"/>
        /// This method is accessible by other classes in this library for classes that return collections of other types of classes
        /// </summary>
        /// <param name="jsonString">Json string to deserialize into a list of <typeparamref name="T"/></param>
        /// <returns></returns>
        internal static List<T> populateObjectListFromJson(string jsonString)
        {
            return listDeserialize(jsonString);
        }

        /// <summary>
        /// Centralized private method for handling deserialization of Json into the proper generic list of type defined by <typeparamref name="T"/>
        /// </summary>
        /// <param name="jsonString">Json string to deserialize into a list of <typeparamref name="T"/></param>
        /// <returns>Generic list of type <typeparamref name="T"/></returns>
        private static List<T> listDeserialize(string jsonString)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                return new List<T>();
            }
            else
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

        /// <summary>
        /// Centralized private method for handling deserialization of Json into the proper type defined by <typeparamref name="T"/>
        /// </summary>
        /// <param name="jsonString">Json string to deserizlize into an instance of <typeparamref name="T"/></param>
        /// <returns>Instance of type <typeparamref name="T"/></returns>
        private static T objectDeserialize(string jsonString)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                return default(T);
            }
            else
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
        }

        #endregion
    }
}
