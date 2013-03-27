using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;

namespace RSPosh
{
    #region servers cmdlets
    [Cmdlet(VerbsCommon.Get, "RSServerArrays")]
    public class serverarray : Cmdlet
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
            List<ServerArray> rsServerArrays = RightScale.netClient.ServerArray.index(lstFilter, view);

            WriteObject(rsServerArrays);

        }
    }

    [Cmdlet(VerbsCommon.Get, "RSServerArray")]
    public class serverarray_show : Cmdlet
    {

        [Parameter(Position = 1, Mandatory = true)]
        public string serverarrayID;

        [Parameter(Position = 2, Mandatory = false)]
        public string view;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            ServerArray rsServerArray = RightScale.netClient.ServerArray.show(serverarrayID, view);

            WriteObject(rsServerArray);

        }
    }
    #endregion

}