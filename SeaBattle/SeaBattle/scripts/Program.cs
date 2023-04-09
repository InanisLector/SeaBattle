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
            GameStateMachine game = new();
            game.StartGameCycle();
        }


    }
}