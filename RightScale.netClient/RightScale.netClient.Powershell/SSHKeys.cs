using System;
using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;

namespace RightScale.netClient.Powershell
{
    #region sshkeys index show cmdlets
    [Cmdlet(VerbsCommon.Get, "RSSSHKeys")]
    public class sshkeys_index_show : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string cloudID;

        [Parameter(Position = 2, Mandatory = false)]
        public string sshKeyID;

        [Parameter(Position = 3, Mandatory = false)]
        public string filter;

        [Parameter(Position = 4, Mandatory = false)]
        public string view;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                if (sshKeyID != null)
                {
                    SshKey rsSSHKey = RightScale.netClient.SshKey.show(cloudID, sshKeyID, view);
                    WriteObject(rsSSHKey);
                }
                else
                {
                    List<Filter> lstFilter = new List<Filter>();

                    if (filter != null)
                    {
                        Filter fltFilter = Filter.parseFilter(filter);
                        lstFilter.Add(fltFilter);
                    }

                    List<SshKey> rsSSHKeys = RightScale.netClient.SshKey.index(cloudID, lstFilter, view);

                    WriteObject(rsSSHKeys);
                }
            }
            catch (RightScaleAPIException rex)
            {
                WriteObject(rex);
                WriteObject(rex.ErrorData);
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
            }
        }


        //moved to single index show method
        //[Cmdlet(VerbsCommon.Get, "RSServer")]
        //public class server_show : Cmdlet
        //{
        //   [Parameter(Position = 1, Mandatory = true)]
        //    public string serverID;
        //
        //    [Parameter(Position = 2, Mandatory = false)]
        //    public string view;
        //
        //    protected override void ProcessRecord()
        //    {
        //        if (view == null) { view = "default"; }
        //
        //        base.ProcessRecord();
        //        try
        //        {
        //            Server rsServer = RightScale.netClient.Server.show(serverID, view);
        //            WriteObject(rsServer);
        //        }
        //        catch(RightScaleAPIException rse)
        //        {
        //            WriteObject("Error Getting Server");
        //            WriteObject(rse.Message);
        //            WriteObject(rse.ErrorData);
        //        }
        //
        //       
        //   }
        //}
=======
=======
>>>>>>> 54db2f79947666cfdb663afbf63c3bb1b64e7ad4
=======
>>>>>>> 54db2f79947666cfdb663afbf63c3bb1b64e7ad4
                WriteObject(rex.APIHref);
            }
        }

<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> 54db2f79947666cfdb663afbf63c3bb1b64e7ad4
=======
>>>>>>> 54db2f79947666cfdb663afbf63c3bb1b64e7ad4
=======
>>>>>>> 54db2f79947666cfdb663afbf63c3bb1b64e7ad4
    #endregion


    }
}