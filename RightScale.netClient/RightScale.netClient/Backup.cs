using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Backup
    {
        public string name { get; set; }
        public string lineage { get; set; }
        public List<Action> actions { get; set; }
        public string created_at { get; set; }
        public List<VolumeSnapshot> volume_snapshots { get; set; }
        public bool completed { get; set; }
        public bool from_master { get; set; }
        public int volume_snapshot_count { get; set; }
        public bool committed { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }
    }
}
