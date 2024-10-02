using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public static class Timer
    {
        private static System.Diagnostics.Stopwatch stopwatch;

        public static void Start()
        {
            stopwatch = System.Diagnostics.Stopwatch.StartNew();
        }

        public static string Stop()
        {
            stopwatch.Stop();
            var resultTime = stopwatch.Elapsed;
            return String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                resultTime.Hours,
                resultTime.Minutes,
                resultTime.Seconds,
                resultTime.Milliseconds);
        }
    }
}
