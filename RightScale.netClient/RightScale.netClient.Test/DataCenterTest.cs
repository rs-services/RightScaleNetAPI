using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;

namespace RightScale.netClient.Test
{
    [TestClass]
    public class DataCenterTest : RSAPITestBase
    {
        string cloudID;
        string datacenterID;
        string servicesOauthToken;

        public DataCenterTest()
        {
            cloudID = ConfigurationManager.AppSettings["DataCenter_cloudID"].ToString();
            datacenterID = ConfigurationManager.AppSettings["DataCenter_datacenterID"].ToString();
            servicesOauthToken = ConfigurationManager.AppSettings["RightScaleServicesAPIRefreshToken"].ToString();
        }

        [TestMethod]
        public void DataCenterCloud()
        {
            netClient.Core.APIClient.Instance.InitWebClient();
            netClient.Core.APIClient.Instance.Authenticate(servicesOauthToken);

            DataCenter dc = DataCenter.show(cloudID, datacenterID);
            Assert.IsNotNull(dc);
            Cloud dcCloud = dc.cloud;
            Assert.IsNotNull(dcCloud);
            Assert.IsTrue(dcCloud.name.Length > 0);

            netClient.Core.APIClient.Instance.InitWebClient();
        }

        [TestMethod]
        public void datacenterIndex()
        {

            netClient.Core.APIClient.Instance.InitWebClient();
            netClient.Core.APIClient.Instance.Authenticate(servicesOauthToken);

            try
            {
                List<DataCenter> dcList = DataCenter.index(cloudID);
                Assert.IsNotNull(dcList);
                Assert.IsTrue(dcList.Count > 0);
            }
            catch (RightScaleAPIException rsae)
            {
                if (rsae.ErrorData.ToLower().StartsWith("unsupportedresource"))
                {
                    Assert.Inconclusive("Cloud tested does not support data centers");
                }
                else
                {
                    Assert.Fail(rsae.Message + Environment.NewLine + rsae.ErrorData);
                }
            }

            netClient.Core.APIClient.Instance.InitWebClient();
        }
    }
}
