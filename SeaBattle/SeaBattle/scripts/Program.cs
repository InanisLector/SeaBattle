using SeaBattle.scripts;
using SeaBattle.scripts.GameStateMachine;

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
            GameSceneManager game = new();
            game.StartGameCycle();
        }


    }
}