using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Configuration;

namespace RightScale.netClient.Core
{
    public class TaggableResourceBase<T>:RightScaleObjectBase<T>, ITaggableResource
    {
        public List<Tag> tags { get; set; }

        public bool autoPopulateTags { get; set; }

        public TaggableResourceBase()
        {
            if (ConfigurationManager.AppSettings["RightScaleAPI_AutoPopulateTags"] != null)
            {
                bool boolResult = false;
                if (bool.TryParse(ConfigurationManager.AppSettings["RightScaleAPI_AutoPopulateTags"].ToString(), out boolResult))
                {
                    this.autoPopulateTags = boolResult;
                }
            }
        }

        public override void populateObject()
        {
            base.populateObject();
            if (this.autoPopulateTags)
            {
                populateTags();
            }
        }

        private void populateTags()
        {
            this.tags = Tag.byResource(this.resource_href);
        }

        public string resource_href
        {
            get 
            { 
                return getLinkValue("self"); 
            }
        }
    }
}
