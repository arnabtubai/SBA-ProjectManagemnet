using ProjectManagement.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;

namespace ProjectMangement.Business
{
    public class UserLogic 
    {
        private ProjectManagementContext db = new ProjectManagementContext();
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public List<Users> GetUsers()
        {
            List<Users> objUser = null;
            try
            {
                objUser= db.Users.ToList();
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error while getting user list");
                throw ex;

            }
            finally
            {
                Dispose(true);
            }
            return objUser;
        }

      
        public Users GetUsers(int id)
        {
            Users users = null;

            try
            {
                users = db.Users.Find(id);
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error while getting user list");
                throw ex;
            }
            finally
            {
                Dispose(true);
            }

            return users;
        }


        public void PutUsers(int id, Users users)
        {

            try
            {
                db.Entry(users).State = EntityState.Modified;

          
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!UsersExists(id))
                {
                    logger.Info(ex, "UserId doesn't exists");
                    throw new Exception("Data Not found");
                }
                else
                {
                    logger.Info(ex, "UserId already exists");
                    throw ex;
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    logger.Info("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                       logger.Error("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw e;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error while updating user");
                throw ex;

            }
            finally
            {
                Dispose(true);
            }

        }

        // POST: api/Users
      
        public void PostUsers(Users users)
        {
            try
            {

                db.Users.Add(users);
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                logger.Info(ex, "error while adding");
                throw ex;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    logger.Info("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        logger.Error("- Property: \"{0}\", Error: \"{1}\"",
                             ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw e;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error while adding user");
                throw ex;

            }
            finally
            {
                Dispose(true);
            }
        }



        public Users DeleteUsers(int id)
        {
            Users users = null;
            try
            {
                users = db.Users.Find(id);

                List<Tasks> taskList = db.Tasks.Where(x => x.User_ID == users.User_ID).ToList();

                db.Tasks.RemoveRange(taskList);
                db.Users.Remove(users);
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                logger.Info(ex, "error while deleting");
                throw ex;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    logger.Info("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        logger.Error("- Property: \"{0}\", Error: \"{1}\"",
                             ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw e;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error while deleting user");
                throw ex;

            }
            finally
            {
                Dispose(true);
            }
            return users;
        }

        private  void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
          
        }

        private bool UsersExists(int id)
        {
            return db.Users.Count(e => e.User_ID == id) > 0;
        }
    }
}
