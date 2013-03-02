using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Deployment
    {
        public string name { get; set; }
        public List<Action> actions { get; set; }
        public string server_tag_scope { get; set; }
        public List<Input> inputs { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }
    }
}
