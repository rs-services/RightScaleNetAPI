using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using RightScale.netClient.Core;
using RightScale.netClient;

namespace RightScale.netClient.ActivityLibrary.Base
{
    /// <summary>
    /// Base Class for Server and ServerArray create calls with required properties provided
    /// </summary>
    public abstract class ServerBasedCreateActivity : ServerBasedActivity
    {
        /// <summary>
        /// ID of the Cloud to be set for the Server/ServerArray
        /// </summary>
        [RequiredArgument]
        public InArgument<string> cloudID { get; set; }

        /// <summary>
        /// Deployment ID for the Server/ServerArray
        /// </summary>
        [RequiredArgument]
        public InArgument<string> deploymentID { get; set; }

        /// <summary>
        /// ServerTemplate to be set for the Server/ServerARray
        /// </summary>
        [RequiredArgument]
        public InArgument<string> serverTemplateID { get; set; }

        /// <summary>
        /// Name to be set for the Server/ServerArray
        /// </summary>
        [RequiredArgument]
        public InArgument<string> name { get; set; }
    }
}
