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
using AutoMapper;
using ProjectManagement.Models;

namespace ProjectManagement.Controllers
{
    public class TasksController : ApiController
    {
        private ProjectManagementContext db = new ProjectManagementContext();

        // GET: api/Tasks
        public List<TaskViewModel> GetTasks()
        {
            List<Tasks> Tasks = db.Tasks.ToList();
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<Tasks, TaskViewModel>().ForMember(destination => destination.ParentTask,

               opts => opts.MapFrom(source => db.Tasks.Find(source.Parent_ID).Task))
               .ForMember(destination => destination.StartDate,

               opts => opts.MapFrom(source =>source.StartDate.Value.ToString("yyyy-MM-dd")))
               .ForMember(destination => destination.EndDate,

               opts => opts.MapFrom(source => source.EndDate.Value.ToString("yyyy-MM-dd"))); ;

            });

            IMapper iMapper = config.CreateMapper();


            List<TaskViewModel> TaskView = iMapper.Map <List<Tasks>, List<TaskViewModel> >(Tasks);
          
            return TaskView;
        }

        // GET: api/Tasks/5
        [ResponseType(typeof(Tasks))]
        public IHttpActionResult GetTasks(int id)
        {
            Tasks Tasks = db.Tasks.Find(id);
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<Tasks, TaskViewModel>().ForMember(destination => destination.ParentTask,

               opts => opts.MapFrom(source =>db.Tasks.Find(source.Parent_ID).Task)).ForMember(destination => destination.StartDate,

               opts => opts.MapFrom(source => source.StartDate.Value.ToString("yyyy-MM-dd")))
               .ForMember(destination => destination.EndDate,

               opts => opts.MapFrom(source => source.EndDate.Value.ToString("yyyy-MM-dd"))); ;

            });

            IMapper iMapper = config.CreateMapper();


            TaskViewModel TaskView = iMapper.Map<Tasks, TaskViewModel>(Tasks);
            if (TaskView == null)
            {
                return NotFound();
            }

            return Ok(TaskView);
        }

        // PUT: api/Tasks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTasks(int id, Tasks Tasks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Tasks.Task_ID)
            {
                return BadRequest();
            }

            db.Entry(Tasks).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TasksExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tasks
        [ResponseType(typeof(Tasks))]
        public IHttpActionResult PostTasks(Tasks Tasks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tasks.Add(Tasks);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Tasks.Task_ID }, Tasks);
        }

        // DELETE: api/Tasks/5
        [ResponseType(typeof(Tasks))]
        public IHttpActionResult DeleteTasks(int id)
        {
            Tasks Tasks = db.Tasks.Find(id);
            if (Tasks == null)
            {
                return NotFound();
            }

            db.Tasks.Remove(Tasks);
            db.SaveChanges();

            return Ok(Tasks);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TasksExists(int id)
        {
            return db.Tasks.Count(e => e.Task_ID == id) > 0;
        }
    }
}