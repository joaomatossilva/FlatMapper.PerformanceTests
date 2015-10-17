using System;
using System.Diagnostics;

namespace FlatMapper.PerformanceTests
{
    public class ScopedTimer : IDisposable
    {
        private readonly TestStep _step;
        private readonly Stopwatch _timer;

        private ScopedTimer(TestStep step)
        {
            _step = step;
            _timer = new Stopwatch();
        }

        private void Start()
        {
            _timer.Start();
        }

        public void Dispose()
        {
            _timer.Stop();
            _step.Elapsed = _timer.Elapsed;
        }


        public static ScopedTimer StartNew(TestStep step)
        {
            var timer = new ScopedTimer(step);
            timer.Start();
            return timer;
        }

    }
}
