using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;

namespace RightScale.netClient.Powershell
{
    //index deployment
    //index server template
    //multi_update deployment
    //multi_update instance
    //multi_update servertemplate

    #region input index deployment cmdlets
    [Cmdlet(VerbsCommon.Get, "RSInputsDeployment")]
    public class inputs_deploymentindex : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string deploymentID;

        [Parameter(Position = 2, Mandatory = false)]
        public string view =  "default";

        protected override void ProcessRecord()
        {
            
            base.ProcessRecord();

            List<Input> rsInputs = RightScale.netClient.Input.index_deployment(deploymentID, view);

            WriteObject(rsInputs);

        }
    }
    #endregion

    #region input index server template cmdlets
    [Cmdlet(VerbsCommon.Get, "RSInputsServerTemplate")]
    public class inputs_servertemplateindex : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string serverTemplateID;

        [Parameter(Position = 2, Mandatory = false)]
        public string view = "default";

        protected override void ProcessRecord()
        {
        
            base.ProcessRecord();

            List<Input> rsInputs = RightScale.netClient.Input.index_servertemplate(serverTemplateID, view);

            WriteObject(rsInputs);

        }
    }
    #endregion

    #region multi update deployment
    [Cmdlet(VerbsCommon.Set, "RSInputsDeployment")]
    public class inputs_setdeployment : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string deploymentID;

        [Parameter(Position = 2, Mandatory = true)]
        public string[] inputs;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            List<Input> lstInputs = new List<Input>();

            foreach (string input in inputs)
            {
                //INPUTNAME:INPUTTYPE:INPUTVALUE
                string[] inpTkns = input.Split(':');
               
                string inputName = inpTkns[0];
                string inputVal = inpTkns[1] + ":" + inpTkns[2];

                lstInputs.Add(new Input(inputName, inputVal));
            }


            bool rsSetDplyInputs = RightScale.netClient.Input.multi_update_deployment(deploymentID, lstInputs);

            WriteObject(rsSetDplyInputs);

        }
    }
    #endregion

    #region multi update instance
    [Cmdlet(VerbsCommon.Set, "RSInputsInstance")]
    public class inputs_setinstance : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string cloudID;

        [Parameter(Position = 2, Mandatory = true)]
        public string instanceID;

        [Parameter(Position = 3, Mandatory = true)]
        public string[] inputs;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            List<Input> lstInputs = new List<Input>();

            foreach (string input in inputs)
            {
                //INPUTNAME:INPUTTYPE:INPUTVALUE
                string[] inpTkns = input.Split(':');

                string inputName = inpTkns[0];
                string inputVal = inpTkns[1] + ":" + inpTkns[2];

                lstInputs.Add(new Input(inputName, inputVal));
            }


            bool rsSetInstInputs = RightScale.netClient.Input.multi_update_instance(cloudID, instanceID, lstInputs);

            WriteObject(rsSetInstInputs);

        }
    }
    #endregion

    #region multi update servertemplate
    [Cmdlet(VerbsCommon.Set, "RSInputsServerTemplate")]
    public class inputs_setservertemplate : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string serverTemplateID;

        [Parameter(Position = 2, Mandatory = true)]
        public string[] inputs;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            List<Input> lstInputs = new List<Input>();

            foreach (string input in inputs)
            {
                //INPUTNAME:INPUTTYPE:INPUTVALUE
                string[] inpTkns = input.Split(':');

                string inputName = inpTkns[0];
                string inputVal = inpTkns[1] + ":" + inpTkns[2];

                lstInputs.Add(new Input(inputName, inputVal));
            }


            bool rsSetInstInputs = RightScale.netClient.Input.multi_update_serverTemplate(serverTemplateID, lstInputs);

            WriteObject(rsSetInstInputs);

        }
    }
    #endregion


    

}