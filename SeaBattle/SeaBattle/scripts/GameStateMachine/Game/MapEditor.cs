using IntVector2;
using SeaBattle.scripts;

namespace SeaBattle.scripts.GameStateMachine.Game
{
    public class MapEditor : BaseGameState
    {
        private bool isChoosingShip = true;
        private int currentShipType = 3;
        private int[] shipsAvailable = new int[4]; // Ship size corresponds (ship size = index - 1)

        public MapEditor(GameSceneManager sceneManager, GameInfo info) :
            base("map editor", sceneManager, info) { }

        public override void Enter()
        {
            base.Enter();

            info.player1map = new Map();
            
            for(int i = 0; i < 4; i++)
                shipsAvailable[i] = 4-i;
        }

        public override void Update()
        {
            base.Update();

            (moveInput, selectInput, deselectInput) = inputKeys.GameInputHandler();

            if (isChoosingShip)
                ShipChoosingUpdate();
            else
                ShipPlacingUpdate();
        }

        protected override void Render()
        {
            base.Render();

            info.player1map.RenderMapEditor();
        }

        #region Ship choosing

        private void ShipChoosingUpdate()
        {
            ChangeShip();
            PickShip();
            ExitGame();
        }

        private void ChangeShip()
        {
            if(moveInput == Vector2.Zero)
                return;

            currentShipType += moveInput.x + moveInput.y;

            currentShipType = (currentShipType + 4) % 4;
        }

        private void PickShip()
            => isChoosingShip = !selectInput;

        private void ExitGame() // TODO: add two step exit from map editor
        {

        }

        #endregion

        #region Ship placing
        
        private void ShipPlacingUpdate()
        {
            
        }

        #endregion
    }
}