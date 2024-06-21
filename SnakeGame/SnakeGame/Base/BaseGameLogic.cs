using static SnakeGame.Base.ConsoleInput;

namespace SnakeGame.Base
{
    internal abstract class BaseGameLogic : IArrowListener
    {
        protected float time { get; private set; }
        protected int screenWigth { get; private set; }
        protected int screenHeight { get; private set; }
        protected BaseGameState? currentState { get; private set; }

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