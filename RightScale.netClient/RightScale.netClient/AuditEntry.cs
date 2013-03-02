using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    //audit_entry
    public class AuditEntry
    {
        public List<Action> actions { get; set; }
        public string updated_at { get; set; }
        public List<Link> links { get; set; }
        public string summary { get; set; }
        public int detail_size { get; set; }
        public string user_email { get; set; }
    }
}
