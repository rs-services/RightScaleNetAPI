using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;

namespace RightScale.netClient
{
    /// <summary>
    /// Utility class provides a place to throw a bunch of useful repetitive tasks 
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Exception message format template for string.format calls
        /// </summary>
        private static string argumentExceptionFormat = "Input '{0}' is not valid, it must be one of the following: '{1}' and was set to '{2}'";

        /// <summary>
        /// Method checks to ensure that the string passed in has a value
        /// </summary>
        /// <param name="requiredString">string to check</param>
        /// <returns>True if string has value, throws ArgumentNullException if it doesn't</returns>
        public static bool CheckStringHasValue(string requiredString)
        {
            if (string.IsNullOrWhiteSpace(requiredString))
            {
                throw new ArgumentNullException("String input does not have a value and is required");
            }
            return true;
        }

        /// <summary>
        /// Method checks to ensure that the contents of the string are numeric
        /// </summary>
        /// <param name="numericString">String to check to make sure it only contains numbers</param>
        /// <returns>true if string is numeric, throws ArgumentOutOfRangeException if it doesn't</returns>
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

        /// <summary>
        /// Method checks a string to ensure it's one of a specified set of values
        /// </summary>
        /// <param name="inputName">name of the input to be identified when throwing errors</param>
        /// <param name="validViews">list of strings representing valid values</param>
        /// <param name="actualValue">string to test - must be one of those specified in <paramref name="validValues"/></param>
        /// <returns>True if <paramref name="actualValue"/> is contained within <paramref name="validViews"/>, throws ArgumentException if not</returns>
        public static bool CheckStringInput(string inputName, List<string> validViews, string actualValue)
        {
            bool retVal = false;

            if (!validViews.Contains(actualValue.ToLower()))
            {
                string validValueString = string.Empty;

                foreach (string s in validViews)
                {
                    validValueString += "'" + s + "', ";
                }
                validValueString = validValueString.Trim().TrimEnd(',');
                throw new ArgumentException(string.Format(argumentExceptionFormat, inputName, validValueString, actualValue));
            }
            else
            {
                retVal = true;
            }
            return retVal;
        }

        /// <summary>
        /// Method checks to make sure that the filters defined are contained within the collection of valid filters defined
        /// </summary>
        /// <param name="inputName">friendly input name used to identify input when throwing exceptions</param>
        /// <param name="validFilters">List of strings identifying all valid filter names</param>
        /// <param name="actualFilters">collection of filters to test</param>
        /// <returns>True if all filters are valid, throws ARgumentException if not</returns>
        public static bool CheckFilterInput(string inputName, List<string> validFilters, List<Filter> actualFilters)
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

        /// <summary>
        /// Helper method builds a query string from a list of keyvaluepair(string,string) object
        /// </summary>
        /// <param name="qsData">list of keyvaluepairs to turn into a query string</param>
        /// <returns>string in URL query string format</returns>
        public static string BuildGetQueryString(List<KeyValuePair<string, string>> qsData)
        {
            string retVal = string.Empty;
            foreach (KeyValuePair<string, string> kvp in qsData)
            {
                retVal += string.Format("{0}={1}&", kvp.Key, kvp.Value);
            }
            return retVal;
        }

        /// <summary>
        /// Static method takes a collection of name/value pairs and creates a properly formatted string representing a set of filters for a given RightScale API call
        /// </summary>
        /// <param name="filterSet">list of key value pairs to be built into a filter string when passing filters to the RightScale API</param>
        /// <returns>properly formatted string for filter collection</returns>
        public static string BuildFilterString(List<Filter> filterSet)
        {
            string retVal = string.Empty;
            if (filterSet != null)
            {
                foreach (Filter f in filterSet)
                {
                    retVal += f.ToString() + "&";
                }
            }
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

        /// <summary>
        /// Helper method reformats inputs from simple key/value collection to proper API formatting.  Also prepends text: in front of all inputs if it or another standard prefix is not in place
        /// </summary>
        /// <param name="inputSet">Raw key/value set of inputs</param>
        /// <returns>API formatted input collection</returns>
        public static List<KeyValuePair<string, string>> FormatInputCollection(List<KeyValuePair<string, string>> inputSet)
        {
            List<KeyValuePair<string, string>> retVal = new List<KeyValuePair<string, string>>();
            if (inputSet != null && inputSet.Count > 0)
            {
                foreach (KeyValuePair<string, string> kvp in inputSet)
                {
                    retVal.Add(new KeyValuePair<string, string>("inputs[][name]", kvp.Key));
                    if (kvp.Value.StartsWith("text:") || kvp.Value.StartsWith("cred:") || kvp.Value.StartsWith("env:") || kvp.Value.StartsWith("key:"))
                    {
                        retVal.Add(new KeyValuePair<string, string>("inputs[][value]", kvp.Value));
                    }
                    else
                    {
                        //assume iput is a text input if not otherwise specified
                        retVal.Add(new KeyValuePair<string, string>("inputs[][value]", string.Format("text:{0}", kvp.Value)));
                    }
                }
            }
            return retVal;
        }

        #region RightScale API href builders

        /// <summary>
        /// Helper method returns properly formatted account_href
        /// </summary>
        /// <param name="objectID">AccountID</param>
        /// <returns>formatted account_href</returns>
        public static string accountHref(string objectID)
        {
            return string.Format("/api/accounts/{0}", objectID);
        }

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
            return string.Format("/api/server_templates/{0}", objectID);
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
            return string.Format("/api/clouds/{0}/instance_types/{1}", cloudID, objectID);
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
        /// Helper method returns properly formatted right_script_href
        /// </summary>
        /// <param name="objectID">RightScript ID</param>
        /// <returns>Formatted right_script_href</returns>
        public static string rightScriptHref(string objectID)
        {
            return string.Format("/api/right_scripts/{0}", objectID);
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

        /// <summary>
        /// Method converts hashtable to list of keyvaluepairs for use in RSAPI calls
        /// </summary>
        /// <param name="htToConvert">Hashtable to convert</param>
        /// <returns>list of keyvaluepair(string,string) corresponding to the hashtable input</returns>
        public static List<KeyValuePair<string, string>> convertToKVP(Hashtable htToConvert)
        {
            List<KeyValuePair<string, string>> retVal = new List<KeyValuePair<string, string>>();

            foreach (string key in htToConvert.Keys)
            {
                retVal.Add(new KeyValuePair<string, string>(key, htToConvert[key].ToString()));
            }

            return retVal;
        }

        /// <summary>
        /// Helper method performs null/empty check on inputParameter and adds to parameterSet
        /// </summary>
        /// <param name="inputParameter"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterSet"></param>
        internal static void addParameter(string inputParameter, string parameterName, List<KeyValuePair<string, string>> parameterSet)
        {
            if (!string.IsNullOrWhiteSpace(inputParameter) && parameterSet != null)
            {
                parameterSet.Add(new KeyValuePair<string, string>(parameterName, inputParameter));
            }
        }
        #endregion
    }
}
