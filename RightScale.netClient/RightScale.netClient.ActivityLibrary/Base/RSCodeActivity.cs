using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using RightScale.netClient.Core;
using RightScale.netClient;
using System.Activities;
using System.Diagnostics;

namespace RightScale.netClient.ActivityLibrary.Base
{
    /// <summary>
    /// Centralized base class containing common properties for authentication as well as authentnication checks and a shared logging mechanism
    /// </summary>
    public abstract class RSCodeActivity : CodeActivity
    {
        /// <summary>
        /// OAuth refresh token used for authenticating to RightScale API
        /// </summary>
        public InArgument<string> rsOAuthToken { get; set; }

        /// <summary>
        /// Username used for authenticating to RightScale API - when used, requires <paramref name="rsPassword"/> and <paramref name="rsAccountID"/>
        /// </summary>
        public InArgument<string> rsUserName { get; set; }

        /// <summary>
        /// Password for authenticating to RightScale API - when used, requires <paramref name="rsUserName"/> and <paramref name="rsAccountID"/>
        /// </summary>
        public InArgument<string> rsPassword { get; set; }

        /// <summary>
        /// Account ID for authenticating to RightScale API - when used, requires <paramref name="rsUserName"/> and <paramref name="rsPassword"/>
        /// </summary>
        public InArgument<string> rsAccountID { get; set; }

        /// <summary>
        /// Number of retries this process should loop through when attempting to launch a server 
        /// </summary>
        public InArgument<int> numRetries { get; set; }

        /// <summary>
        /// Amount of time between retries - implemented as a thread.sleep after an unsuccessful attempt to call the RightScale API
        /// </summary>
        public InArgument<TimeSpan> retryWaitTime { get; set; }

        public RSCodeActivity()
        {
            this.DisplayName = GetFriendlyName();
        }

        /// <summary>
        /// Common method for authenticating to the RightScale API
        /// </summary>
        /// <param name="context">CodeActivityContext of the derived class</param>
        /// <returns>true if authenticated, false if not, RightScaleAPIException if parameters are not set properly</returns>
        protected bool authClient(CodeActivityContext context)
        {
            LogInformation("Starting Auth Process");

            bool isAuthed = false;
            if (!string.IsNullOrWhiteSpace(this.rsOAuthToken.Get(context)))
            {
                isAuthed = APIClient.Instance.Authenticate(this.rsOAuthToken.Get(context));
                LogInformation("Authenticated via OAuth with result of " + isAuthed.ToString());
            }
            else if (!string.IsNullOrWhiteSpace(this.rsUserName.Get(context)) && !string.IsNullOrWhiteSpace(this.rsPassword.Get(context)) && !string.IsNullOrWhiteSpace(this.rsAccountID.Get(context)))
            {
                isAuthed = APIClient.Instance.Authenticate(this.rsUserName.Get(context), this.rsPassword.Get(context), this.rsAccountID.Get(context));
                LogInformation("Authenticated via username/password/account no with result of " + isAuthed.ToString());
            }
            else
            {
                LogError("Authentication failed");
                throw new RightScaleAPIException("Cannot authenticate without either providing an OAuth Refresh token or a username/password/accountno combination");
            }
            return isAuthed;
        }

        private const string categoryName = "RightScale.netClient.ActivityLibrary";

        protected void LogInformation(string message)
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);
            Trace.TraceInformation(sf.GetMethod().Module.Name + "." + sf.GetMethod().Name + ": " + message, categoryName);
        }

        protected void LogError(string message)
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);
            Trace.TraceError(sf.GetMethod().Module.Name + "." + sf.GetMethod().Name + ": " + message, categoryName);
        }

        protected void LogWarning(string message)
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);
            Trace.WriteLine(sf.GetMethod().Module.Name + "." + sf.GetMethod().Name + ": " + message, categoryName);
        }

        protected override void Execute(CodeActivityContext context)
        {
            int retries = 3;
            TimeSpan retryTime = new TimeSpan(0, 1, 0);

            if (numRetries != null && numRetries.Get<int>(context) > 0)
            {
                retries = numRetries.Get<int>(context);
            }

            if (retryWaitTime != null)
            {
                retryTime = retryWaitTime.Get<TimeSpan>(context);
            }

            bool completed = false;
            string errorMessage = string.Empty;

            for (int i = 0; i < retries; i++)
            {
                try
                {
                    if (PerformRightScaleTask(context))
                    {
                        completed = true;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    LogInformation("    RSAPI attempt #" + (i + 1).ToString() + " failed with exception " + ex.Message);
                    errorMessage += "(" + (i + 1).ToString() + " of " + numRetries.Get<int>(context).ToString() + "): " + ex.Message + Environment.NewLine;
                }
                Thread.Sleep(retryTime);
            }
            if (!completed && !string.IsNullOrWhiteSpace(errorMessage))
            {
                throw new RightScaleAPIException("Failed to perform RightScale API Call for " + GetFriendlyName() + " in " + numRetries.Get<int>(context).ToString() + " attempts", string.Empty, errorMessage);
            }
        }

        protected abstract string GetFriendlyName();

        protected abstract bool PerformRightScaleTask(CodeActivityContext context);
    }
}
