using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// An Audit Entry can be used to track various activities of a resource.
    /// </summary>
    public class AuditEntry : Core.RightScaleObjectBase<AuditEntry>
    {
        #region AuditEntry Properties

        /// <summary>
        /// Timestamp representing whenthe AuditEntry was last updated
        /// </summary>
        public string updated_at { get; set; }

        /// <summary>
        /// Summary of this audit entry
        /// </summary>
        public string summary { get; set; }

        /// <summary>
        /// Size/length of the AuditEntry's detail
        /// </summary>
        public int detail_size { get; set; }

        /// <summary>
        /// User's email responsible for this audit entry
        /// </summary>
        public string user_email { get; set; }

        /// <summary>
        /// Detailed message for audit entry
        /// </summary>
        public string audit_detail
        {
            get
            {
                string returnVal = AuditEntry.detail(this.ID);
                this.detail_size = returnVal.Length;
                return returnVal;
            }
        }

        #endregion

        #region ID Properties

        /// <summary>
        /// ID of the resource that this Audit Entry relates to.
        /// </summary>
        public string AuditeeID
        {
            get
            {
                return getLinkIDValue("auditee");
            }
        }

        #endregion

        #region AuditEntry.ctor()

        /// <summary>
        /// Default Constructor for AuditEntry
        /// </summary>
        public AuditEntry()
            : base()
        {

        }

        /// <summary>
        /// Constructor for AuditEntry object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public AuditEntry(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for AuditEntry object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public AuditEntry(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {

        }
        
        #endregion

        #region AuditEntry.show() method

        /// <summary>
        /// Lists the attributes of a given audit entry.
        /// </summary>
        /// <param name="auditEntryID">ID of the AuditEntry to show</param>
        /// <returns>Populated instance of AuditEntry object</returns>
        public static AuditEntry show(string auditEntryID)
        {
            //TODO: currently not working as expected
            Utility.CheckStringIsNumeric(auditEntryID);

            string getURL = string.Format("/api/audit_entries/{0}", auditEntryID);
            string queryString = "view=default";

            string jsonString = Core.APIClient.Instance.Get(getURL, queryString);

            return deserialize(jsonString);
        }

        #endregion

        #region AuditEntry.index methods

        /// <summary>
        /// Lists AuditEntries of the account. Due to the potentially large number of audit entries, a start and end date must be provided during an index call to limit the search. 
        /// </summary>
        /// <param name="start_date">The start date for retrieving audit entries</param>
        /// <returns>Collection of AuditEntry objects from the start_time defined</returns>
        public static List<AuditEntry> index(DateTime start_date)
        {
            return index(null, null, "25", start_date, DateTime.Now);
        }

        /// <summary>
        /// Lists AuditEntries of the account. Due to the potentially large number of audit entries, a start and end date must be provided during an index call to limit the search. 
        /// Using the available filters, one can select or group which audit entries to retrieve.
        /// </summary>
        /// <param name="start_date">The start date for retrieving audit entries</param>
        /// <param name="end_date">The end date for retrieving audit entries (the format must be the same as start date). The time period between start and end date must be less than 3 months (93 days).</param>
        /// <returns>Collection of AuditEntry objects from the start_time to the end_date defined</returns>
        public static List<AuditEntry> index(DateTime start_date, DateTime end_date)
        {
            return index(null, null, "25", start_date, end_date);
        }

        /// <summary>
        /// Lists AuditEntries of the account. Due to the potentially large number of audit entries, a start and end date must be provided during an index call to limit the search. The format of the dates must be YYYY/MM/DD HH:MM:SS [+/-]ZZZZ e.g. 2011/07/11 00:00:00 +0000. A maximum of 1000 records will be returned by each index call.
        /// Using the available filters, one can select or group which audit entries to retrieve.
        /// </summary>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include.</param>
        /// <param name="limit">Limit the audit entries to this number. The limit should >= 1 and <= 1000</param>       
        /// <param name="start_date">The start date for retrieving audit entries</param>
        /// <param name="end_date">The end date for retrieving audit entries (the format must be the same as start date). The time period between start and end date must be less than 3 months (93 days).</param>
        /// <returns>Collection of AuditEntry objects from the start_time defined with a limit and view as specified</returns>
        public static List<AuditEntry> index(string view, string limit, DateTime start_date, DateTime end_date)
        {
            return index(null, view, limit, start_date, end_date);
        }

        /// <summary>
        /// Lists AuditEntries of the account. Due to the potentially large number of audit entries, a start and end date must be provided during an index call to limit the search. The format of the dates must be YYYY/MM/DD HH:MM:SS [+/-]ZZZZ e.g. 2011/07/11 00:00:00 +0000. A maximum of 1000 records will be returned by each index call.
        /// Using the available filters, one can select or group which audit entries to retrieve.
        /// </summary>
        /// <param name="filter">Filter parameters for index query</param>
        /// <param name="limit">Limit the audit entries to this number. The limit should >= 1 and <= 1000</param>
        /// <param name="start_date">The start date for retrieving audit entries</param>
        /// <param name="end_date">The end date for retrieving audit entries (the format must be the same as start date). The time period between start and end date must be less than 3 months (93 days).</param>
        /// <returns>Collection of AuditEntry objects from the start_time defined with a limit and filter as specified</returns>
        public static List<AuditEntry> index(List<Filter> filter, string limit, DateTime start_date, DateTime end_date)
        {
            return index(filter, null, limit, start_date, end_date);
        }

        /// <summary>
        /// Lists AuditEntries of the account. Due to the potentially large number of audit entries, a start and end date must be provided during an index call to limit the search. The format of the dates must be YYYY/MM/DD HH:MM:SS [+/-]ZZZZ e.g. 2011/07/11 00:00:00 +0000. A maximum of 1000 records will be returned by each index call.
        /// Using the available filters, one can select or group which audit entries to retrieve.
        /// </summary>
        /// <param name="filter">Filter parameters for index query</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include.</param>
        /// <param name="limit">Limit the audit entries to this number. The limit should >= 1 and <= 1000</param>
        /// <param name="start_date">The start date for retrieving audit entries</param>
        /// <param name="end_date">The end date for retrieving audit entries (the format must be the same as start date). The time period between start and end date must be less than 3 months (93 days).</param>
        /// <returns>Collection of AuditEntry objects from the start_time defined with a limit, filter and view as specified</returns>
        public static List<AuditEntry> index(List<Filter> filter, string view, string limit, DateTime start_date, DateTime end_date)
        {
            string getHref = "/api/audit_entries";

            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default" };
                Utility.CheckStringInput("view", validViews, view);
            }

            List<string> validFilters = new List<string>() { "auditee_href", "user_email" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            int intCheck;
            if (Int32.TryParse(limit, out intCheck))
            {
                if (intCheck >= 1 && intCheck <= 1000)
                {
                    //this is a good thing... should trace this out
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Input 'limit' was out of bounds.  Limit cannot be less than 1 or greater than 1000: value = " + limit);
                }
            }
            else
            {
                throw new ArgumentException("Input 'limit' was non-numeric: {" + limit + "}");
            }

            if (end_date.Subtract(start_date).Days > 93)
            {
                throw new ArgumentException(string.Format("The difference between the start date [{0}] and the end date [{1}] must not be greater than 93 days", start_date, end_date));
            }

            string timeOffset = DateTime.Now.ToString("%K").Replace(":", "");

            string startDateString = start_date.ToString("yyyy/MM/dd HH:mm:ss ") + timeOffset;
            string endDateString = end_date.ToString("yyyy/MM/dd HH:mm:ss ") + timeOffset;

            List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>();
            getParams.Add(new KeyValuePair<string, string>("end_date", endDateString));
            getParams.Add(new KeyValuePair<string, string>("limit", limit));
            getParams.Add(new KeyValuePair<string, string>("start_date", startDateString));
            if (!string.IsNullOrWhiteSpace(view))
            {
                getParams.Add(new KeyValuePair<string, string>("view", view));
            }

            string queryString = Utility.BuildGetQueryString(getParams);

            if (filter != null && filter.Count > 0)
            {
                queryString += Utility.BuildFilterString(filter);
            }

            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);

            return deserializeList(jsonString);

        }
        #endregion

        #region AuditEntry.create methods

        /// <summary>
        /// Creates a new AuditEntry with the given parameters.
        /// </summary>
        /// <param name="auditee_href">The href of the resource that this audit entry should be associated with (e.g. an instance's href)</param>
        /// <param name="audit_summary">The summary of the audit entry to be created, maximum length is 255 characters.</param>
        /// <returns>ID of AuditEntry that was created</returns>
        public static string create(string auditee_href, string audit_summary)
        {
            return create(auditee_href, audit_summary, string.Empty);
        }

        /// <summary>
        /// Creates a new AuditEntry with the given parameters.
        /// </summary>
        /// <param name="auditee_href">The href of the resource that this audit entry should be associated with (e.g. an instance's href)</param>
        /// <param name="audit_summary">The summary of the audit entry to be created, maximum length is 255 characters.</param>
        /// <param name="detail">The initial details of the audit entry to be created.</param>
        /// <returns>ID of AuditEntry that was created</returns>
        public static string create(string auditee_href, string audit_summary, string detail)
        {
            string putHref = "/api/audit_entries";

            List<KeyValuePair<string, string>> putParameters = new List<KeyValuePair<string, string>>();
            putParameters.Add(new KeyValuePair<string, string>("audit_entry[auditee_href]", auditee_href));
            if (!string.IsNullOrWhiteSpace(detail))
            {
                putParameters.Add(new KeyValuePair<string, string>("audit_entry[detail]", detail));
            }
            putParameters.Add(new KeyValuePair<string, string>("audit_entry[summary]", audit_summary));

            List<string> retVal = Core.APIClient.Instance.Post(putHref, putParameters, "location");
            return retVal.Last<string>().Split('/').Last<string>();
        }

        #endregion

        #region AuditEntry.update methods

        /// <summary>
        /// Updates the summary of a given AuditEntry.
        /// </summary>
        /// <param name="auditID">ID of the audit entry to be edited</param>
        /// <param name="summary">The updated summary for the audit entry, maximum length is 255 characters.</param>
        /// <returns>true if successful, false if not</returns>
        public static bool update(string auditID, string summary)
        {
            string putURL = string.Format("/api/audit_entries/{0}", auditID);
            List<KeyValuePair<string, string>> putParams = new List<KeyValuePair<string, string>>();
            putParams.Add(new KeyValuePair<string, string>("audit_entry[summary]", summary));

            return Core.APIClient.Instance.Put(putURL, putParams);
        }

        #endregion

        #region AuditEntry.append methods

        /// <summary>
        /// Appends more details to a given AuditEntry. Each audit entry detail is stored as one chunk, the offset determines the location of that chunk within the overall audit entry details section. For example, if you create an AuditEntry and append "DEF" at offset 10, and later append "ABC" at offset 9, the overall audit entry details will be "ABCDEF". Use the \n character to separate details by new lines.
        /// </summary>
        /// <param name="auditID">ID of the audit entry to be appended</param>
        /// <param name="detail">The details to be appended to the audit entry record.</param>
        /// <returns>true if successful, false if not</returns>
        public static bool append(string auditID, string detail)
        {
            return append(auditID, detail, "1");
        }

        /// <summary>
        /// Appends more details to a given AuditEntry. Each audit entry detail is stored as one chunk, the offset determines the location of that chunk within the overall audit entry details section. For example, if you create an AuditEntry and append "DEF" at offset 10, and later append "ABC" at offset 9, the overall audit entry details will be "ABCDEF". Use the \n character to separate details by new lines.
        /// </summary>
        /// <param name="auditid">ID of the audit entry to be appended</param>
        /// <param name="detail">The details to be appended to the audit entry record.</param>
        /// <param name="offset">The offset where the new details should be appended to in the audit entry's existing details section.</param>
        /// <returns>true if successful, false if not</returns>
        public static bool append(string auditID, string detail, string offset)
        {
            string postHref = string.Format("/api/audit_entries/{0}/append", auditID);
            Utility.CheckStringIsNumeric(offset);
            List<KeyValuePair<string, string>> postParameters = new List<KeyValuePair<string, string>>();
            postParameters.Add(new KeyValuePair<string, string>("detail", detail));
            postParameters.Add(new KeyValuePair<string, string>("offset", offset));
            return Core.APIClient.Instance.Post(postHref, postParameters);
        }

        #endregion

        #region AuditEntry.detail methods

        /// <summary>
        /// shows the details of a given AuditEntry. Note that the media type of the response is simply text.
        /// </summary>
        /// <param name="auditID">Audit ID of the entry to retrieve detail for</param>
        /// <returns>details of AuditEntry defined by ID passed in</returns>
        public static string detail(string auditID)
        {
            string getHref = string.Format("/api/audit_entries/{0}/detail", auditID);
            return Core.APIClient.Instance.Get(getHref);
        }

        #endregion
    }
}
