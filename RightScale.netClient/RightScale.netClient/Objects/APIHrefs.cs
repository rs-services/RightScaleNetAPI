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

        private static string launchSegment = "/launch";

        private static string detailSegment = "/detail";

        private static string multiRunExecutableSegment = "/multi_run_executable";

        private static string runExecutableSegment = "/run_executable";

        private static string multiTerminateSegment = "/multi_terminate";

        private static string terminateSegment = "/terminate";

        private static string cloneSegment = "/clone";

        private static string rebootSegment = "/reboot";

        private static string ID0 = "/{0}";

        private static string ID1 = "/{1}";

        #endregion

        public static string Account = "/api/accounts";

        /// <summary>
        /// Account href takes one parameter which is the Account ID
        /// </summary>
        public static string AccountByID = Account + ID0;

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


        public static string AuditEntryDetail = AuditEntryByID + detailSegment;

        public static string ServerAlertSpec = ServerByID + "/alert_specs";

        /// <summary>
        /// ServerAlertSpec href takes two parameters - the Server ID and the AlertSpec ID
        /// </summary>
        public static string ServerAlertSpecByID = ServerAlertSpec + ID1;

        public static string ServerArrayAlertSpec = ServerArrayById + "/alert_specs";

        /// <summary>
        /// ServerArrayAlertSpec href takes two parameters - the ServerArray ID and the AlertSpec Id
        /// </summary>
        public static string ServerArrayAlertSpecByID = ServerArrayById + ID1;

        public static string Cloud = "/api/clouds";

        /// <summary>
        /// Cloud href takes one parameter which is the Cloud ID
        /// </summary>
        public static string CloudByID = Cloud + ID0;

        public static string ServerTemplate = "/api/server_templates";

        /// <summary>
        /// ServerTemplate href takes one parameter which is the ServerTempalte ID
        /// </summary>
        public static string ServerTemplateByID = ServerTemplate + ID0;

        public static string SecurityGroup = CloudByID + "/security_groups";

        /// <summary>
        /// SecurityGroup href takes two parameters - the Cloud ID and the Security Group ID
        /// </summary>
        public static string SecurityGroupByID = SecurityGroup + ID1;

        public static string MultiCloudImage = "/api/multi_cloud_images";

        /// <summary>
        /// MultiCloud Image href takes one parameter which is the MultiCloud Image ID
        /// </summary>
        public static string MultiCloudImageByID = MultiCloudImage + ID0;

        public static string Image = CloudByID + "/images";
        /// <summary>
        /// Image href takes two parameters = the Cloud ID and the InstanceType ID
        /// </summary>
        public static string ImageByID = Image + ID1;

        public static string Instance = CloudByID + "/instances";

        public static string InstanceByID = Instance + ID1;

        public static string ServerArrayInstance = ServerArrayById + "/current_instances";

        public static string ServerArray = "/api/server_arrays";

        public static string ServerArrayById = ServerArray + ID0;

        public static string ServerArrayLaunch = ServerArrayById + launchSegment;

        public static string InstanceLaunch = InstanceByID + launchSegment;

        public static string ServerReboot = ServerByID + rebootSegment;

        public static string InstanceReboot = InstanceByID + rebootSegment;



        public static string ServerArrayMultiRunExecutable = ServerArrayById + multiRunExecutableSegment;

        public static string InstanceMultiRunExecutable = InstanceByID + multiRunExecutableSegment;

        public static string InstanceMultiTerminate = InstanceByID + multiTerminateSegment;

        public static string ServerArrayMultiTerminate = ServerArrayById + multiTerminateSegment;

        public static string InstanceRunExecutable = InstanceByID + runExecutableSegment;

        public static string InstanceType = CloudByID + "/instance_types";

        /// <summary>
        /// InstanceType href takes two parameters - the Cloud ID and the InstanceType ID
        /// </summary>
        public static string InstanceTypeByID = InstanceType + ID1;

        public static string SshKey = CloudByID + "/ssh_keys";

        /// <summary>
        /// SSH Key href takes two parameters - the Cloud ID and SSH Key ID
        /// </summary>
        public static string SshKeyByID = SshKeyByID + ID1;

        public static string DataCenter = CloudByID + "/datacenters";

        /// <summary>
        /// DataCenter href takes two parameters - the Cloud ID and DataCenter ID
        /// </summary>
        public static string DataCenterByID = DataCenter + ID1;

        /// <summary>
        /// Deployment href takes one parameter which is the Deployment ID
        /// </summary>
        public static string DeploymentByID = Deployment + ID0;

        public static string RightScript = "/api/right_scripts";

        /// <summary>
        /// RightScript href takes one parameter which is the RightScript ID
        /// </summary>
        public static string RightScriptByID = RightScript + ID0;

        public static string DeploymentServerByID = DeploymentServer + ID1;

        public static string ServerByID = Server + ID0;

        public static string Server = "/api/servers";

        public static string Deployment = "/api/deployments";

        public static string DeploymentServer = DeploymentByID + "/servers";

        public static string ServerClone = ServerByID + cloneSegment;

        public static string ServerLaunch = ServerByID + launchSegment;

        public static string ServerTerminate = ServerByID + terminateSegment;

    }
}
