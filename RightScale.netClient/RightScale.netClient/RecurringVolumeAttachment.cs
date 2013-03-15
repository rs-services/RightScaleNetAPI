using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// A RecurringVolumeAttachment specifies a Volume/VolumeSnapshot to attach to a Server/ServerArray the next time an instance is launched.
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeRecurringVolumeAttachment.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceRecurringVolumeAttachments.html
    /// </summary>
    public class RecurringVolumeAttachment:Core.RightScaleObjectBase<RecurringVolumeAttachment>
    {

        #region RecurringVolumeAttachment Properties

        /// <summary>
        /// Name of this RecurringVolumeAttachment 
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Device specific to this RecurringVolumeAttachment
        /// </summary>
        public string device { get; set; }

        /// <summary>
        /// Datetime when this RecurringVolumeAttachment was created
        /// </summary>
        public string created_at { get; set; }

        /// <summary>
        /// Size of this RecurringVolumeAttachment
        /// </summary>
        public string size { get; set; }

        /// <summary>
        /// Datetime when this RecurringVolumeAttachment was last updated
        /// </summary>
        public string updated_at { get; set; }

        /// <summary>
        /// Storage Type of this RecurringVolumeAttachment
        /// </summary>
        public string storage_type { get; set; }

        /// <summary>
        /// Device ID of this RecurringVolumeAttachment
        /// </summary>
        public string device_id { get; set; }

        /// <summary>
        /// Runnable Type of this RecurringVolumeAttachment
        /// </summary>
        public string runnable_type { get; set; }

        /// <summary>
        /// Status of this RecurringVolumeAttachment
        /// </summary>
        public string status { get; set; }

        #endregion

        #region RecurringVolumeAttachment Relationships
        //TODO: Figure out how to do storage relationship
        //TODO: Figure out how to do runnable relationship

        /// <summary>
        /// Cloud associated with this RecurringVolumeAttachment
        /// </summary>
        public Cloud cloud
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("cloud"));
                return Cloud.deserialize(jsonString);
            }
        }

        #endregion

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

        public static List<RecurringVolumeAttachment> index(List<Filter> filter)
        {
            return index(filter, null);
        }

        public static List<RecurringVolumeAttachment> index(string view)
        {
            return index(null, view);
        }

        public static List<RecurringVolumeAttachment> index(List<Filter> filter, string view)
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
