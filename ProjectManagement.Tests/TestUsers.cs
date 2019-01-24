using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ProjectMangement.Business;
using ProjectMangement.Entity;
using System.Data.Entity;
//using System.Web.Http;
using Moq;
using NBench;

namespace ProjectManager.Tests
{
    [TestFixture]
    public class TestUsers
    {
        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunTimeMilliseconds = 600000, RunMode = RunMode.Iterations)]
        [CounterMeasurement("UserCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void TestGetAllUsers_WithValues()
        {

            try
            {
                var data = new List<ProjectMangement.Entity.Users>()
                {
                    new Persistence.User{EmployeeId=25, FirstName="Krishna", LastName="Kumar"},
                     new Persistence.User{EmployeeId=30, FirstName="John", LastName="Joseph"},
                }.AsQueryable();

                var mockSet = new Mock<DbSet<ProjectManager.Persistence.User>>();
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.Provider).Returns(data.Provider);



                var mockContext = new Mock<ProjectManagerContext>();
                mockContext.Setup(m => m.users).Returns(mockSet.Object);

                var service = new UserRepository(mockContext.Object);
                List<User> userList = service.GetAllUsers();

                Assert.That(userList.Count == 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.That(1 == 0);
            }
        }
        [Test]
        public void TestGetAllUsers_WithSingleUser()
        {
            try
            {
                var data = new List<ProjectManager.Persistence.User>()
                {
                    new Persistence.User{EmployeeId=25, FirstName="Krishna", LastName="Kumar"}
                }.AsQueryable();

                var mockSet = new Mock<DbSet<ProjectManager.Persistence.User>>();
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.Provider).Returns(data.Provider);



                var mockContext = new Mock<ProjectManagerContext>();
                mockContext.Setup(m => m.users).Returns(mockSet.Object);

                var service = new UserRepository(mockContext.Object);
                List<User> userList = service.GetAllUsers();

                Assert.That(userList.Count == 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.That(1 == 0);
            }

        }
        [Test]
        public void TestGetAllUser_Empty()
        {
            try
            {
                var data = new List<ProjectManager.Persistence.User>()
                {
                }.AsQueryable();

                var mockSet = new Mock<DbSet<ProjectManager.Persistence.User>>();
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.Provider).Returns(data.Provider);



                var mockContext = new Mock<ProjectManagerContext>();
                mockContext.Setup(m => m.users).Returns(mockSet.Object);

                var service = new UserRepository(mockContext.Object);
                List<User> userList = service.GetAllUsers();

                Assert.That(userList.Count == 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.That(1 == 0);
            }

        }
        [Test]
        public void TestCompleteUser()
        {
            try
            {
                var orgSvc = new UserRepository(); // No Error should be thrown

                var data = new List<ProjectManager.Persistence.User>()
                {
                     new Persistence.User{EmployeeId=25, FirstName="Krishna", LastName="Kumar"},
                     new Persistence.User{EmployeeId=30, FirstName="John", LastName="Joseph"},
                }.AsQueryable();

                var mockSet = new Mock<DbSet<ProjectManager.Persistence.User>>();
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.Provider).Returns(data.Provider);



                var mockContext = new Mock<ProjectManagerContext>();
                mockContext.Setup(m => m.users).Returns(mockSet.Object);

                var service = new UserRepository(mockContext.Object);
                List<User> userList = service.GetAllUsers();

                Assert.That(userList.Count == 2);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.That(1 == 0);
            }

        }

        [Test]
        public void TestEditUser()
        {
            try
            {
                var data = new List<ProjectManager.Persistence.User>()
                {
                     new Persistence.User{EmployeeId=25, FirstName="Krishna", LastName="Kumar"},
                     new Persistence.User{EmployeeId=30, FirstName="John", LastName="Joseph"},
                }.AsQueryable();

                var mockSet = new Mock<DbSet<ProjectManager.Persistence.User>>();
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.Provider).Returns(data.Provider);



                var mockContext = new Mock<ProjectManagerContext>();
                mockContext.Setup(m => m.users).Returns(mockSet.Object);

                var service = new UserRepository(mockContext.Object);
                List<User> userList = service.GetAllUsers();

                Assert.That(userList.Count == 2);

                var cUser = userList[0];
                cUser.FirstName = "Ram";

                bool ret = service.UpdateUser(cUser.EmployeeId, cUser);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.That(1 == 0);
            }

        }

        [Test]
        public void TestAddUser()
        {
            try
            {
                var data = new List<ProjectManager.Persistence.User>()
                {

                }.AsQueryable();

                var mockSet = new Mock<DbSet<ProjectManager.Persistence.User>>();
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
                mockSet.As<IQueryable<ProjectManager.Persistence.User>>().Setup(m => m.Provider).Returns(data.Provider);

                User user = new Persistence.User { EmployeeId = 25, FirstName = "Krishna", LastName = "Kumar" };


                var mockContext = new Mock<ProjectManagerContext>();
                mockContext.Setup(m => m.users).Returns(mockSet.Object);

                var service = new UserRepository(mockContext.Object);
                List<User> userList = service.GetAllUsers();

                Assert.That(userList.Count == 0);
                bool ret = service.AddUser(user);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.That(1 == 0);
            }

        }

    }
}
