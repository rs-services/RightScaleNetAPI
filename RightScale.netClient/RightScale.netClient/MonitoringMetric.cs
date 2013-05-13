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
        /// <summary>
        /// Private constant holding the regex validation pattern for MonitoringMetric data calls
        /// </summary>
        private const string monitoringMetricTimeRegex = @"^(-\d+)|0$";

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
                string queryString = string.Empty;
                queryString += "end=0&";
                queryString += string.Format("start={0}&", (-3600).ToString());
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("data") + "?" + queryString);
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
        /// <param name="instance">Instance object to be queried for MonitoringMetrics</param>
        /// <returns>Collection of MonitoringMetrics based on input parameters and filters</returns>
        public static List<MonitoringMetric> index(Instance instance)
        {
            return index(instance.cloud.ID, instance.ID, null, null, null, null, null);
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
        /// <param name="instance">Instance object to be queried for MonitoringMetrics</param>
        /// <param name="filter">Set of filters to limit the set of MonitoringMetrics returned</param>
        /// <returns>Collection of MonitoringMetrics based on input parameters and filters</returns>
        public static List<MonitoringMetric> index(Instance instance, List<Filter> filter)
        {
            return index(instance.cloud.ID, instance.ID, filter, null, null, null, null);
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
        /// <param name="instance">Instance object to be queried for MonitoringMetrics</param>
        /// <param name="filter">Set of filters to limit the set of MonitoringMetrics returned</param>
        /// <param name="period">The time scale for which the graph is generated. Default is 'day'</param>
        /// <param name="size">The size of the graph to be generated. Default is 'small'.</param>
        /// <param name="title">The title of the graph.</param>
        /// <param name="tz">The time zone in which the graph will be displayed. Default will be 'America/Los_Angeles'. For more zones, see User Settings -> Preferences.</param>
        /// <returns>Collection of MonitoringMetrics based on input parameters and filters</returns>
        public static List<MonitoringMetric> index(Instance instance, List<Filter> filter, string period, string size, string title, string tz)
        {
            return index(instance.cloud.ID, instance.ID, filter, period, size, title, tz);
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

            string getHref = string.Format(APIHrefs.MonitoringMetric, cloudID, instanceID);

            string queryString = getMonitoringMetricQueryString(filter, ref period, ref size, title, ref tz);

            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserializeList(jsonString);
        }
        #endregion

        #region MonitoringMetric.show methods

        /// <summary>
        /// Shows attributes of a single monitoring metric. Making a request to the graph_href will return a png image corresponding to that monitoring metric.
        /// </summary>
        /// <param name="serverID">ID of the server whose current instance will be queried to gather MonitoringMetrics</param>
        /// <param name="metricID">ID of the specific metric to be returned</param>
        /// <returns>Returns specific MonitoringMetric as specified by inputs</returns>
        public static MonitoringMetric show(string serverID, string metricID)
        {
            Instance instance = Server.show(serverID).currentInstance;
            return show(instance.cloud.ID, instance.ID, metricID, null, null, null, null);
        }

        /// <summary>
        /// Shows attributes of a single monitoring metric. Making a request to the graph_href will return a png image corresponding to that monitoring metric.
        /// </summary>
        /// <param name="serverID">ID of the server whose current instance will be queried to gather MonitoringMetrics</param>
        /// <param name="metricID">ID of the specific metric to be returned</param>
        /// <param name="period">The time scale for which the graph is generated. Default is 'day'</param>
        /// <param name="size">The size of the graph to be generated. Default is 'small'.</param>
        /// <param name="title">The title of the graph.</param>
        /// <param name="tz">The time zone in which the graph will be displayed. Default will be 'America/Los_Angeles'. For more zones, see User Settings -> Preferences.</param>
        /// <returns>Returns specific MonitoringMetric as specified by inputs</returns>
        public static MonitoringMetric show(string serverID, string metricID, string period, string size, string title, string tz)
        {
            Instance instance = Server.show(serverID).currentInstance;
            return show(instance.cloud.ID, instance.ID, metricID, period, size, title, tz);
        }

        /// <summary>
        /// Shows attributes of a single monitoring metric. Making a request to the graph_href will return a png image corresponding to that monitoring metric.
        /// </summary>
        /// <param name="instance">Instance object to be queried for MonitoringMetrics</param>
        /// <param name="metricID">ID of the specific metric to be returned</param>
        /// <returns>Returns specific MonitoringMetric as specified by inputs</returns>
        public static MonitoringMetric show(Instance instance, string metricID)
        {
            return show(instance.cloud.ID, instance.ID, metricID, null, null, null, null);
        }

        /// <summary>
        /// Shows attributes of a single monitoring metric. Making a request to the graph_href will return a png image corresponding to that monitoring metric.
        /// </summary>
        /// <param name="instance">Instance object to be queried for MonitoringMetrics</param>
        /// <param name="metricID">ID of the specific metric to be returned</param>
        /// <param name="period">The time scale for which the graph is generated. Default is 'day'</param>
        /// <param name="size">The size of the graph to be generated. Default is 'small'.</param>
        /// <param name="title">The title of the graph.</param>
        /// <param name="tz">The time zone in which the graph will be displayed. Default will be 'America/Los_Angeles'. For more zones, see User Settings -> Preferences.</param>
        /// <returns>Returns specific MonitoringMetric as specified by inputs</returns>
        public static MonitoringMetric show(Instance instance, string metricID, string period, string size, string title, string tz)
        {
            return show(instance.cloud.ID, instance.ID, metricID, period, size, title, tz);
        }

        /// <summary>
        /// Shows attributes of a single monitoring metric. Making a request to the graph_href will return a png image corresponding to that monitoring metric.
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the Instance to be queried can be found</param>
        /// <param name="instanceID">ID of the Instance to queried to gather MonitoringMetrics</param>
        /// <param name="metricID">ID of the specific metric to be returned</param>
        /// <returns>Returns specific MonitoringMetric as specified by inputs</returns>
        public static MonitoringMetric show(string cloudID, string instanceID, string metricID)
        {
            return show(cloudID, instanceID, metricID, null, null, null, null);
        }

        /// <summary>
        /// Shows attributes of a single monitoring metric. Making a request to the graph_href will return a png image corresponding to that monitoring metric.
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the Instance to be queried can be found</param>
        /// <param name="instanceID">ID of the Instance to queried to gather MonitoringMetrics</param>
        /// <param name="metricID">ID of the specific metric to be returned</param>
        /// <param name="period">The time scale for which the graph is generated. Default is 'day'</param>
        /// <param name="size">The size of the graph to be generated. Default is 'small'.</param>
        /// <param name="title">The title of the graph.</param>
        /// <param name="tz">The time zone in which the graph will be displayed. Default will be 'America/Los_Angeles'. For more zones, see User Settings -> Preferences.</param>
        /// <returns>Returns specific MonitoringMetric as specified by inputs</returns>
        public static MonitoringMetric show(string cloudID, string instanceID, string metricID, string period, string size, string title, string tz)
        {
            string getHref = string.Format(APIHrefs.MonitoringMetricByID, cloudID, instanceID, metricID); 
            
            string queryString = getMonitoringMetricQueryString(null, ref period, ref size, title, ref tz);

            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserialize(jsonString);
        }

        #endregion

        #region MonitoringMetric.data methods

        /// <summary>
        /// Gives the raw monitoring data for a particular metric. The response will include different variables associated with that metric and the data points for each of those variables.
        /// To get the data for a certain duration, for e.g. for the last 10 minutes(600 secs), provide the variables start="-600" and end="0".
        /// </summary>
        /// <param name="instance">Instance to gather MonitoringMetric data for</param>
        /// <param name="metricID">Name/ID of the MonitoringMetric whose data is to be retrieved</param>
        /// <param name="endRefSecs">An integer number of seconds from current time e.g. -150 or 0</param>
        /// <param name="startRefSecs">An integer number of seconds from current time e.g. -300</param>
        /// <returns>Data for specific monitoring metric for the given instance between the reference times</returns>
        public static MonitoringMetricData data(Instance instance, string metricID, string endRefSecs, string startRefSecs)
        {
            return data(instance.cloud.ID, instance.ID, metricID, endRefSecs, startRefSecs);
        }

        /// <summary>
        /// Gives the raw monitoring data for a particular metric. The response will include different variables associated with that metric and the data points for each of those variables.
        /// To get the data for a certain duration, for e.g. for the last 10 minutes(600 secs), provide the variables start="-600" and end="0".
        /// </summary>
        /// <param name="serverID">ID of the server whose current instance should be queried for the specified MonitoringMetric Data</param>
        /// <param name="metricID">Name/ID of the MonitoringMetric whose data is to be retrieved</param>
        /// <param name="endRefSecs">An integer number of seconds from current time e.g. -150 or 0</param>
        /// <param name="startRefSecs">An integer number of seconds from current time e.g. -300</param>
        /// <returns>Data for specific monitoring metric for the given instance between the reference times</returns>
        public static MonitoringMetricData data(string serverID, string metricID, string endRefSecs, string startRefSecs)
        {
            Instance instance = Server.show(serverID).currentInstance;
            return data(instance.cloud.ID, instance.ID, metricID, endRefSecs, startRefSecs);
        }

        /// <summary>
        /// Gives the raw monitoring data for a particular metric. The response will include different variables associated with that metric and the data points for each of those variables.
        /// To get the data for a certain duration, for e.g. for the last 10 minutes(600 secs), provide the variables start="-600" and end="0".
        /// </summary>
        /// <param name="cloudID">ID of the Cloud where the Instance can be found to gather MonitoringMetric data from</param>
        /// <param name="instanceID">ID of the Instance from which to gather MonitoringMetric data</param>
        /// <param name="metricID">Name/ID of the MonitoringMetric whose data is to be retrieved</param>
        /// <param name="endRefSecs">An integer number of seconds from current time e.g. -150 or 0</param>
        /// <param name="startRefSecs">An integer number of seconds from current time e.g. -300</param>
        /// <returns>Data for specific monitoring metric for the given instance between the reference times</returns>
        public static MonitoringMetricData data(string cloudID, string instanceID, string metricID, string endRefSecs, string startRefSecs)
        {
            string getHref = string.Format(APIHrefs.MonitoringMetricData, cloudID, instanceID, metricID);
            Utility.CheckStringHasValue(startRefSecs);
            Utility.CheckStringRegex("startRefSecs", monitoringMetricTimeRegex, startRefSecs);
            Utility.CheckStringHasValue(endRefSecs);
            Utility.CheckStringRegex("endRefSecs", monitoringMetricTimeRegex, endRefSecs);
            string queryString = string.Empty;
            queryString += string.Format("end={0}&", endRefSecs);
            queryString += string.Format("start={0}", startRefSecs);
            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return MonitoringMetricData.deserialize(jsonString);
        }

        #endregion

        #region MonitoringMetric helper methods

        /// <summary>
        /// Private method builds query string for index and show calls for MonitoringMetric requests
        /// </summary>
        /// <param name="filter">Set of filters to limit the set of MonitoringMetrics returned</param>
        /// <param name="period">The time scale for which the graph is generated. Default is 'day'</param>
        /// <param name="size">The size of the graph to be generated. Default is 'small'.</param>
        /// <param name="title">The title of the graph.</param>
        /// <param name="tz">The time zone in which the graph will be displayed. Default will be 'America/Los_Angeles'. For more zones, see User Settings -> Preferences.</param>
        /// <returns></returns>
        private static string getMonitoringMetricQueryString(List<Filter> filter, ref string period, ref string size, string title, ref string tz)
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

            if (string.IsNullOrWhiteSpace(tz))
            {
                tz = "America/Los_Angeles";
            }

            List<string> validFilters = new List<string>() { "plugin", "view" };
            Utility.CheckFilterInput("filter", validFilters, filter);

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
            return queryString;
        }

        #endregion
    }
}
