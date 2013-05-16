using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Subnet : Core.RightScaleObjectBase<Subnet>
    {
        public string name { get; set; }
        public string visibility { get; set; }
        public string resource_uid { get; set; }
        public string description { get; set; }
    }
}
