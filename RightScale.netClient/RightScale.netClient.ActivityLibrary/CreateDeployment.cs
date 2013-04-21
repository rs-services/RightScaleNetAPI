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
    /// Custom Windows Workflow Foundation CodeActivity creates a Deployment within the RightScale system
    /// </summary>
    public sealed class CreateDeployment : Base.RSCodeActivity
    {
        /// <summary>
        /// Name to be used when creating the deployment
        /// </summary>
        [RequiredArgument]
        public InArgument<string> DeploymentName { get; set; }

        /// <summary>
        /// Description to be used when creating the deployment
        /// </summary>
        public InArgument<string> DeploymentDescription { get; set; }

        /// <summary>
        /// Tag scope to assign to the newly created deployment
        /// </summary>
        public InArgument<string> DeploymentTagScope { get; set; }

        /// <summary>
        /// Inputs to be set for the newly created deployment
        /// </summary>
        public InArgument<List<Input>> inputs { get; set; }

        /// <summary>
        /// Output containing the ID of the newly created deployment
        /// </summary>
        public OutArgument<string> DeploymentID { get; set; }

        /// <summary>
        /// Execute method creates a new deployment based on the inputs provided
        /// </summary>
        /// <param name="context">Windows Workflow Foundation CodeActivity runtime context</param>
        protected override void Execute(CodeActivityContext context)
        {
            LogInformation("Beginning Create Deployment process for new deployment [" + this.DeploymentName.Get(context) + "]");
            string retVal = string.Empty;

            if (base.authClient(context))
            {
                retVal = Deployment.create(this.DeploymentName.Get(context), this.DeploymentDescription.Get(context), this.DeploymentTagScope.Get(context));
                
                if (this.inputs.Get(context) != null && this.inputs.Get(context).Count > 0)
                {
                    LogInformation("Attempting to set " + this.inputs.Get(context).Count.ToString() + " inputs on deployment id: " + retVal);
                    bool inputsSet = Input.multi_update_deployment(retVal, this.inputs.Get(context));
                    if (!inputsSet)
                    {
                        throw new RightScaleAPIException("Setting inputs ws not successful.  Please check the input list and try again");
                    }
                    LogInformation("Inputs successfully set for deployment id: " + retVal);
                }

                this.DeploymentID.Set(context, retVal);
                LogInformation("Completed Create Deployment process for new deployment [" + this.DeploymentName.Get(context) + "] with ID of " + retVal);
            }
        }

    }
}
