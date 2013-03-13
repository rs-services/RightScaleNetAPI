using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// Object containing DataCenter Policy information for ServerArray Configurations
    /// </summary>
    public class DataCenterPolicy
    {
        #region DataCenterPolicy properties

        /// <summary>
        /// ID of the DataCenter 
        /// </summary>
        public string dataCenterId;

        /// <summary>
        /// Maximum number of instances (0 for unlimited)
        /// </summary>
        public string max;
        
        /// <summary>
        /// Instance allocation (total should be 100%)
        /// </summary>
        public string weight;

        #endregion

        #region DataCenterPolicy ctor

        /// <summary>
        /// Fully qualified constructor for DataCenterPolicy
        /// </summary>
        /// <param name="DataCenterID">ID of the DataCenter</param>
        /// <param name="Max">Maximum number of instances</param>
        /// <param name="Weight">Instance Allocation (total should be 100%)</param>
        public DataCenterPolicy(string DataCenterID, string Max, string Weight)
        {
            this.dataCenterId = DataCenterID;
            this.max = Max;
            this.weight = Weight;
        }

        #endregion
    }
}
