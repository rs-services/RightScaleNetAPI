using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace RightScale.netClient
{
    public static class Utility
    {
        private static string argumentExceptionFormat = "Input '{0}' is not valid, it must be one of the following: '{1}' and was set to '{2}'";

        public static bool CheckStringHasValue(string requiredString)
        {
            if (string.IsNullOrWhiteSpace(requiredString))
            {
                throw new ArgumentNullException("String input does not have a value and is required");
            }
            return true;
        }

        public static bool CheckStringIsNumeric(string numericString)
        {
            foreach (char c in numericString)
            {
                if (c < '0' || c > '9')
                {
                    throw new ArgumentOutOfRangeException("String input is not numeric");
                }
            }
            return true;
        }

        public static bool CheckStringInput(string inputName, List<string> validViews, string actualName)
        {
            bool retVal = false;

            if (!validViews.Contains(actualName.ToLower()))
            {
                string validValueString = string.Empty;

                foreach (string s in validViews)
                {
                    validValueString += "'" + s + "', ";
                }
                validValueString = validValueString.Trim().TrimEnd(',');
                throw new ArgumentException(string.Format(argumentExceptionFormat, inputName, validValueString, actualName));
            }
            else
            {
                retVal = true;
            }
            return retVal;
        }

        public static bool CheckFilterInput(string inputName, List<string> validFilters, List<KeyValuePair<string, string>> actualFilters)
        {
            bool retVal = false;

            string filterErrorMessage = string.Empty;
            if (actualFilters != null)
            {
                foreach (var kvp in actualFilters)
                {
                    if (!validFilters.Contains(kvp.Key.ToLower()))
                    {
                        filterErrorMessage += "Filter '" + kvp.Key.ToLower() + "' is not a valid filter name" + Environment.NewLine;
                    }
                }
                if (!string.IsNullOrWhiteSpace(filterErrorMessage))
                {
                    string validFilterString = string.Empty;
                    foreach (string s in validFilters)
                    {
                        validFilterString += "'" + s + "', ";
                    }
                    validFilterString = validFilterString.Trim().TrimEnd(',');
                    throw new ArgumentException("Input " + inputName + " is not valid:" + Environment.NewLine + filterErrorMessage + Environment.NewLine + "Valid filter names are: " + validFilterString);
                }
                retVal = true;
            }

            return retVal;
        }

        public static string BuildGetQueryString(List<KeyValuePair<string, string>> qsData)
        {
            string retVal = string.Empty;
            foreach (KeyValuePair<string, string> kvp in qsData)
            {
                retVal += kvp.Key + "=" + kvp.Value + "&";
            }
            retVal = retVal.TrimEnd('&');
            return retVal;
        }

        /// <summary>
        /// Static method takes a collection of name/value pairs and creates a properly formatted string representing a set of filters for a given RightScale API call
        /// </summary>
        /// <param name="filterSet">list of key value pairs to be built into a filter string when passing filters to the RightScale API</param>
        /// <returns>properly formatted string for filter collection</returns>
        public static string BuildFilterString(List<KeyValuePair<string, string>> filterSet)
        {
            string retVal = string.Empty;

            foreach (KeyValuePair<string, string> kvp in filterSet)
            {
                retVal += string.Format(@"filter[]=""{0}=={1}""&", kvp.Key, kvp.Value);
            }

            retVal = retVal.TrimEnd('&');

            return retVal;
        }

        /// <summary>
        /// Static method takes a collection of name/value pairs and creates a properly formatted string representing a set of inputs for a server or deployment within the RightScale system
        /// </summary>
        /// <param name="inputSet">list of key value pairs to be built into an input string when passing inputs to the RightScale API</param>
        /// <returns>properly formatted string for input collection</returns>
        public static string BuildInputString(List<KeyValuePair<string, string>> inputSet)
        {
            string retVal = string.Empty;

            foreach (KeyValuePair<string, string> kvp in inputSet)
            {
                retVal += string.Format("inputs[][name]={0}&inputs[][value]={1}&", kvp.Key, kvp.Value);
            }

            retVal = retVal.TrimEnd('&');

            return retVal;
        }

        #region RightScale API href builders

        /// <summary>
        /// Helper method returns properly formatted cloud_href
        /// </summary>
        /// <param name="objectID">CloudID</param>
        /// <returns>formatted cloud_href</returns>
        public static string cloudHref(string objectID)
        {
            return string.Format("/api/clouds/{0}", objectID);
        }

        /// <summary>
        /// Helper method returns properly formatted server_template_href
        /// </summary>
        /// <param name="objectID">ServerTempalte ID</param>
        /// <returns>formatted server_template_href</returns>
        public static string serverTemplateHref(string objectID)
        {
            return string.Format("/api/server_template{0}", objectID);
        }

        /// <summary>
        /// Helper method returns properly formatted security_group_href
        /// </summary>
        /// <param name="cloudID">Cloud ID where Security Group can be found</param>
        /// <param name="objectID">Security Group ID</param>
        /// <returns>formatted security_group_href</returns>
        public static string securityGroupHref(string cloudID, string objectID)
        {
            return string.Format("/api/clouds/{0}/security_groups/{1}", cloudID, objectID);
        }

        /// <summary>
        /// Helper method returns properly formatted multi_cloud_image_href
        /// </summary>
        /// <param name="objectID">MultiCloudImage ID</param>
        /// <returns>Formateed muti_cloud_image_href</returns>
        public static string multiCloudImageHref(string objectID)
        {
            return string.Format("/api/multi_cloud_images/{0}", objectID);
        }

        /// <summary>
        /// Helper method returns properly formatted kernel_image_href 
        /// </summary>
        /// <param name="cloudID">Cloud ID where Kernel Image can be found</param>
        /// <param name="objectID">KernelImage ID</param>
        /// <returns>Formatted kernel_image_href</returns>
        public static string kernelImageHref(string cloudID, string objectID)
        {
            return string.Format("/api/clouds/{0}/images/{1}", cloudID, objectID);
        }

        /// <summary>
        /// Helper method returns properly formatted instance_type_href
        /// </summary>
        /// <param name="cloudID">Cloud ID where instanceType can be found</param>
        /// <param name="objectID">InstanceType ID</param>
        /// <returns>Formatted instance_type_href</returns>
        public static string instanceTypeHref(string cloudID, string objectID)
        {
            return string.Format("api/clouds/{0}/instance_types/{1}", cloudID, objectID);
        }

        /// <summary>
        /// Helper method returns properly formatted ssh_key_href
        /// </summary>
        /// <param name="cloudID">Cloud ID where SSH Key can be found</param>
        /// <param name="objectID">SshKey ID</param>
        /// <returns>Formatted ssh_key_href</returns>
        public static string sshKeyHref(string cloudID, string objectID)
        {
            return string.Format("/api/clouds/{0}/ssh_keys/{1}", cloudID, objectID);
        }

        /// <summary>
        /// Helper method returns properly formatted image_href
        /// </summary>
        /// <param name="cloudID">Cloud ID were image can be found</param>
        /// <param name="objectID">Image ID</param>
        /// <returns>Formatted image_href</returns>
        public static string imageHref(string cloudID, string objectID)
        {
            return string.Format("/api/clouds/{0}/images/{1}", cloudID, objectID);
        }

        /// <summary>
        /// Helper method returns properly formatted datacenter_href
        /// </summary>
        /// <param name="cloudID">Cloud ID where datacenter can be found</param>
        /// <param name="objectID">Datacenter ID</param>
        /// <returns>Formatted datacenter_href</returns>
        public static string datacenterHref(string cloudID, string objectID)
        {
            return string.Format("/api/clouds/{0}/datacenters{1}", cloudID, objectID);
        }

        /// <summary>
        /// Helper method returns properly formatted deployment_href
        /// </summary>
        /// <param name="objectID">Deployment ID</param>
        /// <returns>Formatted deployment_href</returns>
        public static string deploymentHref(string objectID)
        {
            return string.Format("/api/deployments/{0}", objectID);
        }

        /// <summary>
        /// Helper method returns properly formatted ramdisk_image_href
        /// </summary>
        /// <param name="cloudID">Cloud ID where ramdisk image can be found</param>
        /// <param name="objectID">RamdiskImage ID</param>
        /// <returns>Formatted ramdisk_image_href</returns>
        public static string ramdiskImageHref(string cloudID, string objectID)
        {
            return string.Format("/api/clouds/{0}/images/{1}", cloudID, objectID);
        }
        #endregion
    }
}
