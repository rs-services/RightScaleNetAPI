using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// Elasticity Params specify the parameters around which a ServerArray will scale up and down
    /// </summary>
    public class ElasticityParams
    {
        #region ElasticityParams Properties

        /// <summary>
        /// Alert-specific parameters defining how a ServerArray will scale up and down
        /// </summary>
        public AlertSpecificParams alert_specific_params { get; set; }

        /// <summary>
        /// Bounds between which a ServerArray will scale up and down
        /// </summary>
        public Bounds bounds { get; set; }

        /// <summary>
        /// Pace at which a ServerArray will scale up and down
        /// </summary>
        public Pacing pacing { get; set; }

        /// <summary>
        /// ScheduleEntries defining time-based rules for when this specific set of ElasticityParams will be valid
        /// </summary>
        public List<ScheduleEntry> schedule_entries { get; set; }

        #endregion
    }
}
