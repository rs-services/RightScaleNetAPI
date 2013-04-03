using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;

namespace RSPosh
{
    #region Tags byResource
    /// <summary>
    /// Get RSTags by href
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "RSTagsByHref")]
    public class tags_byhref : Cmdlet
    {

        [Parameter(Position = 1, Mandatory = true)]
        public string href;       

        protected override void ProcessRecord()
        {
           
            List<RightScale.netClient.Tag> rsTags = RightScale.netClient.Tag.byResource(href);

            WriteObject(rsTags);

        }
    }

    #endregion

    #region Tags byResource
    /// <summary>
    /// Get RSTags by href
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "RSTagsByTag")]
    public class tags_byresource : Cmdlet
    {

        [Parameter(Position = 1, Mandatory = true)]
        public string prefix;

        [Parameter(Position = 2, Mandatory = true)]
        public bool matchAll;

        [Parameter(Position = 3, Mandatory = true)]
        public string resourceType;

        [Parameter(Position = 4, Mandatory = true)]
        public string tags;

        protected override void ProcessRecord()
        {
            List<Tag> lstTags = new List<Tag>();

            try
            {

                List<Resource> rsResources = RightScale.netClient.Tag.byTag(prefix, matchAll, resourceType, lstTags);
                WriteObject(rsResources);
            }
            catch (RightScaleAPIException rsEx)
            {
                WriteObject(rsEx);
            }
            catch (System.Exception genEx)
            {
                WriteObject(genEx);

            }
           

        }
    }

    #endregion
}