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
    public class TestProject
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
        NumberOfIterations = 500, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("MyCounter", MustBe.GreaterThan, 40.0d)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThan, 3500000.0d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThan, 3.0d)]
        public void TestGetAllProjects()
        {


            try
            {
                var data = new List<Projects>()
                {
                      new Projects{ Project_ID =1, Project="java Project",Completed=true, StartDate=new DateTime(2019,01,25), EndDate=DateTime.Now, Priority = 15, User_ID=1},
                    new Projects{ Project_ID =2, Project=".net project",Completed=true, StartDate=new DateTime(2018,12,12), EndDate=DateTime.Now, Priority = 26, User_ID=2},
                }.AsQueryable();

                var mockSet = new Mock<DbSet<Projects>>();
                mockSet.As<IQueryable<Projects>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<Projects>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<Projects>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<Projects>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                var mockContext = new Mock<ProjectManagementContext>();
                mockContext.Setup(m => m.Projects).Returns(mockSet.Object);

                var service = new ProjectLogic(mockContext.Object);
                List<ProjectViewModel> projList = service.GetProjects();
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
       NumberOfIterations = 500, RunMode = RunMode.Throughput,
       RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("MyCounter", MustBe.GreaterThan, 40.0d)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThan, 3500000.0d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThan, 3.0d)]
        public void TestGetAllProjectId()
        {


            try
            {

                var data = new List<Projects>()
                {
                      new Projects{ Project_ID =1, Project="java Project",Completed=true, StartDate=new DateTime(2019,01,25), EndDate=DateTime.Now, Priority = 15, User_ID=1},
                    new Projects{ Project_ID =2, Project=".net project",Completed=true, StartDate=new DateTime(2018,12,12), EndDate=DateTime.Now, Priority = 26, User_ID=2},
                }.AsQueryable();

                var mockSet = new Mock<DbSet<Projects>>();
                mockSet.As<IQueryable<Projects>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<Projects>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<Projects>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<Projects>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
          
                var mockContext = new Mock<ProjectManagementContext>();
                mockContext.Setup(m => m.Projects ).Returns(mockSet.Object);

                var service = new ProjectLogic(mockContext.Object);
                int id = 1;
                ProjectViewModel projList = service.GetProjects(id);
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
        
        public void TestEditProject_HappyPath()
        {


            try
            {

                var data = new List<Projects>()
                {

                      new Projects{ Project_ID =1, Project="java Project",Completed=true, StartDate=new DateTime(2019,01,25), EndDate=DateTime.Now, Priority = 15, User_ID=1},
                    new Projects{ Project_ID =2, Project=".net project",Completed=true, StartDate=new DateTime(2018,12,12), EndDate=DateTime.Now, Priority = 26, User_ID=2},
                }.AsQueryable();

                var mockSet = new Mock<DbSet<Projects>>();
                mockSet.As<IQueryable<Projects>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<Projects>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<Projects>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<Projects>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());



                var mockContext = new Mock<ProjectManagementContext>();
                mockContext.Setup(m => m.Projects).Returns(mockSet.Object);

                var service = new ProjectLogic(mockContext.Object);
                int id = 67;
                data.First().Project = "World";
                service.PutProjects(id, data.First());

                if (_opCounter == null)
                    Assert.IsTrue(data.First().Project == "World");
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
       NumberOfIterations = 500, RunMode = RunMode.Throughput,
       RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("MyCounter", MustBe.GreaterThan, 40.0d)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThan, 3500000.0d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThan, 3.0d)]
        public void TestDeleteProject_HappyPath()
        {


            try
            {

                var data = new List<Projects>()
                {

                      new Projects{ Project_ID =1, Project="java Project",Completed=true, StartDate=new DateTime(2019,01,25), EndDate=DateTime.Now, Priority = 15, User_ID=1},
                    new Projects{ Project_ID =2, Project=".net project",Completed=true, StartDate=new DateTime(2018,12,12), EndDate=DateTime.Now, Priority = 26, User_ID=2},
                }.AsQueryable();

                var mockSet = new Mock<DbSet<Projects>>();
                mockSet.As<IQueryable<Projects>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<Projects>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<Projects>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<Projects>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());



                var mockContext = new Mock<ProjectManagementContext>();
                mockContext.Setup(m => m.Projects).Returns(mockSet.Object);

                var service = new ProjectLogic(mockContext.Object);
                int id = 1;
                ProjectViewModel projFound = service.GetProjects(id);
                if (_opCounter == null)
                    Assert.IsTrue(projFound != null);

                service.DeleteProjects(id);
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
        public void TestEditProject_ErrorPathValidation()
        {

            var data = new List<Projects>()
                {

                      new Projects{ Project_ID =1, Project="java Project",Completed=true, StartDate=new DateTime(2019,01,25), EndDate=DateTime.Now, Priority = 15, User_ID=1},
                    new Projects{ Project_ID =2, Project=".net project",Completed=true, StartDate=new DateTime(2018,12,12), EndDate=DateTime.Now, Priority = 26, User_ID=2},
                }.AsQueryable();



            var service = new ProjectLogic();
            
            //service.PostUsers(data[1]);
            int id = 1;
            Projects projectFound = data.First();
            service = new ProjectLogic();
            projectFound.Project = "";

            
            service.PutProjects(id, projectFound);



     
            if (_opCounter != null)
                _opCounter.Increment();
        }


        [TestMethod]
        // [ExpectedException (typeof(DbUpdateConcurrencyException))]
        [ExpectedException(typeof(DbEntityValidationException))]
        
        public void TestADDProject_ErrorPathValidation()
        {

            var dataUser = new List<ProjectManagement.Entity.Users>()
                {
                      new Users{User_ID=1,EmployeeId="ST7896", FirstName="Arnab", LastName="Dey"},
                     new Users{User_ID=2,EmployeeId="YTRRR", FirstName="John", LastName="Doe"},
                }.AsQueryable();
            var data = new List<Projects>()
                {

                      new Projects{ Project_ID =1, Project="Hhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh", StartDate=new DateTime(2019,01,25), EndDate=DateTime.Now, Priority = 15, User_ID=1},
                    new Projects{ Project_ID =2, Project=".net project",Completed=true, StartDate=new DateTime(2018,12,12), EndDate=DateTime.Now, Priority = 26, User_ID=2},
                }.AsQueryable();



            var serviceuser = new UserLogic();
            serviceuser.PostUsers(dataUser.First());
            serviceuser = new UserLogic();
            int id = serviceuser.GetUsers().Max(x => x.User_ID);
            data.First().User_ID = id;
            var service = new ProjectLogic();
            service.PostProjects(data.First());

            if (_opCounter != null)
                _opCounter.Increment();



        }

        [TestMethod]
        // [ExpectedException (typeof(DbUpdateConcurrencyException))]
        [ExpectedException(typeof(DbUpdateException))]
        
        public void TestADDProject_ErrorPathDB()
        {

         
            var data = new List<Projects>()
                {

                      new Projects{ Project_ID =1, Project="Hhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh",Completed=true, StartDate=new DateTime(2019,01,25), EndDate=DateTime.Now, Priority = 15, User_ID=1},
                    new Projects{ Project_ID =2, Project=".net project",Completed=true, StartDate=new DateTime(2018,12,12), EndDate=DateTime.Now, Priority = 26, User_ID=2},
                }.AsQueryable();



            var service = new ProjectLogic();
            service.PostProjects(data.First());

            if (_opCounter != null)
                _opCounter.Increment();



        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateConcurrencyException))]
        //[ExpectedException(typeof(DbEntityValidationException))]
        
        public void TestEditProject_ErrorPathConcurrency()
        {

            var data = new List<Projects>()
                {

                      new Projects{ Project_ID =1, Project="java Project",Completed=true, StartDate=new DateTime(2019,01,25), EndDate=DateTime.Now, Priority = 15, User_ID=1},
                    new Projects{ Project_ID =2, Project=".net project",Completed=true, StartDate=new DateTime(2018,12,12), EndDate=DateTime.Now, Priority = 26, User_ID=2},
                }.AsQueryable();


            var service = new ProjectLogic();
           
            Projects prjFound = data.First();
            service = new ProjectLogic();
            prjFound.Project_ID = 890765;

           
            service.PutProjects(90865, prjFound);




            if (_opCounter != null)
                _opCounter.Increment();
        }



        [TestMethod]
        //[ExpectedException (typeof(DbUpdateConcurrencyException),)]
        //[ExpectedException(typeof(DbEntityValidationException))]
        [PerfBenchmark(Description = "Test to ensure that a minimal throughput test can be rapidly executed.",
      NumberOfIterations = 500, RunMode = RunMode.Throughput,
      RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("MyCounter", MustBe.GreaterThan, 40.0d)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThan, 3500000.0d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThan, 3.0d)]
        public void TestAddProject_HappyPath()
        {


            try
            {
               

                var data = new List<Projects>()
                {

                      new Projects{ Project_ID =1, Project="java Project",Completed=true, StartDate=new DateTime(2019,01,25), EndDate=DateTime.Now, Priority = 15, User_ID=1},
                    new Projects{ Project_ID =2, Project=".net project",Completed=true, StartDate=new DateTime(2018,12,12), EndDate=DateTime.Now, Priority = 26, User_ID=2},
                }.AsQueryable();

                var mockSet = new Mock<DbSet<Projects>>();
                mockSet.As<IQueryable<Projects>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<Projects>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<Projects>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<Projects>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());



                var mockContext = new Mock<ProjectManagementContext>();
                mockContext.Setup(m => m.Projects).Returns(mockSet.Object);

                var service = new ProjectLogic(mockContext.Object);
                int id = 1;
                Projects projFound = (Projects)data.First();
               // service.PostProjects(dataUser.First());

                service.PostProjects(projFound);

                ProjectViewModel projFound1 = service.GetProjects(id);
                if (_opCounter == null)
                    Assert.IsTrue(projFound1.Project == "java Project");
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
