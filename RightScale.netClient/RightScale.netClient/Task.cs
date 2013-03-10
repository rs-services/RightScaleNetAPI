using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Task:Core.RightScaleObjectBase<Task>
    {
        public string detail { get; set; }
        public string summary { get; set; }

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
            //TODO: need to implement process of building task list
            return null;
        }

        /// <summary>
        /// Medhod builds a task object based on the task href specified
        /// </summary>
        /// <param name="taskHref">Task Href to populate instance of task</param>
        /// <returns>Populated Task object</returns>
        public static Task GetTask(string taskHref)
        {
            //TODO: need to implement process of build task
            return null;
        }
		
    }
}
