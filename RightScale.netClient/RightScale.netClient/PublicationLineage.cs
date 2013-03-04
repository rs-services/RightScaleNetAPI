using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class PublicationLineage
    {
        public string name { get; set; }
        public List<Action> actions { get; set; }
        public int created_at { get; set; }
        public bool comments_enabled { get; set; }
        public string short_description { get; set; }
        public int updated_at { get; set; }
        public string long_description { get; set; }
        public bool comments_emailed { get; set; }
        public List<Link> links { get; set; }
    }
}
