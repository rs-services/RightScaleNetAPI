using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// FilterOperator defines whether filters are looking at matching or excluding within the filtering process
    /// </summary>
    public enum FilterOperator
    {
        Equal, 
        NotEqual
    }

    /// <summary>
    /// Class encapsulates the parameters of a RightScale API filter and overrides the ToString() method to build a formatted string filter
    /// </summary>
    public class Filter
    {
        /// <summary>
        /// Key to search on - determines which RightScale field is being filtered on
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// search value - value to use for matching
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Determines whether the operation is for the filter to have the key's value to match the input value or not equal the input value
        /// </summary>
        public FilterOperator Operator { get; set; }

        /// <summary>
        /// string.format template for oututting filter string
        /// </summary>
        private const string toStringFormat = "filter[]={0}{1}{2}";

        /// <summary>
        /// Creates a new instance of a filter
        /// </summary>
        /// <param name="key">name of the field to filter on</param>
        /// <param name="filterType">Equal or NotEqual</param>
        /// <param name="value">value to use for filtering</param>
        public Filter(string key, FilterOperator filterType, string value)
        {
            this.Key = key;
            this.Value = value;
            this.Operator = filterType;
        }

        /// <summary>
        /// Override of ToString method to generate properly formatted string representation of filtre
        /// </summary>
        /// <returns>RightScale API formatted filter string</returns>
        public override string ToString()
        {
            string opSign;
            switch (Operator)
	        {
		        case FilterOperator.Equal:
                    opSign = "==";
                 break;
                case FilterOperator.NotEqual:
                    opSign = "<>";
                 break;
                default:
                    throw new NotSupportedException();
	        }
            return string.Format(toStringFormat, this.Key, opSign, this.Value);
        }

        /// <summary>
        /// Static method returns a colection of Filter Objects from a filter string
        /// </summary>
        /// <param name="filterString">query string formatted filter (key==/<>value) or (filter[]=key==/<>value) for a list of filters</param>
        /// <returns>List of Filter objects</returns>
        public static List<Filter> parseFilterList(string filterString)
        {
            List<Filter> retVal = new List<Filter>();
            foreach (string s in filterString.Split('&'))
            {
                Filter newFilter = parseFilterString(s);
                if(newFilter != null)
                {
                    retVal.Add(newFilter);
                }
                else
                {
                    throw new ArgumentException("Filter '" + s + "' failed to parse for an unknown reason");
                }
            }
            return retVal;
        }

        /// <summary>
        /// Static method returns a single intance of a Filter object from the given filter string
        /// </summary>
        /// <param name="filterString">query string formatted filter for a single filter</param>
        /// <returns>Single instance of Filter object populated from filterString</returns>
        public static Filter parseFilter(string filterString)
        {
            return parseFilterString(filterString);
        }

        /// <summary>
        /// Private method handles the internals of parsing a string into an instance of a Filter object
        /// </summary>
        /// <param name="filterString">string representing a single filter</param>
        /// <returns>Populated instance of a Filter object</returns>
        private static Filter parseFilterString(string filterString)
        {
            Filter retVal = null;
            string workingString = filterString.Replace("filter[]=", "");
            if (workingString.Contains("=="))
            {
                workingString = workingString.Replace("==", "Þ");
                string[] filterTestSplit = workingString.Split('Þ');
                if (filterTestSplit.Length == 2)
                {
                    retVal = new Filter(filterTestSplit[0], FilterOperator.Equal, filterTestSplit[1]);
                }
                else if (filterTestSplit.Length > 2)
                {
                    throw new ArgumentException("Cannot parse filter '" + filterString + "'. There are too many == symbols");
                }
            }
            else if (workingString.Contains("<>"))
            {
                workingString = workingString.Replace("<>", "Þ");
                string[] filterTestSplit = workingString.Split('Þ');
                if (filterTestSplit.Length == 2)
                {
                    retVal = new Filter(filterTestSplit[0], FilterOperator.NotEqual, filterTestSplit[1]);
                }
                else if (filterTestSplit.Length > 2)
                {
                    throw new ArgumentException("Cannot parse filter'" + filterString + "'. There are too many <> symbols");
                }
            }
            else
            {
                throw new ArgumentException("filter " + filterString + " does not contain == or <> which are the only two valid comparison operators for filters");
            }
            return retVal;
        }
    }
}
