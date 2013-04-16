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
    public abstract class ServerBasedCreateActivity : ServerBasedActivity
    {
        [RequiredArgument]
        public InArgument<string> cloudID { get; set; }

        [RequiredArgument]
        public InArgument<string> deploymentID { get; set; }

        [RequiredArgument]
        public InArgument<string> serverTemplateID { get; set; }

        [RequiredArgument]
        public InArgument<string> name { get; set; }

        protected abstract override void Execute(CodeActivityContext context);
    }
}
