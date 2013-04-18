using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;

namespace RightScale.netClient.Powershell
{
    #region input index cmdlets
    [Cmdlet(VerbsCommon.Get, "RSInputsDeployment")]
    public class inputs_deploymentindex : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string deploymentID;

        [Parameter(Position = 2, Mandatory = false)]
        public string view;

        protected override void ProcessRecord()
        {
            if (view == null) { view = "default"; }
            base.ProcessRecord();

            List<Input> rsInputs = RightScale.netClient.Input.index_deployment(deploymentID, view);

            WriteObject(rsInputs);

        }
    }


    #endregion

}