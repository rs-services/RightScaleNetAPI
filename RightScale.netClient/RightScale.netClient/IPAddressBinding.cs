using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class IPAddressBinding
    {
        public int private_port { get; set; }
        public string created_at { get; set; }
        public int public_port { get; set; }
        public string protocol { get; set; }
        public List<Link> links { get; set; }
        public bool recurring { get; set; }
    }
}
