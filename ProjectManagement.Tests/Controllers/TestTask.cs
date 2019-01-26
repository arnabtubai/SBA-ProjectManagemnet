using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NBench;
using ProjectManagement.Entity;
using ProjectMangement.Business;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Tests.Controllers
{
    [TestClass]
    public class TestTask
    {
        private Counter _opCounter;

        [PerfSetup]
        public void SetUp(BenchmarkContext context)
        {
            _opCounter = context.GetCounter("MyCounter");
        }
        [TestMethod]
        //[ExpectedException (typeof(DbUpdateConcurrencyException),)]
        //[ExpectedException(typeof(DbEntityValidationException))]
        [PerfBenchmark(Description = "Test to ensure that a minimal throughput test can be rapidly executed.",
        NumberOfIterations = 10, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("MyCounter", MustBe.GreaterThan, 1000000.0d)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        public void TestGetAllTasks()
        {


            try
            {
                var data = new List<Tasks>()
                {
                      new Tasks{Task_ID=1,Project_ID=25, Task="Complete FSE",Status="N", Priority=2, Parent_ID=5, StartDate=new DateTime(2018,8,12), EndDate=DateTime.Now, User_ID=14},
                    new Tasks{Project_ID=25, Task="Complete SBA", Status="N", Priority=8, Parent_ID=4, StartDate=new DateTime(2018,3,6), EndDate=DateTime.Now, User_ID=17},
                }.AsQueryable();

                var mockSet = new Mock<DbSet<Tasks>>();
                mockSet.As<IQueryable<Tasks>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<Tasks>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<Tasks>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<Tasks>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                var mockContext = new Mock<ProjectManagementContext>();
                mockContext.Setup(m => m.Tasks).Returns(mockSet.Object);

                var service = new TaskLogic(mockContext.Object);
                List<TaskViewModel> projList = service.GetTasks();
                if (_opCounter == null)
                {
                    Assert.IsTrue(projList.Count == 2);


                    Assert.IsTrue(projList != null);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (_opCounter == null)
                    Assert.IsTrue(1 == 0);
            }
            if (_opCounter != null)
                _opCounter.Increment();
        }


        [TestMethod]
        //[ExpectedException (typeof(DbUpdateConcurrencyException),)]
        //[ExpectedException(typeof(DbEntityValidationException))]
        [PerfBenchmark(Description = "Test to ensure that a minimal throughput test can be rapidly executed.",
       NumberOfIterations = 10, RunMode = RunMode.Throughput,
       RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("MyCounter", MustBe.GreaterThan, 1000000.0d)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        public void TestGetAllTaskId()
        {


            try
            {

                var data = new List<Tasks>()
                {
                      new Tasks{Task_ID=1,Project_ID=25, Task="Complete FSE",Status="N", Priority=2, Parent_ID=5, StartDate=new DateTime(2018,8,12), EndDate=DateTime.Now, User_ID=14},
                    new Tasks{Project_ID=25, Task="Complete SBA", Status="N", Priority=8, Parent_ID=4, StartDate=new DateTime(2018,3,6), EndDate=DateTime.Now, User_ID=17},
                }.AsQueryable();

                var mockSet = new Mock<DbSet<Tasks>>();
                mockSet.As<IQueryable<Tasks>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<Tasks>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<Tasks>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<Tasks>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                var mockContext = new Mock<ProjectManagementContext>();
                mockContext.Setup(m => m.Tasks).Returns(mockSet.Object);

                var service = new TaskLogic(mockContext.Object);
                int id = 1;
                TaskViewModel projList = service.GetTasks(id);
                if (_opCounter == null)
                    Assert.IsTrue(projList != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (_opCounter == null)
                    Assert.IsTrue(1 == 0);
            }
            if (_opCounter != null)
                _opCounter.Increment();
        }

        [TestMethod]
        //[ExpectedException (typeof(DbUpdateConcurrencyException),)]
        //[ExpectedException(typeof(DbEntityValidationException))]
     
        public void TestEditTask_HappyPath()
        {


            try
            {

                var data = new List<Tasks>()
                {
                      new Tasks{Task_ID=1,Project_ID=25, Task="Complete FSE",Status="N", Priority=2, Parent_ID=5, StartDate=new DateTime(2018,8,12), EndDate=DateTime.Now, User_ID=14},
                    new Tasks{Project_ID=25, Task="Complete SBA", Status="N", Priority=8, Parent_ID=4, StartDate=new DateTime(2018,3,6), EndDate=DateTime.Now, User_ID=17},
                }.AsQueryable();

                var mockSet = new Mock<DbSet<Tasks>>();
                mockSet.As<IQueryable<Tasks>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<Tasks>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<Tasks>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<Tasks>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());



                var mockContext = new Mock<ProjectManagementContext>();
                mockContext.Setup(m => m.Tasks).Returns(mockSet.Object);

                var service = new TaskLogic(mockContext.Object);
                int id = 1;
              
              
                data.First().Task = "World";
                service.PutTasks(id, data.First());

                if (_opCounter == null)
                    Assert.IsTrue(data.First().Task == "World");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (_opCounter == null)
                    Assert.IsTrue(1 == 0);
            }
            if (_opCounter != null)
                _opCounter.Increment();
        }



        [TestMethod]
        //[ExpectedException (typeof(DbUpdateConcurrencyException),)]
        //[ExpectedException(typeof(DbEntityValidationException))]
        [PerfBenchmark(Description = "Test to ensure that a minimal throughput test can be rapidly executed.",
       NumberOfIterations = 10, RunMode = RunMode.Throughput,
       RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("MyCounter", MustBe.GreaterThan, 1000000.0d)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        public void TestDeleteTask_HappyPath()
        {


            try
            {

                var data = new List<Tasks>()
                {
                      new Tasks{Task_ID=1,Project_ID=25, Task="Complete FSE",Status="N", Priority=2, Parent_ID=5, StartDate=new DateTime(2018,8,12), EndDate=DateTime.Now, User_ID=14},
                    new Tasks{Project_ID=25, Task="Complete SBA", Status="N", Priority=8, Parent_ID=4, StartDate=new DateTime(2018,3,6), EndDate=DateTime.Now, User_ID=17},
                }.AsQueryable();

                var mockSet = new Mock<DbSet<Tasks>>();
                mockSet.As<IQueryable<Tasks>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<Tasks>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<Tasks>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<Tasks>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());



                var mockContext = new Mock<ProjectManagementContext>();
                mockContext.Setup(m => m.Tasks).Returns(mockSet.Object);

                var service = new TaskLogic(mockContext.Object);
                int id = 1;
                TaskViewModel projFound = service.GetTasks(id);
                if (_opCounter == null)
                    Assert.IsTrue(projFound != null);

                service.DeleteTasks(id);
                if (_opCounter == null)
                    Assert.IsTrue(1 == 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (_opCounter == null)
                    Assert.IsTrue(1 == 0);
            }
            if (_opCounter != null)
                _opCounter.Increment();
        }


      
        [TestMethod]
        // [ExpectedException (typeof(DbUpdateConcurrencyException))]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void TestEditTask_ErrorPathValidation()
        {

            var data = new List<Tasks>()
                {
                      new Tasks{Project_ID=25, Task="Complete FSE",Status="N", Priority=2, Parent_ID=5, StartDate=new DateTime(2018,8,12), EndDate=DateTime.Now, User_ID=14},
                    new Tasks{Project_ID=25, Task="Complete SBA", Status="N", Priority=8, Parent_ID=4, StartDate=new DateTime(2018,3,6), EndDate=DateTime.Now, User_ID=17},
                }.AsQueryable();



            var service = new TaskLogic();

            //service.PostUsers(data[1]);
            int id = 1;
            Tasks projectFound = data.First();
            service = new TaskLogic();
            projectFound.Task = "";


            service.PutTasks(id, projectFound);




            if (_opCounter != null)
                _opCounter.Increment();
        }


        [TestMethod]
        // [ExpectedException (typeof(DbUpdateConcurrencyException))]
        [ExpectedException(typeof(DbEntityValidationException))]
      
        public void TestADDTask_ErrorPathValidation()
        {

            var dataProject = new List<Projects>()
                {

                      new Projects{ Project_ID =1, Project="java Project",Completed=true, StartDate=new DateTime(2019,01,25), EndDate=DateTime.Now, Priority = 15, User_ID=1},
                    new Projects{ Project_ID =2, Project=".net project",Completed=true, StartDate=new DateTime(2018,12,12), EndDate=DateTime.Now, Priority = 26, User_ID=2},
                }.AsQueryable();

            var dataUser = new List<ProjectManagement.Entity.Users>()
                {
                      new Users{User_ID=1,EmployeeId="ST7896", FirstName="Arnab", LastName="Dey"},
                     new Users{User_ID=2,EmployeeId="YTRRR", FirstName="John", LastName="Doe"},
                }.AsQueryable();
            var data = new List<Tasks>()
                {
                      new Tasks{Project_ID=25, Task="Complete FSE",Status="uuuuuuuuuuuuuuuuuuuuuuuuuuuuuu", Priority=2, Parent_ID=5, StartDate=new DateTime(2018,8,12), EndDate=DateTime.Now, User_ID=14},
                    new Tasks{Project_ID=25, Task="Complete SBA", Status="N", Priority=8, Parent_ID=4, StartDate=new DateTime(2018,3,6), EndDate=DateTime.Now, User_ID=17},
                }.AsQueryable();




            var serviceuser = new UserLogic();
            serviceuser.PostUsers(dataUser.First());
            serviceuser = new UserLogic();
            int id = serviceuser.GetUsers().Max(x => x.User_ID);
            dataProject.First().User_ID = id;
            data.First().User_ID = id;
            var serviveProject = new ProjectLogic();
            serviveProject.PostProjects(dataProject.First());
            serviveProject = new ProjectLogic();
            id = serviveProject.GetProjects().Max(x => x.Project_ID);
            data.First().Task_ID = id;
            var service = new TaskLogic();
            service.PostTasks(data.First());

            if (_opCounter != null)
                _opCounter.Increment();



        }

        [TestMethod]
        // [ExpectedException (typeof(DbUpdateConcurrencyException))]
        [ExpectedException(typeof(DbUpdateException))]
      
        public void TestADDTask_ErrorPathDB()
        {


            var data = new List<Tasks>()
                {
                      new Tasks{Project_ID=25, Task="Complete FSE",Status="N", Priority=2, Parent_ID=5, StartDate=new DateTime(2018,8,12), EndDate=DateTime.Now, User_ID=14},
                    new Tasks{Project_ID=25, Task="Complete SBA", Status="N", Priority=8, Parent_ID=4, StartDate=new DateTime(2018,3,6), EndDate=DateTime.Now, User_ID=17},
                }.AsQueryable();



            var service = new TaskLogic();
            service.PostTasks(data.First());

            if (_opCounter != null)
                _opCounter.Increment();



        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateConcurrencyException))]
        //[ExpectedException(typeof(DbEntityValidationException))]
       
        public void TestEditTask_ErrorPathConcurrency()
        {

            var data = new List<Tasks>()
                {
                      new Tasks{Task_ID=1,Project_ID=25, Task="Complete FSE",Status="N", Priority=2, Parent_ID=5, StartDate=new DateTime(2018,8,12), EndDate=DateTime.Now, User_ID=14},
                    new Tasks{Project_ID=25, Task="Complete SBA", Status="N", Priority=8, Parent_ID=4, StartDate=new DateTime(2018,3,6), EndDate=DateTime.Now, User_ID=17},
                }.AsQueryable();

            var service = new TaskLogic();

            Tasks prjFound = data.First();
            service = new TaskLogic();
            prjFound.Task_ID = 890765;


            service.PutTasks(90865, prjFound);




            if (_opCounter != null)
                _opCounter.Increment();
        }



        [TestMethod]
        //[ExpectedException (typeof(DbUpdateConcurrencyException),)]
        //[ExpectedException(typeof(DbEntityValidationException))]
        [PerfBenchmark(Description = "Test to ensure that a minimal throughput test can be rapidly executed.",
      NumberOfIterations = 10, RunMode = RunMode.Throughput,
      RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("MyCounter", MustBe.GreaterThan, 1000000.0d)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        public void TestAddTask_HappyPath()
        {


            try
            {


                var data = new List<Tasks>()
                {
                      new Tasks{Task_ID=1, Project_ID=25, Task="Complete FSE",Status="N", Priority=2, Parent_ID=5, StartDate=new DateTime(2018,8,12), EndDate=DateTime.Now, User_ID=14},
                    new Tasks{Project_ID=25, Task="Complete SBA", Status="N", Priority=8, Parent_ID=4, StartDate=new DateTime(2018,3,6), EndDate=DateTime.Now, User_ID=17},
                }.AsQueryable();

                var mockSet = new Mock<DbSet<Tasks>>();
                mockSet.As<IQueryable<Tasks>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<Tasks>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<Tasks>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<Tasks>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());



                var mockContext = new Mock<ProjectManagementContext>();
                mockContext.Setup(m => m.Tasks).Returns(mockSet.Object);

                var service = new TaskLogic(mockContext.Object);
                int id = 1;
                Tasks projFound = (Tasks)data.First();
                // service.PostTasks(dataUser.First());

                service.PostTasks(projFound);

                TaskViewModel projFound1 = service.GetTasks(id);
                if (_opCounter == null)
                    Assert.IsTrue(projFound1.Task == "Complete FSE");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (_opCounter == null)
                    Assert.IsTrue(1 == 0);
            }
            if (_opCounter != null)
                _opCounter.Increment();
        }
        [PerfCleanup]
        public void Cleanup()
        {
            // does nothing
        }
    }
}
