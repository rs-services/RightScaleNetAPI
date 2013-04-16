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
    public abstract class ServerBasedActivity : Base.RSCodeActivity
    {
        public InArgument<List<Input>> inputs { get; set; }

        public InArgument<string> instanceTypeID { get; set; }

        public InArgument<string> imageID { get; set; }

        public InArgument<string> kernelImageID { get; set; }

        public InArgument<string> multiCloudImageID { get; set; }

        public InArgument<string> ramdiskImageID { get; set; }

        public InArgument<List<string>> securityGroupIDs { get; set; }

        public InArgument<string> sshKeyID { get; set; }

        public InArgument<string> userData { get; set; }

        public InArgument<bool> optimized { get; set; }

        public InArgument<string> description { get; set; }

        protected abstract override void Execute(CodeActivityContext context);
    }
}
