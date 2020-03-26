using System;

namespace CountEvent
{
    class CountEventProgram
    {
        static void Main(string[] args)
        {
            Counter counter = new Counter();
            Handler1 handler1 = new Handler1();
            Handler2 handler2 = new Handler2();

            counter.OnCount += handler1.Message;
            counter.OnCount += handler2.Message;

            counter.Count();
        }
    }

    public delegate void CounterHandler(int stopPoint);

    public class Counter
    {
        public event CounterHandler OnCount;

        public void Count()
        {
            Random rnd = new Random();
            int stopPoint = rnd.Next(0, 100);

            for (int i = 0; i < 100; i++)
            {
                if (i == stopPoint)
                {
                    OnCount?.Invoke(stopPoint);
                }
            }
        }
    }

    public class Handler1
    {
        public void Message(int stopPoint)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Пора действовать, ведь уже {stopPoint}!\n");
            Console.ResetColor();
        }
    }

    public class Handler2
    {
        public void Message(int stopPoint)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Уже {stopPoint}, давно пора было начать!\n");
            Console.ResetColor();
        }
    }
}
