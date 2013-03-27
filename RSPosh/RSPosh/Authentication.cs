using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;
using RightScale.netClient.Core;

namespace RSPosh
{
    #region authenticate cmdlets
    [Cmdlet(VerbsCommon.New, "RSSession")]
    public class authenticate : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = false)]
        public string AccountID;

        [Parameter(Position = 2, Mandatory = false)]
        public string Password;

        [Parameter(Position = 3, Mandatory = false)]
        public string Username;

        [Parameter(Position = 4, Mandatory = false)]
        public string oAuthToken;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            bool auth = APIClient.Instance.Authenticate(Username,Password,AccountID);

            if (auth == true)
            {
                WriteObject("Connected to RightScale");
                WriteObject(auth);
            }
            else
            {
                WriteObject("Error connecting to RightScale");
                WriteObject(auth);
            }
            

        }

    }
    #endregion

}