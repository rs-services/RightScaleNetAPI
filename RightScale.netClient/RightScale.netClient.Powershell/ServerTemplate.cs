using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;

namespace RightScale.netClient.Powershell
{
    #region servertemplates cmdlets
    [Cmdlet(VerbsCommon.Get, "RSServerTemplates")]
    public class servertemplate : Cmdlet
    {
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
            List<ServerTemplate> rsServerTemplates = RightScale.netClient.ServerTemplate.index(lstFilter, view);

            WriteObject(rsServerTemplates);

        }

        [Cmdlet(VerbsCommon.Get, "RSServerTemplate")]
        public class servertemplate_show : Cmdlet
        {

            [Parameter(Position = 1, Mandatory = true)]
            public string servertemplateID;

            [Parameter(Position = 2, Mandatory = false)]
            public string view;

            protected override void ProcessRecord()
            {
                base.ProcessRecord();
                ServerTemplate rsServerTemplate = RightScale.netClient.ServerTemplate.show(servertemplateID,view);

                WriteObject(rsServerTemplate);

            }
        }
    }
    #endregion

    #region create / clone
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


    #region destroy
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
}