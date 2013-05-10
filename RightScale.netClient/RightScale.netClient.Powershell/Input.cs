using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;

namespace RightScale.netClient.Powershell
{
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

    

}