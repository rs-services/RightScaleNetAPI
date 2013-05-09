using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;

namespace RightScale.netClient.Powershell
{

    //index x
    //show x
    //create x
    //update x
    //clone x
    //commit x
    //destroy x
    //publish

    #region servertemplates index show cmdlets
    [Cmdlet(VerbsCommon.Get, "RSServerTemplates")]
    public class servertemplate_index_show : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = false)]
        public string servertemplateID;

        [Parameter(Position = 1, Mandatory = false)]
        public string filter;

        [Parameter(Position = 2, Mandatory = false)]
        public string view;

        protected override void ProcessRecord()
        {
            List<Filter> lstFilter = new List<Filter>();

            if (filter != null)
            {
                Filter fltFilter = Filter.parseFilter(filter);
                lstFilter.Add(fltFilter);
            }

            base.ProcessRecord();

            try
            {
                if (servertemplateID != null)
                {
                    ServerTemplate rsServerTemplate = RightScale.netClient.ServerTemplate.show(servertemplateID, view);
                    WriteObject(rsServerTemplate);
                }
                else
                {
                    List<ServerTemplate> rsServerTemplates = RightScale.netClient.ServerTemplate.index(lstFilter, view);
                    WriteObject(rsServerTemplates);
                }
            }
            catch (RightScaleAPIException rex)
            {
                WriteObject(rex.Message);
                WriteObject(rex.ErrorData);
            }

        }

        //[Cmdlet(VerbsCommon.Get, "RSServerTemplate")]
        //public class servertemplate_show : Cmdlet
        //{
        //
        //    [Parameter(Position = 1, Mandatory = true)]
        //    public string servertemplateID;
        //
        //    [Parameter(Position = 2, Mandatory = false)]
        //    public string view;
        //
        //    protected override void ProcessRecord()
        //    {
        //        base.ProcessRecord();
        //        ServerTemplate rsServerTemplate = RightScale.netClient.ServerTemplate.show(servertemplateID,view);
        //
        //        WriteObject(rsServerTemplate);
        //
        //    }
        //}
    }
    #endregion

    #region servertemplates create clone
    [Cmdlet(VerbsCommon.New, "RSServerTemplate")]
        public class servertemplate_new: Cmdlet
        {

            [Parameter(Position = 1, Mandatory = true)]
            public string name;

            [Parameter(Position = 2, Mandatory = false)]
            public string description;

            protected override void ProcessRecord()
            {

                Types.returnServerTemplateCreate result = new Types.returnServerTemplateCreate();

                base.ProcessRecord();

                try
                {
                    string rsServerTemplate = RightScale.netClient.ServerTemplate.create(name, description);

                    if (rsServerTemplate != "")
                    {
                        result.ServerTemplateID = rsServerTemplate;
                        result.Message = "ServerTemplate Created";
                        result.Result = true;                        
                    }
                    else
                    {
                        result.ServerTemplateID = rsServerTemplate;
                        result.Message = "Error creating ServerTemplate";
                        result.Result = false;                        
                    }
                }
                catch (RightScaleAPIException errLaunch)
                {
                    result.Result = false;
                    result.Message = errLaunch.InnerException.ToString() + "-" + errLaunch;
                    result.MessageData = errLaunch.ErrorData;
                }

                WriteObject(result);
            }
    }

    [Cmdlet("Clone", "RSServerTemplate")]
    public class servertemplate_clone : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string servertemplateID;

        [Parameter(Position = 2, Mandatory = true)]
        public string name;

        [Parameter(Position = 3, Mandatory = false)]
        public string description;

        protected override void ProcessRecord()
        {

            Types.returnServerTemplateClone result = new Types.returnServerTemplateClone();

            base.ProcessRecord();

            try
            {
                string rsServerTemplateID = RightScale.netClient.ServerTemplate.clone(servertemplateID, name, description);

                if (rsServerTemplateID != "")
                {
                    result.ServerTemplateID = rsServerTemplateID;
                    result.ServerTemplateName = name;
                    result.Description = description;
                    result.Message = "ServerTemplate cloned";
                    result.Result = true;
                }
                else
                {
                    result.ServerTemplateID = rsServerTemplateID;
                    result.ServerTemplateName = name;
                    result.Description = description;
                    result.Message = "Error cloning ServerTemplate";
                    result.Result = false;
                }
            }
            catch (RightScaleAPIException errLaunch)
            {
                result.ServerTemplateName = name;
                result.Description = description;
                result.Result = false;
                result.Message = errLaunch.InnerException.ToString() + "-" + errLaunch;
                result.MessageData = errLaunch.ErrorData;
            }

            WriteObject(result);
        }
    }

    #endregion

    #region servertemplates update
    [Cmdlet("Update", "RSServerTemplate")]
    public class servertemplate_update : Cmdlet
    {

        [Parameter(Position = 1, Mandatory = true, ValueFromPipeline = true)]
        public string servertemplateID;

        [Parameter(Position = 2, Mandatory = false)]
        public string name;

        [Parameter(Position = 3, Mandatory = false)]
        public string description;

        protected override void ProcessRecord()
        {

            Types.returnServerTemplateUpdate retResult = new Types.returnServerTemplateUpdate();

            base.ProcessRecord();

            try
            {
                bool rsUpdateServerTemplate = RightScale.netClient.ServerTemplate.update(servertemplateID, name, description);

                retResult.ServerTemplateID = servertemplateID;
                retResult.Message = "Success";
                retResult.Details = "ServerTemplate Updated";
                retResult.Result = true;

                WriteObject(retResult);
            }
            catch (RightScaleAPIException rex)
            {
                retResult.ServerTemplateID = servertemplateID;
                retResult.Message = "Fail";
                retResult.Details = rex.ErrorData;
                retResult.APIHref = rex.APIHref;
                retResult.Result = false;

                WriteObject(retResult);
            }
            catch (System.Exception excp)
            {
                retResult.ServerTemplateID = servertemplateID;
                retResult.Message = "Fail";
                retResult.Details = "Exception updating ServerTemplate - " + excp.Message;
                retResult.APIHref = null;
                retResult.Result = false;

                WriteObject(retResult);
            }
        }
    }
    #endregion

    #region servertemplates commit
    [Cmdlet("Commit", "RSServerTemplate")]
    public class servertemplate_commit : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string serverTemplateID;

        [Parameter(Position = 3, Mandatory = true)]
        public string message;

        [Parameter(Position = 2, Mandatory = false)]
        public SwitchParameter headdependencies;

        [Parameter(Position = 4, Mandatory = false)]
        public SwitchParameter freezerepositories;

        protected override void ProcessRecord()
        {

            Types.returnServerTemplateCommit result = new Types.returnServerTemplateCommit();

            base.ProcessRecord();

            try
            {
                string rsServerTemplateCommitID = RightScale.netClient.ServerTemplate.commit(serverTemplateID, headdependencies, message, freezerepositories);

                if (!string.IsNullOrEmpty(rsServerTemplateCommitID))
                {
                    result.ServerTemplateID = serverTemplateID;
                    result.ServerTemplateCommittedID = rsServerTemplateCommitID;
                    result.Message = "ServerTemplate Committed";
                    result.Result = true;

                    WriteObject(result);
                }
                else
                {
                    result.ServerTemplateID = serverTemplateID;
                    result.Message = "Error Committing ServerTemplate";
                    result.Details = rsServerTemplateCommitID;
                    result.Result = false;

                    WriteObject(result);
                }
            }
            catch (RightScaleAPIException rex)
            {
                result.ServerTemplateID = serverTemplateID;
                result.Result = false;
                result.Message = rex.Message;
                result.Description = rex.InnerException.Message; ;
                result.APIHref = rex.APIHref;

                WriteObject(result);
            }
        }
    }
    #endregion

    #region servertemplates destroy
    [Cmdlet("Destroy", "RSServerTemplate")]
    public class servertemplate_destroy : Cmdlet
    {
            [Parameter(Position = 1, Mandatory = true)]
            public string serverTemplateID;

            protected override void ProcessRecord()
            {

                Types.returnServerTemplateDestroy result = new Types.returnServerTemplateDestroy();

                base.ProcessRecord();

                try
                {
                    bool rsServerTemplate = RightScale.netClient.ServerTemplate.destroy(serverTemplateID);

                    if (rsServerTemplate == true)
                    {
                        result.ServerTemplateID = serverTemplateID;
                        result.Message = "ServerTemplate Destroyed";
                        result.Result = true;
                    }
                    else
                    {
                        result.ServerTemplateID = serverTemplateID;
                        result.Message = "Error destroying ServerTemplate";
                        result.Result = false;
                    }
                }
                catch (RightScaleAPIException errDestroy)
                {
                    result.ServerTemplateID = serverTemplateID;
                    result.Result = false;
                    result.Message = errDestroy.InnerException.ToString() + "-" + errDestroy;
                    result.MessageData = errDestroy.ErrorData;
                }

                WriteObject(result);
            }
    }
    #endregion

    #region servertemplates commit
    [Cmdlet("Publish", "RSServerTemplate")]
    public class servertemplate_publish : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string serverTemplateID;

        [Parameter(Position = 3, Mandatory = true)]
        public string accountGroupIDs;

        [Parameter(Position = 2, Mandatory = false)]
        public SwitchParameter allowComments;

        [Parameter(Position = 4, Mandatory = false)]
        public string longDescription;

        [Parameter(Position = 5, Mandatory = false)]
        public string descriptionNotes;

        [Parameter(Position = 6, Mandatory = true)]
        public string shortDescription;

        [Parameter(Position = 7, Mandatory = false)]
        public SwitchParameter emailComments;

        [Parameter(Position = 8, Mandatory = false)]
        public string categories;
        
        protected override void ProcessRecord()
        {

            Types.returnServerTemplatePublish result = new Types.returnServerTemplatePublish();

            List<string> lstAcctGrps = new List<string>();
            List<string> lstCategories = new List<string>();

            if (!string.IsNullOrEmpty(accountGroupIDs)) {lstAcctGrps = new List<string>(accountGroupIDs.Split(','));}
            if (!string.IsNullOrEmpty(categories)){lstCategories = new List<string>(categories.Split(','));}

            base.ProcessRecord();

            try
            {
                string rsServerTemplateCommitID = RightScale.netClient.ServerTemplate.publish(serverTemplateID,lstAcctGrps,allowComments,longDescription,descriptionNotes,shortDescription,emailComments,lstCategories);

                if (!string.IsNullOrEmpty(rsServerTemplateCommitID))
                {
                    result.ServerTemplateID = serverTemplateID;
                    result.Message = "ServerTemplate Published";
                    result.Result = true;

                    WriteObject(result);
                }
                else
                {
                    result.ServerTemplateID = serverTemplateID;
                    result.Message = "Error Publishing ServerTemplate";
                    result.Details = rsServerTemplateCommitID;
                    result.Result = false;

                    WriteObject(result);
                }
            }
            catch (RightScaleAPIException rex)
            {
                result.ServerTemplateID = serverTemplateID;
                result.Result = false;
                result.Message = rex.Message;
                result.Description = rex.InnerException.Message; ;
                result.APIHref = rex.APIHref;

                WriteObject(result);
            }
        }
    }
    #endregion

}