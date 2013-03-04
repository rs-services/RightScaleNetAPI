using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    class ScheduleEntry
    {
        public int max_count { get; set; }
        public string time { get; set; }
        public int min_count { get; set; }
        public string day { get; set; }
    }
}
