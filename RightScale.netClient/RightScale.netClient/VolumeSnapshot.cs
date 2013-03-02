using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    //volume_snapshot
    public class VolumeSnapshot
    {
        public string device { get; set; }
        public string position { get; set; }
        public string resource_uid { get; set; }
        public object size { get; set; }
        public bool committed { get; set; }
        public List<Link> links { get; set; }
        public string state { get; set; }
    }
}
