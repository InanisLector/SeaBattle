namespace SeaBattle.scripts.GameStateMachine.Menu
{
    public class PlayMenu : BaseGameState
    {
        private PlayMenuOptions currentOption;

        public enum PlayMenuOptions
        {
            SinglePlayer,
            Multiplayer
        }
        public const int PlayMenuOptionsLen = 2;

        public PlayMenu(GameSceneManager sceneManager, GameInfo info) :
            base("play menu", sceneManager, info) { }

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
            Renderers.RenderPlayMenu(currentOption, _isInAnimation);
        }
        

        #region Menu navigation

        private void ChangeOption()
        {
            if (moveInput.y == 0)
                return;

            currentOption += moveInput.y;

            currentOption = (PlayMenuOptions)((int)(currentOption + PlayMenuOptionsLen) % PlayMenuOptionsLen);

            somethingChanged = true;
        }
        private void SelectOption()
        {
            if (!selectInput)
                return;

            switch (currentOption)
            {
                case PlayMenuOptions.SinglePlayer:
                    sceneManager.ChangeCurrentState(sceneManager.singlePlayer);
                    break;

                case PlayMenuOptions.Multiplayer:
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
    }
}