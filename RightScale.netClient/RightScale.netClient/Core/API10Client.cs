using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient.Core
{
    /// <summary>
    /// Singleton class for calling RightScale 1.0 API
    /// </summary>
    public sealed class API10Client : APIClientBase
    {
        #region APIClient Singleton Implementation

        /// <summary>
        /// Singleton instance implementation of APIClient
        /// </summary>
        private static API10Client instance;

        /// <summary>
        /// Public instance for singleton access
        /// </summary>
        public static API10Client Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new API10Client();
                }
                return instance;
            }
        }

        /// <summary>
        /// Base constructor initialies http client objects and initializes base url for RightScale API
        /// </summary>
        private API10Client()
        {
            setAPIVersion("1.0");
        }

        #endregion
    }
}
