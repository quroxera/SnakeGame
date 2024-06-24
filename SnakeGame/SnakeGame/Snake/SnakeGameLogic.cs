using Base;
using SnakeGame.Base;

namespace SnakeGame.Snake
{
    internal class SnakeGameLogic : BaseGameLogic
    {
        private SnakeGameplayState _gameplayState = new SnakeGameplayState();
        private bool _newGamePending = false;
        private int _currentLevel = 0;
        private ShowTextState _showTextState = new(2f);

        public override void OnArrowUp()
        {
            if (currentState != _gameplayState)
                return;

            _gameplayState.SetDirection(SnakeDir.up);
        }

        public override void OnArrowDown()
        {
            if (currentState != _gameplayState)
                return;

            _gameplayState.SetDirection(SnakeDir.down);
        }

        public override void OnArrowLeft()
        {
            if (currentState != _gameplayState)
                return;

            _gameplayState.SetDirection(SnakeDir.left);
        }

        public override void OnArrowRight()
        {
            if (currentState != _gameplayState)
                return;

            _gameplayState.SetDirection(SnakeDir.right);
        }

        public override void Update(float deltaTime)
        {
            if (currentState != null && !currentState.IsDone())
                return;

            if (currentState == null || currentState == _gameplayState && !_gameplayState.gameOver)
                GoToNextLevel();
            else if (currentState == _gameplayState && _gameplayState.gameOver)
                GoToGameOver();
            else if (currentState != _gameplayState && _newGamePending)
                GoToNextLevel();
            else if (currentState != _gameplayState && !_newGamePending)
                GoToGameplay();
        }

        public override ConsoleColor[] CreatePallet()
        {
            return
            [
                ConsoleColor.DarkRed, 
                ConsoleColor.DarkGreen, 
                ConsoleColor.DarkBlue, 
                ConsoleColor.White,
            ];
        }

        private void GoToGameplay()
        {
            _gameplayState.level = _currentLevel;
            _gameplayState.fieldWidth = screenWigth;
            _gameplayState.fieldHeight = screenHeight;
            ChangeState(_gameplayState);
            _gameplayState.Reset();
        }

        private void GoToGameOver()
        {
            _currentLevel = 0;
            _newGamePending = true;
            _showTextState.text = "GAME OVER";
            ChangeState(_showTextState);
        }

        private void GoToNextLevel()
        {
            _currentLevel++;
            _newGamePending = false;
            _showTextState.text = $"LEVEL {_currentLevel}";
            ChangeState(_showTextState);
        }
    }
}