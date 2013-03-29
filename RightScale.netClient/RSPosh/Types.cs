using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSPosh
{
    class Types
    {

        public class returnDeployment
        {
            public string DeploymentID { get; set; }
            public bool Result { get; set; }
            public string Message { get; set; }
        }

        public class returnServer
        {
            public string DeploymentID { get; set; }
            public string ServerID { get; set; }
            public string ServerTemplateID { get; set; }
            public bool Result { get; set; }
            public string Message { get; set; }
        }

        public class returnServerLaunch
        {
            public string ServerID { get; set; }
            public bool Result { get; set; }
            public string Message { get; set; }
        }

        public class returnVolumeCreate
        {
            public string VolumeID { get; set; }
            public bool Result { get; set; }
            public string Message { get; set; }
            public string DatacenterID { get; set; }
            public string Iops { get; set; }
            public string Description { get; set; }
            public string ParentVolumeID { get; set; }
            public string ParentVolumeSnapshotID { get; set; }
            public string Size { get; set; }
            public string VolumeTypeID { get; set; }
        }

        public class returnServerTemplateCreate
        {
            public string ServerTemplateID { get; set; }
            public bool Result { get; set; }
            public string Message { get; set; }
            public string MessageData { get; set; }
        }

        public class returnServerTemplateClone
        {
            public string ServerTemplateID { get; set; }
            public string ServerTemplateName { get; set; }
            public bool Result { get; set; }
            public string Description { get; set; }
            public string Message { get; set; }
            public string MessageData { get; set; }
        }

        public class returnServerTemplateDestroy
        {
            public string ServerTemplateID { get; set; }
            public string ServerTemplateName { get; set; }
            public bool Result { get; set; }
            public string Description { get; set; }
            public string Message { get; set; }
            public string MessageData { get; set; }
        }
    }
}
