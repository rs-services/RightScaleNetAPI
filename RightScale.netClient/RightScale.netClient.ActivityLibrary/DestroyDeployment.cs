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

        /// <summary>
        /// Execute method manages process of destroying a given Deployment based on the ID specified
        /// </summary>
        /// <param name="context">Windows Workflow Foundation CodeActivity runtime context</param>
        protected override void Execute(System.Activities.CodeActivityContext context)
        {
            if (base.authClient(context))
            {
                bool retVal = Deployment.destroy(this.deploymentID.Get(context));
                this.isDestroyed.Set(context, retVal);
            }
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
