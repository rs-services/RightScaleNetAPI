using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Activities;
using RightScale.netClient.Core;
using RightScale.netClient;

namespace RightScale.netClient.ActivityLibrary
{
    /// <summary>
    /// Custom Windows Workflow Foundation CodeActivity to run a specific RightScript or Recipe on a given Server's current instance within the RightScale system
    /// </summary>
    public sealed class RunScriptOrRecipe : Base.RSCodeActivity
    {
        /// <summary>
        /// ID of the Server to run the given Recipe or RightScript
        /// </summary>
        [RequiredArgument]
        public InArgument<string> serverID { get; set; }

        /// <summary>
        /// Name of recipe or ID of RightScript to be run on the specified Server
        /// </summary>
        [RequiredArgument]
        public InArgument<string> scriptIdOrRecipeName { get; set; }

        /// <summary>
        /// Boolean indiciating if the script/recipe run should ignore any locks 
        /// </summary>
        public InArgument<bool> ignoreLock { get; set; }

        /// <summary>
        /// Collection of inputs for this run of the given recipe or script
        /// </summary>
        public InArgument<List<Input>> inputs { get; set; }

        /// <summary>
        /// ID of the Task object tracking the progress of the specific script run
        /// </summary>
        public OutArgument<string> taskID { get; set; }

        protected override bool PerformRightScaleTask(CodeActivityContext context)
        {
            bool retVal = false;
            LogInformation("Beginning RunScriptOrRecipe Process for " + scriptIdOrRecipeName.Get(context));

            if (base.authClient(context))
            {
                string recipeName = string.Empty;
                string rightScriptID = string.Empty;

                if (IsDigitsOnly(this.scriptIdOrRecipeName.Get(context)))
                {
                    rightScriptID = this.scriptIdOrRecipeName.Get(context);
                    LogInformation("Process will run RightScript as " + rightScriptID + " is numeric");
                }
                else
                {
                    recipeName = this.scriptIdOrRecipeName.Get(context);
                    LogInformation("Process will run Recipe as " + recipeName + " is not numeric");
                }

                Server currentServer = Server.show(serverID.Get(context));
                LogInformation("Starting call to execute " + this.scriptIdOrRecipeName.Get(context));
                Task executableRun = Instance.run_executable(currentServer.currentInstance.cloud.ID, currentServer.currentInstance.ID, recipeName, rightScriptID, inputs.Get(context), ignoreLock.Get(context));
                this.taskID.Set(context, executableRun.ID);
                LogInformation("Completed call to execute " + this.scriptIdOrRecipeName.Get(context));
            }

            return retVal;
        }


        static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Override to GetFriendlyName sets the name of the objet in the designer
        /// </summary>
        /// <returns>Friently Name of this custom CodeActivity</returns>
        protected override string GetFriendlyName()
        {
            return "RightScale - Run Script or Recipe";
        }
    }
}
