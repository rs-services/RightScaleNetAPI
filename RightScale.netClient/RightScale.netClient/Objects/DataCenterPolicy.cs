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

        /// <summary>
        /// ID of the cloud where the DataCenter resides
        /// </summary>
        public string cloudID;

        #endregion

        /// <summary>
        /// DataCenter that is related to this DataCenterPolicy
        /// </summary>
        public DataCenter DataCenter
        {
            get
            {
                return DataCenter.show(cloudID, dataCenterId);
            }
        }

        #region DataCenterPolicy ctor

        /// <summary>
        /// Fully qualified constructor for DataCenterPolicy
        /// </summary>
        /// <param name="DataCenterID">ID of the DataCenter</param>
        /// <param name="Max">Maximum number of instances</param>
        /// <param name="Weight">Instance Allocation (total should be 100%)</param>
        public DataCenterPolicy(string CloudID, string DataCenterID, string Max, string Weight)
        {
            this.cloudID = CloudID;
            this.dataCenterId = DataCenterID;
            this.max = Max;
            this.weight = Weight;
        }

        /// <summary>
        /// Constructor that accepts a DataCenter object, and a max and min constraint for a DataCenterPolicy object
        /// </summary>
        /// <param name="dataCenter">DataCenter Object to which this policy applies</param>
        /// <param name="Max">Maximum number of instances</param>
        /// <param name="Weight">Instance Allocation (total should be 100%)</param>
        public DataCenterPolicy(DataCenter dataCenter, string Max, string Weight)
        {
            this.cloudID = dataCenter.cloud.ID;
            this.dataCenterId = dataCenter.ID;
            this.max = Max;
            this.weight = Weight;
        }

        #endregion
    }
}
