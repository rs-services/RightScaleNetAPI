using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient.Powershell
{
    class Types
    {

        public class returnDeployment
        {
            public string DeploymentID { get; set; }
            public bool Result { get; set; }
            public string Message { get; set; }
            public string Details { get; set; }
            public string APIHref { get; set; }
        }

        public class returnDeploymentClone
        {
            public string DeploymentID { get; set; }
            public string CloneID { get; set; }
            public bool Result { get; set; }
            public string Message { get; set; }
            public string Details { get; set; }
            public string APIHref { get; set; }
        }

        public class returnDeploymentUpdate
        {
            public string DeploymentID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string ServerTagScope { get; set; }
            public bool Result { get; set; }
            public string Message { get; set; }
            public string Details { get; set; }
            public string APIHref { get; set; }
        }

        public class returnDeploymentServers
        {
            public string DeploymentID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string ServerTagScope { get; set; }
            public bool Result { get; set; }
            public string Message { get; set; }
            public string Details { get; set; }
            public string APIHref { get; set; }
        }

        public class returnServer
        {
            public string DeploymentID { get; set; }
            public string ServerID { get; set; }
            public string ServerTemplateID { get; set; }
            public bool Result { get; set; }
            public string Message { get; set; }
            public string ErrData { get; set; }
            public string APIHref { get; set; }
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

        public class returnServerTemplateUpdate
        {
            public string ServerTemplateID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Result { get; set; }
            public string Message { get; set; }
            public string Details { get; set; }
            public string APIHref { get; set; }
        }

        public class returnServerTemplateCommit
        {
            public string ServerTemplateID { get; set; }
            public string ServerTemplateCommittedID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Result { get; set; }
            public string Message { get; set; }
            public string Details { get; set; }
            public string APIHref { get; set; }
        }

        public class returnServerTemplatePublish
        {
            public string ServerTemplateID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Result { get; set; }
            public string Message { get; set; }
            public string Details { get; set; }
            public string APIHref { get; set; }
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


        public class returnRebootServer
        {
            public string ServerID { get; set; }
            public bool Result { get; set; }
            public string Message { get; set; }
            public string Details { get; set; }
            public string APIHref { get; set; }
        }

        public class returnRebootInstance
        {
            public string CloudID { get; set; }
            public string InstanceID { get; set; }
            public bool Result { get; set; }
            public string Message { get; set; }
            public string Details { get; set; }
            public string APIHref { get; set; }
        }
        //consolidated the class for all instance actions
        public class returnInstanceAction
        {
            public string ActionType { get; set; }
            public string CloudID { get; set; }
            public string InstanceID { get; set; }
            public bool Result { get; set; }
            public string Message { get; set; }
            public string Details { get; set; }
            public string APIHref { get; set; }
            //The values below were added for "update" to work 
            public string name { get; set; }
            public string instanceTypeID { get; set; }
            public string serverTemplateID { get; set; }
            public string multiCloudImageID { get; set; }
            public string[] securityGroupIDs { get; set; }
            public string dataCenterID { get; set; }
            public string imageID { get; set; }
            public string kernelImageID { get; set; }
            public string ramdiskImageID{ get; set; }
            public string sshKeyID { get; set; }
            public string userData { get; set; }
            public List<Filter> filter { get; set;}
        }

        public class returnTagAction
        {
            public string tagAction { get; set; }
            public string href { get; set; }
            public string tag { get; set; }
            public bool Result { get; set; }
            public string Message { get; set; }
            public string Details { get; set; }
            public string APIHref { get; set; }
<<<<<<< HEAD
=======

>>>>>>> 3b201824de02a7a802aefb1ff4f57a61b48cc067
        }

        public class returnVolume
        {
            public string VolumeID { get; set; }
            public string CloudID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Result { get; set; }
            public string Message { get; set; }
            public string Details { get; set; }
            public string APIHref { get; set; }
        }
<<<<<<< HEAD
=======

>>>>>>> 3b201824de02a7a802aefb1ff4f57a61b48cc067



        }

<<<<<<< HEAD

    }
=======


        }

    
>>>>>>> 3b201824de02a7a802aefb1ff4f57a61b48cc067

