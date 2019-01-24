using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ProjectManagement.Entity;
using ProjectMangement.Business;


namespace ProjectManagement.Controllers
{
    public class TasksController : ApiController
    {
        private TaskLogic taskBL = new TaskLogic();

        // GET: api/Tasks
        public List<TaskViewModel> GetTasks()
        {
            return taskBL.GetTasks();
        }

        // GET: api/Tasks/5
        [ResponseType(typeof(Tasks))]
        public IHttpActionResult GetTasks(int id)
        {
            TaskViewModel task = null;
            try
            {
                task = taskBL.GetTasks(id);
                if (task == null) return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(task);
        }

        // PUT: api/Tasks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTasks(int id, Tasks Tasks)
        {
            try
            {
                taskBL.PutTasks(id, Tasks);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tasks
        [ResponseType(typeof(Tasks))]
        public IHttpActionResult PostTasks(Tasks Tasks)
        {
            try
            {
                taskBL.PostTasks(Tasks);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return CreatedAtRoute("DefaultApi", new { id = Tasks.Task_ID }, Tasks);
        }

        // DELETE: api/Tasks/5
        [ResponseType(typeof(Tasks))]
        public IHttpActionResult DeleteTasks(int id)
        {
            Tasks tasks = null;
            try
            {
                tasks =taskBL.DeleteTasks(id);
                if (tasks == null) return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(tasks);
        }

       
    }
}