using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// Schedule-based bounds for a ServerArray defining the minimum and maximum size of a ServerArray by day and time
    /// </summary>
    public class ScheduleEntry : Bounds
    {
        #region ScheduleEntry Properties

        /// <summary>
        /// Valid values for day property
        /// </summary>
        List<string> validDayValues = new List<string>() { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

        /// <summary>
        /// Regex validation string for time property
        /// </summary>
        string timeRegexValidationString = @"^\d\d:\d\d$";


        /// <summary>
        /// Private object to hold value for time property
        /// </summary>
        private string _time;

        /// <summary>
        /// Time for which this set of bounds is valid
        /// </summary>
        public string time
        {
            get
            {
                return _time;
            }
            set
            {
                if (Utility.CheckStringRegex("time", timeRegexValidationString, value))
                {
                    this._time = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Time cannot be set to " + value + ".  Regex validation for pattern [" + timeRegexValidationString + "] failed.");
                }
            }
        }

        /// <summary>
        /// Private object to hold value for day property
        /// </summary>
        private string _day;

        /// <summary>
        /// Day for which this set of bounds is valid
        /// </summary>
        public string day
        {
            get
            {
                return _day;
            }
            set
            {
                if (Utility.CheckStringInput("day", validDayValues, value))
                {
                    this._day = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Day cannot be " + value + ".  Valid Values are 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'");
                }
            }
        }

        #endregion

        #region ScheduleEntry.ctor

        /// <summary>
        /// Fully parameterized constructor taking time and day to populate ScheduleEntry object
        /// </summary>
        /// <param name="Time">Time for which this set of bounds is valid</param>
        /// <param name="Day">Day for which this set of bounds is valid</param>
        public ScheduleEntry(string Time, string Day, string MinCount, string MaxCount):base(MinCount, MaxCount)
        {
            this.time = Time;
            this.day = Day;
        }

        /// <summary>
        /// Default constructor for ScheduleEntry
        /// </summary>
        public ScheduleEntry()
        {

        }

        #endregion
    }
}
