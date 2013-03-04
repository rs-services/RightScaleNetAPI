﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class SecurityGroup
    {
        public string name { get; set; }
        public List<Action> actions { get; set; }
        public string resource_uid { get; set; }
        public List<Link> links { get; set; }

        /* TODO:
         * There's a discrepancy between how SG's are returned from other objects and the full version of the object itself
         * we need to figure out how to handle the 'tiny' response that's returned in some cases
         */

        #region object.index methods

        public static List<object> index()
        {
            return index(null, null);
        }

        public static List<object> index(List<KeyValuePair<string, string>> filter)
        {
            return index(filter, null);
        }

        public static List<object> index(string view)
        {
            return index(null, view);
        }

        public static List<object> index(List<KeyValuePair<string, string>> filter, string view)
        {
            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default", "tiny" };
                Utility.CheckStringInput("view", validViews, view);
            }

            List<string> validFilters = new List<string>() { "name", "resource_uid" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            //TODO: implement object.index
            throw new NotImplementedException();
        }
        #endregion
    }
}
