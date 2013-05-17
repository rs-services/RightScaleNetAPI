using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.Threading;
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
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override bool PerformRightScaleTask(CodeActivityContext context)
        {
            LogInformation("Starting process of launching a single server in the array");
         
            bool retVal = false;
            if (base.authClient(context))
            {
                string newServerID = ServerArray.launch(serverArrayID.Get(context));
                this.serverID.Set(context, newServerID);
                if (!string.IsNullOrWhiteSpace(newServerID))
                {
                    retVal = true;
                }
                isLaunched.Set(context, retVal);
            } 
            string completeMessage = "Completed process of launching a single server in the array id: " + this.serverArrayID.Get(context) + " with result of " + this.isLaunched.Get(context);
            LogInformation(completeMessage);
            
            return retVal;
        }

        /// <summary>
        /// Override to GetFriendlyName sets the name of the objet in the designer
        /// </summary>
        /// <returns>Friently Name of this custom CodeActivity</returns>
        protected override string GetFriendlyName()
        {
            return "RightScale - Launch ServerArray";
        }
    }
}
