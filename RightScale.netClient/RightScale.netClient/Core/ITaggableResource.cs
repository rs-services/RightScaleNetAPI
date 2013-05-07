using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient.Core
{
    /// <summary>
    /// Common interface for all objects that are Taggable
    /// </summary>
    public interface ITaggableResource
    {
        /// <summary>
        /// Method required to handle populating a taggable resource
        /// </summary>
        void populateObject();
    }
}
