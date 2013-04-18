using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;

namespace RightScale.netClient.Powershell
{
    #region cloud cmdlets
    [Cmdlet(VerbsCommon.Get, "RSClouds")]
    public class clouds_index : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = false)]
        public string filter;

        protected override void ProcessRecord()
        {
            if (filter != null)
            {
                Filter fltFilter = Filter.parseFilter(filter);
                List<Filter> lstFilter = new List<Filter>();

                lstFilter.Add(fltFilter);

                base.ProcessRecord();
                List<Cloud> rsClouds = RightScale.netClient.Cloud.index(lstFilter);

                WriteObject(rsClouds);
            }
            else
            {
                base.ProcessRecord();
                List<Cloud> rsClouds = RightScale.netClient.Cloud.index();

                WriteObject(rsClouds);

            }

        }
    }

    [Cmdlet(VerbsCommon.Get, "RSCloud")]
    public class cloud_show : Cmdlet
    {

        [Parameter(Position = 1, Mandatory = true )]
        public string cloudID;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            Cloud rsCloud = RightScale.netClient.Cloud.show(cloudID);

            WriteObject(rsCloud);

        }
    }
    #endregion

}