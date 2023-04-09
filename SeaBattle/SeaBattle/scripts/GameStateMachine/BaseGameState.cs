namespace GameStateMachine
{
    public class BaseGameState
    {
        public readonly string name;
        protected readonly GameStateMachine stateMachine;
        protected readonly GameInfo info;

        protected ConsoleKeyInfo inputKeys { get; private set; }

        public BaseGameState(string name, GameStateMachine stateMachine, GameInfo info)
        {
            this.name = name;
            this.stateMachine = stateMachine;
            this.info = info;
        }
    }
}
