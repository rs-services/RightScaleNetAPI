﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    //volume_snapshot
    public class VolumeSnapshot
    {
        public string name { get; set; }
        public List<Action> actions { get; set; }
        public string resource_uid { get; set; }
        public string created_at { get; set; }
        public string size { get; set; }
        public string updated_at { get; set; }
        public List<Link> links { get; set; }
        public string description { get; set; }
        public string state { get; set; }

        
        #region VolumeSnapshot.index methods

        public static List<VolumeSnapshot> index()
        {
            return index(null, null);
        }

        public static List<VolumeSnapshot> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        public static List<VolumeSnapshot> index(string view)
        {
            return index(null, view);
        }

        public static List<VolumeSnapshot> index(List<KeyValuePair<string, string>> filter, string view)
        {
            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default" };
                Utility.CheckStringInput("view", validViews, view);
            }

            List<string> validFilters = new List<string>() { "description", "name", "parent_volume_href", "resource_uid" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement VolumeSnapshot.index
            throw new NotImplementedException();
        }
        #endregion
		
    }
}
