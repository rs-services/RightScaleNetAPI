using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;

namespace RightScale.netClient.Powershell
{

    #region runnablebindings index show cmdlets
    [Cmdlet(VerbsCommon.Get, "RSRunnableBindings")]
    public class runnablebindings_index_show : Cmdlet
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
                    List<RunnableBindings> rsRunnableBindings = RightScale.netClient.RunnableBindings.index_servertemplate(servertemplateID, view);
                    WriteObject(rsRunnableBindings);
                }
                else
                {

                    WriteObject("No ServerTemplate ID Passed");
                }
            }
            catch (RightScaleAPIException rex)
            {
                WriteObject(rex.Message);
                WriteObject(rex.ErrorData);
            }

        }
    #endregion
    }
}