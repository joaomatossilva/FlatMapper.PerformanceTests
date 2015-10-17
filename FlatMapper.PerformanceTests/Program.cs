using System;
using System.Linq;
using FlatMapper.PerformanceTests.FlatMapper;

namespace FlatMapper.PerformanceTests
{
    class Program
    {
        static void Main(string[] args)
        {
            const int IteractionsCount = 1000*1000;

            using (var test = new FlatMapperFixedLengthTestInstance("Flatmapper - Fixed Lenght"))
            {
                test.Init();

                var items = TestObject.GenerateItems(IteractionsCount).ToArray();

                using (test.StartStep("Write"))
                {
                    test.PerformWriteTest(items);
                }
                using (test.StartStep("Read"))
                {
                    test.PerformReadTest();
                }
                test.PrintSteps();
            }

            Console.WriteLine("");
            using (var test = new FlatMapperDelimitedTestInstance("Flatmapper - Character Delimiter"))
            {
                test.Init();

                var items = TestObject.GenerateItems(IteractionsCount).ToArray();

                using (test.StartStep("Write"))
                {
                    test.PerformWriteTest(items);
                }
                using (test.StartStep("Read"))
                {
                    test.PerformReadTest();
                }
                test.PrintSteps();
            }

        }
    }
}