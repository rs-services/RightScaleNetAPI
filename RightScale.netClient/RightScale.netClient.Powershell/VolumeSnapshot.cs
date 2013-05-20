using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;

namespace RightScale.netClient.Powershell
{
    #region volumesnapshot index show cmdlets
    [Cmdlet(VerbsCommon.Get, "RSVolumeSnapShots")]
    public class volumesnapshots_index : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string cloudID;

        [Parameter(Position = 2, Mandatory = false)]
        public string filter;

        [Parameter(Position = 3, Mandatory = false)]
        public string view = "default";

        protected override void ProcessRecord()
        {
            List<Filter> lstFilter = new List<Filter>();

            if (filter != null)
            {
                Filter fltFilter = Filter.parseFilter(filter);
                lstFilter.Add(fltFilter);
            }

            base.ProcessRecord();

            Types.returnVolume retResult = new Types.returnVolume();

            try
            {
                
                    List<VolumeSnapshot> rsVolumeSnapshots = RightScale.netClient.VolumeSnapshot.index(cloudID, lstFilter, view);
                    WriteObject(rsVolumeSnapshots);
            }
            catch (RightScaleAPIException rex)
            {
                retResult.Message = "Fail";
                retResult.Details = rex.ErrorData;
                retResult.APIHref = rex.APIHref;
                retResult.Result = false;

                WriteObject(retResult);
            }
        }
    }
    #endregion

    #region volume create / destroy cmdlets
    [Cmdlet(VerbsCommon.New, "RSVolumeSnapshot")]
    public class volumesnapshots_create : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string cloudID;

        [Parameter(Position = 2, Mandatory = true)]
        public string name;

        [Parameter(Position = 3, Mandatory = false)]
        public string datacenterid;

        [Parameter(Position = 4, Mandatory = false)]
        public string iops;

        [Parameter(Position = 5, Mandatory = false)]

        [Parameter(Position = 6, Mandatory = false)]
        public string description;

        [Parameter(Position = 7, Mandatory = false)]
        public string parentvolumeid;

        [Parameter(Position = 8, Mandatory = false)]
        public string parentvolumesnapshotid;

        [Parameter(Position = 9, Mandatory = false)]
        public string size;

        [Parameter(Position = 10, Mandatory = false)]
        public string volumetypeid;

        protected override void ProcessRecord()
        {
            Types.returnVolumeCreate result = new Types.returnVolumeCreate();

            base.ProcessRecord();

            try
            {
                string rsVolumeID = RightScale.netClient.Volume.create(cloudID, name, datacenterid, description, iops, parentvolumeid, parentvolumesnapshotid, size, volumetypeid);

                if (rsVolumeID != "")
                {
                    result.VolumeID = rsVolumeID;
                    result.Message = "Volume created";
                    result.Result = true;
                    result.DatacenterID = datacenterid;
                    result.Description = description;
                    result.Iops = iops;
                    result.ParentVolumeID = parentvolumeid;
                    result.ParentVolumeSnapshotID = parentvolumesnapshotid;
                    result.Size = size;
                    result.VolumeTypeID = volumetypeid;

                    WriteObject(result);
                }
                else
                {
                    result.VolumeID = rsVolumeID;
                    result.Message = "Error creating volume";
                    result.Result = false;
                    result.DatacenterID = datacenterid;
                    result.Description = description;
                    result.Iops = iops;
                    result.ParentVolumeID = parentvolumeid;
                    result.ParentVolumeSnapshotID = parentvolumesnapshotid;
                    result.Size = size;
                    result.VolumeTypeID = volumetypeid;

                    WriteObject(result);
                }
            }
            catch (RightScaleAPIException errNewVol)
            {
                result.VolumeID = "";
                result.Message = "Error creating volume - " + errNewVol.InnerException;
                result.Result = false;
                result.DatacenterID = datacenterid;
                result.Description = description;
                result.Iops = iops;
                result.ParentVolumeID = parentvolumeid;
                result.ParentVolumeSnapshotID = parentvolumesnapshotid;
                result.Size = size;
                result.VolumeTypeID = volumetypeid;

                WriteObject(result);
            }
        }
    }
    #endregion
}