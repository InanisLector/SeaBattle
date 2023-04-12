namespace SeaBattle
{
    public class ExitMenu : BaseGameState
    {
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

            (moveInput, selectInput, deselectInput) = inputKeys.MenuInputHandler();

            ChangeOption();
            SelectOption();
            DeselectOption();
        }

        protected override void Render()
        {
            Renderers.RenderExitMenu(currentOption, _isInAnimation);
        }

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
                    sceneManager.ChangeCurrentState(sceneManager.mainMenu);
                    break;

                case ExitMenuOptions.Exit:
                    sceneManager.EndProcess();
                    break;

                default:
                    throw new IndexOutOfRangeException();
            }
        }

        private void DeselectOption()
        {
            if (deselectInput)
                sceneManager.ChangeCurrentState(sceneManager.mainMenu);
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
