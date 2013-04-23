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
    /// Custom Windows Workflow Foundation CodeActivity to enable a given ServerArray within the RightScale system
    /// </summary>
    public sealed class EnableServerArray : Base.RSCodeActivity
    {
        /// <summary>
        /// ID of the ServerArrray to enable
        /// </summary>
        [RequiredArgument]
        public InArgument<string> serverArrayID { get; set; }

        /// <summary>
        /// Boolean indicating success of call to RightScale API - showing that ServerArray is now active
        /// </summary>
        public OutArgument<bool> isEnabled { get; set; }

        /// <summary>
        /// Execute method enables the specific ServerArray as defined by inputs
        /// </summary>
        /// <param name="context">Windows Workflow Foundation CodeActivity runtime context</param>
        protected override void Execute(CodeActivityContext context)
        {
            LogInformation("Beginning call to enable ServerArray " + this.serverArrayID.Get(context));

            if (base.authClient(context))
            {
                bool retVal = ServerArray.setEnabled(this.serverArrayID.Get(context));
                this.isEnabled.Set(context, retVal);
            }

            LogInformation("Completed call to enable ServerArray " + this.serverArrayID.Get(context) + " with return value of " + this.isEnabled.Get(context).ToString());
        }

        /// <summary>
        /// Override to GetFriendlyName sets the name of the objet in the designer
        /// </summary>
        /// <returns>Friently Name of this custom CodeActivity</returns>
        protected override string GetFriendlyName()
        {
            return "RightScale - Enable ServerArray";
        }
    }
}
