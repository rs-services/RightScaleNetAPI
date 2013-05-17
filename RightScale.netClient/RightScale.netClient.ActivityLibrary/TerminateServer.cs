using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;    
using RightScale.netClient.Core;
using RightScale.netClient;

namespace RightScale.netClient.ActivityLibrary
{
    /// <summary>
    /// Terminate Server Custom CodeActivity terminates a given Server by ID
    /// </summary>
    public sealed class TerminateServer : Base.RSCodeActivity
    {
        /// <summary>
        /// ID of the Server to terminate
        /// </summary>
        [RequiredArgument]
        public InArgument<string> serverID { get; set; }

        /// <summary>
        /// Output parameter indicating that the Server was terminated 
        /// </summary>
        public OutArgument<bool> isTerminated { get; set; }

        protected override bool PerformRightScaleTask(CodeActivityContext context)
        {
            bool retVal = false;
            LogInformation("Beginning call to terminate server (" + this.serverID.Get(context) + ")");

            if (base.authClient(context))
            {
                retVal = Server.terminate(this.serverID.Get(context));
                this.isTerminated.Set(context, retVal);
            }

            LogInformation("Done with call to terminate server (" + this.serverID.Get(context) + ") with result of " + retVal.ToString());
            return retVal;
        }

        /// <summary>
        /// Override to GetFriendlyName sets the name of the objet in the designer
        /// </summary>
        /// <returns>Friently Name of this custom CodeActivity</returns>
        protected override string GetFriendlyName()
        {
            return "RightScale - Terminate Server";
        }
    }
}
