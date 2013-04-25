using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RightScale.netClient.Test
{
    public class RSAPITestBase
    {
        protected string azureCloudID = "";
        protected string awsCloudID = "";
        protected string openStackCloudID = "";
        protected string rackSpaceOpenCloudID = "";
        protected string cloudStackCloudID = "";

        protected string authRefreshToken = "";
        protected string authUserName = "";
        protected string authPassword = "";
        protected string authAccountID = "";

        protected string liveTestServerID = "";
        protected string liveTestServerArrayID = "";
        protected string liveTestDeploymentID = "";

        public RSAPITestBase()
        {
            azureCloudID = ConfigurationManager.AppSettings["RightScaleAPI_AzureCloudID"].ToString();
            awsCloudID = ConfigurationManager.AppSettings["RightScaleAPI_AWSCloudID"].ToString();
            openStackCloudID = ConfigurationManager.AppSettings["RightScaleAPI_OpenStackCloudID"].ToString();
            rackSpaceOpenCloudID = ConfigurationManager.AppSettings["RightScaleAPI_RackSpaceOpenCloudID"].ToString();
            cloudStackCloudID = ConfigurationManager.AppSettings["RightScaleAPI_CloudStackCloudID"].ToString();

            authRefreshToken = ConfigurationManager.AppSettings["RightScaleAPI_AuthRefreshToken"].ToString();
            authAccountID = ConfigurationManager.AppSettings["RightScaleAPI_AuthAccountID"].ToString();
            authPassword = ConfigurationManager.AppSettings["RightScaleAPI_AuthPassword"].ToString();
            authUserName = ConfigurationManager.AppSettings["RightScaleAPI_AuthUserName"].ToString();

            liveTestServerID = ConfigurationManager.AppSettings["RightScaleAPI_LiveTestServerID"].ToString();
            liveTestServerArrayID = ConfigurationManager.AppSettings["RightScaleAPI_LiveTestServerArrayID"].ToString();
            liveTestDeploymentID = ConfigurationManager.AppSettings["RightScaleAPI_LiveTestDeploymentID"].ToString();
        }
    }
}
