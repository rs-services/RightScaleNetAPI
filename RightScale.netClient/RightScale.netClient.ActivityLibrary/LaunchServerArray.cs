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
        [RequiredArgument]
        public InArgument<string> serverArrayID { get; set; }

        public OutArgument<bool> isLaunched { get; set; }

        public OutArgument<string> serverID { get; set; }

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
