namespace SeaBattle
{
    public class ExitMenu : BaseGameState
    {
        private int moveInput;
        private bool selectInput;
        private bool deselectInput;

        private int _animationTimer = 0;
        private const int animationDelay = 300;
        private bool _isInAnimation = false;

        private ExitMenuOptions currentOption;
        public enum ExitMenuOptions
        {
            Back, 
            Exit
        }
        public const int ExitMenuOptionsLen = 2;


        public ExitMenu(GameSceneManager sceneManager, GameInfo info) :
            base("exit menu", sceneManager, info) { }

        public override void Update()
        {
            base.Update();

            moveInput = GetMoveInput();
            selectInput = GetSelectInput();
            deselectInput = GetDeselectInput();

            ChangeOption();
            SelectOption();
            DeselectOption();

            AnimationTimer();
        }

        protected override void Render()
        {
            Renderers.RenderExitMenu(currentOption, _isInAnimation);
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

        private bool GetDeselectInput()
        {
            return inputKeys.Key switch
            {
                ConsoleKey.A => true,
                ConsoleKey.LeftArrow => true,
                ConsoleKey.Escape => true,
                _ => false
            };
        }

        #endregion

        #region Menu navigation

        private void ChangeOption()
        {
            if (moveInput == 0)
                return;

            currentOption += moveInput;

            currentOption = currentOption < 0 ? currentOption + ExitMenuOptionsLen : currentOption;
            currentOption = currentOption >= (ExitMenuOptions)ExitMenuOptionsLen ? 0 : currentOption;

            somethingChanged = true;
        }
        private void SelectOption()
        {
            if (!selectInput)
                return;

            switch (currentOption)
            {
                case ExitMenuOptions.Back:
                    SceneManager.ChangeCurrentState(SceneManager.mainMenu);
                    break;

                case ExitMenuOptions.Exit:
                    SceneManager.EndProcess();
                    break;

                default:
                    throw new IndexOutOfRangeException();
            }
        }

        private void DeselectOption()
        {
            if (deselectInput)
                SceneManager.ChangeCurrentState(SceneManager.mainMenu);
        }

        #endregion
        
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
