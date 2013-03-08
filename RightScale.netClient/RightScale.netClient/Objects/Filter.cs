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
    }
}
