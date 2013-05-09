using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Credential : Core.RightScaleAPI10ObjectBase<Credential>
    {
        
        public static void index()
        {
            string xmlString = Core.API10Client.Instance.Get(string.Format(APIHrefs.Credential, Core.API10Client.Instance.accountId));

        }


        /*
        public static Credential show(string credentialID)
        {
            
        }

        public static bool update(string credentialID, string name, string value)
        {
            return update(credentialID, name, value, null);
        }

        public static bool update(string credentialID, string name, string value, string description)
        {

        }

        public static bool destroy(string credentialID)
        {

        }

        public static string create(string name, string value)
        {
            return create(name, value, null);
        }

        public static string create(string name, string value, string description)
        {

        }*/
    }
}
