using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Backup :Core.RightScaleObjectBase<Backup>
    {
        public string name { get; set; }
        public string lineage { get; set; }
        public List<Action> actions { get; set; }
        public string created_at { get; set; }
        public List<VolumeSnapshot> volume_snapshots { get; set; }
        public bool completed { get; set; }
        public bool from_master { get; set; }
        public int volume_snapshot_count { get; set; }
        public bool committed { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }

        #region Backup.show() methods

        public static Backup show(string backupID)
        {
            Utility.CheckStringIsNumeric(backupID);

            string getURL = string.Format("/api/backups/{0}", backupID);

            string jsonString = Core.APIClient.Instance.Get(getURL);

            return deserialize(jsonString);
        }

        #endregion

        #region Backup.index methods

        public static List<Backup> index()
        {
            return index(null, null);
        }

        public static List<Backup> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        public static List<Backup> index(string view)
        {
            return index(null, view);
        }

        public static List<Backup> index(List<KeyValuePair<string, string>> filter, string lineage)
        {
            if(string.IsNullOrWhiteSpace(lineage))
            {
                throw new ArgumentException("Input 'lineage' is requred for api calls to gather information about a given backup");
            }
			
            List<string> validFilters = new List<string>() { "cloud_href", "committed", "completed", "from_master", "latest_before" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement Backup.index
            throw new NotImplementedException();
        }
		#endregion
		
    }
}
