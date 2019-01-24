using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ProjectManager.Business;
using ProjectManager.Persistence;
using System.Data.Entity;
//using System.Web.Http;
using Moq;
using NBench;

namespace ProjectManager.Tests
{
    [TestFixture]
    public class TestProjects
    {
        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunTimeMilliseconds = 600000, RunMode = RunMode.Iterations)]
        [CounterMeasurement("MyCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void TestGetAllProjects_WithValues()
        {

            try
            {
                var data = new List<ProjectManager.Persistence.Project>()
                {
                    new Persistence.Project{ProjectName="My new Project", StartDate=new DateTime(2018,11,25), EndDate=DateTime.Now, Priority = 5, ManagerId=25},
                    new Persistence.Project{ProjectName="Full Stack", StartDate=new DateTime(2018,05,20), EndDate=DateTime.Now, Priority = 2, ManagerId=25},
                }.AsQueryable();

                var mockSet = new Mock<DbSet<ProjectManager.Persistence.Project>>();
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.Provider).Returns(data.Provider);



                var mockContext = new Mock<ProjectManagerContext>();
                mockContext.Setup(m => m.projects).Returns(mockSet.Object);

                var service = new ProjectRepository(mockContext.Object);
                List<Project> projectList = service.GetAllProjects();

                Assert.That(projectList.Count == 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.That(1 == 0);
            }
        }
        [Test]
        public void TestGetAllProjects_WithSingleProject()
        {
            try
            {
                var data = new List<ProjectManager.Persistence.Project>()
                {
                     new Persistence.Project{ProjectName="My new Project", StartDate=new DateTime(2018,11,25), EndDate=DateTime.Now, Priority = 5, ManagerId=25},
                }.AsQueryable();

                var mockSet = new Mock<DbSet<ProjectManager.Persistence.Project>>();
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.Provider).Returns(data.Provider);



                var mockContext = new Mock<ProjectManagerContext>();
                mockContext.Setup(m => m.projects).Returns(mockSet.Object);

                var service = new ProjectRepository(mockContext.Object);
                List<Project> projectList = service.GetAllProjects();

                Assert.That(projectList.Count == 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.That(1 == 0);
            }

        }
        [Test]
        public void TestGetAllProject_Empty()
        {
            try
            {
                var data = new List<ProjectManager.Persistence.Project>()
                {
                }.AsQueryable();

                var mockSet = new Mock<DbSet<ProjectManager.Persistence.Project>>();
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.Provider).Returns(data.Provider);



                var mockContext = new Mock<ProjectManagerContext>();
                mockContext.Setup(m => m.projects).Returns(mockSet.Object);

                var service = new ProjectRepository(mockContext.Object);
                List<Project> projectList = service.GetAllProjects();

                Assert.That(projectList.Count == 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.That(1 == 0);
            }

        }
        [Test]
        public void TestCompleteProject()
        {
            try
            {
                var orgSvc = new ProjectRepository(); // No Error should be thrown

                var data = new List<ProjectManager.Persistence.Project>()
                {
                    new Persistence.Project{ProjectName="Full Stack", StartDate=new DateTime(2018,05,20), EndDate=DateTime.Now, Priority = 2, ManagerId=25},
                    new Persistence.Project{ProjectName="Full Stack", StartDate=new DateTime(2018,05,20), EndDate=DateTime.Now, Priority = 2, ManagerId=25},
                }.AsQueryable();

                var mockSet = new Mock<DbSet<ProjectManager.Persistence.Project>>();
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.Provider).Returns(data.Provider);



                var mockContext = new Mock<ProjectManagerContext>();
                mockContext.Setup(m => m.projects).Returns(mockSet.Object);

                var service = new ProjectRepository(mockContext.Object);
                List<Project> projectList = service.GetAllProjects();

                Assert.That(projectList.Count == 2);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.That(1 == 0);
            }

        }

        [Test]
        public void TestEditProject()
        {
            try
            {
                var data = new List<ProjectManager.Persistence.Project>()
                {
 new Persistence.Project{ProjectName="My new Project", StartDate=new DateTime(2018,11,25), EndDate=DateTime.Now, Priority = 5, ManagerId=25},
                    new Persistence.Project{ProjectName="Full Stack", StartDate=new DateTime(2018,05,20), EndDate=DateTime.Now, Priority = 2, ManagerId=25},
                }.AsQueryable();

                var mockSet = new Mock<DbSet<ProjectManager.Persistence.Project>>();
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.Provider).Returns(data.Provider);



                var mockContext = new Mock<ProjectManagerContext>();
                mockContext.Setup(m => m.projects).Returns(mockSet.Object);

                var service = new ProjectRepository(mockContext.Object);
                List<Project> projectList = service.GetAllProjects();

                Assert.That(projectList.Count == 2);

                var cProject = projectList[0];
                cProject.ProjectName = "SBA";

                bool ret = service.UpdateProject(cProject.ProjectId, cProject);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.That(1 == 0);
            }

        }

       
        [Test]
        public void TestAddProject()
        {
            try
            {
                var data = new List<ProjectManager.Persistence.Project>()
                {

                }.AsQueryable();

                var mockSet = new Mock<DbSet<ProjectManager.Persistence.Project>>();
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
                mockSet.As<IQueryable<ProjectManager.Persistence.Project>>().Setup(m => m.Provider).Returns(data.Provider);

                Project project = new Persistence.Project { ProjectName = "My new Project", StartDate = new DateTime(2018, 11, 25), EndDate = DateTime.Now, Priority = 5, ManagerId = 25 };


                var mockContext = new Mock<ProjectManagerContext>();
                mockContext.Setup(m => m.projects).Returns(mockSet.Object);

                var service = new ProjectRepository(mockContext.Object);
                List<Project> projectList = service.GetAllProjects();

                Assert.That(projectList.Count == 0);
                bool ret = service.AddProject(project);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.That(1 == 0);
            }

        }

    }
}
