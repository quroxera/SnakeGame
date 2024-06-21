using SnakeGame.Base;
using SnakeGame.Snake;

namespace SnakeGame
{
    class Program
    {
        static void Main()
        {
            SnakeGameLogic gameLogic = new SnakeGameLogic();
            ConsoleInput input = new ConsoleInput();
            gameLogic.InitializeInput(input);
            DateTime lastFrameTime = DateTime.Now;
            gameLogic.GotoGameplay();

            while (true)
            {
                input.Update();
                DateTime frameStartTime = DateTime.Now;
                float deltaTime = (float)(frameStartTime - lastFrameTime).TotalSeconds;
                gameLogic.Update(deltaTime);
                lastFrameTime = frameStartTime;
            }
        }
    }
}