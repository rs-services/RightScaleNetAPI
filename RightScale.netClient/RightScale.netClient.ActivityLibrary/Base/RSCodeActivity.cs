using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightScale.netClient.Core;
using RightScale.netClient;
using System.Activities;
using System.Diagnostics;

namespace RightScale.netClient.ActivityLibrary.Base
{
    public abstract class RSCodeActivity : CodeActivity
    {
        public InArgument<string> rsOAuthToken { get; set; }

        public InArgument<string> rsUserName { get; set; }

        public InArgument<string> rsPassword { get; set; }

        public InArgument<string> rsAccountID { get; set; }

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

        protected abstract override void Execute(CodeActivityContext context);
    }
}
