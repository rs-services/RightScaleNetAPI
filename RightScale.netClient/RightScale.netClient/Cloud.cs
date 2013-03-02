using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Cloud
    {
        public string name { get; set; }
        public string cloud_type { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }
    }
}
