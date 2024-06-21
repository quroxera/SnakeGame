using SnakeGame.Base;

namespace SnakeGame.Snake
{
    internal class SnakeGameLogic : BaseGameLogic
    {
        private SnakeGameplayState gameplayState = new SnakeGameplayState();

        public override void OnArrowUp()
        {
            gameplayState.SetDirection(SnakeDir.up);
        }

        public override void OnArrowDown()
        {
            gameplayState.SetDirection(SnakeDir.down);
        }

        public override void OnArrowLeft()
        {
            gameplayState.SetDirection(SnakeDir.left);
        }

        public override void OnArrowRight()
        {
            gameplayState.SetDirection(SnakeDir.right);
        }

        public override void Update(float deltaTime)
        {
            gameplayState.Update(deltaTime);
        }

        public void GotoGameplay()
        {
            gameplayState.Reset();
        }
    }
}