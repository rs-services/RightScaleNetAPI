using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    //alert_spec
    public class AlertSpec : Core.RightScaleObjectBase<AlertSpec>
    {
        public string name { get; set; }
        public int duration { get; set; }
        public List<object> actions { get; set; }
        public string created_at { get; set; }
        public string threshold { get; set; }
        public string updated_at { get; set; }
        public string condition { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }
        public string file { get; set; }
        public string variable { get; set; }
        public string escalation_name { get; set; }

        public AlertSpec()
            : base()
        {

        }

        public AlertSpec(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        public AlertSpec(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {

        }

        #region AlertSpec.show methods

        public static AlertSpec show_server(string serverID, string alertSpecID)
        {
            Utility.CheckStringIsNumeric(serverID);
            Utility.CheckStringIsNumeric(alertSpecID);

            string getURL = string.Format("/api/servers/{0}/alert_specs/{1}", serverID, alertSpecID);

            string jsonString = Core.APIClient.Instance.Get(getURL);

            return deserialize(jsonString);
        }

        public static AlertSpec show_serverArray(string serverArrayID, string alertSpecID)
        {
            Utility.CheckStringIsNumeric(serverArrayID);
            Utility.CheckStringIsNumeric(alertSpecID);

            string getURL = string.Format("/api/server_arrays/{0}/alert_specs/{1}", serverArrayID, alertSpecID);

            string jsonString = Core.APIClient.Instance.Get(getURL);

            return deserialize(jsonString);
        }

        public static AlertSpec show_serverTemplate(string serverTemplateID, string alertSpecID)
        {
            Utility.CheckStringIsNumeric(serverTemplateID);
            Utility.CheckStringIsNumeric(alertSpecID);

            string getURL = string.Format("/api/server_templates/{0}/alert_specs/{1}", serverTemplateID, alertSpecID);

            string jsonString = Core.APIClient.Instance.Get(getURL);

            return deserialize(jsonString);
        }

        public static AlertSpec show(string alertSpecID)
        {
            Utility.CheckStringIsNumeric(alertSpecID);

            string getURL = string.Format("/api/alert_specs/{0}", alertSpecID);

            string jsonString = Core.APIClient.Instance.Get(getURL);

            return deserialize(jsonString);
        }

        #endregion

        #region AlertSpec.index methods

        public static List<AlertSpec> index()
        {
            return index(null, null);
        }

        public static List<AlertSpec> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        public static List<AlertSpec> index(string view)
        {
            return index(null, view);
        }

        public static List<AlertSpec> index(List<KeyValuePair<string, string>> filter, string view)
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

            //TODO: implement AlertSpec.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
