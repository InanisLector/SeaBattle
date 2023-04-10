using System.Diagnostics;

namespace SeaBattle
{
    static class Time
    {
        private static Stopwatch _timer = new Stopwatch();

        public static int deltaTime; // Milliseconds

        public static void Start()
        {
            _timer.Start();
            deltaTime = 0;
        }

        public static void NewFrame()
        {
            deltaTime = _timer.Elapsed.Milliseconds;

            _timer.Restart();
        }
            
    }
}
