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
    public sealed class UpdateServerArray : Base.RSCodeActivity
    {
        [RequiredArgument]
        public InArgument<string> serverArrayID { get; set; }
        
        public InArgument<string> arrayType { get; set; }

        public InArgument<string> state { get; set; }

        public InArgument<List<ElasticityParam>> elasticityParams { get; set; }

        public InArgument<List<DataCenterPolicy>> dataCenterPolicies { get; set; }

        public InArgument<string> dataCenterID { get; set; }

        public InArgument<string> cloudID { get; set; }

        public InArgument<string> deploymentID { get; set; }

        public InArgument<string> serverTemplateID { get; set; }

        public InArgument<string> name { get; set; }

        public InArgument<List<Input>> inputs { get; set; }

        public InArgument<string> instanceTypeID { get; set; }

        public InArgument<string> imageID { get; set; }

        public InArgument<string> kernelImageID { get; set; }

        public InArgument<string> multiCloudImageID { get; set; }

        public InArgument<string> ramdiskImageID { get; set; }

        public InArgument<List<string>> securityGroupIDs { get; set; }

        public InArgument<string> sshKeyID { get; set; }

        public InArgument<string> userData { get; set; }

        public InArgument<bool> optimized { get; set; }

        public InArgument<string> description { get; set; }

        public OutArgument<bool> isUpdated { get; set; } 

        protected override void Execute(System.Activities.CodeActivityContext context)
        {
            LogInformation("Beginning update ServerArray id: " + this.serverArrayID.Get(context));

            if (base.authClient(context))
            {
                bool retVal = ServerArray.update(serverArrayID.Get(context), arrayType.Get(context), dataCenterPolicies.Get(context), deploymentID.Get(context), description.Get(context), elasticityParams.Get(context), cloudID.Get(context), dataCenterID.Get(context), inputs.Get(context), instanceTypeID.Get(context), imageID.Get(context), kernelImageID.Get(context), multiCloudImageID.Get(context), ramdiskImageID.Get(context), securityGroupIDs.Get(context), serverTemplateID.Get(context), sshKeyID.Get(context), userData.Get(context), name.Get(context), optimized.Get(context), state.Get(context));
                this.isUpdated.Set(context, retVal);
            }

            LogInformation("Completed update ServerArray id: " + this.serverArrayID.Get(context) + " with isUpdated = " + this.isUpdated.Get(context));
        }
    }
}
