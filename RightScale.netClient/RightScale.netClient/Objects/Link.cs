using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// Link object is defined by a href, rel(name) and a reference to where this link is inherited from.
    /// </summary>
    public class Link
    {
        #region Link Properties

        /// <summary>
        /// URI fragment defining where this object can be queried from the RightScale API
        /// </summary>
        public string href { get; set; }

        /// <summary>
        /// Name of the object being linked
        /// </summary>
        public string rel { get; set; }

        /// <summary>
        /// Name of the source where this link was inherited from
        /// </summary>
        public string inherited_source { get; set; }

        #endregion

        /// <summary>
        /// Default constructor for building new links
        /// </summary>
        public Link()
        {

        }

        /// <summary>
        /// Constructor designed to take a rel and href value and build a corresponding instance of a Link object
        /// </summary>
        /// <param name="relVal">value to assign to rel property</param>
        /// <param name="hrefVal">value to assign to href property</param>
        public Link(string relVal, string hrefVal)
        {
            this.href = hrefVal;
            this.rel = relVal;
        }

        /// <summary>
        /// Constructor designed to take a rel, href and inherited source value and build a corresponding instance of a Link object
        /// </summary>
        /// <param name="relVal">value to assign to rel property</param>
        /// <param name="hrefVal">value to assign to href property</param>
        /// <param name="inheritedSourceVal">value to assign to inherited_source property</param>
        public Link(string relVal, string hrefVal, string inheritedSourceVal)
        {
            this.href = hrefVal;
            this.rel = relVal;
            this.inherited_source = inheritedSourceVal;
        }
    }
}
