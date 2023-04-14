using System.Runtime.InteropServices;
using SeaBattle.scripts.GameStateMachine.Menu;
using System.Text;
using IntVector2;

namespace SeaBattle.scripts
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
            Console.CursorVisible = false;
        }

        private static void DLLSetup()
        {
            var handle = GetStdHandle(-11);
            int mode;
            GetConsoleMode(handle, out mode);
            SetConsoleMode(handle, mode | 0x4);
        }

        #endregion

        #region Menu renderers

        public static void RenderMainMenu(MainMenu.MainMenuOptions currentOption, bool isAnimated)
        {
            //Console.SetCursorPosition(0, 0);
            Console.Clear();

            StringBuilder builder = new();

            builder.Append($"\t{SetTextColor(255, 32, 32)}Sea{SetTextColor(32, 32, 255)}Battle\r\n\r\n");

            if (currentOption == MainMenu.MainMenuOptions.Play)
                builder.Append($"{SetTextColor(255, 255, 255)}{MenuAnimation(isAnimated)}{SetTextColor(32, 255, 32)}Play\r\n");
            else
                builder.Append($"{SetTextColor(255, 255, 255)}Play\r\n");

            if (currentOption == MainMenu.MainMenuOptions.Settings)
                builder.Append($"{SetTextColor(255, 255, 255)}{MenuAnimation(false)}{SetTextColor(144, 144, 144)}Settings\r\n");
            else
                builder.Append($"{SetTextColor(144, 144, 144)}Settings\r\n");

            if (currentOption == MainMenu.MainMenuOptions.Exit)
                builder.Append($"{SetTextColor(255, 255, 255)}{MenuAnimation(isAnimated)}{SetTextColor(255, 32, 32)}Exit");
            else
                builder.Append($"{SetTextColor(255, 255, 255)}Exit");

            Console.WriteLine(builder.ToString());
        }
        
        public static void RenderExitMenu(ExitMenu.ExitMenuOptions currentOption, bool isAnimated)
        {
            //Console.SetCursorPosition(0, 0);
            Console.Clear();

            StringBuilder builder = new();

            builder.Append($"\t{SetTextColor(255, 32, 32)}Sea{SetTextColor(32, 32, 255)}Battle\r\n\r\n");
            builder.Append($"{SetTextColor(144, 144, 144)}Play\r\n");
            builder.Append("Settings\r\n");
            builder.Append($" >{SetTextColor(255, 32, 32)}Exit\t");

            if (currentOption == ExitMenu.ExitMenuOptions.Back)
                builder.Append($"{SetTextColor(255, 255, 255)}{MenuAnimation(isAnimated)}{SetTextColor(32, 255, 32)}Back\r\n");
            else
                builder.Append($"{SetTextColor(255, 255, 255)}Back\r\n");

            builder.Append("\t");

            if (currentOption == ExitMenu.ExitMenuOptions.Exit)
                builder.Append($"{SetTextColor(255, 255, 255)}{MenuAnimation(isAnimated)}{SetTextColor(255, 32, 32)}Exit");
            else
                builder.Append($"{SetTextColor(255, 255, 255)}Exit");

            Console.WriteLine(builder.ToString());
        }

        public static void RenderPlayMenu(PlayMenu.PlayMenuOptions currentOption, bool isAnimated)
        {
            //Console.SetCursorPosition(0, 0);
            Console.Clear();

            StringBuilder builder = new();

            builder.Append($"\t{SetTextColor(255, 32, 32)}Sea{SetTextColor(32, 32, 255)}Battle\r\n\r\n");
            builder.Append($"{SetTextColor(255, 255, 255)} >{SetTextColor(32, 255, 32)}Play\t"); 
                if (currentOption == PlayMenu.PlayMenuOptions.SinglePlayer)
                    builder.Append($" {SetTextColor(255, 255, 255)}{MenuAnimation(isAnimated)}{SetTextColor(32, 255, 32)}Single Player\r\n");
                else
                    builder.Append($" {SetTextColor(255, 255, 255)}Single Player\r\n");
            builder.Append($"{SetTextColor(144, 144, 144)}Settings");
                if (currentOption == PlayMenu.PlayMenuOptions.Multiplayer)
                    builder.Append($" {SetTextColor(255, 255, 255)}{MenuAnimation(false)}{SetTextColor(144, 144, 144)}Multi Player\r\n");
                else
                    builder.Append($" {SetTextColor(144, 144, 144)}Multi Player\r\n");
            builder.Append($"{SetTextColor(144, 144, 144)}Exit");

            Console.WriteLine(builder.ToString());
        }

        private static string MenuAnimation(bool isAnimated)
            => isAnimated ? " >" : "> ";

        #endregion

        public static void RenderMapEditor(this Map map)
        {
            Console.Clear();

            StringBuilder builder = new($"{SetTextColor(255, 255, 255)}   ");
            builder.AppendNumColumns();

            Vector2 i;
            for (i.y = 0; i.y < Map.Height; i.y++)
            {
                builder.Append($"{(i.y + 1).ToString("00")} ");

                for (i.x = 0; i.x < Map.Width; i.x++)
                {
                    builder.Append(map[i, Map.mapType.ship] ? 'x' : '.');
                    builder.Append(' ');
                }
;

                builder.Append('\n');
            }

            Console.WriteLine(builder.ToString());
        }

        public static void RenderSinglePlayer(this Map map, Vector2 playerPosition)
        {
            Console.Clear();

            StringBuilder builder = new($"{SetTextColor(255, 255, 255)}   ");
            builder.AppendNumColumns();

            Vector2 i;
            for (i.y = 0; i.y < Map.Height; i.y++)
            {
                builder.Append($"{(i.y + 1).ToString("00")} ");

                for (i.x = 0; i.x < Map.Width; i.x++)
                {
                    if (i == playerPosition)
                    {
                        builder.Append($"{SetTextColor(0, 0, 0)}{SetBGColor(255, 255, 255)}");

                        builder.Append(RenderedTile());

                        builder.Append($"{SetTextColor(255, 255, 255)}{SetBGColor(0, 0, 0)}");
                    }
                    else
                    {
                        builder.Append(RenderedTile());
                    }

                    builder.Append(' ');
                }

                builder.Append('\n');
            }

            Console.WriteLine(builder.ToString());

            char RenderedTile()
            {
                (bool ship, bool shot) = map[i];

                if (!shot)
                    return ' ';
               
                return ship ? 'x' : '.';
            }
        }

        private static string SetTextColor(byte red, byte green, byte blue)
            => $"\x1b[38;2;{red};{green};{blue}m";

        private static string SetBGColor(byte red, byte green, byte blue)
            => $"\x1b[48;2;{red};{green};{blue}m";
    }

    internal static class RendererHelper
    {
        internal static void AppendNumColumns(this StringBuilder builder)
        {
            for (int i = 0; i < Map.Width; i++)
            {
                builder.Append((char)(i + 'A'));
                builder.Append(" ");
            }

            builder.Append('\n');
        }
    }
}
