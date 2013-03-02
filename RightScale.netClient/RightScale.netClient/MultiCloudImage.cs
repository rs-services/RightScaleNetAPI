using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class MultiCloudImage
    {
        public string name { get; set; }
        public List<Action> actions { get; set; }
        public int revision { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }
    }
}
