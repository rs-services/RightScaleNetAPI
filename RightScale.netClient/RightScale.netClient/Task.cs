using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// Tasks represent processes that happen (or have happened) asynchronously within the context of an Instance.
    /// An example of a type of task is an operational script that runs in an instance.
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeTask.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceTasks.html
    /// </summary>
    public class Task:Core.RightScaleObjectBase<Task>
    {
        #region Task Properties

        /// <summary>
        /// Detail info for this Task
        /// </summary>
        public string detail { get; set; }

        /// <summary>
        /// Summary info for this Task
        /// </summary>
        public string summary { get; set; }

        #endregion

        #region Task.ctor
        /// <summary>
        /// Default Constructor for Task
        /// </summary>
        public Task()
            : base()
        {
        }

        /// <summary>
        /// Method refreshes this instance of Task with the latest status
        /// </summary>
        public bool Refresh()
        {
            return Refresh("default");
        }

        public bool Refresh(string view)
        {
            bool retVal = false;

            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default", "extended" };
                Utility.CheckStringInput("view", validViews, view);
            }

            string queryString = string.Format("view={0}", view);
            string jsonString = Core.APIClient.Instance.Get(getLinkValue("self"), queryString);
            Newtonsoft.Json.JsonConvert.PopulateObject(jsonString, this);
            retVal = true;

            return retVal;
        }

        #endregion

        /// <summary>
        /// Method builds a collection of Task objects based on a list of Task Hrefs
        /// </summary>
        /// <param name="taskHrefs">List of task hrefs</param>
        /// <returns>list of Task objects</returns>
        public static List<Task> show(List<string> taskHrefs)
        {
            List<Task> retVal = new List<Task>();
            foreach (string href in taskHrefs)
            {
                retVal.Add(show(href));
            }
            return retVal;
        }

        /// <summary>
        /// Medhod builds a task object based on the task href specified
        /// </summary>
        /// <param name="taskHref">Task Href to populate instance of task</param>
        /// <returns>Populated Task object</returns>
        public static Task show(string taskHref)
        {

            string jsonString = Core.APIClient.Instance.Get(taskHref);
            return deserialize(jsonString);
        }

        /// <summary>
        /// Medhod builds a task object based on the task href specified
        /// </summary>
        /// <param name="taskHref">Task Href to populate instance of task</param>
        /// <param name="view"></param>
        /// <returns>Populated Task object</returns>
        public static Task show(string taskHref, string view)
        {
            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default", "extended" };
                Utility.CheckStringInput("view", validViews, view);
            }

            string queryString = string.Format("view={0}", view);
            string jsonString = Core.APIClient.Instance.Get(taskHref, queryString);
            return deserialize(jsonString);
        }

        /// <summary>
        /// Method builds a collection of Task objects based on a list of Task Hrefs
        /// </summary>
        /// <param name="cloudID">Cloud ID of Tasks to query</param>
        /// <param name="instanceID">Instance ID of tasks to query</param>
        /// <param name="view">view of task to return</param>
        /// <returns>list of Task objects</returns>
        public static List<Task> show(string cloudID, string instanceID, string view)
        {
            string getUrl = string.Format(APIHrefs.InstanceTasks,cloudID,instanceID);
            string queryString = string.Empty;

            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default", "extended" };
                Utility.CheckStringInput("view", validViews, view);
            }

            queryString += string.Format("view={0}", view);

            string jsonString = Core.APIClient.Instance.Get(getUrl, queryString);
            return deserializeList(jsonString);
        }
    }
}
