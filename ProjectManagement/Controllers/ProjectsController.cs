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
using ProjectManagement.Models;

namespace ProjectManagement.Controllers
{
    public class ProjectsController : ApiController
    {
        private ProjectManagementContext db = new ProjectManagementContext();

        // GET: api/Projects
        public IQueryable<Projects> GetProjects()
        {
            return db.Projects;
        }

        // GET: api/Projects/5
        [ResponseType(typeof(Projects))]
        public IHttpActionResult GetProjects(int id)
        {
            Projects projects = db.Projects.Find(id);
            if (projects == null)
            {
                return NotFound();
            }

            return Ok(projects);
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