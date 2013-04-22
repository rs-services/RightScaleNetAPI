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
    /// Custom Windows Workflow Foundation CodeActivity to update the information for a given ServerArray within the RightScale system
    /// </summary>
    public sealed class UpdateServerArray : Base.ServerBasedUpdateActivity
    {
        /// <summary>
        /// ID of the ServerArray to be udpated
        /// </summary>
        [RequiredArgument]
        public InArgument<string> serverArrayID { get; set; }
        
        /// <summary>
        /// Type of array - alert or queue
        /// </summary>
        public InArgument<string> arrayType { get; set; }

        /// <summary>
        /// State of array - enabled or disabled
        /// </summary>
        public InArgument<string> state { get; set; }

        /// <summary>
        /// Elasticity parameters defining how the ServerArray will scale up and down
        /// </summary>
        public InArgument<List<ElasticityParam>> elasticityParams { get; set; }

        /// <summary>
        /// DataCenterPolicy objects defining where servers will be launched
        /// </summary>
        public InArgument<List<DataCenterPolicy>> dataCenterPolicies { get; set; }

        /// <summary>
        /// ID of the DataCenter to launch servers into
        /// </summary>
        public InArgument<string> dataCenterID { get; set; }
        
        /// <summary>
        /// Output argument identifying that the given ServerArray was updated
        /// </summary>
        public OutArgument<bool> isUpdated { get; set; } 

        /// <summary>
        /// Execute method updates a ServerArray with all properties that contain values.  Properties that are set to null or string.empty will not be changed.
        /// </summary>
        /// <param name="context">Windows Workflow Foundation CodeActivity runtime context</param>
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
