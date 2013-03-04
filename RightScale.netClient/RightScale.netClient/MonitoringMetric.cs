﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class MonitoringMetric
    {
        public List<Action> actions { get; set; }
        public string plugin { get; set; }
        public string graph_href { get; set; }
        public string view { get; set; }
        public List<Link> links { get; set; }

        
        #region MonitoringMetric.index methods

        public static List<MonitoringMetric> index()
        {
            return index(null, null, null, null, null);
        }

        public static List<MonitoringMetric> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null, null, null, null);
        }

        public static List<MonitoringMetric> index(List<KeyValuePair<string, string>> filter, string period, string size, string title, string tz)
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
