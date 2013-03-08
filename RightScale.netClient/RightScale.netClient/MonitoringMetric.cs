using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class MonitoringMetric:Core.RightScaleObjectBase<MonitoringMetric>
    {
        public string plugin { get; set; }
        public string graph_href { get; set; }
        public string view { get; set; }

        #region MonitoringMetric.ctor
        /// <summary>
        /// Default Constructor for MonitoringMetric
        /// </summary>
        public MonitoringMetric()
            : base()
        {
        }

        /// <summary>
        /// Constructor for MonitoringMetric object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public MonitoringMetric(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for MonitoringMetric object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public MonitoringMetric(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		
        #region MonitoringMetric.index methods

        public static List<MonitoringMetric> index()
        {
            return index(null, null, null, null, null);
        }

        public static List<MonitoringMetric> index(List<Filter> filter)
        {
            return index(filter, null, null, null, null);
        }

        public static List<MonitoringMetric> index(List<Filter> filter, string period, string size, string title, string tz)
        {
            if (string.IsNullOrWhiteSpace(period))
            {
                period = "day";
            }
            else
            {
                List<string> validPeriods = new List<string>() { "now", "day", "yday", "week", "lweek", "month", "quarter", "year" };
                Utility.CheckStringInput("period", validPeriods, period);
            }

            if (string.IsNullOrWhiteSpace(size))
            {
                size = "small";
            }
            else
            {
                List<string> validSizes = new List<string>() { "thumb", "tiny", "small", "large", "xlarge" };
                Utility.CheckStringInput("size", validSizes, size);
            }
            
            if(string.IsNullOrWhiteSpace(tz))
            {
                tz = "America/Los_Angeles";
            }

            List<string> validFilters = new List<string>() { "plugin", "view" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement MonitoringMetric.index
            throw new NotImplementedException();
        }
        #endregion
	
    }
}
