using SnakeGame.Base;
using System.Reflection.Emit;
using System.Text;

namespace SnakeGame.Snake
{
    public enum SnakeDir
    {
        up, down, left, right
    }
    internal class SnakeGameplayState : BaseGameState
    {
        private const string _snakeSymbol = "\ud83d\udc0d";
        private const string _appleSymbol = "\ud83c\udf4e";

        private List<Cell> _body = new();
        private Cell _apple = new();
        private SnakeDir _currentDir = SnakeDir.right;
        private float _timeToMove;
        private Random _random = new();

        public int fieldWidth { get; set; }
        public int fieldHeight { get; set; }
        public int level { get; set; }
        public bool gameOver { get; private set; }
        public bool hasWon { get; private set; }

        public override void Reset()
        {
            _body.Clear();

            int middleX = fieldWidth / 2;
            int middleY = fieldHeight / 2;

            gameOver = false;
            hasWon = false;

            _currentDir = SnakeDir.right;

            _body.Add(new Cell(middleX, middleY));
            _apple = new(_random.Next(fieldWidth - 1), _random.Next(fieldHeight - 1));

            _timeToMove = 0f;
        }

        public override void Update(float deltaTime)
        {
            _timeToMove -= deltaTime;

            if (_timeToMove > 0f || gameOver)
                return;

            _timeToMove = 1f / (level + 4);

            Cell head = _body.First();
            Cell nextCell = ShiftTo(head, _currentDir);
            if (nextCell.Equals(_apple))
            {
                _body.Insert(0, _apple);
                hasWon = _body.Count >= level + 3;
                GenerateApple();
                return;
            }

            if (nextCell.x < 0 || nextCell.x >= fieldWidth || nextCell.y < 0 || nextCell.y >= fieldHeight)
            {
                gameOver = true;
                return;
            }
            _body.RemoveAt(_body.Count - 1);
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
            _currentDir = dir;
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
            renderer.DrawString($"LEVEL: {level}", 0, 0, ConsoleColor.White);
            renderer.DrawString($"SCORE: {_body.Count - 1}", 0, 1, ConsoleColor.White);
            Console.OutputEncoding = Encoding.Unicode;
            foreach (Cell cell in _body)
            {
                int restrictedX = Math.Clamp(cell.x, 0, fieldWidth - 1);
                int restrictedY = Math.Clamp(cell.y, 0, fieldHeight - 1);
                renderer.SetPixel(restrictedX, restrictedY, _snakeSymbol, 1);
            }
            renderer.SetPixel(_apple.x, _apple.y, _appleSymbol, 0);

        }

        private void GenerateApple()
        {
            Cell cell = new Cell();
            cell.x = _random.Next(fieldWidth - 1);
            cell.y = _random.Next(fieldHeight - 1);
            if (cell.Equals(_body.First()))
            {
                if (cell.y > fieldHeight / 2)
                    cell.y--;
                else
                    cell.y++;
            }
            _apple = cell;
        }

        public override bool IsDone()
        {
            return gameOver || hasWon;
        }
    }
}