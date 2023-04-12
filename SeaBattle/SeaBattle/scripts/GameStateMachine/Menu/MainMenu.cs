namespace SeaBattle
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

            (moveInput, selectInput, deselectInput) = inputKeys.MenuInputHandler();

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
            if (moveInput == 0)
                return;

            currentOption += moveInput;

            currentOption = currentOption < 0 ? currentOption + MainMenuOptionsLen : currentOption;
            currentOption = currentOption >= (MainMenuOptions)MainMenuOptionsLen ? 0 : currentOption;

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
