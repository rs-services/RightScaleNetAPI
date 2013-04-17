using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;

namespace RSPosh
{
    #region publication index cmdlets
    [Cmdlet(VerbsCommon.Get, "RSPublications")]
    public class publication_index : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = false)]
        public string filter;

        [Parameter(Position = 2, Mandatory = false)]
        public string view = "default";

        protected override void ProcessRecord()
        {
        
            base.ProcessRecord();

            List<Filter> lstFilter = new List<Filter>();

            if (filter != null)
            {
                Filter fltFilter = Filter.parseFilter(filter);
                lstFilter.Add(fltFilter);
            }

            List<Publication> rsPublications = RightScale.netClient.Publication.index(lstFilter,view);

            WriteObject(rsPublications);

        }
    }


    #endregion
    #region publication index cmdlets
    [Cmdlet(VerbsCommon.Get, "RSPublication")]
    public class publication_show : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string publicationID;

        protected override void ProcessRecord()
        {

            base.ProcessRecord();

            Publication rsPublication = RightScale.netClient.Publication.show(publicationID);

            WriteObject(rsPublication);

        }
    }
    #endregion

    #region publication import cmdlets
    [Cmdlet("Import", "RSPublication")]
    public class publication_import : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string publicationID;

        protected override void ProcessRecord()
        {

            base.ProcessRecord();

            try
            {
                ServerTemplate rsImportPublication = RightScale.netClient.Publication.import(publicationID);
                WriteObject(rsImportPublication);
            }
            catch (RightScaleAPIException errLaunch)
            {
                WriteObject("Error Importing Publication - publicationID");
                WriteObject(errLaunch.InnerException.ToString() + "-" + errLaunch);
                WriteObject(errLaunch.ErrorData);
            }
            catch(System.Exception ex)
            {
                WriteObject("Error Importing Publication - publicationID");
                WriteObject(ex.Message);

            }
            

        }
    }
    #endregion


}