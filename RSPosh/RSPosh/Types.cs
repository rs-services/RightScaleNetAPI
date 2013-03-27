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
        }

    }
}
