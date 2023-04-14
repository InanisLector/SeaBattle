namespace SeaBattle.scripts.GameStateMachine.Menu
{
    public class MainMenu : BaseGameState
    {
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

            (moveInput.y, selectInput, deselectInput) = inputKeys.MenuInputHandler();

            ChangeOption();
            SelectOption();
        }

        protected override void Render()
        {
            Renderers.RenderMainMenu(currentOption, _isInAnimation);
        }

        #region Menu navigation
        private void ChangeOption()
        {
            if (moveInput.y == 0)
                return;

            currentOption += moveInput.y;

            currentOption = (MainMenuOptions)(((int)currentOption + 4) % 4);

            somethingChanged = true;
        }

        private void SelectOption()
        {
            if (!selectInput)
                return;

            switch (currentOption)
            {
                case MainMenuOptions.Play:
                    sceneManager.ChangeCurrentState(sceneManager.playMenu);
                    break;

                case MainMenuOptions.Settings:
                    break;

                case MainMenuOptions.Exit:
                    sceneManager.ChangeCurrentState(sceneManager.exitMenu);
                    break;

                default:
                    throw new IndexOutOfRangeException();
            }
        }

        #endregion
    }
}
