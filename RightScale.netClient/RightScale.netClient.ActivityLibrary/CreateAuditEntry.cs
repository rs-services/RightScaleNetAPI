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
    public enum AuditableObject
    {
        Server,
        ServerArray,
        Deployment
    }

    public sealed class CreateAuditEntry : Base.RSCodeActivity
    {
        [RequiredArgument]
        public InArgument<AuditableObject> auditObjectType { get; set; }

        [RequiredArgument]
        public InArgument<string> auditObjectID { get; set; }

        [RequiredArgument]
        public InArgument<string> auditObjectSummary { get; set; }

        public InArgument<string> auditObjectDetail { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            string apiHref = getAPIHref(context);
            LogInformation("Starting call to create Audit Record on " + auditObjectType.Get(context).ToString() + " with ID of " + auditObjectID.Get(context));
            if (base.authClient(context))
            {
                AuditEntry.create(apiHref, auditObjectSummary.Get(context), auditObjectDetail.Get(context)); 
            }
            LogInformation("Completed creating Audit Record on " + auditObjectType.Get(context).ToString() + " with ID of " + auditObjectID.Get(context));
        }

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
    }
}
