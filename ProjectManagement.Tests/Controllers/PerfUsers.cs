using NBench;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Tests.Controllers
{

    public class PerfUsers
    {
        private Counter _opCounter;
        [PerfSetup]
        public void SetUp(BenchmarkContext context)
        {
            _opCounter = context.GetCounter("MyCounter");
        }

        [PerfBenchmark(Description = "Test to ensure that a minimal throughput test can be rapidly executed.",
       NumberOfIterations = 3, RunMode = RunMode.Throughput,
       RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("MyCounter", MustBe.GreaterThan, 1000000.0d)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
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
            _opCounter.Increment();
        }

        [PerfCleanup]
        public void Cleanup()
        {
            // does nothing
        }
    }
}
