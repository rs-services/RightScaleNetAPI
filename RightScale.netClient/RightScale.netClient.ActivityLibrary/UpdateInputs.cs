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
    public sealed class UpdateInputs : Base.RSCodeActivity
    {
        public InArgument<string> serverID { get; set; }

        public InArgument<List<Input>> inputs { get; set; }
        
        public OutArgument<bool> isUpdated { get; set; }

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
