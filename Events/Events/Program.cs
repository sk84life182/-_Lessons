using System;

namespace Events
{
    class Program
    {
        static void Main(string[] args)
        {
            Counter counter = new Counter();

            counter.Handler1 += Counter_Handler1;
            counter.Handler2 += Counter_Handler2;

            counter.StopFunction();
        }

        private static void Counter_Handler1(object sender, Whatever e)
        {
            Console.WriteLine(e.Message);
        }

        private static void Counter_Handler2(object sender, Whatever e)
        {
            Console.WriteLine(e.Message);
        }

    }

    public delegate void CounterHandler();
    public delegate void ShowMessage(string message);

    public class Counter
    {
        public int stopPoint { get; set; }

        public void StopFunction()
        {
            Random rnd = new Random();
            int stopPoint = rnd.Next(0, 100);

            for (int i = 0; i < 100; i++)
            {
                if (i == stopPoint)
                {
                    Handler1?.Invoke(this, new Whatever($"Пора действовать, ведь уже {stopPoint}!"));
                    Handler2?.Invoke(this, new Whatever($"Уже {stopPoint}, давно пора было начать!"));
                }
            }
        }

        public event EventHandler<Whatever> Handler1;
        public event EventHandler<Whatever> Handler2;
    }

    public class Whatever : EventArgs
    {
        public Whatever(string message)
        {
            Message = message;
        }
        public string Message { get; private set; }
    }

    //class Event
    //{
    //    public event CounterHandler TimeEvent;

    //    public void OnTimeEvent()
    //    {
    //        TimeEvent();
    //    }
    //}

    //public class Counter
    //{
    //    public void Handler1()
    //    {
    //        Random rnd = new Random();
    //        int stopPoint = rnd.Next(0, 100);

    //        for (int i = 0; i < 100; i++)
    //        {
    //            if (i == stopPoint)
    //            {
    //                Console.WriteLine($"Пора действовать, ведь уже {stopPoint}!");
    //            }
    //        }
    //    }
    //}
}
