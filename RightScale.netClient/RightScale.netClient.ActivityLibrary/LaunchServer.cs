using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using RightScale.netClient.Core;
using System.Threading;
using RightScale.netClient;

namespace RightScale.netClient.ActivityLibrary
{
    /// <summary>
    /// Custom Windows Workflow Foundation CodeActivity to launch a given server within the RightScale system
    /// </summary>
    public sealed class LaunchServer : Base.RSCodeActivity
    {
        /// <summary>
        /// ID of the Server to launch
        /// </summary>
        [RequiredArgument]
        public InArgument<string> serverID { get; set; }

        /// <summary>
        /// Output argument identifying whether or not the specific server was launched
        /// </summary>
        public OutArgument<bool> serverLaunched { get; set; }
        
        /// <summary>
        /// Exectute method launches the given server specified within the input variable collection
        /// </summary>
        /// <param name="context">Windows Workflow Foundation CodeActivity runtime contextss</param>
        protected override bool PerformRightScaleTask(CodeActivityContext context)
        {
            bool retVal = false;

            LogInformation("Beginning call to launch Server id: " + this.serverID.Get(context));

            if (base.authClient(context))
            {
                retVal = Server.launch(this.serverID.Get(context));
                this.serverLaunched.Set(context, retVal);
            }

            string completeMessage = "Completed call to launch Server id: " + this.serverID.Get(context) + " with result of serverLaunched = " + this.serverLaunched.Get(context);
            LogInformation(completeMessage);

            return retVal;
        }

        /// <summary>
        /// Override to GetFriendlyName sets the name of the objet in the designer
        /// </summary>
        /// <returns>Friently Name of this custom CodeActivity</returns>
        protected override string GetFriendlyName()
        {
            return "RightScale - Launch Server";
        }
    }
}
