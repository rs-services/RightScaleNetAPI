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
        #region Filter Properties

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

        #endregion

        /// <summary>
        /// string.format template for oututting filter string
        /// </summary>
        private const string toStringFormat = "filter[]={0}{1}{2}";

        /// <summary>
        /// string.format tempalte for outputting ony the key/value pair portion of this object
        /// </summary>
        private const string toFilterOnlyStringFormat = "{0}{1}{2}";

        #region Filter.ctor()

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

        #endregion

        /// <summary>
        /// Method gets filter only portion of a filter object for reformatting within a parameter set for POST and PUT calls
        /// </summary>
        /// <returns>string only containing key=value for filter</returns>
        public string ToFilterOnlyString()
        {
            return string.Format(toFilterOnlyStringFormat, this.Key, getOpSign(this.Operator), this.Value);
        }

        /// <summary>
        /// Private method gets string representation of the current Operator value
        /// </summary>
        /// <returns>string representation of this.Operator</returns>
        private static string getOpSign(FilterOperator filterOp)
        {
            string retVal = string.Empty;
            switch (filterOp)
            {
                case FilterOperator.Equal:
                    retVal = "==";
                    break;
                case FilterOperator.NotEqual:
                    retVal = "<>";
                    break;
                default:
                    break;
            }
            return retVal;
        }

        /// <summary>
        /// Override of ToString method to generate properly formatted string representation of filtre
        /// </summary>
        /// <returns>RightScale API formatted filter string</returns>
        public override string ToString()
        {
            return string.Format(toStringFormat, this.Key, getOpSign(this.Operator), this.Value);
        }

        #region Filter Static Parsing Methods

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
                retVal = parseFilterFromString(workingString, FilterOperator.Equal);
            }
            else if (workingString.Contains("<>"))
            {
                retVal = parseFilterFromString(workingString, FilterOperator.NotEqual);
            }
            else
            {
                throw new ArgumentException("filter " + filterString + " does not contain == or <> which are the only two valid comparison operators for filters");
            }
            return retVal;
        }

        /// <summary>
        /// Private method handles parsing filter from string based on inputs
        /// </summary>
        /// <param name="filterString">full filter string</param>
        /// <param name="workingString">working string pared down from filter string</param>
        /// <param name="opVal">operator value - equals or not equals</param>
        /// <returns></returns>
        private static Filter parseFilterFromString(string workingString, FilterOperator filterOp)
        {
            Filter retVal = null;
            string opVal = getOpSign(filterOp);
            workingString = workingString.Replace(opVal, "Þ");
            string[] filterTestSplit = workingString.Split('Þ');
            if (filterTestSplit.Length == 2)
            {
                retVal = new Filter(filterTestSplit[0], filterOp, filterTestSplit[1]);
            }
            else if (filterTestSplit.Length > 2)
            {
                throw new ArgumentException("Cannot parse filter'" + workingString + "'. There are too many " + opVal + " symbols");
            }
            return retVal;
        }

        #endregion
    }
}
