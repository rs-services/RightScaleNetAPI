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

}