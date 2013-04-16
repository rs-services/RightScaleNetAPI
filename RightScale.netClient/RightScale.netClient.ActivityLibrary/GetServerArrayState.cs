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
    public sealed class GetServerArrayState : Base.RSCodeActivity
    {
        [RequiredArgument]
        public InArgument<string> serverArrayID { get; set; }

        public InArgument<int> minNumServers { get; set; }

        public OutArgument<bool> isOperational { get; set; }

        public OutArgument<int> operationalCount { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            LogInformation("Beginning query to get status of ServerArray id: " + this.serverArrayID.Get(context));

            bool retVal = false;
            int operationalCount = 0;

            if (base.authClient(context))
            {
                ServerArray array = ServerArray.show(this.serverArrayID.Get(context), "default");
                foreach (Instance inst in array.currentInstances)
                {
                    if (inst.state == "operational")
                    {
                        operationalCount++;
                        if (operationalCount >= minNumServers.Get(context))
                        {
                            retVal = true;
                            break;
                        }
                    }
                }
            }
            
            this.isOperational.Set(context, retVal);
            this.operationalCount.Set(context, operationalCount);

            LogInformation("Completed query to get status of ServerArray id: " + this.serverArrayID.Get(context) + " with isOperational = " + retVal.ToString() + " and operationalCount = " + operationalCount.ToString());
        }
    }
}
