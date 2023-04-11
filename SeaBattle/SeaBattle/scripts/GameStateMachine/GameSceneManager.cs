﻿namespace SeaBattle
{
    public class GameSceneManager
    {
        public MainMenu mainMenu { get; private set; }
        public ExitMenu exitMenu { get; private set; }
        public PlayMenu playMenu { get; private set; }

        private BaseGameState _currentState;

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
            Thread.Sleep(1);
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
        
        #endregion
    }
}
