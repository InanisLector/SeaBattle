using System.Runtime.InteropServices;
using IntVector2;

namespace SeaBattle
{
    class Program
    {
        /*
             *  "\x1b[48;5;" + s + "m" - set background color by index in table (0-255)
             *
             *  "\x1b[38;5;" + s + "m" - set foreground color by index in table (0-255)
             *
             *  "\x1b[48;2;" + r + ";" + g + ";" + b + "m" - set background by r,g,b values
             *
             *  "\x1b[38;2;" + r + ";" + g + ";" + b + "m" - set foreground by r,g,b values
             * 
             */

        #region DLL

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleMode(IntPtr hConsoleHandle, int mode);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetConsoleMode(IntPtr handle, out int mode);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int handle);

        #endregion

        static void Main(string[] args)
        {
            var handle = GetStdHandle(-11);
            int mode;
            GetConsoleMode(handle, out mode);
            SetConsoleMode(handle, mode | 0x4);

            while (true)
            {
                SeaBattle game = new();
                game.Start();
            }
        }
    }

    class SeaBattle
    {
        private Map enemyMap;
        private Map playerMap;

        public void Start()
        {
            Init();

            GameCycle();
        }

        private void Init()
        {

        }

        private void GameCycle()
        {
            while (!CheckForWin())
            {
                Render();
            }
        }

        private bool CheckForWin()
        {
            return false;
        }

        private void Render()
        {

        }
    }
}