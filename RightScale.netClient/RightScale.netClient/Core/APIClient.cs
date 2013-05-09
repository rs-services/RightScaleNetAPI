using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient.Core
{
    /// <summary>
    /// Singleton class for accessing RightScale 1.5 API
    /// </summary>
    public sealed class APIClient : APIClientBase
    {
        #region APIClient Singleton Implementation

        /// <summary>
        /// Singleton instance implementation of APIClient
        /// </summary>
        private static APIClient instance;

        /// <summary>
        /// Public instance for singleton access
        /// </summary>
        public static APIClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new APIClient();
                }
                return instance;
            }
        }

        /// <summary>
        /// Base constructor initialies http client objects and initializes base url for RightScale API
        /// </summary>
        private APIClient()
        {
            setAPIVersion("1.5");
        }

        #endregion
    }
}
