using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using RightScale.netClient.Core;
using RightScale.netClient;

namespace RightScale.netClient.ActivityLibrary
{
    /// <summary>
    /// Custom Windows Workflow Foundation CodeActivity to launch an instance of a server for a specific ServerArray within the RightScale system
    /// </summary>
    public sealed class LaunchServerArray : Base.RSCodeActivity
    {
        /// <summary>
        /// ID of the ServerArray where a server will be launched
        /// </summary>
        [RequiredArgument]
        public InArgument<string> serverArrayID { get; set; }

        /// <summary>
        /// Output variable showing whether or not the server was launched or not
        /// </summary>
        public OutArgument<bool> isLaunched { get; set; }

        /// <summary>
        /// Output variable containing ID of the server launched within the given ServerArray
        /// </summary>
        public OutArgument<string> serverID { get; set; }

        /// <summary>
        /// Execute method launches a server within the specified ServerArray
        /// </summary>
        /// <param name="context">Windows Workflow Foundation CodeActivity runtime context</param>
        protected override void Execute(System.Activities.CodeActivityContext context)
        {
            LogInformation("Starting process of launching a single server in the array");
            if (base.authClient(context))
            {
                bool retVal = false;
                string serverID = ServerArray.launch(serverArrayID.Get(context));
                this.serverID.Set(context, serverID);
                if (!string.IsNullOrWhiteSpace(serverID))
                {
                    retVal = true;
                }
                isLaunched.Set(context, retVal);
            }
            LogInformation("Completed process of launching a single server in the array id: " + this.serverArrayID.Get(context) + " with result of " + this.isLaunched.Get(context));
        }
    }
}
