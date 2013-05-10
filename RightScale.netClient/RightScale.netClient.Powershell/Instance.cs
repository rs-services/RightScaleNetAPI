using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;

namespace RightScale.netClient.Powershell
{
    #region Instance index / show cmdlets
    [Cmdlet(VerbsCommon.Get, "RSInstances")]
    public class instance : Cmdlet
    {

        [Parameter(Position = 1, Mandatory = true)]
        public string cloudID;

        [Parameter(Position = 2, Mandatory = false)]
        public string instanceID;

        [Parameter(Position = 3, Mandatory = false)]
        public string filter;

        [Parameter(Position = 4, Mandatory = false)]
        public string view;

        protected override void ProcessRecord()
        {
            List<Filter> lstFilter = new List<Filter>();

            if (filter != null)
            {
                Filter fltFilter = Filter.parseFilter(filter);
                lstFilter.Add(fltFilter);
            }

            if (view == null) { view = "default"; }

            base.ProcessRecord();

            try
            {
                if (instanceID != null)
                {
                    Instance rsInstance = RightScale.netClient.Instance.show(cloudID, instanceID, view);
                    WriteObject(rsInstance);
                }
                else
                {
                    List<Instance> rsInstances = RightScale.netClient.Instance.index(cloudID, lstFilter, view);
                    WriteObject(rsInstances);
                }
            }
            catch (RightScaleAPIException rex)
            {
                WriteObject(rex.Message);
                WriteObject(rex.ErrorData);
                WriteObject(rex.APIHref);
            }

        }
    }

    //Moved to single get instance method
    //[Cmdlet(VerbsCommon.Get, "RSInstance")]
    //public class instance_show : Cmdlet
    //{
    //
    //    [Parameter(Position = 1, Mandatory = true)]
    //    public string cloudID;
    //
    //    [Parameter(Position = 2, Mandatory = true)]
    //    public string instanceID;
    //
    //    [Parameter(Position = 3, Mandatory = false)]
    //    public string view;
    //
    //    protected override void ProcessRecord()
    //    {
    //        if (view == null) { view = "default"; }
    //        base.ProcessRecord();
    //        Instance rsInstance = RightScale.netClient.Instance.show(cloudID,instanceID,view);
    //
    //        WriteObject(rsInstance);
    //
    //    }
    //}
    #endregion

    #region Instance run executable rightscripts chef cmdlets
    [Cmdlet("Run", "RSScript")]
    public class instance_runrsscript : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string cloudID;

        [Parameter(Position = 2, Mandatory = true)]
        public string instanceID;

        [Parameter(Position = 3, Mandatory = true)]
        public string rightScriptID;

        protected override void ProcessRecord()
        {

            Task rsRunScript = RightScale.netClient.Instance.run_rightScript(cloudID, instanceID, rightScriptID);

        }
    }
    #endregion

    #region Instance reboot server
    [Cmdlet("Reboot", "RSServer")]
    public class instance_rebootServer : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string serverID;

        protected override void ProcessRecord()
        {

            Types.returnRebootServer resRebootServer = new Types.returnRebootServer();
            try
            {
                bool rsRebootServer = RightScale.netClient.Instance.reboot_server(serverID);

                resRebootServer.ServerID = serverID;
                resRebootServer.Result = rsRebootServer;
                resRebootServer.Message = "Success";
                resRebootServer.Details = "Server rebooted successfully";
                resRebootServer.APIHref = null;

                WriteObject(resRebootServer);
            }

            catch (RightScaleAPIException rex)
            {

                resRebootServer.ServerID = serverID;
                resRebootServer.Message = "Fail";
                resRebootServer.Details = rex.ErrorData;
                resRebootServer.APIHref = rex.APIHref;
                resRebootServer.Result = false;

                WriteObject(resRebootServer);
            }
        }
    }
    #endregion


    #region Instance reboot Instance
    [Cmdlet("Reboot", "RSInstance")]
    public class instance_rebootInstance : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string cloudID;

        [Parameter(Position = 2, Mandatory = true)]
        public string instanceID;

        protected override void ProcessRecord()
        {

            Types.returnRebootInstance retRebootInstance = new Types.returnRebootInstance();
            try
            {
                bool resRebootInstance = RightScale.netClient.Instance.reboot(cloudID, instanceID);

                retRebootInstance.CloudID = cloudID;
                retRebootInstance.InstanceID = instanceID;
                retRebootInstance.Result = resRebootInstance;
                retRebootInstance.Message = "Success";
                retRebootInstance.Details = "Server rebooted successfully";
                retRebootInstance.APIHref = null;

                WriteObject(retRebootInstance);
            }
            catch (RightScaleAPIException rex)
            {

                retRebootInstance.CloudID = cloudID;
                retRebootInstance.InstanceID = instanceID;
                retRebootInstance.Result = false;
                retRebootInstance.Message = "Fail";
                retRebootInstance.Details = rex.ErrorData;
                retRebootInstance.APIHref = rex.APIHref;

                WriteObject(retRebootInstance);
            }
        }
    }
    #endregion

}