using System.Runtime.InteropServices;

namespace SeaBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            Renderers.Init();
            StartGame();
        }

        private static void StartGame()
        {
            SeaBattle game = new();
            game.Start();
        }

        
    }

    class SeaBattle
    {
        private Map enemyMap;
        private Map playerMap;

        public void Start()
        {
            Init();
            MainMenu();

            GameCycle();
        }

        private void MainMenu()
        {
            bool inGame = false;

            while (!inGame)
            {
                Renderers.RenderMenu();
            }
        }

        

        private void Init()
        {
            Console.CursorVisible = false;
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