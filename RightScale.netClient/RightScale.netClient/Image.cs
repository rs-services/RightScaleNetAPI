using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Image
    {
        public string name { get; set; }
        public List<Action> actions { get; set; }
        public string resource_uid { get; set; }
        public string cpu_architecture { get; set; }
        public string image_type { get; set; }
        public string virtualization_type { get; set; }
        public string os_platform { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }
        public string visibility { get; set; }
    }
}
