using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class InstanceType
    {
        public string name { get; set; }
        public List<Action> actions { get; set; }
        public string resource_uid { get; set; }
        public string cpu_architecture { get; set; }
        public string local_disks { get; set; }
        public string memory { get; set; }
        public string local_disk_size { get; set; }
        public string cpu_count { get; set; }
        public string cpu_speed { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }
    }
}
