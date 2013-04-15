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
    public sealed class RunScriptOrRecipe : Base.RSCodeActivity
    {
        [RequiredArgument]
        public InArgument<string> serverID { get; set; }

        [RequiredArgument]
        public InArgument<string> scriptIdOrRecipeName { get; set; }

        public InArgument<bool> ignoreLock { get; set; }

        public InArgument<List<Input>> inputs { get; set; }

        public OutArgument<string> taskID { get; set; }

        protected override void Execute(System.Activities.CodeActivityContext context)
        {
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

    }
}
