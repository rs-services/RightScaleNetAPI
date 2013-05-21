using System;
using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;

namespace RightScale.netClient.Powershell
{
    #region session index index_instance show cmdlets
    [Cmdlet(VerbsCommon.Get, "RSSessions")]
    public class sessions_index_show : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = false)]
        public string instanceID;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                if (instanceID != null)
                {
                    Instance rsInstanceSession = RightScale.netClient.Session.index_instance_session();
                    WriteObject(rsInstanceSession);
                }
                else
                {
                    Session rsSession = RightScale.netClient.Session.index();

                    WriteObject(rsSession);
                }
            }
            catch (RightScaleAPIException rex)
            {
                WriteObject(rex);
                WriteObject(rex.ErrorData);
                WriteObject(rex.APIHref);
            }
        }
    #endregion


    }
}