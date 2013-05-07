using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class RightScript : Core.RightScaleAPI10ObjectBase<RightScript>
    {
        public string description;

        public string script;

        /*
        public static List<RightScript> index()
        {
            string jsonString = Core.API10Client.Instance.Get("/api/acct/58765/right_scripts");
            return deserializeList(jsonString);
        }

        public static RightScript show(string rightScriptID)
        {

        }*/
    }
}
