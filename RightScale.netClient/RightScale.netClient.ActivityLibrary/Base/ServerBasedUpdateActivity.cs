using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using RightScale.netClient.Core;
using RightScale.netClient;

namespace RightScale.netClient.ActivityLibrary.Base
{
    public abstract class ServerBasedUpdateActivity : ServerBasedActivity
    {
        public InArgument<string> cloudID { get; set; }

        public InArgument<string> deploymentID { get; set; }

        public InArgument<string> serverTemplateID { get; set; }

        public InArgument<string> name { get; set; }

        protected abstract override void Execute(CodeActivityContext context);
    }
}
