using static SnakeGame.Base.ConsoleInput;

namespace SnakeGame.Base
{
    internal abstract class BaseGameLogic : IArrowListener
    {
        private float time { get; set; }
        public int screenWigth { get; set; }
        public int screenHeight { get; set; }

        public BaseGameState? currentState;

        public abstract void OnArrowDown();
        public abstract void OnArrowLeft();
        public abstract void OnArrowRight();
        public abstract void OnArrowUp();
        public abstract void Update(float deltaTime);
        public abstract ConsoleColor[] CreatePallet();

        public void InitializeInput(ConsoleInput input)
        {
            input.Subscribe(this);
        }

        public void ChangeState(BaseGameState state)
        {
            currentState?.Reset();
            currentState = state;
        }

        public void DrawNewState(float deltaTime, ConsoleRenderer renderer)
        {
            deltaTime += time;

            screenWigth = renderer.width;
            screenHeight = renderer.height;

            currentState?.Update(deltaTime);
            currentState?.Draw(renderer);

            Update(deltaTime);
        }
    }
}