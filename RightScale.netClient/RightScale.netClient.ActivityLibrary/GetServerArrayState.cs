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
    /// Custom Windows Workflow Foundation CodeActivity to get information about the state of a given ServerArray within the RightScale system
    /// </summary>
    public sealed class GetServerArrayState : Base.RSCodeActivity
    {
        /// <summary>
        /// ID of the ServerArray whose state should be returned
        /// </summary>
        [RequiredArgument]
        public InArgument<string> serverArrayID { get; set; }

        /// <summary>
        /// Minimum number of servers in 'operational' state to be considered 'fully operational'
        /// </summary>
        public InArgument<int> minNumServers { get; set; }

        /// <summary>
        /// Boolean indicating that the ServerArray is fully operational based on inputs
        /// </summary>
        public OutArgument<bool> isOperational { get; set; }

        /// <summary>
        /// Count of servers that are in 'operational' state
        /// </summary>
        public OutArgument<int> operationalCount { get; set; }

        protected override bool PerformRightScaleTask(CodeActivityContext context)
        {
            bool retVal = false;
            bool isReady = false;

            LogInformation("Beginning query to get status of ServerArray id: " + this.serverArrayID.Get(context));
            
            int operationalCount = 0;
            int minServers = 1;

            if (this.minNumServers == null)
            {
                if (this.minNumServers.Get(context) > 0)
                {
                    minServers = this.minNumServers.Get(context);
                }
            }

            if (base.authClient(context))
            {
                ServerArray array = ServerArray.show(this.serverArrayID.Get(context), "default");
                foreach (Instance inst in array.currentInstances)
                {
                    if (inst.state.ToLower().ToString() == "operational")
                    {
                        operationalCount++;
                        if (operationalCount >= minServers)
                        {
                            isReady = true;
                            retVal = true;
                            break;
                        }
                    }
                }
            }

            this.isOperational.Set(context, isReady);
            this.operationalCount.Set(context, operationalCount);

            LogInformation("Completed query to get status of ServerArray id: " + this.serverArrayID.Get(context) + " with isOperational = " + retVal.ToString() + " and operationalCount = " + operationalCount.ToString());
            return retVal;
        }

        /// <summary>
        /// Override to GetFriendlyName sets the name of the objet in the designer
        /// </summary>
        /// <returns>Friently Name of this custom CodeActivity</returns>
        protected override string GetFriendlyName()
        {
            return "RightScale - Get ServerArray State";
        }
    }
}
