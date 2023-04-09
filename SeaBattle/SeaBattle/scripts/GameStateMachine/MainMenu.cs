namespace SeaBattle
{
    public class MainMenu : BaseGameState
    {
        private int input;

        private MainMenuOptions currentOption;
        public enum MainMenuOptions
        {
            Play,
            Settings,
            Exit
        }

        public MainMenu(GameStateMachine stateMachine, GameInfo info) :
            base("main menu", stateMachine, info)
        {

        }

        public override void Update()
        {
            base.Update();

            input = GetInput();

            if (input != 0)
                somethingChanged = true;
            
            currentOption += input;
        }

        public override void Renderer()
        {
            base.Renderer();

            Renderers.RenderMenu(currentOption);
        }

        private int GetInput()
        {
            return inputKeys.Key switch
            {
                ConsoleKey.W => -1,
                ConsoleKey.UpArrow => -1,
                ConsoleKey.S => 1,
                ConsoleKey.DownArrow => 1,
                _ => 0
            };
        }
    }
}
