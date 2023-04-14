using SeaBattle.scripts.GameStateMachine.Game;
using SeaBattle.scripts.GameStateMachine.Menu;

namespace SeaBattle.scripts.GameStateMachine
{
    public class GameSceneManager
    {
        public MainMenu mainMenu { get; private set; }
        public ExitMenu exitMenu { get; private set; }
        public PlayMenu playMenu { get; private set; }
        public MapEditor mapEditor { get; private set; }
        public  SinglePlayerGameState singlePlayer { get; private set; }

        private BaseGameState _currentState;

        private bool isWorking = true;

        public GameSceneManager()
        {
            InitializeStates();

            _currentState = GetInitialState();
        }

        private void InitializeStates()
        {
            GameInfo info = new GameInfo();

            mainMenu = new(this, info);
            exitMenu = new(this, info);
            playMenu = new(this, info);
            mapEditor = new(this, info);
            singlePlayer = new(this, info);
        }

        private BaseGameState GetInitialState()
            => mainMenu;

        #region State machine logic
        
        public void StartGameCycle()
        {
            while (isWorking)
            {
                Update();
            }

            Environment.Exit(0);
        }

        public void Update()
        {
            Thread.Sleep(1); // For proper animations
            Time.NewFrame();
            _currentState.Update();
            _currentState.CheckForRender();
        }

        public void ChangeCurrentState(BaseGameState newState)
        {
            _currentState.Exit();
            _currentState = newState;
            _currentState.Enter();
            _currentState.Update();
        }

        public void EndProcess()
        {
            isWorking = false;
        }
        
        #endregion
    }
}
