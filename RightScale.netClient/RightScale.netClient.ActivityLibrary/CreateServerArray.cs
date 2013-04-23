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
    /// Custom Windows Workflow Foundation CodeActivity to create a ServerArray within the RightScale System
    /// </summary>
    public sealed class CreateServerArray : Base.ServerBasedCreateActivity
    {
        /// <summary>
        /// Type of array to be created - alert or queue
        /// </summary>
        [RequiredArgument]
        public InArgument<string> arrayType { get; set; }

        /// <summary>
        /// State to set the ServerArray to - enabled or disabled
        /// </summary>
        [RequiredArgument]
        public InArgument<string> state { get; set; }

        /// <summary>
        /// Elasticity parameters defining how the ServerArray will scale 
        /// </summary>
        [RequiredArgument]
        public InArgument<List<ElasticityParam>> elasticityParams { get; set; }

        /// <summary>
        /// DataCenterPolicy object defining where instances will be launched under the context of this ServerArray
        /// </summary>
        public InArgument<List<DataCenterPolicy>> dataCenterPolicies { get; set; }

        /// <summary>
        /// ID of the DataCenter to launch servers into
        /// </summary>
        public InArgument<string> dataCenterID { get; set; }

        /// <summary>
        /// Output parameter returns the ID of the newly created ServerArray   
        /// </summary>
        public OutArgument<string> serverArrayID { get; set; }

        /// <summary>
        /// Execute method creates a ServerArray based on the inputs provided
        /// </summary>
        /// <param name="context">Windows Workflow Foundation CodeActivity runtime context</param>
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

        /// <summary>
        /// Override to GetFriendlyName sets the name of the objet in the designer
        /// </summary>
        /// <returns>Friently Name of this custom CodeActivity</returns>
        protected override string GetFriendlyName()
        {
            return "RightScale - Create ServerArray";
        }
    }
}
