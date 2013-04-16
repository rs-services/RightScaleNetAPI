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
    public sealed class GetServerState : Base.RSCodeActivity
    {
        public InArgument<string> successSate { get; set; }

        [RequiredArgument]
        public InArgument<string> serverID { get; set; }

        public OutArgument<string> serverState { get; set; }

        public OutArgument<bool> isComplete { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            LogInformation("Beginning query to get status of Server id: " + this.serverID.Get(context));

            if (string.IsNullOrWhiteSpace(this.successSate.Get(context)))
            {
                this.successSate.Set(context, "operational");
            }

            if (base.authClient(context))
            {
                string state = Server.show(this.serverID.Get(context)).state;
                this.serverState.Set(context, state);
                if (this.serverState.Get(context).ToLower() == this.successSate.Get(context).ToLower())
                {
                    this.isComplete.Set(context, true);
                }
                else
                {
                    this.isComplete.Set(context, false);
                }
            }
            else
            {
                throw new RightScaleAPIException("Could not authenticate to the RightScale API with the credentials supplied");
            }

            LogInformation("Completed query to get status of Server id: " + this.serverID.Get(context) + " with result of isComplete = " + this.isComplete.Get(context).ToString());
        }
    }
}
