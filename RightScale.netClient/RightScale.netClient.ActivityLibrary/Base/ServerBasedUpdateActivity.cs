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
    /// Base Class for Server and ServerArray update calls with properties provided
    /// </summary>
    public abstract class ServerBasedUpdateActivity : ServerBasedActivity
    {
        /// <summary>
        /// ID of the Cloud to be set for the Server/ServerArray
        /// </summary>
        public InArgument<string> cloudID { get; set; }

        /// <summary>
        /// Deployment ID for the Server/ServerArray
        /// </summary>
        public InArgument<string> deploymentID { get; set; }

        /// <summary>
        /// ServerTemplate to be set for the Server/ServerARray
        /// </summary>
        public InArgument<string> serverTemplateID { get; set; }

        /// <summary>
        /// Name to be set for the Server/ServerArray
        /// </summary>
        public InArgument<string> name { get; set; }

    }
}
