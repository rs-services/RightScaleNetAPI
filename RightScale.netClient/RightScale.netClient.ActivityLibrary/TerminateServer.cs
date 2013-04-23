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

        /// <summary>
        /// Execute method manages process of terminating a given server
        /// </summary>
        /// <param name="context">Windows Workflow Foundation CodeActivity runtime context</param>
        protected override void Execute(System.Activities.CodeActivityContext context)
        {
            if (base.authClient(context))
            {
                bool retVal = Server.terminate(this.serverID.Get(context));
                this.isTerminated.Set(context, retVal);
            }
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
