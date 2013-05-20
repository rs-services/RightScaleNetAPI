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
    /// Custom Windows Workflow Foundation CodeActivity to get the current state of a given Server within the RightScale system
    /// </summary>
    public sealed class GetServerState : Base.RSCodeActivity
    {
        /// <summary>
        /// Input argument that defines which state should be considered a successful running state - defaults to 'operational'
        /// </summary>
        public InArgument<string> successSate { get; set; }

        /// <summary>
        /// ID of the server to query to get the current state
        /// </summary>
        [RequiredArgument]
        public InArgument<string> serverID { get; set; }

        /// <summary>
        /// Output argument defining the server's current state
        /// </summary>
        public OutArgument<string> serverState { get; set; }

        /// <summary>
        /// Output argument indicating whether or not the current state is deemed to be comletely launched as defined by <paramref name="successState"/>
        /// </summary>
        public OutArgument<bool> isComplete { get; set; }

        protected override bool PerformRightScaleTask(CodeActivityContext context)
        {
            bool retVal = false;
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
                    retVal = true;
                }
                else
                {
                    this.isComplete.Set(context, false);
                    retVal = true;
                }
            }
            else
            {
                throw new RightScaleAPIException("Could not authenticate to the RightScale API with the credentials supplied");
            }

            LogInformation("Completed query to get status of Server id: " + this.serverID.Get(context) + " with result of isComplete = " + this.isComplete.Get(context).ToString());
            return retVal;
        }
        
        /// <summary>
        /// Override to GetFriendlyName sets the name of the objet in the designer
        /// </summary>
        /// <returns>Friently Name of this custom CodeActivity</returns>
        protected override string GetFriendlyName()
        {
            return "RightScale - Get Server State";
        }
    }
}
