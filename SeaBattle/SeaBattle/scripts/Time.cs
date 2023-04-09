using System.Diagnostics;

namespace SeaBattle
{
    static class Time
    {
        private static Stopwatch _timer = new Stopwatch();

        public static double deltaTime;

        public static void Start()
        {
            _timer.Start();
            deltaTime = 0;
        }

        public static void NewFrame()
        {
            deltaTime = _timer.Elapsed.Milliseconds / 1000d;

            _timer.Restart();
        }
            
    }
}
