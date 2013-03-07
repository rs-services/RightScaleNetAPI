using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// Excption specific to RightScale API errors
    /// </summary>
    class RightScaleAPIException : Exception 
    {
        /// <summary>
        /// href endpoint reference of failed API call
        /// </summary>
        public string APIHref;

        /// <summary>
        /// Collection of parameters specific to the current API call
        /// </summary>
        public List<KeyValuePair<string, string>> parameterSet;

        /// <summary>
        /// Error data that's returned via the RightScale API
        /// </summary>
        public string ErrorData;

        /// <summary>
        /// Creates a new instance of a RightScale API Exception
        /// </summary>
        public RightScaleAPIException ():base()
	    {

	    }

        /// <summary>
        /// Creates a new instance of a RightScale API Exception
        /// </summary>
        /// <param name="message">Custom message for this instance of an exception</param>
        public RightScaleAPIException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Creates a new instance of a RightScale API Exception
        /// </summary>
        /// <param name="message">Custom message for this instance of an exception</param>
        /// <param name="href">href endpoint reference for a failed API call</param>
        /// <param name="errorData">Error data that's returned via the RightScale API</param>
        public RightScaleAPIException(string message, string href, string errorData):base(message)
        {
            this.APIHref = href;
            this.ErrorData = errorData;
        }

        /// <summary>
        /// Creates a new instance of a RightScale API Exception
        /// </summary>
        /// <param name="message">Custom message for this instance of an exception</param>
        /// <param name="href">href endpoint reference for a failed API call</param>
        /// <param name="errorData">Error data that's returned via the RightScale API</param>
        /// <param name="innerException">Inner exception thrown by the underlying process</param>
        public RightScaleAPIException(string message, string href, string errorData, Exception innerException)
            : base(message, innerException)
        {
            this.APIHref = href;
            this.ErrorData = errorData;
        }

        /// <summary>
        /// Creates a new instance of a RightScale API Exception
        /// </summary>
        /// <param name="message">Custom message for this instance of an exception</param>
        /// <param name="href">href endpoint reference for a failed API call</param>
        /// <param name="errorData">Error data that's returned via the RightScale API</param>
        /// <param name="innerException">Inner exception thrown by the underlying process</param>
        /// <param name="paramSet">Set of parameters passed into the RightScale API for the failed call throwing this exception</param>
        public RightScaleAPIException(string message, string href, string errorData, Exception innerException, List<KeyValuePair<string, string>> paramSet)
            : base(message, innerException)
        {
            this.APIHref = href;
            this.ErrorData = errorData;
            this.parameterSet = paramSet;
        }
    }
}
