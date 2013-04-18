using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;

namespace RightScale.netClient.Powershell
{
    #region auditentry index show cmdlets
    [Cmdlet(VerbsCommon.Get, "RSAuditEntries")]
    public class auditentries_index : Cmdlet
    {

        //TODO:  need to formate date / times with same value for date
        [Parameter(Position = 1, Mandatory = true, HelpMessage = "Date / Time value for start of search range")]
        public System.DateTime startDate;

        [Parameter(Position = 2, Mandatory = true, HelpMessage = "Date / Time value for end of search range")]
        public System.DateTime endDate;

        [Parameter(Position = 3, Mandatory = false)]
        public string view;

        [Parameter(Position = 4, Mandatory = false)]
        public string filter;

        [Parameter(Position = 5, Mandatory = true)]
        public string limit;


        protected override void ProcessRecord()
        {
            if(startDate == endDate){throw new System.Exception("Start Date/Time and End Date/Time can not be the same");}

            List<Filter> lstFilter = new List<Filter>();

            if (filter != null)
            {
                Filter fltFilter = Filter.parseFilter(filter);
                lstFilter.Add(fltFilter);
            }

            base.ProcessRecord();
            try
            {
                List<AuditEntry> rsAuditEntries = RightScale.netClient.AuditEntry.index(lstFilter, view, limit, startDate, endDate);

                if (rsAuditEntries.Count > 0)
                {
                    WriteObject(rsAuditEntries);
                }
                else
                {
                    WriteObject("No Audit entries found");
                }
            }
            catch (RightScaleAPIException rsXCP)
            {
                WriteObject("RSAPI Exception - " + rsXCP.InnerException);
            }
            catch (System.Exception genXCP)
            {
                WriteObject("Exception - " + genXCP.InnerException);

            }
           

        }
    }

    //---------------------------------------------------------------------
    [Cmdlet(VerbsCommon.Show, "RSAuditEntry")]
    public class auditentries_show : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string auditEntryID;


        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            AuditEntry rsAuditEntry = RightScale.netClient.AuditEntry.show(auditEntryID);

            WriteObject(rsAuditEntry);

        }
    }
    #endregion

    #region auditentries create / append
    [Cmdlet(VerbsCommon.New, "RSAuditEntry")]
    public class auditentries_create : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string itemHref;

        [Parameter(Position = 2, Mandatory = true)]
        public string summary;

        [Parameter(Position = 3, Mandatory = true)]
        public string detail;


        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                string rsNewAuditEntryID = RightScale.netClient.AuditEntry.create(itemHref, summary, detail);

                if (rsNewAuditEntryID != "")
                {
                    WriteObject("Audit entry created:  " + rsNewAuditEntryID);
                }
                else
                {
                    WriteObject("Error creating Audit entry");
                }
            }
            catch (RightScaleAPIException errCreate)
            {
                WriteObject(errCreate);
            }
            catch (System.Exception xcpCreate)
            {
                WriteObject(xcpCreate);
            }
        }
    }
    #endregion

}