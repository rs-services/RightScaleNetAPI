﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class MultiCloudImageSetting
    {
        public List<Action> actions { get; set; }
        public List<Link> links { get; set; }

        
        #region MultiCloudImageSetting.index methods

        public static List<MultiCloudImageSetting> index()
        {
            return index(null);
        }

        public static List<MultiCloudImageSetting> index(List<KeyValuePair<string, string>> filter)
        {
            List<string> validFilters = new List<string>() { "cloud_href", "multi_cloud_image_href" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement MultiCloudImageSetting.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
