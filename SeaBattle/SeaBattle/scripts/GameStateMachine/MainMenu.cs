namespace SeaBattle
{
    public class MainMenu : BaseGameState
    {
        private int moveInput;

        private double animationTimer = 0d;
        private const double animationDelay = 0.5d;
        private bool isInAnimation = false;

        private MainMenuOptions currentOption;
        public enum MainMenuOptions
        {
            Play,
            Settings,
            Exit
        }

        public const int MainMenuOptionsLen = 3;

        public MainMenu(GameStateMachine stateMachine, GameInfo info) :
            base("main menu", stateMachine, info)
        {

        }

        public override void Update()
        {
            base.Update();

            moveInput = GetMoveInput();
            
            ChangeOption();

            AnimationTimer();
        }

        private void ChangeOption()
        {
            if (moveInput == 0)
                return;

            currentOption += moveInput;

            currentOption = currentOption < 0 ? currentOption + MainMenuOptionsLen : currentOption;
            currentOption = currentOption >= (MainMenuOptions)MainMenuOptionsLen ? 0 : currentOption;

            somethingChanged = true;
        }

        protected override void Render()
        {
            Renderers.RenderMenu(currentOption, isInAnimation);
        }

        private int GetMoveInput()
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

        private void AnimationTimer()
        {
            if (somethingChanged)
            {
                animationTimer = 0;
                isInAnimation = false;
                return;
            }

            animationTimer += Time.deltaTime;

            if (animationTimer > animationDelay)
            {
                animationTimer = 0;
                isInAnimation = !isInAnimation;
                somethingChanged = true;
            }
        }
    }
}
