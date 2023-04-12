namespace SeaBattle
{
    public class BaseGameState
    {
        public readonly string name;
        protected readonly GameSceneManager sceneManager;
        protected readonly GameInfo info;

        protected bool somethingChanged = true;

        protected int moveInput;
        protected bool selectInput;
        protected bool deselectInput;

        protected int _animationTimer = 0;
        protected const int animationDelay = 300;
        protected bool _isInAnimation = false;

        protected ConsoleKeyInfo inputKeys { get; private set; }

        public BaseGameState(string name, GameSceneManager sceneManager, GameInfo info)
        {
            this.name = name;
            this.sceneManager = sceneManager;
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
            AnimationTimer();

            if(!somethingChanged)
                return;

            Render();
            somethingChanged = false;
        }

        protected virtual void Render() { }

        public virtual void Exit() { }

        #endregion

        #region Inputs

        private ConsoleKeyInfo GetInput()
        {
            if (Console.KeyAvailable)
                return Console.ReadKey(true);

            return new ConsoleKeyInfo();
        }

        #endregion

        protected virtual void AnimationTimer()
        {
            if (somethingChanged)
            {
                _animationTimer = 0;
                _isInAnimation = false;
                return;
            }

            _animationTimer += Time.deltaTime;

            if (_animationTimer > animationDelay)
            {
                _animationTimer = 0;
                _isInAnimation = !_isInAnimation;
                somethingChanged = true;
            }
        }
    }

    public static class InputHandlers
    {
        #region Menu input

        public static (int direction, bool select, bool deselect) MenuInputHandler(this ConsoleKeyInfo keyInput)
            => (GetMoveInput(keyInput), GetSelectInput(keyInput), GetDeselectInput(keyInput));

        private static int GetMoveInput(ConsoleKeyInfo inputKeys)
        {
            return inputKeys.Key switch
            {
                ConsoleKey.W => -1,
                ConsoleKey.UpArrow => -1,
                ConsoleKey.S => 1,
                ConsoleKey.DownArrow => 1,
                _ => 0
            };
        }

        private static bool GetSelectInput(ConsoleKeyInfo inputKeys)
        {
            return inputKeys.Key switch
            {
                ConsoleKey.D => true,
                ConsoleKey.RightArrow => true,
                ConsoleKey.Enter => true,
                _ => false
            };
        }

        private static bool GetDeselectInput(ConsoleKeyInfo inputKeys)
        {
            return inputKeys.Key switch
            {
                ConsoleKey.A => true,
                ConsoleKey.LeftArrow => true,
                ConsoleKey.Escape => true,
                _ => false
            };
        }

        #endregion
    }
}
