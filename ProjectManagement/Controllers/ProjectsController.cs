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
    public class ProjectsController : ApiController
    {
        private ProjectLogic projectBL = new ProjectLogic();

        // GET: api/Projects
        public List<ProjectViewModel> GetProjects()
        {
            return projectBL.GetProjects();
        }

        // GET: api/Projects/5
        [ResponseType(typeof(Projects))]
        public IHttpActionResult GetProjects(int id)
        {
            ProjectViewModel project = null;
            try
            {
                project = projectBL.GetProjects(id);
                if (project == null) return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(project);
        }

        // PUT: api/Projects/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProjects(int id, Projects projects)
        {
            try
            {
               projectBL.PutProjects(id, projects);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Projects
        [ResponseType(typeof(Projects))]
        public IHttpActionResult PostProjects(Projects projects)
        {
            try
            {
                projectBL.PostProjects(projects);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return CreatedAtRoute("DefaultApi", new { id = projects.Project_ID }, projects);
        }

        // DELETE: api/Projects/5
        [ResponseType(typeof(Projects))]
        public IHttpActionResult DeleteProjects(int id)
        {
            Projects project = null;
            try
            {
                project = projectBL.DeleteProjects(id);
                if (project == null) return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(project);
        }

        
    }
}