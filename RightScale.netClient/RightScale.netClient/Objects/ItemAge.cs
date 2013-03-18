using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class ItemAge
    {
        private List<string> validAlgorithms = new List<string>() { "max_10", "avg_10" };
        private string maxAgeRegex = @"^\d+$";

        public string regexp { get; set; }

        private string _max_age;

        public string max_age
        {
            get
            {
                return this._max_age;
            }
            set
            {
                if (Utility.CheckStringRegex("max_age", maxAgeRegex, value))
                {
                    this._max_age = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Value [" + value + "] specified for max_age not a valid value");
                }
            }
        }

        private string _algorithm;

        public string algorithm
        {
            get
            {
                return this._algorithm;
            }
            set
            {
                if (validAlgorithms.Contains(value))
                {
                    this._algorithm = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Value [" + value + "] specified for algorithm not a valid value");
                }
            }
        }

    }
}
