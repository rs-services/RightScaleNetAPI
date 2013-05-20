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
    /// Custom Windows Workflow Foundation CodeActivity to get information about a specific Server's current instance within the RightScale system
    /// </summary>
    public sealed class GetServerCurrentInstanceInfo : Base.RSCodeActivity
    {
        /// <summary>
        /// ID of the Server whose current instance's state will be returned
        /// </summary>
        [RequiredArgument]
        public InArgument<string> serverID { get; set; }

        /// <summary>
        /// Output parameter returns ID of current instance
        /// </summary>
        public OutArgument<string> instanceID { get; set; }

        /// <summary>
        /// Output parameter returns list of private DNS names for the current instance
        /// </summary>
        public OutArgument<List<string>> privateDNSNames { get; set; }

        /// <summary>
        /// Output parameter returns list of private IP addresses for the current instance
        /// </summary>
        public OutArgument<List<string>> privateIPAddresses { get; set; }

        /// <summary>
        /// Output parameter returns list of public DNS names for the current instance
        /// </summary>
        public OutArgument<List<string>> publicDNSNames { get; set; }

        /// <summary>
        /// Output parameter returns list of public IP addresses for the current instance
        /// </summary>
        public OutArgument<List<string>> publicIPAddresses { get; set; }

        /// <summary>
        /// Output parameter returns the current state of the current instance
        /// </summary>
        public OutArgument<string> currentState { get; set; }

        /// <summary>
        /// Output parameter returns the name of the server
        /// </summary>
        public OutArgument<string> instanceName { get; set; }

        /// <summary>
        /// Output parameter returns the OS Platform of the current instance
        /// </summary>
        public OutArgument<string> osPlatform { get; set; }

        protected override bool PerformRightScaleTask(CodeActivityContext context)
        {
            bool retVal = false;
            LogInformation("Starting Process to get instance information");
            if (base.authClient(context))
            {
                Server svr = Server.show(this.serverID.Get(context));
                try
                {
                    Instance currentInstance = svr.currentInstance;
                    this.instanceID.Set(context, string.IsNullOrWhiteSpace(currentInstance.ID) ? string.Empty : currentInstance.ID);
                    this.privateDNSNames.Set(context, currentInstance.private_dns_names == null ? new List<string>() : currentInstance.private_dns_names);
                    this.privateIPAddresses.Set(context, currentInstance.private_ip_addresses == null ? new List<string>() : currentInstance.private_ip_addresses);
                    this.publicDNSNames.Set(context, currentInstance.public_dns_names == null ? new List<string>() : currentInstance.public_dns_names);
                    this.publicIPAddresses.Set(context, currentInstance.public_ip_addresses == null ? new List<string>() : currentInstance.public_ip_addresses);
                    this.currentState.Set(context, string.IsNullOrWhiteSpace(currentInstance.state) ? string.Empty : currentInstance.state);
                    this.instanceName.Set(context, string.IsNullOrWhiteSpace(currentInstance.name) ? string.Empty : currentInstance.name);
                    this.osPlatform.Set(context, string.IsNullOrWhiteSpace(currentInstance.os_platform) ? string.Empty : currentInstance.os_platform);
                    retVal = true;
                }
                catch
                {
                    LogWarning("Server with ID " + serverID.Get(context) + " has no current instance");
                    retVal = true;
                }
            }
            LogInformation("Completed process of getting instance information");
            return retVal;
        }

        /// <summary>
        /// Execute calls to the RightScale API and returns metadata related to the current instance of a given server
        /// </summary>
        /// <param name="context"></param>
        protected override void Execute(System.Activities.CodeActivityContext context)
        {
        }

        /// <summary>
        /// Override to GetFriendlyName sets the name of the objet in the designer
        /// </summary>
        /// <returns>Friently Name of this custom CodeActivity</returns>
        protected override string GetFriendlyName()
        {
            return "RightScale - Get Current Instance Info";
        }
    }
}
