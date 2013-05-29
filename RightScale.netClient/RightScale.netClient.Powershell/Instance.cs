using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;

namespace RightScale.netClient.Powershell
{

    /*    index
    show - done
    update
    launch
    multi_run_executable
    multi_terminate
    reboot - done
    run_executable
    set_custom_lodgement
    start - done
    stop - done
    terminate*/

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

    #region Instance start Instance
    [Cmdlet("Start", "RSInstance")]
    public class instance_startInstance : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string cloudID;

        [Parameter(Position = 2, Mandatory = true)]
        public string instanceID;

        protected override void ProcessRecord()
        {

            Types.returnInstanceAction retInstanceActionStart = new Types.returnInstanceAction();
            try
            {
                bool resInstanceActionStart = RightScale.netClient.Instance.start(cloudID, instanceID);

                retInstanceActionStart.ActionType = "start";
                retInstanceActionStart.CloudID = cloudID;
                retInstanceActionStart.InstanceID = instanceID;
                retInstanceActionStart.Result = resInstanceActionStart;
                retInstanceActionStart.Message = "Success";
                retInstanceActionStart.Details = "Server started successfully";
                retInstanceActionStart.APIHref = null;

                WriteObject(retInstanceActionStart);
            }
            catch (RightScaleAPIException rex)
            {

                retInstanceActionStart.ActionType = "start";
                retInstanceActionStart.CloudID = cloudID;
                retInstanceActionStart.InstanceID = instanceID;
                retInstanceActionStart.Result = false;
                retInstanceActionStart.Message = "Fail";
                retInstanceActionStart.Details = rex.ErrorData;
                retInstanceActionStart.APIHref = rex.APIHref;

                WriteObject(retInstanceActionStart);
            }
        }
    }
    #endregion




    #region Instance stop Instance
    [Cmdlet("Stop", "RSInstance")]
    public class instance_stopInstance : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string cloudID;

        [Parameter(Position = 2, Mandatory = true)]
        public string instanceID;

        protected override void ProcessRecord()
        {

            Types.returnInstanceAction retInstanceActionStop = new Types.returnInstanceAction();
            try
            {
                bool resInstanceActionStop = RightScale.netClient.Instance.stop(cloudID, instanceID);

                retInstanceActionStop.ActionType = "stop";
                retInstanceActionStop.CloudID = cloudID;
                retInstanceActionStop.InstanceID = instanceID;
                retInstanceActionStop.Result = resInstanceActionStop;
                retInstanceActionStop.Message = "Success";
                retInstanceActionStop.Details = "Server stopped successfully";
                retInstanceActionStop.APIHref = null;

                WriteObject(retInstanceActionStop);
            }
            catch (RightScaleAPIException rex)
            {

                retInstanceActionStop.ActionType = "stop";
                retInstanceActionStop.CloudID = cloudID;
                retInstanceActionStop.InstanceID = instanceID;
                retInstanceActionStop.Result = false;
                retInstanceActionStop.Message = "Fail";
                retInstanceActionStop.Details = rex.ErrorData;
                retInstanceActionStop.APIHref = rex.APIHref;

                WriteObject(retInstanceActionStop);
            }
        }
    }
    #endregion


    #region Instance Terminate Instance
    [Cmdlet("terminate", "RSInstance")]
    public class instance_terminateInstance : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string cloudID;

        [Parameter(Position = 2, Mandatory = true)]
        public string instanceID;

        protected override void ProcessRecord()
        {

            Types.returnInstanceAction retInstanceActionStop = new Types.returnInstanceAction();
            try
            {
                bool resInstanceActionStop = RightScale.netClient.Instance.terminate(cloudID, instanceID);

                retInstanceActionStop.ActionType = "terminate";
                retInstanceActionStop.CloudID = cloudID;
                retInstanceActionStop.InstanceID = instanceID;
                retInstanceActionStop.Result = resInstanceActionStop;
                retInstanceActionStop.Message = "Success";
                retInstanceActionStop.Details = "Server terminated successfully";
                retInstanceActionStop.APIHref = null;

                WriteObject(retInstanceActionStop);
            }
            catch (RightScaleAPIException rex)
            {

                retInstanceActionStop.ActionType = "terminate";
                retInstanceActionStop.CloudID = cloudID;
                retInstanceActionStop.InstanceID = instanceID;
                retInstanceActionStop.Result = false;
                retInstanceActionStop.Message = "Fail";
                retInstanceActionStop.Details = rex.ErrorData;
                retInstanceActionStop.APIHref = rex.APIHref;

                WriteObject(retInstanceActionStop);
            }
        }
    }
    #endregion




    #region Instance update Instance
    [Cmdlet("update", "RSInstance")]
    public class instance_updateInstance : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string cloudID;

        [Parameter(Position = 2, Mandatory = true)]
        public string instanceID;

        [Parameter(Position = 3, Mandatory = false)]
        public string name;

        [Parameter(Position = 4, Mandatory = false)]
        public string instanceTypeID;

        [Parameter(Position = 5, Mandatory = false)]
        public string serverTemplateID;

        [Parameter(Position = 6, Mandatory = false)]
        public string multiCloudImageID;

        [Parameter(Position = 7, Mandatory = false)]
        public string[] securityGroupIDs;

        [Parameter(Position = 8, Mandatory = false)]
        public string dataCenterID;

        [Parameter(Position = 9, Mandatory = false)]
        public string imageID;

        [Parameter(Position = 10, Mandatory = false)]
        public string kernelImageID;

        [Parameter(Position = 11, Mandatory = false)]
        public string ramdiskImageID;

        [Parameter(Position = 12, Mandatory = false)]
        public string sshKeyID;

        [Parameter(Position = 13, Mandatory = false)]
        public string userData;

        [Parameter(Position = 14, Mandatory = false)]
        public string[] subnetIDs;

        protected override void ProcessRecord()
        {

            Types.returnInstanceAction retInstanceActionUpdate = new Types.returnInstanceAction();
            // parsing securitygroup id's and turning it into list from array
            List<string> lstSecurityGroups = new List<string>();
            if (securityGroupIDs != null)
            {
                foreach (string securityGroupID in securityGroupIDs)
                {
                    lstSecurityGroups.Add(securityGroupID);
                }
            }


            // parsing subnetIDs and turning it into a list from array
            List<string> lstSubnets = new List<string>();
            if (subnetIDs != null)
            {
                foreach (string subnetID in subnetIDs)
                {
                    lstSubnets.Add(subnetID);
                }
            }


            try
            {
                bool resInstanceAction = RightScale.netClient.Instance.update(cloudID, instanceID, name, instanceTypeID, serverTemplateID, multiCloudImageID, lstSecurityGroups, dataCenterID, imageID, kernelImageID, ramdiskImageID, sshKeyID, userData, lstSubnets);

                retInstanceActionUpdate.ActionType = "update";
                retInstanceActionUpdate.CloudID = cloudID;
                retInstanceActionUpdate.InstanceID = instanceID;
                retInstanceActionUpdate.Result = resInstanceAction;
                retInstanceActionUpdate.Message = "Success";
                retInstanceActionUpdate.Details = "Instance updated successfully";
                retInstanceActionUpdate.APIHref = null;

                WriteObject(retInstanceActionUpdate);
            }
            catch (RightScaleAPIException rex)
            {

                retInstanceActionUpdate.ActionType = "update";
                retInstanceActionUpdate.CloudID = cloudID;
                retInstanceActionUpdate.InstanceID = instanceID;
                retInstanceActionUpdate.Result = false;
                retInstanceActionUpdate.Message = "Fail";
                retInstanceActionUpdate.Details = rex.ErrorData;
                retInstanceActionUpdate.APIHref = rex.APIHref;

                WriteObject(retInstanceActionUpdate);
            }
        }
    }
    #endregion





    #region Instance launch Instance
    [Cmdlet("launch", "RSInstance")]
    public class instance_launchInstance : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string cloudID;

        [Parameter(Position = 2, Mandatory = true)]
        public string instanceID;

        [Parameter(Position = 3, Mandatory = false)]
        //gets parsed and set to a list of inputs
        public List<Filter> filters;


        protected override void ProcessRecord()
        {

            Types.returnInstanceAction retInstanceActionLaunch = new Types.returnInstanceAction();

            /*List<Input> strInputs = new List<Input>();
            if (strInputs != null)
            {
                foreach (Input input in strInputs)
                {
                    
                    string inputName = input.GetHashCode(':')[0];
                    string inputValue = input.Split(':')[1];

                    Input varInput = new Input(inputName, inputValue);
                    strInputs.Add(varInput);
                }
            }*/


<<<<<<< HEAD
            try
            {
                string resInstanceActionlaunch = RightScale.netClient.Instance.launch(cloudID, instanceID);

                retInstanceActionLaunch.ActionType = "launch";
                retInstanceActionLaunch.CloudID = cloudID;
                retInstanceActionLaunch.InstanceID = instanceID;
                retInstanceActionLaunch.filter = filters;
                retInstanceActionLaunch.Result = true;
                retInstanceActionLaunch.Message = "Success";
                retInstanceActionLaunch.Details = "Instance launched successfully";
                retInstanceActionLaunch.APIHref = null;

                WriteObject(retInstanceActionLaunch);
            }
            catch (RightScaleAPIException rex)
            {
=======
            /* try
             {
                 string resInstanceActionlaunch = RightScale.netClient.Instance.launch(cloudID, instanceID, filters);

                 retInstanceActionLaunch.ActionType = "launch";
                 retInstanceActionLaunch.CloudID = cloudID;
                 retInstanceActionLaunch.InstanceID = instanceID;
                 retInstanceActionLaunch.filter = filters;
                 retInstanceActionLaunch.Result = true;
                 retInstanceActionLaunch.Message = "Success";
                 retInstanceActionLaunch.Details = "Instance launched successfully";
                 retInstanceActionLaunch.APIHref = null;
>>>>>>> 3b201824de02a7a802aefb1ff4f57a61b48cc067

                 WriteObject(retInstanceActionLaunch);
             }
             catch (RightScaleAPIException rex)
             {

                 retInstanceActionLaunch.ActionType = "launch";
                 retInstanceActionLaunch.CloudID = cloudID;
                 retInstanceActionLaunch.InstanceID = instanceID;
                 retInstanceActionLaunch.filter = filters;
                 retInstanceActionLaunch.Result = false;
                 retInstanceActionLaunch.Message = "Fail";
                 retInstanceActionLaunch.Details = rex.ErrorData;
                 retInstanceActionLaunch.APIHref = rex.APIHref;

                 WriteObject(retInstanceActionLaunch);
             }
         }
     }
     #endregion



     #region Instance terminate multi_instance
     [Cmdlet("terminate", "multi-Instance")]
     public class instance_multiInstance : Cmdlet
     {
         [Parameter(Position = 1, Mandatory = true)]
         public string cloudID;

         [Parameter(Position = 2, Mandatory = false)]
         //gets parsed and set to a list of inputs
         public List<Filter> filters;

         [Parameter(Position = 3, Mandatory = true)]
         //gets parsed and set to a list of inputs
         public bool terminateAll;


         protected override void ProcessRecord()
         {

             Types.returnInstanceAction retInstanceActionMulti_Terminate = new Types.returnInstanceAction();

             /*
              Commenting out parsing loop to test using List<Filter>
              * List<Filter> strInputs = new List<Filter>();
             if (strInputs != null)
             {
                 foreach (Filter input in strInputs)
                 {

                     string inputName = input.split(':')[0];
                     string inputValue = input.Split(':')[1];

                     Input varInput = new Input(inputName, inputValue);
                     strInputs.Add(varInput);
                 }
             }*/


            /* try
             {
                 List<Task> resInstanceActionlaunch = RightScale.netClient.Instance.multi_terminate(cloudID, terminateAll, filters);

                 retInstanceActionMulti_Terminate.ActionType = "launch";
                 retInstanceActionMulti_Terminate.CloudID = cloudID;
                 retInstanceActionMulti_Terminate.filter = filters;
                 retInstanceActionMulti_Terminate.Result = true;
                 retInstanceActionMulti_Terminate.Message = "Success";
                 retInstanceActionMulti_Terminate.Details = "Instance launched successfully";
                 retInstanceActionMulti_Terminate.APIHref = null;

                 WriteObject(retInstanceActionMulti_Terminate);
             }
             catch (RightScaleAPIException rex)
             {

                 retInstanceActionMulti_Terminate.ActionType = "launch";
                 retInstanceActionMulti_Terminate.CloudID = cloudID;
                 retInstanceActionMulti_Terminate.filter = filters;
                 retInstanceActionMulti_Terminate.Result = false;
                 retInstanceActionMulti_Terminate.Message = "Fail";
                 retInstanceActionMulti_Terminate.Details = rex.ErrorData;
                 retInstanceActionMulti_Terminate.APIHref = rex.APIHref;

                 WriteObject(retInstanceActionMulti_Terminate);
             }
         }
     }
     */
     #endregion
     




        }
    }
}

