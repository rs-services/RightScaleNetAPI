using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// A monitoring metric is a stream of data that is captured in an instance. Metrics can be monitored, graphed and can be used as the basis for triggering alerts.
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeMonitoringMetric.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceMonitoringMetrics.html
    /// </summary>
    public class MonitoringMetric:Core.RightScaleObjectBase<MonitoringMetric>
    {
        #region MonitoringMetric Properties

        /// <summary>
        /// Plugin for this MonitoringMetric
        /// </summary>
        public string plugin { get; set; }

        /// <summary>
        /// Href of the graph for this MonitoringMetric
        /// </summary>
        public string graph_href { get; set; }

        /// <summary>
        /// View for this MonitoringMetric
        /// </summary>
        public string view { get; set; }

        #endregion

        #region MonitoringMetric.ctor
        /// <summary>
        /// Default Constructor for MonitoringMetric
        /// </summary>
        public MonitoringMetric()
            : base()
        {
        }

        #endregion

        #region MonitoringMetric Relationships

        /// <summary>
        /// MonitoringMetricData associated with this MonitoringMetric
        /// </summary>
        public List<MonitoringMetricData> monitoringMetricData
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("data"));
                return MonitoringMetricData.deserializeList(jsonString);
            }
        }

        #endregion

        #region MonitoringMetric.index methods

        /// <summary>
        /// Lists the monitoring metrics available for the instance and their corresponding graph hrefs. Making a request to the graph_href will return a png image corresponding to that monitoring metric.
        /// </summary>
        /// <param name="serverID">ID of the server whose current instance will be queried to gather MonitoringMetrics</param>
        /// <returns>Collection of MonitoringMetrics based on input parameters and filters</returns>
        public static List<MonitoringMetric> index(string serverID)
        {
            Instance inst = Server.show(serverID).currentInstance;
            return index(inst.cloud.ID, inst.ID, null, null, null, null, null);
        }

        /// <summary>
        /// Lists the monitoring metrics available for the instance and their corresponding graph hrefs. Making a request to the graph_href will return a png image corresponding to that monitoring metric.
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the Instance to be queried can be found</param>
        /// <param name="instanceID">ID of the Instance to queried to gather MonitoringMetrics</param>
        /// <returns>Collection of MonitoringMetrics based on input parameters and filters</returns>
        public static List<MonitoringMetric> index(string cloudID, string instanceID)
        {
            return index(cloudID, instanceID, null, null, null, null, null);
        }

        /// <summary>
        /// Lists the monitoring metrics available for the instance and their corresponding graph hrefs. Making a request to the graph_href will return a png image corresponding to that monitoring metric.
        /// </summary>
        /// <param name="serverID">ID of the server whose current instance will be queried to gather MonitoringMetrics</param>
        /// <param name="filter">Set of filters to limit the set of MonitoringMetrics returned</param>
        /// <returns>Collection of MonitoringMetrics based on input parameters and filters</returns>
        public static List<MonitoringMetric> index(string serverID, List<Filter> filter)
        {
            Instance inst = Server.show(serverID).currentInstance;
            return index(inst.cloud.ID, inst.ID, filter, null, null, null, null);
        }

        /// <summary>
        /// Lists the monitoring metrics available for the instance and their corresponding graph hrefs. Making a request to the graph_href will return a png image corresponding to that monitoring metric.
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the Instance to be queried can be found</param>
        /// <param name="instanceID">ID of the Instance to queried to gather MonitoringMetrics</param>
        /// <param name="filter">Set of filters to limit the set of MonitoringMetrics returned</param>
        /// <returns>Collection of MonitoringMetrics based on input parameters and filters</returns>
        public static List<MonitoringMetric> index(string cloudID, string instanceID, List<Filter> filter)
        {
            return index(cloudID, instanceID, filter, null, null, null, null);
        }

        /// <summary>
        /// Lists the monitoring metrics available for the instance and their corresponding graph hrefs. Making a request to the graph_href will return a png image corresponding to that monitoring metric.
        /// </summary>
        /// <param name="serverID">ID of the server whose current instance will be queried to gather MonitoringMetrics</param>
        /// <param name="filter">Set of filters to limit the set of MonitoringMetrics returned</param>
        /// <param name="period">The time scale for which the graph is generated. Default is 'day'</param>
        /// <param name="size">The size of the graph to be generated. Default is 'small'.</param>
        /// <param name="title">The title of the graph.</param>
        /// <param name="tz">The time zone in which the graph will be displayed. Default will be 'America/Los_Angeles'. For more zones, see User Settings -> Preferences.</param>
        /// <returns>Collection of MonitoringMetrics based on input parameters and filters</returns>
        public static List<MonitoringMetric> index(string serverID, List<Filter> filter, string period, string size, string title, string tz)
        {
            Instance inst = Server.show(serverID).currentInstance;
            return index(inst.cloud.ID, inst.ID, filter, period, size, title, tz);
        }

        /// <summary>
        /// Lists the monitoring metrics available for the instance and their corresponding graph hrefs. Making a request to the graph_href will return a png image corresponding to that monitoring metric.
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the Instance to be queried can be found</param>
        /// <param name="instanceID">ID of the Instance to queried to gather MonitoringMetrics</param>
        /// <param name="filter">Set of filters to limit the set of MonitoringMetrics returned</param>
        /// <param name="period">The time scale for which the graph is generated. Default is 'day'</param>
        /// <param name="size">The size of the graph to be generated. Default is 'small'.</param>
        /// <param name="title">The title of the graph.</param>
        /// <param name="tz">The time zone in which the graph will be displayed. Default will be 'America/Los_Angeles'. For more zones, see User Settings -> Preferences.</param>
        /// <returns>Collection of MonitoringMetrics based on input parameters and filters</returns>
        public static List<MonitoringMetric> index(string cloudID, string instanceID, List<Filter> filter, string period, string size, string title, string tz)
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

            string getHref = string.Format(APIHrefs.MonitoringMetric, cloudID, instanceID);
            string queryString = string.Empty;

            if (filter != null && filter.Count > 0)
            {
                foreach (Filter f in filter)
                {
                    queryString += f.ToString() + "&";
                }
            }

            if (!string.IsNullOrWhiteSpace(period))
            {
                queryString += "period=" + period + "&";
            }
            if (!string.IsNullOrWhiteSpace(size))
            {
                queryString += "size=" + size + "&";
            }
            if (!string.IsNullOrWhiteSpace(title))
            {
                queryString += "title=" + title + "&";
            }
            if (!string.IsNullOrWhiteSpace(tz))
            {
                queryString += "tz=" + tz;
            }
            queryString = queryString.TrimEnd('&');

            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserializeList(jsonString);
        }
        #endregion
	
    }
}
