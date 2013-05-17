using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using RightScale.netClient.Core;
using RightScale.netClient;

namespace RightScale.netClient.ActivityLibrary
{
    /// <summary>
    /// Destroy Deployment custom CodeActivity destroys a given Deployment by ID
    /// </summary>
    public sealed class DestroyDeployment : Base.RSCodeActivity
    {
        /// <summary>
        /// ID of the deployment to be destroyed
        /// </summary>
        [RequiredArgument]
        public InArgument<string> deploymentID { get; set; }

        /// <summary>
        /// boolean indicating that the deployment has been destroyed
        /// </summary>
        public OutArgument<bool> isDestroyed { get; set; }

        protected override bool PerformRightScaleTask(CodeActivityContext context)
        {
            LogInformation("Beginning Destroy Deployment process for deployment [" + this.deploymentID.Get(context) + "]");
            bool retVal = false;

            if (base.authClient(context))
            {
                bool setDestroyed = Deployment.destroy(this.deploymentID.Get(context));
                this.isDestroyed.Set(context, setDestroyed);
                retVal = true;
            }

            LogInformation("Completed Destroy Deployment process for deployment [" + this.deploymentID.Get(context) + "] with success = " + retVal.ToString());
            return retVal;
        }

        /// <summary>
        /// Execute method manages process of destroying a given Deployment based on the ID specified
        /// </summary>
        /// <param name="context">Windows Workflow Foundation CodeActivity runtime context</param>
        protected override void Execute(System.Activities.CodeActivityContext context)
        {
        }

        /// <summary>
        /// Override to GetFriendlyName sets the name of the objet in the designer
        /// </summary>
        /// <returns>Friently Name of this custom CodeActivity</returns>
        protected override string GetFriendlyName()
        {
            return "RightScale - Destroy Deployment";
        }
    }
}
