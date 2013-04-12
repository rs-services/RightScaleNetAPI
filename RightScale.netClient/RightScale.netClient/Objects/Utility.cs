using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
            if (validViews != null && !string.IsNullOrWhiteSpace(actualValue) && validViews.Count > 0)
            {
                if (!validViews.Contains(actualValue))
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
            }
            return retVal;
        }

        /// <summary>
        /// Method checks a string against a regular expression to match it conforms to API Standards
        /// </summary>
        /// <param name="inputName">name of the input to be identified when throwing errors</param>
        /// <param name="regularExpression">regular expression to match string against</param>
        /// <param name="actualValue">string to test - must match the regularExpression specified</param>
        /// <returns>True if actualValue matches regularExpression pattern, throws ArgumentException if not</returns>
        public static bool CheckStringRegex(string inputName, string regularExpression, string actualValue)
        {
            bool retVal = false;

            Regex r = new Regex(regularExpression);
            Match m = r.Match(actualValue);
            if (m.Success)
            {
                retVal = true;
            }
            else
            {
                throw new ArgumentException("String input  " + inputName + " does not match regular expression validator: " + regularExpression);
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
            if (actualFilters != null && validFilters != null && actualFilters.Count > 0 && validFilters.Count > 0)
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
            if (qsData != null && qsData.Count > 0)
            {
                foreach (var kvp in qsData)
                {
                    retVal += string.Format("{0}={1}&", kvp.Key, kvp.Value);
                }
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

            if (filterSet != null && filterSet.Count > 0)
            {
                if (filterSet != null)
                {
                    foreach (Filter f in filterSet)
                    {
                        retVal += f.ToString() + "&";
                    }
                }
            }
            return retVal;
        }

        /// <summary>
        /// Static method takes a collection of name/value pairs and creates a properly formatted string representing a set of inputs for a server or deployment within the RightScale system
        /// </summary>
        /// <param name="inputSet">list of key value pairs to be built into an input string when passing inputs to the RightScale API</param>
        /// <returns>properly formatted string for input collection</returns>
        public static string BuildInputString(List<Input> inputSet)
        {
            string retVal = string.Empty;

            if (inputSet != null && inputSet.Count > 0)
            {
                foreach (Input kvp in inputSet)
                {
                    retVal += string.Format("inputs[][name]={0}&inputs[][value]={1}&", kvp.name, kvp.value);
                }
            }

            retVal = retVal.TrimEnd('&');

            return retVal;
        }

        /// <summary>
        /// Helper method reformats inputs from simple key/value collection to proper API formatting.  Also prepends text: in front of all inputs if it or another standard prefix is not in place
        /// </summary>
        /// <param name="inputSet">Raw key/value set of inputs</param>
        /// <returns>API formatted input collection</returns>
        public static List<KeyValuePair<string, string>> FormatInputCollection(List<Input> inputSet)
        {
            List<KeyValuePair<string, string>> retVal = new List<KeyValuePair<string, string>>();

            if (inputSet != null && inputSet.Count > 0)
            {
                foreach (Input kvp in inputSet)
                {
                    retVal.Add(new KeyValuePair<string, string>("inputs[][name]", kvp.name));
                    if (kvp.value.StartsWith("text:") || kvp.value.StartsWith("cred:") || kvp.value.StartsWith("env:") || kvp.value.StartsWith("key:"))
                    {
                        retVal.Add(new KeyValuePair<string, string>("inputs[][value]", kvp.value));
                    }
                    else
                    {
                        //assume iput is a text input if not otherwise specified
                        retVal.Add(new KeyValuePair<string, string>("inputs[][value]", string.Format("text:{0}", kvp.value)));
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
            if (!string.IsNullOrWhiteSpace(objectID))
            {
                return string.Format(APIHrefs.AccountByID, objectID);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Helper method returns properly formatted cloud_href
        /// </summary>
        /// <param name="objectID">CloudID</param>
        /// <returns>formatted cloud_href</returns>
        public static string cloudHref(string objectID)
        {
            if (!string.IsNullOrWhiteSpace(objectID))
            {
                return string.Format(APIHrefs.CloudByID, objectID);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Helper method returns a properly formatted volume snapshot href
        /// </summary>
        /// <param name="cloudID">CloudID </param>
        /// <param name="volumeid">VolumeID</param>
        /// <param name="objectid">Snapshot ID</param>
        /// <returns>formatted volume_snapshot_href</returns>
        public static string volumeSnapshotHref(string cloudID, string volumeid, string objectid)
        {
            if (!string.IsNullOrWhiteSpace(objectid) && !string.IsNullOrWhiteSpace(volumeid) && !string.IsNullOrWhiteSpace(cloudID))
            {
                return string.Format(APIHrefs.VolumeSnapshotByID, cloudID, volumeid, objectid); 
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Helper method returns a properly formatted volume type href
        /// </summary>
        /// <param name="cloudID">ID of Cloud</param>
        /// <returns>formatted volume_types href</returns>
        public static string volumeTypeHref(string cloudID)
        {
            if (!string.IsNullOrWhiteSpace(cloudID))
            {
                return string.Format(APIHrefs.VolumeType, cloudID);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Helper method returns a properly formatted volume type href for a specific volume type
        /// </summary>
        /// <param name="cloudID">ID of Cloud</param>
        /// <param name="volumeTypeID">VolumeType ID</param>
        /// <returns>volume_type_href for specific volume type</returns>
        public static string volumeTypeHrefByID(string cloudID, string volumeTypeID)
        {
            if (!string.IsNullOrWhiteSpace(cloudID) && !string.IsNullOrWhiteSpace(volumeTypeID))
            {
                return string.Format(APIHrefs.VolumeTypeByID, cloudID, volumeTypeID);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Helper method returns a properly formatted account group href for a specific account group
        /// </summary>
        /// <param name="accountGroupID">ID of account group</param>
        /// <returns>account_group_href for specific account group</returns>
        public static string accountGroupHrefByID(string accountGroupID)
        {
            if (!string.IsNullOrWhiteSpace(accountGroupID))
            {
                return string.Format(APIHrefs.AccountGroupByID, accountGroupID);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Helper method returns properly formatted server_template_href
        /// </summary>
        /// <param name="objectID">ServerTempalte ID</param>
        /// <returns>formatted server_template_href</returns>
        public static string serverTemplateHref(string objectID)
        {
            if (!string.IsNullOrWhiteSpace(objectID))
            {
                return string.Format(APIHrefs.ServerTemplateByID, objectID);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Helper method returns properly formatted security_group_href
        /// </summary>
        /// <param name="cloudID">Cloud ID where Security Group can be found</param>
        /// <param name="objectID">Security Group ID</param>
        /// <returns>formatted security_group_href</returns>
        public static string securityGroupHref(string cloudID, string objectID)
        {
            if (!string.IsNullOrWhiteSpace(objectID) && !string.IsNullOrWhiteSpace(objectID))
            {
                return string.Format(APIHrefs.SecurityGroupByID, cloudID, objectID);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Helper method returns properly formatted multi_cloud_image_href
        /// </summary>
        /// <param name="objectID">MultiCloudImage ID</param>
        /// <returns>Formateed muti_cloud_image_href</returns>
        public static string multiCloudImageHref(string objectID)
        {
            if (!string.IsNullOrWhiteSpace(objectID))
            {
                return string.Format(APIHrefs.MultiCloudImageByID, objectID);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Helper method returns properly formatted kernel_image_href 
        /// </summary>
        /// <param name="cloudID">Cloud ID where Kernel Image can be found</param>
        /// <param name="objectID">KernelImage ID</param>
        /// <returns>Formatted kernel_image_href</returns>
        public static string kernelImageHref(string cloudID, string objectID)
        {
            if (!string.IsNullOrWhiteSpace(objectID) && !string.IsNullOrWhiteSpace(cloudID))
            {
                return string.Format(APIHrefs.ImageByID, cloudID, objectID);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Helper method returns properly formatted instance_href
        /// </summary>
        /// <param name="cloudID">Cloud ID where Instance can be found</param>
        /// <param name="objectID">Instance ID</param>
        /// <returns>Formatted instance_href</returns>
        public static string InstanceHref(string cloudID, string objectID)
        {
            if (!string.IsNullOrWhiteSpace(objectID) && !string.IsNullOrWhiteSpace(objectID))
            {
                return string.Format(APIHrefs.InstanceByID, cloudID, objectID);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Helper method returns properly formatted volume_href
        /// </summary>
        /// <param name="cloudID">ID of Cloud where Volume can be found</param>
        /// <param name="objectID">ID of Volume</param>
        /// <returns>properly formatted volume_href</returns>
        public static string VolumeHref(string cloudID, string objectID)
        {
            if (!string.IsNullOrWhiteSpace(objectID) && !string.IsNullOrWhiteSpace(objectID))
            {
                return string.Format(APIHrefs.VolumeByID, cloudID, objectID);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Helper method returns properly formatted instance_type_href
        /// </summary>
        /// <param name="cloudID">Cloud ID where instanceType can be found</param>
        /// <param name="objectID">InstanceType ID</param>
        /// <returns>Formatted instance_type_href</returns>
        public static string instanceTypeHref(string cloudID, string objectID)
        {
            if (!string.IsNullOrWhiteSpace(objectID) && !string.IsNullOrWhiteSpace(cloudID))
            {
                return string.Format(APIHrefs.InstanceTypeByID, cloudID, objectID);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Helper method returns properly formatted ssh_key_href
        /// </summary>
        /// <param name="cloudID">Cloud ID where SSH Key can be found</param>
        /// <param name="objectID">SshKey ID</param>
        /// <returns>Formatted ssh_key_href</returns>
        public static string sshKeyHref(string cloudID, string objectID)
        {
            if (!string.IsNullOrWhiteSpace(objectID) && !string.IsNullOrWhiteSpace(cloudID))
            {
                return string.Format(APIHrefs.SshKeyByID, cloudID, objectID);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Helper method returns properly formatted image_href
        /// </summary>
        /// <param name="cloudID">Cloud ID were image can be found</param>
        /// <param name="objectID">Image ID</param>
        /// <returns>Formatted image_href</returns>
        public static string imageHref(string cloudID, string objectID)
        {
            if (!string.IsNullOrWhiteSpace(objectID) && !string.IsNullOrWhiteSpace(cloudID))
            {
                return string.Format(APIHrefs.ImageByID, cloudID, objectID);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Helper method returns properly formatted datacenter_href
        /// </summary>
        /// <param name="cloudID">Cloud ID where datacenter can be found</param>
        /// <param name="objectID">Datacenter ID</param>
        /// <returns>Formatted datacenter_href</returns>
        public static string datacenterHref(string cloudID, string objectID)
        {
            if (!string.IsNullOrWhiteSpace(objectID) && !string.IsNullOrWhiteSpace(cloudID))
            {
                return string.Format(APIHrefs.DataCenterByID, cloudID, objectID);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Helper method returns properly formatted deployment_href
        /// </summary>
        /// <param name="objectID">Deployment ID</param>
        /// <returns>Formatted deployment_href</returns>
        public static string deploymentHref(string objectID)
        {
            if (!string.IsNullOrWhiteSpace(objectID))
            {
                return string.Format(APIHrefs.DeploymentByID, objectID);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Helper method returns properly formatted right_script_href
        /// </summary>
        /// <param name="objectID">RightScript ID</param>
        /// <returns>Formatted right_script_href</returns>
        public static string rightScriptHref(string objectID)
        {
            if (!string.IsNullOrWhiteSpace(objectID))
            {
                return string.Format(APIHrefs.RightScriptByID, objectID);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Helper method returns properly formatted ramdisk_image_href
        /// </summary>
        /// <param name="cloudID">Cloud ID where ramdisk image can be found</param>
        /// <param name="objectID">RamdiskImage ID</param>
        /// <returns>Formatted ramdisk_image_href</returns>
        public static string ramdiskImageHref(string cloudID, string objectID)
        {
            if (!string.IsNullOrWhiteSpace(objectID) && !string.IsNullOrWhiteSpace(cloudID))
            {
                return string.Format(APIHrefs.ImageByID, cloudID, objectID);
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion

        #region Paramater builders
        /// <summary>
        /// Method converts hashtable to list of keyvaluepairs for use in RSAPI calls
        /// </summary>
        /// <param name="htToConvert">Hashtable to convert</param>
        /// <returns>list of keyvaluepair(string,string) corresponding to the hashtable input</returns>
        public static List<Input> convertToKVP(Hashtable htToConvert)
        {
            List<Input> retVal = new List<Input>();

            if (htToConvert != null && htToConvert.Count > 0)
            {
                foreach (string key in htToConvert.Keys)
                {
                    retVal.Add(new Input(key, htToConvert[key].ToString()));
                }
            }

            return retVal;
        }

        /// <summary>
        /// Helper method performs null/empty check on inputParameter and adds to parameterSet
        /// </summary>
        /// <param name="inputParameter"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterSet"></param>
        public static void addParameter(string inputParameter, string parameterName, List<KeyValuePair<string, string>> parameterSet)
        {
            if (inputParameter != null && !string.IsNullOrWhiteSpace(inputParameter) && parameterSet != null)
            {
                parameterSet.Add(new KeyValuePair<string, string>(parameterName, inputParameter));
            }
        }

        /// <summary>
        /// Private method to translate filter lists to convert to a parameter set 
        /// </summary>
        /// <param name="filterList">list of filters to push to a parameter set</param>
        /// <returns>list of keyvaluepairs for parameter inputs</returns>
        public static List<KeyValuePair<string, string>> FilterListToParameterSet(List<Filter> filterList)
        {
            List<KeyValuePair<string, string>> retVal = new List<KeyValuePair<string, string>>();
            if (filterList != null && filterList.Count > 0)
            {
                foreach (Filter f in filterList)
                {
                    Utility.addParameter(f.ToString(), "filter[]", retVal);
                }
            }
            return retVal;
        }

        /// <summary>
        /// Private method to translate string arrays to convert to a parameter set 
        /// </summary>
        /// <param name="filterList">array of string to push to a parameter set</param>
        /// <returns>list of keyvaluepairs for parameter inputs</returns>
        public static List<KeyValuePair<string, string>> StringArrayToParameterSet(string[] paramStrings, string paramName)
        {
            List<KeyValuePair<string, string>> retVal = new List<KeyValuePair<string, string>>();
            if (paramStrings != null && paramStrings.Length > 0)
            {
                foreach (string param in paramStrings)
                {
                    string thisparamName = string.Format("{0}[]",paramName);
                    //TODO:  I think this is not the correct format to pass, we don't need [] for each,  just one with multiple values
                    Utility.addParameter(param.ToString(), thisparamName, retVal);
                }
            }
            return retVal;
        }

        #endregion
    }
}
