using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Backup : Core.RightScaleObjectBase<Backup>
    {
        public string name { get; set; }
        public string lineage { get; set; }
        public string created_at { get; set; }
        public List<VolumeSnapshot> volume_snapshots { get; set; }
        public bool completed { get; set; }
        public bool from_master { get; set; }
        public int volume_snapshot_count { get; set; }
        public bool committed { get; set; }
        public string description { get; set; }

        #region Backup.ctor
        /// <summary>
        /// Default Constructor for Backup
        /// </summary>
        public Backup()
            : base()
        {
        }

        #endregion

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

        /// <summary>
        /// Lists all of the backups with the given lineage tag. Filters can be used to search for a particular backup. If the 'latest_before' filter is set, only one backup is returned (the latest backup before the given timestamp).
        /// To get the latest completed backup, the 'completed' filter should be set to 'true' and the 'latest_before' filter should be set to the current timestamp. The format of the timestamp must be YYYY/MM/DD HH:MM:SS [+/-]ZZZZ e.g. 2011/07/11 00:00:00 +0000.
        /// To get the latest completed backup just before, say 25 June 2009, then the 'completed' filter should be set to 'true' and the 'latest_before' filter should be set to 2009/06/25 00:00:00 +0000.
        /// </summary>
        /// <returns>Collection of Backup objects</returns>
        public static List<Backup> index()
        {
            return index(null, null);
        }

        /// Lists all of the backups with the given lineage tag. Filters can be used to search for a particular backup. If the 'latest_before' filter is set, only one backup is returned (the latest backup before the given timestamp).
        /// To get the latest completed backup, the 'completed' filter should be set to 'true' and the 'latest_before' filter should be set to the current timestamp. The format of the timestamp must be YYYY/MM/DD HH:MM:SS [+/-]ZZZZ e.g. 2011/07/11 00:00:00 +0000.
        /// To get the latest completed backup just before, say 25 June 2009, then the 'completed' filter should be set to 'true' and the 'latest_before' filter should be set to 2009/06/25 00:00:00 +0000.
        /// <param name="filter">Filters limiting the return set from the API</param>
        /// <returns>Collection of Backup objects</returns>
        public static List<Backup> index(List<Filter> filter)
        {
            return index(filter, null);
        }

        /// Lists all of the backups with the given lineage tag. Filters can be used to search for a particular backup. If the 'latest_before' filter is set, only one backup is returned (the latest backup before the given timestamp).
        /// To get the latest completed backup, the 'completed' filter should be set to 'true' and the 'latest_before' filter should be set to the current timestamp. The format of the timestamp must be YYYY/MM/DD HH:MM:SS [+/-]ZZZZ e.g. 2011/07/11 00:00:00 +0000.
        /// To get the latest completed backup just before, say 25 June 2009, then the 'completed' filter should be set to 'true' and the 'latest_before' filter should be set to 2009/06/25 00:00:00 +0000.
        /// <param name="lineage">Backups belonging to this lineage</param>
        /// <returns>Collection of Backup objects</returns>
        public static List<Backup> index(string lineage)
        {
            return index(null, lineage);
        }

        /// Lists all of the backups with the given lineage tag. Filters can be used to search for a particular backup. If the 'latest_before' filter is set, only one backup is returned (the latest backup before the given timestamp).
        /// To get the latest completed backup, the 'completed' filter should be set to 'true' and the 'latest_before' filter should be set to the current timestamp. The format of the timestamp must be YYYY/MM/DD HH:MM:SS [+/-]ZZZZ e.g. 2011/07/11 00:00:00 +0000.
        /// To get the latest completed backup just before, say 25 June 2009, then the 'completed' filter should be set to 'true' and the 'latest_before' filter should be set to 2009/06/25 00:00:00 +0000.
        /// <param name="filter">Filters limiting the return set from the API</param>
        /// <param name="lineage">Backups belonging to this lineage</param>
        /// <returns>Collection of Backup objects</returns>
        public static List<Backup> index(List<Filter> filter, string lineage)
        {
            Utility.CheckStringHasValue(lineage);			
            List<string> validFilters = new List<string>() { "cloud_href", "committed", "completed", "from_master", "latest_before" };
            Utility.CheckFilterInput("filter", validFilters, filter);
            string queryString = string.Empty;
            if (filter != null && filter.Count > 0)
            {
                foreach (Filter f in filter)
                {
                    queryString += f.ToString() + "&";
                }
            }
            queryString += string.Format("lineage={0}", lineage);
            string jsonString = Core.APIClient.Instance.Get(APIHrefs.Backup, queryString);
            return deserializeList(jsonString);
        }
		#endregion
		
    }
}
