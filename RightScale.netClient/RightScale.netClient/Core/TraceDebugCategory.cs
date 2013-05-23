using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient.Core
{
    /// <summary>
    /// Collection of strings used within the Debug.Write/line/if process to identify categories
    /// </summary>
    public static class TraceDebugCategory
    {
        public const string HttpGet = "Http GET";
        public const string HttpPut = "Http PUT";
        public const string HttpPost = "Http POST";
        public const string HttpDelete = "Http DELETE";
        public const string RSAPIAuthenticate = "RS API Authentication";
    }
}
