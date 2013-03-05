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
    }
}
