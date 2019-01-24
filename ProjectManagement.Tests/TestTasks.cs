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
    public class TestTasks
    {
        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunTimeMilliseconds = 600000, RunMode = RunMode.Iterations)]
        [CounterMeasurement("MyCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void TestGetAllTasks_WithValues()
        {

            try
            {
                var data = new List<ProjectManager.Persistence.Task>()
                {
                    new Persistence.Task{ProjectId=25, TaskName="Support Test Driven Development", IsParentTask=true, Priority=2, ParentTask=5, StartDate=new DateTime(2018,8,12), EndDate=DateTime.Now, UserId=20},
                    new Persistence.Task{ProjectId=25, TaskName="Support Mobile Screens", IsParentTask=true, Priority=8, ParentTask=4, StartDate=new DateTime(2018,3,6), EndDate=DateTime.Now, UserId=14},
                }.AsQueryable();

                var mockSet = new Mock<DbSet<ProjectManager.Persistence.Task>>();
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.Provider).Returns(data.Provider);



                var mockContext = new Mock<ProjectManagerContext>();
                mockContext.Setup(m => m.tasks).Returns(mockSet.Object);

                var service = new TaskRepository(mockContext.Object);
                List<Task> TaskList = service.GetAllTasks();

                Assert.That(TaskList.Count == 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.That(1 == 0);
            }
        }
        [Test]
        public void TestGetAllTasks_WithSingleTask()
        {
            try
            {
                var data = new List<ProjectManager.Persistence.Task>()
                {
                      new Persistence.Task{ProjectId=25, TaskName="Support Test Driven Development", IsParentTask=true, Priority=2, ParentTask=5, StartDate=new DateTime(2018,8,12), EndDate=DateTime.Now, UserId=20},

                }.AsQueryable();

                var mockSet = new Mock<DbSet<ProjectManager.Persistence.Task>>();
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.Provider).Returns(data.Provider);



                var mockContext = new Mock<ProjectManagerContext>();
                mockContext.Setup(m => m.tasks).Returns(mockSet.Object);

                var service = new TaskRepository(mockContext.Object);
                List<Task> TaskList = service.GetAllTasks();

                Assert.That(TaskList.Count == 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.That(1 == 0);
            }

        }
        [Test]
        public void TestGetAllTask_Empty()
        {
            try
            {
                var data = new List<ProjectManager.Persistence.Task>()
                {
                }.AsQueryable();

                var mockSet = new Mock<DbSet<ProjectManager.Persistence.Task>>();
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.Provider).Returns(data.Provider);



                var mockContext = new Mock<ProjectManagerContext>();
                mockContext.Setup(m => m.tasks).Returns(mockSet.Object);

                var service = new TaskRepository(mockContext.Object);
                List<Task> TaskList = service.GetAllTasks();

                Assert.That(TaskList.Count == 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.That(1 == 0);
            }

        }
        [Test]
        public void TestCompleteTask()
        {
            try
            {
                var orgSvc = new TaskRepository(); // No Error should be thrown

                var data = new List<ProjectManager.Persistence.Task>()
                {
                    new Persistence.Task{ProjectId=25, TaskName="Support Test Driven Development", IsParentTask=true, Priority=2, ParentTask=5, StartDate=new DateTime(2018,8,12), EndDate=DateTime.Now, UserId=20},
                    new Persistence.Task{ProjectId=25, TaskName="Support Mobile Screens", IsParentTask=true, Priority=8, ParentTask=4, StartDate=new DateTime(2018,3,6), EndDate=DateTime.Now, UserId=14},

                }.AsQueryable();

                var mockSet = new Mock<DbSet<ProjectManager.Persistence.Task>>();
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.Provider).Returns(data.Provider);



                var mockContext = new Mock<ProjectManagerContext>();
                mockContext.Setup(m => m.tasks).Returns(mockSet.Object);

                var service = new TaskRepository(mockContext.Object);
                List<Task> TaskList = service.GetAllTasks();

                Assert.That(TaskList.Count == 2);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.That(1 == 0);
            }

        }

        [Test]
        public void TestEditTask()
        {
            try
            {
                var data = new List<ProjectManager.Persistence.Task>()
                {
                    new Persistence.Task{ProjectId=25, TaskName="Support Test Driven Development", IsParentTask=true, Priority=2, ParentTask=5, StartDate=new DateTime(2018,8,12), EndDate=DateTime.Now, UserId=20},
                    new Persistence.Task{ProjectId=25, TaskName="Support Mobile Screens", IsParentTask=true, Priority=8, ParentTask=4, StartDate=new DateTime(2018,3,6), EndDate=DateTime.Now, UserId=14},

                }.AsQueryable();

                var mockSet = new Mock<DbSet<ProjectManager.Persistence.Task>>();
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.Provider).Returns(data.Provider);



                var mockContext = new Mock<ProjectManagerContext>();
                mockContext.Setup(m => m.tasks).Returns(mockSet.Object);

                var service = new TaskRepository(mockContext.Object);
                List<Task> TaskList = service.GetAllTasks();

                Assert.That(TaskList.Count == 2);

                var cTask = TaskList[0];
                cTask.Priority = 5;

                bool ret = service.UpdateTask(cTask.TaskId, cTask);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.That(1 == 0);
            }

        }

        [Test]
        public void TestAddTask()
        {
            try
            {
                var data = new List<ProjectManager.Persistence.Task>()
                {

                }.AsQueryable();

                var mockSet = new Mock<DbSet<ProjectManager.Persistence.Task>>();
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
                mockSet.As<IQueryable<ProjectManager.Persistence.Task>>().Setup(m => m.Provider).Returns(data.Provider);

                Task Task = new Persistence.Task { ProjectId = 25, TaskName = "Support Test Driven Development", IsParentTask = true, Priority = 2, ParentTask = 5, StartDate = new DateTime(2018, 8, 12), EndDate = DateTime.Now, UserId = 20 };
                
                var mockContext = new Mock<ProjectManagerContext>();
                mockContext.Setup(m => m.tasks).Returns(mockSet.Object);

                var service = new TaskRepository(mockContext.Object);
                List<Task> TaskList = service.GetAllTasks();

                Assert.That(TaskList.Count == 0);
                bool ret = service.AddTask(Task);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.That(1 == 0);
            }

        }

    }
}
