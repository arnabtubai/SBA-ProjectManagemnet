using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NBench;
using NBench.Util;
using NUnit.Framework;
using ProjectManagement.Entity;
using ProjectMangement.Business;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Tests.Controllers
{
    [TestFixture]
    public class TestUser
    {

        [Test]

        public void TestGetAllUsers()
        {
            byte[] bt = new byte[1024];


            //try
            //{

            //    var service = new UserLogic();
            //    List<Users> userList = service.GetUsers();

            //    NUnit.Framework.Assert.IsTrue(userList!= null);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    NUnit.Framework.Assert.IsTrue(1 == 0);
            //}

        }
    }

      
}
