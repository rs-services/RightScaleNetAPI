using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    //audit_entry
    public class AuditEntry : Core.RightScaleObjectBase<AuditEntry>
    {
        public List<Action> actions { get; set; }
        public string updated_at { get; set; }
        public List<Link> links { get; set; }
        public string summary { get; set; }
        public int detail_size { get; set; }
        public string user_email { get; set; }


        public AuditEntry()
            : base()
        {

        }

        public AuditEntry(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        public AuditEntry(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {

        }

        public static AuditEntry show(string auditEntryID)
        {
            //TODO: currently not working as expected
            Utility.CheckStringIsNumeric(auditEntryID);

            string getURL = string.Format("/api/audit_entries/{0}", auditEntryID);
            string queryString = "view=default";

            string jsonString = Core.APIClient.Instance.Get(getURL, queryString);

            return deserialize(jsonString);
        }

        #region AuditEntry.index methods

        public static List<AuditEntry> index(DateTime start_date)
        {
            return index(null, null, "25", start_date, DateTime.Now);
        }

        public static List<AuditEntry> index(DateTime start_date, DateTime end_date)
        {
            return index(null, null, "25", start_date, end_date);
        }

        public static List<AuditEntry> index(string view, string limit, DateTime start_date, DateTime end_date)
        {
            return index(null, view, limit, start_date, end_date);
        }

        public static List<AuditEntry> index(List<KeyValuePair<string, string>> filter, string limit, DateTime start_date, DateTime end_date)
        {
            return index(filter, null, limit, start_date, end_date);
        }

        public static List<AuditEntry> index(List<KeyValuePair<string, string>> filter, string view, string limit, DateTime start_date, DateTime end_date)
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

            string startDateString = start_date.ToString("yyyy/MM/dd HH:mm:ss %K");
            string endDateString = end_date.ToString("yyyy/MM/dd HH:mm:ss %K");

            //TODO: implement AuditEntry.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
