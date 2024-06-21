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

        public override void Reset()
        {
            _body.Clear();
            currentDir = SnakeDir.right;
            _body.Add(new Cell(0, 0));
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

            Console.WriteLine($"{_body.First().x}, {_body.First().y}");
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
                    return new Cell(startDir.x, startDir.y + 1);
                case SnakeDir.down:
                    return new Cell(startDir.x, startDir.y - 1);
                case SnakeDir.left:
                    return new Cell(startDir.x - 1, startDir.y);
                case SnakeDir.right:
                    return new Cell(startDir.x + 1, startDir.y);
            }
            return startDir;
        }
    }
}