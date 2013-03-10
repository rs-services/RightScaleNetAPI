using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// Actions are the processes that are allowable for a given object at a given point in time
    /// </summary>
    public class Action
    {
        #region Action Properties

        /// <summary>
        /// Name of the action 
        /// </summary>
        public string rel { get; set; }

        #endregion
    }
}
