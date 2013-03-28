using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;

namespace RSPosh
{
    #region Tags index / show cmdlets
    /// <summary>
    /// Get RSTags by href
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "RSTags")]
    public class tags : Cmdlet
    {

        [Parameter(Position = 1, Mandatory = true)]
        public string href;       

        protected override void ProcessRecord()
        {
           
            List<RightScale.netClient.Tag> rsTags = RightScale.netClient.Tag.byResource(href);

            WriteObject(rsTags);

        }
    }

    #endregion

    

}