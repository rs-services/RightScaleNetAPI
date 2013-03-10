using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// Schedule-based bounds for a ServerArray defining the minimum and maximum size of a ServerArray by day and time
    /// </summary>
    public class ScheduleEntry : Bounds
    {
        /// <summary>
        /// Time for which this set of bounds is valid
        /// </summary>
        public string time { get; set; }

        /// <summary>
        /// Day for which this set of bounds is valid
        /// </summary>
        public string day { get; set; }
    }
}
