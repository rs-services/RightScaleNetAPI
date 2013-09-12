using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;

namespace RightScale.netClient.Powershell
{
    //index
    //show
    //create
    //destroy

    #region securitygrouprule index show cmdlets
    [Cmdlet(VerbsCommon.Get, "RSSecurityGroupRule")]
    public class securitygrouprules_index : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string securitygroupruleHREF;

        [Parameter(Position = 2, Mandatory = false)]
        public string filter;
        
        [Parameter(Position = 3, Mandatory = false)]
        public string view;   

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                if (securitygroupruleHREF != null)
                {
   
                    List<SecurityGroupRule> sgrs = RightScale.netClient.SecurityGroupRule.index(securitygroupruleHREF, filter, view);
                    
                    WriteObject(sgrs);
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

    #region securitygrouprule create / delete cmdlets
    [Cmdlet(VerbsCommon.New, "RSSecurityGroupRule")]
    public class securitygrouprule_create : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string securitygroupID;

        [Parameter(Position = 2, Mandatory = true)]
        public string cloudID;

        [Parameter(Position = 3, Mandatory = true)]
        public string protocol;

        [Parameter(Position = 4, Mandatory = true)]
        public string type;

        [Parameter(Position = 5, Mandatory = false)]
        public string cidrIPs;

        [Parameter(Position = 6, Mandatory = false)]
        public string securitygroupName;

        [Parameter(Position = 7, Mandatory = false)]
        public string securitygroupOwner;

        [Parameter(Position = 8, Mandatory = false)]
        public string startPort;

        [Parameter(Position = 9, Mandatory = false)]
        public string endPort;

        [Parameter(Position = 10, Mandatory = false)]
        public string icmpCode;

        [Parameter(Position = 11, Mandatory = false)]
        public string icmpType;
     
        
        //cloudID,securitygroupHREF,protocol,type,cidrIPs,securitygroupName,securitygroupOwner,startPort,endPort,icmpCode,icmpType

        protected override void ProcessRecord()
        {
            Types.returnSecurityGroup result = new Types.returnSecurityGroup();

            base.ProcessRecord();
         
            try
            {
                string rsNewSecurityGroupID = RightScale.netClient.SecurityGroupRule.create(cloudID,securitygroupID,protocol,type,cidrIPs,securitygroupName,securitygroupOwner,startPort,endPort,icmpCode,icmpType);

                if (rsNewSecurityGroupID != "")
                {
                    result.SecurityGoupID = rsNewSecurityGroupID;
                    result.Message = "Security Group Rule Created";
                    result.Result = true;
                    
                    WriteObject(result);
                }
                else
                {
                    result.Message = "Error creating security group rule";
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
    [Cmdlet("Destroy", "RSSecurityGroupRule")]
    public class securitygrouprule_delete : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string securityGroupRuleID;

        protected override void ProcessRecord()
        {
            Types.returnSecurityGroup result = new Types.returnSecurityGroup();

            base.ProcessRecord();

            try
            {
                bool rsSecurityGroupDestroyed = RightScale.netClient.SecurityGroupRule.destroy(securityGroupRuleID);

                if (rsSecurityGroupDestroyed == true)
                {
                    result.SecurityGoupID = securityGroupRuleID;
                    result.Message = "Security Group Destroyed";
                    result.Result = rsSecurityGroupDestroyed;

                    WriteObject(result);
                }
                else
                {
                    result.SecurityGoupID = securityGroupRuleID;
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