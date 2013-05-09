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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string href;

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string name;
        
        #region serializer methods

        protected static Tl xmlDeserialize<Tl>(string xmlString)
        {
            if (string.IsNullOrWhiteSpace(xmlString))
            {
                return default(Tl);
            }
            else
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Tl));
                    var retList = (Tl)serializer.Deserialize(new System.IO.StringReader(xmlString));
                    return retList;
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
