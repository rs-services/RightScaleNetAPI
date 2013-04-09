using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class VolumeTypeTest
    {
        string servicesOAuthToken;
        string cloudStackID;
        string cloudStackVTID;
        string raxCloudID;

        public VolumeTypeTest()
        {
            servicesOAuthToken = ConfigurationManager.AppSettings["RightScaleServicesAPIRefreshToken"].ToString();
            cloudStackID = ConfigurationManager.AppSettings["VolumeType_cloudStackID"].ToString();
            cloudStackVTID = ConfigurationManager.AppSettings["VolumeType_cloudStackVTID"].ToString();
            raxCloudID = ConfigurationManager.AppSettings["VolumeType_raxID"].ToString();
        }

        #region VolumeType Relationships

        [TestMethod]
        public void VolumeTypeShowCloudStack()
        {
            netClient.Core.APIClient.Instance.InitWebClient();
            netClient.Core.APIClient.Instance.Authenticate(servicesOAuthToken);
            VolumeType vt = VolumeType.show(cloudStackID, cloudStackVTID);
            Assert.IsNotNull(vt);
            Cloud cl = vt.cloud;
            Assert.IsNotNull(cl);
            Assert.IsTrue(cl.name.Length > 0);
            netClient.Core.APIClient.Instance.InitWebClient(); //reclean auth on the instance
        }

        #endregion

        #region VolumeType.index tests

        [TestMethod]
        public void indexVolumeTypeSimple()
        {
            netClient.Core.APIClient.Instance.InitWebClient();
            netClient.Core.APIClient.Instance.Authenticate(servicesOAuthToken);
            List<VolumeType> volTypes = VolumeType.index(cloudStackID);
            Assert.IsNotNull(volTypes);
            Assert.IsTrue(volTypes.Count > 0);
            netClient.Core.APIClient.Instance.InitWebClient(); //reclean auth on the instance
        }

        [TestMethod]
        public void indexVolumeTypeRaxOpen()
        {
            netClient.Core.APIClient.Instance.InitWebClient();
            netClient.Core.APIClient.Instance.Authenticate(servicesOAuthToken);
            List<VolumeType> volTypes = VolumeType.index(raxCloudID);
            Assert.IsNotNull(volTypes);
            Assert.IsTrue(volTypes.Count > 0);
            netClient.Core.APIClient.Instance.InitWebClient(); //reclean auth on the instance
        }

        [TestMethod]
        public void indexVolumeTypeFiltered()
        {
            List<Filter> filter = new List<Filter>();
            filter.Add(new Filter("name", FilterOperator.Equal, "Large"));
            netClient.Core.APIClient.Instance.InitWebClient();
            netClient.Core.APIClient.Instance.Authenticate(servicesOAuthToken);
            List<VolumeType> allVolTypes = VolumeType.index(cloudStackID);
            Assert.IsNotNull(allVolTypes);
            Assert.IsTrue(allVolTypes.Count > 0);
            List<VolumeType> volTypes = VolumeType.index(cloudStackID, filter);
            Assert.IsNotNull(volTypes);
            Assert.IsTrue(volTypes.Count > 0);
            Assert.IsTrue(allVolTypes.Count > volTypes.Count);
            netClient.Core.APIClient.Instance.InitWebClient(); //reclean auth on the instance
        }

        [TestMethod]
        public void indexVolumeTypeView()
        {
            netClient.Core.APIClient.Instance.InitWebClient();
            netClient.Core.APIClient.Instance.Authenticate(servicesOAuthToken);
            List<VolumeType> volTypes = VolumeType.index(cloudStackID, "default");
            Assert.IsNotNull(volTypes);
            Assert.IsTrue(volTypes.Count > 0);
            netClient.Core.APIClient.Instance.InitWebClient(); //reclean auth on the instance
        }

        [TestMethod]
        public void indexVolumeTypeFilterView()
        {
            
            List<Filter> filter = new List<Filter>();
            filter.Add(new Filter("name", FilterOperator.Equal, "Large"));
            netClient.Core.APIClient.Instance.InitWebClient();
            netClient.Core.APIClient.Instance.Authenticate(servicesOAuthToken);
            List<VolumeType> allVolTypes = VolumeType.index(cloudStackID);
            Assert.IsNotNull(allVolTypes);
            Assert.IsTrue(allVolTypes.Count > 0);
            List<VolumeType> volTypes = VolumeType.index(cloudStackID, filter, "default");
            Assert.IsNotNull(volTypes);
            Assert.IsTrue(volTypes.Count > 0);
            Assert.IsTrue(allVolTypes.Count > volTypes.Count);
            netClient.Core.APIClient.Instance.InitWebClient(); //reclean auth on the instance
        }

        #endregion

        #region VolumeType.show tests

        [TestMethod]
        public void showVolumeType()
        {
            netClient.Core.APIClient.Instance.InitWebClient();
            netClient.Core.APIClient.Instance.Authenticate(servicesOAuthToken);
            VolumeType vt = VolumeType.show(cloudStackID, cloudStackVTID);
            Assert.IsNotNull(vt);
            netClient.Core.APIClient.Instance.InitWebClient(); //reclean auth on the instance
        }

        #endregion
    }
}
