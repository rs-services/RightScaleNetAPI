﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using RightScale.netClient.Core;
using RightScale.netClient;

namespace RightScale.netClient.ActivityLibrary
{
    public sealed class CreateServer : Base.ServerBasedCreateActivity
    {
        [RequiredArgument]
        public InArgument<Int32> numberOfServers { get; set; }

        public OutArgument<List<string>> serverIDs { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            LogInformation("Creating " + this.numberOfServers.Get(context).ToString() + " servers based on ServerTemplateID: " + this.serverTemplateID.Get(context));

            string retVal = string.Empty;

            List<string> serverIDlist = new System.Collections.Generic.List<string>();

            if (this.numberOfServers.Get(context) < 0)
            {
                throw new ArgumentOutOfRangeException("numberOfServers must be greater than 0 to provision servers through this process.  Please check your inputs and try again.");
            }
            
            if (base.authClient(context))
            {
                if (this.numberOfServers.Get(context) > 1)
                {
                    LogInformation("Creating multiple servers for ServerTemplateID: " + this.serverTemplateID.Get(context));

                    for (int i = 0; i < this.numberOfServers.Get(context); i++)
                    {
                        string serverNum = (i + 1).ToString();
                        string ofServers = this.numberOfServers.Get(context).ToString();

                        LogInformation("Creating Server " + serverNum + " of " + ofServers);

                        string desc = this.description.Get(context);
                        
                        if (string.IsNullOrWhiteSpace(desc))
                        {
                            desc = getDefaultDescription();
                        }
                        
                        string srvName = this.name.Get(context) + " [" + serverNum + " of " + ofServers + "]";
                        string srvID = Server.create(this.cloudID.Get(context), this.deploymentID.Get(context), this.serverTemplateID.Get(context), srvName, desc, this.cloudID.Get(context), this.description.Get(context), this.imageID.Get(context), this.inputs.Get(context), this.instanceTypeID.Get(context), this.kernelImageID.Get(context), this.multiCloudImageID.Get(context), this.ramdiskImageID.Get(context), this.securityGroupIDs.Get(context), this.sshKeyID.Get(context), this.userData.Get(context), this.optimized.Get(context));
                        
                        LogInformation("Server created with ID of: " + srvID);

                        serverIDlist.Add(srvID);
                    }

                    LogInformation("Completed creating multiple servers for ServerTemplateID: " + this.serverTemplateID.Get(context));
                }
                else
                {
                    LogInformation("Creating single server for ServerTemplateID: " + this.serverTemplateID.Get(context));

                    string desc = this.description.Get(context);
                    
                    if (string.IsNullOrWhiteSpace(desc))
                    {
                        desc = getDefaultDescription();
                    }
                    
                    string srvID = Server.create(this.cloudID.Get(context), this.deploymentID.Get(context), this.serverTemplateID.Get(context), this.name.Get(context), desc, this.cloudID.Get(context), this.description.Get(context), this.imageID.Get(context), this.inputs.Get(context), this.instanceTypeID.Get(context), this.kernelImageID.Get(context), this.multiCloudImageID.Get(context), this.ramdiskImageID.Get(context), this.securityGroupIDs.Get(context), this.sshKeyID.Get(context), this.userData.Get(context), this.optimized.Get(context));
                    
                    LogInformation("Server created with ID of: " + srvID);

                    serverIDlist.Add(srvID);
                }
            }
            this.serverIDs.Set(context, serverIDlist);

            LogInformation("Completed creating  " + this.numberOfServers.Get(context).ToString() + " servers based on ServerTemplateID: " + this.serverTemplateID.Get(context));
        }

        private string getDefaultDescription()
        {
            return "Server created by Windows 3 Tier Workflow project based on RightScale.netClient library at " + DateTime.Now.ToString();
        }
    }
}