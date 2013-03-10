using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// Link object is defined by a href, rel(name) and a reference to where this link is inherited from.
    /// </summary>
    public class Link
    {
        #region Link Properties

        /// <summary>
        /// URI fragment defining where this object can be queried from the RightScale API
        /// </summary>
        public string href { get; set; }

        /// <summary>
        /// Name of the object being linked
        /// </summary>
        public string rel { get; set; }

        /// <summary>
        /// Name of the source where this link was inherited from
        /// </summary>
        public string inherited_source { get; set; }

        #endregion
    }
}
