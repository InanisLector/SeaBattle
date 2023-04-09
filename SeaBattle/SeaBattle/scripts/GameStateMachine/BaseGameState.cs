namespace SeaBattle
{
    public class BaseGameState
    {
        public readonly string name;
        protected readonly GameStateMachine stateMachine;
        protected readonly GameInfo info;

        protected bool somethingChanged = true;

        protected ConsoleKeyInfo inputKeys { get; private set; }

        public BaseGameState(string name, GameStateMachine stateMachine, GameInfo info)
        {
            this.name = name;
            this.stateMachine = stateMachine;
            this.info = info;
        }

        #region State logic

        public virtual void Enter() { }

        public virtual void Update()
        {
            inputKeys = GetInput();
        }

        public virtual void Renderer()
        {
            if(!somethingChanged)
                return;

            somethingChanged = false;
        }

        public virtual void Exit() { }

        #endregion

        private ConsoleKeyInfo GetInput()
        {
            if (Console.KeyAvailable)
                return Console.ReadKey(true);

            return new ConsoleKeyInfo();

            
        }

    }
}
