using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// Static class contains href String.format templates for use when creating href links
    /// </summary>
    public static class APIHrefs
    {
        #region private segments

        /// <summary>
        /// Segment that's tacked on to the end of an existing href to launch a server
        /// </summary>
        private static string launchSegment = "/launch";

        /// <summary>
        /// Segment that's tacked on to the end of an existing href to show the detail of that object
        /// </summary>
        private static string detailSegment = "/detail";

        /// <summary>
        /// Segment that's tacked on to the end of an existing href to execute multiple scripts/recipes
        /// </summary>
        private static string multiRunExecutableSegment = "/multi_run_executable";

        /// <summary>
        /// Segment that's tacked on to the end of an existing href to execute a script/recipe
        /// </summary>
        private static string runExecutableSegment = "/run_executable";

        /// <summary>
        /// Segment that's tacked on to the end of an existing href to terminate multiple instances
        /// </summary>
        private static string multiTerminateSegment = "/multi_terminate";

        /// <summary>
        /// Segment that's tacked on to the end of an existing href to terminate a single instance
        /// </summary>
        private static string terminateSegment = "/terminate";

        /// <summary>
        /// Segment that's tacked on to the end of an existing href to clone that object
        /// </summary>
        private static string cloneSegment = "/clone";

        /// <summary>
        /// Segment that's included when working with inputs
        /// </summary>
        private static string inputSegment = "/inputs";

        /// <summary>
        /// Segment that's tacked on to the end of a href to reboot a server
        /// </summary>
        private static string rebootSegment = "/reboot";

        /// <summary>
        /// Segment that's included to accept index 0 replacement in a string.format operation
        /// </summary>
        private static string ID0 = "/{0}";

        /// <summary>
        /// Segment that's included to accept index 1 replacement in a string.format operation
        /// </summary>
        private static string ID1 = "/{1}";

        #endregion

        #region string.format templates for RightScale API hrefs

        /// <summary>
        /// Base account href that takes no parameters
        /// </summary>
        public static string Account = "/api/accounts";

        /// <summary>
        /// Account href takes one parameter which is the Account ID
        /// </summary>
        public static string AccountByID = Account + ID0;

        /// <summary>
        /// Base AccountGroup href that takes no parameters
        /// </summary>
        public static string AccountGroup = "/api/account_groups";

        /// <summary>
        /// AccountGroup href takes one parameter which is the AccountGroup ID
        /// </summary>
        public static string AccountGroupByID = AccountGroup + ID0;

        /// <summary>
        /// AuditEntry - takes no parameters
        /// </summary>
        public static string AuditEntry = "/api/audit_entries";

        /// <summary>
        /// Audit Entry - for getting specific AuditEntries takes one parameter which is the AuditEntry ID
        /// </summary>
        public static string AuditEntryByID = AuditEntry + ID0;

        /// <summary>
        /// Audit Entry Append href - takes one parameter which is the AuditEntry ID
        /// </summary>
        public static string AuditEntryAppend = AuditEntryByID + "/append";

        /// <summary>
        /// AuditEntry detail view which takes one parameter which is the AuditEntry ID
        /// </summary>
        public static string AuditEntryDetail = AuditEntryByID + detailSegment;

        /// <summary>
        /// Server AlertSpec href for working with alert specs within a server scope - takes one href which is the Server ID
        /// </summary>
        public static string ServerAlertSpec = ServerByID + "/alert_specs";

        /// <summary>
        /// ServerAlertSpec href takes two parameters - the Server ID and the AlertSpec ID
        /// </summary>
        public static string ServerAlertSpecByID = ServerAlertSpec + ID1;

        /// <summary>
        /// ServerArray AlertSpec href for working with alert specs within a server array - takes one parameter which is the ServerArray ID
        /// </summary>
        public static string ServerArrayAlertSpec = ServerArrayById + "/alert_specs";

        /// <summary>
        /// ServerArrayAlertSpec href takes two parameters - the ServerArray ID and the AlertSpec Id
        /// </summary>
        public static string ServerArrayAlertSpecByID = ServerArrayById + ID1;

        /// <summary>
        /// href performs a clone action on a given ServerArray - takes one parameter which is the ServerArray ID
        /// </summary>
        public static string ServerArrayClone = ServerArrayById + cloneSegment;

        /// <summary>
        /// href performs a destroy action on a given ServerArray - takes one parameter which is the ServerARray ID
        /// </summary>
        public static string ServerArrayDestroy = ServerArrayById + "/destroy";

        /// <summary>
        /// Base href for working with Cloud obejects - takes no parameters
        /// </summary>
        public static string Cloud = "/api/clouds";

        /// <summary>
        /// Cloud href takes one parameter which is the Cloud ID
        /// </summary>
        public static string CloudByID = Cloud + ID0;

        /// <summary>
        /// Base href for working with ServerTemplates - takes no parameters
        /// </summary>
        public static string ServerTemplate = "/api/server_templates";

        /// <summary>
        /// ServerTemplate href takes one parameter which is the ServerTempalte ID
        /// </summary>
        public static string ServerTemplateByID = ServerTemplate + ID0;

        /// <summary>
        /// Security group base href - requires one parameter which is the Cloud ID the security groups are in
        /// </summary>
        public static string SecurityGroup = CloudByID + "/security_groups";

        /// <summary>
        /// SecurityGroup href takes two parameters - the Cloud ID and the Security Group ID
        /// </summary>
        public static string SecurityGroupByID = SecurityGroup + ID1;

        /// <summary>
        /// Base href for working with MultiCloud Image objects - takes no parameters
        /// </summary>
        public static string MultiCloudImage = "/api/multi_cloud_images";

        /// <summary>
        /// MultiCloud Image href takes one parameter which is the MultiCloud Image ID
        /// </summary>
        public static string MultiCloudImageByID = MultiCloudImage + ID0;

        /// <summary>
        /// Base href for working with images - takes one parameter which is the Cloud ID where the images are located
        /// </summary>
        public static string Image = CloudByID + "/images";

        /// <summary>
        /// Image href takes two parameters = the Cloud ID and the InstanceType ID
        /// </summary>
        public static string ImageByID = Image + ID1;

        /// <summary>
        /// Base href for working with instances - takes one parameter which is the Cloud ID where the instances are located
        /// </summary>
        public static string Instance = CloudByID + "/instances";

        /// <summary>
        /// Instance href takes two parameters - the Cloud ID and InstanceID being queried
        /// </summary>
        public static string InstanceByID = Instance + ID1;

        /// <summary>
        /// Href gets current instances in a given ServerArray - takes one input which is the ServerArray ID
        /// </summary>
        public static string ServerArrayInstance = ServerArrayById + "/current_instances";

        /// <summary>
        /// Base Href for working with ServerArray objects
        /// </summary>
        public static string ServerArray = "/api/server_arrays";

        /// <summary>
        /// Href gets a specific ServerArray - takes one parameter which is the ServerArray ID
        /// </summary>
        public static string ServerArrayById = ServerArray + ID0;

        /// <summary>
        /// Href performs a launch action on a ServerArray - takes one parameter which is the ServerArray ID
        /// </summary>
        public static string ServerArrayLaunch = ServerArrayById + launchSegment;

        /// <summary>
        /// Href performs a launch action on an in stance - takes two parameters which are the Cloud ID and Instance ID 
        /// </summary>
        public static string InstanceLaunch = InstanceByID + launchSegment;

        /// <summary>
        /// Href for performing a reboot on a specific Server object - takes one parameter which is the Server ID
        /// </summary>
        public static string ServerReboot = ServerByID + rebootSegment;

        /// <summary>
        /// Href for performing a reboot on a specific Instance object - takes two parameters which are the Cloud ID and Instance ID
        /// </summary>
        public static string InstanceReboot = InstanceByID + rebootSegment;

        /// <summary>
        /// Href for executing multiple scripts/recipes on a ServerArray - takes one parameter which is the ServerArray ID
        /// </summary>
        public static string ServerArrayMultiRunExecutable = ServerArrayById + multiRunExecutableSegment;

        /// <summary>
        /// Href for executing multiple scripts/recipes on a single Instance - takes two parameters which are the CloudID and the Instance ID
        /// </summary>
        public static string InstanceMultiRunExecutable = InstanceByID + multiRunExecutableSegment;

        /// <summary>
        /// Href for terminating multiple instances - takes two parameters which are the Cloud ID and the Instance ID
        /// </summary>
        public static string InstanceMultiTerminate = InstanceByID + multiTerminateSegment;

        /// <summary>
        /// Href for terminating multiple instances within a ServerArray - takes two parameters which are the CloudID and the Instance ID
        /// </summary>
        public static string ServerArrayMultiTerminate = ServerArrayById + multiTerminateSegment;

        /// <summary>
        /// Href for running an executable on a single Instance - takes two parameters which are the Cloud ID and the Instance ID
        /// </summary>
        public static string InstanceRunExecutable = InstanceByID + runExecutableSegment;

        /// <summary>
        /// Base href for working with IntanceType objects - takes one parameter which is the Cloud ID
        /// </summary>
        public static string InstanceType = CloudByID + "/instance_types";

        /// <summary>
        /// InstanceType href takes two parameters - the Cloud ID and the InstanceType ID
        /// </summary>
        public static string InstanceTypeByID = InstanceType + ID1;

        /// <summary>
        /// Base href for working with SshKey objects - takes one parameter which is the Cloud ID
        /// </summary>
        public static string SshKey = CloudByID + "/ssh_keys";

        /// <summary>
        /// SSH Key href takes two parameters - the Cloud ID and SSH Key ID
        /// </summary>
        public static string SshKeyByID = SshKeyByID + ID1;

        /// <summary>
        /// Base href for working with DataCenter objects - takes one parameter which is the Cloud ID
        /// </summary>
        public static string DataCenter = CloudByID + "/datacenters";

        /// <summary>
        /// DataCenter href takes two parameters - the Cloud ID and DataCenter ID
        /// </summary>
        public static string DataCenterByID = DataCenter + ID1;

        /// <summary>
        /// Deployment href takes one parameter which is the Deployment ID
        /// </summary>
        public static string DeploymentByID = Deployment + ID0;

        /// <summary>
        /// Base href for working with RightScript objects - takes no parameters
        /// </summary>
        public static string RightScript = "/api/right_scripts";

        /// <summary>
        /// RightScript href takes one parameter which is the RightScript ID
        /// </summary>
        public static string RightScriptByID = RightScript + ID0;

        /// <summary>
        /// href returns a specific server within a specific deployment - takes two parameters which are the Deployment ID and the Server ID
        /// </summary>
        public static string DeploymentServerByID = DeploymentServer + ID1;

        /// <summary>
        /// href returns a specific server - takes one parameter which is the Server ID
        /// </summary>
        public static string ServerByID = Server + ID0;

        /// <summary>
        /// Base href for working with servers - takes no parameters
        /// </summary>
        public static string Server = "/api/servers";

        /// <summary>
        /// Base href for working with deployments - takes no parameters
        /// </summary>
        public static string Deployment = "/api/deployments";

        /// <summary>
        /// href for working with servers within a given deployment - takes one parameter which is the Deployment ID
        /// </summary>
        public static string DeploymentServer = DeploymentByID + "/servers";

        /// <summary>
        /// href for working with server arrays within a given deployment - takes one parameter which is the Deployment ID
        /// </summary>
        public static string DeploymentServerArray = DeploymentByID + "/server_arrays";

        /// <summary>
        /// href for working with a specific server array within a given deployment - takes tawo parameters which are the DeploymentID and ServerArrayID
        /// </summary>
        public static string DeploymentServerArrayByID = DeploymentServerArray + ID1;

        /// <summary>
        /// href for cloning a specific Deployment - takes one parameter which is the Deployment ID
        /// </summary>
        public static string DeploymentClone = DeploymentByID + cloneSegment;

        /// <summary>
        /// href for cloning a specific Server - takes one parameter which is the Server ID
        /// </summary>
        public static string ServerClone = ServerByID + cloneSegment;

        /// <summary>
        /// href for performing a launch action on a specific server - takes one parameter which is the Server ID
        /// </summary>
        public static string ServerLaunch = ServerByID + launchSegment;

        /// <summary>
        /// href for performing a terminate action on a specific server - takes one parameter which is the Server ID
        /// </summary>
        public static string ServerTerminate = ServerByID + terminateSegment;

        /// <summary>
        /// href for working with Deployment-level inputs - takes one parameter which is the Deployment ID
        /// </summary>
        public static string DeploymentInput = DeploymentByID + inputSegment;

        /// <summary>
        /// href for working with ServerTemplate-level inputs - takes one parameter which is the ServerTemplate ID
        /// </summary>
        public static string ServerTemplateInput = ServerTemplateByID + inputSegment;

        /// <summary>
        /// href for setting a custom lodgement for a specific Instance - takes two parameters which are the Cloud ID and the Instance ID
        /// </summary>
        public static string InstanceSetCustomLodgement = InstanceByID + "/set_custom_lodgement";

        /// <summary>
        /// href for performing a start action on a specific instance - takes two parameters which are the Cloud ID and the Instance ID
        /// </summary>
        public static string InstanceStart = InstanceByID + "/start";

        /// <summary>
        /// href for performing a stop action on a specific Instance - takes two parameters which are the Cloud ID and the Instance ID
        /// </summary>
        public static string InstanceStop = InstanceByID + "/stop";

        /// <summary>
        /// href for performing a terminmate action on a specific instance - takes two parameters which are the CloudID and the Instance ID
        /// </summary>
        public static string InstanceTerminate = InstanceByID + "/terminate";

        /// <summary>
        /// Base href for working with IPAddress objects - takes one parameter which is the Cloud ID
        /// </summary>
        public static string IPAddress = CloudByID + "/ip_addresses";

        /// <summary>
        /// href is for working with a specific IPAddress object - takes two parameters which are the CloudID and IPAddress ID
        /// </summary>
        public static string IPAddressByID = IPAddress + ID1;

        /// <summary>
        /// href for performing a clone action on a ServerTemplate - takes one parameter which is the ServerTemplate ID
        /// </summary>
        public static string ServerTemplateClone = ServerTemplateByID + cloneSegment;

        /// <summary>
        /// base href for working with Tag objects
        /// </summary>
        public static string Tag = "/api/tags";

        /// <summary>
        /// href for working with Tag objects by resource - takes no parameters
        /// </summary>
        public static string TagByResource = Tag + "/by_resource";

        /// <summary>
        /// href for working with current tasks on a given instance - takes two parameters which are the Cloud ID and the Instance ID
        /// </summary>
        public static string InstanceTasks = InstanceByID + "/live/tasks";

        #endregion
    }
}
