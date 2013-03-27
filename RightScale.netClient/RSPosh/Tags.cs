using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;

namespace RSPosh
{
    #region Tags index / show cmdlets
    [Cmdlet(VerbsCommon.Get, "RSTags")]
    public class tags : Cmdlet
    {

        [Parameter(Position = 1, Mandatory = true)]
        public string[] hrefs;       

        protected override void ProcessRecord()
        {
           
            List<string> rsTags = RightScale.netClient.Tag.byResource(hrefs);

            WriteObject(rsTags);

        }
    }

    #endregion

    

}