using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class RecurringVolumeAttachment:Core.RightScaleObjectBase<RecurringVolumeAttachment>
    {
        public string name { get; set; }
        public string device { get; set; }
        public List<Action> actions { get; set; }
        public string created_at { get; set; }
        public string size { get; set; }
        public string updated_at { get; set; }
        public string storage_type { get; set; }
        public string device_id { get; set; }
        public List<Link> links { get; set; }
        public string runnable_type { get; set; }
        public string status { get; set; }


        #region RecurringVolumeAttachment.ctor
        /// <summary>
        /// Default Constructor for RecurringVolumeAttachment
        /// </summary>
        public RecurringVolumeAttachment()
            : base()
        {
        }

        /// <summary>
        /// Constructor for RecurringVolumeAttachment object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public RecurringVolumeAttachment(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for RecurringVolumeAttachment object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public RecurringVolumeAttachment(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion
		
        
        #region RecurringVolumeAttachment.index methods

        public static List<RecurringVolumeAttachment> index()
        {
            return index(null, null);
        }

        public static List<RecurringVolumeAttachment> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        public static List<RecurringVolumeAttachment> index(string view)
        {
            return index(null, view);
        }

        public static List<RecurringVolumeAttachment> index(List<KeyValuePair<string, string>> filter, string view)
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

            List<string> validFilters = new List<string>() { "runnable_href", "storage_href" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement RecurringVolumeAttachment.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
