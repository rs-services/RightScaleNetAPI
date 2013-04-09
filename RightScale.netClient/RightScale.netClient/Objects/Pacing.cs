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

        string resizeRegexValidator = @"^\d+$";

        /// <summary>
        /// Private object to hold value for resize_down_by property
        /// </summary>
        private string _resize_down_by;

        /// <summary>
        /// Number of servers to terminate at a time when scaling down (until minimum # of servers is reached)
        /// </summary>
        public string resize_down_by
        {
            get
            {
                return this._resize_down_by;
            }
            set
            {
                if (Utility.CheckStringRegex("resize_down_by", resizeRegexValidator, value))
                {
                    this._resize_down_by = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Resize_down_by cannot be set to " + value + ".  Regex validation for pattern [" + resizeRegexValidator + "] failed.");
                }
            }
        }

        /// <summary>
        /// Private object to hold value for resize_up_by property
        /// </summary>
        private string _resize_up_by;

        /// <summary>
        /// Number of servers to launch at a time when scaling up (until maximum # of servers is reached)
        /// </summary>
        public string resize_up_by
        {
            get
            {
                return this._resize_up_by;
            }
            set
            {
                if (Utility.CheckStringRegex("resize_up_by", resizeRegexValidator, value))
                {
                    this._resize_up_by = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Resize_up_by cannot be set to " + value + ".  Regex validation for pattern [" + resizeRegexValidator + "] failed.");
                }
            }
        }

        /// <summary>
        /// Private object to hold value for resize_calm_time
        /// </summary>
        private string _resize_calm_time;
        
        /// <summary>
        /// Time between scaling events 
        /// </summary>
        public string resize_calm_time
        {
            get
            {
                return this._resize_calm_time;
            }
            set
            {
                if (Utility.CheckStringRegex("resize_calm_time", resizeRegexValidator, value))
                {
                    this._resize_calm_time = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Resize_calm_time cannot be set to " + value + ".  Regex validation for pattern [" + resizeRegexValidator + "] failed.");
                }
            }
        }

        #endregion

        #region Pacing.ctor

        /// <summary>
        /// Fully parameterized constructor for Pacing
        /// </summary>
        /// <param name="resizeUpBy">Number of servers to terminate at a time when scaling down (until minimum # of servers is reached)</param>
        /// <param name="resizeDownBy">Number of servers to launch at a time when scaling up (until maximum # of servers is reached)</param>
        /// <param name="resizeCalmTime">Time between scaling events</param>
        public Pacing(int resizeUpBy, int resizeDownBy, int resizeCalmTime)
        {
            this.resize_up_by = resizeUpBy.ToString();
            this.resize_down_by = resizeDownBy.ToString();
            this.resize_calm_time = resizeCalmTime.ToString();
        }

        /// <summary>
        /// Fully parameterized constructor for Pacing
        /// </summary>
        /// <param name="resizeUpBy">Number of servers to terminate at a time when scaling down (until minimum # of servers is reached)</param>
        /// <param name="resizeDownBy">Number of servers to launch at a time when scaling up (until maximum # of servers is reached)</param>
        /// <param name="resizeCalmTime">Time between scaling events</param>
        public Pacing(string resizeUpBy, string resizeDownBy, string resizeCalmTime)
        {
            this.resize_up_by = resizeUpBy;
            this.resize_down_by = resizeDownBy;
            this.resize_calm_time = resizeCalmTime;
        }

        public Pacing()
        {

        }

        #endregion
    }
}
