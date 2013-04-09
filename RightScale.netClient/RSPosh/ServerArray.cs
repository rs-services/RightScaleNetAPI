using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;

namespace RSPosh
{
    #region server array index show cmdlets
    [Cmdlet(VerbsCommon.Get, "RSServerArrays")]
    public class serverarray_index : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = false)]
        public string filter;

        [Parameter(Position = 2, Mandatory = false)]
        public string view;
        protected override void ProcessRecord()
        {
            List<Filter> lstFilter = new List<Filter>();

            if (filter != null)
            {
                Filter fltFilter = Filter.parseFilter(filter);
                lstFilter.Add(fltFilter);
            }

            base.ProcessRecord();
            List<ServerArray> rsServerArrays = RightScale.netClient.ServerArray.index(lstFilter, view);

            WriteObject(rsServerArrays);

        }
    }

    [Cmdlet(VerbsCommon.Get, "RSServerArray")]
    public class serverarray_show : Cmdlet
    {

        [Parameter(Position = 1, Mandatory = true)]
        public string serverarrayID;

        [Parameter(Position = 2, Mandatory = false)]
        public string view;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            ServerArray rsServerArray = RightScale.netClient.ServerArray.show(serverarrayID, view);

            WriteObject(rsServerArray);

        }
    }
    #endregion

#region server array create cmdlets
    [Cmdlet(VerbsCommon.New, "RSServerArray")]
    public class serverarray_create : Cmdlet
    {
        #region params

        [Parameter(Position = 1, Mandatory = true)]
        public string arrayType;

        [Parameter(Position = 2, Mandatory = true)]
        public string dataCenterPolicy;

        [Parameter(Position = 3, Mandatory = true)]
        public string deploymentID;

        [Parameter(Position = 4, Mandatory = true)]
        public string description;

        [Parameter(Position = 5, Mandatory = true)]
        public string elasticityParams;

        [Parameter(Position = 6, Mandatory = true)]
        public string cloudID;

        [Parameter(Position = 7, Mandatory = true)]
        public string dataCenterID;

        [Parameter(Position = 8, Mandatory = true)]
        public string inputs;
        
        [Parameter(Position = 9, Mandatory = false)]
        public string instanceTypeID;

        [Parameter(Position = 10, Mandatory = false)]
        public string imageID;

        [Parameter(Position = 11, Mandatory = false)]
        public string kernelImageID;

        [Parameter(Position = 12, Mandatory = false)]
        public string multiCloudImageID;

        [Parameter(Position = 13, Mandatory = false)]
        public string ramdiskImageID;

        [Parameter(Position = 14, Mandatory = false)]
        public string securityGroupIDs;

        [Parameter(Position = 15, Mandatory = true)]
        public string serverTemplateID;

        [Parameter(Position = 16, Mandatory = false)]
        public string sshKeyID;

        [Parameter(Position = 17, Mandatory = false)]
        public string userData;

        [Parameter(Position = 18, Mandatory = true)]
        public string name;

        [Parameter(Position = 19, Mandatory = false)]
        public bool optimized;

        [Parameter(Position = 20, Mandatory = true)]
        public string state;

        #endregion

        protected override void ProcessRecord()
        {

            List<DataCenterPolicy> lstdataCenterPolicy = new List<DataCenterPolicy>();
            List<ElasticityParams> lstelasticityParams = new List<ElasticityParams>();
            List<Input> lstinputs = new List<Input>();
            List<string> lstsecurityGroupIDs = new List<string>();


            base.ProcessRecord();
            string createArray = RightScale.netClient.ServerArray.create(arrayType, lstdataCenterPolicy, deploymentID, description, lstelasticityParams, cloudID, 
                                                                            dataCenterID, lstinputs,instanceTypeID, imageID, kernelImageID, multiCloudImageID,
                                                                            ramdiskImageID, lstsecurityGroupIDs, serverTemplateID, sshKeyID, userData, name, optimized, state);

            WriteObject(createArray);

        }
    }

#endregion

}