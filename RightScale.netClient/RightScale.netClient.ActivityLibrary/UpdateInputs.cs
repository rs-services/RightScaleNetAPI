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
    /// Custom Windows Workflow Foundation CodeActivity to update inputs for a given Server within the RightScale system
    /// </summary>
    public sealed class UpdateInputs : Base.RSCodeActivity
    {
        /// <summary>
        /// ID of the Server to be updated
        /// </summary>
        [RequiredArgument]
        public InArgument<string> serverID { get; set; }

        /// <summary>
        /// Collection of inputs to update within the context of the given Server specified
        /// </summary>
        [RequiredArgument]
        public InArgument<List<Input>> inputs { get; set; }
        
        /// <summary>
        /// Output argument identifies if the given Server was updated successfully
        /// </summary>
        public OutArgument<bool> isUpdated { get; set; }

        /// <summary>
        /// Execute method updates the inputs of a given server
        /// </summary>
        /// <param name="context">Windows Workflow Foundation CodeActivity runtime context</param>
        protected override void Execute(CodeActivityContext context)
        {
            LogInformation("Beginning Input Update Process for Server id: " + this.serverID.Get(context));

            this.isUpdated.Set(context, false);
            if (base.authClient(context))
            {
                Instance nextInstance = Server.show(this.serverID.Get(context)).nextInstance;
                bool updated = Input.multi_update_instance(nextInstance.cloud.ID, nextInstance.ID, inputs.Get(context));
                this.isUpdated.Set(context, updated);
            }

            LogInformation("Completed Input update process for Server id: " + this.serverID.Get(context) + " with result of isUpdated = " + this.isUpdated.Get(context).ToString());
        }
    }
}
