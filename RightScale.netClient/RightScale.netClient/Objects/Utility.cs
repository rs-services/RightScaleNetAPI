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
        public static string argumentExceptionFormat = "Input '{0}' is not valid, it must be one of the following: '{1}' and was set to '{2}'";

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
    }
}
