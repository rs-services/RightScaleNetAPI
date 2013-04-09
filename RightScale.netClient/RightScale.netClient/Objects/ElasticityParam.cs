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
    public class ElasticityParam
    {
        #region ElasticityParam Properties

        /// <summary>
        /// Alert-specific parameters defining how a ServerArray will scale up and down
        /// </summary>
        public AlertSpecificParam alert_specific_params { get; set; }

        public QueueSpecificParam queue_specific_params { get; set; }

        /// <summary>
        /// Bounds between which a ServerArray will scale up and down
        /// </summary>
        public Bound bounds { get; set; }

        /// <summary>
        /// Pace at which a ServerArray will scale up and down
        /// </summary>
        public Pacing pacing { get; set; }

        /// <summary>
        /// ScheduleEntries defining time-based rules for when this specific set of ElasticityParams will be valid
        /// </summary>
        public List<ScheduleEntry> schedule_entries { get; set; }

        #endregion

        #region ElasticityParam ctors

        /// <summary>
        /// Cosntructor for Alert-based Elasticity Param objects
        /// </summary>
        /// <param name="alertParam">Alert-specific Elasticity Parameter Object</param>
        /// <param name="arrayBounds">Bounds object defining the boundaries fo the array</param>
        /// <param name="arrayPacing">Pacing object defining the pace at which the array will grow and shrink</param>
        /// <param name="arrayScheduleEntries">Schedule object defining the times for which this ruleset is valid</param>
        public ElasticityParam(AlertSpecificParam alertParam, Bound arrayBounds, Pacing arrayPacing, List<ScheduleEntry> arrayScheduleEntries)
        {
            this.alert_specific_params = alertParam;
            setSharedProperties(arrayBounds, arrayPacing, arrayScheduleEntries);
        }

        /// <summary>
        /// Constructor for Queue-based Elasticity Param objects
        /// </summary>
        /// <param name="queueParam">Queue-Specific Elasticity Parameter Object</param>
        /// <param name="arrayBounds">Bounds object defining the boundaries fo the array</param>
        /// <param name="arrayPacing">Pacing object defining the pace at which the array will grow and shrink</param>
        /// <param name="arrayScheduleEntries">Schedule object defining the times for which this ruleset is valid</param>
        public ElasticityParam(QueueSpecificParam queueParam, Bound arrayBounds, Pacing arrayPacing, List<ScheduleEntry> arrayScheduleEntries)
        {
            this.queue_specific_params = queueParam;
            setSharedProperties(arrayBounds, arrayPacing, arrayScheduleEntries);
        }

        /// <summary>
        /// Private set method to handle common properties between two types of Elasticity Param objects
        /// </summary>
        /// <param name="arrayBounds">Bounds object defining the boundaries fo the array</param>
        /// <param name="arrayPacing">Pacing object defining the pace at which the array will grow and shrink</param>
        /// <param name="arrayScheduleEntries">Schedule object defining the times for which this ruleset is valid</param>
        private void setSharedProperties(Bound arrayBounds, Pacing arrayPacing, List<ScheduleEntry> arrayScheduleEntries)
        {
            this.bounds = arrayBounds;
            this.pacing = arrayPacing;
            this.schedule_entries = arrayScheduleEntries;
        }

        public ElasticityParam()
        {

        }

        #endregion
    }
}
