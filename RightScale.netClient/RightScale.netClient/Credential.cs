using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RightScale.netClient
{
    /// <summary>
    /// Helper object for list deserialization of Credential objects
    /// </summary>
    [XmlRoot("credentials")]
    public class CredentialList
    {
        /// <summary>
        /// List of Credential objects 
        /// </summary>
        [XmlElement("credential")]
        public List<Credential> credentialList { get; set; }
    }

    /// <summary>
    /// Secure string within the RightScale dashboard used for inputs to RightScale RightScripts and Recipes
    /// </summary>
    [XmlType(AnonymousType=true)]
    [XmlRoot("credential")]
    [Serializable]
    public class Credential : Core.RightScaleAPI10ObjectBase<Credential>
    {
        /// <summary>
        /// Value of the credential itself - will be masked at runtime
        /// </summary>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string value { get; set; }

        /// <summary>
        /// Description of the credential
        /// </summary>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string description { get; set; }
        
        /// <summary>
        /// Index call returns a list of Credential objects
        /// </summary>
        /// <returns>List of all Credential objects for the current account context</returns>
        public static List<Credential> index()
        {
            string xmlString = Core.API10Client.Instance.Get(string.Format(APIHrefs.Credential, Core.API10Client.Instance.accountId));
            CredentialList credList = xmlDeserialize<CredentialList>(xmlString);
            return credList.credentialList;
        }

        /// <summary>
        /// Show call returns a single Credential object
        /// </summary>
        /// <param name="credentialID">ID of the Credential to return</param>
        /// <returns>Credential related to the ID specified</returns>
        public static Credential show(string credentialID)
        {
            string xmlString = Core.API10Client.Instance.Get(string.Format(APIHrefs.CredentialByID, Core.API10Client.Instance.accountId, credentialID));
            return xmlDeserialize<Credential>(xmlString);
        }

        /// <summary>
        /// Update call consisting of name and value updates
        /// </summary>
        /// <param name="credentialID">ID of the Credential to update</param>
        /// <param name="name">new name of the Credential</param>
        /// <param name="value">new value for the Credential</param>
        /// <returns>true if updated, false if not</returns>
        public static bool update(string credentialID, string name, string value)
        {
            return update(credentialID, name, value, null);
        }

        /// <summary>
        /// Update call consisting of the name, value and description
        /// </summary>
        /// <param name="credentialID">ID of the Credential to update</param>
        /// <param name="name">new name of the Credential</param>
        /// <param name="value">new value for the Credential</param>
        /// <param name="description">new description for the Credential</param>
        /// <returns>true if updated, false if not</returns>
        public static bool update(string credentialID, string name, string value, string description)
        {
            string putHref = string.Format(APIHrefs.CredentialByID, Core.API10Client.Instance.accountId, credentialID);
            List<KeyValuePair<string, string>> putParams = new List<KeyValuePair<string, string>>();
            Utility.addParameter(name, "credential[name]", putParams);
            Utility.addParameter(value, "credential[value]", putParams);
            Utility.addParameter(description, "credential[description]", putParams);
            return Core.API10Client.Instance.Put(putHref, putParams);
        }

        /// <summary>
        /// Destroys a specific Credential by ID
        /// </summary>
        /// <param name="credentialID">ID of the Credential to destroy</param>
        /// <returns>true if updated, false if not</returns>
        public static bool destroy(string credentialID)
        {
            string deleteHref = string.Format(APIHrefs.CredentialByID, Core.API10Client.Instance.accountId, credentialID);
            return Core.API10Client.Instance.Delete(deleteHref);
        }

        /// <summary>
        /// Creates a new Credential with a name and value
        /// </summary>
        /// <param name="name">name for the new Credential</param>
        /// <param name="value">value for the new Credential</param>
        /// <returns>string ID of the newly created Credential</returns>
        public static string create(string name, string value)
        {
            return create(name, value, null);
        }

        /// <summary>
        /// Creates a new Credential with a name and value
        /// </summary>
        /// <param name="name">name for the new Credential</param>
        /// <param name="value">value for the new Credential</param>
        /// <param name="description">description for the new Credential</param>
        /// <returns>string ID of the newly created Credential</returns>
        public static string create(string name, string value, string description)
        {
            string postHref = string.Format(APIHrefs.Credential, Core.API10Client.Instance.accountId);
            List<KeyValuePair<string, string>> putParams = new List<KeyValuePair<string, string>>();
            Utility.addParameter(name, "credential[name]", putParams);
            Utility.addParameter(value, "credential[value]", putParams);
            Utility.addParameter(description, "credential[description]", putParams);
            return Core.API10Client.Instance.Post(postHref, putParams, "Location").Last<string>().Split('/').Last<string>();
        }
    }
}
