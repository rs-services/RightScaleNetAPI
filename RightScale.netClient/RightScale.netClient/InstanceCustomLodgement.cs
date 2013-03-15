using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// An InstanceCustomLodgement represents a way to create custom reports about a specific instance with a user defined quantity. Replaces the legacy Instances#setcustomlodgement interface
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeInstanceCustomLodgement.html
    /// Resources Reference: http://reference.rightscale.com/api1.5/resources/ResourceInstanceCustomLodgements.html
    /// </summary>
    public class InstanceCustomLodgement : Core.RightScaleObjectBase<InstanceCustomLodgement>
    {
        //TODO: need to write this class
        
        #region InstanceCustomLodgement.ctor
        /// <summary>
        /// Default Constructor for InstanceCustomLodgement
        /// </summary>
        public InstanceCustomLodgement()
            : base()
        {
        }

        /// <summary>
        /// Constructor for InstanceCustomLodgement object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public InstanceCustomLodgement(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for InstanceCustomLodgement object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public InstanceCustomLodgement(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        #endregion

        #region InstanceCustomLodgement Relationships

        /// <summary>
        /// ServerTemplate associated with this InstanceCustomLodgement
        /// </summary>
        public ServerTemplate serverTemplate
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("server_template"));
                return ServerTemplate.deserialize(jsonString);
            }
        }

        /// <summary>
        /// Account associated with this InstanceCustomLodgement
        /// </summary>
        public Account account
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("account"));
                return Account.deserialize(jsonString);
            }
        }

        /// <summary>
        /// Instance associated with this InstanceCustomLodgement
        /// </summary>
        public Instance instance
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("instance"));
                return Instance.deserialize(jsonString);
            }
        }

        /// <summary>
        /// Deployment associated with this InstanceCustomLodgement
        /// </summary>
        public Deployment deployment
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("deployment"));
                return Deployment.deserialize(jsonString);
            }
        }

        /// <summary>
        /// Cloud associated with this InstanceCustomLodgement
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


        #region InstanceCustomLodgement.index methods

        public static List<InstanceCustomLodgement> index()
        {
            return index(null, null);
        }

        public static List<InstanceCustomLodgement> index(List<Filter> filter)
        {
            return index(filter, null);
        }

        public static List<InstanceCustomLodgement> index(string view)
        {
            return index(null, view);
        }

        public static List<InstanceCustomLodgement> index(List<Filter> filter, string view)
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

            List<string> validFilters = new List<string>() { "timeframe" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement InstanceCustomLodgement.index
            throw new NotImplementedException();
        }
        #endregion
		
    }


}
