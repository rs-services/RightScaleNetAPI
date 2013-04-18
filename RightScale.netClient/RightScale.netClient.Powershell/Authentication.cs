using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.PowerShell.Commands;
using System;
using System.Security;
using System.Runtime.InteropServices;
using RightScale.netClient;
using RightScale.netClient.Core;

namespace RightScale.netClient.Powershell
{
    #region authenticate cmdlets
    [Cmdlet(VerbsCommon.New, "RSSession")]
    public class authenticate : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = false)]
        public string AccountID;

        [Parameter(Position = 3, Mandatory = false)]
        public string Username;

        [Parameter(Position = 2, Mandatory = false)]
        public System.Security.SecureString Password;

        [Parameter(Position = 4, Mandatory = false)]
        public string oAuthToken;
        

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            bool auth;

            if (string.IsNullOrEmpty(oAuthToken))
            {
                IntPtr stringPointer = Marshal.SecureStringToBSTR(Password);
                string strPwd = Marshal.PtrToStringBSTR(stringPointer);
                Marshal.ZeroFreeBSTR(stringPointer);

                if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(strPwd) || string.IsNullOrEmpty(AccountID)) { throw new System.Exception("Username, Password and AccountID required if not using token authentication"); }

                auth = APIClient.Instance.Authenticate(Username, strPwd, AccountID);

            }
            else
            {
                auth = APIClient.Instance.Authenticate(oAuthToken);
            }

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