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

        public System.Security.SecureString ReadLineAsSecureString()
        {
            SecureString secret = new SecureString();
            ConsoleKeyInfo currentKey;
            while ((currentKey = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                if (currentKey.Key == ConsoleKey.Backspace)
                {
                    if (secret.Length > 0)
                    {
                        secret.RemoveAt(secret.Length - 1);
                        Console.Write(currentKey.KeyChar);
                    }
                }
                else
                {
                    secret.AppendChar(currentKey.KeyChar);
                    Console.Write("*");
                }
            }
            Console.WriteLine();
            secret.MakeReadOnly();
            return secret;
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            bool auth;

            if (string.IsNullOrEmpty(oAuthToken))
            {

                if (string.IsNullOrEmpty(AccountID)) { Console.WriteLine("AccountID:");AccountID = Console.ReadLine();}
                if (string.IsNullOrEmpty(Username)) { Console.WriteLine("Username:"); Username = Console.ReadLine(); }
                if (Password == null) { Console.WriteLine("Paswsword:"); Password = ReadLineAsSecureString(); }

                IntPtr stringPointer = Marshal.SecureStringToBSTR(Password);
                string strPwd = Marshal.PtrToStringBSTR(stringPointer);
                Marshal.ZeroFreeBSTR(stringPointer);

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