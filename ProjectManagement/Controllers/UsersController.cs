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
    public class UsersController : ApiController
    {
        private UserLogic userBL = new UserLogic();

        // GET: api/Users
        public List<Users> GetUsers()
        {
             return userBL.GetUsers(); 
        }

        // GET: api/Users/5
        [ResponseType(typeof(Users))]
        public IHttpActionResult GetUsers(int id)
        {
            Users users = null;
            try
            {
                users = userBL.GetUsers(id);
                if (users == null) return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }

            return Ok(users);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsers(int id, Users users)
        {
            try
            {
                userBL.PutUsers( id, users);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(Users))]
        public IHttpActionResult PostUsers(Users users)
        {

            try {
                userBL.PostUsers(users);
            }
            catch(Exception)
            {
                return BadRequest();
            }

            return CreatedAtRoute("DefaultApi", new { id = users.User_ID }, users);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(Users))]
        public IHttpActionResult DeleteUsers(int id)
        {
            Users users = null;
            try
            {
                users = userBL.DeleteUsers(id);
                if(users == null) return NotFound();
            }
            catch(Exception)
            {
                return BadRequest();
            }

            return Ok(users);
        }

    }
}