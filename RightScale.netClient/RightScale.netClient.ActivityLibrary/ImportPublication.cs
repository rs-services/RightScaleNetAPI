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
    public sealed class ImportPublication : Base.RSCodeActivity
    {
        [RequiredArgument]
        public InArgument<string> publicationID { get; set; }

        public OutArgument<string> serverTemplateID { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            LogInformation("Beginning call to import publication id: " + this.publicationID.Get(context));
            if (base.authClient(context))
            {
                ServerTemplate st = Publication.import(publicationID.Get(context));
                this.serverTemplateID.Set(context, st);
            }
            LogInformation("Completed call to import publication id: " + this.publicationID.Get(context) + " with serverTemplateID: " + this.serverTemplateID.Get(context));
        }
    }
}
