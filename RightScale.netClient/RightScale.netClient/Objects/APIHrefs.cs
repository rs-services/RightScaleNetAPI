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
        /// <summary>
        /// Account href takes one parameter which is the Account ID
        /// </summary>
        public static string Account = "/api/accounts/{0}";

        /// <summary>
        /// AccountGroup href takes one parameter which is the AccountGroup ID
        /// </summary>
        public static string AccountGroup = "/api/account_groups/{0}";

        /// <summary>
        /// AuditEntry - takes no parameters
        /// </summary>
        public static string AuditEntry = "/api/audit_entries";

        /// <summary>
        /// Audit Entry - for getting specific AuditEntries takes one parameter which is the AuditEntry ID
        /// </summary>
        public static string AuditEntryByID = AuditEntry + "/{0}";

        /// <summary>
        /// Audit Entry Append href - takes one parameter which is the AuditEntry ID
        /// </summary>
        public static string AuditEntryAppend = AuditEntryByID + "/append";


        public static string AuditEntryDetail = AuditEntryByID + "/detail";

        /// <summary>
        /// ServerAlertSpec href takes two parameters - the Server ID and the AlertSpec ID
        /// </summary>
        public static string ServerAlertSpec = ServerByID + "/alert_specs/{1}";

        /// <summary>
        /// ServerArrayAlertSpec href takes two parameters - the ServerArray ID and the AlertSpec Id
        /// </summary>
        public static string ServerArrayAlertSpec = "/api/server_arrays/{0}/alert_specs/{1}";

        /// <summary>
        /// Cloud href takes one parameter which is the Cloud ID
        /// </summary>
        public static string Cloud = "/api/clouds/{0}";

        /// <summary>
        /// ServerTemplate href takes one parameter which is the ServerTempalte ID
        /// </summary>
        public static string ServerTemplate = "/api/server_templates/{0}";

        /// <summary>
        /// SecurityGroup href takes two parameters - the Cloud ID and the Security Group ID
        /// </summary>
        public static string SecurityGroup = Cloud + "/security_groups/{1}";

        /// <summary>
        /// MultiCloud Image href takes one parameter which is the MultiCloud Image ID
        /// </summary>
        public static string MultiCloudImage = "/api/multi_cloud_images/{0}";

        /// <summary>
        /// Image href takes two parameters = the Cloud ID and the InstanceType ID
        /// </summary>
        public static string Image = Cloud + "/images/{1}";

        public static string Instance = Cloud + "/instances";

        public static string InstanceByID = Instance + "/{1}";

        public static string ServerArrayInstance = ServerArrayById + "/current_instances";

        public static string ServerArray = "/api/server_arrays";

        public static string ServerArrayById = ServerArray + "/{0}";

        public static string ServerArrayLaunch = ServerArrayById + "/launch";

        public static string InstanceLaunch = InstanceByID + "/launch";

        public static string ServerReboot = ServerByID + "/reboot";

        public static string InstanceReboot = InstanceByID + "/reboot";

        public static string ServerArrayMultiRunExecutable = ServerArrayById + "/multi_run_executable";

        public static string InstanceMultiRunExecutable = InstanceByID + "/multi_run_executable";

        public static string InstanceMultiTerminate = InstanceByID + "/multi_terminate";

        public static string ServerArrayMultiTerminate = ServerArrayById + "/multi_terminate";

        public static string InstanceRunExecutable = InstanceByID + "/run_executable";

        /// <summary>
        /// InstanceType href takes two parameters - the Cloud ID and the InstanceType ID
        /// </summary>
        public static string InstanceType = Cloud + "/instance_types/{1}";

        /// <summary>
        /// SSH Key href takes two parameters - the Cloud ID and SSH Key ID
        /// </summary>
        public static string SshKey = Cloud + "/ssh_keys/{1}";

        public static string DataCenter = Cloud + "/datacenters";

        /// <summary>
        /// DataCenter href takes two parameters - the Cloud ID and DataCenter ID
        /// </summary>
        public static string DataCenterByID = DataCenter + "/{1}";

        /// <summary>
        /// Deployment href takes one parameter which is the Deployment ID
        /// </summary>
        public static string DeploymentByID = "/api/deployments/{0}";

        /// <summary>
        /// RightScript href takes one parameter which is the RightScript ID
        /// </summary>
        public static string RightScript = "/api/right_scripts/{0}";

        public static string DeploymentServerByID = DeploymentServer + "/{1}";

        public static string ServerByID = Server + "/{0}";

        public static string Server = "/api/servers";

        public static string Deployment = "/api/deployments";

        public static string DeploymentServer = DeploymentByID + "/servers";

        public static string ServerClone = ServerByID + "/clone";

        public static string ServerLaunch = ServerByID + "/launch";

        public static string ServerTerminate = ServerByID + "/terminate";

    }
}
