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
    public class ProjectLogic
    {
        private ProjectManagementContext db = new ProjectManagementContext();
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public ProjectLogic() { }
        public ProjectLogic(ProjectManagementContext context)
        {
            db = context;
        }
        public List<ProjectViewModel> GetProjects()
        {
            List<ProjectViewModel> ProjectView = null;
            try
            {
                List<Projects> projects = db.Projects.ToList();

                var config = new MapperConfiguration(cfg =>
                {

                    cfg.CreateMap<Projects, ProjectViewModel>().ForMember(destination => destination.NumberOfTasks,

                   opts => opts.MapFrom(source => db.Tasks.Where(x => x.Project_ID == source.Project_ID).Count())).ForMember(destination => destination.StartDate,

                   opts => opts.MapFrom(source => source.StartDate.Value.ToString("yyyy-MM-dd")))
                   .ForMember(destination => destination.EndDate,

                   opts => opts.MapFrom(source => source.EndDate.Value.ToString("yyyy-MM-dd"))); ;

                });

                IMapper iMapper = config.CreateMapper();


                ProjectView = iMapper.Map<List<Projects>, List<ProjectViewModel>>(projects);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error while getting full project list"+ex.Message);
                throw new Exception("Error while getting full project list");

            }
            finally
            {
                Dispose(true);
            }
            return ProjectView;
        }


        public ProjectViewModel GetProjects(int id)
        {
            ProjectViewModel ProjectView = null;
            try
            {
                Projects projects = db.Projects.Where(x=>x.Project_ID ==id).First();

                var config = new MapperConfiguration(cfg =>
                {

                    cfg.CreateMap<Projects, ProjectViewModel>().ForMember(destination => destination.NumberOfTasks,

                   opts => opts.MapFrom(source => db.Tasks.Where(x => x.Project_ID == source.Project_ID).Count())).ForMember(destination => destination.StartDate,

                   opts => opts.MapFrom(source => source.StartDate.Value.ToString("yyyy-MM-dd")))
                   .ForMember(destination => destination.EndDate,

                   opts => opts.MapFrom(source => source.EndDate.Value.ToString("yyyy-MM-dd"))); ;

                });

                IMapper iMapper = config.CreateMapper();


                ProjectView = iMapper.Map<Projects, ProjectViewModel>(projects);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error while getting project list"+ex.Message);
                throw new Exception("Error while getting project list");
            }
            finally
            {
                Dispose(true);
            }

            return ProjectView;
        }


        public void PutProjects(int id, Projects projects)
        {
            try
            {
                db.Entry(projects).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ProjectsExists(id))
                {
                    logger.Info(ex, "ProjectId doesn't exists"+ex.Message);
                    throw new DbUpdateConcurrencyException("ProjectId doesn't exists");
                }
                else
                {
                    logger.Info("ProjectId already exists" +ex.Message);
                    throw new DbUpdateConcurrencyException("ProjectId already exists");
                }
            }
            catch (DbEntityValidationException e)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var eve in e.EntityValidationErrors)
                {
                    logger.Info("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        logger.Error("- Property: \"{0}\", Error: \"{1}\"",
                             ve.PropertyName, ve.ErrorMessage);
                        sb.AppendLine(string.Format("- Property: \"{0}\", Error: \"{1}\"",
                             ve.PropertyName, ve.ErrorMessage));
                    }
                }
                throw new DbEntityValidationException(sb.ToString());
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error while updating project"+ex.Message);
                throw new Exception("Error while updating project");

            }
            finally
            {
                Dispose(true);
            }
        }

        public void PostProjects(Projects projects)
        {
            try
            {
                db.Projects.Add(projects);
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                logger.Info("error while adding"+ex.Message);
                throw new DbUpdateException("error while adding");
            }
            catch (DbEntityValidationException e)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var eve in e.EntityValidationErrors)
                {
                    logger.Info("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        logger.Error("- Property: \"{0}\", Error: \"{1}\"",
                             ve.PropertyName, ve.ErrorMessage);
                        sb.AppendLine(string.Format("- Property: \"{0}\", Error: \"{1}\"",
                             ve.PropertyName, ve.ErrorMessage));
                    }
                }
                throw new DbEntityValidationException(sb.ToString());
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error while adding project"+ex.Message);
                throw new Exception("Error while adding project");

            }
            finally
            {
                Dispose(true);
            }
        }


        public Projects DeleteProjects(int id)
        {
            Projects Projects = null;

            try
            {
                Projects = db.Projects.Where(x => x.Project_ID == id).First();

                if (db.Tasks != null)
                {
                    List<Tasks> lTask = db.Tasks.Where(z => z.Project_ID == Projects.Project_ID).ToList();
                    db.Tasks.RemoveRange(lTask);
                }
                db.Projects.Remove(Projects);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error while deleting project" + ex.Message);
                throw new Exception("Error while deleting project");

            }
            finally
            {
                Dispose(true);
            }
            return Projects;
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

        }
        private bool ProjectsExists(int id)
        {
            return db.Projects.Count(e => e.Project_ID == id) > 0;
        }
    }
}
