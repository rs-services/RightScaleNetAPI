using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using RightScale.netClient.Core;
using RightScale.netClient;

namespace RightScale.netClient.ActivityLibrary.Base
{
    /// <summary>
    /// Common base class for server-based activities such as creating and updating Server and ServerArray objects
    /// </summary>
    public abstract class ServerBasedActivity : Base.RSCodeActivity
    {
        /// <summary>
        /// Collection of Inputs to be provided to Server/ServerArray
        /// </summary>
        public InArgument<List<Input>> inputs { get; set; }

        /// <summary>
        /// ID of instance type for Server/ServerArray
        /// </summary>
        public InArgument<string> instanceTypeID { get; set; }

        /// <summary>
        /// ID of image for Server/ServerArray
        /// </summary>
        public InArgument<string> imageID { get; set; }

        /// <summary>
        /// ID of kernel image for Server/ServerArray
        /// </summary>
        public InArgument<string> kernelImageID { get; set; }

        /// <summary>
        /// ID of MultiCloud Image for Server/ServerArray
        /// </summary>
        public InArgument<string> multiCloudImageID { get; set; }

        /// <summary>
        /// ID of ramdisk image for Server/ServerArray
        /// </summary>
        public InArgument<string> ramdiskImageID { get; set; }

        /// <summary>
        /// Collection of Security Group IDs for Server/ServerArray
        /// </summary>
        public InArgument<List<string>> securityGroupIDs { get; set; }

        /// <summary>
        /// ID of SSH key to be used for this Server/ServerArray
        /// </summary>
        public InArgument<string> sshKeyID { get; set; }

        /// <summary>
        /// User Data to be provided to the Server/ServerArray
        /// </summary>
        public InArgument<string> userData { get; set; }

        /// <summary>
        /// Boolean indicating if optimized storage is to be used for this Server/ServerArray - not supported in all clouds
        /// </summary>
        public InArgument<bool> optimized { get; set; }

        /// <summary>
        /// Description to be used for Server/ServerArray
        /// </summary>
        public InArgument<string> description { get; set; }

    }
}
