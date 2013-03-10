using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// Bounds define the upper and lower bounds for server counts within a ServerArray
    /// </summary>
    public class Bounds
    {
        /// <summary>
        /// Maximum number of servers within this instance of a Bounds object
        /// </summary>
        public string max_count { get; set; }

        /// <summary>
        /// Minimum number of servers within this instance of a Bounds object
        /// </summary>
        public string min_count { get; set; }
    }
}
