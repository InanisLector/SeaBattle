namespace SeaBattle
{
    public class BaseGameState
    {
        public readonly string name;
        protected readonly GameSceneManager SceneManager;
        protected readonly GameInfo info;

        protected bool somethingChanged = true;

        protected ConsoleKeyInfo inputKeys { get; private set; }

        public BaseGameState(string name, GameSceneManager sceneManager, GameInfo info)
        {
            this.name = name;
            this.SceneManager = sceneManager;
            this.info = info;
        }

        #region State logic

        public virtual void Enter()
        {
            somethingChanged = true;
        }

        public virtual void Update()
        {
            inputKeys = GetInput();
        }

        public virtual void CheckForRender()
        {
            if(!somethingChanged)
                return;

            Render();
            somethingChanged = false;
        }

        protected virtual void Render() { }

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
