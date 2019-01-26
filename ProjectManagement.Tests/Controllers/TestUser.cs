using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NBench;
using NBench.Util;

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
    public class TestUser
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
        [CounterThroughputAssertion("MyCounter", MustBe.GreaterThan, 100.0d)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThan, 2000000.0d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        public void TestGetAllUsers()
        {

           
            try
            {

                var data = new List<ProjectManagement.Entity.Users>()
                {
                    new Users{User_ID=1,EmployeeId="ST7896", FirstName="Arnab", LastName="Dey"},
                     new Users{User_ID=2,EmployeeId="YTRRR", FirstName="John", LastName="Doe"},
                }.AsQueryable();

                var mockSet = new Mock<DbSet<ProjectManagement.Entity.Users>>();
                mockSet.As<IQueryable<ProjectManagement.Entity.Users>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<ProjectManagement.Entity.Users>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<ProjectManagement.Entity.Users>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<ProjectManagement.Entity.Users>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());



                var mockContext = new Mock<ProjectManagementContext>();
                mockContext.Setup(m => m.Users).Returns(mockSet.Object);

                var service = new UserLogic(mockContext.Object);
                List<Users> userList = service.GetUsers();
                if (_opCounter == null)
                {
                    Assert.IsTrue(userList.Count == 2);


                    Assert.IsTrue(userList != null);
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
        [CounterThroughputAssertion("MyCounter", MustBe.GreaterThan, 100.0d)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThan, 2000000.0d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        public void TestGetAllUserID()
        {


            try
            {

                var data = new List<ProjectManagement.Entity.Users>()
                {
                      new Users{User_ID=1,EmployeeId="ST7896", FirstName="Arnab", LastName="Dey"},
                     new Users{User_ID=2,EmployeeId="YTRRR", FirstName="John", LastName="Doe"},
                }.AsQueryable();

                var mockSet = new Mock<DbSet<ProjectManagement.Entity.Users>>();
                mockSet.As<IQueryable<ProjectManagement.Entity.Users>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<ProjectManagement.Entity.Users>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<ProjectManagement.Entity.Users>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<ProjectManagement.Entity.Users>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
                mockSet.As<IQueryable<ProjectManagement.Entity.Users>>().Setup(m => m.Provider).Returns(data.Provider);



                var mockContext = new Mock<ProjectManagementContext>();
                mockContext.Setup(m => m.Users).Returns(mockSet.Object);

                var service = new UserLogic(mockContext.Object);
                int id = 1;
                Users userList = service.GetUsers(id);
                if (_opCounter == null)
                    Assert.IsTrue(userList != null);
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
      
        public void TestEditUser_HappyPath()
        {


            try
            {

                var data = new List<ProjectManagement.Entity.Users>()
                {
                      new Users{User_ID=1,EmployeeId="ST7896", FirstName="Arnab", LastName="Dey"},
                     new Users{User_ID=2,EmployeeId="YTRRR", FirstName="John", LastName="Doe"},
                }.AsQueryable();

                var mockSet = new Mock<DbSet<ProjectManagement.Entity.Users>>();
                mockSet.As<IQueryable<ProjectManagement.Entity.Users>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<ProjectManagement.Entity.Users>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<ProjectManagement.Entity.Users>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<ProjectManagement.Entity.Users>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());



                var mockContext = new Mock<ProjectManagementContext>();
                mockContext.Setup(m => m.Users).Returns(mockSet.Object);

                var service = new UserLogic(mockContext.Object);
                int id = 1;
                Users userFound = service.GetUsers(id);
                if (_opCounter == null)
                    Assert.IsTrue(userFound != null);
                userFound.FirstName = "David";
                service.PutUsers(id, userFound);
               
                 userFound = service.GetUsers(id);
                if (_opCounter == null)
                    Assert.IsTrue(userFound.FirstName == "David");
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
        [CounterThroughputAssertion("MyCounter", MustBe.GreaterThan, 100.0d)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated,MustBe.LessThan, 2000000.0d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        public void TestDeleteUser_HappyPath()
        {


            try
            {

                var data = new List<ProjectManagement.Entity.Users>()
                {
                      new Users{User_ID=1,EmployeeId="ST7896", FirstName="Arnab", LastName="Dey"},
                     new Users{User_ID=2,EmployeeId="YTRRR", FirstName="John", LastName="Doe"},
                }.AsQueryable();

                var mockSet = new Mock<DbSet<ProjectManagement.Entity.Users>>();
                mockSet.As<IQueryable<ProjectManagement.Entity.Users>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<ProjectManagement.Entity.Users>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<ProjectManagement.Entity.Users>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<ProjectManagement.Entity.Users>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());



                var mockContext = new Mock<ProjectManagementContext>();
                mockContext.Setup(m => m.Users).Returns(mockSet.Object);

                var service = new UserLogic(mockContext.Object);
                int id = 1;
                Users userFound = service.GetUsers(id);
                Assert.IsTrue(userFound != null);
              
                service.DeleteUsers(id);
                if (_opCounter == null)
                    Assert.IsTrue(1==1);
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
       
        public void TestEditUser_ErrorPathValidation()
        {

            DbEntityValidationException dbEx = null;
          
                var data = new List<ProjectManagement.Entity.Users>()
                {
                      new Users{EmployeeId="ST7896", FirstName="Arnab", LastName="Dey"},
                     new Users{EmployeeId="YTRRR", FirstName="John", LastName="Doe"},
                };

                

           
                //service.PostUsers(data[1]);
                int id = 78909;
                Users userFound = data[0];
                var service = new UserLogic();
                userFound.FirstName = "hhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh";

           
            service.PutUsers(id, userFound);

            if (_opCounter != null)
                _opCounter.Increment();


        }


        [TestMethod]
        // [ExpectedException (typeof(DbUpdateConcurrencyException))]
        [ExpectedException(typeof(DbEntityValidationException))]
      
        public void TestADDUser_ErrorPathValidation()
        {

            

            var data = new List<ProjectManagement.Entity.Users>()
                {
                      new Users{EmployeeId="ST7896", FirstName="hhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh", LastName="Dey"},
                     new Users{EmployeeId="YTRRR", FirstName="John", LastName="Doe"},
                };


            var service = new UserLogic();
            service.PostUsers(data[0]);
           
            if (_opCounter != null)
                _opCounter.Increment();
            


        }

        [TestMethod]
         [ExpectedException (typeof(DbUpdateConcurrencyException))]
        //[ExpectedException(typeof(DbEntityValidationException))]
       
        public void TestEditUser_ErrorPathConcurrency()
        {

            DbEntityValidationException dbEx = null;

            var data = new List<ProjectManagement.Entity.Users>()
                {
                      new Users{EmployeeId="ST7896", FirstName="Arnab", LastName="Dey"},
                     new Users{EmployeeId="YTRRR", FirstName="John", LastName="Doe"},
                };



            var service = new UserLogic();
            service.PostUsers(data[0]);
          
            Users userFound = data[0];
            service = new UserLogic();
            userFound.User_ID = 890765;

            if (_opCounter != null)
                _opCounter.Increment();
            service.PutUsers(90865, userFound);



            
        }



        [TestMethod]
        //[ExpectedException (typeof(DbUpdateConcurrencyException),)]
        //[ExpectedException(typeof(DbEntityValidationException))]
        [PerfBenchmark(Description = "Test to ensure that a minimal throughput test can be rapidly executed.",
      NumberOfIterations = 500, RunMode = RunMode.Throughput,
      RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("MyCounter", MustBe.GreaterThan, 100.0d)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThan, 2500000.0d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        public void TestAddUser_HappyPath()
        {


            try
            {

                var data = new List<ProjectManagement.Entity.Users>()
                {
                      new Users{User_ID=1,EmployeeId="ST7896", FirstName="Arnab", LastName="Dey"},
                     new Users{User_ID=2,EmployeeId="YTRRR", FirstName="John", LastName="Doe"},
                }.AsQueryable();

                var mockSet = new Mock<DbSet<ProjectManagement.Entity.Users>>();
                mockSet.As<IQueryable<ProjectManagement.Entity.Users>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<ProjectManagement.Entity.Users>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<ProjectManagement.Entity.Users>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<ProjectManagement.Entity.Users>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());



                var mockContext = new Mock<ProjectManagementContext>();
                mockContext.Setup(m => m.Users).Returns(mockSet.Object);

                var service = new UserLogic(mockContext.Object);
                int id = 1;
                Users userFound = (Users)data.First();
               
               
                service.PostUsers(userFound);

                userFound = service.GetUsers(id);
                if(_opCounter == null)
                 Assert.IsTrue(userFound.FirstName == "Arnab");
         
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
