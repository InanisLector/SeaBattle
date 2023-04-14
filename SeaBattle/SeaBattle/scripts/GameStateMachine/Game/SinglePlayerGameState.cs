using IntVector2;

namespace SeaBattle.scripts.GameStateMachine.Game
{
    public class SinglePlayerGameState : BaseGameState
    {
        private Vector2 playerPosition;
        private Map map;

        public SinglePlayerGameState(GameSceneManager sceneManager, GameInfo info) :
            base("single player", sceneManager, info) { }

        #region State logic

        public override void Enter()
        {
            base.Enter();

            map = new();
        }

        public override void Update()
        {
            base.Update();

            (moveInput, selectInput, deselectInput) = inputKeys.GameInputHandler();

            MovePlayer();
            Shoot();
        }
        
        protected override void Render()
        {
            base.Render();

            map.RenderSinglePlayer(playerPosition);
        }

        #endregion

        private void MovePlayer()
        {
            if(moveInput == Vector2.Zero)
                return;

            somethingChanged = true;

            playerPosition += moveInput;

            playerPosition.x = playerPosition.x < 0 ? 0 : playerPosition.x;
            playerPosition.y = playerPosition.y < 0 ? 0 : playerPosition.y;
            playerPosition.x = playerPosition.x < Map.Width ? playerPosition.x : Map.Width;
            playerPosition.y = playerPosition.y < Map.Height ? playerPosition.y : Map.Height;
        }

        private void Shoot()
        {
            if(!selectInput)
                return;

            somethingChanged = true;

            map.Shoot(playerPosition);
        }
    }
}
