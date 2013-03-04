using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Task
    {
        public List<Action> actions { get; set; }
        public List<Link> links { get; set; }
        public string detail { get; set; }
        public string summary { get; set; }
    }
}
