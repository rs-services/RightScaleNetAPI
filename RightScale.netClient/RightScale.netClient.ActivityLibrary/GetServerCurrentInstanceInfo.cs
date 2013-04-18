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
    public sealed class GetServerCurrentInstanceInfo : Base.RSCodeActivity
    {
        [RequiredArgument]
        public InArgument<string> serverID { get; set; }

        public OutArgument<string> instanceID { get; set; }

        public OutArgument<List<string>> privateDNSNames { get; set; }

        public OutArgument<List<string>> privateIPAddresses { get; set; }

        public OutArgument<List<string>> publicDNSNames { get; set; }

        public OutArgument<List<string>> publicIPAddresses { get; set; }

        public OutArgument<string> currentState { get; set; }

        public OutArgument<string> instanceName { get; set; }

        public OutArgument<string> osPlatform { get; set; }

        protected override void Execute(System.Activities.CodeActivityContext context)
        {
            LogInformation("Starting Process to get instance information");
            if (base.authClient(context))
            {
                Server svr = Server.show(this.serverID.Get(context));
                Instance currentInstance = svr.currentInstance;
                this.instanceID.Set(context, currentInstance.ID);
                this.privateDNSNames.Set(context, currentInstance.private_dns_names);
                this.privateIPAddresses.Set(context, currentInstance.private_ip_addresses);
                this.publicDNSNames.Set(context, currentInstance.public_dns_names);
                this.publicIPAddresses.Set(context, currentInstance.public_ip_addresses);
                this.currentState.Set(context, currentInstance.state);
                this.instanceName.Set(context, currentInstance.name);
                this.osPlatform.Set(context, currentInstance.os_platform);
            }
            LogInformation("Completed process of getting instance information");
        }
    }
}
