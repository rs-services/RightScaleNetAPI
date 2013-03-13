using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace RightScale.netClient
{
    /// <summary>
    /// Serialization Extension static class holds helper methods for managing serialization and deserialization via XML
    /// </summary>
    public static class SerializationExtensions
    {
        /// <summary>
        /// Generic XML Serializer
        /// </summary>
        /// <typeparam name="T">Type being serialized</typeparam>
        /// <param name="toSerialize">Object being serialized</param>
        /// <returns>Serialized string representation of the object passed in</returns>
        public static string Serialize<T>(T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());
            string serializedValue = string.Empty;
            using (StringWriter sw = new StringWriter())
            {
                xmlSerializer.Serialize(sw, toSerialize);
                sw.Flush();
                serializedValue = sw.ToString();
            }
            return serializedValue;
        }

        /// <summary>
        /// Generic XML Deserializer
        /// </summary>
        /// <typeparam name="T">Type being deserialized to</typeparam>
        /// <param name="toDeserialize">String to deserialize</param>
        /// <returns>Instance of object of type T</returns>
        public static T Deserialize<T>(string toDeserialize)
        {
            T retVal = default(T);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringReader sr = new StringReader(toDeserialize))
            {
                retVal = (T)(xmlSerializer.Deserialize(sr));
            }
            return retVal;
        }
    }
}
