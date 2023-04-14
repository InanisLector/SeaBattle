namespace SeaBattle.scripts.GameStateMachine.Menu
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

            (moveInput.y, selectInput, deselectInput) = inputKeys.MenuInputHandler();

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
            if (moveInput.y == 0)
                return;

            currentOption += moveInput.y;

            currentOption = (ExitMenuOptions)(((int)currentOption + 4) % 4);

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
