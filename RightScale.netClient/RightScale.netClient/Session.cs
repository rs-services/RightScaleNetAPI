using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Session
    {
        public List<Action> actions { get; set; }
        public List<Link> links { get; set; }
        public string message { get; set; }

        public static List<Session> index()
        {
            //TODO: implement Session.index
            throw new NotImplementedException();
        }
    }
}
