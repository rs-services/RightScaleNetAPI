using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;

namespace RSPosh
{
    #region servers index show cmdlets
    [Cmdlet(VerbsCommon.Get, "RSServers")]
    public class server_index : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = false)]
        public List<RightScale.netClient.Filter> filter;

        [Parameter(Position = 2, Mandatory = false)]
        public string view;
        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            List<Server> rsServers = RightScale.netClient.Server.index(filter, view);

            WriteObject(rsServers);

        }
    }

    [Cmdlet(VerbsCommon.Get, "RSServer")]
    public class server_show : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string serverID;

        [Parameter(Position = 2, Mandatory = false)]
        public string view;

        protected override void ProcessRecord()
        {
            if (view == null) { view = "default"; }
            base.ProcessRecord();
            Server rsServer = RightScale.netClient.Server.show(serverID, view);

            WriteObject(rsServer);

        }
    }
    #endregion
    #region server create / delete cmdlets
    [Cmdlet(VerbsCommon.New, "RSServer")]
    public class server_create : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string serverName;

        [Parameter(Position = 2, Mandatory = true)]
        public string cloudID;

        [Parameter(Position = 3, Mandatory = true)]
        public string deploymentID;

        [Parameter(Position = 4, Mandatory = true)]
        public string serverTemplateID;

        protected override void ProcessRecord()
        {
            Types.returnServer result = new Types.returnServer();

            base.ProcessRecord();
            string rsNewServerID = RightScale.netClient.Server.create(cloudID,deploymentID,serverTemplateID,serverName);

            if (rsNewServerID != "")
            {
                result.ServerID = rsNewServerID;
                result.Message = "Server created";
                result.Result = true;
                result.DeploymentID = deploymentID;                
                result.ServerTemplateID = serverTemplateID;
                
                WriteObject(result);
            }
            else
            {
                result.ServerID = rsNewServerID;
                result.Message = "Error creating server";
                result.Result = false;
                result.DeploymentID = deploymentID;                
                result.ServerTemplateID = serverTemplateID;                
            
                WriteObject(result);
            }
        }
    }

    [Cmdlet(VerbsCommon.Remove, "RSServer")]
    public class server_destroy : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string serverID;
       
        protected override void ProcessRecord()
        {
            Types.returnServer result = new Types.returnServer();

            base.ProcessRecord();
            bool rsDestroyServer = RightScale.netClient.Server.destroy(serverID);

            if (rsDestroyServer == true)
            {
                result.ServerID = serverID;
                result.Message = "Server destroyed";
                result.Result = true;               
                
                WriteObject(result);
            }
            else
            {
                result.ServerID = serverID;
                result.Message = "Error destroying server";
                result.Result = false;               
                
                WriteObject(result);               

            }
        }
    }
    #endregion
    #region server launch cmdlets
    [Cmdlet("Launch", "RSServer")]
    public class server_launch : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string serverID;

        protected override void ProcessRecord()
        {
            Types.returnServerLaunch result = new Types.returnServerLaunch();

            base.ProcessRecord();

            try
            {
                bool rsLaunchServer = RightScale.netClient.Server.launch(serverID);

                if (rsLaunchServer == true)
                {
                    result.ServerID = serverID;
                    result.Message = "Server Launched";
                    result.Result = true;

                    WriteObject(result);
                }
                else
                {
                    result.ServerID = serverID;
                    result.Message = "Error launching server";
                    result.Result = false;

                    WriteObject(result);

                }
            }
            catch (RightScaleAPIException errLaunch)
            {
                WriteObject(errLaunch);
                WriteObject(errLaunch.InnerException);
            }

        }
    }
    #endregion

}