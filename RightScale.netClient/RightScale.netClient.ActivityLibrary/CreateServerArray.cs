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
    public sealed class CreateServerArray : Base.ServerBasedCreateActivity
    {
        [RequiredArgument]
        public InArgument<string> arrayType { get; set; }

        [RequiredArgument]
        public InArgument<string> state { get; set; }

        [RequiredArgument]
        public InArgument<List<ElasticityParam>> elasticityParams { get; set; }

        public InArgument<List<DataCenterPolicy>> dataCenterPolicies { get; set; }

        public InArgument<string> dataCenterID { get; set; }

        public OutArgument<string> serverArrayID { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            LogInformation("Creating ServerArray for ServerTemplateID: " + this.serverTemplateID.Get(context));

            if (base.authClient(context))
            {
                if (dataCenterPolicies.Get(context) == null)
                {
                    dataCenterPolicies.Set(context, new List<DataCenterPolicy>());
                }
                string arrayID = RightScale.netClient.ServerArray.create(this.arrayType.Get(context), this.dataCenterPolicies.Get(context), this.elasticityParams.Get(context), this.cloudID.Get(context), this.deploymentID.Get(context), this.serverTemplateID.Get(context), this.name.Get(context), this.state.Get(context), this.description.Get(context), this.dataCenterID.Get(context), this.inputs.Get(context), this.instanceTypeID.Get(context), this.imageID.Get(context), this.kernelImageID.Get(context), this.multiCloudImageID.Get(context), this.ramdiskImageID.Get(context), this.securityGroupIDs.Get(context), this.sshKeyID.Get(context), this.userData.Get(context), this.optimized.Get(context));
                this.serverArrayID.Set(context, arrayID);
            }

            LogInformation("Completed creating ServerArray for ServerTemplateID: " + this.serverTemplateID.Get(context));
        }
    }
}
