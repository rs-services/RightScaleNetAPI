using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient 
{
    public class QueueSize
    {
        private string ipiRegex = @"^\d+$";

        private string _items_per_instance;
        public string items_per_instance
        {
            get
            {
                return _items_per_instance;
            }
            set
            {
                if (Utility.CheckStringRegex("items_per_instance", ipiRegex, value))
                {
                    this._items_per_instance = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Value [" + value + "] specified for items_per_instance not a valid value");
                }
            }
        }
    }
}
