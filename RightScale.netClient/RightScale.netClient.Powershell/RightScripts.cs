using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;

namespace RightScale.netClient.Powershell
{
        
    #region rightscripts index show cmdlets
    [Cmdlet(VerbsCommon.Get, "RSRightScripts")]
    public class rightscript_index_show : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = false)]
        public string rightscriptID;

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
                if(rightscriptID != null)
                {
                   RightScripts rsRightScripts = RightScale.netClient.RightScripts.show_rightscripts(rightscriptID, view);
                    WriteObject(rsRightScripts);
                }
                else
                {
                    WriteObject("No RightScriptID Passed");
                }
            }
            catch (RightScaleAPIException rex)
            {
                WriteObject(rex.Message);
                WriteObject(rex.ErrorData);
            }

        }

   
    }
    #endregion
}