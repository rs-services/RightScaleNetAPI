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
    /// Custom Windows Workflow Foundation CodeActivity to launch a given server within the RightScale system
    /// </summary>
    public sealed class LaunchServer : Base.RSCodeActivity
    {
        public InArgument<string> serverID { get; set; }

        public OutArgument<bool> serverLaunched { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            LogInformation("Beginning call to launch Server id: " + this.serverID.Get(context));

            if (base.authClient(context))
            {
                bool retVal = Server.launch(this.serverID.Get(context));
                this.serverLaunched.Set(context, retVal);
            }

            LogInformation("Completed call to launch Server id: " + this.serverID.Get(context) + " with result of serverLaunched = " + this.serverLaunched.Get(context));
        }
    }
}
