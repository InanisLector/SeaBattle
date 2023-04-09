namespace GameStateMachine
{
    public class GameStateMachine
    {
        public MainMenu mainMenu;

        private BaseGameState _currentState;

        public GameStateMachine()
        {
            InitializeStates();

            _currentState = GetInitialState();
        }

        private void InitializeStates()
        {
            GameInfo info = new GameInfo();

            mainMenu = new();
        }

        private BaseGameState GetInitialState()
            => mainMenu;
    }
}
