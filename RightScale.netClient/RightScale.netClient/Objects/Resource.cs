using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Resource : Core.RightScaleObjectBase<Resource>
    {
        public List<Tag> tags { get; set; }
    }
}
