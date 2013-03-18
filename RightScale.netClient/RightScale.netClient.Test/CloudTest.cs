using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Collections.Generic;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class CloudTest
    {
        string awsCloudID;
        string azureCloudID;
        string openstackCloudID;

        public CloudTest()
        {
            awsCloudID = ConfigurationManager.AppSettings["CloudTest_awsCloudID"].ToString();
            azureCloudID = ConfigurationManager.AppSettings["CloudTest_azureCloudID"].ToString();
            openstackCloudID = ConfigurationManager.AppSettings["CloudTest_openstackCloudID"].ToString();
        }

        #region Cloud Relationships tests

        [TestMethod]
        public void CloudDataCenters()
        {
            Cloud azureCloud = Cloud.show(azureCloudID);
            Assert.IsNotNull(azureCloud);
            List<DataCenter> dcs = azureCloud.datacenters;
            Assert.IsNotNull(dcs);
        }

        [TestMethod]
        public void AzureVolumeSnapshots()
        {
            Cloud azureCloud = Cloud.show(azureCloudID);
            Assert.IsNotNull(azureCloud);
            List<VolumeSnapshot> vss = azureCloud.volumeSnapshots;
            Assert.IsNotNull(vss);
        }

        [TestMethod]
        public void AzureInstances()
        {
            Cloud azureCloud = Cloud.show(azureCloudID);
            Assert.IsNotNull(azureCloud);
            List<Instance> instances = azureCloud.instances;
            Assert.IsNotNull(instances);
            Assert.IsTrue(instances.Count > 0);
        }

        [TestMethod]
        public void AzureVolumeTypes()
        {
            Cloud azureCloud = Cloud.show(azureCloudID);
            Assert.IsNotNull(azureCloud);
            List<VolumeType> vts = azureCloud.volumeTypes;
            Assert.IsNotNull(vts);
            Assert.IsTrue(vts.Count > 0);
        }

        [TestMethod]
        public void AzureSSHKeys()
        {
            Cloud azureCloud = Cloud.show(azureCloudID);
            Assert.IsNotNull(azureCloud);
            List<SshKey> sshs = azureCloud.sshKeys;
            Assert.IsNotNull(sshs);
            Assert.IsTrue(sshs.Count == 0); //no ssh keys in azure?
        }

        [TestMethod]
        public void OpenStackSSHKeys()
        {
            Cloud openStackCloud = Cloud.show(openstackCloudID);
            Assert.IsNotNull(openStackCloud);
            List<SshKey> sshs = openStackCloud.sshKeys;
            Assert.IsNotNull(sshs);
            Assert.IsTrue(sshs.Count > 0);//openstack does have ssh keys
        }

        [TestMethod]
        public void AzureRecurringVolumeAttachments()
        {
            Cloud azureCloud = Cloud.show(azureCloudID);
            Assert.IsNotNull(azureCloud);
            List<RecurringVolumeAttachment> rvas = azureCloud.recurringVolumeAttachments;
            Assert.IsNotNull(rvas);
        }

        [TestMethod]
        public void AzureVolumeAttachments()
        {
            Cloud azureCloud = Cloud.show(azureCloudID);
            Assert.IsNotNull(azureCloud);
            List<VolumeAttachment> vas = azureCloud.volumeAttachments;
            Assert.IsNotNull(vas);
        }

        [TestMethod]
        public void AzureVolumes()
        {
            Cloud azureCloud = Cloud.show(azureCloudID);
            Assert.IsNotNull(azureCloud);
            List<Volume> vols = azureCloud.volumes;
            Assert.IsNotNull(vols);
            Assert.IsTrue(vols.Count > 0);
        }

        [TestMethod]
        public void AzureIPAddressBindings()
        {
            Cloud azureCloud = Cloud.show(azureCloudID);
            Assert.IsNotNull(azureCloud);
            List<IPAddressBinding> ipab = azureCloud.ipAddressBindings;
            Assert.IsNotNull(ipab);
        }

        [TestMethod]
        public void OpenStackIPAddressBindings()
        {
            Cloud openStackCloud = Cloud.show(openstackCloudID);
            Assert.IsNotNull(openStackCloud);
            List<IPAddressBinding> ipab = openStackCloud.ipAddressBindings;
            Assert.IsNotNull(ipab);
            Assert.IsTrue(ipab.Count > 0);
        }

        [TestMethod]
        public void AzureImages()
        {
            Cloud azureCloud = Cloud.show(azureCloudID);
            Assert.IsNotNull(azureCloud);
            List<Image> images = azureCloud.images;
            Assert.IsNotNull(images);
            Assert.IsTrue(images.Count > 0);
        }

        [TestMethod]
        public void AzureInstanceTypes()
        {
            Cloud azureCloud = Cloud.show(azureCloudID);
            Assert.IsNotNull(azureCloud);
            List<InstanceType> its = azureCloud.instanceTypes;
            Assert.IsNotNull(its);
            Assert.IsTrue(its.Count > 0);
        }

        [TestMethod]
        public void AzureIPAddresses()
        {
            Cloud azureCloud = Cloud.show(azureCloudID);
            Assert.IsNotNull(azureCloud);
            List<IPAddress> ipad = azureCloud.ipAddresses;
            Assert.IsNotNull(ipad);
            Assert.IsTrue(ipad.Count == 0);
        }

        [TestMethod]
        public void OpenStackIPAddresses()
        {
            Cloud openStackCloud = Cloud.show(openstackCloudID);
            Assert.IsNotNull(openStackCloud);
            List<IPAddress> ipad = openStackCloud.ipAddresses;
            Assert.IsNotNull(ipad);
            Assert.IsTrue(ipad.Count > 0);
        }

        #endregion

        #region Cloud.show tests

        [TestMethod]
        public void AzureCloudShowTest()
        {
            Cloud azureCloud = Cloud.show(azureCloudID);
            Assert.IsNotNull(azureCloud);
        }

        [TestMethod]
        public void OpenStackCloudShowTest()
        {
            Cloud openstackCloud = Cloud.show(openstackCloudID);
            Assert.IsNotNull(openstackCloud);
        }

        #endregion

        #region Cloud Relationships tests

        [TestMethod]
        public void cloudDatacentersExist()
        {
            Cloud azureCloud = Cloud.show(azureCloudID);
            Assert.IsNotNull(azureCloud);
            List<DataCenter> datacenters = azureCloud.datacenters;
            Assert.IsTrue(datacenters.Count == 0);
        }

        [TestMethod]
        public void cloudVolumeSnapshotsExist()
        {
            
            Cloud azureCloud = Cloud.show(azureCloudID);
            Assert.IsNotNull(azureCloud);
            List<VolumeSnapshot> volSnaps = azureCloud.volumeSnapshots;
            Assert.IsTrue(volSnaps.Count == 0);
        }

        #endregion

    }
}
