using System.Collections.Generic;
using System.Management.Automation;
using RightScale.netClient;

namespace RSPosh
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

        protected override void ProcessRecord()
        {
            Types.returnVolumeCreate result = new Types.returnVolumeCreate();

            base.ProcessRecord();

            try
            {
                string rsVolumeID = RightScale.netClient.Volume.create(cloudID, name);

                if (rsVolumeID != "")
                {
                    result.VolumeID = rsVolumeID;
                    result.Message = "Volume created";
                    result.Result = true;

                    WriteObject(result);
                }
                else
                {
                    result.VolumeID = rsVolumeID;
                    result.Message = "Error creating volume";
                    result.Result = false;

                    WriteObject(result);
                }
            }
            catch (RightScaleAPIException errNewVol)
            {
                result.VolumeID = "";
                result.Message = "Error creating volume - " + errNewVol.InnerException;
                result.Result = false;

                WriteObject(result);
            }
        }
    }
    #endregion

}