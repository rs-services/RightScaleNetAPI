using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// An AlertSpec defines the conditions under which an alert is triggered and an alert escalation is called. Condition sentence: if <file>.<variable> <condition> '<threshold>' for <duration> min then escalate to '<escalation_name>'.
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeAlertSpec.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceAlertSpecs.html
    /// </summary>
    public class AlertSpec : Core.RightScaleObjectBase<AlertSpec>
    {
        public string name { get; set; }
        public int duration { get; set; }
        public string created_at { get; set; }
        public string threshold { get; set; }
        public string updated_at { get; set; }
        public string condition { get; set; }
        public string description { get; set; }
        public string file { get; set; }
        public string variable { get; set; }
        public string escalation_name { get; set; }

        private const string alertSpecFormat = "alert_spec[{0}]";

        #region AlertSpec.ctor()

        /// <summary>
        /// Default constructor for AlertSpec object
        /// </summary>
        public AlertSpec()
            : base()
        {

        }

        /// <summary>
        /// Constructor for AlertSpec object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public AlertSpec(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for AlertSpec object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public AlertSpec(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {


        }
        
        #endregion

        #region AlertSpec.show methods

        /// <summary>
        /// Gets a specific instance of an AlertSpec for a given server
        /// </summary>
        /// <param name="alertSpecID">ID of the AlertSpec to retrieve</param>
        /// <param name="serverID">Server where the AlertSpec can be found</param>
        /// <returns>instance of AlertSpec</returns>
        public static AlertSpec show_server(string alertSpecID, string serverID)
        {
            Utility.CheckStringIsNumeric(serverID);
            Utility.CheckStringIsNumeric(alertSpecID);

            string getURL = string.Format("/api/servers/{0}/alert_specs/{1}", serverID, alertSpecID);

            string jsonString = Core.APIClient.Instance.Get(getURL);

            return deserialize(jsonString);
        }

        /// <summary>
        /// Gets a speific instance of an AlertSpec for a given ServerArray
        /// </summary>
        /// <param name="alertSpecID">ID of the AlertSpec to retrieve</param>
        /// <param name="serverArrayID">ServerArray where the AlertSpec can be found</param>
        /// <returns>instance of AlertSpec</returns>
        public static AlertSpec show_serverArray(string alertSpecID, string serverArrayID)
        {
            Utility.CheckStringIsNumeric(serverArrayID);
            Utility.CheckStringIsNumeric(alertSpecID);

            string getURL = string.Format("/api/server_arrays/{0}/alert_specs/{1}", serverArrayID, alertSpecID);

            string jsonString = Core.APIClient.Instance.Get(getURL);

            return deserialize(jsonString);
        }

        /// <summary>
        /// Gets a specific instance of an AlertSpec for a given ServerTemplate
        /// </summary>
        /// <param name="alertSpecID">ID of the AlertSpec to retrieve</param>
        /// <param name="serverTemplateID">ServerTemplate where the Alert can be found</param>
        /// <returns>instance of AlertSpec</returns>
        public static AlertSpec show_serverTemplate(string alertSpecID, string serverTemplateID)
        {
            Utility.CheckStringIsNumeric(serverTemplateID);
            Utility.CheckStringIsNumeric(alertSpecID);

            string getURL = string.Format("/api/server_templates/{0}/alert_specs/{1}", serverTemplateID, alertSpecID);

            string jsonString = Core.APIClient.Instance.Get(getURL);

            return deserialize(jsonString);
        }

        /// <summary>
        /// Gets a specific instance of an AlertSpec 
        /// </summary>
        /// <param name="alertSpecID">ID of the AlertSpec to retrieve</param>
        /// <returns>instance of AlertSpec</returns>
        public static AlertSpec show(string alertSpecID)
        {
            Utility.CheckStringIsNumeric(alertSpecID);

            string getURL = string.Format("/api/alert_specs/{0}", alertSpecID);

            string jsonString = Core.APIClient.Instance.Get(getURL);

            return deserialize(jsonString);
        }

        #endregion

        #region AlertSpec.index methods

        /// <summary>
        /// Gets a collection of AlertSpecs for a given ServerArray
        /// </summary>
        /// <param name="serverArrayID">ID of the ServerArray to query for AlertSpecs</param>
        /// <returns>collection of AlertSpecs for the given ServerArray</returns>
        public static List<AlertSpec> index_serverArray(string serverArrayID)
        {
            return index_serverArray(serverArrayID, null, null);
        }

        /// <summary>
        /// Gets a collection of AlertSpecs for a given ServerArray
        /// </summary>
        /// <param name="serverArrayID">ID of the ServerArray to query for AlertSpecs</param>
        /// <param name="filter">Filters for querying AlertSpecs</param>
        /// <returns>collection of AlertSpecs for the given ServerArray</returns>
        public static List<AlertSpec> index_serverArray(string serverArrayID, List<KeyValuePair<string, string>> filter)
        {
            return index_serverArray(serverArrayID, filter, null);
        }

        /// <summary>
        /// Gets a collection of AlertSpecs for a given ServerArray
        /// </summary>
        /// <param name="serverArrayID">ID of the ServerArray to query for AlertSpecs</param>
        /// <param name="view">View name for querying AlertSpecs</param>
        /// <returns>collection of AlertSpecs for the given ServerArray</returns>
        public static List<AlertSpec> index_serverArray(string serverArrayID, string view)
        {
            return index_serverArray(serverArrayID, null, view);
        }

        /// <summary>
        /// Gets a collection of AlertSpecs for a given ServerArray
        /// </summary>
        /// <param name="serverArrayID">ID of the ServerArray to query for AlertSpecs</param>
        /// <param name="filter">Filters for querying AlertSpecs</param>
        /// <param name="view">View name for querying AlertSpecs</param>
        /// <returns>collection of AlertSpecs for the given ServerArray</returns>
        public static List<AlertSpec> index_serverArray(string serverArrayID, List<KeyValuePair<string, string>> filter, string view)
        {
            string getHref = string.Format("/api/server_arrays/{0}/alert_specs", serverArrayID);
            return indexGet(getHref, filter, view);
        }

        /// <summary>
        /// Gets a collection of AlertSpecs for a given ServerTemplate
        /// </summary>
        /// <param name="serverTemplateID">ServerTemplate ID to query for AlertSpecs</param>
        /// <returns>collection of AlertSpecs for the given ServerTemplate</returns>
        public static List<AlertSpec> index_serverTemplate(string serverTemplateID)
        {
            return index_serverTemplate(serverTemplateID, null, null);
        }

        /// <summary>
        /// Gets a collection of AlertSpecs for a given ServerTemplate
        /// </summary>
        /// <param name="serverTemplateID">ServerTemplate ID to query for AlertSpecs</param>
        /// <param name="filter">Filters for querying AlertSpecs</param>
        /// <returns>collection of AlertSpecs for the given ServerTemplate</returns>
        public static List<AlertSpec> index_serverTemplate(string serverTemplateID, List<KeyValuePair<string, string>> filter)
        {
            return index_serverTemplate(serverTemplateID, filter, null);
        }

        /// <summary>
        /// Gets a collection of AlertSpecs for a given ServerTemplate
        /// </summary>
        /// <param name="serverTemplateID">ServerTemplate ID to query for AlertSpecs</param>
        /// <param name="view">View name for querying AlertSpecs</param>
        /// <returns>collection of AlertSpecs for the given ServerTemplate</returns>
        public static List<AlertSpec> index_serverTemplate(string serverTemplateID, string view)
        {
            return index_serverTemplate(serverTemplateID, null, view);
        }

        /// <summary>
        /// Gets a collection of AlertSpecs for a given ServerTemplate
        /// </summary>
        /// <param name="serverTemplateID">ServerTemplate ID to query for AlertSpecs</param>
        /// <param name="filter">Filters for querying AlertSpecs</param>
        /// <param name="view">View name for querying AlertSpecs</param>
        /// <returns>collection of AlertSpecs for the given ServerTemplate</returns>
        public static List<AlertSpec> index_serverTemplate(string serverTemplateID, List<KeyValuePair<string, string>> filter, string view)
        {
            string getHref = string.Format("/api/server_templates/{0}/alert_specs", serverTemplateID);
            return indexGet(getHref, filter, view);
        }

        /// <summary>
        /// Gets a collection of AlertSpecs
        /// </summary>
        /// <returns>collection of AlertSpecs</returns>
        public static List<AlertSpec> index(string serverID)
        {
            return index(serverID, null, null);
        }

        /// <summary>
        /// Gets a collection of AlertSpecs for a given set of filters
        /// </summary>
        /// <param name="filter">Filters for querying AlertSpecs</param>
        /// <returns>collection of AlertSpecs</returns>
        public static List<AlertSpec> index(string serverID, List<KeyValuePair<string, string>> filter)
        {
            return index(serverID, filter, null);
        }

        /// <summary>
        /// Gets a collection of AlertSpecs for a specific view
        /// </summary>
        /// <param name="view">view name for querying AlertSpecs</param>
        /// <returns>collection of AlertSpecs</returns>
        public static List<AlertSpec> index(string serverID, string view)
        {
            return index(null, view);
        }

        /// <summary>
        /// Gets a collection of AlertSpecs for a given set of filters and a specific view
        /// </summary>
        /// <param name="filter">Filters for querying AlertSpecs</param>
        /// <param name="view">View name for querying AlertSpecs</param>
        /// <returns>collection of AlertSpecs</returns>
        public static List<AlertSpec> index(string serverID, List<KeyValuePair<string, string>> filter, string view)
        {
            string getHref = string.Format("/api/servers/{0}/alert_specs", serverID);
            return indexGet(getHref, filter, view);
        }

        /// <summary>
        /// Private implementation to centrally manage all calls to index AlertSpecs
        /// </summary>
        /// <param name="getHref">RightScale API Href fragment for indexing AlertSpecs</param>
        /// <param name="filter">Filters for querying AlertSpecs</param>
        /// <param name="view">View name for querying AlertSpecs</param>
        /// <returns>collection of AlertSpecs</returns>
        private static List<AlertSpec> indexGet(string getHref, List<KeyValuePair<string, string>> filter, string view)
        {
            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default" };
                Utility.CheckStringInput("view", validViews, view);
            }

            List<string> validFilters = new List<string>() { "description", "escalation_name", "name", "subject_href" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            string queryString = Utility.BuildFilterString(filter);
            if (!string.IsNullOrEmpty(view))
            {
                queryString += string.Format("&view={0}", view);
            }

            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserializeList(jsonString);
        }

        #endregion

        #region AlertSpec.destroy methods

        /// <summary>
        /// Deletes a given AlertSpec
        /// </summary>
        /// <param name="alertSpecID">ID of the AlertSpec to delete</param>
        /// <returns>true if deleted, false if not</returns>
        public static bool destroy(string alertSpecID)
        {
            Utility.CheckStringIsNumeric(alertSpecID);

            string getURL = string.Format("/api/alert_specs/{0}", alertSpecID);
            return Core.APIClient.Instance.Delete(getURL);
        }

        /// <summary>
        /// Deletes a given AlertSpec for a specific Server
        /// </summary>
        /// <param name="alertSpecID">ID of the AlertSpec to delete</param>
        /// <param name="serverID">ID of the server where the AlertSpec resides</param>
        /// <returns>true if deleted, false if not</returns>
        public static bool destroy_server(string alertSpecID, string serverID)
        {
            Utility.CheckStringIsNumeric(alertSpecID);
            Utility.CheckStringIsNumeric(serverID);

            string getURL = string.Format("/api/servers/{0}/alert_specs/{1}", serverID, alertSpecID);
            return Core.APIClient.Instance.Delete(getURL);
        }

        /// <summary>
        /// Deletes a given AlertSpec for a specific ServerTemplate
        /// </summary>
        /// <param name="alertSpecID">ID of the AlertSpec to delete</param>
        /// <param name="serverTemplateID">ID of the ServerTemplate where the AlertSpec resides</param>
        /// <returns>true if deleted, false if not</returns>
        public static bool destroy_serverTemplate(string alertSpecID, string serverTemplateID)
        {
            Utility.CheckStringIsNumeric(alertSpecID);
            Utility.CheckStringIsNumeric(serverTemplateID);

            string getURL = string.Format("/api/server_templates/{0}/alert_specs/{1}", serverTemplateID, alertSpecID);
            return Core.APIClient.Instance.Delete(getURL);
        }

        /// <summary>
        /// Deletes a given AlertSpec for a specific ServerArray
        /// </summary>
        /// <param name="alertSpecID">ID of the AlertSpec to delete</param>
        /// <param name="serverTemplateID">ID of the ServerArray where the AlertSpec resides</param>
        /// <returns>true if deleted, false if not</returns>
        public static bool destroy_serverArray(string alertSpecID, string serverTemplateID)
        {
            Utility.CheckStringIsNumeric(alertSpecID);
            Utility.CheckStringIsNumeric(serverTemplateID);

            string getURL = string.Format("/api/server_templates/{0}/alert_specs/{1}", serverTemplateID, alertSpecID);
            return Core.APIClient.Instance.Delete(getURL);
        }

        #endregion

        #region AlertSpec.create methods

        /// <summary>
        /// Creates a new AlertSpec with the given parameters.
        /// </summary>
        /// <param name="condition">The condition (operator) in the condition sentence.</param>
        /// <param name="description">The description of the AlertSpec.</param>
        /// <param name="duration">The duration in minutes of the condition sentence.</param>
        /// <param name="escalation_name">Escalate to the named alert escalation when the alert is triggered. Must either escalate or vote</param>
        /// <param name="file">The RRD path/file_name of the condition sentence.</param>
        /// <param name="name">The name of the AlertSpec.</param>
        /// <param name="subject_href">The href of the resource that this AlertSpec should be associated with. The subject can be a ServerTemplate, Server, or Server Array</param>
        /// <param name="threshold">The threshold of the condition sentence.</param>
        /// <param name="variable">The RRD variable of the condition sentence.</param>
        /// <param name="vote_tag">Should correspond to a vote tag on a ServerArray if vote to grow or shrink</param>
        /// <param name="vote_type">Vote to grow or shrink a ServerArray when the alert is triggered. Must either escalate or vote</param>
        /// <returns></returns>
        public static string create(string condition, string description, string duration, string escalation_name, string file, string name, string subject_href, string threshold, string variable, string vote_tag, string vote_type)
        {
            string postUrl = "/api/alert_specs";
            if (string.IsNullOrWhiteSpace(subject_href))
            {
                throw new ArgumentNullException("Calls to AlertSpec.create must have an associated subject_href");
            }
            List<KeyValuePair<string, string>> parameters = getCreatePostData(condition, description, duration, escalation_name, file, name, subject_href, threshold, variable, vote_tag, vote_type);
            List<string> returnList = Core.APIClient.Instance.Create(postUrl, parameters, "location");
            string[] hrefSplit = returnList[0].Split('/');
            return hrefSplit.Last<string>();
        }

        /// <summary>
        /// Creates a new AlertSpec for a given Server identified by serverId
        /// </summary>
        /// <param name="condition">The condition (operator) in the condition sentence.</param>
        /// <param name="description">The description of the AlertSpec.</param>
        /// <param name="duration">The duration in minutes of the condition sentence.</param>
        /// <param name="escalation_name">Escalate to the named alert escalation when the alert is triggered. Must either escalate or vote</param>
        /// <param name="file">The RRD path/file_name of the condition sentence.</param>
        /// <param name="name">The name of the AlertSpec.</param>
        /// <param name="subject_href">The href of the resource that this AlertSpec should be associated with. The subject can be a ServerTemplate, Server, or Server Array</param>
        /// <param name="threshold">The threshold of the condition sentence.</param>
        /// <param name="variable">The RRD variable of the condition sentence.</param>
        /// <param name="vote_tag">Should correspond to a vote tag on a ServerArray if vote to grow or shrink</param>
        /// <param name="vote_type">Vote to grow or shrink a ServerArray when the alert is triggered. Must either escalate or vote</param>
        /// <param name="serverId">Server ID where the AlertSpec is to be created</param>
        /// <returns></returns>
        public static string create_server(string condition, string description, string duration, string escalation_name, string file, string name, string subject_href, string threshold, string variable, string vote_tag, string vote_type, string serverId)
        {
            string postUrl = string.Format("/api/servers/{0}/alert_specs", serverId);
            List<KeyValuePair<string, string>> parameters = getCreatePostData(condition, description, duration, escalation_name, file, name, subject_href, threshold, variable, vote_tag, vote_type);
            List<string> returnList = Core.APIClient.Instance.Create(postUrl, parameters, "location");
            string[] hrefSplit = returnList[0].Split('/');
            return hrefSplit.Last<string>();
        }

        /// <summary>
        /// Creates a new AlertSpec for a given ServerTemplate identified by serverTemplateID
        /// </summary>
        /// <param name="condition">The condition (operator) in the condition sentence.</param>
        /// <param name="description">The description of the AlertSpec.</param>
        /// <param name="duration">The duration in minutes of the condition sentence.</param>
        /// <param name="escalation_name">Escalate to the named alert escalation when the alert is triggered. Must either escalate or vote</param>
        /// <param name="file">The RRD path/file_name of the condition sentence.</param>
        /// <param name="name">The name of the AlertSpec.</param>
        /// <param name="subject_href">The href of the resource that this AlertSpec should be associated with. The subject can be a ServerTemplate, Server, or Server Array</param>
        /// <param name="threshold">The threshold of the condition sentence.</param>
        /// <param name="variable">The RRD variable of the condition sentence.</param>
        /// <param name="vote_tag">Should correspond to a vote tag on a ServerArray if vote to grow or shrink</param>
        /// <param name="vote_type">Vote to grow or shrink a ServerArray when the alert is triggered. Must either escalate or vote</param>
        /// <param name="serverTemplateId">ServerTemplate ID where the AlertSpec is to be created</param>
        /// <returns></returns>
        public static string create_serverTemplate(string condition, string description, string duration, string escalation_name, string file, string name, string subject_href, string threshold, string variable, string vote_tag, string vote_type, string serverTemplateId)
        {
            string postUrl = string.Format("/api/server_templates/{0}/alert_specs", serverTemplateId);
            List<KeyValuePair<string, string>> parameters = getCreatePostData(condition, description, duration, escalation_name, file, name, subject_href, threshold, variable, vote_tag, vote_type);
            List<string> returnList = Core.APIClient.Instance.Create(postUrl, parameters, "location");
            string[] hrefSplit = returnList[0].Split('/');
            return hrefSplit.Last<string>();
        }

        /// <summary>
        /// Creates a new AlertSpec for a given ServerArray identified by serverArrayID
        /// </summary>
        /// <param name="condition">The condition (operator) in the condition sentence.</param>
        /// <param name="description">The description of the AlertSpec.</param>
        /// <param name="duration">The duration in minutes of the condition sentence.</param>
        /// <param name="escalation_name">Escalate to the named alert escalation when the alert is triggered. Must either escalate or vote</param>
        /// <param name="file">The RRD path/file_name of the condition sentence.</param>
        /// <param name="name">The name of the AlertSpec.</param>
        /// <param name="subject_href">The href of the resource that this AlertSpec should be associated with. The subject can be a ServerTemplate, Server, or Server Array</param>
        /// <param name="threshold">The threshold of the condition sentence.</param>
        /// <param name="variable">The RRD variable of the condition sentence.</param>
        /// <param name="vote_tag">Should correspond to a vote tag on a ServerArray if vote to grow or shrink</param>
        /// <param name="vote_type">Vote to grow or shrink a ServerArray when the alert is triggered. Must either escalate or vote</param>>
        /// <param name="serverArrayID">ServerArray ID where the AlertSpec is to be created</param>
        /// <returns></returns>
        public static string create_serverArray(string condition, string description, string duration, string escalation_name, string file, string name, string subject_href, string threshold, string variable, string vote_tag, string vote_type, string serverArrayID)
        {
            string postUrl = string.Format("/api/server_arrays/{0}/alert_specs", serverArrayID);
            List<KeyValuePair<string, string>> parameters = getCreatePostData(condition, description, duration, escalation_name, file, name, subject_href, threshold, variable, vote_tag, vote_type);
            List<string> returnList = Core.APIClient.Instance.Create(postUrl, parameters, "location");
            string[] hrefSplit = returnList[0].Split('/');
            return hrefSplit.Last<string>();
        }

        /// <summary>
        /// Private Method checks required inputs for formatting and existence within the AlertSpec.create process
        /// </summary>
        /// <param name="condition">The condition (operator) in the condition sentence.</param>
        /// <param name="duration">The duration in minutes of the condition sentence.</param>
        /// <param name="file">The RRD path/file_name of the condition sentence.</param>
        /// <param name="name">The name of the AlertSpec.</param>
        /// <param name="threshold">The threshold of the condition sentence.</param>
        /// <param name="variable">The RRD variable of the condition sentence.</param>
        /// <param name="vote_type">Vote to grow or shrink a ServerArray when the alert is triggered. Must either escalate or vote</param>
        private static void checkCreateInputs(string condition, string duration, string file, string name, string threshold, string variable, string vote_type)
        {
            Utility.CheckStringHasValue(duration);
            Utility.CheckStringIsNumeric(duration);
            Utility.CheckStringHasValue(file);
            Utility.CheckStringHasValue(name);
            Utility.CheckStringHasValue(threshold);
            Utility.CheckStringHasValue(variable);
            Utility.CheckStringHasValue(condition);
            checkInputFormatting(condition, vote_type);
        }

        /// <summary>
        /// Private method validates formatting if values exist for inputs
        /// </summary>
        /// <param name="condition">The condition (operator) in the condition sentence.</param>
        /// <param name="vote_type">Vote to grow or shrink a ServerArray when the alert is triggered. Must either escalate or vote</param>
        private static void checkInputFormatting(string condition, string vote_type)
        {
            List<string> validVote_types = new List<string>() { "grow", "shrink" };
            List<string> validConditions = new List<string>() { ">", ">=", "<", "<=", "==", "!=" };
            if (!string.IsNullOrWhiteSpace(condition))
            {
                Utility.CheckStringInput("condition", validConditions, condition);
            }
            if (!string.IsNullOrWhiteSpace(vote_type))
            {
                Utility.CheckStringInput("vote_type", validVote_types, vote_type);
            }
        }

        /// <summary>
        /// Private method builds keyvaluepair list of parameters to be posted to the AlertSpec.create process
        /// </summary>
        /// <param name="condition">The condition (operator) in the condition sentence.</param>
        /// <param name="description">The description of the AlertSpec.</param>
        /// <param name="duration">The duration in minutes of the condition sentence.</param>
        /// <param name="escalation_name">Escalate to the named alert escalation when the alert is triggered. Must either escalate or vote</param>
        /// <param name="file">The RRD path/file_name of the condition sentence.</param>
        /// <param name="name">The name of the AlertSpec.</param>
        /// <param name="subject_href">The href of the resource that this AlertSpec should be associated with. The subject can be a ServerTemplate, Server, or Server Array</param>
        /// <param name="threshold">The threshold of the condition sentence.</param>
        /// <param name="variable">The RRD variable of the condition sentence.</param>
        /// <param name="vote_tag">Should correspond to a vote tag on a ServerArray if vote to grow or shrink</param>
        /// <param name="vote_type">Vote to grow or shrink a ServerArray when the alert is triggered. Must either escalate or vote</param>
        /// <returns>List<KeyValuePair<string, string>> to be posted to AlertSpec.create process</returns>
        private static List<KeyValuePair<string, string>> getCreatePostData(string condition, string description, string duration, string escalation_name, string file, string name, string subject_href, string threshold, string variable, string vote_tag, string vote_type)
        {
            checkCreateInputs(condition, duration, file, name, threshold, variable, vote_type);
            List<KeyValuePair<string, string>> retVal = new List<KeyValuePair<string, string>>();
            retVal.Add(new KeyValuePair<string,string>(string.Format(alertSpecFormat, "condition"), condition));
            if(!string.IsNullOrWhiteSpace(description))
            {
                retVal.Add(new KeyValuePair<string, string>(string.Format(alertSpecFormat, "description"), description));
            }
            retVal.Add(new KeyValuePair<string, string>(string.Format(alertSpecFormat, "duration"), duration));
            if (!string.IsNullOrWhiteSpace(escalation_name))
            {
                retVal.Add(new KeyValuePair<string, string>(string.Format(alertSpecFormat, "escalation_name"), escalation_name));
            }
            retVal.Add(new KeyValuePair<string, string>(string.Format(alertSpecFormat, "file"), file));
            retVal.Add(new KeyValuePair<string, string>(string.Format(alertSpecFormat, "name"), name));
            if (!string.IsNullOrWhiteSpace(subject_href))
            {
                retVal.Add(new KeyValuePair<string, string>(string.Format(alertSpecFormat, "subject_href"), subject_href));
            }
            retVal.Add(new KeyValuePair<string, string>(string.Format(alertSpecFormat, "threshold"), threshold));
            retVal.Add(new KeyValuePair<string, string>(string.Format(alertSpecFormat, "variable"), variable));
            if (!string.IsNullOrWhiteSpace(vote_tag))
            {
                retVal.Add(new KeyValuePair<string, string>(string.Format(alertSpecFormat, "vote_tag"), vote_tag));
            }
            if (!string.IsNullOrWhiteSpace(vote_type))
            {
                retVal.Add(new KeyValuePair<string, string>(string.Format(alertSpecFormat, "vote_type"), vote_type));
            }
            return retVal;
        }

        #endregion

        #region AlertSpec.update methods

        /// <summary>
        /// Updates a given AlertSpec 
        /// </summary>
        /// <param name="condition">The condition (operator) in the condition sentence.</param>
        /// <param name="description">The description of the AlertSpec.</param>
        /// <param name="duration">The duration in minutes of the condition sentence.</param>
        /// <param name="escalation_name">Escalate to the named alert escalation when the alert is triggered. Must either escalate or vote</param>
        /// <param name="file">The RRD path/file_name of the condition sentence.</param>
        /// <param name="name">The name of the AlertSpec.</param>
        /// <param name="subject_href">The href of the resource that this AlertSpec should be associated with. The subject can be a ServerTemplate, Server, or Server Array</param>
        /// <param name="threshold">The threshold of the condition sentence.</param>
        /// <param name="variable">The RRD variable of the condition sentence.</param>
        /// <param name="vote_tag">Should correspond to a vote tag on a ServerArray if vote to grow or shrink</param>
        /// <param name="vote_type">Vote to grow or shrink a ServerArray when the alert is triggered. Must either escalate or vote</param>
        /// <param name="alertSpecID">ID of the AlertSpec to be updated</param>
        /// <returns></returns>
        public static bool update(string condition, string description, string duration, string escalation_name, string file, string name, string subject_href, string threshold, string variable, string vote_tag, string vote_type, string alertSpecID)
        {
            string putUrl = string.Format("/api/alert_specs/{0}", alertSpecID); 
            return performUpdate(condition, description, duration, escalation_name, file, name, subject_href, threshold, variable, vote_tag, vote_type, putUrl);
        }

        /// <summary>
        /// Updates a given AlertSpec within the context of a specific ServerTemplate
        /// </summary>
        /// <param name="condition">The condition (operator) in the condition sentence.</param>
        /// <param name="description">The description of the AlertSpec.</param>
        /// <param name="duration">The duration in minutes of the condition sentence.</param>
        /// <param name="escalation_name">Escalate to the named alert escalation when the alert is triggered. Must either escalate or vote</param>
        /// <param name="file">The RRD path/file_name of the condition sentence.</param>
        /// <param name="name">The name of the AlertSpec.</param>
        /// <param name="subject_href">The href of the resource that this AlertSpec should be associated with. The subject can be a ServerTemplate, Server, or Server Array</param>
        /// <param name="threshold">The threshold of the condition sentence.</param>
        /// <param name="variable">The RRD variable of the condition sentence.</param>
        /// <param name="vote_tag">Should correspond to a vote tag on a ServerArray if vote to grow or shrink</param>
        /// <param name="vote_type">Vote to grow or shrink a ServerArray when the alert is triggered. Must either escalate or vote</param>
        /// <param name="alertSpecID">ID of the AlertSpec to be updated</param>
        /// <param name="serverTemplateID">ID of the ServerTemplate where the AlertSpec to be updtaed can be found</param>
        /// <returns></returns>
        public static bool update_serverTemplate(string condition, string description, string duration, string escalation_name, string file, string name, string subject_href, string threshold, string variable, string vote_tag, string vote_type, string alertSpecID, string serverTemplateID)
        {
            string putUrl = string.Format("/api/server_templates/{0}/alert_specs/{1}", serverTemplateID, alertSpecID);
            return performUpdate(condition, description, duration, escalation_name, file, name, subject_href, threshold, variable, vote_tag, vote_type, putUrl);
        }

        /// <summary>
        /// Updates a given AlertSpec within the context of a specific ServerArray
        /// </summary>
        /// <param name="condition">The condition (operator) in the condition sentence.</param>
        /// <param name="description">The description of the AlertSpec.</param>
        /// <param name="duration">The duration in minutes of the condition sentence.</param>
        /// <param name="escalation_name">Escalate to the named alert escalation when the alert is triggered. Must either escalate or vote</param>
        /// <param name="file">The RRD path/file_name of the condition sentence.</param>
        /// <param name="name">The name of the AlertSpec.</param>
        /// <param name="subject_href">The href of the resource that this AlertSpec should be associated with. The subject can be a ServerTemplate, Server, or Server Array</param>
        /// <param name="threshold">The threshold of the condition sentence.</param>
        /// <param name="variable">The RRD variable of the condition sentence.</param>
        /// <param name="vote_tag">Should correspond to a vote tag on a ServerArray if vote to grow or shrink</param>
        /// <param name="vote_type">Vote to grow or shrink a ServerArray when the alert is triggered. Must either escalate or vote</param>
        /// <param name="alertSpecID">ID of the AlertSpec to be updated</param>
        /// <param name="serverArrayID">ID of the ServerArray where the AlertSpec to be updated can be found</param>
        /// <returns></returns>
        public static bool update_serverArray(string condition, string description, string duration, string escalation_name, string file, string name, string subject_href, string threshold, string variable, string vote_tag, string vote_type, string alertSpecID, string serverArrayID)
        {
            string putUrl = string.Format("/api/server_arrays/{0}/alert_specs/{1}", serverArrayID, alertSpecID);
            return performUpdate(condition, description, duration, escalation_name, file, name, subject_href, threshold, variable, vote_tag, vote_type, putUrl);
        }

        /// <summary>
        /// Updates a given AlertSpec within the context of a specific Server
        /// </summary>
        /// <param name="condition">The condition (operator) in the condition sentence.</param>
        /// <param name="description">The description of the AlertSpec.</param>
        /// <param name="duration">The duration in minutes of the condition sentence.</param>
        /// <param name="escalation_name">Escalate to the named alert escalation when the alert is triggered. Must either escalate or vote</param>
        /// <param name="file">The RRD path/file_name of the condition sentence.</param>
        /// <param name="name">The name of the AlertSpec.</param>
        /// <param name="subject_href">The href of the resource that this AlertSpec should be associated with. The subject can be a ServerTemplate, Server, or Server Array</param>
        /// <param name="threshold">The threshold of the condition sentence.</param>
        /// <param name="variable">The RRD variable of the condition sentence.</param>
        /// <param name="vote_tag">Should correspond to a vote tag on a ServerArray if vote to grow or shrink</param>
        /// <param name="vote_type">Vote to grow or shrink a ServerArray when the alert is triggered. Must either escalate or vote</param>
        /// <param name="alertSpecID">ID of the AlertSpec to be updated</param>
        /// <param name="serverID">ID of the Server where the AlertSpec to be updated can be found</param>
        /// <returns></returns>
        public static bool update_server(string condition, string description, string duration, string escalation_name, string file, string name, string subject_href, string threshold, string variable, string vote_tag, string vote_type, string alertSpecID, string serverID)
        {
            string putUrl = string.Format("/api/servers/{0}/alert_specs/{1}", serverID, alertSpecID);
            return performUpdate(condition, description, duration, escalation_name, file, name, subject_href, threshold, variable, vote_tag, vote_type, putUrl);
        }
        

        /// <summary>
        /// Underlying implementation for updating AlertSpec object including call to RSAPI PUT url as specified
        /// </summary>
        /// <param name="condition">The condition (operator) in the condition sentence.</param>
        /// <param name="description">The description of the AlertSpec.</param>
        /// <param name="duration">The duration in minutes of the condition sentence.</param>
        /// <param name="escalation_name">Escalate to the named alert escalation when the alert is triggered. Must either escalate or vote</param>
        /// <param name="file">The RRD path/file_name of the condition sentence.</param>
        /// <param name="name">The name of the AlertSpec.</param>
        /// <param name="subject_href">The href of the resource that this AlertSpec should be associated with. The subject can be a ServerTemplate, Server, or Server Array</param>
        /// <param name="threshold">The threshold of the condition sentence.</param>
        /// <param name="variable">The RRD variable of the condition sentence.</param>
        /// <param name="vote_tag">Should correspond to a vote tag on a ServerArray if vote to grow or shrink</param>
        /// <param name="vote_type">Vote to grow or shrink a ServerArray when the alert is triggered. Must either escalate or vote</param>
        /// <param name="putUrl">URL for Put action to be performed on</param>
        /// <returns></returns>
        private static bool performUpdate(string condition, string description, string duration, string escalation_name, string file, string name, string subject_href, string threshold, string variable, string vote_tag, string vote_type, string putUrl)
        {
            checkInputFormatting(condition, vote_type);
            List<KeyValuePair<string, string>> putData = getUpdatePutData(condition, description, duration, escalation_name, file, name, subject_href, threshold, variable, vote_tag, vote_type);
            return Core.APIClient.Instance.Put(putUrl, putData);
        }

        /// <summary>
        /// Private method handles building post data for update to a given AlertSpec
        /// </summary>
        /// <param name="condition">The condition (operator) in the condition sentence.</param>
        /// <param name="description">The description of the AlertSpec.</param>
        /// <param name="duration">The duration in minutes of the condition sentence.</param>
        /// <param name="escalation_name">Escalate to the named alert escalation when the alert is triggered. Must either escalate or vote</param>
        /// <param name="file">The RRD path/file_name of the condition sentence.</param>
        /// <param name="name">The name of the AlertSpec.</param>
        /// <param name="subject_href">The href of the resource that this AlertSpec should be associated with. The subject can be a ServerTemplate, Server, or Server Array</param>
        /// <param name="threshold">The threshold of the condition sentence.</param>
        /// <param name="variable">The RRD variable of the condition sentence.</param>
        /// <param name="vote_tag">Should correspond to a vote tag on a ServerArray if vote to grow or shrink</param>
        /// <param name="vote_type">Vote to grow or shrink a ServerArray when the alert is triggered. Must either escalate or vote</param>
        /// <returns></returns>
        private static List<KeyValuePair<string, string>> getUpdatePutData(string condition, string description, string duration, string escalation_name, string file, string name, string subject_href, string threshold, string variable, string vote_tag, string vote_type)
        {
            List<KeyValuePair<string, string>> retVal = new List<KeyValuePair<string, string>>();

            if (!string.IsNullOrWhiteSpace(condition))
            {
                retVal.Add(new KeyValuePair<string, string>(string.Format(alertSpecFormat, "condition"), condition));
            }
            if (!string.IsNullOrWhiteSpace(description))
            {
                retVal.Add(new KeyValuePair<string, string>(string.Format(alertSpecFormat, "description"), description));
            }
            if (!string.IsNullOrWhiteSpace(duration))
            {
                retVal.Add(new KeyValuePair<string, string>(string.Format(alertSpecFormat, "duration"), duration));
            }
            if (!string.IsNullOrWhiteSpace(escalation_name))
            {
                retVal.Add(new KeyValuePair<string, string>(string.Format(alertSpecFormat, "escalation_name"), escalation_name));
            }
            if (!string.IsNullOrWhiteSpace(file))
            {
                retVal.Add(new KeyValuePair<string, string>(string.Format(alertSpecFormat, "file"), file));
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                retVal.Add(new KeyValuePair<string, string>(string.Format(alertSpecFormat, "name"), name));
            }
            if (!string.IsNullOrWhiteSpace(subject_href))
            {
                retVal.Add(new KeyValuePair<string, string>(string.Format(alertSpecFormat, "subject_href"), subject_href));
            }
            if (!string.IsNullOrWhiteSpace(threshold))
            {
                retVal.Add(new KeyValuePair<string, string>(string.Format(alertSpecFormat, "threshold"), threshold));
            }
            if (!string.IsNullOrWhiteSpace(variable))
            {
                retVal.Add(new KeyValuePair<string, string>(string.Format(alertSpecFormat, "variable"), variable));
            }
            if (!string.IsNullOrWhiteSpace(vote_tag))
            {
                retVal.Add(new KeyValuePair<string, string>(string.Format(alertSpecFormat, "vote_tag"), vote_tag));
            }
            if (!string.IsNullOrWhiteSpace(vote_type))
            {
                retVal.Add(new KeyValuePair<string, string>(string.Format(alertSpecFormat, "vote_type"), vote_type));
            }
            return retVal;
        }
        #endregion
    }
}
