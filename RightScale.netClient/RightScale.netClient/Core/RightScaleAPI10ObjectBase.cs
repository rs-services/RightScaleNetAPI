using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RightScale.netClient.Core
{
    /// <summary>
    /// Object base for RightScale API 1.0-based objects
    /// </summary>
    /// <typeparam name="T">Type of object - used for deserialization processes</typeparam>
    public class RightScaleAPI10ObjectBase<T>
    {
        public string href;

        public string name;

        public string created_at;

        public string updated_at;

        #region serializer methods

        /// <summary>
        /// Method deserializes a XML string into a single instance of the derived class defined by <typeparamref name="T"/>
        /// </summary>
        /// <param name="xmlString">XML String to deserialize into an instance of <typeparamref name="T"/></param>
        /// <returns>Instance of type <typeparamref name="T"/></returns>
        protected static T deserialize(string xmlString)
        {
            return objectDeserialize(xmlString);
        }

        /// <summary>
        /// Method deserializes a XML string into a generic list of objects of the derived class defined by <typeparamref name="T"/>
        /// </summary>
        /// <param name="xmlString">XML string to deserialize into a list of <typeparamref name="T"/></param>
        /// <returns>Generic list of <typeparamref name="T"/></returns>
        protected static List<T> deserializeList(string xmlString)
        {
            return listDeserialize(xmlString);
        }

        /// <summary>
        /// Method deserializes a XML string into a single instance of the derived class defined by <typeparamref name="T"/>
        /// This method is accessible by other classes in this library for classes that return collections of other types of classes
        /// </summary>
        /// <param name="xmlString">XML string to deserialize into a list of <typeparamref name="T"/></param>
        /// <returns></returns>
        internal static T populateObjectFromJson(string xmlString)
        {
            return objectDeserialize(xmlString);
        }

        /// <summary>
        /// Method deserializes a XML string into a generic list of objects of the derived class defined by <typeparamref name="T"/>
        /// This method is accessible by other classes in this library for classes that return collections of other types of classes
        /// </summary>
        /// <param name="xmlString">XML string to deserialize into a list of <typeparamref name="T"/></param>
        /// <returns></returns>
        internal static List<T> populateObjectListFromJson(string xmlString)
        {
            return listDeserialize(xmlString);
        }

        /// <summary>
        /// Centralized private method for handling deserialization of XML into the proper generic list of type defined by <typeparamref name="T"/>
        /// </summary>
        /// <param name="xmlString">XML string to deserialize into a list of <typeparamref name="T"/></param>
        /// <returns>Generic list of type <typeparamref name="T"/></returns>
        private static List<T> listDeserialize(string xmlString)
        {
            if (string.IsNullOrWhiteSpace(xmlString))
            {
                return new List<T>();
            }
            else
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                    return (List<T>)serializer.Deserialize(new System.IO.StringReader(xmlString));
                }
                catch (InvalidOperationException ioe)
                {
                    throw new RightScaleAPIException("Error Deserializing String.  See error data property for the string that was attempted to be deserialized.", null, xmlString, ioe);
                }
            }
        }

        /// <summary>
        /// Centralized private method for handling deserialization of XML into the proper type defined by <typeparamref name="T"/>
        /// </summary>
        /// <param name="xmlString">XML string to deserizlize into an instance of <typeparamref name="T"/></param>
        /// <returns>Instance of type <typeparamref name="T"/></returns>
        private static T objectDeserialize(string xmlString)
        {
            if (string.IsNullOrWhiteSpace(xmlString))
            {
                return default(T);
            }
            else
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(new System.IO.StringReader(xmlString));
                }
                catch (InvalidOperationException ioe)
                {
                    throw new RightScaleAPIException("Error Deserializing String.  See error data property for the string that was attempted to be deserialized.", null, xmlString, ioe);
                }
            }
        }

        #endregion
    }
}
