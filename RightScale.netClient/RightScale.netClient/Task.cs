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
        /// Constructor for Task object that takes in an oAuth Refresh token for RSAPI Authentication purposes
        /// </summary>
        /// <param name="oAuthRefreshToken">RightScale OAuth Refresh Token</param>
        public Task(string oAuthRefreshToken)
            : base(oAuthRefreshToken)
        {
        }

        /// <summary>
        /// Cosntructor for Task object that takes username, password and accountno for RSAPI Authentication purposes
        /// </summary>
        /// <param name="userName">RightScale user name</param>
        /// <param name="password">RightScale user password</param>
        /// <param name="accountNo">RightScale account to be accessed programmatically</param>
        public Task(string userName, string password, string accountNo)
            : base(userName, password, accountNo)
        {
        }

        /// <summary>
        /// Method refreshes this instance of Task with the latest status
        /// </summary>
        public void Refresh()
        {
            //TODO: need to implement object instance-based update process
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Method builds a collection of Task objects based on a list of Task Hrefs
        /// </summary>
        /// <param name="taskHrefs">List of task hrefs</param>
        /// <returns>list of Task objects</returns>
        public static List<Task> GetTaskList(List<string> taskHrefs)
        {
            List<Task> retVal = new List<Task>();
            foreach (string href in taskHrefs)
            {
                retVal.Add(GetTask(href));
            }
            return retVal;
        }

        /// <summary>
        /// Medhod builds a task object based on the task href specified
        /// </summary>
        /// <param name="taskHref">Task Href to populate instance of task</param>
        /// <returns>Populated Task object</returns>
        public static Task GetTask(string taskHref)
        {
<<<<<<< HEAD
            
            //TODO: need to implement process of build task
            return null;
=======
            string jsonString = Core.APIClient.Instance.Get(taskHref);
            return deserialize(jsonString);
>>>>>>> 1ab0ffc19397b228eb49a024e33d92970e119dec
        }

        public static List<Task> GetTasks(string cloudID, string instanceID, string view)
        {
            string getUrl = string.Format("/api/clouds/{0}/instances/{1}/live/tasks",cloudID,instanceID);
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
