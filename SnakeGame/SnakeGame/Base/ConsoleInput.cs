namespace SnakeGame.Base
{
    internal class ConsoleInput
    {
        public interface IArrowListener
        {
            abstract void OnArrowUp();
            abstract void OnArrowDown();
            abstract void OnArrowLeft();
            abstract void OnArrowRight();
        }

        private List<IArrowListener> arrowListenersList = new();

        public void Subscribe(IArrowListener arrowListener)
        {
            arrowListenersList.Add(arrowListener);
        }

        public void Update()
        {
            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        foreach (IArrowListener arrowListener in arrowListenersList)
                            arrowListener.OnArrowUp();
                        break;
                    case ConsoleKey.DownArrow:
                        foreach (IArrowListener arrowListener in arrowListenersList)
                            arrowListener.OnArrowDown();
                        break;
                    case ConsoleKey.LeftArrow:
                        foreach (IArrowListener arrowListener in arrowListenersList)
                            arrowListener.OnArrowLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        foreach (IArrowListener arrowListener in arrowListenersList)
                            arrowListener.OnArrowRight();
                        break;
                }
            }
        }
    }
}