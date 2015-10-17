using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace FlatMapper.PerformanceTests
{
    public abstract class TestInstance<T> : IDisposable
    {
        private string Name { get; set; }
        private IList<TestStep> _steps = new List<TestStep>(); 

        protected string TempFile { get; private set; }

        protected TestInstance(string name)
        {
            Name = name;
        }

        public void Init()
        {
            TempFile = Path.GetTempFileName();
            //TempFile = ".\\testData"+ DateTime.Now.Ticks + ".txt";
            Setup();
        }

        protected virtual void Setup() { }

        public abstract void PerformWriteTest(IEnumerable<T> items);

        public abstract void PerformReadTest();

        public ScopedTimer StartStep(string stepName)
        {
            var step = new TestStep {Name = stepName};
            _steps.Add(step);
            return ScopedTimer.StartNew(step);
        }

        public void PrintSteps()
        {
            Console.WriteLine("{0} Results", Name);
            Console.WriteLine("-----------------------------------------------");
            foreach (var testStep in _steps)
            {
                Console.WriteLine("{0,-20}{1}", testStep.Name, testStep.Elapsed);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!string.IsNullOrEmpty(TempFile) && File.Exists(TempFile))
                {
                    try { File.Delete(TempFile); }
                    catch { /* soak exception */ }
                }
            }
        }

        ~TestInstance()
        {
            Dispose(false);
        }
    }

    public class TestStep
    {
        public string Name { get; set; }
        public TimeSpan Elapsed { get; set; }
    }
}