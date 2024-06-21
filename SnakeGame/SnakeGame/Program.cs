using SnakeGame.Base;
using SnakeGame.Snake;

namespace SnakeGame
{
    class Program
    {
        const float targetFrameTime = 1f / 60f;
        static void Main()
        {
            SnakeGameLogic gameLogic = new();
            ConsoleColor[] palette = gameLogic.CreatePallet();

            ConsoleRenderer renderer0 = new(palette);
            ConsoleRenderer renderer1 = new(palette);
            ConsoleRenderer prevRenderer = renderer0;
            ConsoleRenderer currRenderer = renderer1;

            ConsoleInput input = new ConsoleInput();
            gameLogic.InitializeInput(input);
            DateTime lastFrameTime = DateTime.Now;

            while (true)
            {
                DateTime frameStartTime = DateTime.Now;
                float deltaTime = (float)(frameStartTime - lastFrameTime).TotalSeconds;
                input.Update();
                lastFrameTime = frameStartTime;
                gameLogic.DrawNewState(deltaTime, currRenderer);

                if (currRenderer != prevRenderer)
                    currRenderer.Render();

                ConsoleRenderer tmp = prevRenderer;
                prevRenderer = currRenderer;
                currRenderer = tmp;
                currRenderer.Clear();

                DateTime nextFrameTime = frameStartTime.AddSeconds(targetFrameTime);
                DateTime endFrameTime = DateTime.Now;

                if (nextFrameTime > endFrameTime)
                    Thread.Sleep((int)(nextFrameTime - endFrameTime).TotalMilliseconds);
            }
        }
    }
}