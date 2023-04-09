using System.Runtime.InteropServices;

namespace SeaBattle
{
    static class Renderers
    {
        #region Init
        
        #region DLL

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleMode(IntPtr hConsoleHandle, int mode);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetConsoleMode(IntPtr handle, out int mode);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int handle);

        #endregion

        // "\x1b[48;5;" + s + "m" - set background color by index in table (0-255)
        // "\x1b[38;5;" + s + "m" - set foreground color by index in table (0-255)
        // "\x1b[48;2;" + r + ";" + g + ";" + b + "m" - set background by r,g,b values
        // "\x1b[38;2;" + r + ";" + g + ";" + b + "m" - set foreground by r,g,b values

        public static void Init()
        {
            DLLSetup();
        }

        private static void DLLSetup()
        {
            var handle = GetStdHandle(-11);
            int mode;
            GetConsoleMode(handle, out mode);
            SetConsoleMode(handle, mode | 0x4);
        }

        #endregion

        public static void RenderMenu()
        {
            Console.SetCursorPosition(0, 0);

            Console.WriteLine($"\t{SetColor(255, 32, 32)}Sea{SetColor(32, 32, 255)}Battle\r\n\r\n> {SetColor(32, 255, 32)}Play {SetColor(255, 255, 255)}\r\nSettings\r\nExit");
        }

        private static string SetColor(byte red, byte green, byte blue)
            => $"\x1b[38;2;{red};{green};{blue}m";
    }
}
