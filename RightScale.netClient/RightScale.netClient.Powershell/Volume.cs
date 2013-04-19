using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;

namespace RightScale.netClient.Powershell
{
    #region volume index / show cmdlets
    [Cmdlet(VerbsCommon.Get, "RSVolumes")]
    public class volumes_index : Cmdlet
    {
        [Parameter(Position = 1, Mandatory = true)]
        public string cloudID;

        [Parameter(Position = 2, Mandatory = false)]
        public string filter;

        [Parameter(Position = 3, Mandatory = false)]
        public string view;

        protected override void ProcessRecord()
        {
            if (view == null) { view = "default"; }

            List<Filter> lstFilter = new List<Filter>();

            if (filter != null)
            {
                Filter fltFilter = Filter.parseFilter(filter);
                lstFilter.Add(fltFilter);
            }

            base.ProcessRecord();

            List<Volume> rsVolumes = RightScale.netClient.Volume.index(cloudID, lstFilter, view);

            WriteObject(rsVolumes);

        }

        [Cmdlet(VerbsCommon.Get, "RSVolume")]
        public class volumes_show : Cmdlet
        {
            [Parameter(Position = 1, Mandatory = true)]
            public string cloudID;

            [Parameter(Position = 2, Mandatory = true)]
            public string volumeID;

            [Parameter(Position = 3, Mandatory = false)]
            public string view;

            protected override void ProcessRecord()
            {
                if (view == null) { view = "default"; }

                base.ProcessRecord();

                Volume rsVolume = RightScale.netClient.Volume.show(cloudID, volumeID, view);

                WriteObject(rsVolume);

            }
        }

    }
    #endregion

    #region volume create / destroy cmdlets
    [Cmdlet(VerbsCommon.New, "RSVolume")]
    public class volumes_create : Cmdlet
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