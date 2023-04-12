using static SeaBattle.ExitMenu;

namespace SeaBattle
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

            (moveInput, selectInput, deselectInput) = inputKeys.MenuInputHandler();

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
            if (moveInput == 0)
                return;

            currentOption += moveInput;

            currentOption = currentOption < 0 ? currentOption + ExitMenuOptionsLen : currentOption;
            currentOption = currentOption >= (PlayMenuOptions)PlayMenuOptionsLen ? 0 : currentOption;

            somethingChanged = true;
        }
        private void SelectOption()
        {
            if (!selectInput)
                return;

            switch (currentOption)
            {
                case PlayMenuOptions.SinglePlayer:
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