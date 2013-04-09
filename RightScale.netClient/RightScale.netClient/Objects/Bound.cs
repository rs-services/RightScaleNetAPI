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
    public class Bound
    {

        #region Bounds Properties


        /// <summary>
        /// Min and Max regex validation string
        /// </summary>
        string minMaxRegexValidationString = @"^\d+$";

        /// <summary>
        /// Private string to hold value for max_count
        /// </summary>
        private string _max_count;

        /// <summary>
        /// The maximum number of servers that must be operational at all times in the server array. NOTE: Any changes that are made to the min/max count in the server array schedule will overwrite the array's default min/max count settings.
        /// </summary>
        public string max_count
        {
            get
            {
                return _max_count;
            }
            set
            {
                if (Utility.CheckStringRegex("max_count", minMaxRegexValidationString, value))
                {
                    this._max_count = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Max_count cannot be set to " + value + ".  Regex validation for pattern [" + minMaxRegexValidationString + "] failed.");
                }
            }
        }

        /// <summary>
        /// Private string to hold value for min_count
        /// </summary>
        private string _min_count;

        /// <summary>
        /// The minimum number of servers that must be operational at all times in the server array. NOTE: Any changes that are made to the min/max count in the server array schedule will overwrite the array's default min/max count settings.
        /// </summary>
        public string min_count
        {
            get
            {
                return _min_count;
            }
            set
            {
                if (Utility.CheckStringRegex("min_count", minMaxRegexValidationString, value))
                {
                    this._min_count = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Min_count cannot be set to " + value + ".  Regex validation for pattern [" + minMaxRegexValidationString + "] failed.");
                }
            }
        }

        #endregion

        #region Bounds.ctor

        /// <summary>
        /// Fully parameterized constructor for Bounds
        /// </summary>
        /// <param name="minCount">The minimum number of servers that must be operational at all times in the server array. NOTE: Any changes that are made to the min/max count in the server array schedule will overwrite the array's default min/max count settings.</param>
        /// <param name="maxCount">The maximum number of servers that must be operational at all times in the server array. NOTE: Any changes that are made to the min/max count in the server array schedule will overwrite the array's default min/max count settings.</param>
        public Bound(string minCount, string maxCount)
        {
            this.min_count = minCount;
            this.max_count = maxCount;
        }

        /// <summary>
        /// Fully parameterized constructor for Bounds
        /// </summary>
        /// <param name="minCount">The minimum number of servers that must be operational at all times in the server array. NOTE: Any changes that are made to the min/max count in the server array schedule will overwrite the array's default min/max count settings.</param>
        /// <param name="maxCount">The maximum number of servers that must be operational at all times in the server array. NOTE: Any changes that are made to the min/max count in the server array schedule will overwrite the array's default min/max count settings.</param>
        public Bound(int minCount, int maxCount)
        {
            this.min_count = minCount.ToString();
            this.max_count = maxCount.ToString();
        }

        public Bound()
        {

        }

        #endregion
    }
}
