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
        public List<Action> actions { get; set; }
        public string created_at { get; set; }
        public List<VolumeSnapshot> volume_snapshots { get; set; }
        public bool completed { get; set; }
        public bool from_master { get; set; }
        public int volume_snapshot_count { get; set; }
        public bool committed { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }

        #region Backup.ctor
        /// <summary>
        /// Default Constructor for Backup
        /// </summary>
        public Backup()
            : base()
        {
        }

        /// <summary>
        /// Constructor for Backup object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public Backup(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for Backup object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public Backup(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
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
