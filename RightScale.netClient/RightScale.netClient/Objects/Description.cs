using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// Description class is used in defining information when publishing assets to the MultiCloud Marketplace
    /// </summary>
    public class Description
    {
        /// <summary>
        /// Long description
        /// </summary>
        public string longDescription { get; set; }
        
        /// <summary>
        /// New Revision Notes
        /// </summary>
        public string notes { get; set; }
 
        /// <summary>
        /// Short Description
        /// </summary>
        public string shortDescription { get; set; }

        /// <summary>
        /// Full constructor for description which takes three parameters
        /// </summary>
        /// <param name="longDesc">Long description</param>
        /// <param name="shortDesc">Short description</param>
        /// <param name="descNote">Notes for description</param>
        public Description(string longDesc, string shortDesc, string descNote)
        {
            this.longDescription = longDesc;
            this.notes = descNote;
            this.shortDescription = shortDesc;
        }

        /// <summary>
        /// Minimal constructor for description which takes two parameters
        /// </summary>
        /// <param name="longDesc">Long description</param>
        /// <param name="shortDesc">Short description</param>
        public Description(string longDesc, string shortDesc)
        {
            this.longDescription = longDesc;
            this.shortDescription = shortDesc;
        }

        /// <summary>
        /// Method returns a formatted list of keyvaluepairs to be used as parameters for RS API calls - assumes a default format of descriptions[{0}] 
        /// </summary>
        /// <returns>formatted list of keyvalue pairs of parameters for this Description</returns>
        public List<KeyValuePair<string, string>> descriptionParameters()
        {
            return descriptionParameters("descriptions[{0}]");
        }

        /// <summary>
        /// Method returns a formatted list of keyvaluepairs to be used as parameters for RS API calls
        /// </summary>
        /// <param name="paramFormat">string.format template for the key portion of the parameter set - if this is empty assumes a default format of descriptions[{0}] </param>
        /// <returns>formatted list of keyvalue pairs of parameters for this Description</returns>
        public List<KeyValuePair<string,string>> descriptionParameters(string keyFormat)
        {
            if (string.IsNullOrWhiteSpace(keyFormat))
            {
                keyFormat = "descriptions[{0}]";
            }
            List<KeyValuePair<string, string>> retVal = new List<KeyValuePair<string, string>>();
            retVal.Add(new KeyValuePair<string,string>(string.Format(keyFormat, "long"), this.longDescription));
            if (!string.IsNullOrWhiteSpace(this.notes))
            {
                retVal.Add(new KeyValuePair<string,string>(string.Format(keyFormat,"notes"), this.notes));
            }
            retVal.Add(new KeyValuePair<string,string>(string.Format(keyFormat,"short"), this.shortDescription));
            return retVal;
        }
    }
}
