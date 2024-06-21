using SnakeGame.Base;

namespace SnakeGame.Snake
{
    internal class SnakeGameLogic : BaseGameLogic
    {
        private SnakeGameplayState gameplayState = new SnakeGameplayState();

        public override void OnArrowUp()
        {
            if (currentState != gameplayState)
                return;

            gameplayState.SetDirection(SnakeDir.up);
        }

        public override void OnArrowDown()
        {
            if (currentState != gameplayState)
                return;

            gameplayState.SetDirection(SnakeDir.down);
        }

        public override void OnArrowLeft()
        {
            if (currentState != gameplayState)
                return;

            gameplayState.SetDirection(SnakeDir.left);
        }

        public override void OnArrowRight()
        {
            if (currentState != gameplayState)
                return;

            gameplayState.SetDirection(SnakeDir.right);
        }

        public override void Update(float deltaTime)
        {
            if (currentState != gameplayState)
                GotoGameplay();
        }

        private void GotoGameplay()
        {
            gameplayState.fieldWidth = screenWigth;
            gameplayState.fieldHeight = screenHeight;
            ChangeState(gameplayState);
            gameplayState.Reset();
        }

        public override ConsoleColor[] CreatePallet()
        {
            return
            [
                ConsoleColor.DarkRed, 
                ConsoleColor.DarkGreen, 
                ConsoleColor.DarkBlue, 
                ConsoleColor.DarkMagenta,
            ];
        }
    }
}