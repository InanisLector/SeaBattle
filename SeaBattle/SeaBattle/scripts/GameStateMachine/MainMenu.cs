namespace SeaBattle
{
    public class MainMenu : BaseGameState
    {
        private int moveInput;
        private bool selectInput;

        private int _animationTimer = 0;
        private const int animationDelay = 300;
        private bool _isInAnimation = false;

        private MainMenuOptions currentOption;
        public enum MainMenuOptions
        {
            Play,
            Settings,
            Exit
        }
        public const int MainMenuOptionsLen = 3;

        public MainMenu(GameSceneManager sceneManager, GameInfo info) :
            base("main menu", sceneManager, info) { }

        public override void Update()
        {
            base.Update();

            moveInput = GetMoveInput();
            selectInput = GetSelectInput();            

            ChangeOption();
            SelectOption();

            AnimationTimer();
        }

        protected override void Render()
        {
            Renderers.RenderMainMenu(currentOption, _isInAnimation);
        }

        #region Inputs
        
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
        private bool GetSelectInput()
        {
            return inputKeys.Key switch
            {
                ConsoleKey.D => true,
                ConsoleKey.RightArrow => true,
                ConsoleKey.Enter => true,
                _ => false
            };
        }

        #endregion

        private void ChangeOption()
        {
            if (moveInput == 0)
                return;

            currentOption += moveInput;

            currentOption = currentOption < 0 ? currentOption + MainMenuOptionsLen : currentOption;
            currentOption = currentOption >= (MainMenuOptions)MainMenuOptionsLen ? 0 : currentOption;

            somethingChanged = true;
        }

        private void SelectOption()
        {
            if(!selectInput)
                return;

            switch (currentOption)
            {
                case MainMenuOptions.Play:
                    Console.WriteLine("Not finished yet");
                    break;

                case MainMenuOptions.Settings:
                    Console.WriteLine("Here too");
                    break;

                case MainMenuOptions.Exit:
                    SceneManager.ChangeCurrentState(SceneManager.exitMenu);
                    break;

                default:
                    throw new IndexOutOfRangeException();
            }
        }

        private void AnimationTimer()
        {
            if (somethingChanged)
            {
                _animationTimer = 0;
                _isInAnimation = false;
                return;
            }

            _animationTimer += Time.deltaTime;

            if (_animationTimer > animationDelay)
            {
                _animationTimer = 0;
                _isInAnimation = !_isInAnimation;
                somethingChanged = true;
            }
        }
    }
}
