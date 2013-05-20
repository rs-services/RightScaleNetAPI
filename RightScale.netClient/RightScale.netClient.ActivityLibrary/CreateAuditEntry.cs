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
    /// Enumeration determining which type of object will be audited
    /// </summary>
    public enum AuditableObject
    {
        Server,
        ServerArray,
        Deployment
    }

    /// <summary>
    /// Custom Windows Workflow Foundation CodeActivity to create an Audit Entry within the RightScale system
    /// </summary>
    public sealed class CreateAuditEntry : Base.RSCodeActivity
    {
        /// <summary>
        /// Type of object being audited
        /// </summary>
        [RequiredArgument]
        public InArgument<AuditableObject> auditObjectType { get; set; }

        /// <summary>
        /// ID of object to be audited
        /// </summary>
        [RequiredArgument]
        public InArgument<string> auditObjectID { get; set; }

        /// <summary>
        /// Summary message for Audit Entry
        /// </summary>
        [RequiredArgument]
        public InArgument<string> auditObjectSummary { get; set; }

        /// <summary>
        /// Detail message for Audit Entry
        /// </summary>
        public InArgument<string> auditObjectDetail { get; set; }

        public CreateAuditEntry()
        {
            this.DisplayName = "RightScale - Create Audit Entry";
        }

        protected override bool PerformRightScaleTask(CodeActivityContext context)
        {
            bool retVal = false;
            string apiHref = getAPIHref(context);
            LogInformation("Starting call to create Audit Record on " + auditObjectType.Get(context).ToString() + " with ID of " + auditObjectID.Get(context));
            if (base.authClient(context))
            {
                AuditEntry.create(apiHref, auditObjectSummary.Get(context), auditObjectDetail.Get(context));
                retVal = true;
            }
            LogInformation("Completed creating Audit Record on " + auditObjectType.Get(context).ToString() + " with ID of " + auditObjectID.Get(context));
            return retVal;
        }
        
        /// <summary>
        /// Private method to build proper href for calling out to the RightScale API based on the AuditObjectType provided
        /// </summary>
        /// <param name="context">Code Activity Context</param>
        /// <returns>formatted href for creating an audit entry</returns>
        private string getAPIHref(CodeActivityContext context)
        {
            switch (auditObjectType.Get(context))
            {
                case AuditableObject.Server:
                    return string.Format(RightScale.netClient.APIHrefs.ServerByID, auditObjectID);
                case AuditableObject.ServerArray:
                    return string.Format(RightScale.netClient.APIHrefs.ServerArrayById, auditObjectID);
                case AuditableObject.Deployment:
                    return string.Format(RightScale.netClient.APIHrefs.DeploymentByID, auditObjectID);
                default:
                    LogWarning("Could not determine api href for " + context.ToString());
                    return string.Empty;
            }
        }

        /// <summary>
        /// Override to GetFriendlyName sets the name of the objet in the designer
        /// </summary>
        /// <returns>Friently Name of this custom CodeActivity</returns>
        protected override string GetFriendlyName()
        {
            return "RightScale - Create Audit Entry";
        }
    }
}
