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
    public class ProjectsController : ApiController
    {
        private ProjectManagementContext db = new ProjectManagementContext();

        // GET: api/Projects
        public List<ProjectViewModel> GetProjects()
        {
            List<Projects> projects = db.Projects.ToList();

            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<Projects, ProjectViewModel>().ForMember(destination => destination.NumberOfTasks,

               opts => opts.MapFrom(source => db.Tasks.Where(x => x.Project_ID == source.Project_ID).Count())).ForMember(destination => destination.StartDate,

               opts => opts.MapFrom(source => source.StartDate.Value.ToString("yyyy-MM-dd")))
               .ForMember(destination => destination.EndDate,

               opts => opts.MapFrom(source => source.EndDate.Value.ToString("yyyy-MM-dd"))); ;

            });

            IMapper iMapper = config.CreateMapper();


            List<ProjectViewModel> ProjectView = iMapper.Map<List<Projects>, List<ProjectViewModel>>(projects);
            return ProjectView;
        }

        // GET: api/Projects/5
        [ResponseType(typeof(Projects))]
        public IHttpActionResult GetProjects(int id)
        {
            Projects projects = db.Projects.Find(id);
          
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<Projects, ProjectViewModel>().ForMember(destination => destination.NumberOfTasks,

               opts => opts.MapFrom(source => db.Tasks.Where(x=> x.Project_ID==source.Project_ID).Count())).ForMember(destination => destination.StartDate,

               opts => opts.MapFrom(source => source.StartDate.Value.ToString("yyyy-MM-dd")))
               .ForMember(destination => destination.EndDate,

               opts => opts.MapFrom(source => source.EndDate.Value.ToString("yyyy-MM-dd"))); ;

            });

            IMapper iMapper = config.CreateMapper();


            ProjectViewModel ProjectView = iMapper.Map<Projects, ProjectViewModel>(projects);
            if (ProjectView == null)
            {
                return NotFound();
            }

            return Ok(ProjectView);
        }

        // PUT: api/Projects/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProjects(int id, Projects projects)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projects.Project_ID)
            {
                return BadRequest();
            }

            db.Entry(projects).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectsExists(id))
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

        // POST: api/Projects
        [ResponseType(typeof(Projects))]
        public IHttpActionResult PostProjects(Projects projects)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Projects.Add(projects);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = projects.Project_ID }, projects);
        }

        // DELETE: api/Projects/5
        [ResponseType(typeof(Projects))]
        public IHttpActionResult DeleteProjects(int id)
        {
            Projects projects = db.Projects.Find(id);
            if (projects == null)
            {
                return NotFound();
            }
            List<Tasks> lTask = db.Tasks.Where(z => z.Project_ID == projects.Project_ID).ToList();
            db.Tasks.RemoveRange(lTask);
            db.Projects.Remove(projects);
            db.SaveChanges();

            return Ok(projects);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectsExists(int id)
        {
            return db.Projects.Count(e => e.Project_ID == id) > 0;
        }
    }
}