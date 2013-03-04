using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Input
    {
        public string name { get; set; }
        public string value { get; set; }

        
        #region Input.index methods

        public static List<Input> index()
        {
            return index(null);
        }

        public static List<Input> index(string view)
        {
            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default", "inputs_2_0" };
                Utility.CheckStringInput("view", validViews, view);
            }

            //TODO: implement Input.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
