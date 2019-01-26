using AutoMapper;
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

namespace ProjectMangement.Business
{
    public class TaskLogic
    {
        private ProjectManagementContext db = new ProjectManagementContext();
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public TaskLogic() { }

        public TaskLogic(ProjectManagementContext obj)
        {
            db = obj;
        }
        // GET: api/Tasks
        public List<TaskViewModel> GetTasks()
        {
            List<TaskViewModel> TaskView = null;
            try
            {
                List<Tasks> Tasks = db.Tasks.ToList();
                var config = new MapperConfiguration(cfg =>
                {

                    cfg.CreateMap<Tasks, TaskViewModel>().ForMember(destination => destination.ParentTask,

                   opts => opts.MapFrom(source => db.Tasks.Find(source.Parent_ID).Task))
                   .ForMember(destination => destination.StartDate,

                   opts => opts.MapFrom(source => source.StartDate.Value.ToString("yyyy-MM-dd")))
                   .ForMember(destination => destination.EndDate,

                   opts => opts.MapFrom(source => source.EndDate.Value.ToString("yyyy-MM-dd"))); ;

                });

                IMapper iMapper = config.CreateMapper();


                TaskView = iMapper.Map<List<Tasks>, List<TaskViewModel>>(Tasks);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error while getting task list");
                throw ex;

            }
            finally
            {
                Dispose(true);
            }
            return TaskView;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TaskViewModel GetTasks(int id)
        {
            TaskViewModel TaskView = null;
            try
            {
                Tasks Tasks = db.Tasks.Where(x=>x.Task_ID==id).First();
                var config = new MapperConfiguration(cfg =>
                {

                    cfg.CreateMap<Tasks, TaskViewModel>().ForMember(destination => destination.ParentTask,

                   opts => opts.MapFrom(source => db.Tasks.Find(source.Parent_ID).Task)).ForMember(destination => destination.StartDate,

                   opts => opts.MapFrom(source => source.StartDate.Value.ToString("yyyy-MM-dd")))
                   .ForMember(destination => destination.EndDate,

                   opts => opts.MapFrom(source => source.EndDate.Value.ToString("yyyy-MM-dd"))); ;

                });

                IMapper iMapper = config.CreateMapper();


                TaskView = iMapper.Map<Tasks, TaskViewModel>(Tasks);
               
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error while getting task list");
                throw ex;
            }
            finally
            {
                Dispose(true);
            }

            return TaskView;
        }

        public void PutTasks(int id, Tasks Tasks)
        {
            try
            {
                db.Entry(Tasks).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!TasksExists(id))
                {
                    logger.Info(ex, "TaskId doesn't exists");
                    throw ex;
                }
                else
                {
                    logger.Info(ex, "TaskId already exists");
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
                logger.Error(ex, "Error while updating task");
                throw ex;

            }
            finally
            {
                Dispose(true);
            }
        }


        public void PostTasks(Tasks Tasks)
        {
            try
            {
                db.Tasks.Add(Tasks);
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
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
                logger.Error(ex, "Error while adding Tasks");
                throw ex;

            }
            finally
            {
                Dispose(true);
            }
        }


        public Tasks DeleteTasks(int id)
        {
            Tasks Tasks = null;
          
            try
            {
                Tasks= db.Tasks.Where(x => x.Task_ID == id).First();
                db.Tasks.Remove(Tasks);
                db.SaveChanges();

            }
            catch (DbUpdateException ex)
            {
                logger.Info(ex, "error while deleting");
                throw ex;
            }
           catch (Exception ex)
            {
                logger.Error(ex, "Error while deleting task");
                throw ex;

            }
            finally
            {
                Dispose(true);
            }
            return Tasks;
        }

        private  void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
          
        }

        private bool TasksExists(int id)
        {
            return db.Tasks.Count(e => e.Task_ID == id) > 0;
        }
    }
}
