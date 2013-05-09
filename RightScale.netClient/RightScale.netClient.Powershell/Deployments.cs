using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;

namespace RightScale.netClient.Powershell
{
    #region deployments index show
    [Cmdlet(VerbsCommon.Get, "RSDeployments")]
    public class deployments_index_show : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = false)]
        public string deploymentID;

        [Parameter(Position = 2, Mandatory = false)]
        public string filter;

        [Parameter(Position = 3, Mandatory = false)]
        public string view;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                if (deploymentID != null)
                {
                    Deployment rsDeployment = RightScale.netClient.Deployment.show(deploymentID);
                    WriteObject(rsDeployment);
                }
                else
                {
                    List<Filter> lstFilter = new List<Filter>();

                    if (filter != null)
                    {
                        Filter fltFilter = Filter.parseFilter(filter);
                        lstFilter.Add(fltFilter);
                    }

                    List<Deployment> rsDeployments = RightScale.netClient.Deployment.index(lstFilter, view);

                    WriteObject(rsDeployments);
                }
            }
            catch(RightScaleAPIException rex)
            {
                WriteObject(rex);
                WriteObject(rex.ErrorData);
            }
        }
    }

    //Consolidate to single index show method
    //[Cmdlet(VerbsCommon.Get, "RSDeployment")]
    //public class deployments_show : Cmdlet
    //{
    //    4-30-2013
    //
    //    [Parameter(Position = 1, Mandatory = true )]
    //    public string deploymentID;
    //
    //    protected override void ProcessRecord()
    //    {
    //        base.ProcessRecord();
    //        Deployment rsDeployment = RightScale.netClient.Deployment.show(deploymentID);
    //
    //        WriteObject(rsDeployment);
    //
    //    }
    //}
    #endregion

    #region deployments create cmdlets
    [Cmdlet(VerbsCommon.New, "RSDeployment")]
    public class deployments_create : Cmdlet
    {

        [Parameter(Position = 1, Mandatory = true)]
        public string name;

        protected override void ProcessRecord()
        {
            Types.returnDeployment result = new Types.returnDeployment();

            base.ProcessRecord();
            string rsNewDeploymentID = RightScale.netClient.Deployment.create(name);

            if (rsNewDeploymentID != "")
            {
                result.Message = "Deployment created";
                result.Result = true;
                result.DeploymentID = rsNewDeploymentID;
                
                WriteObject(result);
            }
            else
            {
                result.Message = "Error creating deployment";
                result.Result = false;
                result.DeploymentID = rsNewDeploymentID;
                
                WriteObject(result);
            }
        }
    }
    #endregion

    #region deployments destroy
    [Cmdlet("Destroy", "RSDeployment")]
    public class deployments_destroy : Cmdlet
    {

        [Parameter(Position = 1, Mandatory = true)]
        public string deploymentID;
        
        protected override void ProcessRecord()
        {

            Types.returnDeployment retResult = new Types.returnDeployment();
            retResult.DeploymentID = deploymentID;

            base.ProcessRecord();

            try
            {
                bool rsDestroyDeployment = RightScale.netClient.Deployment.destroy(deploymentID);

                if (rsDestroyDeployment == true)
                {
                    retResult.Message = "Deployment deleted";
                    retResult.Result = rsDestroyDeployment;

                    WriteObject(retResult);
                }
                else
                {
                    retResult.Message = "Error destroying Deployment";
                    retResult.Result = rsDestroyDeployment;

                    WriteObject(retResult);
                }
            }
            catch (RightScaleAPIException rex)
            {

                retResult.DeploymentID = deploymentID;
                retResult.Message = "Error destroying Deployment";
                retResult.Details = rex.ErrorData;
                retResult.Result = false;
                retResult.APIHref = rex.APIHref;

                WriteObject(retResult);                

            }
            catch (System.Exception exc)
            {
                retResult.DeploymentID = deploymentID;
                retResult.Message = "Error destroying Deployment";
                retResult.Details = exc.Message;
                retResult.Result = false;
                retResult.APIHref = null;

                WriteObject(retResult);
            }    
        }
    }

    #endregion

    #region deployments clone
    [Cmdlet("Clone", "RSDeployment")]
    public class deployments_clone : Cmdlet
    {

        [Parameter(Position = 1, Mandatory = true)]
        public string deploymentID;

        protected override void ProcessRecord()
        {

            Types.returnDeploymentClone retResult = new Types.returnDeploymentClone();

            retResult.DeploymentID = deploymentID;

            base.ProcessRecord();

            try
            {
                string rsCloneDeployment = RightScale.netClient.Deployment.clone(deploymentID);

                retResult.DeploymentID = deploymentID;
                retResult.CloneID = rsCloneDeployment;
                retResult.Message = "Success";
                retResult.Details = "Deployment Cloned";
                retResult.Result = true;

                WriteObject(retResult);
            }
            catch (RightScaleAPIException rex)
            {
                retResult.DeploymentID = deploymentID;
                retResult.CloneID = null;
                retResult.Message = "Fail";
                retResult.Details = rex.ErrorData;
                retResult.APIHref = rex.APIHref;
                retResult.Result = false;

                WriteObject(retResult);
            }
            catch (System.Exception excp)
            {
                retResult.DeploymentID = deploymentID;
                retResult.CloneID = null;
                retResult.Message = "Fail";
                retResult.Details = "Exception cloning Deployment - " + excp.Message;
                retResult.APIHref = null;
                retResult.Result = false;

                WriteObject(retResult);      
            }
        }
    }

    #endregion

    #region deployments update
    [Cmdlet("Update", "RSDeployment")]
    public class deployments_update : Cmdlet
    {

        [Parameter(Position = 1, Mandatory = true, ValueFromPipeline=true)]
        public string deploymentID;

        [Parameter(Position = 2, Mandatory = false)]
        public string name;

        [Parameter(Position = 3, Mandatory = false)]
        public string description;

        [Parameter(Position = 4, Mandatory = false)]
        public string servertagscope;

        protected override void ProcessRecord()
        {

            Types.returnDeploymentUpdate retResult = new Types.returnDeploymentUpdate();

            retResult.DeploymentID = deploymentID;

            base.ProcessRecord();

            try
            {
                bool rsUpdateDeployment = RightScale.netClient.Deployment.update(deploymentID,name,description,servertagscope);

                retResult.DeploymentID = deploymentID;
                retResult.Message = "Success";
                retResult.Details = "Deployment Updated";
                retResult.Result = true;

                WriteObject(retResult);
            }
            catch (RightScaleAPIException rex)
            {
                retResult.DeploymentID = deploymentID;
                retResult.Message = "Fail";
                retResult.Details = rex.ErrorData;
                retResult.APIHref = rex.APIHref;
                retResult.Result = false;

                WriteObject(retResult);
            }
            catch (System.Exception excp)
            {
                retResult.DeploymentID = deploymentID;
                retResult.Message = "Fail";
                retResult.Details = "Exception updating Deployment - " + excp.Message;
                retResult.APIHref = null;
                retResult.Result = false;

                WriteObject(retResult);
            }
        }
    }

    #endregion

    #region deployments servers
    [Cmdlet(VerbsCommon.Get, "RSDeploymentServers")]
    public class deployments_servers : Cmdlet
    {

        [Parameter(Position = 1, Mandatory = true, ValueFromPipeline = true)]
        public string deploymentID;       

        protected override void ProcessRecord()
        {

            base.ProcessRecord();

            try
            {
                List<Server> rsDeploymentServers = RightScale.netClient.Deployment.getServers(deploymentID);

                WriteObject(rsDeploymentServers);
            }
            catch (RightScaleAPIException rex)
            {
                Types.returnDeploymentServers retResult = new Types.returnDeploymentServers();

                retResult.DeploymentID = deploymentID;
                retResult.Message = "Fail";
                retResult.Details = rex.ErrorData;
                retResult.APIHref = rex.APIHref;
                retResult.Result = false;

                WriteObject(retResult);
            }
            catch (System.Exception excp)
            {
                Types.returnDeploymentServers retResult = new Types.returnDeploymentServers();

                retResult.DeploymentID = deploymentID;
                retResult.Message = "Fail";
                retResult.Details = "Exception getting Deployment Servers - " + excp.Message;
                retResult.APIHref = null;
                retResult.Result = false;

                WriteObject(retResult);
            }
        }
    }

    #endregion

}