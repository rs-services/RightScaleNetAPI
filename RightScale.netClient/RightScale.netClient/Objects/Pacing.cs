using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// Pacing defines the pace or rate at which a ServerArray will scale up and down 
    /// </summary>
    public class Pacing
    {
        #region Pacing Properties

        /// <summary>
        /// Number of servers to terminate at a time when scaling down (until minimum # of servers is reached)
        /// </summary>
        public string resize_down_by { get; set; }

        /// <summary>
        /// Number of servers to launch at a time when scaling up (until maximum # of servers is reached)
        /// </summary>
        public string resize_up_by { get; set; }
        
        /// <summary>
        /// Time between scaling events 
        /// </summary>
        public string resize_calm_time { get; set; }

        #endregion
    }
}
