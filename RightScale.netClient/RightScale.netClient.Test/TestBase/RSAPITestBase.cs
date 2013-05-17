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
        protected string eucaCloudID = "";
        protected string awsUSEastCloudID = "";

        protected string authRefreshToken = "";
        protected string authUserName = "";
        protected string authPassword = "";
        protected string authAccountID = "";

        protected string liveTestServerID = "";
        protected string liveTestServerArrayID = "";
        protected string liveTestDeploymentID = "";

        public RSAPITestBase()
        {
            this.azureCloudID = ConfigurationManager.AppSettings["RightScaleAPI_AzureCloudID"].ToString();
            this.awsCloudID = ConfigurationManager.AppSettings["RightScaleAPI_AWSCloudID"].ToString();
            this.openStackCloudID = ConfigurationManager.AppSettings["RightScaleAPI_OpenStackCloudID"].ToString();
            this.rackSpaceOpenCloudID = ConfigurationManager.AppSettings["RightScaleAPI_RackSpaceOpenCloudID"].ToString();
            this.cloudStackCloudID = ConfigurationManager.AppSettings["RightScaleAPI_CloudStackCloudID"].ToString();
            this.eucaCloudID = ConfigurationManager.AppSettings["RightScaleAPI_EucaCloudID"].ToString();
            this.awsUSEastCloudID = ConfigurationManager.AppSettings["RightScaleAPI_AWSUSEastID"].ToString();

            this.authRefreshToken = ConfigurationManager.AppSettings["RightScaleAPI_AuthRefreshToken"].ToString();
            this.authAccountID = ConfigurationManager.AppSettings["RightScaleAPI_AuthAccountID"].ToString();
            this.authPassword = ConfigurationManager.AppSettings["RightScaleAPI_AuthPassword"].ToString();
            this.authUserName = ConfigurationManager.AppSettings["RightScaleAPI_AuthUserName"].ToString();

            this.liveTestServerID = ConfigurationManager.AppSettings["RightScaleAPI_LiveTestServerID"].ToString();
            this.liveTestServerArrayID = ConfigurationManager.AppSettings["RightScaleAPI_LiveTestServerArrayID"].ToString();
            this.liveTestDeploymentID = ConfigurationManager.AppSettings["RightScaleAPI_LiveTestDeploymentID"].ToString();
        }
    }
}
