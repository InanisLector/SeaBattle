namespace SeaBattle
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

            mainMenu = new(this, info);
        }

        private BaseGameState GetInitialState()
            => mainMenu;

        #region State machine logic
        
        public void StartGameCycle()
        {
            while (true)
            {
                Update();
            }
        }

        public void Update()
        {
            Time.NewFrame();
            _currentState.Update();
            _currentState.Renderer();
        }

        public void ChangeCurrentState(BaseGameState newState)
        {
            _currentState.Exit();
            _currentState = newState;
            _currentState.Enter();
            _currentState.Update();
        }
        
        #endregion
    }
}
