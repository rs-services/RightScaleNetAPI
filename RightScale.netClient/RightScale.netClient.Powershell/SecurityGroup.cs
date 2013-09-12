using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;

namespace RightScale.netClient.Powershell
{
    //index
    //show
    //create
    //destroy

    #region securitygroup index show cmdlets
    [Cmdlet(VerbsCommon.Get, "RSSecurityGroups")]
    public class securitygroups_index : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = false)]
        public string cloudID;

        [Parameter(Position = 2, Mandatory = false)]
        public string securitygroupID;

        [Parameter(Position = 3, Mandatory = false)]
        public string filter;

        [Parameter(Position = 4, Mandatory = false)]
        public string view;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                if (securitygroupID != null)
                {
                    SecurityGroup sg = RightScale.netClient.SecurityGroup.show(cloudID,securitygroupID);
                   
                    WriteObject(sg);
                }
                else
                {
                    List<Filter> lstFilter = new List<Filter>();

                    if (filter != null)
                    {
                        Filter fltFilter = Filter.parseFilter(filter);
                        lstFilter.Add(fltFilter);
                    }

                    List<SecurityGroup> sgs = RightScale.netClient.SecurityGroup.index(cloudID,lstFilter,view);
                    
                    WriteObject(sgs);
                }
            }
            catch(RightScaleAPIException rex)
            {
                WriteObject(rex);
                WriteObject(rex.ErrorData);
            }
        }
    }  
  
   #endregion

    #region securitygroup create / delete cmdlets
    [Cmdlet(VerbsCommon.New, "RSSecurityGroup")]
    public class securitygroup_create : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string securitygroupName;

        [Parameter(Position = 2, Mandatory = true)]
        public string cloudID;

        [Parameter(Position = 3, Mandatory = false)]
        public string description;

        protected override void ProcessRecord()
        {
            Types.returnSecurityGroup result = new Types.returnSecurityGroup();

            base.ProcessRecord();

            try
            {
                string rsNewSecurityGroupID = RightScale.netClient.SecurityGroup.create(cloudID,securitygroupName,description);

                if (rsNewSecurityGroupID != "")
                {
                    result.SecurityGoupID = rsNewSecurityGroupID;
                    result.Message = "Security Group Created";
                    result.Result = true;
                    
                    WriteObject(result);
                }
                else
                {
                    result.Message = "Error creating security group";
                    result.Result = false;
                    
                    WriteObject(result);
                }
            }
            catch (RightScaleAPIException errCreate)
            {
                result.ErrData = errCreate.ErrorData;
                result.APIHref = errCreate.APIHref;
                result.Message = errCreate.InnerException.Message;
                result.Result = false;
                
                WriteObject(result);
            }
        }
    }
    [Cmdlet("Destroy", "RSSecurityGroup")]
    public class securitygroup_delete : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string cloudID;

        [Parameter(Position = 2, Mandatory = true)]
        public string securitygroupID;

        protected override void ProcessRecord()
        {
            Types.returnSecurityGroup result = new Types.returnSecurityGroup();

            base.ProcessRecord();

            try
            {
                bool rsSecurityGroupDestroyed = RightScale.netClient.SecurityGroup.destroy(cloudID,securitygroupID);

                if (rsSecurityGroupDestroyed == true)
                {
                    result.SecurityGoupID = securitygroupID;
                    result.Message = "Security Group Destroyed";
                    result.Result = rsSecurityGroupDestroyed;

                    WriteObject(result);
                }
                else
                {
                    result.SecurityGoupID = securitygroupID;
                    result.Message = "Error Destroying Security Group";
                    result.Result = rsSecurityGroupDestroyed;

                    WriteObject(result);
                }
            }
            catch (RightScaleAPIException errCreate)
            {
                result.ErrData = errCreate.ErrorData;
                result.APIHref = errCreate.APIHref;
                result.Message = errCreate.InnerException.Message;
                result.Result = false;

                WriteObject(result);
            }
        }
    }

  #endregion

}