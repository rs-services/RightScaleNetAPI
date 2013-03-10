using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// AlertSpecificParams define the parameters for controling AlertSpec behavior
    /// </summary>
    public class AlertSpecificParams
    {
        /// <summary>
        /// Tag predicate for vote tags
        /// </summary>
        public string voters_tag_predicate { get; set; }

        /// <summary>
        /// Decision threshold for AlertSpec
        /// </summary>
        public string decision_threshold { get; set; }
    }
}
