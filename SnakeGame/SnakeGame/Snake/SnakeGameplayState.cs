using SnakeGame.Base;

namespace SnakeGame.Snake
{
    public enum SnakeDir
    {
        up, down, left, right
    }
    internal class SnakeGameplayState : BaseGameState
    {
        private List<Cell> _body = new();
        private SnakeDir currentDir = SnakeDir.right;
        private float timeToMove;

        public int fieldWidth;
        public int fieldHeight;

        private const char snakeSymbol = '■';
        public override void Reset()
        {
            _body.Clear();

            int middleX = fieldWidth / 2;
            int middleY = fieldHeight / 2;

            currentDir = SnakeDir.right;

            _body.Add(new Cell(middleX, middleY));

            timeToMove = 0f;
        }

        public override void Update(float deltaTime)
        {
            timeToMove -= deltaTime;

            if (timeToMove > 0f)
                return;

            timeToMove = 1f / 5f;

            Cell head = _body.First();
            Cell nextCell = ShiftTo(head, currentDir);

            _body.Remove(_body.Last());
            _body.Insert(0, nextCell);
        }

        private struct Cell
        {
            public int x;
            public int y;

            public Cell(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public void SetDirection(SnakeDir dir)
        {
            currentDir = dir;
        }

        private Cell ShiftTo(Cell startDir, SnakeDir endDir)
        {
            switch (endDir)
            {
                case SnakeDir.up:
                    return new Cell(startDir.x, startDir.y - 1);
                case SnakeDir.down:
                    return new Cell(startDir.x, startDir.y + 1);
                case SnakeDir.left:
                    return new Cell(startDir.x - 1, startDir.y);
                case SnakeDir.right:
                    return new Cell(startDir.x + 1, startDir.y);
            }
            return startDir;
        }

        public override void Draw(ConsoleRenderer renderer)
        {
            foreach (Cell cell in _body)
            {
                int restrictedX = Math.Clamp(cell.x, 0, fieldWidth - 1);
                int restrictedY = Math.Clamp(cell.y, 0, fieldHeight - 1);

                renderer.SetPixel(restrictedX, restrictedY, snakeSymbol, 1);
            }
        }
    }
}