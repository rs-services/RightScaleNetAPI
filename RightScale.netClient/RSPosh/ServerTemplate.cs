using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;

namespace RSPosh
{
    #region servers cmdlets
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

    #region
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
                        result.Message = "Server Launched";
                        result.Result = true;                        
                    }
                    else
                    {
                        result.ServerTemplateID = rsServerTemplate;
                        result.Message = "Error creating server template";
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
    #endregion

}