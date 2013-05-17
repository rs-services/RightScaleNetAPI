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
    /// Custom Windows Workflow Foundation CodeActivity to import a given publication to the account for the logged in API user within the RightScale system
    /// </summary>
    public sealed class ImportPublication : Base.RSCodeActivity
    {
        /// <summary>
        /// ID of the publication to import into the API user's account
        /// </summary>
        [RequiredArgument]
        public InArgument<string> publicationID { get; set; }

        /// <summary>
        /// Output argument defining the serverTemplate ID corresponding to the newly imported object
        /// </summary>
        public OutArgument<string> serverTemplateID { get; set; }
        
        protected override bool PerformRightScaleTask(CodeActivityContext context)
        {
            bool retVal = false;
            LogInformation("Beginning call to import publication id: " + this.publicationID.Get(context));
            if (base.authClient(context))
            {
                ServerTemplate st = Publication.import(publicationID.Get(context));
                if (st != null && !string.IsNullOrWhiteSpace(st.ID))
                {
                    this.serverTemplateID.Set(context, st.ID);
                    retVal = true;
                }
            }
            LogInformation("Completed call to import publication id: " + this.publicationID.Get(context) + " with serverTemplateID: " + this.serverTemplateID.Get(context));
            return retVal;
        }

        /// <summary>
        /// Override to GetFriendlyName sets the name of the objet in the designer
        /// </summary>
        /// <returns>Friently Name of this custom CodeActivity</returns>
        protected override string GetFriendlyName()
        {
            return "RightScale - Import Publication";
        }
    }
}
